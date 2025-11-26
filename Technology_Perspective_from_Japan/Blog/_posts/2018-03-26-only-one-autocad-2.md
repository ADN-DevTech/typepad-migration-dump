---
layout: "post"
title: "Only One AutoCAD - モバイル アプリと Web アプリ"
date: "2018-03-26 00:23:37"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/only-one-autocad-2.html "
typepad_basename: "only-one-autocad-2"
typepad_status: "Publish"
---

<p>AutoCAD 2017 以来、AutoCAD/AutoCAD LT サブスクライバ（サブスクリプション契約をお持ちの方）には、特典として&#0160;Apple iOS、Google Android、Microsoft Windows のプラットフォーム毎に用意されたモバイル アプリの利用権を提供されています。リリースされたばかりの AutoCAD 2019 も例外なく、この特典を利用することが出来ます。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/my1zG5DAB9g?feature=oembed" width="500"></iframe></p>
<p class="asset-video">AutoCAD モバイル アプリですが、少し前には <strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/05/update-autocad-360-mobile-app.html" rel="noopener noreferrer" target="_blank">AutoCAD 360 Mobile App</a></strong> と呼ばれていたことをご記憶かと思います。また、AuttoCAD 360 Mobile App とともに AutoCAD 360 には Web ブラウザで利用する<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/12/about-autocad-360-web.html" rel="noopener noreferrer" target="_blank">&#0160;AutoCAD 360 Web</a></strong> も存在していました。そして、AutoCAD 360 は、クラウドを利用した現在の形態で登場した際、<strong>AutoCAD WS</strong> と呼ばれていました。当初、Adobe Systems 社の Adobe Flash Player を利用する Web 版のみの提供でしたが、後に iOS 版アプリ、Android 版アプリが順次登場した経緯があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e47b6d970c-pi" style="display: inline;"><img alt="Autocad_ws" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e47b6d970c image-full img-responsive" src="/assets/image_988077.jpg" title="Autocad_ws" /></a></p>
<p>ご存じの方もいらっしゃるかと思いますが、実は、この AutoCAD WS、当時イスラエルで設立さた Visual Tao 社の製品をオートデスクが買収したものです。買収当時、AutoCAD WS は TrustedDWG テクノロジを利用していなかったため、オートデスクは&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/09/about-realdwg.html" rel="noopener noreferrer" target="_blank">RealDWG</a></strong> をモバイルに移植するなどして、一貫した図面表現が出来るように努めてきました。ところが、残念ながら各国の多様な業種、及び、設定で扱われる DWG ファイルの解釈、表示において、特に Web 版で一部オブジェクトの表現に差異が発生してしまっていたようです。それが、過去のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/12/about-autocad-360-web.html" rel="noopener noreferrer" target="_blank">AutoCAD 360 Web について</a></strong> でご案内した Web 版のメンテナンスに至る結果となっています。</p>
<p>経緯を簡単にまとめると、次のような歴史があります。</p>
<p style="padding-left: 30px;">2009 年: Visual Tao 社を買収</p>
<p style="padding-left: 60px;">Adobe Flash を利用した Web アプリを AutoCAD WS として公開</p>
<p style="padding-left: 60px;">&gt;&gt; AutoCAD WS &gt;&gt; 後に AutoCAD 360 に名称変更</p>
<p style="padding-left: 30px;">2011 年: iOS 版用に C++ でコード書き換え AutoCAD WS Mobile 第一弾として公開</p>
<p style="padding-left: 30px;">2012 年: Android 版リリース&#0160;AutoCAD WS Mobile 第二弾として公開</p>
<p style="padding-left: 30px;">2014 年: HTML5 版リリース</p>
<p style="padding-left: 60px;">Java を介して C++ コードからフォーク</p>
<p style="padding-left: 30px;">2015 年: AutoCAD 360 Web をメインテンスモードへ移行し、DWG エンジンを共通化する新プロジェクトを発足</p>
<p>ここでご注目いただきたいのが、2015 年、Mobile 版、Web 版の DWG エンジンをデスクトップ版の DWG エンジンと共通化するために立ち上げられた Project Fabric です。このプロジェクトの目的は、デバイスを問わず利用出来る&#0160;<strong>One AutoCAD</strong> であり、デスクトップ版から継承する完全な&#0160; DWG エンジンを利用する&#0160;<strong>Original AutoCAD</strong> の提供です。</p>
<p style="padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fd6ed0970d-pi" style="display: inline;"><img alt="Project_fabric" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fd6ed0970d image-full img-responsive" src="/assets/image_723857.jpg" title="Project_fabric" /></a></p>
<p>今回、AutoCAD 2019/AutoCAD LT 2019 のリリースと共に AutoCAD サブスクライバの皆様にに提供されるのは、前述の AutoCAD モバイル アプリ同様、Project Fabric の成果として登場した&#0160; AutoCAD Web アプリの利用権でもあるのです。もちろん、従来通り、Web ブラウザで利用することが出来ます。</p>
<p>新たに登場した AutoCAD Web アプリは、Project Fabric を導入して初めての登場なので、まだ、完璧とは言えない部分もあるかも知れません。たたし、デスクトップ版 AutoCAD と同じ DWG エンジンを採用しているのです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e47c68970c-pi" style="display: inline;"><img alt="Fabric_cross_platform" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e47c68970c image-full img-responsive" src="/assets/image_545692.jpg" title="Fabric_cross_platform" /></a>&#0160;</p>
<p>AutoCAD 2019/AutoCAD LT 2019 サブスクライバの方には、AutoCAD モバイル アプリと同様に <strong><a href="https://web.autocad.com" rel="noopener noreferrer" target="_blank">https://web.autocad.com</a></strong> から簡単にアクセス出来る AutoCAD Web アプリを使用・評価いいただきたいと思います（アクセスには Google Chrome を推奨）。</p>
<p class="asset-video"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/5_wknH4U1c8?feature=oembed" width="500"></iframe></p>
<p class="asset-video">By Toshiaki Isezaki</p>
