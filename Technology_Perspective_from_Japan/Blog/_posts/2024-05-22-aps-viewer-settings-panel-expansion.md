---
layout: "post"
title: "APS Viewer：設定パネルの拡張"
date: "2024-05-22 00:02:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/aps-viewer-settings-panel-expansion.html "
typepad_basename: "aps-viewer-settings-panel-expansion"
typepad_status: "Publish"
---

<p>APS Viewer&#0160; を利用するアプリが独自の設定項目を持つ場合があります。このような場面では、Viewer 内の設定パネルを拡張して、設定項目のユーザーイ ンターフェースを用意することが出来ます。実装は、GuiViewer3D.getSettingsPanel メソッドを介して<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Faps.autodesk.com%2Fen%2Fdocs%2Fviewer%2Fv7%2Freference%2FUI%2FSettingsPanel%2F&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7Cee554aa5ad124788f8cb08dc4f910bcb%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638472731585436286%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C0%7C%7C%7C&amp;sdata=pa70VTe1gvZYbgYX1Nuv6MgWhsgU7kDA2lRnRNbsmZ8%3D&amp;reserved=0" rel="noopener" target="_blank"> SettingsPanel</a> にアクセスすることで可能です。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension-which-has-html-contents-on-panel.html" rel="noopener" target="_blank">HTML ベースで作成するパネル</a>と異なり、特性のメソッドを使ってコントロールを配置していくことになります。例えば、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/UI/SettingsPanel/#addcheckbox-tabid-caption-initialstate-onchange-description-options" rel="noopener" target="_blank">addCheckbox</a> メソッドでチェックボックスを、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/UI/SettingsPanel/#adddropdownmenu-tabid-caption-items-initialitemindex-onchange-options" rel="noopener" target="_blank">addDropDownMenu</a> メソッドでドロップダウン メニューを、それぞれ追加することが出来ます。</p>
<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/UI/SettingsPanel/#addtab-tabid-tabtitle-options" rel="noopener" target="_blank">addTab</a> メソッドで新しいタブの追加も可能です。ただし、Viewer 標準の設定と区別するため、作成したタブの表示は 2 列目になってしまいます。下記例は、<a href="https://adndevblog.typepad.com/technology_perspective/2024/03/aps-viewer-boundingbox.html" rel="noopener" target="_blank">APS Viewer：境界ボックス</a> でご紹介した境界ボックスの表示内容を設定したパネルの例です。境界ボックスの作図色と塗潰しの有無、塗潰しを有効にした際の透過度を異なるコントロールで実装しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af14c8200d-pi" style="display: inline;"><img alt="Settings_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af14c8200d image-full img-responsive" src="/assets/image_934733.jpg" title="Settings_panel" /></a></p>
<p>新しいタブを追加すると、下部に設定パネル内のすべてのタブにある項目をリセットする [すべて既定の設定に戻す] ボタンが自動的に配置されます。このボタンのクリック動作は、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/#restore-default-settings-event" rel="noopener" target="_blank">RESTORE_DEFAULT_SETTINGS_EVENT</a> イベントで取得出来るので、適切なコールバックを用意すれば、標準項目の設定値と同じタイミングで追加した項目の設定値をリセットすることが可能です。</p>
<p>次のコードは、上記の内容を実装した<a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension.html" rel="noopener" target="_blank">エクステンション</a> BBoxExtension.js の例です。</p>
<pre>class BBoxExtension extends Autodesk.Viewing.Extension {
    constructor(viewer, options) {
        super(viewer, options);
        this._group = null;
        this._button = null;
        this._enabled = false;
        this._geom = null;
        this._material = null;
        this._mesh = null;
        this._colorControlId = &quot;&quot;;
        this._fillControlId = &quot;&quot;;
        this._opacityControlId = &quot;&quot;;
    }

    load = () =&gt; {
        let panel = this.viewer.getSettingsPanel();
        let tabId = 999;
        panel.addTab(tabId, &quot;境界ボックス&quot;);
        panel.addRow(tabId, &quot;&quot;, &quot;&quot;);
        this._colorControlId = panel.addDropDownMenu(tabId, &quot;表示色&quot;, [&quot;黒&quot;, &quot;白&quot;, &quot;赤&quot;, &quot;緑&quot;, &quot;青&quot;, &quot;黄&quot;], null, 0);
        this._fillControlId = panel.addCheckbox(tabId, &quot;塗潰し&quot;, &quot;塗潰し表示するかを指定&quot;, false, (e) =&gt; {
            panel.getControl(this._opacityControlId).setDisabled(!panel.getControl(this._fillControlId).checkElement.checked);
            this.updateBoundingBox();
        });
        this._opacityControlId = panel.addSlider(tabId, &quot;透過度&quot;, 1, 10, 1, (e) =&gt; {
            this.updateBoundingBox();
        });
        panel.getControl(this._opacityControlId).setValue(5);
        panel.getControl(this._opacityControlId).setDisabled(!panel.getControl(this._fillControlId).checkElement.checked);
        panel.getControl(this._colorControlId).addEventListener(&quot;change&quot;, (e) =&gt; {
            this.updateBoundingBox();
        });
        this.viewer.addEventListener(Autodesk.Viewing.RESTORE_DEFAULT_SETTINGS_EVENT, this.onResetSettings);
        return true;
    }

    unload = () =&gt; {
        if (this._group) {
            this._group.removeControl(this._button);
            if (this._group.getNumberOfControls() === 0) {
                this.viewer.toolbar.removeControl(this._group);
            }
        }
        let panel = this.viewer.getSettingsPanel();
        let tabId = 999;
        panel.getControl(this._colorControlId).removeEventListener(&quot;change&quot;, (e) =&gt; { this.removeBoundingBox(); });
        panel.removeCheckbox(this._fillControlId);
        panel.removeDropdownMenu(this._colorControlId);
        panel.removeTab(tabId);
        this._fillControlId = &quot;&quot;;
        this._colorControlId = &quot;&quot;;
        this.viewer.removeEventListener(Autodesk.Viewing.RESTORE_DEFAULT_SETTINGS_EVENT, this.onResetSettings);
        this.viewer.removeEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, this.onSelectedByPick);
        this.viewer.removeEventListener(Autodesk.Viewing.ISOLATE_EVENT, this.onSelectedByMBrowser);
        this.removeBoundingBox();
        return true;
    }

    onToolbarCreated = () =&gt; {
        this._group = this.viewer.toolbar.getControl(&#39;customExtensions&#39;);
        if (!this._group) {
            this._group = new Autodesk.Viewing.UI.ControlGroup(&#39;customExtensions&#39;);
            this.viewer.toolbar.addControl(this._group);
        }

        this._button = new Autodesk.Viewing.UI.Button(&#39;BBoxExtension&#39;);
        this._button.onClick = (ev) =&gt; {
            this._enabled = !this._enabled;
            this._button.setState(this._enabled ? 0 : 1);
            if (this._enabled) {
                this.viewer.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, this.onSelectedByPick);
                this.viewer.addEventListener(Autodesk.Viewing.ISOLATE_EVENT, this.onSelectedByMBrowser);
                this.updateBoundingBox();
            }
            else {
                this.viewer.removeEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, this.onSelectedByPick);
                this.viewer.removeEventListener(Autodesk.Viewing.ISOLATE_EVENT, this.onSelectedByMBrowser);
                this.removeBoundingBox();
            }

        };
        this._button.setToolTip(this.options.button.tooltip);
        this._button.container.children[0].classList.add(&#39;fas&#39;, this.options.button.icon);
        this._group.addControl(this._button);
    }

    onResetSettings = (e) =&gt; {
        let panel = this.viewer.getSettingsPanel();
        panel.getControl(this._fillControlId).checkElement.checked = false;
        panel.getControl(this._opacityControlId).setDisabled(!panel.getControl(this._fillControlId).checkElement.checked);
        panel.getControl(this._colorControlId).setSelectedIndex(0);
        this.updateBoundingBox();
    }

    onSelectedByPick = (e) =&gt; {
        let dbIdArray = e.dbIdArray;
        console.log(&quot;--- onSelectedByPick ---&quot;);
        this.addBoundingBox(dbIdArray);
    }

    onSelectedByMBrowser = (e) =&gt; {
        let dbIdArray = e.nodeIdArray;
        this.viewer.select(dbIdArray); // force to be &#39;current&#39;
        this.viewer.clearSelection();  //
        console.log(&quot;--- onSelectedByMBrowser ---&quot;);
        this.addBoundingBox(dbIdArray);
    }

    addBoundingBox = (dbIdArray) =&gt; {
        if (!this._enabled)
            return;

        let bbox;
        if (dbIdArray.length &gt; 0) {
            for (var i = 0; i &lt; dbIdArray.length; i++) {
                var node = dbIdArray[i];
                bbox = this.viewer.utilities.getBoundingBox();
                console.log(&quot;****&quot; + JSON.stringify(bbox));
            }

            const min = JSON.parse(JSON.stringify(bbox.min));
            const max = JSON.parse(JSON.stringify(bbox.max));

            const xw = max.x - min.x;
            const yw = max.y - min.y;
            const zh = max.z - min.z;
            console.log(&quot;x=&quot; + xw + &quot;, y=&quot; + yw + &quot;, z=&quot; + zh);

            if (this.viewer.overlays.hasScene(&#39;boundingbox-scene&#39;)) {
                this.removeBoundingBox();
            }

            let _color = 0x000000;
            let panel = this.viewer.getSettingsPanel();
            switch (panel.getControl(this._colorControlId).selectedIndex) {
                case 0:
                    _color = 0x000000;
                    break;
                case 1:
                    _color = 0xffffff;
                    break;
                case 2:
                    _color = 0xff0000;
                    break;
                case 3:
                    _color = 0x00ff00;
                    break;
                case 4:
                    _color = 0x0000ff;
                    break;
                case 5:
                    _color = 0xffff00;
                    break;
            }

            this._geom = new THREE.BoxGeometry(xw, yw, zh);
            this._material = new THREE.MeshBasicMaterial({
                color: _color,
                wireframe: !panel.getControl(this._fillControlId).checkElement.checked,
                wireframeLinewidth: 1
            });
            this._material.transparent = true;
            if (panel.getControl(this._fillControlId).checkElement.checked) {
                this._material.opacity = panel.getControl(this._opacityControlId).value * 0.1;
            }
            console.log(&quot;opacity:&quot; + this._material.opacity);
            this._mesh = new THREE.Mesh(this._geom, this._material);
            this._mesh.position.set(min.x + xw / 2, min.y + yw / 2, min.z + zh / 2);
            if (!this.viewer.overlays.hasScene(&#39;boundingbox-scene&#39;)) {
                this.viewer.overlays.addScene(&#39;boundingbox-scene&#39;);
            }
            this.viewer.overlays.addMesh(this._mesh, &#39;boundingbox-scene&#39;);
        }
    }

    updateBoundingBox = () =&gt; {
        if (!this._enabled)
            return;

        let dbIdArray = this.viewer.getSelection();
        if (dbIdArray.length &gt; 0) {
            this.addBoundingBox(dbIdArray);
        } else {
            dbIdArray = this.viewer.getIsolatedNodes(this.viewer.model);
            if (dbIdArray.length &gt; 0) {
                this.addBoundingBox(dbIdArray);
            } else {
                this.removeBoundingBox();
            }
        }
    }

    removeBoundingBox = () =&gt; {
        if (this.viewer.overlays.hasScene(&#39;boundingbox-scene&#39;)) {
            this.viewer.overlays.removeMesh(this._mesh, &#39;boundingbox-scene&#39;);
            this.viewer.overlays.removeScene(&#39;boundingbox-scene&#39;);
            this._material.dispose();
            this._geom.dispose();
            this._material = null;
            this._geom = null;
            this._mesh = null;
        }
    }

}

Autodesk.Viewing.theExtensionManager.registerExtension(&#39;BBoxExtension&#39;, BBoxExtension);
</pre>
<p>設定パネルに追加した「境界ボックス」タブでの実際の動作は、次のようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af14d5200d-pi" style="display: inline;"><img alt="Settings_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af14d5200d image-full img-responsive" src="/assets/image_126124.jpg" title="Settings_panel" /></a></p>
<p>もし、追加したタブの内容だけをリセットしたい場合には、タブに <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/UI/SettingsPanel/#addbutton-tabid-label" rel="noopener" target="_blank">addButton メソッド</a> で作成したボタンを配置して、setOnClick メソッドでクリック イベントのコールバックを実装、同タブの設定値のリセットを実装することが出来ます。</p>
<pre>let panel = NOP_VIEWER.getSettingsPanel();
let tabId = 999;
panel.addTab(tabId,&quot;App Settings&quot;);
panel.addRow(tabId, &quot;&quot;, &quot;&quot;);
const controlId = panel.addButton(tabId, &quot;My button&quot;);
panel.getControl(controlId).setOnClick((e) =&gt; { alert(&quot;My button was clicked&quot;); });
</pre>
<p>By Toshiaki Isezaki</p>
