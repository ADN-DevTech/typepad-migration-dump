---
layout: "post"
title: "Inventor API 入門　その５ Inventor製品の未インストールPC環境で Inventorファイルを読む方法"
date: "2013-03-18 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/03/inventor-api-%E5%85%A5%E9%96%80%E3%81%9D%E3%81%AE%EF%BC%95-inventor%E8%A3%BD%E5%93%81%E3%81%AE%E6%9C%AA%E3%82%A4%E3%83%B3%E3%82%B9%E3%83%88%E3%83%BC%E3%83%ABpc%E7%92%B0%E5%A2%83%E3%81%A7-inventor%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%92%E8%AA%AD%E3%82%80%E6%96%B9%E6%B3%95.html "
typepad_basename: "inventor-api-入門その５-inventor製品の未インストールpc環境で-inventorファイルを読む方法"
typepad_status: "Publish"
---

<p>Autodesk Inventor View 2013 製品（無償）により、Autodesk Inventor 2013 がインストールされていないＰＣ環境で、ネイティブの Autodesk Inventor 2013 CAD データを表示することができます。</p>

<p>Inventor API では 本体のInventor オブジェクトモデルの サブセットAPIとしてApprentice Serverオブジェクトモデル(アペレンテス サーバー)がリリースされています。</p>

<p>これは、Autodesk Inventor View 2013 製品同様 PC内にAutodesk Inventor 2013製品がインストールされていない環境で、一般のCOM/ActiveXメカニズムが使える外部の実行環境内に 無償のサブセットの Apprentice Serverタイプライブラリを参照する事により 「Apprentice Serverオブジェクトモデル」を使って、Inventorのファイルを 読み込み専用(一部R/W可能なデータ有り)で操作できるプログラムの作成が可能です。</p>

<p>今回は、単独で「Windowsフォームアプリケーション」をVisualStudio2010で作成し、外部の実行Exeプログラム内から Inventorのアセンブリファイルを開き ファイル内のサムネイルとオカレンスの構成情報にアクセスし、フォームに表現する方法と、デバックの仕方をお見せします。<br />
<iframe width="500" height="281" src="http://www.youtube.com/embed/Dhox_Qx4Bc0?feature=oembed" frameborder="0" allowfullscreen></iframe><br />
(画質は"HD"でご覧いただけます)</p>

<p>Autodesk Inventor View 2013は、Autodesk Inventor 2013製品のインストール時にインストールされるほか、<a href="http://www.autodesk.co.jp/adsk/servlet/item?siteID=1169823&id=19583507&linkID=14171176">ここ</a>より別途ダウンロードしインストールする事でご利用できます。</p>
