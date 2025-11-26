---
layout: "post"
title: "Revit Connector を使った Inventor へのデータ交換"
date: "2023-07-05 01:17:01"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/07/data-exchange-with-revit-connector-to-inventor.html "
typepad_basename: "data-exchange-with-revit-connector-to-inventor"
typepad_status: "Publish"
---

<p>粒状データで Revit 2024 からデータ交換する際には、<a href="https://apps.autodesk.com/ja" rel="noopener" target="_blank">Autodesk App Store</a> 上で ”<strong><em>Data Exchange connector</em></strong>” と検索して表示される &#0160;Public Beta 扱い（Early Access）の <strong>Revit 2024 Data Exchange Connector</strong> を、ベータポータルからダウンロード、インストールする必要があります。</p>
<p>ここでは、Revit 2024 Data Exchange Connector を使って Inventor 2024 にデータを渡す流れをご紹介しておきたいと思います。Revit 2024 Data Exchange Connector を作成した <a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/developers_guide/" rel="noopener" target="_blank">Data Exchange API</a> は、2023年7月現在、ベータ段階であるため、今後、手順や内容に小さな変更が加えられる可能性があります。あらかじめ、ご承知おきください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cca2f6200b-pi" style="display: inline;"><img alt="Autodesk_app_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cca2f6200b image-full img-responsive" src="/assets/image_306077.jpg" title="Autodesk_app_store" /></a></p>
<p>具体的には、Autodesk App Store ページに Autodesk ID でサインイン後、<a href="https://apps.autodesk.com/RVT/ja/Detail/Index?id=827207946618909505&amp;appLang=en&amp;os=Web" rel="noopener" target="_blank">Data Exchange - Revit 2024, Early Access</a> ページから、ページ 右上の [リンクを開く] をクリックします。その後、ジャンプしたベータポータルの Data Exchange Connectors Public Beta ページで、Data Exchange - Revit Connector - 2024.msi（インストーラ）をダウンロードしてインストールをおこないます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cca308200b-pi" style="display: inline;"><img alt="Revit_2024_data_exchange_connector" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cca308200b image-full img-responsive" src="/assets/image_412443.jpg" title="Revit_2024_data_exchange_connector" /></a></p>
<p>インストールが正常に完了すると、コントロールパネルに <strong>Data Exchange - Revit Connector - 2024</strong> が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cca258200b-pi" style="display: inline;"><img alt="Control_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cca258200b image-full img-responsive" src="/assets/image_966669.jpg" title="Control_panel" /></a></p>
<p>この状態で Revit 2024 を起動後、Revit プロジェクトを開き、データ交換したいビューを表示させると、[コラボレート] リボンタブの [共有] タブに [データ交換] ボタンが表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6ccb70b200b-pi" style="display: inline;"><img alt="Revit_ui" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6ccb70b200b image-full img-responsive" src="/assets/image_679274.jpg" title="Revit_ui" /></a></p>
<p>[データ交換] ボタンをクリックすると、[Data Exchange] ダイアログが表示されるので、上部の [+ Create Data Exchange] をクリックしてデータ交換（Data Exchange）の作成を開始します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa24fb200c-pi" style="display: inline;"><img alt="Create_dx1-" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa24fb200c img-responsive" src="/assets/image_170062.jpg" title="Create_dx1-" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa24b9200c-pi" style="display: inline;"><br /></a>データ交換に使用する Autodesk Construction Cloud（ACC）上のフォルダをナビゲートして、データ交換したい Revit カテゴリ（例えば「階段」）を選択したら、[Create Data Exchange] をクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2589ba2200d-pi" style="display: inline;"><img alt="Create_dx3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2589ba2200d image-full img-responsive" src="/assets/image_530629.jpg" title="Create_dx3" /></a></p>
<p>データ交換が作成されると、Revit 上の [Data Exchange] ダイアログに指定したデータ交換名が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa250c200c-pi" style="display: inline;"><img alt="Create_dx4" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa250c200c img-responsive" src="/assets/image_573668.jpg" title="Create_dx4" /></a></p>
<p>この際、Autodesk Construction Cloud（ACC）を参照すると、Revit 上でナビゲートしたフォルダにデータ交換名がアイテム登録されていることがわかります。このアイテムは、Data Exchange API 上、データ交換コンテナ（Data Exchange Cotainer）の役割を持つことになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2589bb9200d-pi" style="display: inline;"><img alt="Acc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2589bb9200d image-full img-responsive" src="/assets/image_441827.jpg" title="Acc" /></a></p>
<p>これで、他製品にデータ交換したい内容の準備が出来ました。ここでは、Inventor を2024 起動して、Revit からデータ交換された内容を読み取ります。</p>
<p>[コラボレーション] リボンタブから [データ交換] ボタンを見つけてクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa2529200c-pi" style="display: inline;"><img alt="Inventor_ui" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa2529200c image-full img-responsive" src="/assets/image_516756.jpg" title="Inventor_ui" /></a></p>
<p>Inventor 側に表示される [Data Exchange] ダイアログでアカウントとプロジェクトを適宜ナビゲートして、Revit から作成されたデータ交換名を選択後、ダイアログ下部の [ロード] をクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa2563200c-pi" style="display: inline;"><img alt="Read_dx1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa2563200c image-full img-responsive" src="/assets/image_646802.jpg" title="Read_dx1" /></a></p>
<p>データ交換作成時に指定したカテゴリ（ここでは「階段」）が Inventor にアセンブリとして読み込まれます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2589c34200d-pi" style="display: inline;"><img alt="Read_dx3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2589c34200d image-full img-responsive" src="/assets/image_136441.jpg" title="Read_dx3" /></a></p>
<p>By Toshiaki Isezaki</p>
