---
layout: "post"
title: "AutoCAD LT のカスタマイズについて"
date: "2014-12-22 00:10:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/12/about-customizing-autocad-lt.html "
typepad_basename: "about-customizing-autocad-lt"
typepad_status: "Publish"
---

<p>AutoCAD LT は、AutoCAD とプログラムを共有する CAD ソフトウェアですが、差別化のため、AutoCAD で利用可能ないくつかの機能が省かれています。バージョンによって差別化されている機能に差がありますが、 AutoCAD &#0160;と AutoCAD LT の機能差は、おおまかに次の図で把握できるものと思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c72287fc970b-pi" style="display: inline;"><img alt="Differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c72287fc970b image-full img-responsive" src="/assets/image_555045.jpg" title="Differences" /></a></p>
<p>この図から、AutoCAD で提供されていて AutoCAD LT にない機能は、カスタマイズ、ネットワーク ライセンス、プレゼンテーション/ビジュアライゼーション であることがわかります。最後のプレゼンテーション/ビジュアライゼーションとは、3D モデリング機能やレンダリング機能を指しています。</p>
<p>このカスタマイズについて、AutoCAD LT でのサポート範囲の質問を受けることがありますので、今回は、AutoCAD と AutoCAD LT の「カスタマイズ」機能の違いを明確にしておきたいと思います。</p>
<p>カスタマイズとは CAD を使いやすいように設定する、あるいは 調整することを意味します。AutoCAD や AutoCAD LT は、カスタマイズで調整可能な機能を標準機能として提供しています。まず、AutoCAD と AutoCAD LT のカスタマイズ範囲を表した次の図を見てみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c72286fd970b-pi" style="display: inline;"><img alt="Covered_area" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c72286fd970b image-full img-responsive" src="/assets/image_499316.jpg" title="Covered_area" /></a>&#0160;</p>
<p style="text-align: left;">一口にカスタマイズと言っても、 かない広範囲にカスタマイズできることが分かります。もっとも分かり易いのが、ユーザ インタフェースのカスタマイズです。例えば、よく利用するコマンドをボタン化して、独自のツールバーやリボン タブにまとめたりすることが出来ます。また、独自のハッチング パターンや線種を定義する、定義ファイルのカスタマイズも AutoCAD LT でサポートされています。これらの詳細は、AutoCAD LT オンライヘルプの<a href="http://help.autodesk.com/view/ACDLT/2015/JPN/?guid=GUID-A6FAA0E7-2B4D-4C8E-AA70-3D2CF1E53115" target="_blank"><strong>カスタマイズ</strong></a> 項として詳細を参照することが出来ます。</p>
<p>一方、AutoCAD LT でサポートされていないカスタマイズも存在します。上の図では赤い色で示されているプロセス カスタマイズです。一般には、API を用いた開発作業でアドイン アプリケーションを作成して、AutoCAD にロード後に、独自に定義した処理（コマンド）を実行させる方法と、考えることが出来ます。また、マウス操作を記録して、再利用できるようにするアクション レコーダーという機能も、AutoCAD LT では搭載されていません(サポートされていません）。もし、AutoLISP や ObjectARX、.NET API 等で作成されたアドイン アプリケーションをロードして実行するような状態の&#0160;AutoCAD LT を見かけたら、注意が必要です。オートデスクがサポートしない不正な方法が利用されている可能性があります。</p>
<p style="text-align: left;">なお、API カスタマイズには、アドイン アプリケーションだけでなく、COM API（ActiveX オートメーション）を利用して、外部アプリケーションから AutoCAD をコントロールするものも存在します。もちろん、AutoCAD LT では利用できません。AutoCAD API の概要は、過去のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/02/autocad-api-helps-cutomizing-autocad.html" target="_blank"><strong>AutoCAD のカスタマイズを手助けする AutoCAD API</strong></a>&#0160;をご参照ください。最新バージョンの AutoCAD では、<a href="http://adndevblog.typepad.com/technology_perspective/2014/05/autocad-2015-javascript-api.html" target="_blank"><strong>JavaScript API</strong></a> もサポートされるようになっています。繰り返しますが、AutoCAD API の利用は AutoCAD LT ではサポートされていません。</p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p style="text-align: left;">&#0160;</p>
