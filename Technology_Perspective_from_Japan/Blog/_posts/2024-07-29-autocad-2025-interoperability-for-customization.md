---
layout: "post"
title: "AutoCAD 2025 のカスタマイズ互換性"
date: "2024-07-29 00:04:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/07/autocad-2025-interoperability-for-customization.html "
typepad_basename: "autocad-2025-interoperability-for-customization"
typepad_status: "Publish"
---

<p>前バージョンから AutoCAD 2025（Windows 版）への互換性をまとめてご案内していきます。</p>
<p>AutoCAD 2025 の概要については次のブログ記事をご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/03/autocad-2025-debut.html" rel="noopener" target="_blank">AutoCAD 2025 登場</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/04/new-features-on-autocad-2025.html" rel="noopener" target="_blank">AutoCAD 2025 新機能</a></p>
<p><strong>図面ファイル形式</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html" rel="noopener noreferrer" target="_blank"></a>AutoCAD 2025、AutoCAD LT 2025 では、引き続き、<strong>2018 図面ファイル形式</strong>&#0160;を採用しています。新規図面を作成して保存する際には、この 2018 図面ファイル形式が既定値となります。</p>
<p style="padding-left: 40px;">もちろん、旧バージョンの図面ファイル形式を開いたり、保存したりする機能も従来通りです。</p>
<p style="padding-left: 40px;"><strong>図面読み込み：</strong></p>
<ul>
<li>
<ul>
<li>すべての AutoCAD バージョンで作成した DWG ファイル （<a href="https://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html" rel="noopener" target="_blank">非TrustedDWG</a>&#0160;を除く）</li>
<li>すべての AutoCAD バージョンで作成した DXF ファイル</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;"><strong>図面の保存：</strong></p>
<ul>
<li>
<ul>
<li>R14, 2000, 2004, 2007, 2010, 2013, 2018 形式の DWG ファイル</li>
<li>R12, 2000, 2004, 2007, 2010, 2013, 2018&#0160; 形式の DXF ファイル</li>
</ul>
</li>
</ul>
<p><strong>アクション マクロの互換性</strong></p>
<p style="padding-left: 40px;">アクション マクロを保存する .actm ファイルが .actmx ファイルに更新されています。AutoCAD 2025 で既存のアクション マクロ資産を再生するには、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-8D38A466-DD1E-454C-9855-A78848BF1745" rel="noopener" target="_blank">[アクション マクロ管理] ダイアログ</a>で .actmx ファイル形式に毎グレートする必要があります。AutoCAD 2025 で新しく記録するアクション マクロは、.actmx ファイル形式で格納されます。</p>
<p style="padding-left: 40px;">なお、.actmx ファイルは、AutoCAD 2024 以前のリリースと互換性がありません。もし、旧バージョンでもアクション マクロを使う必要がある場合には、マイグレート前に .actm ファイルのバックアップを取っておくことをお勧めします。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af6414200b-pi" style="display: inline;"><img alt="Action_macro" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af6414200b img-responsive" src="/assets/image_626268.jpg" title="Action_macro" /></a></p>
<p><strong>アドイン アプリケーションの互換性</strong></p>
<p style="padding-left: 40px;">AutoCAD 2025 は、AutoLISP/Visual LISP、ActiveX オートメーション（COM）、ObjectARX、.NET API、JavaScript API の 5 &#0160;つの AutoCAD API をサポートします。前バージョンの AutoCAD 2024 からは <strong>バイナリ<span style="text-decoration: underline;">非互換</span>リリース&#0160;</strong>となるため、同バージョン用に作成されたアドイン アプリケーションは、使用する API によって移植作業が必要になる場合があります。AutoLISP（.lsp）など、従来ファイルをそのままロード出来た場合でも、綿密に動作チェックされることをお勧めしています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b05056200b-pi" style="display: inline;"><img alt="Autocad_gokan" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b05056200b image-full img-responsive" src="/assets/image_211475.jpg" title="Autocad_gokan" /></a></p>
<p style="padding-left: 40px;">過去バージョンと、その前バージョンからの互換状況、また、移植に必要となる基本情報は次のとおりです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab909b200c-pi" style="display: inline;"><img alt="Migration_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab909b200c image-full img-responsive" src="/assets/image_553314.jpg" title="Migration_info" /></a></p>
<p><strong>サポート コンパイラ</strong></p>
<p style="padding-left: 40px;">ObjectARX と .NET API でお使いいただくコンパイラは、Visual Studio 2022 17.8.0 以降をサポートしています（ObjectARX は Visual Studio 2022 17.8.0 以降）。Visual Studio 2022 のインストール時には、多数のインストール オプションの指定が必要です。ObjectARX を使った開発をする場合には「<strong>C++ によるデスクトップ開発</strong>」を、.NET API を使った開発をする場合には「<strong>.NET デスクトップ開発</strong>」をそれぞれ選択してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af5e7e200b-pi" style="display: inline;"><img alt="Vs_installer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af5e7e200b image-full img-responsive" src="/assets/image_753524.jpg" title="Vs_installer" /></a></p>
<p><strong>ObjectARX</strong></p>
<p style="padding-left: 40px;">前バージョンの AutoCAD 2024 用に作成したアドイン アプリケーションをお持ちの場合には、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad/objectarx&#0160;</a></strong>から ObjectARX SDK for AutoCAD 2025 をダウンロード、参照して新しい開発環境となる <strong>Visual Studio 2022&#0160;</strong>で再ビルドする必要がありますす。それ以前の既存プロジェクトの移植では、この作業が必須となります。プロジェクトに設定する「プラットフォーム ツールセット」は <strong>Visual Studio 2022(v143)</strong>&#0160;になります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180d40b200b-pi"><img alt="Platform_toolset" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180d40b200b image-full img-responsive" src="/assets/image_202181.jpg" title="Platform_toolset" /></a></p>
<p style="padding-left: 40px;">また、リンクするスタティック リンク ライブラリは、ObjectARX SDK for AutoCAD 2025 の <strong>*</strong><strong>25.lib</strong>&#0160;に変更してください。</p>
<p style="padding-left: 40px;">ObjectARX でカスタム オブジェクトを定義していて、COM サーバーとしてオブジェクト、メソッド、プロパティを COM で公開している場合には、.idl ファイルでインポートしているタイプライブラリも&#0160;<strong>acax25enu.tlb</strong>&#0160;ないし、<strong>acax25jpn.tlb</strong>&#0160;に置き換える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスや関数については、ObjectARX SDK for AutoCAD 2025 の docs フォルダの <strong>Reference Guide（arxref.chm）</strong>から<strong>&#0160;ObjectARX Migration Guide</strong>&#0160;セクション、または、<strong><a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=OARX-RefGuide-ObjectARX_Migration_Guide" rel="noopener" target="_blank">オンラインヘルプ</a></strong>&#0160;をご確認ください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afad5b200d-pi" style="display: inline;"><img alt="Oarx_migration_guide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3afad5b200d image-full img-responsive" src="/assets/image_265032.jpg" title="Oarx_migration_guide" /></a></p>
<p style="padding-left: 40px;">以前の AutoCAD からの互換情報については、AutoCAD 2025 のオンライン ヘルプ、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-C21B8F00-C7DE-4E44-8006-D5DC99199F31" rel="noopener" target="_blank"><strong>ObjectARX の互換性</strong></a>&#0160;もご確認ください。</p>
<p style="padding-left: 40px;">AutoCAD 2025 用の ObjectARX Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> ページ下部からダウンロードすることが出来ます。詳細は、<strong><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/objectarx-objectarx-wizard-noinsutoru/ta-p/12921563" rel="noopener" target="_blank">ObjectARX：ObjectARX Wizard のインストール</a></strong><strong>&#0160;</strong>&#0160;をご確認ください。</p>
<p style="padding-left: 40px;">また、正しくインストールされた&#0160; ObjectARX Wizard を使用した場合でも、Wizardの [MFC Support] 画面で&#0160;<strong>Extension DLL using MFC shared DLL(recommended for MFC support)</strong>&#0160;オプションにチェックしてプロジェクトを作成した場合、プロジェクト作成直後に作成されたプロジェクトがロードされず、[新しいプロジェクト] ダイアログが再度表示されてしまう場合には、お使いの Visual Studio 2022 に MFC コンポーネントがインストールされていない可能性があります。MFC コンポーネントは､コントロール パネル &gt;&gt; プログラムのアンインストール から、<strong>Microsoft Visual Studio Installer</strong>、または、<strong>Visual Studio 2022</strong>&#0160;を選択後、「<strong>変更</strong>」をクリックすると、Visual Studio 2022 のインストール後でも確認やインストール指示が可能です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afad6f200d-pi" style="display: inline;"><img alt="C++_mfc_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3afad6f200d image-full img-responsive" src="/assets/image_513107.jpg" title="C++_mfc_install" /></a></p>
<p><strong>.NET API</strong></p>
<p style="padding-left: 40px;">サポートされる .NET は .<strong>NET&#0160; 8.0</strong> です。前バージョンの AutoCAD 2024 用に作成したアドイン アプリケーション用の Visual Studio プロジェクトをお持ちの場合には、プロジェクトをアップグレード後、AutoCAD 2025 のアセンブリ ファイルを参照して再ビルドが必要です。 具体的な手順は、次の記事でご案内しています。</p>
<p style="padding-left: 80px;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2024/04/autocad-2025-dotnet8-migration.html%20" rel="noopener" target="_blank">AutoCAD 2025 .NET 8 へのアドイン移植</a></strong></p>
<p style="padding-left: 40px;">&#0160;.NET 8.0 への移行には、Visual Studio Installer から、Visual Studio 2022 の [個別のコンポーネント] で「.NET 8.0 Runtime (Long Term Support)」を先にインストールしておく必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af60cf200b-pi" style="display: inline;"><img alt="Net8_runtime_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af60cf200b image-full img-responsive" src="/assets/image_859027.jpg" title="Net8_runtime_install" /></a></p>
<p style="padding-left: 40px;">一部のクラスやメソッド、プロパティが変更されている場合がありますので、ソースコードに適切な変更を加える必要があります。</p>
<p style="padding-left: 40px;">廃止、変更されたクラスやメソッド、プロパティについては、ObjectARX SDK for AutoCAD 2025 の docs フォルダの <strong>Managed Class Reference Guide（arxmgd.chm）</strong>から&#0160;<strong>.NET Migration Guide</strong>&#0160;セクションセクション、または、<strong><a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=OARX-ManagedRefGuide-_NET_Migration_Guide" rel="noopener" target="_blank">オンラインヘルプ&#0160;</a></strong>をご確認ください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afafe7200d-pi" style="display: inline;"><img alt="Net_migration_guide" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3afafe7200d image-full img-responsive" src="/assets/image_761960.jpg" title="Net_migration_guide" /></a></p>
<p style="padding-left: 40px;">なお、.NET API の開発者用ガイドは、AutoCAD 2025 のオンライン ヘルプ内で <strong><a href="http://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener noreferrer" target="_blank">Managed .NET 開発者用ガイド(.NET)</a></strong>&#0160;として日本語化されたドキュメントを参照することが出来ます。</p>
<p style="padding-left: 40px;">以前の AutoCAD からの互換情報については、同じく、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-A6C680F2-DE2E-418A-A182-E4884073338A" rel="noopener" target="_blank"><strong>Managed .NET の互換性</strong></a>&#0160;もご確認ください。</p>
<p style="padding-left: 40px;">AutoCAD 2025 用の .NET Wizard は、<strong><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a></strong> ページ下部からダウンロードすることが出来ます。詳細は、<strong><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-net-8-dui-ying-autocad-2025-yong-net-api-wizard/ta-p/12738877" rel="noopener" target="_blank">AutoCAD .NET API：.NET 8 対応 AutoCAD 2025 用 .NET API Wizard</a></strong>&#0160;をご確認ください。</p>
<p><strong>ActiveX オートメーション（COM）</strong></p>
<p style="padding-left: 40px;">前バージョン用に作成されたアドイン アプリケーションは、そのままロードして実行できるはずです。こちらも、可能であれば、新しいバージョンのタイプライブラリを参照しなおしてテストすることをお勧めします。以前の AutoCAD からの互換情報を含め、タイプライブラリの詳細は、AutoCAD 2025 のオンライン ヘルプ、<strong><a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-927E71C2-E515-438E-9D7A-246D97BEF93F" rel="noopener noreferrer" target="_blank">VBA と ActiveX の互換性</a></strong>&#0160;をご確認ください。</p>
<p style="padding-left: 40px;">VBA をお使いの場合、VBA コンポーネントは<strong> <a href="http://www.autodesk.com/vba-download-jpn" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/vba-download-jpn</a></strong>&#0160;から参照可能な Autodesk &#0160;Knowledge Network 記事からダウンロードすることが出来ます。</p>
<p><strong>AutoLISP</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/04/autolisp-development-using-vs-code.html" rel="noopener" target="_blank"><strong>Visual Studio Code での AutoLISP 開発</strong>&#0160;</a>でご案内のとおり、AutoCAD 2025 でも、従来の Visual LISP エディタが Visual Studio Code に置き換えられていますのでご注意ください。AutoCAD 2025 でも <a class="xref" href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-1A8B50AA-1DEA-4853-AAA8-09AF0827A0ED" rel="noopener" target="_blank">MAKELISPAPP[LISP アプリを作成]</a>&#0160;コマンドを使って、配布に適したアプリケーション ファイル（.vlx ファイル）にコンパイルすることが出来ます。コンパイル時には、複数の AutoLISP ファイル（.lsp ファイル）を 1 つの .vlx ファイルにすることが出来るだけでなく、同時にバイナリ ファイル化されるので、ソース コードを保護することも出来ます。なお、コンパイル時には、従来通り、ウィザードが用意されています。</p>
<p style="padding-left: 40px;">以前の AuoCAD からの互換情報については、AutoCAD 2025 のオンライン ヘルプ、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-31FD1A96-C002-434E-8684-63D50BE0CF94" rel="noopener" target="_blank"><strong>AutoLISP の互換性</strong></a>&#0160;もご確認ください。</p>
<p><strong>JavaScript</strong></p>
<p style="padding-left: 40px;">JavaScript ライブラリには変更はありませんので移植作業は不要です。</p>
<p style="padding-left: 40px;">その他、アドイン アプリケーションの互換性に関する情報は、AutoCAD 2025 の<strong><a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-D54B0935-1638-4F97-8B37-1EC3635A1E71" rel="noopener noreferrer" target="_blank">オンライン ヘルプ</a></strong>をご参照ください。&#0160;&#0160;</p>
<hr />
<p><strong>AutoCAD .NET API の参照アセンブリ</strong></p>
<p style="padding-left: 40px;">以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html" rel="noopener noreferrer" target="_blank">NuGet と AutoCAD.NET API</a></strong> のブログ記事でもご案内したのと同様に、AutoCAD 2025 上で .NET API で使用する際に参照するアセンブリは、Visual Studio 上の NuGet パッケージ マネージャからオンラインで入手することが出来ます。</p>
<p style="padding-left: 40px;"><strong><a href="https://www.nuget.org/packages/AutoCAD.NET/" rel="noopener" target="_blank">https://www.nuget.org/packages/AutoCAD.NET/</a></strong>&#0160;&#0160;</p>
<p>By Toshiaki Isezaki</p>
