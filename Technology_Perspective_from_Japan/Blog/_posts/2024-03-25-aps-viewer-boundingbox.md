---
layout: "post"
title: "APS Viewer：境界ボックス"
date: "2024-03-25 00:08:15"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/03/aps-viewer-boundingbox.html "
typepad_basename: "aps-viewer-boundingbox"
typepad_status: "Publish"
---

<p>APS Viewer を使ったプログラミング作業をしていると、特定の要素の最小、あるいは最大位置を把握したい場合があります。そのような場面では、デスクトップで利用する CAD のアドイン開発でよく見られる境界ボックス（Bounding Box や Boundary Box）を、同様に取得、利用することが出来ます。</p>
<p>境界ボックスは、要素を囲む最小座標と最大座標を返すのが一般的です。APS Viewer では、現在、画面上でマウス左ボタンを使って選択している要素、またはモデル ブラウザなどで選択した「現在」の要素について、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/ViewingUtilities/#getboundingbox-ignoreselection" rel="noopener" target="_blank">ViewingUtilities.getBoundingBox</a> メソッドが Three.js の THREE.Box3 を返します。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-overlay-and-scene-builder.html" rel="noopener" target="_blank">Forge Viewer：オーバーレイとシーン ビルダー</a> でご紹介した方法と、要素の選択時に発生する <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/#selection-changed-event" rel="noopener" target="_blank"><span style="font-size: 10pt;">SELECTION_CHANGED_EVENT</span></a> イベントを併用すると、Viewer 上に境界ボックスを描画して可視化するようなことも可能です。</p>
<blockquote>
<p><span style="font-size: 10pt;">_viewer.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, onSelectedByPick);<br />...</span></p>
<p><span style="font-size: 10pt;">function onSelectedByPick(event) {</span></p>
<div>
<div><span style="font-size: 10pt;">&#0160; &#0160; let dbIdArray = event.dbIdArray;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; boundingBox(dbIdArray);</span></div>
<div><span style="font-size: 10pt;">}<br />...</span></div>
<div><span style="font-size: 10pt;">function boundingBox(dbIdArray) {</span></div>
</div>
<div>
<div><span style="font-size: 10pt;">&#0160; &#0160; let bbox;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; if (dbIdArray.length &gt; 0) {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; for (i = 0; i &lt; dbIdArray.length; i++) {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var node = dbIdArray[i];</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bbox = _viewer.utilities.getBoundingBox();</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<br />
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; const min = JSON.parse(JSON.stringify(bbox.min));</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; const max = JSON.parse(JSON.stringify(bbox.max));</span></div>
<br />
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; const xw = max.x - min.x;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; const yw = max.y - min.y;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; const zh = max.z - min.z;</span></div>
<br />
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; if (_viewer.overlays.hasScene(&#39;custom-scene&#39;)) {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _viewer.overlays.removeMesh(_scene.mesh, &#39;custom-scene&#39;);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _viewer.overlays.removeScene(&#39;custom-scene&#39;);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _scene.material.dispose();</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _scene.geom.dispose();</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _scene = null;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<br />
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; _scene = new sceneMember(</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; new THREE.BoxGeometry(xw, yw, zh),</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; new THREE.MeshBasicMaterial({</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; color: 0x000000,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; wireframe: true,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; wireframeLinewidth: 1</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; })</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; );</span></div>
<br />
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; _scene.mesh = new THREE.Mesh(_scene.geom, _scene.material);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; _scene.mesh.position.set(min.x + xw / 2, min.y + yw / 2, min.z + zh / 2);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; if (!_viewer.overlays.hasScene(&#39;custom-scene&#39;)) {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _viewer.overlays.addScene(&#39;custom-scene&#39;);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; _viewer.overlays.addMesh(_scene.mesh, &#39;custom-scene&#39;);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt;">}</span></div>
</div>
</blockquote>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad7c41200d-pi" style="display: inline;"><img alt="Bounding_box1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad7c41200d image-full img-responsive" src="/assets/image_25424.jpg" title="Bounding_box1" /></a></p>
<p>APS Viewer v7 では、モデルブラウザ上の要素選択は要素自身を選択状態にしません。このため、画面上の選択操作時と異なり、<span style="font-size: 10pt;">SELECTION_CHANGED_EVENT イベントが発生しません。代わりに、モデルブラウザ上で選択した要素は、選択表示（孤立表示、独立表示、ISOLATE 表示）の状態に遷移します。</span></p>
<p><span style="font-size: 10pt;">選択状態になる際には <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/#isolate-event" rel="noopener" target="_blank">ISOLATE_EVENT</a> イベントが発生するので、このイベントを使うと、モデルブラウザ上の選択を利用することも出来ます。</span></p>
<blockquote>
<p><span style="font-size: 10pt;">_viewer.addEventListener(Autodesk.Viewing.ISOLATE_EVENT, onSelectedByMBrowser);<br />...</span></p>
<p><span style="font-size: 10pt;">function onSelectedByMBrowser(event) {</span><br /><span style="font-size: 10pt;">&#0160; &#0160; let dbIdArray = event.nodeIdArray;<br />&#0160; &#0160; _viewer.select(dbIdArray); // force to be &#39;current&#39;<br />&#0160; &#0160; _viewer.clearSelection();&#0160; &#0160; //</span><br /><span style="font-size: 10pt;">&#0160; &#0160; boundingBox(dbIdArray);</span><br /><span style="font-size: 10pt;">}</span></p>
</blockquote>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a997e4200c-pi" style="display: inline;"><img alt="Bounding_box3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a997e4200c image-full img-responsive" src="/assets/image_871115.jpg" title="Bounding_box3" /></a></p>
<p>この方法を使うと、POST job エンドポイントを generateMasterViews advanced オプションを指定して変換した Revit モデルに対して、部屋の境界ボックスを得ることも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad260b200b-pi" style="display: inline;"><img alt="Bounding_box2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad260b200b image-full img-responsive" src="/assets/image_983816.jpg" title="Bounding_box2" /></a></p>
<p>getBoundingBox メソッドは単一要素だけでなく、ノード全体の境界ボックスも返す点にご注意ください。[Shift] キーを押しながら、画面上で複数の要素を直接選択した場合も同様です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad63fe200b-pi" style="display: inline;"><img alt="Bounding_box4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad63fe200b image-full img-responsive" src="/assets/image_237258.jpg" title="Bounding_box4" /></a></p>
<p>By Toshiaki Isezaki</p>
