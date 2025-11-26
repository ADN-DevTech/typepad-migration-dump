---
layout: "post"
title: "A360 API とストレージの考え方"
date: "2015-01-21 00:08:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/a360-api-and-storage.html "
typepad_basename: "a360-api-and-storage"
typepad_status: "Publish"
---

<p>このブログでは、既にいくつかの A360 Web サービス API をいくつか紹介していますが、データをアップロードして保存するストレージ領域について、誤解が生じやすいように思います。今回は、A360 配下のサービスが利用するストレージ領域と、View and Data API が利用するストレージ領域に注目しながら、クラウドを説明する際に使われる一般的な用語を使って、A360 の考え方や違いを説明したいと思います。</p>
<p><strong>A360 を構成する SaaS、PaaS、IaaS</strong></p>
<p>オートデスクが提供する A360 クラウド サービスは、<strong><a href="http://ja.wikipedia.org/wiki/SaaS" target="_blank">SaaS</a></strong>（Software As A Service）&#0160;にあたるもので、Web ブラウザやオートデスクが用意したモバイル アプリケーションから利用することになります。ここでは、SaaS を、エンド ユーザが利用する最終成果物と考えることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07dcb6ab970d-pi" style="display: inline;"><img alt="A360_web_services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07dcb6ab970d image-full img-responsive" src="/assets/image_344534.jpg" title="A360_web_services" /></a></p>
<p>一方、View and Data API を利用する開発者は、オートデスクの SaaS がおこなっているのと同じように&#0160;<a href="http://ja.wikipedia.org/wiki/Platform_as_a_Service" target="_blank"><strong>PaaS</strong></a>&#0160;を利用することになります。つまり、開発者は、独自の SaaS を作成するために PaaS を利用します。開発者は、PaaS で利用可能な、表示、検索などの機能を API を介して利用することで（プログラムすることで）、独自の SaaS を作成することが出来ます。</p>
<p>また、オートデスクが提供する A360 SaaS や PaaS は、<a href="http://aws.amazon.com/jp/" target="_blank"><strong>Amazon Web Services</strong></a> のインフラ上に構築されています。別の言い方をするなら、A360 は Amazon Web Services を&#0160;<a href="http://ja.wikipedia.org/wiki/Infrastructure_as_a_Service" target="_blank"><strong>IaaS</strong></a>&#0160;として利用しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07d08288970d-pi" style="display: inline;"><img alt="Paas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07d08288970d image-full img-responsive" src="/assets/image_277770.jpg" title="Paas" /></a></p>
<p><strong>A360 の SaaS、PaaS、IaaS が利用するストレージ領域</strong></p>
<p>A360 SaaS では、図面ファイルや 3D モデルを含まれるファイルを<strong>&#0160;A360</strong>（<a href="http://autodek360.com" target="_blank">http://autodek360.com</a>） にアップロードして、表示・検索 等で利用することになります。また、その他のサービス、例えば、<strong>AutoCAD 360</strong>（<a href="http://www.autocad360.com" target="_blank">http://www.autocad360.com</a>）や&#0160;<strong>ReCap 360</strong>（<a href="http://recap360.autodesk.com" target="_blank">http://recap360.autodesk.com</a>）、<strong>Fusion 360</strong> で作成した 3D モデルなどでも、同じアカウントでログインしていれば、他のサービスで作成したファイルを A360 側でも直接確認することが出来ます。アカウント毎にストレージ領域を共有利用することで、シームレスな運用が可能になり、利便性が高めるために実装されている機能です。</p>
<p>View and Data API でも、もちろん、データ ファイルをアップロードしてストリーミング配信するためにストレージを利用します。ところが、PaaS を利用する開発者は、A360 SaaS が利用するアカウント（Autodesk ID）を認識したり、アカウント情報にアクセスすることが出来ません。このため、開発者に割り当てられた&#0160;<strong>Consumer Secret</strong>&#0160;と&#0160;<strong>Consumer Key</strong>&#0160;を利用して、<strong><a href="http://ja.wikipedia.org/wiki/OAuth" target="_blank">OAuth</a>&#0160;</strong>仕様に基づいたアクセス許可を得ることでストレージにアクセスします。なお、API がアクセス可能なストレージ領域は、API 用途に用意された専用領域に限定されています。API 用のストレージ領域では、バケット名（Bucket）で一意に識別した場所を用意することで、他の開発者がアップロードするファイルとの競合を防止する仕組みを導入しています。</p>
<p>IaaS として見た場合、利用されているストレージは、<a href="http://aws.amazon.com/jp/s3/" target="_blank"><strong>Amazon S3</strong> </a>です。A360 SaaS を利用するエンドユーザや、PaaS を利用する開発者は、S3 ストレージを意識することはありません。</p>
<p><strong>まとめ</strong>&#0160;</p>
<p>簡単な説明ですが、ここまでの内容をまとめると、次のようになります。</p>
<ul>
<li>View and Data API は、A360 のユーザ アカウント（Autodesk ID でログイン）が利用するストレージ領域を利用することは出来ません。</li>
<li>ユーザ アカウント（Autodesk ID）で&#0160;A360 へログインしても、View and Data API でアップロードしたファイルを確認することは出来ません。</li>
<li>SaaS が利用するストレージ領域と、PaaS が利用する領域は、別物と考えることが出来ます。</li>
</ul>
<p>現在のところ、SaaS ストレージにデータ ファイルをアップロードしたり、SaaS ストレージにアップロードされたファイルを表示、操作する API は存在していません。</p>
<p><span style="background-color: #ffff00;">&lt;注意&gt;：2016年6月に Forge DevCon で発表された Data Management API を利用することで、A360 や Fusion 360 のユーザ領域にアクセスする機能が提供されました。</span></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
