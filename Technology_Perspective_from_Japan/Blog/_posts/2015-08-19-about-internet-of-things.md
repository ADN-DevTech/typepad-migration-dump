---
layout: "post"
title: "Internet Of Things とは"
date: "2015-08-19 01:19:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/08/about-internet-of-things.html "
typepad_basename: "about-internet-of-things"
typepad_status: "Publish"
---

<p>最近、<strong>IoT</strong> という言葉をよく耳にしたり、Web ニュースで話題になることが多くあります。IoT は、<strong>Internet Of Things</strong> の略で、日本語では「<strong>モノのインターネット</strong>」と訳されるようです。</p>
<p>AutoCAD や &#0160;Revit など、インターネットや社内ネットワークに接続して利用するパーソナル コンピュータ（以下、PC）は、すべて<strong> IP アドレス</strong> と呼ばれる一意な識別子が割り当てられています。スマートデバイスと呼ばれるスマートフォンやタブレットにも、インターネットに接続するために、IP アドレスが割り当てられています。近年になって、PC の利用が一般化したところに、スマートデバイスが激増したこともあり、IP アドレスが足りなくなってしまうことが<a href="https://ja.wikipedia.org/wiki/IP%E3%82%A2%E3%83%89%E3%83%AC%E3%82%B9%E6%9E%AF%E6%B8%87%E5%95%8F%E9%A1%8C" target="_blank"><strong>懸念</strong></a>されたほどです。</p>
<p>その後、<a href="https://ja.wikipedia.org/wiki/IPv6" target="_blank"><strong>IPv6</strong> </a>の登場でIPアドレス枯渇問題を避けることが出来たことから、更に、多数のデバイスにも IP アドレスを与えようとする動きが出てきました。具体的には、家電や自動車をはじめ、センサーやスイッチといった電子部品にも IP アドレスを割り当ててネット接続してしまおう、というものです。これが IoT です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d149c8c8970c-pi" style="display: inline;"><img alt="IoT_image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d149c8c8970c img-responsive" src="/assets/image_113925.jpg" title="IoT_image" /></a></p>
<p>すべてのモノがネット接続出来るということは、つまり、PC やスマートデバイスと同じように、ネットを介してすべてのモノと通信出来るということです。もちろん、通信する内容は、それを制御するプログラムに依存することになります。ただし、その内容はアイデア次第で無限です。各種センサーから動的に情報を得たり、帰宅前に家のエアコンを入れて適温に設定するなど、出先から家電をコントロールすることも可能でしょう。交差点に設置したセンサーから、通行中の自動車に交差点通過まで時間を渡すようなことも可能なはです。GPS を使ってドローンを予定したコースで飛行させるだけでなく、ドローン自身がコース周辺の風力センサーから情報を得て、出力を最適化したり、高度を上下させて墜落を防止することも出来るでしょう。</p>
<p>もう、お気づきと思いますが、これらアイデアは既に実現されたり、実現しつつあるものです。背景は IoT という概念の登場と無縁ではありません。現在、統一された規格や仕様がない、セキィリティが十分でない、という懸念は残っていますが、それを解決しようとする動きはあります。具体的には、<strong>Web Of Things</strong>&#0160;（<strong>WoT</strong>）、「<strong>モノの Web</strong>」という言葉で、IoT を利用するアプリケーションの開発概念や具体的な手法が提供され始めています。</p>
<p>IoT は、いままで静止物としてのモノに、インテリジェンスと接続性を与えます。前述のセンサーで収集したデータを IoT で取得し続けると発生する膨大なデータが&#0160;<a href="https://ja.wikipedia.org/wiki/%E3%83%93%E3%83%83%E3%82%B0%E3%83%87%E3%83%BC%E3%82%BF" target="_blank"><strong>ビックデータ</strong></a>&#0160;になります。ビックデータの保存先としてクラウドが選択されたり、クラウド上のビックデータの解析や応用に <a href="http://www.ibm.com/smarterplanet/jp/ja/ibmwatson/" target="_blank"><strong>IBM Watson</strong></a> のような人工知能が活用され始めています。これらのテクノロジは、いままで単独も存在していましたが、すべてがネット接続されることで、再び脚光を浴び始めているのです。</p>
<p>重要なのは、IoT や WoT が一部の専門家だけのため技術ではないという点です。これは、受動的に恩恵を受けるだけではなく、誰でも IoT を使った作業に参加できることを意味しています。3D プリンタの普及や手軽に利用出来る 3D デザイン ソフトウェアの登場で、誰もが独自の 3D モデルを設計して作製出来る<a href="https://ja.wikipedia.org/wiki/%E3%83%A1%E3%82%A4%E3%82%AB%E3%83%BC%E3%82%BA%E3%83%A0%E3%83%BC%E3%83%96%E3%83%A1%E3%83%B3%E3%83%88" target="_blank"><strong>時代</strong></a>です。</p>
<p>これらのムーブメントには、3D モデリングだけではなく、電子回路設計やプログラミングも含まれます。小学生のプログラミング学習が議論されてもいますので、この流れは今後加速していくはずです。そして、誰にでもオープンな Web 開発環境が、この動きを支えています。&#0160;肝心の IoT デバイスにも、<strong><a href="https://ja.wikipedia.org/wiki/Raspberry_Pi" target="_blank">Raspberry Pi&#0160;</a></strong>や&#0160;<a href="http://mozopenhard.mozillafactory.org/" target="_blank"><strong>MozOpenHard CHIRIMEN</strong></a>&#0160;等を利用することが出来ます。3D デザインから手掛ける Fabricator やスタートアップ の方にも、重要なテクノロジになるはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bff5e2970b-pi" style="display: inline;"><img alt="Open_source" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bff5e2970b image-full img-responsive" src="/assets/image_2337.jpg" title="Open_source" /></a></p>
<p>現在のところ（このブログ記事を執筆した時点で）、オートデスクが &#0160;IoT 、WoT そのものの仕様や規格に直接関わる予定はありませんが、設計やデザイン業界も含め、IoT/WoT が生活を一変させてしまう可能性を秘めているため、注意深く見守っているといった状態です。<a href="http://www.autodesk.co.jp/products/fusion-360/overview" target="_blank"><strong>Fusion 360</strong></a>&#0160;や&#0160;<a href="https://123d.circuits.io/" target="_blank"><strong>123D&#0160;Circuit</strong></a>&#0160;などの提供でメイカーズムーブメントを支援しているのも、その一環と考えることが出来ます。</p>
<p>By Toshiaki Isezaki</p>
