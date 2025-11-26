---
layout: "post"
title: "View and Data API アプリ登録とキーの入手"
date: "2015-09-02 01:05:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/09/view-and-data-api-app-register-and-getting-keys.html "
typepad_basename: "view-and-data-api-app-register-and-getting-keys"
typepad_status: "Publish"
---

<p>View and Data API はクラウドを利用する Web サービス API です。各種 CAD データをクラウド上にアップロードすることで、どんなデバイスでも Web ブラウザで 2D 図面や 3D モデルを表示したり、データ内の検索が出来るように、ファイルを中間形式に変換する処理が必要です。</p>
<p>一般的なクラウド サービスでは、クラウドにアクセスするために「ログイン」または「サインイン」と呼ばれるアカウント認証が必要なのはご存じのとおりです。API を使ってクラウドにアクセスする場合にも、もちろん、アカウント認証に代わる処理が必要となります。ただ、ここで扱うことになるのは、ユーザ名やパスワードではありません。開発者に割り当てられた&#0160;<strong>Consumer Secret</strong>&#0160;と&#0160;<strong>Consumer Key</strong>&#0160;を利用して、<strong><a href="http://ja.wikipedia.org/wiki/OAuth" target="_blank">OAuth</a>&#0160;</strong>仕様に基づいたアクセス許可を得ることで、オートデスクが管理するストレージにアクセスします。</p>
<p>オートデスクのクラウド サービスで利用するストレージについては、過去のブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2015/01/a360-api-and-storage.html" target="_blank"><strong>A360 API とストレージの考え方</strong></a>&#0160;で概要をご紹介しています。</p>
<p>つまり、View and Data API を使うには、&#0160;Consumer Secret&#0160;と&#0160;Consumer Key&#0160;を事前に取得しておく必要があります。ここでは、これらキーの取得方法をご案内します。</p>
<p><strong>Consumer Secret&#0160;と&#0160;Consumer Key&#0160;の入手手順</strong></p>
<ol>
<li><strong>Consumer Secret</strong>&#0160;と&#0160;<strong>Consumer Key</strong>&#0160;を取得するには、はじめに<a href="https://developer.autodesk.com/" target="_blank"><strong>オートデスク デベロッパ ポータル（https://developer.autodesk.com）</strong></a>にアクセスします。</li>
<li>デベロッパ ポータルにアクセスすると、画面の右上に [Sign Up] と [Sign In] の 2 つのリンクボタンが表示されます。既に Autodesk ID をお持ちであれば、[Sign In] をクリックしてサインインしてください。このとき、デベロッパ ポータルへはじめてサインインする場合は、下記の 4. へ、一度サインインしたことがある場合には、6. へ進んでください。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c7c065970b-pi" style="display: inline;"><img alt="Sign_in" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c7c065970b image-full img-responsive" src="/assets/image_286605.jpg" title="Sign_in" /><br /></a></li>
<li>もし、Autodesk ID をお持ちでなければ、[Sign Up] をクリックして Autodesk ID を作成してください。Autodesk ID は、無償で作成することが出来ます。必要な情報を入力して、[サイン アップ] をクリックするだけです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d151ad0c970c-pi" style="display: inline;"><img alt="Sign_up" class="asset  asset-image at-xid-6a0167607c2431970b01b8d151ad0c970c img-responsive" src="/assets/image_234175.jpg" style="width: 350px;" title="Sign_up" /><br /></a></li>
<li>デベロッパ ポータルに初めてサインインをおこなうと、アカウントを作成した本人かどうか確認する処理が実施されます。次のページが表示されます。Autodesk ID 作成時に登録した電子メールアドレスのメールボックスを確認してください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c0f18970d-pi" style="display: inline;"><img alt="Confirmation" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c0f18970d img-responsive" src="/assets/image_799322.jpg" style="width: 350px;" title="Confirmation" /></a></li>
<li>メールボックスに「Autodesk アカウントを確認」 というタイトルのメールが届いているはずです。メールを開いて、[電子メールを確認] リンクをクリックして本人確認を終了します。<br /><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d151ad3b970c-pi" style="display: inline;"><img alt="Confirmation_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d151ad3b970c image-full img-responsive" src="/assets/image_88812.jpg" title="Confirmation_email" /></a>&#0160;</li>
<li>サインインが完了すると、右上に Autodesk ID のアカウント名が表示されます。続いて、[Create an App] をクリックして、アプリを登録することで Consumer Key &#0160;と Consumer Secret を取得していきます。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c0fb4970d-pi" style="display: inline;"><img alt="Create_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c0fb4970d image-full img-responsive" src="/assets/image_599005.jpg" title="Create_app" /><br /></a></li>
<li>表示されるページ上部の部分で、登録するアプリで使用する API を選択します。このブログの執筆の時点で、AutoCAD I/O API と View and Data API の 2 つを選択することが出来ます。ここでは、View and Data API をクリックして選択します。&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7c7c25c970b-pi" style="display: inline;"><img alt="Select_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7c7c25c970b image-full img-responsive" src="/assets/image_965809.jpg" title="Select_api" /><br /></a></li>
<li>&#0160;ページ下部にアプリの情報を入力します。上から、アプリ名（App Name）、アプリの説明（App description）、コールバック URL（Callback URL）、自身の持つ Web サイトの URL（Your Web Site URL）を入力して、[Create App] をクリックしてください。なお、コールバック URL は、OAuth2 3-legged で利用する URL を指しますが、ここではダミーで結構です。<br /><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c0ff3970d-pi" style="display: inline;"><img alt="Create_app2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c0ff3970d image-full img-responsive" src="/assets/image_466554.jpg" title="Create_app2" /><br /></a></li>
<li>&#0160;[Create App] をクリックしてアプリが登録されると、画面上にアプリの情報とともに、Consumer Key と Consumer Secret が表示されるはずです。Consumer Secret は既定値で非表示になっていますが、<strong>Show</strong>&#0160;をクリックすることで画面に表示されます。逆に <strong>Hide</strong> をクリックすると非表示になります。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c108e970d-pi" style="display: inline;"><img alt="My_app_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c108e970d image-full img-responsive" src="/assets/image_983758.jpg" title="My_app_info" /></a></li>
</ol>
<p>これで、アプリの登録と、そのアプリ用に割り当てられた Consumer Key と Consumer Secret の取得が完了です。&#0160;この Consumer Key と Consumer Secret は、ユーザ名とパスワードに相当するもので、アプリを開発する開発者を特定するものとなります。もちろん、通常公開すべきものではありませんので、開発者は外部に漏れないよう注意してください。</p>
<p>なお、 Consumer Secret は、いつでも、<strong>Regenerate</strong> で再生成することが出来ます。ただし、再生成後には、再生成前の Consumer Secret を利用していた既存アプリやサービスは動作しなくなります。注意してください。登録したアプリは、アプリ自体を削除したり、登録アプリの内容を変更することも可能です。&#0160;</p>
<p>&#0160;</p>
<p>-----------------------&#0160;<strong>参考：OAuth とは？</strong>&#0160;</p>
<p style="padding-left: 30px;">さて、Consumer Key と Consumer Secret 、あるいは&#0160;OAuth や OAuth2 とはいったい何なのか？という方もいらっしゃると思います。簡単な例で説明するなら、次のようなアクセス権限付与の仕組みです。</p>
<ul style="padding-left: 30px;">
<li>この Autodesk Perspective From Japan ブログを含むオートデスクの<strong><a href="http://www.autodesk.com/blogs" target="_blank">ブログ</a></strong>では、<strong>Typedpad</strong>（<a href="http://www.typepad.com" target="_blank">http://www.typepad.com</a>）という企業のブログ システムを利用しています。</li>
<li>Typedpad ブログでは、ブログオーナーが公開した記事に、読者のコメントを投稿する仕組みが用意されています。ただし、基本的には、コメントの投稿でも Typedpad のアカウントでサインインが求められます。</li>
<li>読者がブログ記事にコメントを投稿する際に、いちいち&#0160;Typedpad のアカウントを作成するのは面倒です。</li>
<li>そこで、OAuth を使ったアクセス権限付与の仕組みが用意されています。「コメントを投稿」の部分には、Typedpad アカウント以外のアイコンが配置されていて、いずれかの SNS アカウントがあれば、そのアカウントを使って&#0160;Typedpad &#0160;へコメントを投稿することが出来ます。つまり、Typedpad へのアクセス権限を得ることが出来るようになります。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d151af93970c-pi" style="display: inline;"><br /></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c1490970d-pi" style="display: inline;"><img alt="Oauth" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c1490970d img-responsive" src="/assets/image_389083.jpg" title="Oauth" /></a></li>
<li>もし、ここで Facebook アカウントでサインインしてコメントを投稿しようとすると、必ず、同意を求める画面が表示されるはずです。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086c132a970d-pi" style="display: inline;"><img alt="OAuth代理認証確認" class="asset  asset-image at-xid-6a0167607c2431970b01bb086c132a970d img-responsive" src="/assets/image_637916.jpg" style="width: 420px;" title="OAuth代理認証確認" /><br /></a></li>
<li>ここで使われているのが OAuth です。SNS &#0160;や Web ショッピングをお使いの方は、一度は似たような画面を目にしたことがあるかも知れません。</li>
</ul>
<p style="padding-left: 30px;">View and Data API を始めとして、オートデスクの Web サービス API では、この OAuth を使ってクラウド ストレージにアクセスしています（現在は OAuth2）。OAuth の概要は、<a href="https://ja.wikipedia.org/wiki/OAuth" target="_blank"><strong>こちら</strong></a> を参照してみてください。</p>
<p style="padding-left: 30px;">もう少し詳しく、OAuth の動作を理解したい場合には、<a href="http://www.atmarkit.co.jp/ait/articles/1208/27/news129.html" target="_blank"><strong>＠IT の記事</strong></a> が分かり易いと思います。また、<a href="http://www.atmarkit.co.jp/ait/articles/1209/10/news105.html" target="_blank"><strong>こちらの記事</strong></a> では、OAuth2 については、上記のような利用方法が解説されています。</p>
<p style="padding-left: 30px;">Web の世界では、異なるアカウント認証の手続きを出来るだけ簡素化することで、ユーザの利便性を高めていると理解することが出来ます。</p>
<p>-----------------------&#0160;&#0160;</p>
<p>By Toshiaki Isezaki</p>
