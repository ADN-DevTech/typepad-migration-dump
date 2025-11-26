---
layout: "post"
title: "Web ブラウザのデベロッパーツールについて"
date: "2018-10-29 00:03:27"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html "
typepad_basename: "about-developer-tool-on-web-browser"
typepad_status: "Publish"
---

<p>Forge アプリ開発をする上では、基本中の基本ですが、今日は Web ブラウザで利用出来るデベロッパーツール/開発者ツールについて、改めてご紹介しておきたいと思います。</p>
<hr />
<p><strong>Web ブラウザで表示、実行されるコードはまる見え</strong></p>
<p style="padding-left: 30px;">Forge が提供する API で Web ブラウザ内で実行されるのは、Forge Viewer として提供される Viewer JavaScript API のみです。Model Derivative API や Data Management API などの他の API は、Forge サーバー（クラウド）とのコミュニケーションで使われる <strong><a href="https://ja.wikipedia.org/wiki/Representational_State_Transfer" rel="noopener noreferrer" target="_blank">RESTful</a></strong> API なので、Web ブラウザ側（クライアント側）ではなく、Forge アプリを配信することになる Web サーバーとして利用、実装するのが一般的です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3762127200c-pi" style="display: inline;"><img alt="Forge_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3762127200c image-full img-responsive" src="/assets/image_7719.jpg" title="Forge_app" /></a></p>
<p style="padding-left: 30px;">よくある質問に、「Forge OAuth API（Authentication API）をクライアント側の JavaScript で呼び出すと <strong><a href="https://developer.mozilla.org/ja/docs/Web/HTTP/HTTP_access_control" rel="noopener noreferrer" target="_blank">CORS</a></strong> エラーとなり、Access Token を取得することは出来ない」、というものがあります。これはセキュリティ上の制限、ある意味、仕様（Access-Control-Allow-Origin: * などを返さない）なのですが、デスクトップ製品のアドイン/プラグイン開発の方には「可能なはずなのに、なぜ、Web ブラウザ側（クライアント側）で OAuth API（Authentication API）の呼び出しが出来ないのか」、と不思議に感じる方もいらっしゃるようです。</p>
<p style="padding-left: 30px;">このシナリオでは、最も理解し易い懸念点があります。Forge アプリ開発者の視点では、Access Token の取得には Client ID と Client Secret が必要になるのは周知のとおりです。両者は、クラウド サービスへのサインイン時に利用する、他人に知られないようにすべき、ユーザ名とパスワードに相当するものと捉えることが出来ます。そして、この Access Token がないと Forge クラウド上のリソースへのアクセスが却下されてしまう訳です。もし、Web ブラウザ側（クライアント側）で両者を記述して Access Token を取得するようなコードを書いてしまうと、秘匿すべき&#0160; Client ID と Client Secret をわざわざ第 3 者に教えてしまうのと変わらない結果になってしまいます。</p>
<p style="padding-left: 30px;">なぜ、そんなことが言えるのか？</p>
<p style="padding-left: 30px;">Web ブラウザで任意のページを表示して F12 キーを押してみてください。ブラウザによりますが、画面下側や右側に複数のタブを持った画面が表示されるはずです。例えば、Google Chrome でオートデスクの HP&#0160;<strong><a href="https://www.autodesk.co.jp/" rel="noopener noreferrer" target="_blank">https://www.autodesk.co.jp/</a></strong> を表示して、F12 キーを押すと、次のように表示されます。ここで表示されているのが <strong>デベロッパー ツール</strong>、あるいは、<strong>開発ツール</strong> と呼ばれる Web ブラウザ組み込みの画面です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39c40bc200d-pi" style="display: inline;"><img alt="Developer_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39c40bc200d image-full img-responsive" src="/assets/image_300170.jpg" title="Developer_tool" /></a></p>
<p style="padding-left: 30px;">Web ブラウザによって名称が異なりますが、デベロッパーツールに複数あるタブの中から、[Sources] や [デバッガー] をアクティブにすると、表示中のページを定義する HTML コードや、HTML が参照、実行する JavaScript コードを直接見ることが出来るはずです。</p>
<p style="padding-left: 30px;">つまり、Web ブラウザ側（クライアント側）で実行されることになる JavaScript コードに&#0160; Client ID と Client Secret を記述していまうと、誰でも、その値を把握することが出来てしまうことになります。この 2 つさえ分かってしまえば、然るべき手順で Access Token を取得出来てしまうため、偽装アプリなどからストレージの内容が余も取られてしまい、大切なデータが漏洩してしまう結果にもつながります。このように、Web ブラウザ側（クライアント側）の HTML や JavaScript はまる見えなので、実装内容が推測されてしまうようなコメント行も含め、不用意な記述は避けるべきなのです。</p>
<p><strong>便利な開発者用のデバッグ ツール</strong></p>
<p style="padding-left: 30px;">前置きが長くなってしまいましたが、このデベロッパーツール/開発者ツールは、その名の通り、Web ブラウザ側（クライアント側）の HTML や JavaScript をデバッグする開発者用のツールで、さまざまな機能を持っています。おおまかには、次のようなものがあります。</p>
<p style="padding-left: 30px;"><strong>コンソールによるメッセージ表示と確認</strong></p>
<p style="padding-left: 30px;">HTML ページの表示時（ロード時）や JavaScript の実行時に問題が発生した際、原因追及の最初の手がかりとなる のエラーや警告を表示します。JavaScript 実装で colsole.log() 関数を使って出力したメッセージを表示させることも出来ます。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/another-way-to-specify-forge-viewer-version.html" rel="noopener noreferrer" target="_blank">Forge Viewer バージョン指定のもう1つの方法</a></strong> でご紹介した Forge Viewer のバージョンについては、バージョン番号が格納されているグローバル変数 <strong>LMV_VIEWER_VERSION</strong> をこの方法で表示させれば確認することが出来ます。</p>
<p style="padding-left: 30px;"><strong>Web ページ リソースやネットワーク情報の表示</strong></p>
<p style="padding-left: 30px;">Web ページのロード時に参照、使用している画像やフォント、JavaScript ライブラリ（CDN）、CSS などのリソースのツリー構造やロード速度や、HTTP プロトコルで送受信された RESTful API の内容など、ネットワークの使用状況を表示します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc0e28200b-pi" style="display: inline;"><img alt="Network" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc0e28200b image-full img-responsive" src="/assets/image_105874.jpg" title="Network" /></a></p>
<p style="padding-left: 30px;"><strong>HTML、JapaScript ビューアとデバッグ</strong></p>
<p style="padding-left: 30px;">表示中の Web ページについて、HTML コードと DOM 情報、付随する JavaScript コードを表示することが出来ます、JavaScript では、ブレークポイントを置いてステップ実行したり、変数の内容を参照するシンボリック デバッグ機能を利用することも可能です。PC やスマートフォンなどのデバイスに表示を切り替えることも可能です。</p>
<p style="padding-left: 30px;">&#0160; <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc0e4c200b-pi" style="display: inline;"><img alt="Device" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc0e4c200b image-full img-responsive" src="/assets/image_176153.jpg" title="Device" /></a></p>
<p style="padding-left: 30px;"><strong>セキュリティのチェック</strong></p>
<p style="padding-left: 30px;">Web サイトにアクセスした際の接続がセキュアかを評価します。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/07/postpone-dropping-tls10-and-11-on-forge.html" rel="noopener noreferrer" target="_blank">TLS 1.0/1.1 の Forge サポート中止の延期処置について</a></strong> でご案内した TLS バージョンなども表示されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc0eb1200b-pi" style="display: inline;"><img alt="Secure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc0eb1200b image-full img-responsive" src="/assets/image_708015.jpg" title="Secure" /></a></p>
<p style="padding-left: 30px;">Forge Viewer を使った HTML ページが正しく表示されない場合など、このツールを利用することで、どこで処理が止まってしまうのか、どのようなエラーが発生しているのか、などを把握するのに役立つはずです。</p>
<p style="padding-left: 30px;">Web ブラウザによって少しづつ異なるデベロッパーツール/開発者ツールですが、詳細は各ブラウザ毎にオンラインヘルプやブログ記事で数多く紹介されていますので、お使いのブラウザに合わせて内容を確認してみてください。</p>
<p style="padding-left: 30px;"><strong>Google Chrome&#0160;</strong></p>
<p style="padding-left: 30px;"><a href="https://developers.google.com/web/tools/chrome-devtools/console/?utm_source=dcc&amp;utm_medium=redirect&amp;utm_campaign=2016q3" rel="noopener noreferrer" target="_blank">https://developers.google.com/web/tools/chrome-devtools/console/?utm_source=dcc&amp;utm_medium=redirect&amp;utm_campaign=2016q3</a></p>
<p style="padding-left: 30px;"><strong>Mozilla Firefox</strong></p>
<p style="padding-left: 30px;"><a href="https://developer.mozilla.org/ja/docs/Tools/Web_Console" rel="noopener noreferrer" target="_blank">https://developer.mozilla.org/ja/docs/Tools/Web_Console</a></p>
<p style="padding-left: 30px;"><strong>Microsoft Internet Explorer</strong></p>
<p style="padding-left: 30px;"><a href="https://html5experts.jp/osamum_ms/1928/" rel="noopener noreferrer" target="_blank">https://html5experts.jp/osamum_ms/1928/</a></p>
<p style="padding-left: 30px;"><strong>Microsoft Edge</strong></p>
<p style="padding-left: 30px;"><a href="https://docs.microsoft.com/ja-jp/microsoft-edge/devtools-guide?source=f12help" rel="noopener noreferrer" target="_blank">https://docs.microsoft.com/ja-jp/microsoft-edge/devtools-guide?source=f12help</a></p>
<p style="padding-left: 30px;"><strong>Apple Safari</strong></p>
<p style="padding-left: 30px;"><a href="https://support.apple.com/ja-jp/guide/safari/sfri20948/mac" rel="noopener noreferrer" target="_blank">https://support.apple.com/ja-jp/guide/safari/sfri20948/mac</a></p>
<hr />
<p>さまざまな Web ブラウザですが、視点を変えて<strong><a href="https://ja.wikipedia.org/wiki/HTML%E3%83%AC%E3%83%B3%E3%83%80%E3%83%AA%E3%83%B3%E3%82%B0%E3%82%A8%E3%83%B3%E3%82%B8%E3%83%B3" rel="noopener noreferrer" target="_blank"> HTML レンダリングエンジン</a></strong>と見ることが出来ます。デベロッパーツール/開発者ツールは実装もまちまちですが、HTML レンダリングエンジンの由来からか、Web 開発で必要とされる機能が集約した結果からか、どれも似たような機能や操作感になっているようです。</p>
<p>以前、AutoCAD 内で Web ページを表示 でも触れましたが、AutoCAD には Webkit が組み込まれているので、ここでご紹介した内容でデバッグすることも可能です。</p>
<p>もし、Forge を使った開発で問題があった場合など、デベロッパーツール/開発者ツールでの検証、デバッグをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
