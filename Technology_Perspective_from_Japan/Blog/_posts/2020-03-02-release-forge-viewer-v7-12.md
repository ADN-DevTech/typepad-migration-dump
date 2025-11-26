---
layout: "post"
title: "Forge Viewer バージョン 7.12 リリース"
date: "2020-03-02 00:19:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/03/release-forge-viewer-v7-12.html "
typepad_basename: "release-forge-viewer-v7-12"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b377966200c-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b025d9b377966200c img-responsive" src="/assets/image_524956.jpg" style="width: 150px; margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ed0e33200d-pi" style="float: right;"></a>少し遅くなりましたが、Forge Viewer の バージョン 7.12 がリースされていますので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<h3>変更<strong>された項目</strong></h3>
<ul>
<li>PDF モデルのキャリブレーション強制を止め、既定の単位をポイントに設定しないようになりました。</li>
<li>測定ツールは、再アクティブ化されると既存の測定値を復元します。</li>
</ul>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>ViewCube のビューを永続的な設定として保存します。</li>
<li>Avatar Extension：ターゲット ビューアの 3D カメラに基づいて、2D シートにアバターを表示する Extention が追加されました。</li>
<li>測定設定パネルに自由計測用のスイッチが追加されました。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ed0ede200d-pi" style="display: inline;"><img alt="Measure_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ed0ede200d img-responsive" src="/assets/image_245579.jpg" title="Measure_settings" /></a></li>
</ul>
<ul>
<li>すべての測定値を削除するための <code>measureExt.deleteMeasurements</code> が追加されました。</li>
<li>SVF の標準サーフェスマテリアルをサポートする <code>Autodesk.StandardSurface</code> Extension が追加されました。</li>
<li>waitForLoadDone メソッドと isLoadDone メソッドがViewer3D API に追加されました。</li>
</ul>
<h3><strong>削除された項目</strong></h3>
<ul>
<li>X、Y、Z 断面平面の選択時に青いハイライトが削除されました。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b37d9f5200c-pi" style="display: inline;"><img alt="Section" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b37d9f5200c image-full img-responsive" src="/assets/image_772701.jpg" title="Section" /></a></li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
