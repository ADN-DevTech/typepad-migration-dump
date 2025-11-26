---
layout: "post"
title: "APS の開発環境"
date: "2017-01-11 23:42:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html "
typepad_basename: "development-environment-for-forge"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/au-2022autodesk-platform-services-forge-deepening.html" rel="noopener" style="background-color: #ffff00;" target="_blank">Autodesk Forge は 2022年9月開催の Autodesk University で<strong>&#0160;Autodesk Platform Services</strong>&#0160;へ改名されました。</a></span></p>
<p>Autodesk Platform Services（APS）は、オートデスクのクラウド サービスが使用している様々な要素技術を、個々に Web サービス API として公開しているクラウド プラットフォームです。リファレンスを含む各 API の詳細は、APS ポータル（<a href="https://aps.autodesk.com" rel="noopener noreferrer" target="_blank">https://aps.autodesk.com</a>）で公開されています。今後も、継続して既存 API の機能を更新していくと同時に、新しい API の公開も予定されています。</p>
<p>ここでは、RESTful API の動作チェックや理解、GitHub 上で公開しているサンプルの利用も考慮に入れて、APS API の開発環境について、64 ビッ版 Windows プラットフォームを前提にオートデスクがお勧めするツールや環境の導入手順を紹介しておきたいと思います。今回は、次にあげる開発ツールの導入手順を見ていきますが、この環境でないと APS を使った開発が出来ないわけではありませんのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807ab535200d-pi" style="display: inline;"><img alt="Development_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807ab535200d image-full img-responsive" src="/assets/image_769262.jpg" title="Development_environment" /></a></p>
<p>各ツール、アプリケーション、サーバー環境は、それぞれ、次の項目で説明する手順で入手してインストールを完了させてください。なお、このガイドでは、管理者権限でログインした 64 ビット版 Windows を前提に説明しています。</p>
<ul>
<li>ここで記載しているバージョンは、<span style="background-color: #ffff00;">2025</span><span style="background-color: #ffff00;">年4月4日現在 </span>のものです。今後、各ツールや環境のバージョンアップでダウンロード先の URL や、ダウンロードしたファイル名や画面等が変わる可能性があります。ご注意ください。</li>
</ul>
<hr />
<p><strong>Google Chrome</strong></p>
<p>作成した Web ページを表示してテストします。この Web ブラウザは、APS Viewer で 3D モデルを表示するための必要な、HTML5、CSS3、WebGL をサポートしています。</p>
<ol>
<li><a href="https://www.google.co.jp/chrome/" rel="noopener noreferrer" target="_blank"><strong>https://www.google.co.jp/chrome</strong>/</a> から [Chrome をダウンロード] ボタンをクリックして ChromeSetup.exe を任意の場所にダウンロードします。&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdc9d2200d-pi" style="display: inline;"><img alt="Chrome_download_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fdc9d2200d img-responsive" src="/assets/image_355645.jpg" title="Chrome_download_page" /></a></li>
<li>ダウンロードした ChromeSetup.exe をダブルクリックすると、インストールに必要なモジュールのダウンロードが始まり、インストールが自動的に実行されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1534905200b-pi" style="display: inline;"><img alt="Chrome_download" class="asset  asset-image at-xid-6a0167607c2431970b0282e1534905200b img-responsive" src="/assets/image_57660.jpg" style="width: 400px;" title="Chrome_download" /></a></li>
<li>インストールが完了すると、C:\Program Files\Google\Chrome\Application フォルダに Google Chrome がインストールされているはずです。</li>
</ol>
<hr />
<p><strong>VS Code</strong></p>
<p>VS Code（Visual Studio Code）は、Microsoft 社が<a href="https://github.com/microsoft/vscode" rel="noopener" target="_blank">オープン ソース</a>として無償で公開している JavaScript や Python など、主にクラウド・Web 開発をおこなうための統合開発環境です。Microsoft Azure との親和性が高く、機能を拡張する Extension を使って編集対象の開発言語を増やしたり、ツールを追加したりすることが出来ます。</p>
<ol>
<li><a href="https://code.visualstudio.com/Download" rel="noopener" target="_blank"><strong>https://code.visualstudio.com/Download</strong></a> から [Windows]&#0160; ボタンをクリックして、インストーラ ファイル <strong>VSCodeUserSetup-x64-1.98.2.exe</strong>&#0160;をダウンロードします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6cd62200b-pi" style="display: inline;"><img alt="Vscode_download_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e6cd62200b image-full img-responsive" src="/assets/image_858049.jpg" title="Vscode_download_page" /></a></li>
<li>ダウンロードした <strong>VSCodeUserSetup-x64-1.98.2.exe</strong><strong>&#0160;</strong>を実行します。</li>
<li>特に APS 開発に必須のオプションはありませんので、画面の指示に従ってインストールを完了してください。</li>
</ol>
<ul>
<li>オートデスクは、APS 開発の手助けとなる VS Code 用の <em><strong><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Platform Services VSCode Extension</a>&#0160;</strong>エクステンションを</em>用意しています。<em><strong><a href="https://marketplace.visualstudio.com/items?itemName=petrbroz.vscode-forge-tools" rel="noopener" target="_blank">Autodesk Platform Services VSCode Extension</a>&#0160;</strong>エクステンションのインストールに関する情報は、</em><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html" rel="noopener" target="_blank"><strong>Visual Studio Code での Forge 開発</strong></a> <em>のブログ記事をご確認ください。</em></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d01dde200c-pi" style="display: inline;"><img alt="Vscode_aps_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d01dde200c img-responsive" src="/assets/image_590952.jpg" title="Vscode_aps_extension" /></a></p>
<hr />
<p><strong>Postman</strong></p>
<p>Postman は、APS API でも多用する RESTful API をテストするためのツールです。Postman を利用することで、Authentication API や Model Derivative API、Data Management API で使用するエンドポイントをプログラムすることなくテスト、評価することが出来ます。</p>
<ol>
<li><a href="https://www.getpostman.com/apps" rel="noopener noreferrer" target="_blank"><strong>https://www.getpostman.com/apps</strong></a> から [Windows 64-bit]&#0160; ボタンをクリックしてインストーラ ファイル <strong>Postman-win64-Setup.exe</strong> をダウンロードします<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdca07200d-pi" style="display: inline;"><img alt="Postman_download_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fdca07200d image-full img-responsive" src="/assets/image_858036.jpg" title="Postman_download_page" /></a></li>
<li>ダウンロードした <strong>Postman-win64-Setup.exe</strong>&#0160;を実行します。</li>
<li>Postman が起動したらセットアップ完了です。初回起動時には、Postman アカウントの作成を促す画面が表示されますので、無償アカウントをお持ちでない場合には、[Sign up for free] ボタンをクリック後、画面の指示に従ってサインアップをしてください。サインアップは無償です。アカウントをお持ちの場合にはサインインしてくだい。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6ce0a200b-pi" style="display: inline;"><img alt="Signup_postman" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e6ce0a200b img-responsive" src="/assets/image_615724.jpg" title="Signup_postman" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdca20200d-pi" style="display: inline;"></a></li>
<li>サインアップが完了すると、Postman の利用が可能になります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdca2b200d-pi" style="display: inline;"><img alt="Postman" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860fdca2b200d image-full img-responsive" src="/assets/image_45203.jpg" title="Postman" /></a></li>
</ol>
<hr />
<p><strong>Node.js</strong></p>
<p>Node.js は、JavaScript を Web サーバー上で実行するための環境です。JavaScript は、もともとクライアント コンピュータ上の Web ブラウザで実行することを目的の作成されたものですが、サーバー側の処理を実行するために用意されたものです。実行には、Google V8 JavaScript Engine が使用されています。Node.js は、3<sup>rd</sup>party が用意したミドルウェアと呼ばれるパッケージをインストールして、その機能を拡張することが出来ます。パッケージのインストール等の管理には、パッケージ管理ツール Node Package Manager (npm) を利用します。開発時には、Webサイトをクライアント コンピュータ上で実現する目的で利用出来ます。</p>
<ol>
<li><a href="https://nodejs.org/" rel="noopener noreferrer" target="_blank"><strong>https://nodejs.org/</strong></a> から [Node.jp（LTS）をダウンロードする] をクリックして、64ビット版 Windows 用のインストーラ <strong>node-v22.14.0-x64.msi&#0160;</strong>をダウンロードします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d01e36200c-pi" style="display: inline;"><img alt="Nodejs_download_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d01e36200c img-responsive" src="/assets/image_428873.jpg" title="Nodejs_download_page" /></a></li>
<li>ダウンロードした<strong>node-v22.14.0-x64.msi</strong>&#0160;をダブルクリックで実行して、インストールを開始します。[Welcome to the Node.js Setup Wizard] 画面が表示されたら、画面右下の [Next] ボタンをクリックします。</li>
<li>[End-User License Agreement] 画面のライセンス規約を一読の上、「I Accept the terms in the License Agreement」にチェックを入れて、[Next] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6cdf1200b-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdca55200d-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d01e55200c-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d01e57200c-pi" style="display: inline;"><img alt="Nodejs_install1" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d01e57200c img-responsive" src="/assets/image_530000.jpg" title="Nodejs_install1" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6cde4200b-pi" style="display: inline;"></a></li>
<li>[Destination Folder] 画面でインストール先フォルダを既定値の C:\Program Files\nodejs\ まま、[Next] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fdca5c200d-pi" style="display: inline;"><img alt="Nodejs_install2" class="asset  asset-image at-xid-6a0167607c2431970b02e860fdca5c200d img-responsive" src="/assets/image_394385.jpg" title="Nodejs_install2" /></a></li>
<li>[Custom Setup] 画面では、すべてのオプションをインストールする既定値まま、[Next] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6ce1b200b-pi" style="display: inline;"><img alt="Nodejs_install3" class="asset  asset-image at-xid-6a0167607c2431970b02e860e6ce1b200b img-responsive" src="/assets/image_847071.jpg" title="Nodejs_install3" /></a></li>
<li>[Tools for Native Modules] 画面の内容は、ここでは特に必須ではありません。[Next] ボタンをクリックします。</li>
<li>[Ready to install Node.js] 画面で [Install] ボタンをクリックしてインストールを開始します。</li>
<li>インストールが完了したら、[Finish] ボタンをクリックしてインストーラを完了します。</li>
</ol>
<hr />
<p><strong>git for Windows</strong></p>
<p>GitHub 上のサンプル プログラムを入手して利用する際にコマンド プロンプト(bash) で <strong><a href="https://ja.wikipedia.org/wiki/Git" rel="noopener noreferrer" target="_blank">git</a></strong> コマンドを利用して、クライアント コンピュータに GitHub リポジトリの内容をクローンして利用します。は有用です。今回は、git for Windows は<strong>&#0160;<a href="https://gitforwindows.org/" rel="noopener noreferrer" target="_blank">https://gitforwindows.org/</a></strong>&#0160;を使用することとします。</p>
<ol>
<li><a href="https://gitforwindows.org/" rel="noopener noreferrer" target="_blank"><strong>https://gitforwindows.org/</strong></a><a href="https://git-for-windows.github.io/" rel="noopener noreferrer" target="_blank"> </a><a href="https://windows.github.com/" rel="noopener noreferrer" target="_blank"> </a> ページを表示後に [Download] をクリックして、<strong>Git-2.49.0-64-bit.exe</strong>&#0160;をダウンロードします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e6cf36200b-pi" style="display: inline;"><img alt="Gitforwindows_download_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e6cf36200b img-responsive" src="/assets/image_805372.jpg" title="Gitforwindows_download_page" /></a></li>
<li>ダウンロードした <strong>Git-2.49.0-64-bit.exe&#0160;</strong> をダブルクリックで実行して、git for Windows のインストールを開始します。[Information] 画面が表示されたら [Install] ボタンをクリックします。</li>
<li>インストールが終了したら、コマンド プロンプトを起動して git コマンドが有効なことを確認してください。<strong>git --version</strong> と入力して <strong>git version 2.49.0.windows.1</strong>&#0160;と表示されれば正常です。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d01f64200c-pi" style="display: inline;"><img alt="Git_version_command_prompt" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d01f64200c image-full img-responsive" src="/assets/image_142142.jpg" title="Git_version_command_prompt" /></a></li>
</ol>
<hr />
<p>以上で環境セットアップは終了です。</p>
<p>By Toshiaki Isezaki</p>
