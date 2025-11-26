---
layout: "post"
title: "RESTful API とテスト ツール"
date: "2016-09-21 02:56:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/09/restful-api-and-testing-tools.html "
typepad_basename: "restful-api-and-testing-tools"
typepad_status: "Publish"
---

<p>Forge Platform API の多くは、オートデスクのクラウドに対してリソースをリクエストしたり、処理をさせたりする目的で&#0160;RESTful API&#0160; を使用しています。RESTful API には、そのリクエスト内容によっていくつかのメソッドが定義されています。よく利用される代表的なメソッドは次のとおりです。&#0160;</p>
<ul>
<li><strong>GET</strong> – Web サービスでリソースの取得に利用されるメソッド</li>
<li><strong>POST</strong> – Web サービスで新しいリソースの作成に利用されるメソッド</li>
<li><strong>PUT</strong> – Web サービスでリソースの更新に利用されるメソッド</li>
<li><strong>PATCH</strong> – Web サービスでどう編集されるべきかを記述してリソースを更新するメソッド</li>
<li><strong>DELETE</strong> – Web サービス上のデータ アイテムの削除に利用されるメソッド</li>
</ul>
<p>RESTful API は HTTP プロトコルを利用して各種メッセージをサーバー（クラウド）にリクエストします。このとき、Header と Body にリクエストに必要なパラメータを指定することになります。Forge Platform API でも、、当然、この方法に沿ってリクエストをすることになります。</p>
<p>Forge に限らず、RESTful API をテストする場合には、いきなりコーディングして 個々の <a href="http://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html#_endpoint" target="_blank"><strong>endpoint</strong>&#0160;（エンドポイント）</a>&#0160;を呼び出すのではなく、事前にテストしたい場合があります。そのような場面では、Web 上で公開されてるテスト ツールを利用することが出来ます。無償ツールも含め、非常に多くのツールが公開されていますので、インターネット検索で <strong>&quot;<a href="http://lmgtfy.com/?q=restful+api+%E3%83%86%E3%82%B9%E3%83%88%E3%83%84%E3%83%BC%E3%83%AB" target="_blank">restful api テストツール</a>&quot;</strong> のキーワードを使って検索してみてください。</p>
<p>オートデスクも、過去類似したテスト ツール <strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/11/api-console-usage.html" target="_blank">API Console</a></strong> をデベロッパ ポータルに搭載していた時期がありますが、現在では、次のツールを説明時などに利用しています。それぞれ、記載された URL からダウンロードしてインストールすることが出来ます。</p>
<ul>
<li><strong>Fiddler - <a href="http://www.telerik.com/fiddler" target="_blank">http://www.telerik.com/fiddler</a></strong></li>
<li><strong>Postman - <a href="https://www.getpostman.com/" target="_blank">https://www.getpostman.com/</a></strong></li>
</ul>
<p>オートデスクが Forge Viewer、A360 Viewer とも呼ばれる&#0160; Viewer を実装テストする場合には、Google Chrome を基準にテストしているので、ここでは、Google アプリとして公開されていて親和性の高い <strong>Postman</strong> を使って、簡単に RESTful API のテスト方法をご紹介しましょう。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8957dc3970b-pi" style="float: right;"><img alt="Postman2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8957dc3970b img-responsive" src="/assets/image_265909.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Postman2" /></a>オートデスクのデスクトップ製品用アドイン開発では、どうしても &#0160;Windows が主体になりがちですが、Web 開発では Mac が好まれる傾向もあります。その意味で、Postman が Mac でも利用できるという利点もあります。</p>
<hr />
<p>Forge Platform API を利用する際に最初に利用する RESTful API に、Access Token を取得する <strong><a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/" target="_blank">Authenticate API（OAuth API）</a></strong>があります。いわゆる認証処理ですが、この endpoint の詳細は、<strong><a href="https://developer.autodesk.com/" target="_blank">デベロッパ ポータル</a></strong>に記載されています。2-legged 認証の場合には、<strong><a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/" target="_blank">https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/</a></strong> に呼び出しに必要な情報（endpoint、header、body）が記載されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0938eefe970d-pi" style="display: inline;"><img alt="Oauth" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0938eefe970d image-full img-responsive" src="/assets/image_483070.jpg" title="Oauth" /></a></p>
<p>&#0160;この記述からは、次の情報を読み取ることが出来ます。</p>
<ul>
<li><strong>POST</strong> メソッドを利用</li>
<li>endpoint は<strong>&#0160;https://developer.api.autodesk.com/authentication/v1/authenticate</strong></li>
<li>Header には<strong>&#0160;Content-Type</strong> パラメータに&#0160;<strong>application/x-www-form-urlencoded</strong> 値の指定が必須（Required が Yes）</li>
<li>Body には、次のパラメータと対応する値の設定が必須
<ul>
<li><strong>client_id</strong> に <a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" target="_blank">&#0160;<strong>アプリ登録で取得</strong></a>&#0160;した Consumer Key を指定</li>
<li><strong>client_secret</strong> に&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" target="_blank"><strong>アプリ登録で取得</strong></a>&#0160;した Consumer Secret を指定</li>
<li><strong>grant_type</strong> に&#0160;<strong>client_credentials</strong> の指定が必須</li>
<li><strong>scope</strong> にアクセス権限を指定（2016年6月15日以降に登録したアプリは必須）&#0160;</li>
</ul>
</li>
</ul>
<hr />
<p>この呼び出しは、次の手順で Postman でテストすることが出来ます。なお、ここでは認証処理をテストすることになるので、Authirization タブは利用しないことにしています。</p>
<ol>
<li>Postman を起動</li>
<li>メソッドを POST に変更して、その右隣に endpoint を入力</li>
<li>Headers タブをアクティブにして&#0160;Content-Type パラメータと値&#0160;application/x-www-form-urlencoded を入力<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d21f4f12970c-pi" style="display: inline;"><img alt="Oauth_body" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d21f4f12970c image-full img-responsive" src="/assets/image_647125.jpg" title="Oauth_body" /></a><br /><br /></li>
<li>Body タブをアクティブにして、client_id、client_secret、grant_type、scope の各パラメータと対応する値をそれぞれ入力<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0938f0bb970d-pi" style="display: inline;"><img alt="Oauth_body" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0938f0bb970d image-full img-responsive" src="/assets/image_358391.jpg" title="Oauth_body" /></a><br /><br /></li>
<li>画面右上にある&#0160;<span style="background-color: #4040ff; color: #ffffff;"> &#0160;Send &#0160;&#0160;</span>ボタンをクリック</li>
</ol>
<p>正しく実行されると、このリクエストに対するステータスと応答メッセージが返されます、応答メッセージには、 JSON 形式が採用されています。ここでは、Access Token の値として&#0160;7oPR4ha6kavJOMa96SIr5fSgx76F の文字列が返されていることがわかります。expires_in に示されている値は、この Access Token の有効期間です（秒単位）。</p>
<p>&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c895808c970b-pi" style="display: inline;"><img alt="Oauth_response" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c895808c970b image-full img-responsive" src="/assets/image_693144.jpg" title="Oauth_response" /></a></p>
<hr />
<p>一度テストした RESTful 呼び出しは、endpoind 毎に名前を付けて保存しておくことも出来ます。他の endpoint も同様にテストして保存しておくと、コーディングを始めた後にプログラム上で問題が発生した場合でも、コード上の問題なのか、endpoint を含む API 側の問題なのか、切り分けの判断で利用することが出来るので便利かもしれません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0938f11e970d-pi" style="display: inline;"><img alt="Save" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0938f11e970d image-full img-responsive" src="/assets/image_100406.jpg" title="Save" /></a></p>
<p>始めて RESTful API に触れる方は、ぜひ試してみてください。</p>
<p>By Toshiaki Isezaki</p>
