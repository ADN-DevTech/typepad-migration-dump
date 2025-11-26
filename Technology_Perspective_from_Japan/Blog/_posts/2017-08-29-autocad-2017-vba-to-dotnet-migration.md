---
layout: "post"
title: "AutoCAD 2017 VBA の VB.Netへのマイグレーション手順の公開"
date: "2017-08-29 03:01:54"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/08/autocad-2017-vba-to-dotnet-migration.html "
typepad_basename: "autocad-2017-vba-to-dotnet-migration"
typepad_status: "Publish"
---

<p>過去の AutoCAD バージョン製品内で稼働済みの VBA マクロファイルを、AutoCAD 2017 製品上で実行可能な VB.Net カスタムコマンドとして変換するための Visual Studio 2015 開発環境を使用した マイグレーション手順を公開します。</p>

<p>Microsoft社は Visual Studio 2010 開発環境発表後 Visual Studio 2015 開発環境においても Visual Basic 6.0 のプロジェクト (.vbp ) の読み込み、および、自動変換をサポートしなくなりました。</p>

<p>一方、AutoCAD 2013 製品まで VBA の IDE 環境向けにリリースしておりました「マイグレーションツールである”マジックマクロ”」が現時点でも AutoCAD 2017 向けにリリースされておらず、そのため、VBAマクロをAutoCAD 2017 VB.NET環境のカスタムコマンドにマイグレーションするには、マジックマクロなどで目的のVBAマクロを、一旦Visual Basic 6.0プロジェクト( .vbp )に変換したのちに、一度 Visual Studio 2008 までの Visual Studio 開発環境などを使い、旧Visual Studio のプロジェクト( . vbproj )環境に変換させ、最終的に移行先の AutoCAD 2017 .NET 開発環境である Visual Studio 2015 + Update1 環境に読み込んで、マイグレーションをする必要があります。</p>

<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb09bde38c970d img-responsive"><a href="http://adndevblog.typepad.com/files/acad2017vbamigration.zip">ACAD2017VBAMigrationをダウンロード</a></span></p>

<p><strong>AutoCAD 2017 向けにマイグレーションする際に必要な環境：</strong></p>

<p>1.AutoCAD 2013以前の製品</p>

<p>2.AutoCAD 2013以前の製品用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
    既にダウンロードし、インストールしておられます VBA イネーブラーをお使いください。<br />
    ( AutoCAD 2013 製品以前の VBA イネーブラーは新たにダウンロードする事はできません。<br />
    尚、過去、ダウンロードし現在お使いいただいております、既存のイネーブラーは継続してインストールしてお使いいただけます。 )</p>

<p>3.AutoCAD 2017</p>

<p>4.<a href="http://www.autodesk.com/vba-download">AutoCAD 2017用 VBAイネーブラー</a> のインストール( OSに従いどちらか一方 )   </p>

<p>　AutoCAD_2017_VBA_module_Win_32bit_dlm.sfx.exe ( 32Bit用 )をダウンロード<br />
　AutoCAD_2017_VBA_module_Win_64bit_dlm.sfx.exe ( 64Bit用 )をダウンロード<br />
    (VBAIDE 環境は AutoCAD2017 32/64ビット共にVBAは7.1 であり、64Bitネイティブな動作となります）</p>

<p>5.マイグレーションツール( AutoCAD2013用 マジックマクロ ) <br />
    VBA_to_VB6_Converter2013.dvb  (AutoCAD 2013以前の AutoCAD 製品上でも動作可能。)</p>

<p>6.<a href="http://go.microsoft.com/?LinkId=9348306">Microsoft Visual Basic 2008 Express Edition</a> のインストール<br />
       ( Microsoft 社のサイト内 ISOイメージダウンロード)</p>

<p>7.Visual Studio 2015 + Update1</p>

<p>　「<a href="http://adndevblog.typepad.com/technology_perspective/2015/05/autocad-and-net-framework-version.html">AutoCAD と対応する .NET Framework バージョン</a>」を参考にしてください。</p>

<p>8.ObjectARX 2013 SDK のインストール    <br />
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7717e9f970b img-responsive"><a href="http://adndevblog.typepad.com/files/objectarx_2013_win_64_and_32bit.exe">Objectarx_2013_win_64_and_32bit.exe をダウンロード</a></span></p>

<p>9.<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550">ObjectARX 2017 SDK </a>のインストール    </p>

<p><br />
AutoCAD 2014 /2015 / 2016 向けの以下の情報も参考にしていただければ幸いです。</p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/autocad-2016-vba-to-dotnet-migration.html">AutoCAD 2016 VBA の VB.Netへのマイグレーション手順の公開</a></p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2014/08/autocad-2015-vba-to-dotnet-migration.html">AutoCAD 2015 VBA の VB.NETマイグレーション手順 </a></p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/autocad-2014-vba2dotnet_migration.html">AutoCAD 2014 VBA の VB.NETマイグレーション手順</a> </p>

<p><br />
<iframe width="459" height="344" src="http://www.youtube.com/embed/8ajCS0WzPjw?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;</p>

<p>By Shigekazu Saito</p>
