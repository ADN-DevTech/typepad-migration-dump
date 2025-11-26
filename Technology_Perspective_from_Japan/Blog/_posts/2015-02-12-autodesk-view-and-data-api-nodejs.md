---
layout: "post"
title: "Autodesk View and Data サービス を Node.jsで動かす ~ 1. MEANスタックとは"
date: "2015-02-12 22:47:17"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/autodesk-view-and-data-api-%E3%82%92-nodejs%E3%81%A7%E5%8B%95%E3%81%8B%E3%81%99.html "
typepad_basename: "autodesk-view-and-data-api-を-nodejsで動かす"
typepad_status: "Draft"
---

<p><br />Autodesk View &amp; Data サービスをNode.jsで動かす ～ 1. MEANスタックとは</p>
<p>Webアプリケーションサーバーの開発・実行環境として、これまで広く普及してきた構成には、オープンソースソフトウェアを組み合わせた「 LAMP 」や、マイクロソフト社の IIS と SQL Server を利用したASP .NET、J2EE ( Java Servlet, JSP, EJB ) などが挙げられます。いずれの Web サーバー構成も基本的なアプリケーション構成は共通していますが、プラットフォームや開発言語、フレームワーク等によって使い分けられてきました。<br />昨今では、サーバサイド JavaScript 環境である Node.js を組み合わせた「 MEAN 」という構成が注目されており、Autodesk View &amp; Data Service でも、MEAN 環境向けのサンプルプロジェクトが用意されています。</p>
<p>本連載では、Autodesk View &amp; Data Serviceを「 MEAN スタック」で構築する方法をご紹介いたします。</p>
<p style="padding-left: 30px;">デモサイト<br />http://mongo.autodesk.io/</p>
<p>今回は、まず MEAN スタックについてご説明したいと思います。<br />MEAN とは、LAMP と同じように、Web アプリケーションを構成する技術の頭文字をつなげたものです。</p>
<p style="padding-left: 30px;">MongoDB： ドキュメント指向データベース、NoSQL データベース<br />Express： Node.js 対応の MVC フレームワーク<br />AngularJS： フロントエンドの JavaScript 用 MVW フレームワーク<br />Node.js： サーバーサイド JavaScript の実行環境</p>
<p>http://www.atmarkit.co.jp/ait/articles/1412/01/news041.html</p>
<p>MEANの特徴として、フロントエンドからサーバサイド、データベースのデータ操作までを、単一の開発言語 JavaScript で実行できる点にあります。またクライアントからデータベースまでのデータの受け渡しも、一気通貫してJSONフォーマットを使用することができます。<br />サーバサイドには Node.js 、フロントエンドには Angular.js 、データベースには MongoDB を組み合わせることで、シンプルな開発環境が実現されています。</p>
<p>たとえば Google Cloud Platform のサービスである Google Compute Engine では、この MEAN スタックを、ボタンクリックするだけで簡単にクラウド上にデプロイすることができる「 Click to Deploy 」機能が用意されています。</p>
<p>https://cloud.google.com/solutions/mean/</p>
<p><br />次回からは、4つの技術の解説を交えながら、以下のサンプルプロジェクトのインストールを手順を説明いたします。</p>
<p>integration-mongo-view.and.data.api<br />https://github.com/Developer-Autodesk/integration-mongo-view.and.data.api</p>
