---
layout: "post"
title: "モデリング以外の Fusion 360 機能"
date: "2016-05-06 02:01:50"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/features-on-fusion360-except-modeling.html "
typepad_basename: "features-on-fusion360-except-modeling"
typepad_status: "Publish"
---

<p>モデリング機能の話題が先行している Fusion 360 ですが、ワークスペースを <strong>モデル</strong>&#0160;以外に切り替えれば、モデリング以外の機能にも簡単にアクセスすることが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08f3db0d970d-pi" style="display: inline;"><img alt="Workspaces" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08f3db0d970d img-responsive" src="/assets/image_12157.jpg" title="Workspaces" /></a></p>
<p>大分前になりますが、<strong>レンダリング機能</strong>について、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/11/modeling-on-fusion-360-capabilities.html" target="_blank">Fusion 360 モデリングと機能</a></strong>&#0160; で一部触れています。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/4pO1pT4jIfw?feature=oembed" width="500"></iframe>&#0160;</p>
<p>また、もともと &#0160;Fusion 360 Ultimate にしか搭載されていなかった<strong>アニメーション機能</strong>が、Fusion 360 に統合で利用できるようになっています。その点については、&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/11/fusion-360-update-and-fusion-360-ultimate.html" target="_blank">Fusion 360 の更新と Fusion 360 Ultimate</a></strong>&#0160;の中でご紹介しています。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Deb-Uq2gDu0?feature=oembed" width="500"></iframe>&#0160;</p>
<p>今回は、それ以外の<strong>シミュレーション機能</strong>と <strong>CAM 機能</strong>について、簡単に触れておきたいと思います。といっても、個々の機能について羅列するだけだと具体性に欠けてしまうので、若干ストーリーを持たせてご案内していきたいと思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1da0a1d970c-pi" style="display: inline;"><img alt="Various_features_on_fusion360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1da0a1d970c image-full img-responsive" src="/assets/image_884220.jpg" title="Various_features_on_fusion360" /></a></p>
<p>Fusion 360 は、AutoCAD 2017 など、最新のデスクトップ製品と同様に、Print Studio と連携して 3D プリンタで確実に出力可能なデータをシームレスに作成していくことが出来ます。</p>
<p>そこで、突然ですが、私の上司の Jim の登場です。サンフランシスコ在住の彼は、自身のヨットを持っているほどのヨット愛好家なのすが、<strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B5%E3%83%90%E3%83%86%E3%82%A3%E3%82%AB%E3%83%AB" target="_blank">サバティカル</a></strong>と呼ばれる長期休暇の際には、ハワイ諸島までレースに出てしまう本格派です。その彼が Fusion 360 でヨットをモデリングして、Print Studio での編集を経て出力したものが、次の写真にあるモデルです。もちろん、複数のパーツで構成されたアセンブリになっています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1da0b21970c-pi" style="display: inline;"><img alt="Jim" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1da0b21970c image-full img-responsive" src="/assets/image_225636.jpg" title="Jim" /></a></p>
<p>実物よりも大分小さいヨット モデルですが、数年を経ずに実物大のヨットを 3D プリント出来るようになる可能性があるので、少し真剣です。</p>
<p>さて、モデリングしたパーツの中に、Gooseneck と呼ばれるパーツがあります。このパーツは、ヨットのマストとブームを連結させる部品で、通常は、複数の金具で構成されていますが、ここでは単一モデルとして共同設計者がデザインしています。</p>
<p>Gooseneck を 3D プリントするにあたり、強度に不安を感じた Jim は、Fusion 360 の シミュレーション機能を利用して、材質や拘束位置、負荷を指定するだけで、簡単に強度を調査をしていきます。一部、強度不足と感じられる箇所が見つかったら、Fusion 360 が持つライブレビューを使って、共同設計者と補強すべき箇所をリアルタイムに決定していくことも出来ます。</p>
<p>もし、ある程度の強度を見込みたいなら、金属部品にする必要があるかも知れません。そこで、オートデスク所有の工房 Pier 9 &#0160;で部品を削りだす必要性を感じた彼は、、Fusion 360 の CAM 機能を使って、NC 工作機械に渡す NC データを作成していきます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1da0c5e970c-pi" style="display: inline;"><img alt="Simulation_and_cam" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1da0c5e970c image-full img-responsive" src="/assets/image_834530.jpg" title="Simulation_and_cam" /></a></p>
<p>少々強引なストーリーですが、ここまでに内容をまとめてみましたので、次の動画を見てみてください。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/YNB22T3nTBg?feature=oembed" width="500"></iframe>&#0160;</p>
<p>このように、Fusion 360 は、いままで個別に購入してインストール、利用していた各種ソフトウェアの機能を、1 つのクラウド サービスの中で、いつでも横断的に利用することが出来るプロフェッショナル ツールです。今後も、さまざまな機能が適宜導入される予定です。</p>
<p>API の側面でも、Fusion 360 内部で動作するアドインやスクリプト以外に、クラウド上のデザイン ファイルから必要なデータを抽出したり、デザイン ファイル自体を操作したりするなど、クラウド サービスならではのデータ管理機能が API として提供されていく予定です。</p>
<p>Autodesk Fusion 360 は、次世代の製造業向けの統合クラウド サービスなのです。</p>
<p>By Toshiaki Isezaki</p>
