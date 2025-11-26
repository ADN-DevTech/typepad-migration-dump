---
layout: "post"
title: "AutoCAD 2015 VBA の VB.Netへのマイグレーション手順の公開"
date: "2014-08-25 01:54:09"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/08/autocad-2015-vba-to-dotnet-migration.html "
typepad_basename: "autocad-2015-vba-to-dotnet-migration"
typepad_status: "Publish"
---

<p>過去の <a class="zem_slink" href="http://construction-project-management-software.findthebest.com/l/70/AutoCAD" title="AutoCAD" rel="fdbsoftware" target="_blank">AutoCAD</a> バージョン製品内で稼働済みの VBA マクロファイルを、AutoCAD 2015 製品上で実行可能な VB.Net カスタムコマンドとして変換するための Visual Studio 2012 開発環境を使用した マイグレーション手順を公開します。</p>

<p>これは、<span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d1b3d888970c img-responsive"><a href="http://adndevblog.typepad.com/files/qa-9181-1.pdf">「QA-9181 AutoCAD 2015 VBA の VB.NET マイグレーション手順.pdf」</a></span> としても掲載されています。</p>

<p>Microsoft社は Visual Studio 2010 開発環境発表後 Visual Studio 2012 開発環境においても Visual Basic 6.0 のプロジェクト (.vbp ) の読み込み、および、自動変換をサポートしなくなりました。</p>

<p>一方、AutoCAD 2013 製品まで VBA の IDE 環境向けにリリースしておりました「マイグレーションツールである”マジックマクロ”」が現時点では AutoCAD 2015 向けにリリースされておらず、そのため、VBAマクロをAutoCAD 2015 VB.NET環境のカスタムコマンドにマイグレーションするには、マジックマクロなどで目的のVBAマクロを、一旦Visual Basic 6.0プロジェクト( .vbp )に変換したのちに、一度 Visual Studio 2008 までの Visual Studio 開発環境などを使い、旧Visual Studio のプロジェクト( . vbproj )環境に変換させ、最終的に移行先の AutoCAD 2015 .NET 開発環境である Visual Studio 2012 + Update4 環境に読み込んで、マイグレーションをする必要があります。</p>

<p><strong>AutoCAD 2015 向けにマイグレーションする際に必要な環境：</strong></p>

<p>1.AutoCAD 2013以前の製品<br />
2.AutoCAD 2013以前の製品用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
    既にダウンロードし、インストールしておられます VBA イネーブラーをお使いください。<br />
    ( AutoCAD 2013 製品以前の VBA イネーブラーは新たにダウンロードする事はできません。<br />
    尚、過去、ダウンロードし現在お使いいただいております、既存のイネーブラーは継続してインストールしてお使いいただけます。 )<br />
3.AutoCAD 2015<br />
4.AutoCAD 2015用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
    <a href="http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/download-the-microsoft-visual-basic-for-applications-module.html">AutoCAD_2015_AcVbaInstaller_English_Win_32bit.sfx.exe ( 32Bit用 )</a><br />
    <a href="http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/download-the-microsoft-visual-basic-for-applications-module.html">AutoCAD_2015_AcVbaInstaller_English_Win_64bit.sfx.exe ( 64Bit用 )</a><br />
    (VBAIDE 環境は AutoCAD2015 32/64ビット共にVBAは7.1 であり、過去の64Bitバージョンのようなエミュレート動作では無く、64Bitネイティブな動作となります）<br />
5.マイグレーションツール( AutoCAD2013用 マジックマクロ ) <br />
    VBA_to_VB6_Converter2013.dvb  (AutoCAD 2013以前の AutoCAD 製品上でも動作可能。)<br />
6.Microsoft Visual Basic 2008 Express Edition のインストール<br />
    <a href="http://go.microsoft.com/?LinkId=9348306">VS2008ExpreessWithSP1JPNX1504866.iso</a>  （Microsoft 社のサイト内 ダウンロード先）<br />
7.Visual Studio Express 2012 for Windows Desktop のインストール<br />
    <a href="http://www.microsoft.com/ja-jp/download/details.aspx?id=34673">Visual Studio Express 2012 for Windows Desktop</a> （Microsoft 社のサイト内 ダウンロード先）  <br />
8.ObjectARX 2013 SDK のインストール<br />
    <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=785550">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=785550</a><br />
9.ObjectARX 2015 SDK のインストール<br />
    <a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=785550">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&amp;id=785550</a></p>

<p>尚、QA-9181　で公開している情報は、Microsoft社が「評価目的にのみ許可」しております、各種 無償版のVisual Studio Express バージョンを使用してのご案内となります。</p>

<p><br />
AutoCAD 2015 VBAのマイグレーション関連のドキュメントやプロジェクト一式はこちらの <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c829502a970b img-responsive"><a href="http://adndevblog.typepad.com/files/acad2015vbamigration.zip">ACAD2015VBAMigration</a></span>から ダウンロードしご利用いただけます。</p>

<p><br />
以下のAutoCAD2014向けの情報もドキュメントを更新しましたので参考にしていただければ幸いです。</p>

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/autocad-2014-vba2dotnet_migration.html">AutoCAD 2014 VBA の VB.NETマイグレーション手順</a> </p>

<p><iframe src="http://www.youtube.com/embed/8ajCS0WzPjw?feature=oembed" allowfullscreen="" frameborder="0" height="344" width="459"></iframe>&nbsp;</p>

<p>By Shigekazu Saito</p>
