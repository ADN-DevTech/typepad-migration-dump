---
layout: "post"
title: "Design Automation for Revit 2022 で PDF 書き出しをサポート"
date: "2021-05-28 07:01:59"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/da4r2022_support_exporting_pdf.html "
typepad_basename: "da4r2022_support_exporting_pdf"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2021/05/new-features-on-revit-2022-part6.html">前回の記事</a>でご紹介した Revit 2022 の API は、もちろん Revit アドイン開発でもご利用いただけますが、Forge の <a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-revit.html">Design Automation API for Revit</a> を通じて、クラウドサービスの開発にもご活用いただけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802c843a200d-pi" style="display: inline;"><img alt="DA4R_ExportPDF_06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802c843a200d image-full img-responsive" src="/assets/image_826543.jpg" title="DA4R_ExportPDF_06" /></a></p>
<p>さらに、Design Automation for Revit の Revit 2022 のエンジンでは、PDF の書き出し機能がサポートされました。</p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/design-automation/v3/change_history/revit_release_notes/">Design Automation for Revit Release Notes</a></li>
</ul>
<p>これまで、Design Automation API の Revit エンジンでは、PDF を直接書き出す方法は、いくつかの制約により、サポート対象外となっておりました。<br />そのため、回避策として、まず Revit から DWG ファイルを書き出し、次に Design Automation for AutoCAD を通じて DWG から PDF に書き出す方法をご提案してきました。</p>
<p>そして今回のアップデートにより、Design Automation API for Revit の Revit 2022 エンジンを使用して、クラウド上で簡単に PDF を書き出すことができるようになりました。</p>
<hr />
<p><strong>PDF 書き出しの検証</strong></p>
<p>今回は、Revit 2022 から追加された日本向けのサンプルモデル「サンプル意匠.rvt」を使用して、実施図のシートを１つの PDF に書き出してみました。</p>
<p>Design Automation for Revit 用の AppBundle （Revit アドイン）に使用したソースコードは、下記のリンクからダウンロードいただけます。</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b0282e104e54a200b img-responsive"><a href="https://adndevblog.typepad.com/files/app.cs">ソースコードをダウンロード</a></span></li>
</ul>
<p><strong>出力結果の比較</strong></p>
<p>書き出した PDF ファイルを確認するために、Revit 2022 の画面と PDF ファイルのスクリーンショットをそれぞれご参照ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e104e4fe200b-pi" style="display: inline;"><img alt="DA4R_ExportPDF_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e104e4fe200b image-full img-responsive" src="/assets/image_782091.jpg" title="DA4R_ExportPDF_01" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e104e578200b-pi" style="display: inline;"><img alt="DA4R_ExportPDF_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e104e578200b image-full img-responsive" src="/assets/image_258501.jpg" title="DA4R_ExportPDF_02" /></a></p>
<p>さらに、日本語のフォントが正しく表示されるか確認するために、事前にデスクトップ版 Revit 2022 で、設計概要書シートに表示される文字を MS P ゴシックに変換して検証してみました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278802c83e0200d-pi" style="display: inline;"><img alt="DA4R_ExportPDF_03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278802c83e0200d image-full img-responsive" src="/assets/image_321167.jpg" title="DA4R_ExportPDF_03" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e104e598200b-pi" style="display: inline;"><img alt="DA4R_ExportPDF_04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e104e598200b image-full img-responsive" src="/assets/image_119395.jpg" title="DA4R_ExportPDF_04" /></a></p>
<p>その結果、フォントは、MS P ゴシックから英語名の MS-PGothic に変換されてしまいますが、想定通りに表示されていることがわかります。これは、Activity の定義で日本語版 Revit を起動するように設定しても、Windows OS が英語版のため、自動的にフォントが Windows OS 英語版にインストールされているフォントに変換されてしまうことが原因です。</p>
<p>また、デスクトップ版 Revit 2022 で書き出した PDF と見比べると、若干、文字が上にずれていることを確認しました。</p>
<p>これらの点については、今後、ご要望に応じて改善のリクエストを行う予定です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e104e5a1200b-pi" style="display: inline;"><img alt="DA4R_ExportPDF_05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e104e5a1200b image-full img-responsive" src="/assets/image_407997.jpg" title="DA4R_ExportPDF_05" /></a></p>
<hr />
<p>Revit 2022 で強化された API と Design Automation API for Revit 2022 の PDF 書き出し機能をぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
