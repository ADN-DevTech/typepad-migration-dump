---
layout: "post"
title: "View & Data API サンプル ～ その2"
date: "2014-10-31 23:58:15"
author: "Toshiaki Isezaki"
categories: []
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample2.html "
typepad_basename: "view-data-api-sample2"
typepad_status: "Publish"
---

<p>今回も View and Data API のサンプルについてご案内します。View and Data Web サービス API&#0160;Web サービス API のサンプルが、<strong>Autodesk Developer Portal</strong>（<strong><a href="http://developer.autodesk.com/" target="_blank">http://developer.autodesk.com</a></strong>）からダウンロードできるのは、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample1.html" target="_blank"><strong>前回</strong></a>、ご紹介したとおりです。</p>
<p>今日ご紹介するのは、<a href="https://github.com/Developer-Autodesk/workflow-wpf-view.and.data.api" target="_blank"><strong>WPF workflow</strong> </a>サンプルです。このサンプルは、View and Data API の REST API を使った<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html" target="_blank"><strong>一連の処理</strong></a>を、操作手順に沿ってテストできるサンプルです。もちろん、最後の部分で、アップロードしたファイルを WebGL 対応の Web ブラウザで表示できるようになっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0885108970c-pi" style="display: inline;"><img alt="WPF sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0885108970c image-full img-responsive" src="/assets/image_330423.jpg" title="WPF sample" /></a></p>
<p>GutHub リポジトリからプロジェクトをダウンロードして展開すると、Visual Stduio 2012 で作成された 3 つのプロジェクトファイルが、1つのソリューション ファイルでまとめられたかたちで展開されます。まずが、ソリューション ファイルを Visual Studio 2012 で開いてみてください。</p>
<p>3つのプロジェクトにある参照設定を確認すると、一部のアセンブリの参照に失敗している状態で展開されているはずです。Autodesk.ADN.Toolkit.ViewData プロジェクトでは&#0160;RestSharp と&#0160;Newtonsoft.Json、Autodesk.ADN.ViewDataDemo プロジェクトでも&#0160;Newtonsoft.Json が参照エラーになっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a38d7b970d-pi" style="display: inline;"><img alt="Reference_errors" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a38d7b970d img-responsive" src="/assets/image_568834.jpg" title="Reference_errors" /></a></p>
<p>このサンプルをテストするためには、まず、この参照エラーを解決していく必要があります。ここで参照エラーになっているは、オープンソースのアセンブリです。個々に Web サイトで検索して、適切なアセンブリをダウンロードしていくことも可能ですが、ここでは、Visual Studio に組み込まれている ライブラリ パッケージ マネージャー を利用します。今回は、Visual Studio の [パッケージ マネージャー コンソール] を使って、<a href="http://msdn.microsoft.com/ja-jp/magazine/hh547106.aspx" target="_blank"><strong>NuGet</strong></a> で不足しているアセンブリを解決します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a38dc2970d-pi" style="display: inline;"><img alt="Package_console" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a38dc2970d image-full img-responsive" src="/assets/image_590174.jpg" title="Package_console" /></a></p>
<p>パッケージ マネージャー コンソールが Visual Studio 上に表示されたら、参照エラーになっているアセンブリを1つずつ解決していきます。対象のプロジェクトに注意しながら、パッケージ マネージャー コンソールに&#0160;<strong>Install-Package &lt;参照エラーのアセンブリ名&gt;&#0160;</strong>のように入力してみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6fe574c970b-pi" style="display: inline;"><img alt="Package_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6fe574c970b image-full img-responsive" src="/assets/image_388725.jpg" title="Package_manager" /></a></p>
<p>他の不足アセンブリ名で、同じ作業を繰り返すことで、参照エラーをすべて解決できるはずです。次に、Autodesk.ADN.ViewDataDemo プロジェクト配下の&#0160;<strong>UserSettings.cs</strong>&#0160;を開いて、<strong>A</strong><strong>utodesk Developer Portal</strong>（<strong><a href="http://developer.autodesk.com/" target="_blank">http://developer.autodesk.com</a></strong>）で入手した Consumer Key と Consumer Secret を記入して、ソリューション全体をビルドします。さて、いよいよサンプルを実行してみます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/Bo3zX5RekRI?feature=oembed" width="500"></iframe>&#0160;</p>
<p>いかがでしょうか。このサンプルで、&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html" target="_blank"><strong>A360 View and Data サービス API 利用の手引き</strong></a> の内容を把握できるはずです。なお、最後に Web ブラウザでビューワを表示しているのは、Autodesk.ADN.ViewDataDemo\resources フォルダにある&#0160;viewer.html ファイルです。もちろん、このファイルには、HTML と JavaScript コードが記載されています。&#0160;</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
