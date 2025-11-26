---
layout: "post"
title: "Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample のセットアップ"
date: "2019-03-15 02:09:04"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample.html "
typepad_basename: "setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample"
typepad_status: "Publish"
---

<p>これまで、Postman Sample で Design Automation API の利用手順をご紹介いたしました。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/setup-design-automation-api-for-revit-postman-sample.html">Design Automation API for Revit - Postman Sample のセットアップ</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-1.html">Design Automation API for Revit - Postman Sample で動作確認 1</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-2.html">Design Automation API for Revit - Postman Sample で動作確認 2</a></li>
</ul>
<p>今回から、Forge Platform API を包括的に習得いただけるチュートリアル <strong><a href="https://learnforge.autodesk.io/#/ja-JP/">Learn Autodesk Forge（日本語）</a></strong>の Design Automation API のセクション、<a href="https://learnforge.autodesk.io/#/ja-JP/tutorials/modifymodels"><strong>モデルを修正する&nbsp;セクション</strong></a>について、ご紹介いたします。</p>
<p><a href="http://learnforge.autodesk.io/#/tutorials/modifymodels"></a><strong><a href="https://learnforge.autodesk.io/#/ja-JP/tutorials/modifymodels">モデルを修正する&nbsp;セクション</a></strong>では、.NET Core を利用して Forge アプリを開発する手順と、 AutoCAD、Inventor、Revit、3ds Max のアドインの対応方法がそれぞれ解説されております。</p>
<p>このセクションでは、Revit プロジェクトの窓ファミリタイプの幅と高さを変更して、OSS の Bucket に保存する&nbsp;<a href="https://github.com/Autodesk-Forge/learn.forge.designautomation"></a><a href="https://github.com/Autodesk-Forge/learn.forge.designautomation"><strong>learn.forge.designautomation サンプル</strong></a>が GitHub で公開されております。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493f70e200b-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493f70e200b image-full img-responsive" src="/assets/image_411704.jpg" title="DesignAutomationRevitLearnForgeTutorial0" /></a></p>
<p><br />今回は、このサンプルをローカル環境でテストするための環境構築の手順をご案内いたします。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html">以前のブログ記事（Forge の開発環境）</a>では、Node.js をベースとした開発環境のセットアップ手順についてご案内しておりますが、今回のサンプルは <strong>.NET Core</strong> をベースとするため、Visual Studio を利用します。<br />一部、重複している点もございますので、既にインストールされているツールがある場合は、スキップしてください。</p>
<p>&nbsp;</p>
<p><strong>1. Visual Studio 2017 以降のバージョン（最新は 2022）のインストール&nbsp;</strong></p>
<p>下記の URL から<strong> Visual Studio 2017 以降のバージョンの Community</strong>&nbsp;<strong>エディション（無償）</strong>または、<strong>Visual Studio 2017 以降のバージョンのProfessional&nbsp;エディション（有償）</strong> のインストーラをダウンロードしてください。</p>
<ul>
<li><a href="https://visualstudio.microsoft.com/ja/downloads/">https://visualstudio.microsoft.com/ja/downloads/</a></li>
</ul>
<p><span style="text-decoration: underline;"><span style="color: #ff0000; text-decoration: underline;">※サンプルを動かすためには、<strong>Visual Studio 2017 バージョン 15.7 以降</strong>が必要になります。Visual Studio 2017 をお持ちの方は、予めバージョンをご確認ください。</span></span></p>
<p>お使いの Visual Studio バージョンを確認するには、次の手順を実行します。</p>
<ol>
<li>[ヘルプ] メニューの [About Microsoft Visual Studio] (Microsoft Visual Studio のバージョン情報) を選択します。</li>
<li>[Microsoft Visual Studio のバージョン情報] ダイアログで、バージョン番号を確認します。</li>
</ol>
<p>インストーラを実行すると、Visual Studio 2017以降のバージョン にインストールできるワークロードの選択ウィンドウが表示されます。<br />この<strong>[ワークロード]タブ</strong>の一覧から、<strong>「.NET デスクトップ開発」</strong>と<strong>「ASP.NET と Web 開発」</strong>の２つを選択してください。</p>
<p>ここでは、Visual Studio 2022 Community エディションでご案内いたします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a446317a200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807f3739200d-pi" style="display: inline;"><img alt="Forge-online-training-design-automation-revit-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807f3739200d image-full img-responsive" src="/assets/image_110261.jpg" title="Forge-online-training-design-automation-revit-1" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a446317a200c-pi" style="display: inline;"><br /></a></p>
<p>次に、ポップアップウィンドウ上部の<strong>[個別のコンポーネント]タブ</strong>をクリックします。</p>
<p><strong>.NET Core 3.1 ランタイム</strong>と Revit API を利用するために必要な <strong>.NET Framework SDK と Targeting Pack を選択してください。</strong></p>
<p><strong>.NET Frameworkのバージョンは、お使いの Revit に応じて異なり、また Design Automation API for Revit は、Revit 2019 以降をサポートしています。</strong></p>
<ul>
<li><strong>Revit 2019 : .NET Framework 4.7/4.7.1/4.7.2</strong></li>
<li><strong>Revit 2020 : .NET Framework 4.7/4.7.1/4.7.2</strong></li>
<li><strong>Revit 2021 : .NET Framework 4.8</strong></li>
<li><strong>Revit 2022 : .NET Framework 4.8</strong></li>
<li><strong>Revit 2023 : .NET Framework 4.8</strong></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942face9f3200c-pi" style="display: inline;"><img alt="Forge-online-training-design-automation-revit-2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942face9f3200c image-full img-responsive" src="/assets/image_333733.jpg" title="Forge-online-training-design-automation-revit-2" /></a></p>
<p>既に Visual Studio がインストールされている場合は、下記の URL に記載されている方法で、ワークロードと個別のコンポーネントを追加でインストールすることができます。</p>
<ul>
<li><a href="https://docs.microsoft.com/ja-jp/visualstudio/install/modify-visual-studio?view=vs-2017">https://docs.microsoft.com/ja-jp/visualstudio/install/modify-visual-studio?view=vs-2017</a></li>
</ul>
<p>インストールが完了したら、Visual Studio 2022 を起動し、[ファイル]-&gt;[新規作成]-&gt;[プロジェクト]を選択して[新しいプロジェクト]のテンプレート選択ウィザードを表示し、下記のテンプレートが一覧にあるかご確認ください。</p>
<ul>
<li><strong>[クラス ライブラリ]</strong><br />
<ul>
<li>Revit アドインを作成して、編集、ビルド、デバッグを実行するために使用するテンプレートです。<br /><br /></li>
</ul>
</li>
<li><strong>[ASP.NET Core Webアプリ]</strong><br />
<ul>
<li>Design Automation API を利用する Forge アプリを作成するために使用するテンプレートです。</li>
</ul>
</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807f3783200d-pi" style="display: inline;"><img alt="Forge-online-training-design-automation-revit-3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807f3783200d image-full img-responsive" src="/assets/image_696463.jpg" title="Forge-online-training-design-automation-revit-3" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278807f378b200d-pi" style="display: inline;"><img alt="Forge-online-training-design-automation-revit-4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278807f378b200d image-full img-responsive" src="/assets/image_848890.jpg" title="Forge-online-training-design-automation-revit-4" /></a></p>
<p>&nbsp;</p>
<p><strong>2. Revit 2019 - 2023 のインストール</strong></p>
<p>Revit アドインを編集、ビルド、デバッグし、Revit プロジェクトを閲覧するためには、Revit アプリケーションが必要です。<br />下記のアドレスから、<strong>Revit 2019 - 2023 のいずれかのバージョン</strong>をインストールしてください。</p>
<ul>
<li><a href="https://www.autodesk.co.jp/products/revit/free-trial">https://www.autodesk.co.jp/products/revit/free-trial</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f5fb7200d-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f5fb7200d image-full img-responsive" src="/assets/image_81863.jpg" title="DesignAutomationRevitLearnForgeTutorial5" /></a></p>
<p>インストールが完了したら、下記のディレクトリに RevitAPI.dll が保存されていることを確認してください。</p>
<ul>
<li>C:\Program Files\Autodesk\Revit 20xx\RevitAPI.dll</li>
</ul>
<p>&nbsp;</p>
<p><strong>3. 7zip のインストール</strong></p>
<p>7zip は、オープンソースの高圧縮率のファイルアーカイバ（圧縮・展開/圧縮・解凍ソフト）です。Revit アドインのバンドルパッケージを Visual Studio のビルドイベントで自動で ZIP 圧縮して作成する際に <strong>7zip</strong> を利用します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493f768200b-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493f768200b image-full img-responsive" src="/assets/image_604099.jpg" title="DesignAutomationRevitLearnForgeTutorial6" /></a><br />下記のいずれかのアドレスから、7zip をダウンロードし、インストールしてください。</p>
<ul>
<li><a href="https://www.7-zip.org/">https://www.7-zip.org/</a></li>
<li><a href="https://sevenzip.osdn.jp/">https://sevenzip.osdn.jp/（日本語サイト）</a></li>
</ul>
<p>インストールが完了したら、下記のディレクトリに実行ファイルがあることを確認してください。</p>
<ul>
<li>C:\Program Files\7-Zip\7z.exe</li>
</ul>
<p>&nbsp;</p>
<p><strong>4. ngrok ツールの入手</strong></p>
<p>ngrok は、ローカル Webサーバーをインターネットに公開することができるツールです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493f776200b-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493f776200b image-full img-responsive" src="/assets/image_306828.jpg" title="DesignAutomationRevitLearnForgeTutorial7" /></a><br /><br />ローカルマシンで起動している Webサーバーを、<strong>ngrok ツール</strong>を実行して、任意のポート番号を指定して、インターネット上に一時的に公開することができます。その際、一時的にグローバルの IP アドレスが割り当てられます。<br /><br />下記のアドレスから、ngrok をダウンロードします。Windows 64bit の場合は、「ngrok-stable-windows-amd64.zip」がダウンロードできます。このファイルを解凍して、任意のディレクトリに配置します。最新版は <strong>Agent v3</strong> です。</p>
<ul>
<li><a href="https://ngrok.com/download">https://ngrok.com/download</a></li>
</ul>
<p>次に、下記の URL から ngrok のアカウントを作成します。</p>
<ul>
<li><a href="https://dashboard.ngrok.com/signup">https://dashboard.ngrok.com/signup</a></li>
</ul>
<p>ログイン後、下記のページで割り当てられたトークンを確認します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d3e96ea200b-pi" style="display: inline;"><img alt="Ngrok_authroken" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d3e96ea200b image-full img-responsive" src="/assets/image_358528.jpg" title="Ngrok_authroken" /></a></p>
<p>コマンドプロンプトを開いて、下記のコマンドを実行します。</p>
<ul>
<li>ngrok config add-authtoken Your Authtoken</li>
</ul>
<p>トークンが正常に設定されたら、下記のコマンドを実行します。</p>
<ul>
<li>ngrok http 3000 --host-header=localhost:3000 --scheme http</li>
</ul>
<p>下記のような画面が表示され、http://localhost:3000 にアドレスが割り当てられたことを確認したら完了です。Ctrl+C で終了します。</p>
<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eec7336f200d-pi" style="display: inline;"><img alt="Ngrok_launch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eec7336f200d image-full img-responsive" src="/assets/image_558473.jpg" title="Ngrok_launch" /></a><br /></code></p>
<p><strong>5. Google Chrome</strong></p>
<p>Design Automation API で処理した Revit プロジェクトを <strong>Forge Viewer</strong> で表示するためには、HTML5、CSS3、WebGL をサポートする Webブラウザが必要です。下記の Webブラウザがサポート対象ですが、<strong>Google Chrome</strong> を推奨いたします。</p>
<ul>
<li>Chrome 50+</li>
<li>Firefox 45+</li>
<li>Opera 37+</li>
<li>Safari 9+</li>
<li>Microsoft Edge 20+</li>
<li>Internet Explorer 11</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f6103200d-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f6103200d image-full img-responsive" src="/assets/image_216037.jpg" title="DesignAutomationRevitLearnForgeTutorial8" /></a></p>
<p>下記のアドレスから、Google Chrome をダウンロードし、インストールしてください。</p>
<ul>
<li><a href="https://www.google.co.jp/chrome/">https://www.google.co.jp/chrome/</a></li>
</ul>
<p>Windows 版Windows 版（10 / 8.1 / 8 / 7、64 ビット）の同意画面で [同意してインストール] ボタンで同意後、セットアップ プログラムである ChromeSetup.exe を任意の場所にダウンロードします。</p>
<p>ダウンロードした ChromeSetup.exeをダブルクリックすると、インストールに必要なモジュールのダウンロードが始まり、インストールが自動的に実行されます。</p>
<p>インストールが完了したら、下記のディレクトリに実行ファイルがあることを確認してください。</p>
<ul>
<li>C:\Program Files (x86)\Google\Chrome\Application\chrome.exe</li>
</ul>
<p>&nbsp;</p>
<p><strong>6. Git for Windows のインストール</strong></p>
<p>GitHub 上のサンプル プログラムを入手して利用する際にコマンド プロンプト(bash) で git コマンドを利用して、クライアント コンピュータに GitHub リポジトリの内容をクローンして利用します。今回は、git for Windows を使用することとします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493faff200b-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493faff200b image-full img-responsive" src="/assets/image_470655.jpg" title="DesignAutomationRevitLearnForgeTutorial9" /></a></p>
<p>下記のアドレスから、git for Windows をダウンロードし、インストールしてください。</p>
<ul>
<li><a href="https://gitforwindows.org/">https://gitforwindows.org/</a></li>
</ul>
<p>インストーラを実行すると、git for Windows のインストールを開始します。[Information] 画面が表示されたら [Next &gt;] ボタンをクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f6339200d-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f6339200d img-responsive" src="/assets/image_49268.jpg" title="DesignAutomationRevitLearnForgeTutorial10" /></a></p>
<p>後続する複数の画面では、全項目を既定値のまま [Next &gt;] ボタンをクリックしていきます。</p>
<p>[Configuring the terminal emulator to use with Git Bash] 画面が表示されたら、[Use Windows' default console window] を選択して、[Next] ボタンをクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f6371200d-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f6371200d img-responsive" src="/assets/image_494558.jpg" title="DesignAutomationRevitLearnForgeTutorial11" /></a></p>
<p>[Configuring extra options] 画面が表示されたら、そのまま [Install] ボタンをクリックします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a493fb70200b-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a493fb70200b img-responsive" src="/assets/image_293719.jpg" title="DesignAutomationRevitLearnForgeTutorial12" /></a></p>
<p>インストールが終了したら、コマンド プロンプトを起動して git コマンドが有効なことを確認してください。git --version と入力して git version 2.21.0.windows.1 と表示されれば正常です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46f643a200d-pi" style="display: inline;"><img alt="DesignAutomationRevitLearnForgeTutorial13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46f643a200d image-full img-responsive" src="/assets/image_10128.jpg" title="DesignAutomationRevitLearnForgeTutorial13" /></a></p>
<p>以上で <strong><a href="https://github.com/Autodesk-Forge/learn.forge.designautomation">learn.forge.designautomation サンプル</a></strong> の環境セットアップは完了です。</p>
<p>次回は、サンプルのソースコードを入手して、ローカル環境で実行する手順をご紹介します。</p>
<p>By Ryuji Ogasawara</p>
