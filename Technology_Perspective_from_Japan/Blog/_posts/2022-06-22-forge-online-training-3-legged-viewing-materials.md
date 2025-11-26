---
layout: "post"
title: "Forge Online Training - 3-legged Viewing 収録公開"
date: "2022-06-22 00:31:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/06/forge-online-training-3-legged-viewing-materials.html "
typepad_basename: "forge-online-training-3-legged-viewing-materials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d3f45c1200b-pi" style="display: inline;"><img alt="AUTODESK_Forge_Training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d3f45c1200b image-full img-responsive" src="/assets/image_100135.jpg" title="AUTODESK_Forge_Training" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eec70520200d-pi" style="display: inline;"><br /></a></p>
<p>去る 2022 年 6 月 15 日に、Web ページ上に Forge Viewer に A360 、BIM 360 Docs、Fusion Team 等に格納されているデザインデータを表示させるためのオンライン トレーニングを開催しました。</p>
<p>当日は、補足も交え、<a href="https://learnforge.autodesk.io/#/ja-JP/" rel="noopener" target="_blank">Learn Forge</a> コンテンツを利用しています。具体的には、Web サーバー実装に Node.js と Forge SDK 、クライアント実装に Forge Viewer を使った Forge アプリを構築しています。</p>
<p>使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。ページ中には後日参照可能な数多くのリンクが埋め込まれていますので、このページに記載した収録動画とともにご確認ください。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b02a30d3e6b7d200b img-responsive"><a href="https://adndevblog.typepad.com/files/forge-training---2-legged.pdf" rel="noopener" target="_blank"> </a><a href="https://adndevblog.typepad.com/files/forge-training---3-legged.pdf"><strong>Forge Training - 3-legged.pdf</strong> をダウンロード</a></span></p>
<p><strong>前提：</strong></p>
<ul>
<li>トレーニングでは Node.js と VS Code を利用します。次のブログ記事を事前にご確認の上、必要となるツールや環境のインストールをお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank"><strong>Forge の開発環境</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>Forgeのデベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>Forge API を利用するアプリの登録とキーの取得</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank"><strong>Forge 開発に際して...</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a>&#0160;をご一読いただくことをお勧めします。</li>
</ul>
<hr />
<p style="padding-left: 40px;"><strong>はじめに</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Qx0V8jjM8N8" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>Forge Viewer 利用手順の理解</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/PAwDMwIEUFw" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>Learn Forge：モデルを表示する</strong></p>
<p style="padding-left: 80px;"><strong>サーバを作成する</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/dcWG8kbGSHI" width="480"></iframe></p>
<p style="padding-left: 80px;"><strong>認可する</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/ccAdTeCN1n8" width="480"></iframe></p>
<p style="padding-left: 80px;"><strong>ハブとプロジェクトを一覧表示する</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Bc-gn6pGEOM" width="480"></iframe></p>
<p style="padding-left: 80px;"><strong>ユーザ情報</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/2x-tQW96b2o" width="480"></iframe></p>
<p style="padding-left: 80px;"><strong>ビューアに表示する</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/muyWWDZgEUs" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>付録：Forge Viewer カスタマイズ その２</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/cGx4MyC31j8" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>付録：BIM 360 Docs アクセスについて</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/FyYOnx0QOgM" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>付録：コストについて</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/8xnXZ3F7_5I" width="480"></iframe></p>
<hr />
<p>By Toshiaki Isezaki</p>
