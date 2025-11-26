---
layout: "post"
title: "Forge Viewer：dbid に沿ったマークアップの表示"
date: "2020-12-23 00:02:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-markup-along-dbid.html "
typepad_basename: "forge-viewer-markup-along-dbid"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-overlay-and-scene-builder.html" rel="noopener" target="_blank"><strong>Forge Viewer：オーバーレイとシーン ビルダー</strong> </a>では、Three.js メッシュを Forge Viewer カンバス上にオーバーレイ、または、シーン ビルダーを使って描画する方法をご紹介しました。この方法で、元のデザイン ファイルにない形状表現を実現することが可能になります。ただ、複雑な形状を多数配置すると Viewer 自体のパフォーマンスが低下したり、配置した肝心のメッシュが他のオブジェクトに紛れて目立たない、といった点も散見してきました。</p>
<p>Forge Viewer カンバス上の付加情報の表示という意味では、カンバス内に Three.js メッシュを表示する必要がない場合も存在します。デジタルツイン表現で特定のオブジェクトや位置にセンサー情報を明示したい場合などです。</p>
<p>Forge の登場以降、さまざまな方法が試行されてきましたが、ここでは、Three.js を使ったアプリでは比較的利用されている表現方法をご紹介しておきます。</p>
<p>Forge ブログでは、既に <a href="https://forge.autodesk.com/blog/placing-custom-markup-dbid" rel="noopener" target="_blank"><strong>Placing custom markup by dbId</strong></a> 記事として紹介されているものです。この方法を使用すると、dbid と関連付けを持つ「マークアップ」表現を次のように実現することが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42ee0de200d-pi" style="display: inline;"><img alt="Marker" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42ee0de200d image-full img-responsive" src="/assets/image_727201.jpg" title="Marker" /></a></p>
<p>仕組みとして、指定した dbid を使用して形状を構成する Three.js フラグメントを合成、その中心座標（ワールド座標）を&#0160; <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#worldtoclient-point-camera" rel="noopener" target="_blank">Viewer3D.worldToClient</a> メソッドで HTML のスクリーン座標に変換、<a href="https://developer.mozilla.org/ja/docs/Web/CSS/z-index" rel="noopener" target="_blank">z-index</a> 指定を用いて、カンバス上の適切な位置にオーバーレイすることで、CSS 表現された Label 要素をマークアップのように表示します。オービットやパンなどの画面操作には、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/#camera-change-event" rel="noopener" target="_blank">Autodesk.Viewing.CAMERA_CHANGE_EVENT</a> イベントを用いて検出、Label 位置を更新するものです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeb017f3200c-pi" style="display: inline;"><img alt="Scheme" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeb017f3200c image-full img-responsive" src="/assets/image_274288.jpg" title="Scheme" /></a></p>
<p>同ブログで紹介している実装は Forge Viewer エクステンションになっているので、大きな変更を加えなくとも、そのままお手持ちの Forge Viewer で使用することが出来ます。冗長ですが、下記は、そのエクステンション コードを転記したものです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-2"><span class="hljs-keyword">class</span> IconMarkupExtension extends Autodesk.Viewing.Extension {
    constructor(viewer, options) {
        super(viewer, options);
        <span class="hljs-keyword">this</span>._group = <span class="hljs-literal">null</span>;
        <span class="hljs-keyword">this</span>._button = <span class="hljs-literal">null</span>;
        <span class="hljs-keyword">this</span>._icons = options.icons || [];
    }

    load() {
        <span class="hljs-keyword">const</span> updateIconsCallback = () =&gt; {
            <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>._enabled) {
                <span class="hljs-keyword">this</span>.updateIcons();
            }
        };
        <span class="hljs-keyword">this</span>.viewer.addEventListener(Autodesk.Viewing.CAMERA_CHANGE_EVENT, updateIconsCallback);
        <span class="hljs-keyword">this</span>.viewer.addEventListener(Autodesk.Viewing.ISOLATE_EVENT, updateIconsCallback);
        <span class="hljs-keyword">this</span>.viewer.addEventListener(Autodesk.Viewing.HIDE_EVENT, updateIconsCallback);
        <span class="hljs-keyword">this</span>.viewer.addEventListener(Autodesk.Viewing.SHOW_EVENT, updateIconsCallback);
        <span class="hljs-keyword">return</span> <span class="hljs-literal">true</span>;
    }

    unload() {
        <span class="hljs-comment">// Clean our UI elements if we added any</span>
        <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>._group) {
            <span class="hljs-keyword">this</span>._group.removeControl(<span class="hljs-keyword">this</span>._button);
            <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>._group.getNumberOfControls() === <span class="hljs-number">0</span>) {
                <span class="hljs-keyword">this</span>.viewer.toolbar.removeControl(<span class="hljs-keyword">this</span>._group);
            }
        }

        <span class="hljs-keyword">return</span> <span class="hljs-literal">true</span>;
    }

    onToolbarCreated() {
        <span class="hljs-comment">// Create a new toolbar group if it doesn&#39;t exist</span>
        <span class="hljs-keyword">this</span>._group = <span class="hljs-keyword">this</span>.viewer.toolbar.getControl(<span class="hljs-string">&#39;customExtensions&#39;</span>);
        <span class="hljs-keyword">if</span> (!<span class="hljs-keyword">this</span>._group) {
            <span class="hljs-keyword">this</span>._group = <span class="hljs-keyword">new</span> Autodesk.Viewing.UI.ControlGroup(<span class="hljs-string">&#39;customExtensions&#39;</span>);
            <span class="hljs-keyword">this</span>.viewer.toolbar.addControl(<span class="hljs-keyword">this</span>._group);
        }

        <span class="hljs-comment">// Add a new button to the toolbar group</span>
        <span class="hljs-keyword">this</span>._button = <span class="hljs-keyword">new</span> Autodesk.Viewing.UI.Button(<span class="hljs-string">&#39;IconExtension&#39;</span>);
        <span class="hljs-keyword">this</span>._button.onClick = (ev) =&gt; {
            <span class="hljs-keyword">this</span>._enabled = !<span class="hljs-keyword">this</span>._enabled;
            <span class="hljs-keyword">this</span>.showIcons(<span class="hljs-keyword">this</span>._enabled);
            <span class="hljs-keyword">this</span>._button.setState(<span class="hljs-keyword">this</span>._enabled ? <span class="hljs-number">0</span> : <span class="hljs-number">1</span>);

        };
        <span class="hljs-keyword">this</span>._button.setToolTip(<span class="hljs-keyword">this</span>.options.button.tooltip);
        <span class="hljs-keyword">this</span>._button.container.children[<span class="hljs-number">0</span>].classList.add(<span class="hljs-string">&#39;fas&#39;</span>, <span class="hljs-keyword">this</span>.options.button.icon);
        <span class="hljs-keyword">this</span>._group.addControl(<span class="hljs-keyword">this</span>._button);
    }

    showIcons(show) {
        <span class="hljs-keyword">const</span> $viewer = $(<span class="hljs-string">&#39;#&#39;</span> + <span class="hljs-keyword">this</span>.viewer.clientContainer.id + <span class="hljs-string">&#39; div.adsk-viewing-viewer&#39;</span>);

        <span class="hljs-comment">// remove previous...</span>
        $(<span class="hljs-string">&#39;#&#39;</span> + <span class="hljs-keyword">this</span>.viewer.clientContainer.id + <span class="hljs-string">&#39; div.adsk-viewing-viewer label.markup&#39;</span>).remove();
        <span class="hljs-keyword">if</span> (!show) <span class="hljs-keyword">return</span>;

        <span class="hljs-comment">// do we have anything to show?</span>
        <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>._icons === <span class="hljs-literal">undefined</span> || <span class="hljs-keyword">this</span>.icons === <span class="hljs-literal">null</span>) <span class="hljs-keyword">return</span>;

        <span class="hljs-comment">// do we have access to the instance tree?</span>
        <span class="hljs-keyword">const</span> tree = <span class="hljs-keyword">this</span>.viewer.model.getInstanceTree();
        <span class="hljs-keyword">if</span> (tree === <span class="hljs-literal">undefined</span>) { console.log(<span class="hljs-string">&#39;Loading tree...&#39;</span>); <span class="hljs-keyword">return</span>; }

        <span class="hljs-keyword">const</span> onClick = (e) =&gt; {
            <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>.options.onClick)
                <span class="hljs-keyword">this</span>.options.onClick($(e.currentTarget).data(<span class="hljs-string">&#39;id&#39;</span>));
        };

        <span class="hljs-keyword">this</span>._frags = {}
        <span class="hljs-keyword">for</span> (<span class="hljs-keyword">var</span> i = <span class="hljs-number">0</span>; i &lt; <span class="hljs-keyword">this</span>._icons.length; i++) {
            <span class="hljs-comment">// we need to collect all the fragIds for a given dbId</span>
            <span class="hljs-keyword">const</span> icon = <span class="hljs-keyword">this</span>._icons[i];
            <span class="hljs-keyword">this</span>._frags[<span class="hljs-string">&#39;dbId&#39;</span> + icon.dbId] = []

            <span class="hljs-comment">// create the label for the dbId</span>
            <span class="hljs-keyword">const</span> $label = $(`
            &lt;label <span class="hljs-keyword">class</span>=<span class="hljs-string">&quot;markup update&quot;</span> data-id=<span class="hljs-string">&quot;${icon.dbId}&quot;</span>&gt;
                <span class="xml"><span class="hljs-tag">&lt;<span class="hljs-title">span</span> <span class="hljs-attribute">class</span>=<span class="hljs-value">&quot;${icon.css}&quot;</span>&gt;</span> ${icon.label || &#39;&#39;}<span class="hljs-tag">&lt;/<span class="hljs-title">span</span>&gt;</span>
            <span class="hljs-tag">&lt;/<span class="hljs-title">label</span>&gt;</span>
            `);
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
        var fragList = this.viewer.model.getFragmentList();
        const nodebBox = new THREE.Box3()

        // for each fragId on the list, get the bounding box
        for (const fragId of this._frags[&#39;dbId&#39; + dbId]) {
            const fragbBox = new THREE.Box3();
            fragList.getWorldBounds(fragId, fragbBox);
            nodebBox.union(fragbBox); // create a unifed bounding box
        }

        return nodebBox
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
}

Autodesk.Viewing.theExtensionManager.registerExtension(&#39;IconMarkupExtension&#39;, IconMarkupExtension);</span></code></pre>
<p>このエクステンション、IconMarkupExtension エクステンションを使用するには、<a href="https://jquery.com/" rel="noopener" target="_blank">JQuery</a> JavaScript ライブラリを参照する必要があるほか、ブログ記事では、&lt;Label&gt; タグのフォント表現と同じようにアイコン リソースを利用出来るよう、<a data-ved="2ahUKEwjbtcbgjuHtAhXUF4gKHW7EA0gQFjAAegQIARAD" href="https://fontawesome.com/" ping="/url?sa=t&amp;source=web&amp;rct=j&amp;url=https://fontawesome.com/&amp;ved=2ahUKEwjbtcbgjuHtAhXUF4gKHW7EA0gQFjAAegQIARAD"></a><a href="https://fontawesome.com/" rel="noopener" target="_blank">Font Awesome</a> というライブラリも使用しています。</p>
<p>fa-exclamation のように特定のアイコン指定と同時に、fas、far などのアイコン スタイルを <a href="https://fontawesome.com/how-to-use/on-the-web/referencing-icons/basic-use" rel="noopener" target="_blank">CSS スタイル名</a>で指定することが出来るので便利です。上記の例では、一部、点滅などのアニメーションを見て取れますが、このような機能も Font Awesome 内に含まれます。</p>
<p>次のコードは、先の例で示したマークアップを作成するものです。dbid と同じ値（data-id）を HTML 要素に割り当てているので、マークアップへのクリック イベントで渡された ID を使って、Forge Viewer 上のオブジェクトを <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#select-dbids-model-selectiontype" rel="noopener" target="_blank">Viewer3D.Select</a> メソッドで選択、また、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#fittoview-objectids-model-immediate" rel="noopener" target="_blank">Viewer3D.fitToView</a> メソッドで拡大表示していることがわかります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.loadExtension(&#39;IconMarkupExtension&#39;, {
    button: {
        icon: &#39;fa-thermometer-half&#39;,
        tooltip: &#39;Show Temperature&#39;
    },
    icons: [
        { dbId: 83068, label: &#39;非常階段：26°C&#39;, css: &#39;fa-thermometer-half fas temperatureBorder&#39; },
        { dbId: 2886, label: &#39; 通用口：27°C&#39;, css: &#39;fa-thermometer-half temperatureOk fas temperatureBorder&#39; },
        { dbId: 111934, label: &#39;熱交換器：49°C&#39;, css: &#39;fa-thermometer-half temperatureHigh fas temperatureBorder&#39; },
        { dbId: 2917, label: &#39;要点検&#39;, css: &#39;iconWarning fas fa-exclamation fa-lg faa-horizontal animated&#39; },
        { dbId: 111540, label: &#39;故障&#39;, css: &#39;iconWarning  fas fa-exclamation-triangle fa-2x faa-flash animated&#39; },
    ],
    onClick: (id) =&gt; {
        _viewer.select(id);
        _viewer.utilities.fitToView();
    }
});</code></pre>
<p>もちろん、<a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank">デベロッパーツール</a>を使って表示されマークアップをチェックすると、実際の HTML 要素を確認することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42f1864200d-pi" style="display: inline;"><img alt="Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42f1864200d image-full img-responsive" src="/assets/image_562238.jpg" title="Result" /></a></p>
<p>Viewer 本体への負荷が少なく、一般的な HTML5 / CSS3 が適用出来るので、簡単に実装することが出来ます。参考としてご確認いただければと思います。</p>
<p>By Toshiaki Isezaki</p>
