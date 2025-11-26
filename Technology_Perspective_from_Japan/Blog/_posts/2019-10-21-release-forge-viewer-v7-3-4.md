---
layout: "post"
title: "Forge Viewer バージョン 7.3/7.4 リリース"
date: "2019-10-21 00:31:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/10/release-forge-viewer-v7-3_4.html "
typepad_basename: "release-forge-viewer-v7-3_4"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ba16ed200d-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ba16ed200d img-responsive" src="/assets/image_391596.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a></p>
<p>Forge Viewer の バージョン 7.3 と 7.4 が立て続けにリースされていますので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<p>まずは、バージョン 7.3 の内容です。</p>
<h3><strong>変更された項目</strong></h3>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/ProfileSettings/" rel="noopener" target="_blank">プロファイル API</a> が正式にサポートされました。</li>
<li>ピクセル比較 - ベクトル PDF がサポートされました。</li>
<li>テーマとゴーストを使用時に変更の統合が機能します。</li>
</ul>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>Autodesk.Measure Extension の setFreeMeasureMode(bool) メソッドを使用すると、任意の場所から測定できます。</li>
<li>BimWalk Extensionの AEC ナビゲーターで、 Navisworks や Revit の一人称ツールに似たエクスペリエンスをユーザーに提供します</li>
<li>viewer.hitTest() は自身ぼ実装拡張により、sceneAfter からのオブジェクトも交差させます。</li>
<li>getMeasurementListおよびgetCalibration APIを使用して、カンバス上の測定値とキャリブレーション値のリストを取得します。</li>
<li>ジオメトリにスナップするためのツールをエクスポーズする&#0160;<code class="docutils literal"><span class="pre">Autodesk.Extensions.Snapping.Snapper</span></code>Extension がサポートされました。</li>
</ul>
<h3><strong>削除された項目</strong></h3>
<ul class="simple">
<li>Field-of-View ツールをアクティブにするためのホットキー（Ctrl + Shift）は削除されました。 ユーザーは、適切なツールバーボタンを使用して、引き続き機能にアクセス出来ます。</li>
<li>Roll ツールをアクティブにするためのホットキー（Alt + Shift）は削除されました。 ユーザーは、適切なツールバーボタンを使用して、引き続き機能にアクセス出来ます。</li>
</ul>
<hr />
<p>続いて、バージョン 7.4 の内容です。</p>
<h3><strong>変更された項目</strong></h3>
<ul>
<li>プッシュピンを作成または編集するときに、分解 UI を無効にします。</li>
<li>Autodesk.MemoryLimited Extension で使用されるデフォルトのメモリ バジェットを更新しました。</li>
</ul>
<h3><strong>追加された項目</strong></h3>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/SceneBuilder/" rel="noopener" target="_blank">SceneBuilder API</a> が追加されました。</li>
<li>既存の Viewer プロファイルへの切り替えに使用する <code>Autodesk.ProfileUi</code> Extensionが追加されました。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a490efd9200c-pi" style="display: inline;"><img alt="Profile_ui" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a490efd9200c img-responsive" src="/assets/image_305062.jpg" title="Profile_ui" /></a></li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
