---
layout: "post"
title: "Forge Viewer バージョン 7.2 リリース"
date: "2019-08-19 00:02:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/08/release-forge-viewer-v7-2.html "
typepad_basename: "release-forge-viewer-v7-2"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c5717d200b-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c5717d200b img-responsive" src="/assets/image_50245.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a>早いペースで新バージョンとなるバージョン 7.2 がリリースされていますので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<p><strong>変更された項目</strong></p>
<ul>
<li>ベクトル PDF にキャリブレーションが強制されます。</li>
<li>モデル ブラウザでオブジェクト名をクリックすると、当該オブジェクトが選択表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c594f5200b-pi" style="display: inline;"><img alt="Isolate_from_modelbrowser" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c594f5200b image-full img-responsive" src="/assets/image_144076.jpg" title="Isolate_from_modelbrowser" /></a></li>
<li>既定でロードされる&#0160;<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/LayerManagerExtension/" rel="noopener" target="_blank">Autodesk.LayerManager</a> Extension は、画層管理パネル ボタンをツールバーに追加するかどうかを制御します。&#0160;</li>
<li>1 つの設定に複数のコールバックを登録できます。 詳細：<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Private/Preferences/#addlisteners-name-onchangedcallback-onresetcallback" rel="noopener" target="_blank">Autodesk.Viewing.Private.Preferences#addListeners</a>.</li>
</ul>
<p><strong>追加された項目</strong></p>
<ul>
<li>PDF ファイル内のテキストを選択するツールバーボタン。</li>
<li>PDF ファイルへの画層サポート。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c5d797200b-pi" style="display: inline;"><img alt="Pdf_layer_text_select" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c5d797200b image-full img-responsive" src="/assets/image_234388.jpg" title="Pdf_layer_text_select" /></a></li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/PDFExtension/" rel="noopener" target="_blank">PDF Extension</a>: <strong>options.enableHyperlinks</strong> と <strong>options.enableTextSearch </strong>&#0160;のサポートを追加。</li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/UI/DataTable/" rel="noopener" target="_blank">Autodesk.Viewing.UI.DataTable</a> コンポーネント</li>
<li><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Profile/">Autodesk.Viewing.Profile</a> の Beta リリース。プロファイルを使用して、設定を適用、Viewer から Extension をロード/ロード解除できます。</li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
