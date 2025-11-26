---
layout: "post"
title: "Inventor API 入門　その４ 外部実行プログラムより Inventor製品 を制御する方法"
date: "2013-03-11 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/03/inventor-api-%E5%85%A5%E9%96%80%E3%81%9D%E3%81%AE%EF%BC%94-%E5%A4%96%E9%83%A8%E5%AE%9F%E8%A1%8C%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%A0%E3%82%88%E3%82%8A-inventor%E8%A3%BD%E5%93%81-%E3%82%92%E5%88%B6%E5%BE%A1%E3%81%99%E3%82%8B%E6%96%B9%E6%B3%95.html "
typepad_basename: "inventor-api-入門その４-外部実行プログラムより-inventor製品-を制御する方法"
typepad_status: "Publish"
---

<p>Inventor製品は、APIを COMのタイプライブラリ「Autodesk Inventor Object Library」として、一般に公開しています。</p>

<p>オブジェクトモデルの pdf は<a href="http://adndevblog.typepad.com/technology_perspective/2013/02/inventor-api-入門.html">以前のポスト</a>よりダウンロードできます。</p>

<p>これは、一般のCOM/ActiveXメカニズムが使える外部の実行環境内にタイプライブラリを参照する事により VBAやアドイン環境同様に「InventorのAPIオブジェクトモデル」を操作できるプログラムの作成が可能な事を意味しています。</p>

<p>今回は、単独で「Windowsフォームアプリケーション」をVisualStudio2010で作成し、外部の実行Exeプログラム内から Inventorを起動してパーツファイルを開き ファイル内の構成にアクセスする方法と、デバックの仕方をお見せします。<br />
<iframe width="500" height="281" src="http://www.youtube.com/embed/JV5N2SlX-Ts?feature=oembed" frameborder="0" allowfullscreen></iframe><br />
(画質は"HD"でご覧いただけます)</p>

<p>次回は、Inventor製品がインストールされていないPC環境で、Inventorファイルの読み出し専用にリリースされている API環境（Apperentice オブジェクトモデル）を使い、プログラムを作成する方法とデバックのビデオの公開を予定しています。<br />
</p>
