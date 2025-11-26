---
layout: "post"
title: "AU Japan 2016：Forge トラック E-4 セッション サマリ"
date: "2016-09-14 01:22:11"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/09/au-japan-2016-e-4-session-summary.html "
typepad_basename: "au-japan-2016-e-4-session-summary"
typepad_status: "Publish"
---

<p>Autodesk University Japan 2016 にご来場いただいた皆様、誠にありがとうございました。</p>
<p>Forge トラックの E-4 セッションでは、Autodesk Forge Platform を利用した Web サービスの仕組みについて、実際に開発したサンプルをご覧いただきながら説明いたしました。</p>
<p><strong>AU Japan 2016：Forge トラック BIM プロジェクトを Forge で拡張する ～Web サービス開発の概要</strong><br /><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/au-japan-2016-e-4-session.html">http://adndevblog.typepad.com/technology_perspective/2016/08/au-japan-2016-e-4-session.html</a></p>
<p>&#0160;</p>
<p>今回は、E-4 セッションのサマリをレポート致します。</p>
<p><strong>Autodesk Forge Platform を利用した Webサービス開発</strong><br />Autodesk Forge Platform は、Autodesk のクラウド サービスを構成するさまざまな機能を、API というかたちで公開し、再利用するための開発プラットフォームです。</p>
<p>Autodesk のクラウド サービスが基盤としている要素技術を、自社の独自技術や、外部データベース、外部サービスと組み合わせることで、新規に Web サービスを開発したり、自社システムに統合することができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0935f72d970d-pi" style="display: inline;"><img alt="E4-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0935f72d970d image-full img-responsive" src="/assets/image_502370.jpg" title="E4-1" /></a></p>
<p>&#0160;</p>
<p><strong>現在リリースされている要素技術（API）</strong><br />Autodesk Forge Platform API の概要については、下記のブログ記事にまとめられております。</p>
<p>Autodesk Forge Platform API（2016年9月時点）<br /><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/autodesk-forge-overview.html">http://adndevblog.typepad.com/technology_perspective/2016/09/autodesk-forge-overview.html</a></p>
<p>&#0160;</p>
<p><strong>マイクロサービス・アーキテクチャと&#0160;RESTful Web API<br /></strong>Autodesk Forge Platform は、マイクロサービス・アーキテクチャに基づいて設計されております。マイクロサービス・アーキテクチャとは、巨大で複雑な一塊のシステムを、ビジネス機能に沿って複数の小さい「マイクロサービス」に分割し、それらを連携させるアーキテクチャにすることで、迅速なデプロイ、優れた回復性やスケーラビリティといった利点を実現しようとするものです。(※1)</p>
<p>マイクロサービスが他のマイクロサービスと連携する代表的な方法は、RESTful Web API です。URI にアクセスすると XML や JSON などのデータが返ってくるシンプルなタイプ――XML over HTTP 方式や JSON over HTTP 方式――の API です。(※2)</p>
<p>勿論、 Froge の API だけを組み合わせても Web サービスを開発することができますが、インターネット上では、様々な Web サービスが Web API を公開しており、オープンソース ソフトウェア（OSS）も急速に普及しております。<br />Web サービスの目的に応じて、Forge の API だけではなく、これら Web API や OSS、自社の独自技術などを組み合わせることで、最適なサービスを構築することができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8928668970b-pi" style="display: inline;"><img alt="E4-3" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8928668970b img-responsive" src="/assets/image_26577.jpg" style="width: 400px;" title="E4-3" /></a></p>
<p><strong>Web API（代表的なもの）</strong></p>
<ul>
<li>Microsoft Office 365, Microsoft Azure, Microsoft SharePoint</li>
<li>Google Maps API, Google Calendar API, Google Charts, Google Translate API</li>
<li>IBM Watson API</li>
<li>Yahoo! Open Local Platform</li>
<li>日本気象協会 天気予報 API</li>
<li>国土地理院 標高 API</li>
<li>ゼンリン いつもNAVI API</li>
</ul>
<p><strong>オープンソース</strong></p>
<ul>
<li>各種 Web サーバ</li>
<li>各種 Web アプリケーション フレームワーク</li>
<li>各種 データベース</li>
<li>各種 ライブラリ</li>
</ul>
<p>&#0160;</p>
<p><strong>Autodesk Forge を利用した IoT インテグレーション サンプル デモ<br /></strong>E-4 セッションでは、BIM プロジェクトでの利用を想定した Web サービス開発のサンプルとして、「現場作業環境チェックサービス」をご覧頂きました。</p>
<p>この Web サービスは、主に２つの機能を実装しています。</p>
<ul>
<li>複数の専門工事業者が現場で作業を行う際に、仕上げ・設備工事を実施する部屋ごとに、 お互いに現在の作業の進捗状況を確認する。</li>
<li>温室度や照度、あるいは設備機器の稼働状況など、リアルタイムのセンシングデータから、現場作業に適している環境か判断する。</li>
</ul>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/GGE8jcu9pDM?feature=oembed" width="500"></iframe>&#0160;</p>
<p>Autodesk Forge Platform の API と、プロトタイピング用マイコンボード（<a href="https://www.raspberrypi.org/">Raspberry Pi 3</a>）、各種センサ、 IoT 関連のオープンソース ライブラリを活用して、それらを組み合わせた Web サービスです。</p>
<p>Autodesk は、IoT プラットフォームとして <a href="http://autodeskfusionconnect.com/">&#0160;Fusion Connect</a> をリリースしておりますが、Forge で この API をサポートするかは、まだ検討中です。そのため、このサンプルでは、リアルタイム通信を実現するために外部の Web サービス「<a href="https://pusher.com/">Pusher</a>」や、マイコンボードからデータを送信するために「<a href="https://nodejs.org/">Node.js</a>」、各種センサと通信するためにオープンソースライブラリを利用しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c89285e0970b-pi" style="display: inline;"><img alt="E4-2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c89285e0970b image-full img-responsive" src="/assets/image_399907.jpg" title="E4-2" /></a></p>
<p>※1:&#0160;<a href="https://www.oreilly.co.jp/books/9784873117607/">マイクロサービスアーキテクチャ</a>, オライリー・ジャパン</p>
<p>※2: <a href="http://www.oreilly.co.jp/books/9784873116860/">Web API: The Good Parts</a>,&#0160;オライリー・ジャパン</p>
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
