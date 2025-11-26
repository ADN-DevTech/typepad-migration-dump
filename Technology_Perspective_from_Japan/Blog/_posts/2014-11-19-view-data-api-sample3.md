---
layout: "post"
title: "View & Data API サンプル ～ その3"
date: "2014-11-19 05:50:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/11/view-data-api-sample3.html "
typepad_basename: "view-data-api-sample3"
typepad_status: "Publish"
---

<p><strong>Autodesk Developer Portal</strong>（<strong><a href="http://developer.autodesk.com/" target="_blank">http://developer.autodesk.com</a></strong>）を介して <a href="https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples" target="_blank">GirHub</a> に記載されているサンプルには、クライアントで利用できる JavaScript サンプルも多数記載されています。この中には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample2.html" target="_blank">前回</a>&#0160;</strong>ご紹介した&#0160;<a href="https://github.com/Developer-Autodesk/workflow-wpf-view.and.data.api" target="_blank"><strong>WPF workflow</strong>&#0160;</a>サンプルとともに利用するように設計されたものも存在します。起動したサンプルから View and Data API を利用するビューワを起動する際に、WPF workflow サンプルに含まれた HTML ではなく、独自に作成した HTML を指定することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d08d0757970c-pi" style="display: inline;"><img alt="Custom_viewer_call" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d08d0757970c image-full img-responsive" src="/assets/image_572651.jpg" title="Custom_viewer_call" /></a></p>
<p>この&#0160;WPF workflow サンプルを連携できるのは、<a href="https://github.com/Developer-Autodesk/client-view-save-animate-view.and.data.api" target="_blank"><strong>ViewSaveAnimate</strong></a> サンプルです。View and Data API は、他の JavaScript ライブラリとの併用が可能ですが、このサンプルでは<a href="http://jqueryui.com/" target="_blank"> ｊQuery UI</a> を利用して、ビューワ上にフローティング ツールバーを実現しています。</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7030126970b-pi" style="display: inline;"><img alt="Jquery_ui_toolbar" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7030126970b img-responsive" src="/assets/image_85755.jpg" title="Jquery_ui_toolbar" /></a>&#0160;</p>
<p>このツールバーを利用して、任意に登録したビューの登録と、複数登録したビュー間をアニメーションで表示する機能を提供しています。登録したビューは、ツールバー上でボタンとして表示されます。このボタンは、ドラッグ&amp;ドロップで順序を変更したり、削除したりすることが出来るようになっています。</p>
<p>Workflow サンプルからは、HTML パラメータとして表示に必要なアクセス トークンとドキュメントを指定する URN が渡されています。Workflow サンプルが想定しているパラメータ名は、<strong>accessToken</strong> と <strong>urn</strong> です。</p>
<p>file:///C:/...../client-view-save-animate-view.and.data.api-master/ViewSaveAnimate.html<strong>?accessToken</strong>=yWsaRuM2oQsvsWaEdHQly6ioGPzQ<strong>&amp;urn</strong>=dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6YWRuLTA1LjA4LjIwMTQtMTUuMDMuNDAvTFJUX1N0YXRpb24ubndk</p>
<p>このパラメータを処理する ViewSaveAnimate サンプル（ViewSaveAnimate.html 内の&#0160;JavaScript コード）では、次のようなコードで渡されたパラメータを処理しています。</p>
<pre><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif;">var urn = Autodesk.Viewing.Private.getParameterByName(&quot;<strong>urn</strong>&quot;);
var accessToken = Autodesk.Viewing.Private.getParameterByName(&quot;<strong>accessToken</strong>&quot;);</span></pre>
<p>ファイルのアップロードや変換などの処理をすべて実装しなくても、この方法を使用することで、クライアント側の処理をテストすることが出来るわけです。</p>
<p>ここまでの内容を動画にしてみましたので、確認してみてください。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/_Q3GwrNiJPM?feature=oembed" width="459"></iframe>&#0160;</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
