---
layout: "post"
title: "Project Leopard の試み"
date: "2016-02-09 00:24:22"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/02/experient-on-project-leopard.html "
typepad_basename: "experient-on-project-leopard"
typepad_status: "Publish"
---

<p>Web 開発の世界では、Web ブラウザを利用してさまざまなサービスが提供されてきています。オートデスクでも、Web ブラウザのみで利用することが出来る A360 や AutoCAD 360、Rendering in A360 など、複数の異なるサービスを提供しています。これらは、Web ブラウザ内で動作する JavaScript で記述されたプログラムと、クラウド側のバックエンド プログラムが連携して動作するものです。</p>
<p>なぜ、Web ブラウザに頼ったプログラムに脚光が当たるのでしょう？</p>
<p>理由は簡単です。Web ブラウザ上で動作するプログラムを開発すれば、Windows や Mac、Linux、また、iOS や Android といったプラットフォーム毎に、個別の専用ソフトウェアを作成する必要がなくなるためです。例外も存在しますが、JavaScript は、どのプラットフォームの、どの Web ブラウザでも動作します。つまり、1 つプログラムを記述してしまえば、クラウド側のバックエンド プログラムを共有して、どのプラットフォームでも動作するサービスを提供することが出来るのです。</p>
<p>クライアント コンピュータには、Web ブラウザがあればいいので、他に何もインストールする必要はありません。プラットフォーム毎に異なる開発チームを編成してクライアント プログラムを開発する必要もなくなるので、開発コストも低減させることが出来ます。</p>
<p>ちょうど、以前のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/05/use-type-of-cloud.html" target="_blank">クラウドの利用形態について</a></strong>&#0160;ご紹介した流れです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19d3565970c-pi" style="display: inline;"><img alt="Pure_cloud_services" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19d3565970c image-full img-responsive" src="/assets/image_967304.jpg" title="Pure_cloud_services" /></a></p>
<p>そして、この流れは、今後も加速していくはずです、事実、Web ブラウザ内で動作するクライアント プログラムを高速化させるために、<strong><a href="https://en.wikipedia.org/wiki/WebAssembly" target="_blank">WebAssembly</a></strong> や <strong><a href="https://ja.wikipedia.org/wiki/Emscripten" target="_blank">Emscripten</a></strong>&#0160;といった新しいテクノロジが議論されつつあります。</p>
<p>一方、オートデスクが提供するクラウド サービスの中には、クライアント コンピュータに小さなプログラムをインストールしなければならないものも存在します。例えば、Fusion 360 や BIM 360 Glue、Infraworks 360 などです。Fusion 360 の場合には、Windows と Mac の両方のプラットフォームをサポートしているので、2種類のクライアント用プログラムが用意されていることになります。このようなクラウド サービスが存在する理由ですが、モデリング機能等で、まだクライアント コンピュータのグラフィックス性能に依存する部分があるためです。ただ、これらのプログラム（クラウド サービス）も、いずれは Web ブラウザで動作するようになるものと思います。</p>
<p>その1つとして、Project Leopard が既に公開されています。これは、Web ブラウザを使った「ゼロ クライアント」の試みとして、Fusion 360 の機能や能力を実験しようとするプロジェクトです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c812a35d970b-pi" style="display: inline;"><img alt="Project_leopard_catch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c812a35d970b image-full img-responsive" src="/assets/image_606822.jpg" title="Project_leopard_catch" /></a></p>
<p>Project Leopard は、<strong><a href="http://projectleopard.com/" target="_blank">http://projectleopard.com/</a></strong>&#0160;からアクセスすることが出来ますが、あくまで、ベータ評価の扱いです。Autodesk ID でお使いいただけます。決して、このままの形で Fusion 360 が Web クライアント化される訳ではありませんが、テスト プロジェクトとしてご紹介しておきます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c812a3a1970b-pi" style="display: inline;"><img alt="Project_leopard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c812a3a1970b image-full img-responsive" src="/assets/image_357074.jpg" title="Project_leopard" /></a>&#0160;</p>
<p>By Toshiaki Isezaki</p>
