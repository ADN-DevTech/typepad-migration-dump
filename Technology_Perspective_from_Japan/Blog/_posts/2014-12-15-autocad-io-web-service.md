---
layout: "post"
title: "AutoCAD I/O Web サービス"
date: "2014-12-15 00:35:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/12/autocad-io-web-service.html "
typepad_basename: "autocad-io-web-service"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：AutoCAD I/O&#0160;は2016年6月に Design Automation API &#0160;に名称変更されました。</span></p>
<p>オートデスクのクラウド サービスに、<strong>AutoCAD I/O</strong>&#0160;（Input/Output）という新しいサービスが加わりました。といっても、このサービスは View and Data API と同様、API カスタマイズを前提とした開発者向けのサービスです。</p>
<p>AutoCAD I/O は、多くの方から要望のあった AutoCAD のバッチ処理を実現するための専用サービスです。AutoCAD のバッチ処理でよくあるシナリオでは、入力フォームを持つ Web ページをユーザ インタフェースとして用意し、入力された値によってスクリプトを生成、サーバー内の AutoCAD を起動してスクリプトを実行して図面を成果物として得る、といったものです。ただし、一般に市販されている AutoCAD では、<a href="http://download.autodesk.com/us/FY15/Suites/LSA/ja-JP/lsa.html" target="_blank">Software License Agreement</a>（SLA）&#0160;でサーバーにインストールして不特定多数のユーザからアクセスするような実装を禁止しています。つまり、技術的には可能でも、SLA によって AutoCAD のホスティングによる処理が出来ません。</p>
<p>そこで登場するのが、AutoCAD I/O です。オートデスクが用意するクラウド上で実行される <a href="http://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html" target="_blank"><strong>AcCoreConsole</strong></a> の実行環境を用意して、REST API 呼び出しで、バッチ処理したい図面をアップロードしたり、結果として生成された図面ファイルをダウンロードすることが出来ます。決して、エンドユーザが対面して操作する AutoCAD 360 のようなサービスではありあせん。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c71fbdc8970b-pi" style="display: inline;"><img alt="Autocad_io" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c71fbdc8970b image-full img-responsive" src="/assets/image_293690.jpg" title="Autocad_io" /></a></p>
<p>素材となる図面をアップロードしたり、成果物として図面をダウンロードする際には、3rd party のクラウド ストレージ サービスを利用することも出来ます。もちろん、新規に図面を生成するようなことも可能です。</p>
<p>AutoCAD I/O は実行に AcCoreConsole を用いるため、バッチ処理するスクリプトに、ダイアログボックスなどのユーザ インタフェールを持たない ObjectARX アプリケーション（.crx ファイル）や AutoCAD .NET アプリケーションを指定することも出来ます。</p>
<p>AutoCAD I/O の利用には、View and Data API のようにアクセスキーの取得が必要です。こちらの取得も<strong>デベロッパ ポータル</strong>（<a href="http://developer.autodesk.com" target="_blank"><strong>http://developer.autodesk.com</strong></a>）でおこなうことが出来ます。また、ドキュメントやサンプルも同サイトから入手できますので、ぜひ評価をしてみてください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
