---
layout: "post"
title: "業種別 AutoCAD ベース製品の API カスタマイズ"
date: "2013-06-26 05:50:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/06/vertical-autocad-api-1.html "
typepad_basename: "vertical-autocad-api-1"
typepad_status: "Publish"
---

<p>AutoCAD 2014 になって、AutoCAD API には新しく JavaScript API が加わりました。これで事実上 5 つめの API が AutoCAD 上で利用できるようになったわけですが、これとは別に、よく混乱の原因となる API の問題があります。それが、AutoCAD をベースとした業界別の製品が提供する API の存在です。</p>
<p>オートデスクは、AutoCAD API を使って、製造、建築、土木、電気などの業界別製品を販売しています。それぞれ、AutoCAD Mechanical、AutoCAD Architecture、AutoCAD Map3D や Civil 3D、AutoCAD Electrical が、これに該当します。それぞれの製品は、AutoCAD の機能を土台にして、API カスタマイズで業種に特化したコマンドや機能が盛り込まれています。&#0160;</p>
<p style="text-align: center;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901bb4dd63970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="BaseProducts" src="/assets/image_129334.jpg" title="BaseProducts" /></a></p>
<p>これら、AutoCAD ベース製品でも、標準の AutoCAD が持つ AutoCAD API を利用していただくことが可能です。ただし、カスタマイズの対象となるのは標準の AutoCAD がも持つ機能に関連するもののみで、業界別に盛り込まれた機能が、そのまま全ての API でカスタマイズできるとは限りません。</p>
<p>業界別の AutoCAD は、その専用機能を特定の API でのみ公開しています。理由はさまざまですが、買収よってオートデスクが手に入れた製品であったり、開発を始めた時期による、と考えていいのかも知れません。次の表は、AutoCAD ベースの業界別製品の専用機能は、どの AutoCAD API で公開されているか簡単にまとめたものです。</p>
<p style="text-align: center;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eeab22ddc970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="APIs" src="/assets/image_808131.jpg" title="APIs" /></a></p>
<p>加えて、これらの API でも、業界別製品が提供するすべての専用機能が、API 公開されているわかではない、と考えるべきです。例えば、AutoCAD Mechanical は、1990 年代後半に、AutoCAD 用の機械設計用アプリケーションだった Genius という製品を買収したのが始まりです。買収後には、オートデスク自身はアドオン アプリケーションの開発をするようになり、製品名も変更しました。</p>
<p>Genius には、パワーディメンション と呼ばれる専用の寸法機能がありますが、残念ながら、パワーディメンションは API としては公開されていません。一方、買収後に実装された ストラクチャ といった機能は、Mechanical API として公開されています。先の表で、AutoCAD Mechanical の ObjectARX と ActiveX/COM が △ になっているのは、そういった意味合いがあります。もちろん、acedCommand() や (comnmand) といった関数、あるいは、sendStringToExecute() や SendMessage などの方法で、用意されている専用コマンドを呼び出すことは出来ますが、引数やパラメータ、戻値りのチェックによる分岐、といった詳細な制御ができるApplication Programming Interface としての利用は出来ません。</p>
<p>もし、AutoCAD ベースの業界別製品を用いて、用意された専用機能をカスタマイズしようとされる場合には、どのAPIで、どの範囲の API が公開されているか、事前に十分調べていただくことをお勧めします。</p>
<p>情報の入手先は、<a href="http://adndevblog.typepad.com/technology_perspective/2013/04/sdk_for_new_versions.html" target="_self">デスクトップ製品の新バージョン用 SDK の入手方法</a> で紹介した URL や、<a href="http://www.autodesk.co.jp/developerplatforms">http://www.autodesk.co.jp/developerplatforms</a> 配下のページ、ADN Extranet（<a href="http://adn.autodesk.com">http://adn.autodesk.com</a>）などがあります。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
