---
layout: "post"
title: "Design Review 新リリースの注意点"
date: "2017-04-12 00:06:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/04/caution-for-new-release-of-design-review.html "
typepad_basename: "caution-for-new-release-of-design-review"
typepad_status: "Publish"
---

<p>お気づきの方も多いかと思いますが、一部の 2018 バージョン製品をインストールすると、Design Review の新しいリリースが同時にインストールされるようになっています。&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ebb4c6970b-pi" style="display: inline;"><img alt="Design_review_with_2018" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ebb4c6970b image-full img-responsive" src="/assets/image_681133.jpg" title="Design_review_with_2018" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d27610eb970c-pi" style="float: right;"><img alt="Design-review-icon-128px-hd" class="asset  asset-image at-xid-6a0167607c2431970b01b8d27610eb970c img-responsive" src="/assets/image_288233.jpg" style="margin: 0px 0px 5px 5px;" title="Design-review-icon-128px-hd" /></a>また、米国のダウンロードサイト（<a href="http://www.autodesk.com/products/design-review/download" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/products/design-review/download</a>）から&#0160; Design Review をダウンロードすると、同じく新しいバージョンの Design Review を入手することが出来ます。現在のところ、日本語のダウンロードサイト（<a href="http://www.autodesk.co.jp/products/design-review/download" rel="noopener noreferrer" target="_blank">http://www.autodesk.co.jp/products/design-review/download</a>）から製品をダウンロードした場合には、旧バージョンとなる Design Review 2013 がダウンロードされます（2017年4月12日現在）。</p>
<p>以前のブログ&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/03/design-review-and-dwf.html" rel="noopener noreferrer" target="_blank">Design Review と DWF</a>&#0160;</strong>でご案内しているとおり、Design Review は 2012年にリリースされた <strong>Design Review 2013</strong> を最後にバージョンアップを停止していました。その代替として登場したのが、Autodesk Forge をベースにするクラウドを利用したオンライン ビューアです。A360 Team などのクラウド サービス内で使用できるほか、無償で利用できるものとして <a href="https://a360.autodesk.com/viewer/" rel="noopener noreferrer" target="_blank">https://a360.autodesk.com/viewer/</a>&#0160;からアクセス可能な A360 Viewer があります。これらオンライン ビューア サービスは、Design Review のようにクライアント コンピュータに何もインストールする必要はなく、Web ブラウザがあれば利用出来るので、 より手軽にビューア機能を使用出来る利点があります。&#0160;</p>
<p>それでは、なぜ、いまになって新しいバージョンが登場したのでしょう？</p>
<p>理由はとても単純です。Windows 10 で安定して動作させるためです。</p>
<p>前述のとおり、現在でも Autodesk Vault などの一部オートデスク デスクトップ製品が内部的に DWF ファイルを扱う目的で Design Review を使用しています。ただ、Design Review 2013 が Windows 10 をサポートしていないため、さまざまな不都合が発生していました。ちょうど、Autodesk Knowledge Network 記事&#0160;<strong><a href="https://knowledge.autodesk.com/ja/support/navisworks-products/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000mPum.html" rel="noopener noreferrer" target="_blank">Microsoft Windows 10 対応の DWF ビューアソフトはあるのか</a>&#0160;</strong>で説明されているのもその 1 つです。</p>
<p>新しい Design Review は Windows 10 上で動作するような対応が施されたのみで、ビューア機能に拡張はありません。A360 や BIM 360 Docs、A360 Viewer へアクセスするためのボタンが新設されていますが、新機能と言えるものではありません。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d276130e970c-pi" style="display: inline;"><img alt="New_features_on_new_version" class="asset  asset-image at-xid-6a0167607c2431970b01b8d276130e970c img-responsive" src="/assets/image_893976.jpg" style="width: 620px;" title="New_features_on_new_version" /></a></p>
<p>このため、今後とも Design Review の ActiveX コントロール を利用したソリューションの新規開発や継続利用をお勧めするものではありませんのでご注意ください（もともと未サポートです）。可能であれば、Windows プラットフォーム以外でも利用可能な Forge Viewer を使ったソリューション開発をお勧めします。</p>
<p>なお、新バージョンの Design Review には、他の製品のように 2018 等のバージョン名が付けられていない点に注目ください。次の画像は、Design Review 2013 の起動時に表示されるスプラッシュ スクリーン（左）と、新バージョンのスプラッシュ スクリーン(右）です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2761270970c-pi" style="display: inline;"><img alt="Splash_screens" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2761270970c image-full img-responsive" src="/assets/image_328565.jpg" title="Splash_screens" /></a></p>
<p>場合によっては今後も改良バージョンがリリースされる可能性がありますが、すくなくとも、毎年、新しいバージョンがリリースされるわけではない、との意味があります。</p>
<p>By Toshiaki Isezaki</p>
