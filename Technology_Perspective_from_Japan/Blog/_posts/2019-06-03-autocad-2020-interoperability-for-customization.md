---
layout: "post"
title: "AutoCAD 2020 のカスタマイズ互換性（SDK、ツール）"
date: "2019-06-03 00:12:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/06/autocad-2020-interoperability-for-customization.html "
typepad_basename: "autocad-2020-interoperability-for-customization"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part1.html" rel="noopener" target="_blank"> </a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49a81c3200b-pi" style="float: right;"><img alt="Qubit" class="asset  asset-image at-xid-6a0167607c2431970b0240a49a81c3200b img-responsive" src="/assets/image_650747.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Qubit" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a475eb41200d-pi" style="float: right;"></a><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part1.html" rel="noopener" target="_blank">AutoCAD 2020 新機能 ～ その 1</a></strong> で一部ご案内していますが、今回は、前バージョンに対する AutoCAD 2020 の互換性について、アドイン アプリケーション用の SDK やツールも含め、まとめておきたいと思います。</p>
<p><strong>図面ファイル形式</strong></p>
<p style="padding-left: 40px;">AutoCAD 2020、AutoCAD LT 2020 では、昨年の AutoCAD 2019/AutoCAD LT 2019 同様、<strong>2018 図面ファイル形式</strong>&#0160;を採用しています。新規図面を作成して保存する際には、この 2018 図面ファイル形式が既定値となります。</p>
<p style="padding-left: 40px;">もちろん、旧バージョンの図面ファイル形式を開いたり、保存したりする機能も従来通りです。</p>
<p style="padding-left: 40px;"><strong>図面読み込み：</strong></p>
<ul>
<li>すべての AutoCAD バージョンで作成した DWG ファイル</li>
<li>すべての AutoCAD バージョンで作成した DXF ファイル</li>
</ul>
<p style="padding-left: 40px;"><strong>図面の保存：</strong></p>
<ul>
<li>R14, 2000, 2004, 2007, 2010, 2013, 2018 形式の DWG ファイル</li>
<li>R12, 2000, 2004, 2007, 2010, 2013, 2018&#0160; 形式の DXF ファイル</li>
</ul>
<p><strong>アドイン アプリケーションの互換性</strong></p>
<p style="padding-left: 40px;">AutoCAD 2020 は、引き続き、AutoLISP/Visual LISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript API の 5 &#0160;つの AutoCAD API をサポートします。前バージョン AutoCAD 2019 との <strong>バイナリ互換リリース </strong>となるため、AutoCAD 2019 用に各 AutoCAD API 作成されたアドイン アプリケーションの移植作業が不要です。念のため、AutoCAD 2020 の<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-E69C877D-F84B-4282-807D-E084C931D533" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>に記載のある アドイン アプリケーションの互換性に関する情報もご参照ください。&#0160;&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49a8174200b-pi" style="display: inline;"><img alt="Compatibilities" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a49a8174200b image-full img-responsive" src="/assets/image_546819.jpg" title="Compatibilities" /></a></p>
<p style="padding-left: 40px;">下記の表は、過去数バージョンとの比較早見表です。「DWG形式文字列」は、DWG ファイルに書き込まれている DWG ファイル形式を表す文字列（<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/sfdcarticles/sfdcarticles/JPN/drawing-version-codes-for-autocad.html" rel="noopener" target="_blank">AutoCAD の図面形式のバージョン コード</a></strong>）です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4764a70200d-pi" style="display: inline;"><img alt="Quick_check_matrix" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4764a70200d image-full img-responsive" src="/assets/image_297147.jpg" title="Quick_check_matrix" /></a></p>
<p><strong>サポート コンパイラ</strong></p>
<p style="padding-left: 40px;">ObjectARX と .NET API でお使いいただくコンパイラは、Visual Studio 2017 15.7.5 以降をサポートしています。Visual Studio 2017 のインストール時には、多数のインストール オプションの指定が必要です。ObjectARX の開発をされる場合には「<strong>C++ によるデスクトップ開発</strong>」を、.NET API の開発をされる場合には「<strong>.NET デスクトップ開発</strong>」をそれぞれ選択してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49add09200b-pi" style="display: inline;"><img alt="Vx_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a49add09200b image-full img-responsive" src="/assets/image_598473.jpg" title="Vx_install" /></a></p>
<p><strong>ObjectARX</strong></p>
<p style="padding-left: 40px;">前バージョンの AutoCAD 2019 用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。</p>
<p style="padding-left: 40px;">新規に Visual Studio プロジェクトを作成、または、ビルドする場合には、ObjetARX SDK for AutoCAD 2020 が必要です。SDK は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx" rel="noopener" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx</a></strong> からダウンロード ページへ遷移して入手することが出来ます。なお、AutoCAD 2020 の <strong><a href="https://knowledge.autodesk.com/ja/search-result/caas/simplecontent/content/autocad-and-autocad-lt-32-bit-discontinuation.html" rel="noopener" target="_blank">32 ビット版廃止</a></strong>の決定を受けて、ObjetARX SDK for AutoCAD 2020 は 64 ビット版のみの提供となります。</p>
<p style="padding-left: 40px;">AutoCAD 2020 用の ObjectARX Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> からダウンロードすることが出来ます。ObjectARX Wizard のインストールでは、後述する <strong>Windows 10 での &#0160;Wizards の問題</strong> の他、次の点にご注意ください。</p>
<p style="padding-left: 40px;"><strong>Windows 10 への ObjectARX Wizards のインストール</strong></p>
<p style="padding-left: 40px;">Windows 10 上でコマンド プロンプトを管理者モードで起動して、<strong><a href="https://msdn.microsoft.com/ja-jp/library/cc759262%28v=ws.10%29.aspx" rel="noopener noreferrer" target="_blank">msiexec</a></strong>&#0160;を使って ObjectARX Wizard をインストールしてください。コマンド プロンプトを管理者モードで起動するには、スタート ボタンから [Windows システム ツール] &gt;&gt; [コマンド プロンプト] を見つけて、マウスの右ボタン メニューから [その他] &gt;&gt; [管理者として実行] を選択します。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a08c9b200b-pi" style="display: inline;"><img alt="Launch_prpmpt_as_admin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a08c9b200b image-full img-responsive" src="/assets/image_394352.jpg" title="Launch_prpmpt_as_admin" /></a></p>
<p style="padding-left: 40px;">コマンド プロンプト上で misexec の実行する際には、<strong>msiexec /i ObjectARXWizard2020.msi</strong>&#0160;と入力をしてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a47beaa4200d-pi" style="display: inline;"><img alt="Oarx_wizard_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a47beaa4200d image-full img-responsive" src="/assets/image_113055.jpg" title="Oarx_wizard_install" /></a></p>
<p style="padding-left: 40px;">Windows 10 で上記手順で ObjectARX Wizards をインストールしないと、Visual Studio 2017 を使ってインストールされた <strong>ARX/DBX Project for AutoCAD 2020 テンプレート</strong>を指定後、新規プロジェクトを作成しようとしても、何も処理されない現象が発生します。</p>
<p><strong>.NET API</strong></p>
<p style="padding-left: 40px;">サポートされる .NET Framework は <strong>.NET Framework &#0160;4.7</strong> です。前バージョンの AutoCAD 2019 用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。.NET API の開発者用ガイドは、AutoCAD 2020 の<strong><a href="http://help.autodesk.com/view/OARX/2020/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>内に日本語化されたものをご参照いただけます。</p>
<p style="padding-left: 40px;">AutoCAD 2020 用の .NET Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> からダウンロードすることが出来ます。インストールでは、後述する <strong>Windows 10 での &#0160;Wizards の問題</strong> にご注意ください。</p>
<p style="padding-left: 40px;">Windows 10 をお使いで、.NET Framework 4.7 以降のバージョンが既にインストール済のため、.NET Framework 4.7 をインストール出来ない、あるいは、Visual Studio 2017 でターゲット フレームワークを 4.7 に設定出来ない（.NET Framework 4.7 を指定した新規プロジェクト作成が出来ない）場合は、[コントロール パネル] &gt;&gt; [プログラム] &gt;&gt; [プログラムと機能] から、<strong>Microsoft Visual Studio Installer</strong> を起動して、[個別コンポーネント] タブから .NET Framework 4.7&#0160; のインストールをお試しください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a488747a200d-pi" style="display: inline;"><img alt="Dotnet_4.7_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a488747a200d image-full img-responsive" src="/assets/image_458147.jpg" title="Dotnet_4.7_install" /></a></p>
<p><strong>AutoLISP/ActiveX オートメーション（COM）、VBA/JavaScript</strong></p>
<p style="padding-left: 40px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。可能であれば、新しいバージョンのタイプライブラリを参照しなおしてテストすることをお勧めします。タイプライブラリの詳細は、AutoCAD 2020 の<strong><a href="http://help.autodesk.com/view/OARX/2020/JPN/?guid=GUID-F085048B-819F-4D70-BB4F-A95FF1498170" rel="noopener noreferrer" target="_blank">オンラインヘルプ</a></strong>をご確認ください。</p>
<p style="padding-left: 40px;">VBA をお使いの場合、VBA コンポーネントは<a href="http://www.autodesk.com/vba-download" rel="noopener noreferrer" target="_blank">&#0160;h<strong>ttp://www.autodesk.com/vba-download</strong></a> から参照可能な Autodesk &#0160;Knowledge Network 記事からダウンロードすることが出来ます。こちらも、 64 ビット版のみの提供となります。</p>
<p style="padding-left: 40px;">JavaScript ライブラリには変更はありません。また、移植作業は不要です。</p>
<p><strong>Windows 10 での &#0160;Wizards の問題</strong></p>
<p style="padding-left: 40px;">AutoCAD アドイン開発用に Visual Studio のスケルトン プロジェクトを作成する Wizards&#0160; の msi 形式のインストーラには、インストーラ自体にデジタル署名が施されていないため、インストール時に警告メッセージが表示されてしまう問題があります。</p>
<p style="padding-left: 40px;">インストール開始時に「Windows によって PC が保護されました」とメッセージが表示されたら、画面上に表示されてる&#0160;<strong>詳細情報</strong>&#0160;リンクをクリックして、[実行] ボタンからインストールをしてみてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e857f7970c-pi"><img alt="Win10_1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e857f7970c img-responsive" src="/assets/image_30430.jpg" title="Win10_1" /></a></p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0a0143e2970d-pi"><img alt="Win10_2" class="asset  asset-image at-xid-6a0167607c2431970b01bb0a0143e2970d img-responsive" src="/assets/image_64972.jpg" title="Win10_2" /></a></p>
<p><strong>AutoCAD .NET API の参照アセンブリ</strong></p>
<p style="padding-left: 40px;">以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html" rel="noopener noreferrer" target="_blank">NuGet と AutoCAD.NET API</a></strong> のブログ記事でもご案内したのと同様に、AutoCAD 2020 上で .NET API で使用する際に参照するアセンブリは、Visual Studio 上の NuGet パッケージ マネージャか、オンラインで入手することも出来ます。当該 NuGet ページは <a href="https://www.nuget.org/packages/AutoCAD.NET/23.1.0" rel="noopener noreferrer" target="_blank">https://www.nuget.org/packages/AutoCAD.NET/23.1.0</a>&#0160;です。&#0160;&#0160;</p>
<p>By Toshiaki Isezaki</p>
