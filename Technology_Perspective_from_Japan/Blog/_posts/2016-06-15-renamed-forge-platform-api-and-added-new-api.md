---
layout: "post"
title: "Forge Platform API の一部名称変更と新しい API について"
date: "2016-06-15 22:55:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/06/renamed-forge-platform-api-and-added-new-api.html "
typepad_basename: "renamed-forge-platform-api-and-added-new-api"
typepad_status: "Publish"
---

<p>本日から始まった Forge Developer Conference にて、Forge プラットフォーム API の利用コストの発表に加え、既存の Web サービス API の名称変更と新しい API の発表がありました。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f84607970c-pi" style="display: inline;"><img alt="Apis" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f84607970c img-responsive" src="/assets/image_301483.jpg" style="width: 600px;" title="Apis" /></a></p>
<p>まずは、簡単ですが、API の名称変更と新しい API について概要をお知らせしておきたいと思います。まず、名称変更については、下記のようになっています。</p>
<table width="919">
<tbody>
<tr>
<td width="369">
<p><strong>いままでの名称</strong></p>
</td>
<td width="550">
<p><strong>新しい名称</strong></p>
</td>
</tr>
<tr>
<td width="369">
<p><strong>View and Data API</strong></p>
</td>
<td width="550">
<p><strong>Model Derivative API + Viewer</strong></p>
</td>
</tr>
<tr>
<td width="369">
<p><strong>AutoCAD I/O</strong></p>
</td>
<td width="550">
<p><strong>Design Automation API</strong></p>
</td>
</tr>
<tr>
<td width="369">
<p><strong>ReCap Photo API</strong></p>
</td>
<td width="550">
<p><strong>Photo to 3D API</strong></p>
</td>
</tr>
<tr>
<td width="369">
<p><strong>Spark SDK</strong></p>
</td>
<td width="550">
<p><strong>3D Print API</strong></p>
</td>
</tr>
</tbody>
</table>
<p>なお、「API」は、RESTful API を利用するもののみに付けられるようになっている点にご注意ください。Viewer には API は付きません。&#0160;</p>
<p>&#0160;</p>
<p><strong>名称変更された API と新しい API&#0160;</strong></p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116bf4970d-pi" style="float: left;"><img alt="Viewer" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116bf4970d img-responsive" src="/assets/image_710310.jpg" style="margin: 0px 5px 5px 0px;" title="Viewer" /></a><strong>Viewer<br /></strong>Web ブラウザを利用して 50 種類以上のファイル形式から 関連付けられたデータと 2D 及び 3D デザイン ファイルを表示します。コメントやマークアップ、計測も可能です。旧 View and Data API のクライアント用 JapaScript API に相当するものです。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116c14970d-pi" style="float: left;"><img alt="Model_derivative_api" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116c14970d img-responsive" src="/assets/image_169965.jpg" style="margin: 0px 5px 5px 0px;" title="Model_derivative_api" /></a></p>
<p style="padding-left: 30px;"><strong>Model Derivative API<br /></strong>1 つの形式から他へデザイン ファイルに変換して、Viewer を使ったオンライン表示の準備をしたり、ジオメトリ データの展開をさせることが出来ます。展開データを他のアプリケーションに渡して、重要なデザイン情報のコミュニケーションに流用出来ます。なお、旧 View and Data API の RESTful API に相当する部分もカバーされます。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c86dfb64970b-pi" style="float: left;"><img alt="Design_automation_api" class="asset  asset-image at-xid-6a0167607c2431970b01b7c86dfb64970b img-responsive" src="/assets/image_100623.jpg" style="margin: 0px 5px 5px 0px;" title="Design_automation_api" /></a></p>
<p style="padding-left: 30px;"><strong>De</strong><strong>s</strong><strong>ign</strong> <strong>Au</strong><strong>t</strong><strong>oma</strong><strong>t</strong><strong>ion</strong> <strong>API<br /></strong>クラウド上で AutoCAD で動作するコアモジュールを利用して、AutoCAD のスクリプトや API の実行することを可能にするWeb サービス API です。主に Web アプリケーションで利用する図面生成エンジンとして活用されていますが、数千の DWG ファイルをバッチ処理で PDF ファイルに変換するサービスもあります。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116c37970d-pi" style="float: left;"><img alt="Authentication" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116c37970d img-responsive" src="/assets/image_213001.jpg" style="margin: 0px 5px 5px 0px;" title="Authentication" /></a></p>
<p style="padding-left: 30px;"><strong>Authen</strong><strong>t</strong><strong>ica</strong><strong>t</strong><strong>ion<br /></strong>Forge プラットフォームにアクセスするための認証と許可をするためのオープン スタンダードです。認証は、3rd party デベロッパがユーザ資格情報を漏えいすることなく、制限されたアクセスで特定機能の実行を可能にする &quot;キー&quot; を提供する安全な方法です。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116c4d970d-pi" style="float: left;"><img alt="Data_managemnt_api" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116c4d970d img-responsive" src="/assets/image_417331.jpg" style="margin: 0px 5px 5px 0px;" title="Data_managemnt_api" /></a><strong>Data</strong> <strong>Management</strong> <strong>API<br /></strong>A360、Fusion 360、BIM 360 Docs と Forge ネイティブな Object Storage Service（OSS）間のデータを管理します。この API は、1 つの一貫した方法で、異なるオートデスク製品から生成されたデータ ファイルのアップロードとダウンロードを可能にします。つまり、いままで実現出来なかった、A360 をはじめとするオートデスクの SaaS が利用するユーザ アカウント領域へのアクセスが出来るようになります。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116cd5970d-pi" style="float: left;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<img alt="3d_print_api" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116cd5970d img-responsive" src="/assets/image_606467.jpg" style="width: 71px; margin: 0px 5px 5px 0px;" title="3d_print_api" /></a></p>
<p style="padding-left: 30px;"><strong>3D</strong> <strong>P</strong><strong>rint</strong> <strong>API</strong> <strong>(ベータ)<br /></strong>3D プリントの準備から印刷管理まで、カスタマイズされた 3D プリント &#0160;ソリューションを素早く構築することを可能にします。この API は、メッシュのリペアやスライス用のツール等で、ユーザが &#0160;3D プリント用モデルの準備する手助けするものです。リモート監視での 3D プリンタへのファイル配信と &#0160;3D プリントの制御、または、3D プリント用にモデルを修復するサンプル アプリの使用などが可能です。</p>
<p style="padding-left: 30px;">&#0160;<br />&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09116c80970d-pi" style="float: left;"><img alt="Reality_capture_api" class="asset  asset-image at-xid-6a0167607c2431970b01bb09116c80970d img-responsive" src="/assets/image_471960.jpg" style="margin: 0px 5px 5px 0px;" title="Reality_capture_api" /></a></p>
<p style="padding-left: 30px;"><strong>R</strong><strong>ea</strong><strong>l</strong><strong>ity</strong><strong> C</strong><strong>aptu</strong><strong>re</strong> <strong>API</strong> <strong>(ベータ)<br /></strong>複数の写真を 3D データにする旧 ReCap Photo API です。もし、写真にUAV/ドローンで撮影された位置情報を含む場合には、演算されたリアリティ データには地理的位置を持つオルソビューが含まれます。リアリティ データは、Web アプリやデスクトップ アプリでの利用のために、オートデスクのクラウド サービスやパートナーのプラットフォームでアクセス可能です。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1f7cee8970c-pi" style="float: left;"><img alt="Bim360_api" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1f7cee8970c img-responsive" src="/assets/image_767876.jpg" style="margin: 0px 5px 5px 0px;" title="Bim360_api" /></a></p>
<p style="padding-left: 30px;"><strong>BIM</strong> <strong>360</strong> <strong>API<br /></strong>シームレスな建設業向けソフトウェア システムを作成するために、3rd party アプリケーションが BIM 360 プラットフォームに接続する機能を提供します。BIM 360 API でインテグレーションすることで、設計者やエンジニア、関連業者が手動でデータの入力をしたり、データ品質や一貫性を改善したり、また、建設ワークフローの自動化をおこなう手助けをします。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p>&#0160;</p>
<p><strong>デベロッパ ポータルの更新</strong></p>
<p style="padding-left: 30px;">上記の変更に先立って、6 月 9 日からデベロッパ ポータル（<a href="http://developer.autodesk.com" target="_blank">http://developer.autodesk.com</a>) の内容が更新されています。また、API が正常に稼働しているかを表示するヘルス ステータスも確認できるようになっています。ヘルス ステータスは、Support メニューの API Status から表示させることが出来ます。残念ながら、オートデスク SaaS のヘルス ダッシュボードとは異なり、状態変化が発生した場合などにメール通知を配信する機能は、今のところありません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c86e8262970b-pi" style="display: inline;"><img alt="Api_status1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c86e8262970b image-full img-responsive" src="/assets/image_575062.jpg" title="Api_status1" /></a></p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c86e8267970b-pi" style="display: inline;"><img alt="Api_status2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c86e8267970b image-full img-responsive" src="/assets/image_624100.jpg" title="Api_status2" /></a></p>
<p>By Toshiaki Isezaki</p>
