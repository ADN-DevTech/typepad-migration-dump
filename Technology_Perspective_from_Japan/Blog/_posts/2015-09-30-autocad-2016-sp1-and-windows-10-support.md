---
layout: "post"
title: "AutoCAD 2016 SP1 と Windows 10 サポートについて"
date: "2015-09-30 18:53:02"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/09/autocad-2016-sp1-and-windows-10-support.html "
typepad_basename: "autocad-2016-sp1-and-windows-10-support"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15fc5b9970c-pi" style="float: right;"><img alt="Win10" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15fc5b9970c img-responsive" src="/assets/image_777555.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Win10" /></a></p>
<p>AutoCAD 2016 と AutoCAD LT 2016 に Service Pack 1 がリリースされて、Windows 10 をサポートできるようになりました。Windows 10 上で AutoCAD 2016 や AutoCAD LT 2016 を利用する場合には、事前に SP1 を適用してください。なお、サポートされる Windows 10 のエディションは、<strong>Windows 10 Pro</strong>&#0160;と&#0160;<strong>Windows 10 Enterprise</strong>&#0160;のみになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d162a5b1970c-pi" style="display: inline;"><img alt="Win10-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d162a5b1970c image-full img-responsive" src="/assets/image_467868.jpg" title="Win10-1" /></a></p>
<p>もし、既に AutoCAD 2016 や AutoCAD LT 2016 製品をインストールしている場合には、Autodesk Application Manager を通じて通知が表示され、インストール指示をおこなうことが出来るはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d69990970b-pi" style="display: inline;"><img alt="App_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d69990970b image-full img-responsive" src="/assets/image_76686.jpg" title="App_manager" /></a></p>
<p>SP1 は、下記のページから直接ダウンロード ページから入手することも出来ます。SP1 で修正された問題などの詳細は、両ページにリンクされている日本語の Readme ドキュメントをご参照ください。&#0160;</p>
<p><a href="http://knowledge.autodesk.com/ja/support/autocad/downloads/caas/downloads/downloads/JPN/content/autocad-2016-service-pack-1.html" target="_blank"><strong>AutoCAD 2016 Service Pack 1 ダウンロード</strong></a></p>
<p><a href="http://knowledge.autodesk.com/ja/support/autocad-lt/downloads/caas/downloads/downloads/JPN/content/autocad-lt-2016-service-pack-1.html" target="_blank"><strong>AutoCAD 2016 LT Service Pack 1 ダウンロード</strong></a></p>
<p>さて、今回の SP1 は、以前のバージョン用に提供されている Service Pack とは適用範囲が少し異なります。AutoCAD 2016 シリーズには、AutoCAD 2016 単体として販売されている製品とは別に、AutoCAD 2016 をベースにした業種別製品が存在します。具体的には、機械設計用の A<a href="http://www.autodesk.co.jp/products/autocad-mechanical/overview" target="_blank"><strong>utoCAD Mechanical</strong></a>、電機設計用の <a href="http://www.autodesk.co.jp/products/autocad-electrical/overview" target="_blank"><strong>AutoCAD Electrical</strong></a>、2D ベースの GIS、マッピング用の <a href="http://www.autodesk.co.jp/products/autocad-map-3d/overview" target="_blank"><strong>AutoCAD Map 3D</strong></a>、3D ベースの土木設計用 <a href="http://www.autodesk.co.jp/products/autocad-civil-3d/overview" target="_blank"><strong>AutoCAD Civil 3D</strong></a> などの製品が、これに該当します。</p>
<ul>
<li>注意：AutoCAD の業種別製品は、英語だと「Vertical Products」と表現されるので、自動翻訳だと「垂直製品」と訳されてしまいますが、正しくは「業種別製品」が適訳です。</li>
</ul>
<p>AutoCAD 2015 シリーズまでの業種別製品では、Service Pack が各製品用に個別に用意されていました。別の言い方をするなら、AutoCAD Mechanical 2015 には、AutoCAD 2015 の SP 1 を適用することが出来ず、AutoCAD Mechanical 2015 SP1 を待って適用する必要がありました。</p>
<p>AutoCAD 2016 シリーズでは、従来の慣例を破り、AutoCAD 2016 SP1 を AutoCAD 2016 だけではなく、AutoCAD 2016 シリーズの業種別製品にも適用できるようになっています。SP1 適用時の確認は、<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-CDBAD44E-F661-430C-A99E-192B83D41C10" target="_blank"><strong>ABOUT[バージョン情報] &#0160;コマンド</strong></a>でおこなうことが出来ます。</p>
<p>例えば、AutoCAD Mechanial 2016 をインストールした直後に ABOUT コマンドを実行すると、&quot;ビルド&quot; 表記でベースになっている AutoCAD 2016 のビルド番号が表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15fd842970c-pi" style="display: inline;"><img alt="Acm_rtm2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15fd842970c image-full img-responsive" src="/assets/image_869253.jpg" title="Acm_rtm2" /></a></p>
<p>この AutoCAD Mechanical 2016 に AutoCAD 2016 SP1 を適用すると、 ベースになっている AutoCAD 2016 のビルド番号だけが更新されることがわかります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb087a08db970d-pi" style="display: inline;"><img alt="Acm_sp12" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb087a08db970d image-full img-responsive" src="/assets/image_191345.jpg" title="Acm_sp12" /></a></p>
<p>この状態の AutoCAD Mechanical 2016 には、ベースとなった AutoCAD 2016 の機能修正のみが含まれます。少し紛らわしいのですが、AutoCAD Mechanical 2016 の固有機能に関する修正モジュールは、AutoCAD Mechanical 2016 SP1 として個別にリリースされる予定です。</p>
<p>By Toshiaki Isezaki</p>
