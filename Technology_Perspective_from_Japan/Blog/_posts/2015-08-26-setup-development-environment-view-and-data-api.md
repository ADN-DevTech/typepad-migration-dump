---
layout: "post"
title: "View and Data API の開発環境"
date: "2015-08-26 21:18:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/setup-development-environment-view-and-data-api.html "
typepad_basename: "setup-development-environment-view-and-data-api"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span>&#0160;</p>
<p>View and Data API を含むオートデスクの Web サービス API は、現在、Autodesk Developer Portal サイト <a href="https://developer.autodesk.com" target="_blank">https://developer.autodesk.com</a> で公開が始まっていて、今後も新しい API の追加公開が予定されています。</p>
<p>最も早い段階に公開された&#0160;View and Data API の場合、Autodesk Developer Portal &#0160;には API リファレンスや GitHub 上のサンプルだけでなく、実装手順や機能を把握するための Live Demo も記載されています。実装手順を理解する<a href="https://github.com/Developer-Autodesk/LmvQuickStart" target="_blank"><strong>&#0160;クイック スタート サンプル</strong></a>&#0160; については、過去のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2015/01/view-data-api-quick-start-sample.html" target="_blank"><strong>View &amp; Data API クイック スタート サンプル</strong></a> で紹介していますが、この時、アクセス トークンの発行で利用していた&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/11/api-console-usage.html" target="_blank">API コンソール</a></strong>&#0160;の機能が&#0160;Autodesk Developer Portal から廃止されてしまっています。</p>
<p>そこで、今後の&#0160;View and Data API 開発も前提に、GitHub 上で公開しているワークフロー 系サンプルの利用も考慮に入れて、今回は開発環境の用意について、オートデスクがお勧めするツールや導入手順を紹介しておきたいと思います。なお、アクセス トークンについては、テスト用の取得ということで<a href="https://ja.wikipedia.org/wiki/CURL" target="_blank"><strong> Curl</strong></a> を利用する前提にしておきます。</p>
<p>今回は、次の開発ツールを導入することをかんがて見ます。もちろん、Web 開発はオープンソースを利用するのが一般的なので、これらツールや環境でないと &#0160;View and Data API を利用できないわけではありません。ご注意ください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c662b0970d-pi" style="display: inline;"><img alt="Development_tools" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c662b0970d img-responsive" src="/assets/image_76667.jpg" title="Development_tools" /></a></p>
<p>各ツール、アプリケーション、サーバー環境は、それぞれ、次の項目で説明する手順で入手してインストールを完了させてください。なお、このガイドでは、管理者権限でログインした 64 ビット版 Windows を前提に説明しています。</p>
<ul>
<li>&#0160;ここで記載しているバージョンは、2016年3月10日現在で最新のものです（2015年8月20日記載を2016年3月10日改訂）。今後のバージョンアップでダウンロード先の URL や、ダウンロードしたファイル名が変わる可能性があります。ご注意ください。</li>
</ul>
<p style="text-align: left;"><strong>Google Chrome</strong></p>
<p style="text-align: left;">作成した Web ページを表示してテストします。この Web ブラウザは、View and Data API で 3D モデルを表示するための必要な、HTML5、CSS3、WebGL をサポートしています。ここでは、現在、一般的に利用されている 32 ビット版を使用することとします。</p>
<ol>
<li><a href="https://www.google.co.jp/chrome/browser/desktop/" target="_blank">https://www.google.co.jp/chrome/browser/desktop/</a> からWindows 版（10/8.1/8/7/Vista/XP 32-bit）と表示されている Google Chrome のセットアップ プログラムである ChromeSetup.exe を任意の場所にダウンロードします。<span style="text-align: center;">&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d14eddd0970c-pi" style="float: right;"><img alt="Chrome" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d14eddd0970c image-full img-responsive" src="/assets/image_130183.jpg" style="margin: 0px 0px 5px 5px;" title="Chrome" /></a><br /></span></li>
<li><span style="text-align: center;">ダウンロードした ChromeSetup.exeをダブルクリックして、セットアップ プログラムを開始し、インストールをおこなってください。</span></li>
<li>インストールが完了すると、C:\Program Files (x86)\Google\Chrome フォルダに Google Chrome がインストールされているはずです。</li>
</ol>
<p><strong>Adobe Brackets</strong></p>
<p style="text-align: left;">Adobe Systems 社が公開しているオープン ソースのソースコード エディタです。今回は、HTML ファイルや JavaScript コードを編集するために使用します。</p>
<ol>
<li><a href="http://brackets.io/" target="_blank">http://brackets.io/</a> から <a href="https://github.com/adobe/brackets/releases/download/release-1.4/Brackets.Release.1.4.msi" target="_blank">Download Brackets without Extract</a> を選択してインストーラ <strong>Brackets.Release.1.6.msi</strong> をダウンロードします。<span style="text-align: center;">&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c659d8970d-pi" style="display: inline;"><img alt="Brackets" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c659d8970d image-full img-responsive" src="/assets/image_939020.jpg" title="Brackets" /><br /></a><br /></span></li>
<li><span style="text-align: center;">ダウンロードした <strong>Brackets.Release.1.6.msi</strong> を実行して、インストールを開始します。<br /><br /></span></li>
<li>Brackets インストーラが起動後に [Brackets Destination Folder] 画面が表示されます。念のため、インストール先のフォルダが既定値である C:\Program Files (x86)\Brackets\ になっていることを確認します。その他、オプションも既定値のまま、[Next] ボタンをクリックします。<span style="text-align: center;">&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac0ddb970c-pi" style="display: inline;"><img alt="Brackets_install1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac0ddb970c img-responsive" src="/assets/image_458366.jpg" style="width: 450px;" title="Brackets_install1" /><br /></a><br /></span></li>
<li>[Install] ボタンをクリックしてインストールを開始します。<span style="text-align: center;">&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c659e6970d-pi" style="display: inline;"><img alt="Brackets_install2" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c659e6970d img-responsive" src="/assets/image_347794.jpg" style="width: 450px;" title="Brackets_install2" /><br /></a><br /></span></li>
<li>インストールが完了したら、[Finish] ボタンをクリックしてインストーラを完了します。Windows の [スタート] ボタンから Brackets アイコンを見つけて起動出来ることを確認してください。</li>
</ol>
<p><strong>Node.js</strong></p>
<p style="text-align: left;">Node.js は、JavaScript をサーバー上で実行するための環境です。JavaScript は、もともとクライアント コンピュータ上の Web ブラウザで実行することを目的の作成されたものですが、サーバー側の処理を実行するために用意されたものです。実行には、Google V8 JavaScript Engine が使用されています。Node.js は、3<sup>rd</sup> party が用意したパッケージをインストールして、機能を拡張することが出来ます。パッケージのインストール等の管理には、パッケージ管理ツール Node Package Manager (npm) を利用します。ここでは、express、request、serve-favicon の 3 つのパッケージ モジュールを利用するものと仮定してセットアップをおこないます。開発時には、Webサイトをクライアント コンピュータ上で実現する目的で利用出来ます。</p>
<ol>
<li><a href="https://nodejs.org/" target="_blank">https://nodejs.org/</a> から画面中央の [V4.4.0 LTS] をクリックして、64ビット版 Windows 用インストーラ <strong>node-v4.4.0-x64.msi&#0160;</strong>をダウンロードします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac0dea970c-pi" style="display: inline;"><img alt="Nodejs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac0dea970c image-full img-responsive" src="/assets/image_295195.jpg" title="Nodejs" /><br /><br /></a></li>
<li>ダウンロードした <strong>node-v4.4.0-x64.msi</strong>&#0160;を実行して、インストールを開始します。[Welcome to the Node.js Setup Wizard] 画面が表示されたら、画面右下の [Next] ボタンをクリックします。<br /><br /></li>
<li>[End-User License Agreement] 画面のライセンス規約を一読の上、「I Accept the terms in the License Agreement」にチェックを入れて、[Next] ボタンをクリックします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac0e08970c-pi" style="display: inline;"><img alt="Nodejs_install1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac0e08970c img-responsive" src="/assets/image_435829.jpg" style="width: 450px;" title="Nodejs_install1" /><br /><br /></a></li>
<li>[Destination Folder] 画面でインストール先フォルダを既定値の C:\Program Files\nodejs\ まま、[Next] ボタンをクリックします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c821cdb9970b-pi" style="display: inline;"><img alt="Nodejs_install2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c821cdb9970b img-responsive" src="/assets/image_624395.jpg" style="width: 450px;" title="Nodejs_install2" /><br /><br /></a></li>
<li>[Custom Setup] 画面では、すべてのオプションをインストールする既定値まま、[Next] ボタンをクリックします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c65a27970d-pi" style="display: inline;"><img alt="Nodejs_install3" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c65a27970d img-responsive" src="/assets/image_795763.jpg" style="width: 450px;" title="Nodejs_install3" /><br /><br /></a></li>
<li>[Install] ボタンをクリックしてインストールを開始します。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac0e1a970c-pi" style="display: inline;"><img alt="Nodejs_install4" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac0e1a970c img-responsive" src="/assets/image_929061.jpg" style="width: 450px;" title="Nodejs_install4" /><br /><br /></a></li>
<li>インストールが完了したら、[Finish] ボタンをクリックしてインストーラを完了します。</li>
</ol>
<p><strong>Curl</strong></p>
<p style="text-align: left;">Curl は、さまざまなプロトコルを用いてデータを転送するライブラリとコマンドラインツールを提供するものです。一般に cURL と表記されることが多いようです。スタートアップ時には、Consumer Key と Consumer Secret からアクセス トークンを取得する目的で利用します。なお、アクセス トークンの取得は、通常、サーバー サイド実装でプログラムを介しておこなわれるべきものです。</p>
<ol>
<li><a href="http://curl.haxx.se/download.html" target="_blank" title="http://curl.haxx.se/download.html ">http://curl.haxx.se/download.html </a>のページ下部から Win64 x86_64 CAB と書かれたリンクを見つけてください。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac1c06970c-pi" style="display: inline;"><img alt="Curl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac1c06970c image-full img-responsive" src="/assets/image_121222.jpg" title="Curl" /><br /><br /></a></li>
<li>バージョン番号である 7.47.1 をクリックして、<strong>curl-7.47.1.cab</strong>&#0160;を任意の場所にダウンロードしてください。<br /><br /></li>
<li>Windows エクスプローラを使って、C ドライブのルート フォルダに CURL フォルダを作成します。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac0e2d970c-pi" style="display: inline;"><img alt="Curl_setup1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac0e2d970c image-full img-responsive" src="/assets/image_872426.jpg" title="Curl_setup1" /><br /><br /></a></li>
<li>ダウンロードした <strong>curl-7.47.1.cab</strong> をブルクリックして、Windows エクスプローラで表示させます。この時、パス名を表示して、<strong>AMD64</strong> フォルダにある <strong>CURL.EXE</strong>、<strong>LIBCURL.DLL</strong>、<strong>LIBCURL.EXE</strong>、<strong>LIBCURL.LIB</strong> を選択して、C:\CURL フォルダにコピーしてください。<span style="text-align: center;">&#0160;&#0160;<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c821cddb970b-pi" style="display: inline;"><img alt="Curl_setup2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c821cddb970b image-full img-responsive" src="/assets/image_986149.jpg" title="Curl_setup2" /></a><br /></span></li>
</ol>
<p><strong>GitHub Desktop</strong></p>
<p style="text-align: left;">GitHub Desktop の利用は必須ではありませんが、GitHub 上のサンプル プログラムを入手したり、今後の開発作業でソース コードを共有する計画がある場合には有用です。今回は、前者の目的で利用します。GitHub Desktop は、プラットフォーム別に、それぞれ次のリンクから入手することが出来ます。</p>
<ul>
<li><a href="https://windows.github.com/">Windows</a></li>
<li><a href="https://mac.github.com/">Mac OSX</a></li>
<li><a href="http://git-scm.com/download/linux">Linux</a></li>
</ul>
<p style="text-align: left;">ここでは、Windows プラットフォーム用の GitHub Desktop ユーティリティ、別名、GitHub for Windows をインストールしておきます。</p>
<ol>
<li><a href="https://windows.github.com/" target="_blank">https://windows.github.com/</a> ページを表示後に [Download GitHub Desktop] をクリックして、<strong>GitHubSetup.exe</strong> をダウンロードします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c821d5c2970b-pi" style="display: inline;"><img alt="Github_desktop" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c821d5c2970b image-full img-responsive" src="/assets/image_222017.jpg" title="Github_desktop" /></a><br /><br /></li>
<li>ダウンロードした&#0160;<strong>GitHubSetup.exe</strong> を実行して、GitHub Desktop のインストールを開始します。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac1a68970c-pi" style="display: inline;"><img alt="Github_desktop_install1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac1a68970c img-responsive" src="/assets/image_947708.jpg" style="width: 400px;" title="Github_desktop_install1" /><br /><br /></a></li>
<li>インストールが自動的に終了すると、GitHub のセットアップ画面が表示されます。GitHub アカウントの情報を入力して、セットアップを完了してください。もし、GitHub アカウントをお持ちえないには、4.を参照してください。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac1a9b970c-pi" style="display: inline;"><img alt="Github_desktop_install2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac1a9b970c image-full img-responsive" src="/assets/image_675189.jpg" title="Github_desktop_install2" /><br /><br /></a></li>
<li>無償版の GitHub アカウントを登録します。<a href="https://github.com/" target="_blank">https://github.com/</a> ページを表示させて、User Name、Email アドレス、パスワードを入力後に [Sign up for GitHub] をクリックします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c66526970d-pi" style="display: inline;"><img alt="Github_account_setup1" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c66526970d img-responsive" src="/assets/image_247000.jpg" style="width: 700px;" title="Github_account_setup1" /><br /><br /></a></li>
<li>次の画面が表示されたら、Free プランを選択して [Finish sign up] をクリックします。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c821d758970b-pi" style="display: inline;"><img alt="Github_account_setup2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c821d758970b image-full img-responsive" src="/assets/image_521141.jpg" title="Github_account_setup2" /><br /><br /></a></li>
<li>登録したメール アドレスに確認用のメールが届きます。[Verify email address] ボタンをクリックしてアカウント作成を完了します。後は、3. に戻って、アカウント情報を入力してください。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c82250ab970b-pi" style="display: inline;"><img alt="Verify_email_address" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c82250ab970b image-full img-responsive" src="/assets/image_293560.jpg" title="Verify_email_address" /></a><br /><br /></li>
</ol>
<p style="text-align: left;">以上で環境セットアップは終了です。</p>
<p style="text-align: left;">次回以降、View and Data API を利用するために必要となるキーの取得手順と、GitHub から利用できる Live Demo を使った View and Data API の利用手順の確認について、2 回に分けてご紹介します。</p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p>&#0160;</p>
