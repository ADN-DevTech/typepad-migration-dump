---
layout: "post"
title: "CPU とグラフィックスカード ～ その後"
date: "2015-07-13 03:08:48"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/07/cpu-and-graphics-card-afterward.html "
typepad_basename: "cpu-and-graphics-card-afterward"
typepad_status: "Publish"
---

<p>2 年程前に、<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/cpu-and-graphics-card.html" target="_blank"><strong>CPU とグラフィックスカード</strong></a> と題して、AutoCAD を利用する上での CPU のビット数とグラフィックス カードについてご紹介しました。あれから、たった 2 年しか経過していませんが、状況はだいぶ変わってきてしまったと感じます。</p>
<p>CPU ビット数でいえば、AutoCAD 2016 や AutoCAD LT 2016、DWG TrueView 2016 といった製品を除いて、主要なオードデスク製品は 64 ビット版 Windows しかサポートしなくなってきています。背景には Windows を実行するためのコンピュータ自身が数年前から 64 ビット化されていて、それらに搭載されてくる Windows OS も 64 ビット版が一般化していることがあるのだと思います。もちろん、64 ビット版 Windows で 64 ビット版のオートデスク製品を実行すると、32 ビット版に比べて多くのメモリを利用できる利点を広めようとする意図もあります。</p>
<p>ビューワー製品に関しては、DWF/DWFx ファイルを閲覧するための Design Review が、Design Review 2013 を最後に新バージョンの開発を停止しています。 Design Review は 32 ビット版のみの提供でしたが、64 ビット版 Windows 上で互換モードを用いて利用することが可能です。DWG ファイルを閲覧するための DWG TrueView は、現在でも DWG TrueView 2016 として 32 ビット版と 64 ビット版の両者を1つのインストーラから利用することが出来ます。DWG TrueView インストーラは、<a href="http://www.autodesk.co.jp/products/dwg/viewers" target="_blank">こちら</a> からダウンロード出来ます。</p>
<p>こういったデスクトップ製品とは別に、クラウド上で実行した機能を Web ブラウザで利用しようとする動きもあります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0851ad82970d-pi" style="display: inline;"><img alt="Offline_to_onliine" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0851ad82970d image-full img-responsive" src="/assets/image_64289.jpg" title="Offline_to_onliine" /></a></p>
<p>ビューワー機能では、オンラインで実現する <strong><a href="https://360.autodesk.com/viewer" target="_blank">A360 ビューワー（別名、オンライン ビューワー）</a>&#0160;</strong>が該当します。もし、ご存じなければ、次のブログ記事をご一読いただくことをお勧めします。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/easy-way-to-test-a360-viewer.html" target="_blank"><strong>簡単に A360 ビューワー機能を評価する方法</strong></a></p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank"><strong>A360 ビューワーの新機能</strong></a></p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2015/06/a360-viewer-widget.html" target="_blank"><strong>A360 ビューワー ウィジェット</strong></a></p>
<p>Web ブラウザを利用した A360 ビューワーの場合、Web ブラウザのビット数と Windows OS のビット数で、AutoCAD などのオートデスク製品と同じメモリ上の制限などが発生します。このため、最近では Web ブラウザの 64 ビット化も進んでいます。同様に、<strong>ハードウェア アクセラレーション</strong> の設定名称で、Web ブラウザ自身がコンピュータに搭載されているグラフィックス カードを利用するようにもなってきています。</p>
<p style="padding-left: 30px;"><a href="https://technet.microsoft.com/ja-jp/library/gg699417.aspx" target="_blank"><strong>Internet Explorer</strong></a></p>
<p style="padding-left: 30px;"><a href="https://support.google.com/chrome/answer/1220892?hl=ja" target="_blank"><strong>Google Chrome</strong></a></p>
<p style="padding-left: 30px;"><a href="https://support.mozilla.org/ja/kb/upgrade-graphics-drivers-use-hardware-acceleration" target="_blank"><strong>Mozilla Firefox</strong></a></p>
<p>CAD 製品とグラフィックス カードの組み合わせで発生する問題を低減する目的で導入された <strong>認定グラフィックス カード</strong> の仕組みですが、Web ブラウザとの組み合わせで同じような問題が起こらないことを祈るばかりです。</p>
<p>グラフィックス カードを利用する製品の中では、AutoCAD LT でもグラフィックスカードを利用する機能が増えてきいる点が特筆すべき点と思います。直近の機能として、AutoCAD LT 2015 で登場した&#0160;<strong>スムーズライン&#0160;</strong>や、 AutoCAD LT 2016 で登場した&#0160;<strong>高品質ジオメトリ&#0160;</strong>の機能でグラフィックス カードを利用しています。</p>
<p style="text-align: center;">&lt;クリックして拡大&gt;<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ad9bf9970b-pi" style="display: inline;"><img alt="Comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ad9bf9970b image-full img-responsive" src="/assets/image_362577.jpg" title="Comparison" /></a></p>
<p>グラフィックス カードの利用というと、いままでは、3D モデルの描画を高速にする手段として認識されてきましたが、最近では、2D の描画に加えて、精緻な表示などにも応用され始めていることをご理解いただけるはずです。これらの設定は、AutoCAD や AutoCAD LT の&#0160;<strong id="GUID-14929CF2-8E39-4F87-B8C2-18440B12922E__GUID-26A4346B-3885-45E8-811E-6DEACA05DA06">GRAPHICSCONFIG コマンド で設定を変更することが出来ます。</strong></p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ad981d970b-pi" style="display: inline;"><img alt="Lt_graphics_performance" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ad981d970b img-responsive" src="/assets/image_822075.jpg" style="width: 400px;" title="Lt_graphics_performance" /></a></p>
<p>AutoCAD や AutoCAD LT で利用すべきグラフィックスカードは、従来通り、認定グラフィックス カード検索ページで検索することが出来ます。</p>
<p style="text-align: center;"><span style="font-size: 18pt;"><strong><a href="http://www.autodesk.co.jp/autocad-graphicscard">http://www.autodesk.co.jp/autocad-graphicscard</a></strong></span></p>
<p>ただし、AutoCAD LT は AutoCAD と同等とみなされているので、&quot;AutoCAD LT&quot; の製品名は表示されません。代わりに、AutoCAD で検索してみてください。</p>
<p>By Toshiaki Isezaki</p>
