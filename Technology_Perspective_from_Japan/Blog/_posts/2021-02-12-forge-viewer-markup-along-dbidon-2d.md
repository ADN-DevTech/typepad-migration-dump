---
layout: "post"
title: "Forge Viewer：dbid に沿ったマークアップの 2D 上の表示"
date: "2021-02-12 00:20:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/02/forge-viewer-markup-along-dbidon-2d.html "
typepad_basename: "forge-viewer-markup-along-dbidon-2d"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-markup-along-dbid.html" rel="noopener" target="_blank"><strong>Forge Viewer：dbid に沿ったマークアップの表示</strong></a> でご紹介した内容は、あいにく、3D 環境にのみに対応するものです。要素に対するフラグメントの関連付けは 2D 環境と 3D 環境とで異なっているため、そのまま getFragmentList() だけでは&#0160; 2D シート/図面上の要素から適切なフラグメント情報を得ることが出来ません。</p>
<p>3D 環境の場合、メッシュが大きすぎたり、異なるマテリアルを使用したりすると、1 つの要素（1 つの dbid）を複数のフラグメントに分割して形状を表現しています。一方、2D 環境の場合、1 つの要素が 1 つのフラグメントで表現されることがほとんどなためです。</p>
<p>2D 環境で dbid から要素に対するフラグメントの Bounding Box （境界ボックス）を取得し、その中心座標を得るには VertexBufferReader() を利用する次のコードを利用することが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">                                      :
    _viewer.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, onSelected);
                                      :
    function onSelected(event) {
        var dbIdArray = event.dbIdArray;
        if (dbIdArray.length &gt; 0) {
            console.log(&quot;dbId = &quot; + dbIdArray[0]);
            console.log(<strong>get2DBounds(dbIdArray[0], _viewer.model).center()</strong>);
        }
    }
                                      :

function find2DBounds(fragList, fragId, dbId, bc) {
    const mesh = fragList.getVizmesh(fragId);
    const vbr = new Autodesk.Viewing.Private.VertexBufferReader(mesh.geometry);
    vbr.enumGeomsForObject(dbId, bc);
}

function get2DBounds(dbId, model, useInstancing) {
    const it = model.getData().instanceTree;
    const fragList = model.getFragmentList();

    let bounds = new THREE.Box3();
    let bc = new Autodesk.Viewing.Private.BoundsCallback(bounds);
    const dbId2fragId = model.getData().fragments.dbId2fragId;
    const remappedId = model.reverseMapDbId(dbId);
    const fragIds = dbId2fragId[remappedId];

    if (Array.isArray(fragIds)) {
        for (let i = 0; i &lt; fragIds.length; i++) {
            find2DBounds(fragList, fragIds[i], remappedId, bc);
        }
    } else if (typeof fragIds === &#39;number&#39;) {
        find2DBounds(fragList, fragIds, remappedId, bc);
    }

    return bc.bounds;
}
</code></pre>
<p>先のブログの IconMarkupExtension エクステンションをそのまま流用する場合には、IconMarkupExtension.js 内に次のようなかたちで実装を追加、修正する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebc57da200c-pi" style="display: inline;"><img alt="2d_markups" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebc57da200c image-full img-responsive" src="/assets/image_324314.jpg" title="2d_markups" /></a></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">class IconMarkupExtension extends Autodesk.Viewing.Extension {
    constructor(viewer, options) {
        super(viewer, options);
        this._group = null;
        this._button = null;
        this._icons = options.icons || [];
    }

    load() {
        const updateIconsCallback = () =&gt; {
            if (this._enabled) {
                this.updateIcons();
            }
        };
        this.viewer.addEventListener(Autodesk.Viewing.CAMERA_CHANGE_EVENT, updateIconsCallback);
        this.viewer.addEventListener(Autodesk.Viewing.ISOLATE_EVENT, updateIconsCallback);
        this.viewer.addEventListener(Autodesk.Viewing.HIDE_EVENT, updateIconsCallback);
        this.viewer.addEventListener(Autodesk.Viewing.SHOW_EVENT, updateIconsCallback);
        return true;
    }

    unload() {
        // Clean our UI elements if we added any
        if (this._group) {
            this._group.removeControl(this._button);
            if (this._group.getNumberOfControls() === 0) {
                this.viewer.toolbar.removeControl(this._group);
            }
        }

        return true;
    }

    onToolbarCreated() {
        // Create a new toolbar group if it doesn&#39;t exist
        this._group = this.viewer.toolbar.getControl(&#39;customExtensions&#39;);
        if (!this._group) {
            this._group = new Autodesk.Viewing.UI.ControlGroup(&#39;customExtensions&#39;);
            this.viewer.toolbar.addControl(this._group);
        }

        // Add a new button to the toolbar group
        this._button = new Autodesk.Viewing.UI.Button(&#39;IconExtension&#39;);
        this._button.onClick = (ev) =&gt; {
            this._enabled = !this._enabled;
            this.showIcons(this._enabled);
            this._button.setState(this._enabled ? 0 : 1);

        };
        this._button.setToolTip(this.options.button.tooltip);
        this._button.container.children[0].classList.add(&#39;fas&#39;, this.options.button.icon);
        this._group.addControl(this._button);
    }

    showIcons(show) {
        const $viewer = $(&#39;#&#39; + this.viewer.clientContainer.id + &#39; div.adsk-viewing-viewer&#39;);

        // remove previous...
        $(&#39;#&#39; + this.viewer.clientContainer.id + &#39; div.adsk-viewing-viewer label.markup&#39;).remove();
        if (!show) return;

        // do we have anything to show?
        if (this._icons === undefined || this.icons === null) return;

        // do we have access to the instance tree?
        const tree = this.viewer.model.getInstanceTree();
        if (tree === undefined) { console.log(&#39;Loading tree...&#39;); return; }

        const onClick = (e) =&gt; {
            if (this.options.onClick)
                this.options.onClick($(e.currentTarget).data(&#39;id&#39;));
        };

        this._frags = {}
        for (var i = 0; i &lt; this._icons.length; i++) {
            // we need to collect all the fragIds for a given dbId
            const icon = this._icons[i];
            this._frags[&#39;dbId&#39; + icon.dbId] = []

            // create the label for the dbId
            const $label = $(`<br />            &lt;label class=&quot;markup update&quot; data-id=&quot;${icon.dbId}&quot;&gt;<br />                &lt;span class=&quot;${icon.css}&quot;&gt; ${icon.label || &#39;&#39;}&lt;/span&gt;<br />            &lt;/label&gt;<br />            `);
            $label.css(&#39;display&#39;, this.viewer.isNodeVisible(icon.dbId) ? &#39;block&#39; : &#39;none&#39;);
            $label.on(&#39;click&#39;, onClick);
            $viewer.append($label);

            // now collect the fragIds
            const _this = this;
            tree.enumNodeFragments(icon.dbId, function (fragId) {
                _this._frags[&#39;dbId&#39; + icon.dbId].push(fragId);
                _this.updateIcons(); // re-position of each fragId found
            });
        }
    }

    getModifiedWorldBoundingBox(dbId) {
<strong>        var nodebBox = get2DBounds(dbId, this.viewer.model);</strong>        
        return nodebBox;
    }

    updateIcons() {
        for (const label of $(&#39;#&#39; + this.viewer.clientContainer.id + &#39; div.adsk-viewing-viewer .update&#39;)) {
            const $label = $(label);
            const id = $label.data(&#39;id&#39;);

            // get the center of the dbId (based on its fragIds bounding boxes)
            const pos = this.viewer.worldToClient(this.getModifiedWorldBoundingBox(id).center());

            // position the label center to it
            $label.css(&#39;left&#39;, Math.floor(pos.x - $label[0].offsetWidth / 2) + &#39;px&#39;);
            $label.css(&#39;top&#39;, Math.floor(pos.y - $label[0].offsetHeight / 2) + &#39;px&#39;);
            $label.css(&#39;display&#39;, this.viewer.isNodeVisible(id) ? &#39;block&#39; : &#39;none&#39;);
        }
    }

<strong>    find2DBounds(fragList, fragId, dbId, bc) {
        const mesh = fragList.getVizmesh(fragId);
        const vbr = new Autodesk.Viewing.Private.VertexBufferReader(mesh.geometry);
        vbr.enumGeomsForObject(dbId, bc);
    }

    get2DBounds(dbId, model, useInstancing) {
        const it = model.getData().instanceTree;
        const fragList = model.getFragmentList();

        let bounds = new THREE.Box3();
        let bc = new Autodesk.Viewing.Private.BoundsCallback(bounds);
        const dbId2fragId = model.getData().fragments.dbId2fragId;
        const remappedId = model.reverseMapDbId(dbId);
        const fragIds = dbId2fragId[remappedId];

        if (Array.isArray(fragIds)) {
            for (let i = 0; i &lt; fragIds.length; i++) {
                find2DBounds(fragList, fragIds[i], remappedId, bc);
            }
        } else if (typeof fragIds === &#39;number&#39;) {
            find2DBounds(fragList, fragIds, remappedId, bc);
        }

        return bc.bounds;
    }
</strong>
}

Autodesk.Viewing.theExtensionManager.registerExtension(&#39;IconMarkupExtension&#39;, IconMarkupExtension);


</code></pre>
<p>エクステンション呼び出し側の定義は、同じ手法を用いることが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.loadExtension(&#39;IconMarkupExtension&#39;, {
    button: {
        icon: &#39;fa-thermometer-half&#39;,
        tooltip: &#39;Show Temperature&#39;
    },
    icons: [
<strong>                { dbId: 83066, label: &#39;ささら&#39;, css: &#39;fa-2x temperatureOk fas temperatureBorder&#39; },
                { dbId: 6650, label: &#39;屋上&#39;, css: &#39;fa-2x temperatureOk fas temperatureBorder&#39; },
                { dbId: 2246, label: &#39;フェンス&#39;, css: &#39;fa-thermometer-half temperatureHigh fas temperatureBorder&#39; },
                { dbId: 1069, label: &#39;レベル線&#39;, css: &#39;fas temperatureHigh fa-2x faa-flash animated&#39; },
</strong>    ],
    onClick: (id) =&gt; {
        _viewer.select(id);
        _viewer.utilities.fitToView();
    }
});</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdebc5056200c-pi" style="display: inline;"><img alt="2d_markups" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdebc5056200c image-full img-responsive" src="/assets/image_810172.jpg" title="2d_markups" /></a></p>
<p>By Toshiaki Isezaki</p>
