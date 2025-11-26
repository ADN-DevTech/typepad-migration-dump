---
layout: "post"
title: "Forge Viewer のパフォーマンス強化"
date: "2017-02-17 00:54:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/performance-enhancement-on-forge-viewer.html "
typepad_basename: "performance-enhancement-on-forge-viewer"
typepad_status: "Publish"
---

<p>バージョン 2.12 以降の Forge Viewer では、メッシュ結合とハードウェアのインスタンス化を使用して、モデル表示時のレンダリング処理時間を短縮する機能が追加されています。また、メッシュ結合で消費する GPU メモリ量は既定値で 100 MB ですが、この値をコントロールするオプションも用意されています。</p>
<p>メッシュ結合オプションを指定した場合には、表示する 3D モデル全体がメモリ上に展開される必要があります。「設定」画面で「プログレッシブ モデル表示」を指定している場合には、モデル全体がメモリ上にロードされるまで最適化がおこなわれないので注意してください。この最適化は、大規模な Navisworks モデルのように、モデル点数が多いことが原因でパフォーマンスが低下するようなモデルに効果的です。</p>
<p>次の例は、メッシュ結合を有効にして、試用する GPU メモリを 150 MB に設定して Viewer を初期化する例です。</p>
<pre>&#0160;var viewerApp;<br /> var options = {<br />   env: &#39;AutodeskProduction&#39;,<br />   language: &#39;ja&#39;,<br /><strong>   useConsolidation: true,</strong><br /><strong>   consolidationMemoryLimit: 150 * 1024 * 1024, // 150MB = 150byte * 2^20(Mega bytes)</strong><br /><br />   ... &lt;中略&gt; ...<br /><br /> };<br /><br /> var documentId = &#39;urn:&#39; + defaultUrn;<br /> Autodesk.Viewing.Initializer(options, function onInitialized(){<br />   var avp = Autodesk.Viewing.Private;<br />   avp.logger.setLevel( avp.LogLevels.Debug );<br />   var config3d = {<br />     extensions: [&#39;Autodesk.Viewing.WebVR&#39;]<br />   };<br />   viewerApp = new Autodesk.Viewing.ViewingApplication(&#39;viewerDiv&#39;);<br />   viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D, config3d);<br />   viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);<br /> });</pre>
<p>なお、メッシュ結合にはいくつかの制限がありますので、併せてご紹介しておきます。</p>
<ul>
<li>このオプションを指定した場合には、CPU と GPU の両者に追加メモリを必要とします。</li>
<li>アニメーションや分解機能を使用した場合には、自動的にオプションは無効になります。</li>
<li>ゴーストやテーマ設定などのフラグメント単位の変更は、現在のところ単純なフォールバックのみで行われます。特別なフラグメント単位のカスタマイズが適用されている場合は、通常の（最適化されていない）レンダリングが使用されます。</li>
<li>このオプションの有効時には、透明度を含むシーンの粒状度が低下する場合があります。</li>
</ul>
<p>通常の場合、あまり効果を体感することは難しいかもしれませんが、<strong>Autodesk.Viewing.WebVR</strong> Extension 使用時にも適用されるようなので、今後、有効に感じる場面があるかも知れません。</p>
<p>By Toshiaki Isezaki</p>
