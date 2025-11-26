---
layout: "post"
title: "Autodesk App ストアと BIM 360 アプリ"
date: "2017-10-09 00:24:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/10/autodesk-app-store-and-bim-360-apps.html "
typepad_basename: "autodesk-app-store-and-bim-360-apps"
typepad_status: "Publish"
---

<p>以前のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2017/07/announcing-bim-360-online-hackathon.html" rel="noopener noreferrer" target="_blank"><strong>BIM 360 Online Hackathon / Webinar</strong></a> のお知らせのとおり、BIM 360 Team や BIM 360 Docs と連携するアプリを公開出来るよう Autodesk App ストアが拡張されています。すでに、どのようなアプリが構築可能なのか、その可能性を示すべく、いくつかのサンプル アプリが無料で公開されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9276603970b-pi" style="display: inline;"><img alt="Bim_360_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9276603970b image-full img-responsive" src="/assets/image_766327.jpg" title="Bim_360_store" /></a></p>
<p>現在公開されているアプリでは、特に、3-leggeed OAuth を使って BIM 360 Team、または、BIM 360 Docs&#0160; などのユーザ領域に保存されている Revit ファイルにアクセスして 3D モデルを表示するとともに。その Revit ファイルに格納されている BIM 情報を Excel シートに書き出すアプリ、<strong><a href="https://apps.autodesk.com/BIM360/ja/Detail/Index?id=6539893793294860527&amp;appLang=en&amp;os=Web" rel="noopener noreferrer" target="_blank">Autodesk® BIM 360® Excel Exporter</a></strong> がユニークです。</p>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/800d2xmQl0s?feature=oembed" width="459"></iframe></p>
<p class="asset-video">ちなみに、Autodesk BIM 360 Excel Exporter のソースコードは&#0160;<a href="https://github.com/Autodesk-Forge/bim360appstore-model.derivative-nodejs-xls.exporter" rel="noopener noreferrer" target="_blank"><strong>https://github.com/Autodesk-Forge/bim360appstore-model.derivative-nodejs-xls.exporter</strong></a> で公開されています。</p>
<p>こららのアプリは、AutoCAD や Revit アドインなどと同じように Autodesk App ストアに新しく作られた BIM 360 カテゴリ ページ（<strong><a href="https://apps.autodesk.com/BIM360/ja/Home/Index" rel="noopener noreferrer" target="_blank">https://apps.autodesk.com/BIM360/ja/Home/Index</a></strong>）から参照していただくことが出来ます。今後、BIM 360 Hackathon で有用と認められた BIM 360 連携アプリも記載されていく予定です。</p>
<p>さて、BIM 360 に連携するアプリや Web サービスは、App ストアで公開されている他のカテゴリ アプリと同じように、無償、有償、試用版問わず、どなたでも無償で公開することが出来ます。アプリ公開の一般的な情報は <strong><a href="http://www.autodesk.co.jp/developapps" rel="noopener noreferrer" target="_blank">http://www.autodesk.co.jp/developapps</a></strong> でご参照いただけます。BIM 360 連携アプリ固有の公開要件は&#0160; <strong><a href="http://www.autodesk.co.jp/adsk/servlet/item?siteID=1169823&amp;id=26838457" rel="noopener noreferrer" target="_blank">Autodesk App ストア - BIM 360 デベロッパ向けの情報</a></strong>&#0160;に記載されています。また、BIM 360 連携アプリの構築にい必要な Webinar 動画は <strong><a href="https://bim360hackathon.devpost.com/details/webinars" rel="noopener noreferrer" target="_blank">https://bim360hackathon.devpost.com/details/webinars</a></strong> から参照いただけます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ca876b970d-pi" style="float: right;"><img alt="Bim-360-icon-128px" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ca876b970d img-responsive" src="/assets/image_930754.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Bim-360-icon-128px" /></a>過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html" rel="noopener noreferrer" target="_blank">BIM 360 Docs と Data Management API アクセス</a></strong> でご案内のとおり、Forge を利用すれば、BIM 360 Team や BIM 360 Docs に格納されているデザイン データや各種設計図書（ファイル）にアプリからアクセス可能です。 ストレージ連携だけでなく、既存の業務ワークフローへの組み込みなど、多様なアプリ構築もアイデア次第です。これを機に、BIM 360 連携アプリの公開をご検討ください。</p>
<p>By Toshiaki Isezaki</p>
