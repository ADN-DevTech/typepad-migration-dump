---
layout: "post"
title: "View & Data API サンプル ～ その１"
date: "2014-10-24 02:43:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample1.html "
typepad_basename: "view-data-api-sample1"
typepad_status: "Publish"
---

<p><strong>Autodesk Developer Portal</strong>（<strong><a href="http://developer.autodesk.com/" target="_blank">http://developer.autodesk.com</a></strong>）では、オートデスクの View and Data Web サービス API&#0160;Web サービス API のサンプルが多数記載されています。サンプルには、トップページ内の左下の SAMPLE APP バナーか、ページ上部の <a href="https://developer.autodesk.com/sample-apps" target="_blank">Sample Apps</a> リンクからアクセスすることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb079eb0f5970d-pi" style="display: inline;"><img alt="Developer_portal" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb079eb0f5970d image-full img-responsive" src="/assets/image_657968.jpg" title="Developer_portal" /></a></p>
<p>数回のクリックで、実際にサンプル群が記載されたページにたどり着くことが出来ます。サンプルは、GitHub と呼ばれる開発者向けのクラウド リポジトリで公開されています。</p>
<p>View and Data Web サービス API には、クラウド アクセスの認証やファイルのアップロードやダウンロードをおこなう REST API と、クライアント側で Web ブラウザを利用して 3D モデルや 2D 図面を表示する JavaScript API に分けることが出来ます。これらサンプルには、workflow- の名前で始まるサンプルと、client- の名前で始まるサンプルがあります。前者が REST API、後者が JavaScript API を使ったものと考えることが出来ます。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f98ad7970b-pi" style="display: inline;"><img alt="Github" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f98ad7970b image-full img-responsive" src="/assets/image_916279.jpg" title="Github" /></a></p>
<p>各リンクをクリックすると、サンプルの概要や実行時に必要となる他のライブラリの依存関係などが記述されていまます。実際に、サンプル一式をダウンロードする場合には、画面右下の [Download ZIP] ボタンを利用することが出来ます。</p>
<p>さて、ここでは、記載されたサンプルの内で、視覚的にも分かり易いサンプルを1つご紹介しておきます。サンプル名は&#0160;<a data-skip-pjax="true" href="https://github.com/Developer-Autodesk/client-steampunked-morgan">client-steampunked-morgan</a>&#0160;です。サンプルは、<a href="http://autode.sk/m3w" target="_blank">http://autode.sk/m3w</a>&#0160;で実際に動作している状態を直接確認することも出来ますので、まずは動作をご確認ください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb079eb062970d-pi" style="display: inline;"><img alt="Steampunked-mogan" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb079eb062970d image-full img-responsive" src="/assets/image_240038.jpg" title="Steampunked-mogan" /></a></p>
<p>画面中央に表示されているのが、&#0160;View and Data Web サービス API の部分です。画面右側のボタンをクリックすると、アニメーションとともに、ボタン ラベルに表示されている部位の視点で 3D モデルを表示するはずです。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/3p1lc6X95Ao?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>オートデスクの Web サービスというと、CAD ユーザを対象にしたものを想定されると思います。ただ、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/10/autodesk-3d-hackathon-results.html" target="_blank">オートデスク 3D ハッカソン</a></strong> で出たアイデアも、このサンプルも、3D モデルを表示する Web アプリケーションでは、CAD/CAM を意識させないアプリケーションの作成も出来るはずです。</p>
<p>さて、クライアントが&#0160;View and Data Web サービス API で 3D モデルや 2D 図面を表示するには、クラウドにアクセスしてストリーミング配信を受けるために認証を受ける必要があります。通常のクラウド アクセスであれば、この部分はユーザ名とパスワードの入力でおこなわれる部分です。View and Data Web サービス API &#0160;では、OAuth 2.0 に沿って Consumer Key と Consumer Secret を使い、アクセスの委任権をアクセス トークンとして受取り、アクセス時にコード内で利用します。&#0160;</p>
<p>Consumer Key と Consumer Secret &#0160;は、クライアント上で参照されてしまう JavaScript コード内に記述することは通常しないはずです。サーバー側でアクセス トークンを発行するメカニズムを構築して隠蔽します。この時、サーバー側の実装には、さまざまな手法を考えることが出来ますが、この&#0160;client-steampunked-morgan サンプルでは、Node.js を利用しています。オートデスク製品に関連するアプリケーションを開発されている方は、Windows 上で開発される方が多いと思いますが、アクセス トークンの発行処理には、もちろん、使い慣れた ASP.NET でも代替可能です。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
