---
layout: "post"
title: "AutoCAD I/O サンプル"
date: "2015-02-25 01:07:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/autocad-io-sample.html "
typepad_basename: "autocad-io-sample"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：AutoCAD I/O&#0160;は2016年6月に Design Automation API &#0160;に名称変更されました。</span></p>
<p>以前、<a href="http://adndevblog.typepad.com/technology_perspective/2014/12/autocad-io-web-service.html" target="_blank"><strong>ご案内</strong></a>した AutoCAD I/O サービスについて、分かり易いサンプルがありますので、ここで内容をご紹介しましょう。</p>
<p>AutoCAD I/O サービス API のドキュメントやキーコードの取得、サンプルへは、View and Data API と同じ&#0160;<strong>デベロッパ ポータル</strong>（<a href="http://developer.autodesk.com/" target="_blank"><strong>http://developer.autodesk.com</strong></a>）からアクセスすることが出来ます。実際のサンプルの記載場所は、こちらも&#0160;View and Data API サンプルと同様の <a href="https://github.com/Developer-Autodesk/AutoCAD.io" target="_blank"><strong>GitHub</strong></a> となります。</p>
<p>まず最初に、この中から最も単純な簡単な <a data-branch="master" data-direction="back" data-pjax="true" href="https://github.com/Developer-Autodesk/workflow-simplest-autocad.io">workflow-simplest-autocad.io</a>&#0160;サンプルを取り上げてみたいと思います。このサンプルは、Autodesk Knowledge Network ページにある AutoCADのサンプル図面ファイルのリンクを利用して、DWG ファイルを PDF ファイルに変換する Windows コンソールアプリケーションです。C# で記述されたコードは短いコードで、AutoCAD I/O の動きを把握するのに役立つはずです。</p>
<p>サンプル プロジェクトをダウンロードしたら、プロジェクトを開いて NuGet パッケージ マネージャを使って参照設定を修復します。この方法は、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample2.html" target="_blank">View and Data API サンプル</a>や <a href="http://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html" target="_blank">AutoCAD 2015 .NET API アセンブリの参照設定</a>でもご紹介していますので、初めての方は、そちらをご参照ください。</p>
<p>もちろん、サンプルを実行する前には、デベロッパ ポータルから AutoCAD I/O API 用のキー（Consumer Key と Consumer Secret）を事前に取得する必要があります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c742e82d970b-pi" style="display: inline;"><img alt="Retreive_key" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c742e82d970b image-full img-responsive" src="/assets/image_777363.jpg" title="Retreive_key" /></a></p>
<p style="text-align: left;">キーの取得が出来たら、Program.cs 内の冒頭で対応する変数に&#0160;Consumer Key と Consumer Secret を代入設定してビルドするだけです。実際の処理内容を動画にしていますので、ご参照ください。PDF ファイル化の処理は、REST API を介してクラウド上で処理されています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/WfZCf7Qe4yE?feature=oembed" width="500"></iframe>&#0160;</p>
<p>AutoCAD I/O Web サービスでは、新規に DWG ファイルを作成することも出来ます。GitHub で公開されている別のサンプル&#0160;<a href="https://github.com/Developer-Autodesk/workflow-aspdotnet-autocad.io" target="_blank">workflow-aspdotnet-autocad.io</a> では、Web ページ上に入力したクローゼットの各種寸法から、AutoCAD I/O API を使って指定された大きさで新しい DWG ファイルを生成、View and Data API で生成された図面を表示する例を参照することが出来ます。また、このサンプルでは生成された DWG 図面を指定されたメール アドレスまで送信する処理も実現しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/9SkicByqttU?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>AutoCAD I/O はソフトウェア ライセンス使用許諾に違反せずに、クラウド上の AutoCAD を起動してバッチ処理をおこなうことが出来る新しいカスタマイズ手法です。まだ、Beta 扱いの Web サービス API ですが、今後のカスタマイズで検討していただくこともお勧めします。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
