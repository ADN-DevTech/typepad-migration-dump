---
layout: "post"
title: "AutoCAD 2016 VBA の VB.Netへのマイグレーション手順の公開"
date: "2015-04-06 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/autocad-2016-vba-to-dotnet-migration.html "
typepad_basename: "autocad-2016-vba-to-dotnet-migration"
typepad_status: "Publish"
---

<p>過去の AutoCAD バージョン製品内で稼働済みの VBA マクロファイルを、AutoCAD 2016 製品上で実行可能な VB.Net カスタムコマンドとして変換するための Visual Studio 2012 開発環境を使用した マイグレーション手順を公開します。</p>

<p>これは、「<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9622">QA-9622 AutoCAD 2016 VBA の VB.NET マイグレーション手順</a>」 としても掲載されています。</p>

<p>Microsoft社は Visual Studio 2010 開発環境発表後 Visual Studio 2012 開発環境においても Visual Basic 6.0 のプロジェクト (.vbp ) の読み込み、および、自動変換をサポートしなくなりました。</p>

<p>一方、AutoCAD 2013 製品まで VBA の IDE 環境向けにリリースしておりました「マイグレーションツールである”マジックマクロ”」が現時点では AutoCAD 2016 向けにリリースされておらず、そのため、VBAマクロをAutoCAD 2016 VB.NET環境のカスタムコマンドにマイグレーションするには、マジックマクロなどで目的のVBAマクロを、一旦Visual Basic 6.0プロジェクト( .vbp )に変換したのちに、一度 Visual Studio 2008 までの Visual Studio 開発環境などを使い、旧Visual Studio のプロジェクト( . vbproj )環境に変換させ、最終的に移行先の AutoCAD 2015 .NET 開発環境である Visual Studio 2012 + Update4 環境に読み込んで、マイグレーションをする必要があります。</p>

<p>AutoCAD 2016 向けにマイグレーションする際に必要な環境：</p>

<p>1.AutoCAD 2013以前の製品<br />
2.AutoCAD 2013以前の製品用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
    既にダウンロードし、インストールしておられます VBA イネーブラーをお使いください。<br />
    ( AutoCAD 2013 製品以前の VBA イネーブラーは新たにダウンロードする事はできません。<br />
    尚、過去、ダウンロードし現在お使いいただいております、既存のイネーブラーは継続してインストールしてお使いいただけます。 )<br />
3.AutoCAD 2016<br />
4.AutoCAD 2016用 VBAイネーブラー のインストール( OSに従いどちらか一方 )    <br />
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb0815791f970d img-responsive"><a href="http://adndevblog.typepad.com/files/autocad_2016_acvbainstaller_win_32bit_dlm.sfx.exe">AutoCAD_2016_AcVbaInstaller_Win_32bit_dlm.sfx.exe ( 32Bit用 )をダウンロード</a></span>    <br />
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7717e93970b img-responsive"><a href="http://adndevblog.typepad.com/files/autocad_2016_acvbainstaller_win_64bit_dlm.sfx.exe">AutoCAD_2016_AcVbaInstaller_Win_64bit_dlm.sfx.exe ( 64Bit用 )をダウンロード</a></span></p>

<p>    (VBAIDE 環境は AutoCAD2015 32/64ビット共にVBAは7.1 であり、過去の64Bitバージョンのようなエミュレート動作では無く、64Bitネイティブな動作となります）<br />
5.マイグレーションツール( AutoCAD2013用 マジックマクロ ) <br />
    VBA_to_VB6_Converter2013.dvb  (AutoCAD 2013以前の AutoCAD 製品上でも動作可能。)<br />
6.<a href="http://go.microsoft.com/?LinkId=9348306">Microsoft Visual Basic 2008 Express Edition</a> のインストール<br />
       ( Microsoft 社のサイト内 ISOイメージダウンロード)<br />
7.<a href="http://www.microsoft.com/ja-jp/download/details.aspx?id=34673">Visual Studio Express 2012 for Windows Desktop</a> のインストール<br />
      （Microsoft 社のサイト内 ダウンロード先）<br />
8.ObjectARX 2013 SDK のインストール    <br />
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7717e9f970b img-responsive"><a href="http://adndevblog.typepad.com/files/objectarx_2013_win_64_and_32bit.exe">Objectarx_2013_win_64_and_32bit.exe をダウンロード</a></span></p>

<p>9.ObjectARX 2015 SDK のインストール<br />
    http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550</p>

<p>尚、QA-9622　で公開している情報は、Microsoft社が「評価目的にのみ許可」しております、各種 無償版のVisual Studio Express バージョンを使用してのご案内となります。</p>

<p>QA-9622 で公開している資料は、ここからも <a href="http://tech.autodesk.jp/faq/file/ACAD2016VBAMigration.zip">ACAD2016VBAMigration.zip</a> としてダウンロードいただけます。</p>

<p><br />
AutoCAD 2014 /2015 向けの以下の情報も参考にしていただければ幸いです。</p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2014/08/autocad-2015-vba-to-dotnet-migration.html">AutoCAD 2015 VBA の VB.NETマイグレーション手順 </a></p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/autocad-2014-vba2dotnet_migration.html">AutoCAD 2014 VBA の VB.NETマイグレーション手順</a> </p>

<p><br />
<iframe width="459" height="344" src="http://www.youtube.com/embed/8ajCS0WzPjw?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;</p>

<p>By Shigekazu Saito<br />
</p>
