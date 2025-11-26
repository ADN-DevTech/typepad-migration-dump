---
layout: "post"
title: "ObjectARX の開発とデバッグ環境"
date: "2015-08-03 02:22:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/objectarx-development-and-debug-environment.html "
typepad_basename: "objectarx-development-and-debug-environment"
typepad_status: "Publish"
---

<p>AutoCAD API の 1 つである ObjectARX は、カスタム オブジェクトの定義を含め、AutoCAD カスタマイズで最も強力な API です。その登場は1996 年初頭にリリースされた AutoCAD R13 C4 からで、すでに 20 年弱の歴史があります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13defa3970c-pi" style="display: inline;"><img alt="Autocad_api_comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13defa3970c image-full img-responsive" src="/assets/image_53563.jpg" title="Autocad_api_comparison" /></a></p>
<p>一見すると、ObjectARX が万能なような印象を受けますが、開発環境や効率を考えた場合、単純にそうとも言い切れません。</p>
<p><strong>ObjectARX アプリケーションの原則</strong></p>
<p style="padding-left: 30px;">ObjectARX アプリケーション（.arx、.dbx、.crx ファイル）は、AutoCAD とプロセスやメモリ空間を共有する DLL ファイルの拡張子を変更したものです。32 ビット、64 ビット毎に異なる実行ファイルを持つ AutoCAD にロードして実行させるためには、32 ビット AutoCAD 用と 64 ビット AutoCAD 用に、異なる実行形式を用意する必要があります。</p>
<p style="padding-left: 30px;">32 ビット AutoCAD に 64 ビット ObjectARX アプリケーションをロードしたり、64&#0160;ビット AutoCAD に 32&#0160;ビット ObjectARX アプリケーションをロードしたりすることは出来ません。</p>
<p><strong>ObjectARX の歴史的背景</strong></p>
<p style="padding-left: 30px;">ObjectARX の登場当時、当然、.NET Framework は存在していませんでした。 このため、ObjectARX で使用する言語環境は、.NET Framework に依存しない&#0160;Unmanaged C++ で、現在でも変わりありません。.NET Framework の利点は、過去のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/12/benefit-by-autocad-dotnet-api.html" target="_blank"><strong>AutoCAD .NET API の利点</strong></a> で触れていますが、重要なのはプラットフォームに依存しない点です。</p>
<p style="padding-left: 30px;">AutoCAD .NET API では、生成される DLL ファイルはあくまで中間ファイルで、実行時にはじて JIT コンパイラがプラットフォーム差に応じた実行形式にコンパイルされるため、開発者が 32 ビット AutoCAD 用の開発、64 ビット AutoCAD &#0160;用の開発で、差異を特に意識する必要はありません。</p>
<p style="padding-left: 30px;">逆に、.NET Framework を利用しない&#0160;ObjectARX では、32 ビット AutoCAD 用の開発、64 ビット AutoCAD 用の開発を意識しなければなりません。1 つの Visual Studio プロジェクトですべてのプラットフォームをカバーするためには、ObjectARX のビルドとデバッグ（単なる実行テストも含む）に、一定の環境設定や準備が必要となります。</p>
<p><strong>ビルド</strong></p>
<p style="padding-left: 30px;">開発に利用する Visual Studio 自体は 32 ビットアプリケーションであるため、Visual Studio 自体を 32 ビット版 Windows と&#0160;64 ビット版 Windows の両方にインストールして利用することが可能です。32 ビット アプリケーションを 64 ビット Windows で実行できるようにしているのが、Windows に搭載されている&#0160;<strong><a href="https://ja.wikipedia.org/wiki/WOW64" target="_blank">WOW64</a>&#0160;</strong>という仕組みです。<br /> <br /><a href="http://www.autodesk.com/objectarx" target="_blank">ObjectARX SDK</a> には、32 ビット用のビルドに用いるヘッダーとライブラリ ファイルが、プラットフォーム共通の inc フォルダとは別に、それぞれ、inc-win32 フォルダ、lib-win32 フォルダに用意されております。同様に、64 ビット用のビルドに用いるヘッダーとライブラリ ファイルは、inc-x64 フォルダ、lib-x64 フォルダに用意されています。<br /> <br />Visual Studio プロジェクト内で構成マネージャを使えば、ターゲットとなるプラットフォーム（32 ビット、64 ビット）に応じて、参照するヘッダー ファイル格納フォルダとライブラリ格納フォルダを切り替えることが出来ます。プロジェクトに構成が適切に設定されていれば、64 ビット Windows 上の Visual Studio で 32 ビット AutoCAD 用 ObjectARX アプリケーションをビルドしたり、その逆が可能です。&#0160;</p>
<p style="padding-left: 30px;"><a href="http://www.autodesk.co.jp/developautocad" target="_blank">http://www.autodesk.co.jp/developautocad</a> からダウンロード可能な ObjectARX Wizards for AutoCAD &#0160;を使ってプロジェクトを作成すると、はじめから 32 ビット用の Debug モードと Release モード、また、64 ビット用の &#0160;Debug モードと Release モードの 4 つの構成がプロジェクト内に生成されます。添付画像は、ObjectARX Wizard で作成したプロジェクトのプロパティ画面で、構成切り替えを表示させたものです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13df190970c-pi" style="display: inline;"><img alt="VS Project Properties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13df190970c image-full img-responsive" src="/assets/image_702031.jpg" title="VS Project Properties" /></a></p>
<p><strong>デバッグ</strong></p>
<p style="padding-left: 30px;">32 ビット Windows 上には、どんな 64 ビット アプリケーション（ソフトウェア）もインストールすることは出来ません。AutoCAD の場合には、32 ビット Windows にインストールして実行することが出来るのは、32 ビット AutoCAD のみです。</p>
<p style="padding-left: 30px;">一方、64 ビット Windows の環境では、&#0160;一般的な 32 ビット アプリケーション（ソフトウェア）を、WOW64 で実行せることが出来ます。ところが、AutoCAD の場合は少し状況が異なります。AutoCAD のインストーラは、<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/cpu-and-graphics-card.html" target="_blank">64 ビット Winodws メモリを最大限利用</a>する目的で、64 ビット Windows に 32 ビット AutoCAD をインストールできないようにブロックするようにプログラムされています。</p>
<p style="padding-left: 30px;">64 ビット Windows 上で 32 ビット AutoCAD のインストーラを起動すると、次のようなエラーでインストールが抑止されるはずです。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d13df48b970c-pi" style="display: inline;"><img alt="Error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d13df48b970c img-responsive" src="/assets/image_362332.jpg" title="Error" /></a>&#0160;</p>
<p style="padding-left: 30px;">結果として、64 ビット Windows 上では、32 ビット AutoCAD を実行させることが出来ません。32 ビット ObjectARX アプリケーションのロードと実行には 32 ビット AutoCAD が必須なため、デバッグをおこなうには、 32 ビット Windows に 32 ビット AutoCAD をインストールした環境を用意する必要があります。</p>
<p>&#0160;ここまでの内容をまとめると、次のようになります。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b486d5970b-pi" style="display: inline;"><img alt="Build_and_debug" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b486d5970b image-full img-responsive" src="/assets/image_994145.jpg" title="Build_and_debug" /></a></p>
<p>今後、32 ビット Windows の環境は徐々に減っていくものと思われますが、両方のプラットフォーム用に ObjectARX アプリケーションを開発する場合には、この手間について、考察をお勧めします。</p>
<p>特に大きな理由がない限り、.NET API が最適な選択と言えます。</p>
<p>By Toshiaki Isezaki</p>
