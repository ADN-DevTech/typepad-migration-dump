---
layout: "post"
title: "オートデスク 3D ハッカソンのテクノロジ"
date: "2014-07-18 15:59:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/07/technologies-on-autodesk-3d-hachathon.html "
typepad_basename: "technologies-on-autodesk-3d-hachathon"
typepad_status: "Publish"
---

<p style="text-align: left;">ここでは、<strong>オートデスク 3D ハッカソン</strong> で提供するテクノロジについて、簡単に概要をお伝えします。</p>
<p style="text-align: left;"><strong><span style="color: #ff0000;">オートデスク 3D ハッカソンは、諸藩の事情から、当初、予定していた8月から、10月に開催を延期しました。</span></strong></p>
<p style="text-align: left;">まずは、<strong>Autodesk 360 Tech Preview</strong>（<a href="http://autodesk360.com" target="_blank">http://autodesk360.com</a>） で搭載されたビューイング機能です。いままで、オートデスクでは、DWG ファイルや DWF ファイルなど、ファイル形式によって、さまざまな無償ビューワーを提供してきました。ただ、<a href="http://adndevblog.typepad.com/technology_perspective/2014/03/design-review-and-dwf.html" target="_blank"><strong>Design Review と DWF</strong></a> のブログ記事でご案内したとおり、ここに来て、Design Review の提供が 2013 バージョンで停止されています（DWG TrueView では、最新バージョンの DWG TrueView 2015 がリリースされています）。</p>
<p style="text-align: left;">もちろん、Autodesk 360 が、より高度なビューワー機能を実装しつつある、というのが1つの理由になりうると思います。高度な機能とは、次の点です。</p>
<ul>
<ul>
<li>クラウドでファイル形式を表示可能な形式に変換するため、他社CAD製品のファイル形式を含め、約60のファイル形式を表示出来る。</li>
<li>ストリーミング テクノロジにより、大規模なモデルや図面でも素早く、高速に表示操作が出来る。</li>
<li>全ファイル形式ではありませんが、3D モデルの階層構造を維持して、階層毎に表示を制御出来る。</li>
<li>これも全ファイル形式ではありませんが、3D モデルをリアルタイムに分解表示することが出来る。</li>
<li>モデルを選択して、固有のプロパティを表示させることが出来る。</li>
<li>遠近法を用いた視点設定や背景色を変更できる。</li>
<li>ビューワーとしての基本機能である、拡大、縮小、回転などの視点変更操作や、ホームビューへの復帰がサポートされる。</li>
<li>3D モデルと 2D 図面を表示出来る。</li>
<li>WebGL をサポートするブラウザであれば、プラグインをインストールすることなく利用出来る。</li>
<li>JavaScript API で表示制御を含むさまざまなカスタマイズが出来る。</li>
<li>他の JavaScript ライブラリとの併用が出来る。</li>
<li>オブジェクトは一意に識別できるため、カスタマイズ次第で DB 連携なども可能。当然、オブジェクト選択を含むイベント処理が可能。</li>
<li>ビューワー内に表示される標準 UI の表示制御が出来る。また、Web パーツを用いた独自 UI の実装も可能。</li>
<li>特定オブジェクトへのアニメーション ズーム（フォーカス）が可能。</li>
<li>独自に作成した Web ページやサイトに表示を埋め込んで利用出来る。下記は、Fusion 360 Gallerry に記載されたモデルをビューワーとして埋め込んだものです。</li>
</ul>
</ul>
<p style="text-align: right;"><iframe allowfullscreen="" frameborder="0" height="270" mozallowfullscreen="" src="https://fusion360.autodesk.com/models/b08cf2b5131d6749ec151e0ba1ba3cc4/embed" webkitallowfullscreen="" width="400"></iframe></p>
<ul>
<li>検索機能により、表示中のコンテンツから特定の属性を持つオブジェクトを見つけ出すことが可能。</li>
<li>選択したオブジェクトだけを表示したり、選択したオブジェクトを非表示にすることが可能。</li>
<li>一部制限のあるファイル形式もありますが、半透明なガラスや石材、木材、など、オーサリングツールで付加したマテリアルを表現することが可能。</li>
</ul>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e41e46970c-pi" style="display: inline;"><img alt="Viewing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511e41e46970c image-full img-responsive" src="/assets/image_863359.jpg" title="Viewing" /></a><br />&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e423bc970c-pi" style="display: inline;"><img alt="Viewing2d" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511e423bc970c image-full img-responsive" src="/assets/image_627309.jpg" title="Viewing2d" /></a></p>
<ul>
<li><a href="http://adndevblog.typepad.com/files/autodesk-360-view-data-service-%E3%83%81%E3%83%A9%E3%82%B7-1.pdf"><strong>Autodesk 360 View &amp; Data Service チラシ</strong>をダウンロード</a></li>
</ul>
<p style="text-align: left;">写真から 3D オブジェクトを生成する ReCap PhotoAPI では、写真のアップロードと、各種オプション指定、生成したモデルのダウンロードを REST API で実装することが出来ます。ビューワー機能も同様ですが、Autodesk 360 クラウドへの認証も、トークンを用いてユーザ入力なしに自動化が可能です。なお、ReCap API 自体には、表示機能はありません。下記は ReCap 360 上での操作イメージです。生成された 3D モデルを、どう活用するかはアイデア次第です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73def7dcf970d-pi" style="display: inline;"><img alt="Recap_photo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73def7dcf970d image-full img-responsive" src="/assets/image_647734.jpg" title="Recap_photo" /></a></p>
<ul>
<li><a href="http://adndevblog.typepad.com/files/autodesk-360-recap-photo-service-%E3%83%81%E3%83%A9%E3%82%B7-1.pdf"><strong>Autodesk 360 ReCap Photo Service チラシ&#0160;</strong>をダウンロード</a></li>
</ul>
<p>さて、これらを組み合わせて既存のサービスやアプリケーションにない、新しい発想を <strong>オートデスク 3D ハッカソン</strong>&#0160;で作っていただくことになります。CAD や CG といったオートデスクのユーザ層以外にも、さまざまに応用していただくことが出来る内容になるはずです。</p>
<p><a href="http://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%83%E3%82%B7%E3%83%A5%E3%82%A2%E3%83%83%E3%83%97_(Web%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)" target="_blank"><strong>マッシュアップ</strong></a>で他のサービスと融合させることも出来るので、オリジナルの運用シナリオやストーリー、つまり、アイデア次第で、ユニークなポータルサイトやサービスを作ることも出来るでしょう。</p>
<p><strong>Autodesk University Japan 2014</strong>&#0160;では、ビューイング サービスについて、開発手法や手順を含めた概要をご案内する予定です。ぜひ、Track C Autodesk カスタマイズ の C-2 トラックにお申込みください。お申込みは、<a href="http://au.autodesk.co.jp/2014/tc/adn01/">http://au.autodesk.co.jp/2014/tc/adn01/</a>&#0160;からアクセスしていただけます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
