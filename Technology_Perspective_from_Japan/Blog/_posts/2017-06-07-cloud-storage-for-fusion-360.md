---
layout: "post"
title: "Fusion 360 のクラウド ストレージ"
date: "2017-06-07 00:01:14"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/cloud-storage-for-fusion-360.html "
typepad_basename: "cloud-storage-for-fusion-360"
typepad_status: "Publish"
---

<p>Fusion 360 は次世代の CAD と言われています。「次世代」たる所以は、単にクラウドを利用するということだけでなく、モデリングや図面化以外にアニメーションや CAD、シミュレーション機能など、従来のデスクトップ CAD が単体で持っていなかった機能を 1 つの製品に内包し、かつ、安価な Subscription で提供しているため、と考えることも出来ます。また、登場当初からシームレスにクラウド レンダリングが組み込まれるなど、他のクラウド サービスとの連携がデザインされている点も重要です。</p>
<p>他のクラウド サービスとの連携で最も分かり易いのは、ストレージ サービス連携です。Fusion 360 は編集こそクライアント コンピュータ上で実行しますが、デザイン データはクラウド ストレージに保存されます。ご存じのとおり、Fusion 360 のユーザ インタフェース左側のデータ パネルには、Fusion 360 が使用するプロジェクトや、プロジェクトに含まれるフォルダやデザイン ファイルが表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d27e9012970c-pi" style="display: inline;"><img alt="Fusion_data_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d27e9012970c image-full img-responsive" src="/assets/image_690868.jpg" title="Fusion_data_panel" /></a></p>
<p>このデータ パネルで表示されるのは、A360 や BIM 360 Team、あるいは Fusion Team クラウド サービスと同じストレージ領域でもあります。したがって、Fusion 360 使用時にサインインしているのと同じアカウントで A360 などにサインインすれば、別サービスでも全く同じストレージ内容を参照することが出来ます。</p>
<p><img alt="A360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8f442c8970b image-full img-responsive" src="/assets/image_153743.jpg" title="A360" /></p>
<p>ストレージに保存されたデザイン データは Fusion 360 編集後の保存操作で自動的にバージョン管理されるので、必要に応じて古いバージョンを最新バージョンに変更して編集を加えていくようなことも出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d27e941b970c-pi" style="display: inline;"><img alt="Version_up_on_a360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d27e941b970c image-full img-responsive" src="/assets/image_306337.jpg" title="Version_up_on_a360" /></a></p>
<p>このようなデータ管理機能は、デザイン ファイルのアップロード毎にバージョン管理出来るよう&#0160;A360 がもともと持っているもので、Fusion 360 も流用しているわけです。別の言い方をするなら、<strong>オートデスクのクラウド サービスではユーザ アカウント毎に同じストレージ領域を共有している</strong>、と表現することが出来るわけです。そして、これら<strong>オートデスクのクラウド サービスが利用しているクラウド ストレージ構造は Forge でもアクセスが可能</strong>です。この部分は、先日のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/cloud-storage-forge-uses.html">Forge が使用するクラウド ストレージ</a></strong>&#0160;でご紹介したとおりです。</p>
<p>ご注意いただきたいのは、クラウド サービスのストレージ領域はユーザ アカウント毎に隠蔽されているので、他のユーザをプロジェクトに招待しないかぎり、本来、他のユーザやアプリからはアクセス出来ない点です。ただし、Forge を使ったアプリがユーザからアクセス認可を得ることが出来れば、アプリもユーザのストレージ領域にアクセスすることが出来るようになります。アクセス許可を得るプロセスが 3-legged 認証です。つまり、Forge を利用するアプリは、Fusion 360 が利用するストレージ領域にアクセスして、プロジェクトやフォルダ、デザイン ファイルと、その各バージョンについて、アクセスして情報を得ることが可能です。</p>
<p>3-legged 認証は Forge の Authentication API（OAuth API）、プロジェクトやフォルダ、アイテム、バージョンへのアクセスには Forge の Data Management API を用います。このように、オートデスク クラウド サービスと Forge は一体であり、アプリからのデータ アクセスを提供することが出来るわけです。</p>
<p>By Toshiaki Isezaki</p>
