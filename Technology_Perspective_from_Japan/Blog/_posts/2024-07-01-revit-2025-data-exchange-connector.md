---
layout: "post"
title: "Revit 2025 用 Data Exchange コネクター"
date: "2024-07-01 00:46:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/07/revit-2025-data-exchange-connector.html "
typepad_basename: "revit-2025-data-exchange-connector"
typepad_status: "Publish"
---

<p>オートデスクの「<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html" rel="noopener" target="_blank">Design &amp; Make（デザインと創造）プラットフォーム</a>」で粒状データを使ったデータ交換（Data Exchange）を担う Data Exchange コネクターに、Revit 2025 用のコネクターが用意されました。</p>
<p>Revit 2025 用コネクターは <a href="https://apps.autodesk.com/ja" rel="noopener" target="_blank">Autodesk App Store</a> で公開されていますので、&#0160;”<strong><em>Data Exchange connector</em></strong>” のキーワードで検索して当該ページを表示させことが出来ます。実際の入手にはページ右上の [リンクを開く] ボタンからベータポータルにサインインして、ダウンロードとインストールをおこなうことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c818d9200d-pi" style="display: inline;"><img alt="Revit_2025_connector" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c818d9200d image-full img-responsive" src="/assets/image_696706.jpg" title="Revit_2025_connector" /></a></p>
<p>Revit 2025 がインストールされているコンピュータに Revit 2025 用コネクターをインストールすると、[コラボレート] タブに「データ交換」ボタンが作成されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b7f77c200b-pi" style="display: inline;"><img alt="Revit_2025" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b7f77c200b image-full img-responsive" src="/assets/image_389824.jpg" title="Revit_2025" /></a></p>
<p>実際のデータ交換の手順は、昨年ご紹介した <a href="https://adndevblog.typepad.com/technology_perspective/2023/07/data-exchange-with-revit-connector-to-inventor.html" rel="noopener" target="_blank">Revit Connector を使った Inventor へのデータ交換</a> と同じです。選択した要素やカテゴリ毎に Autodesk Construction Cloud 領域に作成したデータ交換領域に粒状データを書き出して、同じく Data Exchange&#0160;コネクターを持つ他の製品で粒状データを読み込む、といったことが出来ます。</p>
<p>次の例は、Revit 2025 から Autodesk Construction Cloud のプロジェクト フォルダを介して、Inventor にデータを渡す手順を示しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/e7ImjsC2BfU" width="480"></iframe></p>
<p>3rd party 製のコネクターも含め、<a href="https://apps.autodesk.com/ja" rel="noopener" target="_blank">Autodesk App Store</a> には、他にも多くの Data Exchange コネクターが用意されていますので、ファイル単位ではなく、ファイルから抽出した本当に必要な粒度の高いデータ（粒状データ）だけを利用する、新しいデータ交換の方法をお試しいただくことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b44d91200c-pi" style="display: inline;"><img alt="Connectors_on_app_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b44d91200c image-full img-responsive" src="/assets/image_499912.jpg" title="Connectors_on_app_store" /></a></p>
<ul>
<li>Data Exchange コネクターは、特定製品の特定バージョン用に用意されるアドイン（プラグイン）アプリケーションとして用意されています。また、一部、データ交換が双方向に対応していないものもありますのでご注意ください。</li>
<li>Data Exchange コネクターは、Autodesk Platform Services の <a href="https://aps.autodesk.com/en/docs/dx-sdk-beta/v1/developers_guide/overview/" rel="noopener" target="_blank">Data Exchange SDK</a> を使って開発されています。<a href="https://aps.autodesk.com/developer/overview/data-exchange" rel="noopener" target="_blank">Data Exchange API/SDK</a> は2024年7月1日現在、Beta 版扱いです。</li>
</ul>
<p>By Toshiaki Isezaki</p>
