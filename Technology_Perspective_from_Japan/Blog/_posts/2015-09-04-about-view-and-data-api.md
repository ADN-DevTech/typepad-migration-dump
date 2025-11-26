---
layout: "post"
title: "View and Data API とは ?"
date: "2015-09-04 00:14:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/09/about-view-and-data-api.html "
typepad_basename: "about-view-and-data-api"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>&#0160;</p>
<p>改めて、View and Data API の概要をまとめておきたいと思います。オートデスクは、AutoCAD 360 や Fusion 360、BIM 360 Glue や BIM 360 Field、ReCap 360 など、適用業種に応じて多様なクラウド サービスを提供していて、すべてを包括して&#0160;<strong>A360</strong>&#0160;というブランド名でご案内しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c8cdad970b-pi" style="display: inline;"><img alt="A360_services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c8cdad970b image-full img-responsive" src="/assets/image_823546.jpg" title="A360_services" /></a></p>
<p>これらクラウド サービスの中核になるのが、クラウド上に用意されたストレージ機能です。このストレージ サービスには、設計者やデザイナーが設計データを表示するためのビューワ機能と、関係者との協調作業をサポートするコラボレーション機能 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank">Live Review</a></strong>&#0160;が組み込まれています。</p>
<p>A360 には、設計やデザイン、建築や建設、施工や製造 といった様々な場面で、使っていただけるようなコンセプトがあります。問題は、それら業務に携わる方々のすべてがオートデスク製品をお使いではない、という点です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d152b47e970c-pi" style="display: inline;"><img alt="Various_design_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d152b47e970c image-full img-responsive" src="/assets/image_208294.jpg" title="Various_design_data" /></a></p>
<p>そこで、一般に流通しているデザイン データを一括して扱えるような仕組みを A360 に組み込みました。もっとも基本的なものが、デザインデータの表示です。従来のように、専用のビューワ ソフトウェアなどを一切、追加インストールすることなく、Web ブラウザだけで表示出来るようにしています。そこで使われているテクノロジが、<a href="https://ja.wikipedia.org/wiki/WebGL" target="_blank"><strong>WebGL</strong></a> です。WebGL に対応した Web ブラウザがインストールされていれば、ほかに何も必要ありません。</p>
<p>また、Web ブラウザ上での表示に際して、A360 ストレージにアップロードしたデザイン データを一元的に表示できる形式に変換する仕組みも取り入れています。現在では、約 60 種類のデザインデータを変換、表示することが出来るようになっています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c8cf36970b-pi" style="display: inline;"><img alt="Translation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c8cf36970b img-responsive" src="/assets/image_468918.jpg" title="Translation" /></a></p>
<p>変換されたデザイン データを Web ブラウザで表示する際には、セキュリティを考慮して、HTML5 ベースの&#0160;<a href="https://ja.wikipedia.org/wiki/%E3%82%B9%E3%83%88%E3%83%AA%E3%83%BC%E3%83%9F%E3%83%B3%E3%82%B0" target="_blank"><strong>ストリーミング</strong></a>&#0160;が利用されています。ファイルそのものを丸ごとダウンロードしてキャッシュするのではなく、表示に必要な差分データを転送しながら表示するため、大規模データでも短い時間で軽快に表示することが出来ます。</p>
<p>表示されるデザイン データは、そのデータを作成した CAD/CG ツールで適用したマテリアルを、最大限そのまま再現出来るようになっています。また、CAD データにある属性やプロパティといったメタデータは、プロパティ画面で確認出来だけでなく、与えられたキーワードに該当したオブジェクトを見つけ出す、検索機能も提供されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c8d076970b-pi" style="display: inline;"><img alt="Search" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c8d076970b image-full img-responsive" src="/assets/image_995961.jpg" title="Search" /></a></p>
<p>現在では、モデル分解やライブ断面の作成、コメント（マークアップ）などのビューワ機能も提供されてきています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d152b7cd970c-pi" style="display: inline;"><img alt="Explode_section" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d152b7cd970c image-full img-responsive" src="/assets/image_668255.jpg" title="Explode_section" /></a><br /><br />このビューワ機能は、通常、A360 ストレージ サービスの中で利用することが出来ます。もちろん、A360 へのアクセスするは、Autodesk ID でユーザ管理されていて、アップロードしたデザインデータは、そのアカウントで A360 にサインインした場合にしか表示することは出来ません（共有機能で他のユーザに共有した場合を除く）。</p>
<p>そして、A360 &#0160;ストレージ サービスと関係なく、自由にビューワ機能をカスタマイズ出来る API として提供されるのが <a href="http://developer-autodesk.github.io/" target="_blank"><strong>View and Data API</strong></a> です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8204aeb970b-pi" style="float: right;"><img alt="Lmv_client_api" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8204aeb970b img-responsive" src="/assets/image_787800.jpg" style="width: 180px; margin: 0px 0px 5px 5px;" title="Lmv_client_api" /></a></p>
<p>WebGL をサポートする主要な Web ブラウザがあれば、デスクトップとモバイルの両方で、60 種類を超える 3D データ形式を含むデザイン データ &#0160;ファイルを表示する機能を利用することが出来ます。もちろん、単に 2D/3D の形状を表示するだけでなく、モデルや図形に埋め込まれたメタデータを API で検索して取得することができます。カスタマイズには、クラウドとのコミュニケーションに RESTful API を、クライアントとなる Web ブラウザ上の表示制御に JavaScript を利用します。</p>
<p>View and Data API のクライアント側 API である JavaScript API は、WebGL API と three,js 上に構築された JavaScript ライブラリです。このため、カンバス領域を共有して独自の表現を実装するようなことも可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8204b24970b-pi" style="display: inline;"><img alt="Summary" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8204b24970b image-full img-responsive" src="/assets/image_284599.jpg" title="Summary" /></a></p>
<p>View and Data API を使った ビューワ サンプルは、<a href="https://developer.static.autodesk.com/interactive/liveSample.html" target="_blank"><strong>こちら</strong></a> から参照出来ますので、まずは、何が出来るかをお試しください。もし、手持ちのデザイン データで表示をお試しいただくなら、少し制約がありますが、無料の <strong><a href="https://360.autodesk.com/viewer" target="_blank">A360 オンライン ビューワ</a>&#0160;</strong>もお使いいただけます。</p>
<p>By Toshiaki Isezaki</p>
