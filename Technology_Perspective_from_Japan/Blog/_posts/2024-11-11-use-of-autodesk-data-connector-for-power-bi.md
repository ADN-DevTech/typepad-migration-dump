---
layout: "post"
title: "Autodesk Data Connector for Power BI コネクターの利用"
date: "2024-11-11 00:00:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/use-of-autodesk-data-connector-for-power-bi.html "
typepad_basename: "use-of-autodesk-data-connector-for-power-bi"
typepad_status: "Publish"
---

<p>先日ご案内した <a href="https://adndevblog.typepad.com/technology_perspective/2024/10/autodesk-data-connector-power-bi-now-generally-available.html" rel="noopener" target="_blank">Autodesk Data Connector for Power BI</a>&#0160;について、具体的な利用手順に触れておきたいと思います。</p>
<p>Autodesk Data Connector for Power BI は、Autodesk App Store で公開されているので、Autodesk ID（オートデスク アカウント）で App Store にサインインして、インストーラをダウンロードすることが出来ます。もし、Autodesk Data Connector for Power BI が見つけられない場合には、検索ボックスに「<strong>data exchange connector</strong>」と入力してコネクター一覧から見つけ出すことが出来ると思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f0947d200d-pi" style="display: inline;"><img alt="App_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f0947d200d image-full img-responsive" src="/assets/image_645765.jpg" title="App_store" /></a></p>
<p>データ交換時の書き出し手順は、以前ご紹介した <a href="https://adndevblog.typepad.com/technology_perspective/2023/07/data-exchange-with-revit-connector-to-inventor.html" rel="noopener" target="_blank">Revit Connector を使った Inventor へのデータ交換</a> と同じです。 Autodesk Construction Cloud（ACC）領域に作成したデータ交換領域に粒状データを書き出した後で、Data Connector for Power BI コネクターをインストールした Power BI Desktop に粒状データを読み込んで利用することが出来ます。</p>
<p>次の動画は、ACC 上に作成したデータ交換領域から粒状データを読み込み、Power BI Desktop で利用する例です。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/w80wDyAxlZE" width="480"></iframe></p>
<p>Power BI Desktop で APS Viewer を使ったモデル表示をおこなう場合には、Autodesk Data Connector for Power BI と共にインストールされる <strong>Autodesk Data Connector for Power BI.pbiviz</strong> をインポートする必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f094b6200d-pi" style="display: inline;"><img alt="Viewr_setup" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f094b6200d image-full img-responsive" src="/assets/image_473388.jpg" title="Viewr_setup" /></a></p>
<p>なお、オートデスクの「<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html" rel="noopener" target="_blank">Design &amp; Make（デザインと創造）プラットフォーム</a>」で粒状データを使ったデータ交換（Data Exchange）を担う Data Exchange コネクターは、Autodesk Platform Services の&#0160;<a href="https://aps.autodesk.com/en/docs/dx-sdk-beta/v1/developers_guide/overview/" rel="noopener" target="_blank">Data Exchange SDK</a>&#0160;を使って開発されています。<a href="https://aps.autodesk.com/developer/overview/data-exchange" rel="noopener" target="_blank">Data Exchange API/SDK</a> 自体は、2024年11月11日現在でも Beta 版扱いです。</p>
<ul>
<li>Data Exchange API・SDK は新規にコネクターを開発する際に使用するものです。Autodesk Data Connector for Power BI コネクターを含め、既存のオートデスク製、あるいは、3rd party 製データ コネクターをカスタマイズするものではありません。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c2eade200c-pi" style="display: inline;"><img alt="Caution" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c2eade200c img-responsive" src="/assets/image_909399.jpg" title="Caution" /></a></li>
</ul>
<p>By Toshiaki Isezaki</p>
