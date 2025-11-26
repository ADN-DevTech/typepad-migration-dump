---
layout: "post"
title: "AutoCAD 2024 のカスタマイズ互換性"
date: "2023-05-29 00:01:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/05/autocad-2024-interoperability-for-customization.html "
typepad_basename: "autocad-2024-interoperability-for-customization"
typepad_status: "Publish"
---

<p>前バージョンから AutoCAD 2024（Windows 版）への互換性をまとめてご案内していきます。</p>
<p>AutoCAD 2024 の概要については次のブログ記事をご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/new-features-on-autocad-2024-part1.html" rel="noopener" target="_blank">AutoCAD 2024 新機能 ～ その１</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/new-features-on-autocad-2024-part2.html" rel="noopener" target="_blank">AutoCAD 2024 新機能 ～ その２</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/04/new-features-on-autocad-2024-part3.html" rel="noopener" target="_blank">AutoCAD 2024 新機能 ～ その３</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/04/new-features-on-autocad-2024-part4.html" rel="noopener" target="_blank">AutoCAD 2024 新機能 ～ その４</a></p>
<p><strong>図面ファイル形式</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html" rel="noopener noreferrer" target="_blank"></a>AutoCAD 2024、AutoCAD LT 2024 では、引き続き、<strong>2018 図面ファイル形式</strong>&#0160;を採用しています。新規図面を作成して保存する際には、この 2018 図面ファイル形式が既定値となります。</p>
<p style="padding-left: 40px;">もちろん、旧バージョンの図面ファイル形式を開いたり、保存したりする機能も従来通りです。</p>
<p><strong>図面読み込み：</strong></p>
<ul>
<li>すべての AutoCAD バージョンで作成した DWG ファイル （<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html" rel="noopener" target="_blank">非TrustedDWG</a>&#0160;を除く）</li>
<li>すべての AutoCAD バージョンで作成した DXF ファイル</li>
</ul>
<p><strong>図面の保存：</strong></p>
<ul>
<li>R14, 2000, 2004, 2007, 2010, 2013, 2018 形式の DWG ファイル</li>
<li>R12, 2000, 2004, 2007, 2010, 2013, 2018&#0160; 形式の DXF ファイル</li>
</ul>
<p><strong>アドイン アプリケーションの互換性</strong></p>
<p style="padding-left: 40px;">AutoCAD 2024 は、AutoLISP/Visual LISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript API の 5 &#0160;つの AutoCAD API をサポートします。前バージョンの AutoCAD 2023 からは <strong>バイナリ互換リリース&#0160;</strong>となるため、同バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。ただし、念のため、動作チェックすることをお勧めしています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a62fd1200c-pi" style="display: inline;"><img alt="Autocad_gokan" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a62fd1200c image-full img-responsive" src="/assets/image_913314.jpg" title="Autocad_gokan" /></a></p>
<p style="padding-left: 40px;">過去バージョンと、その前バージョンからの互換状況、また、移植に必要となる基本情報は次のとおりです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a536b3200c-pi" style="display: inline;"><img alt="Migration_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a536b3200c image-full img-responsive" src="/assets/image_667666.jpg" title="Migration_info" /></a></p>
<p><strong>サポート コンパイラ</strong></p>
<p style="padding-left: 40px;">ObjectARX と .NET API でお使いいただくコンパイラは、Visual Studio 2022 17.2 以降をサポートしています（ObjectARX は Visual Studio 2022 17.2.6 移行）。Visual Studio 2022 のインストール時には、多数のインストール オプションの指定が必要です。ObjectARX を使った開発をする場合には「<strong>C++ によるデスクトップ開発</strong>」を、.NET API を使った開発をする場合には「<strong>.NET デスクトップ開発</strong>」をそれぞれ選択してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d3df200b-pi" style="display: inline;"><img alt="C++_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d3df200b image-full img-responsive" src="/assets/image_116699.jpg" title="C++_install" /></a></p>
<p><strong>ObjectARX</strong></p>
<p style="padding-left: 40px;">前バージョンの AutoCAD 2023 用に作成したアドイン アプリケーションをお持ちで、AutoCAD 2023 で使用する必要がない場合には、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx&#0160;</a></strong>から ObjectARX SDK for AutoCAD 2024 をダウンロード、参照して新しい開発環境となる <strong>Visual Studio 2022 </strong>で再ビルドすることをお勧めします。それ以前の既存プロジェクトの移植では、この作業が必須となります。プロジェクトに設定する「プラットフォーム ツールセット」は&#0160;<strong>Visual Studio 2022(v143)</strong>&#0160;になります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d40b200b-pi" style="display: inline;"><img alt="Platform_toolset" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d40b200b image-full img-responsive" src="/assets/image_202181.jpg" title="Platform_toolset" /></a></p>
<p style="padding-left: 40px;">また、リンクするスタティック リンク ライブラリは、ObjectARX SDK for AutoCAD 2024 の <strong>*</strong><strong>24.lib</strong>&#0160;に変更してください。</p>
<p style="padding-left: 40px;">ObjectARX でカスタム オブジェクトを定義していて、COM サーバーとしてオブジェクト、メソッド、プロパティを COM で公開している場合には、.idl ファイルでインポートしているタイプライブラリも&#0160;<strong>acax24enu.tlb</strong>&#0160;ないし、<strong>acax24jpn.tlb</strong>&#0160;に置き換える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスや関数については、ObjectARX SDK for AutoCAD 2024 の docs フォルダの <strong>Reference Guide（arxref.chm）</strong>から<strong>&#0160;ObjectARX Migration Guide</strong> セクション、または、<strong><a href="https://help.autodesk.com/view/OARX/2024/JPN/?guid=OARX-RefGuide-ObjectARX_Migration_Guide" rel="noopener" target="_blank">オンラインヘルプ</a></strong>&#0160;をご確認ください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d461200b-pi" style="display: inline;"><img alt="Oarx_migration_guide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d461200b image-full img-responsive" src="/assets/image_364088.jpg" title="Oarx_migration_guide" /></a></p>
<p style="padding-left: 40px;">以前の AuoCAD からの互換情報については、AutoCAD 2024 のオンライン ヘルプ、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-C21B8F00-C7DE-4E44-8006-D5DC99199F31" rel="noopener" target="_blank"><strong>ObjectARX の互換性</strong></a> もご確認ください。</p>
<p style="padding-left: 40px;">AutoCAD 2024 用の ObjectARX Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> ページ下部からダウンロードすることが出来ます。ただし、Windows 10 以降移行でのインストールには注意が必要です。詳細は、このページ下部の&#0160; <a href="https://adndevblog.typepad.com/technology_perspective/2023/05/autocad-2024-interoperability-for-customization.html#win10_install"><strong>Windows 10 での &#0160;Wizards の問題&#0160;</strong></a>をごください。</p>
<p style="padding-left: 40px;">また、正しくインストールされた&#0160; ObjectARX Wizard を使用した場合でも、Wizardの [MFC Support] 画面で&#0160;<strong>Extension DLL using MFC shared DLL(recommended for MFC support)</strong> オプションにチェックしてプロジェクトを作成した場合、プロジェクト作成直後に作成されたプロジェクトがロードされず、[新しいプロジェクト] ダイアログが再度表示されてしまう場合には、お使いの Visual Studio 2022 に MFC コンポーネントがインストールされていない可能性があります。MFC コンポーネントは､コントロール パネル &gt;&gt; プログラムのアンインストール から、<strong>Microsoft Visual Studio Installer</strong>、または、<strong>Visual Studio 2022</strong>&#0160;を選択後、「<strong>変更</strong>」をクリックすると、Visual Studio 2022 のインストール後でも確認やインストール指示が可能です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a5377d200c-pi" style="display: inline;"><img alt="C++_mfc_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a5377d200c image-full img-responsive" src="/assets/image_981962.jpg" title="C++_mfc_install" /></a></p>
<p><strong>.NET API</strong></p>
<p style="padding-left: 40px;">サポートされる .NET Framework は .<strong>NET Framework &#0160;4.8</strong> です。前バージョンの AutoCAD 2023 用に作成したアドイン アプリケーションをお持ちで、AutoCAD 2023 で使用する必要がない場合には、AutoCAD 2024 のアセンブリ ファイルを参照後、再ビルドをお勧めします。 ターゲット フレームワークは .NET Framework 4.8 のままで変更はありません。</p>
<p style="padding-left: 40px;">AutoCAD 2020 以前からの移植で ターゲット フレームワークに .NET Framework 4.8 に指定する場合には、Visual Studio Installer から、Visual Studio 2022 の [個別のコンポーネント] で「.NET Framework 4.8 Targeting Pack」を先にインストールしておく必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d4ee200b-pi" style="display: inline;"><img alt="4_8_target_pack" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d4ee200b image-full img-responsive" src="/assets/image_962988.jpg" title="4_8_target_pack" /></a></p>
<p style="padding-left: 40px;">一部のクラスやメソッド、プロパティが変更されている場合がありますので、ソースコードに適切な変更を加える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスやメソッド、プロパティについては、ObjectARX SDK for AutoCAD 2024 の docs フォルダの <strong>Managed Class Reference Guide（arxmgd.chm）</strong>から&#0160;<strong>.NET Migration Guide</strong>&#0160;セクションセクション、または、<strong><a href="https://help.autodesk.com/view/OARX/2024/JPN/?guid=OARX-ManagedRefGuide-_NET_Migration_Guide" rel="noopener" target="_blank">オンラインヘルプ </a></strong>をご確認ください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d504200b-pi" style="display: inline;"><img alt="Net_migration_guide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d504200b image-full img-responsive" src="/assets/image_897517.jpg" title="Net_migration_guide" /></a></p>
<p style="padding-left: 40px;">なお、.NET API の開発者用ガイドは、AutoCAD 2024 のオンライン ヘルプ内で <strong><a href="http://help.autodesk.com/view/OARX/2024/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener noreferrer" target="_blank">Managed .NET 開発者用ガイド(.NET)</a></strong> として日本語化されたドキュメントを参照することが出来ます。</p>
<p style="padding-left: 40px;">以前の AuoCAD からの互換情報については、同じく、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-A6C680F2-DE2E-418A-A182-E4884073338A" rel="noopener" target="_blank"><strong>Managed .NET の互換性</strong></a> もご確認ください。</p>
<p style="padding-left: 40px;">AutoCAD 2024 用の .NET Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> ページ下部からダウンロードすることが出来ます。ただし、Windows 10 以降でのインストールには注意が必要です。詳細は、このページ下部の <a href="https://adndevblog.typepad.com/technology_perspective/2023/05/autocad-2024-interoperability-for-customization.html#win10_install"><strong>Windows 10 での &#0160;Wizards の問題</strong></a>&#0160;をご確認ください。</p>
<p><strong>ActiveX オートメーション（COM）</strong></p>
<p style="padding-left: 40px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。こちらも、可能であれば、新しいバージョンのタイプライブラリを参照しなおしてテストすることをお勧めします。以前の AuoCAD からの互換情報を含め、タイプライブラリの詳細は、AutoCAD 2024 のオンライン ヘルプ、<strong><a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-927E71C2-E515-438E-9D7A-246D97BEF93F" rel="noopener noreferrer" target="_blank">VBA と ActiveX の互換性</a></strong>&#0160;をご確認ください。</p>
<p style="padding-left: 40px;">VBA をお使いの場合、VBA コンポーネントは<strong><a href="http://www.autodesk.com/vba-download-jpn" rel="noopener noreferrer" target="_blank"> http://www.autodesk.com/vba-download-jpn</a></strong>&#0160;から参照可能な Autodesk &#0160;Knowledge Network 記事からダウンロードすることが出来ます。</p>
<p><strong>AutoLISP</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank"><strong>Visual Studio Code での AutoLISP 開発</strong>&#0160;</a>でご案内しているとおり、AutoCAD 2024 でも、従来の Visual LISP エディタが Visual Studio Code に置き換えられていますのでご注意ください。AutoCAD 2024 でも <a class="xref" href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-1A8B50AA-1DEA-4853-AAA8-09AF0827A0ED" rel="noopener" target="_blank">MAKELISPAPP[LISP アプリを作成]</a>&#0160;コマンドを使って、配布に適したアプリケーション ファイル（.vlx ファイル）にコンパイルすることが出来ます。コンパイル時には、複数の AutoLISP ファイル（.lsp ファイル）を 1 つの .vlx ファイルにすることが出来るだけでなく、同時にバイナリ ファイル化されるので、ソース コードを保護することも出来ます。なお、コンパイル時には、従来通り、ウィザードが用意されています。</p>
<p style="padding-left: 40px;">以前の AuoCAD からの互換情報については、AutoCAD 2024 のオンライン ヘルプ、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-31FD1A96-C002-434E-8684-63D50BE0CF94" rel="noopener" target="_blank"><strong>AutoLISP の互換性</strong></a> もご確認ください。</p>
<p><strong>JavaScript</strong></p>
<p style="padding-left: 40px;">JavaScript ライブラリには変更はありませんので移植作業は不要です。</p>
<p style="padding-left: 40px;">その他、アドイン アプリケーションの互換性に関する情報は、AutoCAD 2024 の<strong><a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-D54B0935-1638-4F97-8B37-1EC3635A1E71" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>をご参照ください。&#0160;&#0160;</p>
<hr />
<p><a name="win10_install"></a><strong>Windows 10 以降での &#0160;Wizards の問題</strong></p>
<p>オートデスクは、AutoCAD アドイン開発用に Visual Studio のスケルトン プロジェクトを作成する Wizards を、.NET API 用と ObjectARX 用にそれぞれ提供しています。両 Wizards は、<a href="http://www.autodesk.com/developautocad" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/developautocad</a>&#0160;から入手することが出来ます。</p>
<p>Windows 10 上での ObjectARX Wizards のインストールや動作不良については、次の Autodesk Knowledge Network 記事をご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/rN505dBb9mYfimVM6GmCD.html" rel="noopener" target="_blank"><strong>ObjectARX：ObjectARX Wizards が動作しない</strong></a></p>
<p style="padding-left: 40px;"><span style="background-color: #ffffff;"><strong><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/objectarx-objectarx-wizard-ga-visual-studio-xin-guipupurojekutoni-biao-shisarenai/ta-p/11995266" rel="noopener" target="_blank">ObjectARX：ObjectARX Wizard が Visual Studio 新規プロジェクトに表示されない</a></strong></span></p>
<p style="padding-left: 40px;"><span style="background-color: #ffffff;"><strong>ご参考：</strong>AutoCAD 2021 開発用に&#0160;<a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/5eAUv6NmaQ191L5yZO4qv5.html" rel="noopener" target="_blank"><strong>AutoCAD API：Visual Studio 2019 に .NET Wizards が認識されない</strong> </a>の Autodesk Knowledge Network 記事に沿って _34EEC1CC133F4F489A28FCAE47DA4684.zip ファイルと _71ED6AA364074B9BAE8E4BDC8E024143.zip ファイルを C:\Users\<em>&lt;user name&gt;</em>\ドキュメント\Visual Studio 2019\Templates\ProjectTemplatesフォルダ（または C:\Users\<em>&lt;user name&gt;</em>\OneDrive\ドキュメント\Visual Studio 2019\Templates\ProjectTemplates フォルダに配置している場合、AutoCAD 2023 用の .NET Wizard が識別されない場合があります。AutoCAD 2023 用の .NET Wizard のインストール前に _34EEC1CC133F4F489A28FCAE47DA4684.zip ファイルと _71ED6AA364074B9BAE8E4BDC8E024143.zip ファイルを削除してください。</span></p>
<p><strong>AutoCAD .NET API の参照アセンブリ</strong></p>
<p style="padding-left: 40px;">以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html" rel="noopener noreferrer" target="_blank">NuGet と AutoCAD.NET API</a></strong> のブログ記事でもご案内したのと同様に、AutoCAD 2024 上で .NET API で使用する際に参照するアセンブリは、Visual Studio 上の NuGet パッケージ マネージャか、オンラインで入手することも出来ます。</p>
<p style="padding-left: 40px;"><strong><a href="https://www.nuget.org/packages/AutoCAD.NET/" rel="noopener" target="_blank">https://www.nuget.org/packages/AutoCAD.NET/</a></strong>&#0160;&#0160;</p>
<p>By Toshiaki Isezaki</p>
