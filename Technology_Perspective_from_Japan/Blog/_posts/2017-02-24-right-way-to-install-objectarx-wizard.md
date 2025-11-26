---
layout: "post"
title: "ObjectARX Wizard の正しいインストール方法"
date: "2017-02-24 01:13:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/right-way-to-install-objectarx-wizard.html "
typepad_basename: "right-way-to-install-objectarx-wizard"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d263e849970c-pi" style="float: left;"><img alt="Autocad-icon-128px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d263e849970c img-responsive" src="/assets/image_837043.jpg" style="margin: 0px 5px 5px 0px;" title="Autocad-icon-128px" /></a><strong><a href="http://www.autodesk.com/developautocad" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/developautocad</a></strong> などの開発情報ページには、AutoCAD のバージョン毎に <strong>ObjectARX Wizard</strong>&#0160;というツールが用意されています。</p>
<p>ObjectARX Wizard は、ObjectARX で AutoCAD アドインを開発する際に必要な Visual Studio 用のスケルトン プロジェクトを作成するものです。初心者の方の学習時や試験的なプロトタイプ開発の場面で、手早くプロジェクトを作成したいとき、しばしば ObjectARX Wizard が利用されています。</p>
<p>とても便利な ObjectARX Wizard ですが、インストール時に注意が必要な点がいくつかあります。</p>
<ul>
<li>異なるバージョンの ObjectARX Wizard の共存は出来ない。</li>
<li>インストール時に Windows の ユーザーアカウント制御（UAC）の設定を オフ にする必要がある。</li>
<li>Windows 10 の場合、UAC をオフにすると同時に&#0160;<strong>msiexec</strong> を使ってインストールする必要がある。</li>
</ul>
<p>下記に、各注意点の詳細と対策/回避策を記載します。</p>
<p><strong>異なるバージョンの ObjectARX Wizard の共存は出来ない</strong></p>
<p style="padding-left: 30px;">ObjectARX Wizard は AutoCAD バージョン毎に実装内容が異なりますが、内部で使用している <strong><a href="https://ja.wikipedia.org/wiki/GUID" rel="noopener noreferrer" target="_blank">GUID</a></strong> に同じ値を使用しているため、同じコンピュータで異なるバージョン用の ObjectARX Wizard を共存インストールして利用することが出来ません。</p>
<p style="padding-left: 30px;">例えば、AutoCAD 2015 用の ObjectARX アドインを開発するために、Visual Studio 2012 Update 4 上に ObjectARX Wizards for AutoCAD 2015 for Visual Studio 2012 をインストールして使っているとします。今回、AutoCAD 2017&#0160;用の ObjectARX アドインを開発する目的で、Visual Studio 2015 と &#0160;ObjectARX Wizards for AutoCAD 2017 for Visual Studio 2015 を同じコンピュータにインストールするとします。この場合、AutoCAD 2015 と AutoCAD 2017、また、Visual Studio 2012 Update 4 と Visual Studio 2015 自体の共存は可能ですが、ObjectARX Wizard の共存はサポートされません。もし、インストールが正常に終了しても、先にインストールされていた Visual Studio 2012 Update 4 &#0160;上の ObjectARX Wizard は、プロジェクト内容に AutoCAD 2017 用の環境設定をしてしまいます。もちろん、Visual Studio 2012 には識別できない設定値が含まれるため、プロジェクトの作成に失敗してしまいます。</p>
<p style="padding-left: 30px;">ここでは、一旦、&#0160;ObjectARX Wizards for AutoCAD 2015 for Visual Studio 2012 をアンインストールしてから 、ObjectARX Wizards for AutoCAD 2017 for Visual Studio 2015 をインストールする必要があります。 つまり、どちらか一方のアドイン開発しか出来ません。</p>
<p><strong>ユーザーアカウント制御（UAC）の設定を オフ にする</strong>&#0160;</p>
<p style="padding-left: 30px;">Windows Vista 以降の Windows 上に ObjectARX Wizard をインストールする場合は、<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%A6%E3%83%BC%E3%82%B6%E3%83%BC%E3%82%A2%E3%82%AB%E3%82%A6%E3%83%B3%E3%83%88%E5%88%B6%E5%BE%A1" rel="noopener noreferrer" target="_blank">ユーザ アカウント制御 (UAC)</a></strong>&#0160; の設定を一旦無効にしてからインストールすることをお勧めします。UAC を無効にしないと、インストール自体が成功しても、システム レジストリへの書き込みが出来ていない場合があります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d263eae9970c-pi" style="display: inline;"><img alt="Uac" class="asset  asset-image at-xid-6a0167607c2431970b01b8d263eae9970c img-responsive" src="/assets/image_177023.jpg" style="width: 800px;" title="Uac" /></a></p>
<p style="padding-left: 30px;">この制限はインストール時のみのもので、ObjectARX Wizard の運用時にはユーザ アカウント制御がオンになっていても問題は発生しません。なお、ユーザアカウント制御の設定を変更すると、コンピュータの再起動が必要になる場合があります。</p>
<p><strong>Windows 10 では&#0160;msiexec を使ってインストールする</strong></p>
<p style="padding-left: 30px;">ObjectARX &#0160;Wizard のインストーラ（ObjectARXWizard.msi）にはデジタル署名が施されていないため、インストールに成功してもプロジェクト作成が出来ないことが報告されています。</p>
<p style="padding-left: 30px;">具体的には、Windows 10 にインストールされた Visual Studio 2015 に&#0160;ObjectARX Wizards for AutoCAD 2017 for Visual Studio 2015 をインストールした際、<strong>ObjectARX/DBX Project テンプレート</strong>を指定して新規プロジェクトを作成しようとしても、何も処理されずにスケルトン プロジェクトが作成されない現象が発生します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb097cb822970d-pi" style="display: inline;"><img alt="Vs2015" class="asset  asset-image at-xid-6a0167607c2431970b01bb097cb822970d img-responsive" src="/assets/image_161591.jpg" style="width: 700px;" title="Vs2015" /></a></p>
<p style="padding-left: 30px;">この問題は、Windows 10 上でコマンド プロンプトを管理者モードで起動して、次のように、<strong><a href="https://ja.wikipedia.org/wiki/Microsoft_Windows_Installer" rel="noopener noreferrer" target="_blank">msiexec</a></strong> を使って ObjectARX Wizard をインストールすることで回避することが出来ます。</p>
<p style="padding-left: 60px;"><strong>msiexec /i ObjectARXWizards.msi</strong></p>
<p style="padding-left: 30px;">コマンド プロンプトを管理者モードで起動するには、スタート ボタンから [Windows システム ツール] &gt;&gt; [コマンド プロンプト] を見つけて、マウスの右ボタン メニューから [その他] &gt;&gt; [管理者として実行] を選択してください。</p>
<p>By Toshiaki Isezaki</p>
