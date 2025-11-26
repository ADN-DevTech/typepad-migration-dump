---
layout: "post"
title: "AutoCAD 2014 VBA の VB.Netへのマイグレーション手順の公開"
date: "2013-06-24 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/06/autocad-2014-vba2dotnet_migration.html "
typepad_basename: "autocad-2014-vba2dotnet_migration"
typepad_status: "Publish"
---

<p>過去のAutoCADバージョン製品内で稼働済みの VBA マクロファイルを、AutoCAD 2014製品上で実行可能な VB.Netカスタムコマンドとして変換するための Visual Studio 2010 開発環境を使用した マイグレーション手順を公開します。</p>

<p><iframe width="459" height="344" src="http://www.youtube.com/embed/8ajCS0WzPjw?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;</p>

<p>これは、「<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=8186">QA-8186 AutoCAD 2014 VBA の VB.NETマイグレーション手順</a>」 としても掲載されています。</p>

<p><br />
Microsoft社は Visual Studio 2010 開発環境発表以来 Visual Basic 6.0 のプロジェクトの読み込み、および、自動変換をサポートしなくなりました。</p>

<p>一方、AutoCAD 2013製品までVBAのIDE環境向けにリリースしておりました「マイグレーションツールである”マジックマクロ”」が現時点ではリリースされておらず、そのため、VBAマクロをAutoCAD 2014 VB.NET環境のカスタムコマンドにマイグレーションするには、一旦AutoCAD2013製品の力を借りて、”マジックマクロ”を使い目的のVBAマクロを "VB6" プロジェクトに変換した後で、Visual Studio 2008までのVisual Studio製品を使い、旧Visual Studioのプロジェクト環境に変換させ、最終的にAutoCAD2014製品 .NETカスタムコマンドの開発環境である Visual Studio 2010 環境 に読み込んで、マイグレーションをする必要があります。</p>

<p><br />
AutoCAD 2014 向けにマイグレーションする際に必要な環境：<br />
1.	AutoCAD2013<br />
2.	AutoCAD2013用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
既にダウンロードし、インストールしておられます VBA イネーブラーをお使いください。<br />
( AutoCAD 2013 製品以前の VBA イネーブラーは新たにダウンロードする事はできません。<br />
尚、過去、ダウンロードし現在お使いいただいております、既存のイネーブラーは継続してインストールしてお使いいただけます。 )<br />
3.	AutoCAD2014<br />
4.	AutoCAD2014用 VBAイネーブラー のインストール( OSに従いどちらか一方 )<br />
<a href="http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/download-the-microsoft-visual-basic-for-applications-module.html">AutoCAD_2014_VBA_Enabler_English_Win_32bit_dlm.sfx.exe</a> ( 32Bit用 )<br />
<a href="http://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/download-the-microsoft-visual-basic-for-applications-module.html">AutoCAD_2014_VBA_Enabler_English_Win_64bit_dlm.sfx.exe</a> ( 64Bit用 )<br />
  (VBAIDE 環境は AutoCAD2014 32/64ビット共にVBAは7.1 であり、過去の64Bitバージョンのようなエミュレート動作では無く、64Bitネイティブな動作となります）<br />
5.	マイグレーションツール( AutoCAD2013用 マジックマクロ )<br />
VBA_to_VB6_Converter2013.dvb <br />
6.	Microsoft Visual Basic 2008 Express Edition のインストール<br />
<a href="http://go.microsoft.com/?LinkId=9348306">VS2008ExpreessWithSP1JPNX1504866.iso</a>  （Microsoft 社のサイト内 ダウンロード先）<br />
7.	Microsoft Visual Basic 2010 Express のインストール<br />
<a href="http://www.visualstudio.com/downloads/download-visual-studio-vs">Microsoft Visual Basic 2010 Express</a>　（ダウンロード先：ページ内最下部)<br />
8.	Microsoft Visual C++ 2010 Express のインストール　<-  ObjectARX 2014 SDKのインストールのみに必須<br />
9.	ObjectARX 2013 SDK のインストール<br />
<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550</a><br />
10.	ObjectARX 2014 SDK のインストール<br />
<a href="http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550">http://usa.autodesk.com/adsk/servlet/item?siteID=123112&id=785550</a></p>

<p><br />
尚、QA-8186　で公開している情報は、Microsoft社が「評価目的にのみ許可」しております、各種 無償版のVisual Studio Express バージョンを使用してのご案内となります。</p>

<p>AutoCAD2014VBAのマイグレーション関連のドキュメントやプロジェクト一式は <span class="asset  asset-generic at-xid-6a0167607c2431970b01a511f9b740970c img-responsive"><a href="http://adndevblog.typepad.com/files/acad2014vbamigration_ver2.zip"> こちらから </a></span> ダウンロードしご利用いただけます。</p>

<p>By Shigekazu Saito</p>
