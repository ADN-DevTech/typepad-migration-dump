---
layout: "post"
title: "Design Review と DWF"
date: "2014-03-12 11:04:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/design-review-and-dwf.html "
typepad_basename: "design-review-and-dwf"
typepad_status: "Publish"
---

<p>昨年来、Design Review や DWF ファイルをカスタマイズして利用されている方々から、両者の将来について質問を受けましたので、今回は、それらについて言及しておきたいと思います。</p>
<p>オートデスクでは、DWG ファイルを表示する DWG TrueView と、DWF を表示してマークアップをする Design Review の 2つのデスクトップ製品を、無償ビューワーとして公開しています。無償製品であるため、原則、<span style="text-decoration: underline;">サポート対象になっていません</span>が、<strong><a href="http://ja.wikipedia.org/wiki/ActiveX" target="_blank">ActiveX コントロール</a>&#0160;</strong>として、API を利用することが出来ます。また、Design Review で表示できる DWF ファイルは、AuroCAD を始めとした有償製品から、2D の図面ファイルや 3D モデルを出力機能を提供し続けています。</p>
<p>次に示すのは、無償ビューワーの系譜です。VoloView は1990年代の後半に米国で最初に有償販売されていたと記憶していますが、いずれのビューワーも、世代によって名称が変わっていることが分かります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d8e4a28970d-pi" style="display: inline;"><img alt="Virwers" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d8e4a28970d image-full img-responsive" src="/assets/image_997536.jpg" title="Virwers" /></a></p>
<p>最近では、DWG TrueView も Design Review も、オートデスクの他のデスクトップ製品と同様に、1 年に 1 回のバージョンアップを繰り返しながら提供されてきました。ただ、昨年 AutoCAD や DWG TrueView に 2014 バージョンが登場した後でも、Design Review 2014 は公開されていません。問い合わせを受ける主な内容は、</p>
<ul>
<li>なぜ、Design Review 2014 が公開されないのか？</li>
</ul>
<p>という点です。</p>
<p>また、Design Review 2014 が公開されていないことが要因となって、</p>
<ul>
<li>オートデスクは DWF ファイル形式のサポートを止めてしまうのか？</li>
</ul>
<p>に発展した質問を受けることがあります。</p>
<p>これらに対する回答は、次のようになるかと思います。</p>
<ul>
<li>Windows プラットフォーム（OS）へのインストールが必要な Design Review は、現在、Autodesk 360 クラウド サービスが提供するビューワー機能で代替できます。</li>
<li>Autodesk 360 は、Windows プラットフォームだけでなく、さまざまなタイプのクライアントで利用できる利点があります。</li>
<li>Autodesk 360 上に表示した DWF ファイルには、Design Review と同等なマークアップや計測機能を適用することが出来ます。</li>
<li>上記理由により、Design Review は 2013 バージョン以降、新バージョンの開発と提供を中止しています。</li>
<li>Design Review をカスタマイズしたり、インターネット接続が出来ないコンピュータ上で DWF ファイルの表示やマークアップする必要があるお客様のために、Design Review 2013 を現在でも&#0160;<a href="http://usa.autodesk.com/design-review/download/" target="_blank">http://usa.autodesk.com/design-review/download/</a> からダウンロード提供し続けています（Select Language で Japanese を選択すると、日本語バージョンのダウンロードが可能です）。</li>
<li>DWF ファイル形式は、今後もオートデスク製品でサポートされます。</li>
</ul>
<p>Autodesk 360 上で表示可能なファイル形式については、過去のブログ記事「<a href="http://adndevblog.typepad.com/technology_perspective/2013/09/supported-files-on-autodesk-360.html" target="_blank"><strong>Autodesk 360 クラウドのサポート ファイル</strong></a>」を参照してみてください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
