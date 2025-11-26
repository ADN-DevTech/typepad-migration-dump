---
layout: "post"
title: "AutoCAD 2022 のカスタマイズ互換性"
date: "2021-05-26 00:15:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/05/autocad-2022-interoperability-for-customization.html "
typepad_basename: "autocad-2022-interoperability-for-customization"
typepad_status: "Publish"
---

<p>今回は過去バージョンに対する Windows 版 AutoCAD 2022 の互換性についてまとめておきたいと思います。まず、AutoCAD 2022 の概要については次のブログ記事をご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/03/new-features-on-autocad-2022-part1.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 の新機能 ～ その1</strong></a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/03/new-features-on-autocad-2022-part2.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 の新機能 ～ その2</strong></a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/03/new-features-on-autocad-2022-part3.html" rel="noopener" target="_blank"><strong>AutoCAD 2022 の新機能 ～ その3</strong></a></p>
<p><strong>図面ファイル形式</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html" rel="noopener noreferrer" target="_blank"></a>AutoCAD 2022、AutoCAD LT 2022 では、昨年の AutoCAD 2021/AutoCAD LT 2021 同様、<strong>2018 図面ファイル形式</strong>&#0160;を採用しています。新規図面を作成して保存する際には、この 2018 図面ファイル形式が既定値となります。</p>
<p style="padding-left: 40px;">もちろん、旧バージョンの図面ファイル形式を開いたり、保存したりする機能も従来通りです。</p>
<p style="padding-left: 40px;"><strong>図面読み込み：</strong></p>
<ul>
<li>すべての AutoCAD バージョンで作成した DWG ファイル （<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html" rel="noopener" target="_blank">非TrustedDWG</a> を除く）</li>
<li>すべての AutoCAD バージョンで作成した DXF ファイル</li>
</ul>
<p style="padding-left: 40px;"><strong>図面の保存：</strong></p>
<ul>
<li>R14, 2000, 2004, 2007, 2010, 2013, 2018 形式の DWG ファイル</li>
<li>R12, 2000, 2004, 2007, 2010, 2013, 2018&#0160; 形式の DXF ファイル</li>
</ul>
<p><strong>アドイン アプリケーションの互換性</strong></p>
<p style="padding-left: 40px;">AutoCAD 2022 は、引き続き、AutoLISP/Visual LISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript API の 5 &#0160;つの AutoCAD API をサポートします。ただし、前バージョンの AutoCAD 2021からは <strong>バイナリ互換リリース </strong>となるため、前バージョンの AutoCAD 2021 用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。ただし、念のため、動作チェックすることをお勧めしています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9a14c7b200b-pi" style="display: inline;"><img alt="Autocad_gokan_1172x660" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9a14c7b200b image-full img-responsive" src="/assets/image_510331.jpg" title="Autocad_gokan_1172x660" /></a></p>
<p><strong>サポート コンパイラ</strong></p>
<p style="padding-left: 40px;">ObjectARX と .NET API でお使いいただくコンパイラは、Visual Studio 2019 16.7 以降をサポートしています（ObjectARX は Visual Studio 2019 16.7）。Visual Studio 2019 のインストール時には、多数のインストール オプションの指定が必要です。ObjectARX を使った開発をする場合には「<strong>C++ によるデスクトップ開発</strong>」を、.NET API を使った開発をする場合には「<strong>.NET デスクトップ開発</strong>」をそれぞれ選択してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fd5f14200d-pi"><img alt="Vs2019_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fd5f14200d image-full img-responsive" src="/assets/image_653349.jpg" title="Vs2019_install" /></a></p>
<p><strong>ObjectARX</strong></p>
<p style="padding-left: 40px;">前バージョンの AutoCAD 2021 用に作成したアドイン アプリケーションをお持ちで、AutoCAD 2021 で使用する必要がない場合には、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx&#0160;</a></strong>から ObjectARX SDK for AutoCAD 2022 をダウンロード、参照して新しい開発環境となる <strong>Visual Studio 2019&#0160;</strong>で再ビルドすることをお勧めします。それ以前の既存プロジェクトの移植では、この作業が必須となります。プロジェクトに設定する「プラットフォーム ツールセット」は <strong>Visual Studio 2019(v142)</strong>&#0160;になります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b482093200c-pi"><img alt="Vs_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b482093200c image-full img-responsive" src="/assets/image_653750.jpg" title="Vs_settings" /></a></p>
<p style="padding-left: 40px;">また、リンクするスタティック リンク ライブラリは、ObjectARX SDK for AutoCAD 2022 の <strong>*</strong><strong>24.lib</strong>&#0160;に変更してください。</p>
<p style="padding-left: 40px;">ObjectARX でカスタム オブジェクトを定義していて、COM サーバーとしてオブジェクト、メソッド、プロパティを COM で公開している場合には、.idl ファイルでインポートしているタイプライブラリも&#0160;<strong>acax24enu.tlb</strong>&#0160;ないし、<strong>acax24jpn.tlb</strong>&#0160;に置き換える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスや関数については、ObjectARX SDK for AutoCAD 2022 の docs フォルダの <strong>Reference Guide（arxref.chm）</strong>から<strong>&#0160;ObjectARX Migration Guide</strong>&#0160;セクションをご確認ください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99cf256200b-pi" style="display: inline;"><img alt="Oarx_migration_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99cf256200b image-full img-responsive" src="/assets/image_31654.jpg" title="Oarx_migration_info" /></a></p>
<p style="padding-left: 40px;">AutoCAD 2022 用の ObjectARX Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> からダウンロードすることが出来ます。ただし、Windows 10 でのインストールには注意が必要です。詳細は、このページ下部の&#0160; <a href="#win10_install"><strong>Windows 10 での &#0160;Wizards の問題 </strong></a>をご確認ください。</p>
<p style="padding-left: 40px;">また、正しくインストールされた&#0160; ObjectARX Wizard を使用した場合でも、Wizardの [MFC Support] 画面で&#0160;<strong>Extension DLL using MFC shared DLL(recommended for MFC support)</strong>&#0160;オプションにチェックしてプロジェクトを作成した場合、プロジェクト作成直後に作成されたプロジェクトがロードされず、[新しいプロジェクト] ダイアログが再度表示されてしまう場合には、お使いの Visual Studio 2019 に MFC コンポーネントがインストールされていない可能性があります。MFC コンポーネントは､コントロール パネル &gt;&gt; プログラムのアンインストール から、<strong>Microsoft Visual Studio Installer</strong>、または、<strong>Visual Studio 2019</strong>&#0160;を選択後、「<strong>変更</strong>」をクリックすると、Visual Studio 2019 のインストール後でも確認やインストール指示が可能です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b482303200c-pi"><img alt="Mfc_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b482303200c image-full img-responsive" src="/assets/image_923381.jpg" title="Mfc_install" /></a></p>
<p><strong>.NET API</strong></p>
<p style="padding-left: 40px;">サポートされる .NET Framework は .<strong>NET Framework &#0160;4.8</strong> です。前バージョンの AutoCAD 2021 用に作成したアドイン アプリケーションをお持ちで、AutoCAD 2021 で使用する必要がない場合には、AutoCAD 2022 のアセンブリ ファイルを参照後、再ビルドをお勧めします。 ターゲット フレームワークに .NET Framework 4.8 のまま変更はありません。</p>
<p style="padding-left: 40px;">AutoCAD 2020 以前からの移植で ターゲット フレームワークに .NET Framework 4.8 に指定する場合には、Visual Studio Installer から、Visual Studio 2019 の [個別のコンポーネント] で「.NET Framework 4.8 Targeting Pack」を先にインストールしておく必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e8605a70200d-pi"><img alt="Dotnet_framework_4_8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e8605a70200d image-full img-responsive" src="/assets/image_246026.jpg" title="Dotnet_framework_4_8" /></a></p>
<p style="padding-left: 40px;">一部のクラスやメソッド、プロパティが変更されている場合がありますので、ソースコードに適切な変更を加える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスやメソッド、プロパティについては、ObjectARX SDK for AutoCAD 2022 の docs フォルダの <strong>Managed Class Reference Guide（arxmgd.chm）</strong>から&#0160;<strong>.NET Migration Guide</strong>&#0160;セクションをご確認ください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeca3a4a200c-pi" style="display: inline;"><img alt="Dotnet_migration_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeca3a4a200c image-full img-responsive" src="/assets/image_297362.jpg" title="Dotnet_migration_info" /></a></p>
<p style="padding-left: 40px;">なお、.NET API の開発者用ガイドは、AutoCAD 2022 の<strong><a href="http://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>内に日本語化された状態で参照することが出来ます。</p>
<p style="padding-left: 40px;">AutoCAD 2022 用の .NET Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> からダウンロードすることが出来ます。ただし、Windows 10 でのインストールには注意が必要です。詳細は、このページ下部の <a href="#win10_install"><strong>Windows 10 での &#0160;Wizards の問題</strong></a>&#0160;をご確認ください。</p>
<p><strong>ActiveX オートメーション（COM）</strong></p>
<p style="padding-left: 40px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。こちらも、可能であれば、新しいバージョンのタイプライブラリを参照しなおしてテストすることをお勧めします。タイプライブラリの詳細は、AutoCAD 2022 の<strong><a href="http://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-927E71C2-E515-438E-9D7A-246D97BEF93F" rel="noopener noreferrer" target="_blank">オンラインヘルプ</a></strong>をご確認ください。</p>
<p style="padding-left: 40px;">VBA をお使いの場合、VBA コンポーネントは<strong><a href="http://www.autodesk.com/vba-download" rel="noopener noreferrer" target="_blank">&#0160;http://www.autodesk.com/vba-download</a></strong>&#0160;から参照可能な Autodesk &#0160;Knowledge Network 記事からダウンロードすることが出来ます。</p>
<p><strong>AutoLISP</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank"><strong>Visual Studio Code での AutoLISP 開発</strong>&#0160;</a>でご案内しているとおり、AutoCAD 2022 でも、従来の Visual LISP エディタが Visual Studio Code に置き換えられていますのでご注意ください。AutoCAD 2022 でも <a class="xref" href="http://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-1A8B50AA-1DEA-4853-AAA8-09AF0827A0ED" rel="noopener" target="_blank">MAKELISPAPP[LISP アプリを作成]</a>&#0160;コマンドを使って、配布に適したアプリケーション ファイル（.vlx ファイル）にコンパイルすることが出来ます。コンパイル時には、複数の AutoLISP ファイル（.lsp ファイル）を 1 つの .vlx ファイルにすることが出来るだけでなく、同時にバイナリ ファイル化されるので、ソース コードを保護することも出来ます。なお、コンパイル時には、従来通り、ウィザードが用意されています。</p>
<p><strong>JavaScript</strong></p>
<p style="padding-left: 40px;">JavaScript ライブラリには変更はありませんので移植作業は不要です。</p>
<p style="padding-left: 40px;">その他、アドイン アプリケーションの互換性に関する情報は、AutoCAD 2022 の<strong><a href="https://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-D54B0935-1638-4F97-8B37-1EC3635A1E71" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>をご参照ください。&#0160;&#0160;</p>
<hr />
<p><strong>AutoCAD 2014 以前のリリースからアドインを移植される際の注意</strong></p>
<p style="padding-left: 40px;">AutoCAD 2014 より前のリリースからアドイン アプリケーションを移植する場合には、リリース毎に加えられたアーキテクチャとセキュリティ仕様等の変更に注意が必要です。特に、次に説明する点は単なる移植ではなく、コードの変更やアプリケーション構成の見直しが必要になる場合があります。</p>
<p style="padding-left: 40px;"><strong>ファイバー削除</strong></p>
<p style="padding-left: 40px;">AutoCAD 2015 では、AutoCAD ウィンドウ内に複数の図面を表示させることが出来るようになった AutoCAD 2000 以降、初めてとなる大きな変更が加えられています。AutoCAD 2014 までは、 図面を表示する子ウィンドウ間の切り替え処理に「ファイバー」という機構が利用されてきましたが、Microsoft 社がファイバーのサポートを終了するのにあわせ、AutoCAD 2015 ではファイバーを使用しない仕組みを導入しています。これが「ファイバー削除」と呼ばれる所以です。</p>
<p style="padding-left: 80px;">ファイバー削除によって、子ウィンドウの切り替え時の振る舞いに仕様変更が加えられています。また、ドキュメントの切り替えなど、各種 API を利用してイベントの発生順序に依存するような処理を実装している場合には、従来の構成が期待通り動作しない可能性もあります。AutoCAD 2015 で導入されたファイバー削除の影響については、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/04/autocad-2015-interoperability-for-customization.html" rel="noopener noreferrer" target="_blank">AutoCAD 2015 のカスタマイズ互換性</a>&#0160;</strong>の&#0160;<strong>ファイバー削除の影響&#0160;</strong>項をご参照ください。</p>
<p style="padding-left: 40px;"><strong>ゼロドキュメントの状態</strong>&#0160;</p>
<p style="padding-left: 80px;">AutoCAD 2015 では、いままでのバージョンのように、起動直後に図面を表示する図面子ウィンドウは表示されません。既定では [新しいタブ] が表示されます（AutoCAD 2016 以降は [スタート] タブに名称変更）。ObjectARX や AutoCAD .NET API を使ったアプリケーションがアクティブなドキュメント（AcApDocument、Document）や図面データベース（AcDbDatabase、Database）を参照している場合には、図面が表示されていない状態、つまり、ゼロドキュメント状態 を想定したプログラム改修が必要です。例えば、現在アクティブな図面データベースの取得を実装している場合には、acdbHostApplicationServices()-&gt;workingDatabase() や&#0160;HostApplicationServices.WorkingDatabase の戻り値が、null、Null、Nothing（言語によって異なる）でないことを明示的に確認する必要があります。</p>
<p style="padding-left: 40px;"><strong>デジタル署名</strong></p>
<p style="padding-left: 80px;">AutoCAD 2016 以降、セキュリティ向上の観点から、使用する API の種類を問わず、すべてのアドイン アプリケーションにはデジタル署名が求められます。デジタル署名のないアドインを AutoCAD にロードしようとすると、警告メッセージが表示されますのでご注意ください。アドイン アプリケーションに対するデジタル署名埋め込みの詳細については、ブログ記事&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2015/08/security-on-autocad-2016-and-digital-signature-to-addin.html" rel="noopener noreferrer" target="_blank">AutoCAD 2016 のセキュリティとアドインのデジタル署名</a></strong>&#0160;をご確認ください。この内容は、AutoCAD 2018 にも当てはまります。</p>
<p style="padding-left: 40px;"><strong>メニュー ボタン</strong></p>
<p style="padding-left: 80px;">旧バージョンでカスタム ボタンの背景を透明に表現する目的で使用できた RGB 値、192,192,192 の色指定は、仕様変更によって AutoCAD 2017 から無効になっています。このため、AutoCAD 2017 以降のバージョンで透過性を持つカスタム ボタンのイメージを利用するためには、BMP ファイル形式ではなく、標準で透過性をサポートする PNG ファイルをお使いください。</p>
<p style="padding-left: 80px;">PNG ファイルへの変換手順は、AutoCAD 2018 オンライン ヘルプの<strong><a href="http://help.autodesk.com/view/ACD/2018/JPN/?guid=GUID-94A0EA14-5165-4D84-A2F1-F44D33F80BCC" rel="noopener noreferrer" target="_blank">記事</a></strong>をご確認ください。もし、リソース DLL でカスタム ボタンのビットマップ イメージを管理される場合は、ブログ記事&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/resource-only-dll-with-png-files.html" rel="noopener noreferrer" target="_blank">PNG ファイルを使ったリソース DLL</a></strong>&#0160;に PNG ファイルを使ったリソース DLL ファイルの作成方法をご紹介していますのでご参照ください。<br />この方法で、ダークテーマとライトテーマの両方を 1 つの画像ファイルでサポート出来るようになります。</p>
<hr />
<p><a name="win10_install"></a><strong>Windows 10 での &#0160;Wizards の問題</strong></p>
<p style="padding-left: 40px;">オートデスクは、AutoCAD アドイン開発用に Visual Studio のスケルトン プロジェクトを作成する Wizards を、.NET API 用と ObjectARX 用にそれぞれ提供しています。両 Wizards は、<a href="http://www.autodesk.com/developautocad" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/developautocad</a>&#0160;から入手することが出来ます。</p>
<p style="padding-left: 40px;">Windows 10 上での ObjectARX Wizards のインストールや動作不良については、次の Autodesk Knowledge Network 記事をご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://knowledge.autodesk.com/community/article/57616" rel="noopener" target="_blank"><strong>AutoCAD API：ObjectARX Wizards が動作しない</strong></a></p>
<p style="padding-left: 40px;"><span style="color: #0000ff; background-color: #ffff00;"><strong>ご注意：</strong></span>AutoCAD 2021 開発用に <strong><a class="ng-binding ng-scope" href="https://knowledge.autodesk.com/community/article/300101" ng-bind-html="doc.title | highlight:filterText" ng-href="https://knowledge.autodesk.com/community/article/300101" ng-if="doc.dtype != &#39;Link&#39;" rel="noopener" target="_blank">AutoCAD API：Visual Studio 2019 に .NET Wizards が認識されない</a></strong> の Autodesk Knowledge Network 記事に沿って _34EEC1CC133F4F489A28FCAE47DA4684.zip ファイルと _71ED6AA364074B9BAE8E4BDC8E024143.zip ファイルを C:\Users\<span style="background-color: #ffffff;"><em>&lt;user name&gt;</em></span>\ドキュメント\Visual Studio 2019\Templates\ProjectTemplatesフォルダ（または C:\Users\<span style="background-color: #ffffff;"><em>&lt;user name&gt;</em></span>\OneDrive\ドキュメント\Visual Studio 2019\Templates\ProjectTemplates フォルダに配置している場合、AutoCAD 2022 用の .NET Wizard が識別されない場合があります。AutoCAD 2022 用の .NET Wizard のインストール前に _34EEC1CC133F4F489A28FCAE47DA4684.zip ファイルと _71ED6AA364074B9BAE8E4BDC8E024143.zip ファイルを削除してください。</p>
<p><strong>AutoCAD .NET API の参照アセンブリ</strong></p>
<p style="padding-left: 40px;">以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html" rel="noopener noreferrer" target="_blank">NuGet と AutoCAD.NET API</a></strong> のブログ記事でもご案内したのと同様に、AutoCAD 2022 上で .NET API で使用する際に参照するアセンブリは、Visual Studio 上の NuGet パッケージ マネージャか、オンラインで入手することも出来ます。当該 NuGet ページは <a href="https://www.nuget.org/packages/AutoCAD.NET/24.1.51000" rel="noopener" target="_blank">https://www.nuget.org/packages/AutoCAD.NET/24.1.51000</a>&#0160;です。&#0160;&#0160;</p>
<p>By Toshiaki Isezaki</p>
