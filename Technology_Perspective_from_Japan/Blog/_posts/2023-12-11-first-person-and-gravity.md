---
layout: "post"
title: "APS Viewer：一人称視点と重力設定"
date: "2023-12-11 00:29:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/12/first-person-and-gravity.html "
typepad_basename: "first-person-and-gravity"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a505cc200b-pi" style="display: inline;"></a>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/aps-viewer-first-person-tool.html" rel="noopener" target="_blank">Viewer の「一人称視点ツール」</a>&#0160;のブログ記事でご案内したウォークスルー機能は、APS Viewer の <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Extensions/BimWalkExtension/" rel="noopener" target="_blank">BimWalkExtension</a> で実装されています。<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/" rel="noopener" target="_blank">GuiViewer3D</a> で Viewer カンバスを用意した場合、このエクステンション（Autodesk.BimWalk）は自動的にロードされるので、アプリ側で明示的なロードは必要ありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a56720200d-pi" style="display: inline;"><img alt="First_person" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a56720200d image-full img-responsive" src="/assets/image_48511.jpg" title="First_person" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a505cc200b-pi" style="display: inline;"><br /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Extensions/BimWalkExtension/" rel="noopener" target="_blank">BimWalkExtension のドキュメント</a> にあるとおり、ウォークスルーモードは activate() でオン、deactivate() でオフにすることが出来るので、表示するビューによってすぐに有効（オン）にしてウォークスルーを開始することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a503f8200b-pi" style="display: inline;"><img alt="Bimwalk" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a503f8200b image-full img-responsive" src="/assets/image_947128.jpg" title="Bimwalk" /></a></p>
<p>次の例では、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/#extension-loaded-event" rel="noopener" target="_blank">EXTENSION_LOADED_EVENT</a> イベントでロードされた各種エクステンションの中から Autodesk.BimWalk エクステンションのロードを検出し、HTML 上に配置した &quot;camera”（id）コントロールで特定のビュー選択時にウォークスルーをオン・オフするものです。</p>
<div>
<blockquote>
<div>_viewer.addEventListener(Autodesk.Viewing.EXTENSION_LOADED_EVENT, onExtensionLoaded);</div>
<div>...</div>
<div>function onExtensionLoaded(event) {</div>
<div>&#0160; &#0160; if (event.extensionId === &#39;Autodesk.BimWalk&#39;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; var ext = _viewer.getExtension(&#39;Autodesk.BimWalk&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; if ($(&quot;#cameras&quot;).val() === &#39;0&#39;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ext.deactivate();</div>
<div>&#0160; &#0160; &#0160; &#0160; } else {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ext.activate();</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</blockquote>
</div>
<p>ウォークスルーの際、<a href="https://adndevblog.typepad.com/technology_perspective/2023/03/aps-viewer-first-person-tool.html" rel="noopener" target="_blank">Viewer の「一人称視点ツール」</a> で触れた「重力を有効にする」がオンになっていると、ウォークスルー中の視点が上下にぶれる現象が発生してしまいます。次の例では、床や机などと視点との高さを一定にしようとするため、後半で視点がジャンプしたような結果になってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a554d8200d-pi" style="display: inline;"><img alt="W_gravity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a554d8200d image-full img-responsive" src="/assets/image_80095.jpg" title="W_gravity" /></a></p>
<p>アプリ側でウォークスルーを自動で有効（オン）にするような場合には、コード上で「重力を有効にする」が無効（オフ）に指定することも出来ます。</p>
<div>
<blockquote>
<div>ext.tool.navigator.enableGravity(false);</div>
</blockquote>
</div>
<p>この指定でウォークスルー中の視点の上下移動を抑止出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a0f64e200c-pi" style="display: inline;"><img alt="Wo_gravity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a0f64e200c image-full img-responsive" src="/assets/image_106088.jpg" title="Wo_gravity" /></a></p>
<p>By Toshiaki Isezaki</p>
