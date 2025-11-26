---
layout: "post"
title: "日本語化された Revit 2014 API ドキュメントと Revit SDK"
date: "2014-06-10 16:30:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/06/localized-revit-api-document-revit-sdk.html "
typepad_basename: "localized-revit-api-document-revit-sdk"
typepad_status: "Publish"
---

<p>Autodesk Revit の API（Application Programming Interface）を利用することで、Revit 内でよくあるタスクを自動実行させたり、複雑な手順をプログラムに代替させて設計者の操作を簡素化させたりすることが出来ます。</p>
<p>このための実装方法が <strong>アドイン</strong>&#0160;や <strong>マクロ</strong>&#0160;と呼ばれるカスタマイズ モジュールです。詳細は、以前のブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2013/12/understanding-revit-api-for-autocad-addon-developers-part1.html" target="_blank"><strong>AutoCAD アドオン開発者のための Revit API 入門 ～ 概説</strong></a>&#0160;に譲りますが、初めて実際の開発作業をおこなう場合には、やはり、開発について説明するドキュメントが必要になります。</p>
<p>Revit API にも、API をどのような手順で利用したり、どのようなことが出来るのかを説明する「Developers Guide」ドキュメントが存在しています。このドキュメントは、従来から Revit のオンライヘルプに含まれていましたが、製品の一般機能に関するドキュメントとは異なり、残念ながら日本語化されていませんでした。</p>
<p>昨年リリースされた Revit 2014 では、日本の開発者の方の利便性を改善するため、オンラインヘルプと独立した形で&#0160;Developers Guide を日本語化して、 .chm 形式のドキュメントを ZIP 圧縮してダウンロード出来るようにしています。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b42274970d img-responsive"><a href="http://adndevblog.typepad.com/files/revit_api_developer_guide.zip">Revit 2014 API の日本語 Developer Guide をダウンロード</a></span></strong></p>
<p style="padding-left: 30px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b42274970d img-responsive" style="background-color: #ffff00;">※ ダウンロードして展開した .chm ファイルは、セキュリティ機能によってコンテンツ表示がブロックされる場合があります。コンテンツがブロックされている場合には、各ページが白く表示されて何もない状態となります。その場合には、エクスプローラから .chm ファイルを右クリックして [プロパティ] ダイアログの [全般] タブを開き、画面右下にある 「ブロックの解除」 にチェックを入れてください。</span></p>
<p>そして、Revit 2015では、 ようやく他のドキュメントと同じレベルで Revit API Developers Guide を翻訳して、公開することが出来ました。日本語化された <strong>Revit API 開発者用ガイド&#0160;</strong>は、<strong><a href="http://help.autodesk.com/view/RVT/2015/JPN/" target="_blank">Revit 2015 ヘルプ</a></strong>&#0160;の目次からアクセスすることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd1a4d3b970b-pi" style="display: inline;"><img alt="Revit_2015_developer_guide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd1a4d3b970b image-full img-responsive" src="/assets/image_553970.jpg" title="Revit_2015_developer_guide" /></a></p>
<p>Revit API 開発者用ガイドをご一読いただくことで、アドインの仕組みや考え方、Revit API の利用方法等を把握していただくことが出来るようになります。</p>
<p>一方、 Revit API を利用する際に必要となる、クラス、メソッドやプロパティといった内容を記されているリファレンス ドキュメントは、従来通り、Revit SDK に含まれています。リファレンス ドキュメントのファイル名は、従来と同じ <strong>RevitAPI.chm</strong> です。このドキュメントは、残念ながら英語版のみでの提供となっています。</p>
<p>さて、肝心の Revit SDK の入手方法ですが、こちらも従来のバージョンと同様に、製品インストーラからのインストールと、ダウンロードの 2 つの方法があります。製品インストーラから入手する場合には、「ツールとユーティリティ」項から Revit SDK（Revit Software Development Kit）からインストールを指定することが出来ます。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dd50fd6970d-pi" style="display: inline;"><img alt="Revit_2015_sdk" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73dd50fd6970d image-full img-responsive" src="/assets/image_986037.jpg" title="Revit_2015_sdk" /></a>&#0160;</p>
<p>一点注意が必要なのは、Revit SDK がまれに更新されることがある点です。Revit 2015 でも、すでに一度 SDK が更新されています。つまり、製品インストーラに含まれる Revit SDK は、最新の SDK とは言えなくなっています。最新の Revit SDK（Update May 14, 2014）は、<strong>日本語 Revit Developer Center <a href="http://www.autodesk.co.jp/developrevit" target="_blank">http://www.autodesk.co.jp/developrevit</a></strong> または、<strong>英語 Revit Developer Center&#0160;<a href="http://www.autodesk.com/developrevit" target="_blank">http://www.autodesk.com/developrevit</a></strong>&#0160;から、ダウンロードすることが出来ます。現在、どちらのページから同じものをダウンロードできます。初期バージョンの SDK との違いは、SDK に含まれる Revit Addin Manager の更新のみです。</p>
<p>なお、最新の Revit SDK でも、開発時に便利な Revit Lookup ツールは同梱されていません。Revit Lookup ツールの入手方法は、次のドキュメントでご案内していますので、併せてご参照ください。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d19a0a8f970c img-responsive"><a href="http://adndevblog.typepad.com/files/qa-8893.pdf" target="_blank">QA-8893 Revit 2015 SDK に Lookup ツールがない</a></span></strong></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
