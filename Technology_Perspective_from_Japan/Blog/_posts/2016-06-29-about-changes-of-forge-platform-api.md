---
layout: "post"
title: "Forge Platform API の変更について"
date: "2016-06-29 03:55:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/06/about-changes-of-forge-platform-api.html "
typepad_basename: "about-changes-of-forge-platform-api"
typepad_status: "Publish"
---

<p>既に<strong> <a href="http://adndevblog.typepad.com/technology_perspective/2016/06/renamed-forge-platform-api-and-added-new-api.html" target="_blank">お知らせ</a></strong> していますが、Autodesk Forge プラットフォームで利用できる API が、新しく編成し直されて、一部が名称変更されています。同時に、新しい 機能 も利用可能な状態になっています。今回、新しく&#0160;Autodesk Forge に適用されたルールがありますので、ここでご案内しておきます。</p>
<p><strong>View and Data API の分離について</strong></p>
<p style="padding-left: 30px;">従来、View and Data API と呼ばれていた API が、いくつかに分離されています。分離された理由は単純で、View and Data API &#0160;の一連にプロセスを分割して独立させることで、今後登場する API も含め、他の API からも再利用し易くなるためです。分離された API &#0160;には、次の 4 つが該当します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb091c76c7970d-pi" style="display: inline;"><img alt="Divided_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb091c76c7970d image-full img-responsive" src="/assets/image_873941.jpg" title="Divided_api" /></a></p>
<ol>
<li><strong>OAuth</strong>（Authentication）<br />API 呼び出しでストレージへのアクセス権を取得するため、Consumer Key と Consumer Secret から生成されたアクセス トークンを生成する認証プロセスを担います。<br /><br /></li>
<li><strong>Data Management API<br /></strong>API 専用領域にバケットの作成して、ファイルをアップロードしたりダウンロードする機能を提供します。今回から、OAuth で 3 legged 認証を使用することで、A360 や Fusion 360 などの SaaS が利用するユーザ領域にアクセスすることも出来るようにもなっています。<strong><br /><br /></strong></li>
<li><strong>Model Derivative API<br /></strong>Model Derivative API は、Web ブラウザにストリーミング配信して表示するための変換だけでなく、他のファイル形式への変換処理もカバーするようになっています。<br /><br /></li>
<li><strong>Viewer</strong>（Forge Viewer）<br />Web ブラウザに &#0160;2D 図面や 3D モデルを表示する JavaScript コード部分は、単に Viewer と呼ばれるようになっています。</li>
</ol>
<p><strong>Fusion 360 API</strong></p>
<p style="padding-left: 30px;">従来、スクリプトやアドインを作成して Fusion 360 をカスタマイズするためのクライアント API は、RESTful API を使ってクラウドとのコミュニケーションをおこなう必要がないことから、Forge プラットフォーム API として記載されなくなりました。もちろん、今後も Fusion 360 のカスタマイズに利用することは出来ますが、Forge の課金対象とはなりません。</p>
<p style="padding-left: 30px;">なお、クラウドに保存されている Fusion 360 データには、Data Management API によってアクセスして情報を抽出したり、Data Derivative API によって変換後に Viewer で Web ブラウザで表示させることが可能です。</p>
<p><strong>View and Data API 時の Consumer Key と Consymer Secret</strong></p>
<p style="padding-left: 30px;">API 共有領域にユニークな名称でバケットを作成して View and Data API を使用していた場合には、Consumer Key と Consumer Secret が適用される API が、Model Derivative API に変更されているはずです。デベロッパ ポータル（<a href="https://developer.autodesk.com/" target="_blank">https://developer.autodesk.com/</a>）にサインイン して、MyApps ページを確認してみてください。</p>
<p style="padding-left: 30px;">なお、View and Data API を使用した既存のサービスは、現在でも、そのまま動作するはずです。</p>
<p><strong>API 名称について</strong></p>
<p style="padding-left: 30px;">新しく API の名称に命名規則が適用されています。<strong>API</strong> という文字が付いているものは、一般に Web サービス API として知られている RESTful API を利用するもので、クラウドとの直接コミュニケーションをする API に限られます。Web ブラウザ内で HTML ファイルとともに表示を担当する Viewer に API が付かないのは、これが理由です。</p>
<p style="padding-left: 30px;">認証で利用する&#0160;OAuth も RESTful API を利用しますが、Web の世界で広く知られた仕様であるため、あえて API が付加されていません。</p>
<p>By Toshiaki Isezaki</p>
