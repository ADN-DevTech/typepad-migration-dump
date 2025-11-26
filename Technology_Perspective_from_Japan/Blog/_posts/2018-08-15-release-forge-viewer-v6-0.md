---
layout: "post"
title: "Forge Viewer バージョン 6.0 リリース"
date: "2018-08-15 02:12:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/08/release-forge-viewer-v6_0.html "
typepad_basename: "release-forge-viewer-v6_0"
typepad_status: "Publish"
---

<p>新しい Forge Viewer バージョン 6.0 がリリースされていますので、その機能や変更点をご紹介しておきます。 一部の Extension については、<strong><a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">LMV Ninja</a></strong>&#0160; でお試しいただけます。LMV Ninja については、過去のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html" rel="noopener noreferrer" target="_blank">LMV Ninja を使った URN 指定表示</a></strong> などをご参照ください。</p>
<p><strong>変更点</strong>&#0160;</p>
<ul>
<li>PDF ファイルのロード パフォーマンスが向上しています。</li>
<li>モデルが作成されるまで環境光プリセットの設定が遅延されるようになったため、2D モデルを表示する際の環境マップのロードとデコードが回避され、パフォーマンスが向上しています。</li>
<li>getScreenShot() が改良されて、任意のサイズのスクリーンショットが可能になりました。 （非互換性：詳細については、下記の <strong>削除された点</strong> セクションを参照してください）。</li>
</ul>
<p style="padding-left: 60px;">================================&#0160;<br />&#0160; &#0160;Viewer Canvas &#0160;(350px x 400px)&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<br />================================&#0160;</p>
<p style="padding-left: 60px;"><img alt="canvas 1" data-entity-type="file" data-entity-uuid="a93cfe8c-e2a9-46ee-925f-126e8f57ed77" height="335" src="/assets/low_fidelity_viewer_canvas_350x400.png" width="297" /></p>
<p style="padding-left: 60px;">&#0160;================================<br />&#0160; getScreenShot (350, 400)<br />&#0160; ================================</p>
<p style="padding-left: 60px;"><img alt="canvas 2" data-entity-type="file" data-entity-uuid="f9e59e03-4c51-4b59-ba77-2fa39d73449f" height="330" src="/assets/ScreenShot_low_fidelity_350x400.png" width="288" /></p>
<p style="padding-left: 60px;">================================&#0160;<br />&#0160; &#0160;**getScreenShot (3500,4000)**&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;<br />===============================&#0160;</p>
<p style="padding-left: 60px;"><img alt="canvas 1 hugh" data-entity-type="file" data-entity-uuid="ffe6a2ff-f696-48a3-974a-e15e0186cf13" height="329" src="/assets/ScreenShot_high_fidelity_3500x4000.png" width="287" /></p>
<p style="padding-left: 60px;">================================&#0160;<br />&#0160;**Blow-up of getScreenShot(3500,4000)**<br />&#0160;===============================</p>
<p style="padding-left: 60px;">&#0160;<img alt="canvas 2 blowup" data-entity-type="file" data-entity-uuid="b05a3d12-e3cc-4ac6-b4d5-da7a55dd2a13" height="328" src="/assets/blow-up.png" width="287" />&#0160;</p>
<p><strong>新機能</strong>&#0160;</p>
<ul>
<li>後述の Autodesk.SplitScreen Extension 用にマルチビューポートレンダリングを容易にするオプションのパラメータが内部的に追加されています。</li>
<li>Autodesk.SplitScreen Extension が導入されて Canvas を分割表示することが出来ます。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3885357200d-pi" style="display: inline;"><img alt="Split" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3885357200d image-full img-responsive" src="/assets/image_763344.jpg" title="Split" /></a></li>
<li>Vertex Array オブジェクトは、メモリを節約するためにモバイル デバイスでは既定値で false が適用されます。</li>
<li>BIM 360 用に Autodesk.BIM360.Extension.PushPin Extension が導入されています。</li>
<li>マークアップを編集するときに中央ボタンでパン（画面移動）が可能になっています。</li>
<li>BIM 360 用に Layer Order（画層表示順序）がサポートされています。</li>
<li>処理時間が必要な PDF スクリーンショットをキャンセルするためにViewer3D.impl.cancelLeafletScreenshot() が追加されています。</li>
<li>EMEA(EU) 用の API エンドポイントを追加されています。</li>
</ul>
<p><strong>削除された点</strong>&#0160;</p>
<ul>
<li>Viewer3D.getScreenShotBuffer() の代わりに Viewer3D.getScreenShot() をお使いください。</li>
<li>Autodesk.Viewer.Hammerのため window.Hammer が削除されました。</li>
</ul>
<p>By Toshiaki Isezaki</p>
