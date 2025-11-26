---
layout: "post"
title: "AutoCAD JavaScript API TIPS ～ 1 前提知識編"
date: "2015-01-16 01:29:52"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html "
typepad_basename: "autocad-2015-javascript-api-usage1"
typepad_status: "Publish"
---

<p>AutoCAD 2015のJavaScript APIは、Webクライアント技術を活用した様々な新しい機能を提供できる強力なAPIです。</p>
<p>今回は、このJavaScript APIをご利用いただく上で知っておいて頂きたいWebクライント技術についてご紹介させて頂きます。</p>
<p>Webアプリケーションのクライアントサイド開発の分野では、現在はHTML5, JavaScript, CSS3をベースとした実装方法が主流となっております。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c737a437970b-pi" style="display: inline;"><img alt="Languages" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c737a437970b image-full img-responsive" src="/assets/image_502860.jpg" title="Languages" /></a></p>
<p>HTML5は、ブラウザに特定のプラグインをインストールせずにRIA（リッチ・インターネット・アプリケーション）を実現するマークアップ言語として、W3Cによって標準化が勧められ、2014年10月28日に仕様が勧告となりました。</p>
<p>たとえば<a href="http://ja.wikipedia.org/wiki/HTML5" target="_blank" title="HTML5">HTML5</a>の仕様では、次のような機能を実装できるAPIが定義されています。</p>
<ul>
<li>セマンティックス - マイクロデータ、マイクロフォーマット</li>
<li>オフラインとストレージ - App Cache、Web Storage、Indexed DB API、File API</li>
<li>デバイスアクセス - Geolocation API、マイク・カメラ、アドレス帳・カレンダー</li>
<li>接続性 - WebSocket、Server-Sent Events</li>
<li>操作性 - ドラッグ&amp;ドロップ</li>
<li>マルチメディア - audio要素, video要素</li>
<li>3D、グラフィックス、エフェクト - SVG、canvas要素、WebGL、CSS3 3D</li>
<li>パフォーマンスと統合 - Web Workers、XMLHttpRequest Level 2</li>
</ul>
<p>JavaScriptは、その柔軟性から、クロスブラウザ・クロスプラットフォームで動作し、昨今では大規模なWebアプリケーションのサーバサイドでも利用されるようになったスクリプト言語です。Webブラウザが搭載するJavaScriptエンジンの進歩により、実行速度も飛躍的に向上しています。</p>
<p>AutoCADに搭載されているAcWebBrowser.exeは、<a href="https://code.google.com/p/chromiumembedded/" target="_blank" title="Chromium Embedded Framework">Chromium Embedded Framework (CEF)</a>というブラウザフレームワークをホストしています。Chromiumとは、Google社のChromeブラウザが採用しているオープンソースのウェブブラウザのプロジェクト名です。AutoCADをChromiumブラウザで拡張することにより、Chromeブラウザの<a href="https://code.google.com/p/v8/" target="_blank" title="V8">JavaScriptエンジン（V8）</a>とランタイム環境を利用することができるようになっております。またChromiumのデバッグツールを起動することもできます。</p>
<p>CSS（Cascading Style Sheets）は、HTML要素の表示スタイルを定義するための文書ですが、CSS3では、次のようなグラフィックス面での機能が強化されております。</p>
<ul>
<li>トランスフォーメーション - HTML要素の2D、3D変形・回転機能。</li>
<li>アニメーション - HTML要素のアニメーション機能</li>
<li>メディアクエリ - 表示するデバイスごとにスタイルを切り替える機能。</li>
<li>マルチカラムレイアウト - 段組表示機能。</li>
</ul>
<p>AutoCADのJavaSript APIが目指すゴールは、このようなHTML5ベースの技術のユーザーインターフェースをアプリケーション上に統合することで、より豊かな表現と操作性を提供することにあります。</p>
<p>この連載では、JavaScript APIと連携してご利用いただけるHTML5ベースのオープンソースライブラリをご紹介していきたいと思います。</p>
<p>AutoCAD 2015 JavaScript API の概要につきましては、以前紹介したブログ記事を参照してみてください。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2014/05/autocad-2015-javascript-api.html" target="_blank" title="AutoCAD 2015 JavaScript API">AutoCAD 2015 JavaScript API</a></li>
</ul>
<p style="padding-left: 30px;">&#0160;</p>
<p>By Ryuji Ogasawara</p>
