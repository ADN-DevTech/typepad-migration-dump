---
layout: "post"
title: "Forge Platform API の全体像"
date: "2019-01-21 00:15:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/01/overall-picture-forge-platform-api.html "
typepad_basename: "overall-picture-forge-platform-api"
typepad_status: "Publish"
---

<p>Forge はオートデスクが A360 や Fusion 360、BIM 360 などのクラウド サービスで培ってきた要素技術を Web API として公開している API セットであり、そのブランド名でもあります。ただし、従来、オートデスクが提供してきたデスクトップ製品用の API とは、目的や考え方、また、提供方式や利用形態が大きく異なります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3d2beaf200b-pi" style="display: inline;"><img alt="Forge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3d2beaf200b image-full img-responsive" src="/assets/image_241131.jpg" title="Forge" /></a></p>
<p><strong>デスクトップ製品 API 概説</strong></p>
<p style="padding-left: 40px;">デスクトップ製品の API の形態は <strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" rel="noopener" target="_blank">オートデスク製品の API が利用するテクノロジ</a></strong> でご案内していますので、精通さえていない方は必要に応じて内容をご確認ください。さて、デスクトップ製品 API は製品が持っている機能を、対応する関数、メソッド、プロパティとして同じように API 化されていることが普通です。そして、API を用いて、製品の機能を拡張したり、API 機能を再利用して反復タスクを自動化したりするのが目的になっているかと思いす。API を利用するのは、デスクトップ製品にロードして実行されるアドイン（別名、アドオン、プラグイン）アプリが主です。また、API の提供は、デスクトップ製品のインストール時に同時にインストールされたり、SDK で提供されたりしてます。</p>
<p><strong>Forge の実装</strong></p>
<p style="padding-left: 40px;">Forge の場合はどうでしょう。デスクトップ製品の API と違って、Forge は単一のテクノロジで構成されているわけではありません。主に、Forge サービス本体を提供しているサーバー（クラウド）とコミュニティする RESTful API と、Web ブラウザで 2D 図面や 3D モデルを表示する Forge Viewer の JavaScript API です。重要なのは、Forge を利用するのが&#0160; Web サーバー上に構築される Forge アプリになる点です。決して、A360 や BIM 360 の機能自体を拡張するようなアドイン形式のアプリではありません。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3d2c3c0200b-pi" style="display: inline;"><img alt="Use_of_forge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3d2c3c0200b image-full img-responsive" src="/assets/image_882758.jpg" title="Use_of_forge" /></a></p>
<p style="padding-left: 40px;">Forge アプリを Web サーバーに実装しなけければならないのは、端的に言えば、Web セキュリティを保持するためです。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank">Web ブラウザのデベロッパーツールについて</a></strong> でも触れていますが、RESTful API 呼び出しで必須な Access Token は Web ブラウザ内（クライアント）で動作する JavaScript プログラムからは取得出来ません。また、Access Token&#0160; を取得する際に利用する Client ID と Client Secret を隠蔽することも可能です。</p>
<p style="padding-left: 40px;">Viewer 以外で利用することになる Forge の RESTful API は、オートデスクが利用している AWS やオートデスクにどこかのサーバーから直接配信されているわけではなく、Google APIgee ゲートウェイを介して配信されています。apigee によって、RESTful API 呼び出しのすべてがログされ、かつ、どのデベロッパキー（Client ID と Client Secret）によって呼び出されたものかが特定可能になっているわけです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad38d17c3200c-pi" style="display: inline;"><img alt="Use_of_forge2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad38d17c3200c image-full img-responsive" src="/assets/image_251578.jpg" title="Use_of_forge2" /></a></p>
<p><strong>Forge アプリを構築する Web サーバー テクノロジと Forge SDK の関係</strong></p>
<p style="padding-left: 40px;">このように、Forge はインターネット、あるいは Web 開発の環境で運用することが自明です。そして、Forge アプリをホストする Web サーバーに、どのテクノロジを用いるかという選択肢にも繋がることになります。Microsoft Internet Information Server（IIS)上に .NET テクノロジを使って Forge アプリを構築したり、Apache や Node.js、Nginx や PHP、Java のいずれかを用いたり、開発者の自由裁量に任されています。</p>
<p style="padding-left: 40px;">ただ、それぞれのテクノロジ内でネイティブに RESTful API の endpoint を呼び出しを実装していくのは、パラメータの設定などで少々面倒に感じるかもしれません。この煩雑さを低減する目的で、すべてではありませんが、<strong><a href="https://forge.autodesk.com/code-samples" rel="noopener" target="_blank">https://forge.autodesk.com/code-samples</a></strong> で主要な Web サーバー テクノロジ毎に <strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/forge-sdk.html" rel="noopener" target="_blank">Forge SDK</a></strong> が提供されています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad38d1e57200c-pi" style="display: inline;"><img alt="Forge_sdk" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad38d1e57200c image-full img-responsive" src="/assets/image_869195.jpg" title="Forge_sdk" /></a></p>
<p style="padding-left: 40px;">Forge SDK の利用は必須ではありませんが、RESTful API の呼び出しをラップしているので、Forge アプリの実装を簡素化することが可能となります。</p>
<p><strong><br />Forge Viewer</strong></p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad38e80e1200c-pi" style="float: right;"><img alt="Viewer_javascript_library" class="asset  asset-image at-xid-6a0167607c2431970b022ad38e80e1200c img-responsive" src="/assets/image_535060.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer_javascript_library" /></a>一方 、Forge Viewer は、<strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%86%E3%83%B3%E3%83%84%E3%83%87%E3%83%AA%E3%83%90%E3%83%AA%E3%83%8D%E3%83%83%E3%83%88%E3%83%AF%E3%83%BC%E3%82%AF" rel="noopener noreferrer" target="_blank">CDN（Content Delivery Network）</a></strong>を介して CSS と JavaScript ライブラリとして配信されています。Forge View JavaScript ライブラリは、オープンソースの three.js ライブラリをベースに構築されています。もちろん、three.js は WebGL JavaScript ライブラリの上位ラッパーです。このため、Forge Viewer ライブラリは three.js ライブラリと親和性が高く、場合によっては、座標やマトリックスなど、three.js 側の要素を用する場面も考えられます。</p>
<p style="padding-left: 40px;">これとは別に、Forge Viewer JavaScript ライブラリは、Web 開発で一般的に使用されている JavaScript ライブラリを併用するることが可能です。JQuery、AngularJS、React、BACKBORN.JS、ember などです。これらの利用も開発者の自由裁量となります。</p>
<p>デスクトップ製品 API が単一企業による単一テクノロジで提供、利用することが出来ることを考えると、Forge の全体像を把握するにも広範な知識が必要になってしまいます。ただ、これが現在の「繋がる」世界を実現している柔軟な環境と捉えたほうがいいかとも考えます。もはや、特定の単一企業や団体で全てを創り出すのは現実的ではありません。逆に、このような環境だからこそ、<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%83%E3%82%B7%E3%83%A5%E3%82%A2%E3%83%83%E3%83%97_(Web%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)" rel="noopener" target="_blank">マッシュアップ</a></strong> が容易になっている、とも思います。</p>
<p>By Toshiaki Isezaki</p>
