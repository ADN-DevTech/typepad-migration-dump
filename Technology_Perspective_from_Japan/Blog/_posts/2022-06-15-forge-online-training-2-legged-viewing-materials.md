---
layout: "post"
title: "Forge Online Training - 2-legged Viewing 収録公開"
date: "2022-06-15 00:36:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/06/forge-online-training-2-legged-viewing-materials.html "
typepad_basename: "forge-online-training-2-legged-viewing-materials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eec7050a200d-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eec7dbc0200d-pi" style="display: inline;"><img alt="AUTODESK_Forge_Training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eec7dbc0200d image-full img-responsive" src="/assets/image_134606.jpg" title="AUTODESK_Forge_Training" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eec7050a200d-pi" style="display: inline;"><br /></a></p>
<p>去る 2022 年 6 月 8 日に、Web ページ上に Forge Viewer を使って 3D モデルや 2D 図面を表示させるためのオンライン トレーニングを開催しました。</p>
<p>当日は、補足も交え、<a href="https://learnforge.autodesk.io/#/ja-JP/" rel="noopener" target="_blank">Learn Forge</a> コンテンツを利用しています。具体的には、Web サーバー実装に Node.js と Forge SDK 、クライアント実装に Forge Viewer を使った Forge アプリを構築しています。</p>
<p>使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。ページ中には後日参照可能な数多くのリンクが埋め込まれていますので、このページに記載した収録動画とともにご確認ください。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b02a30d3e6b7d200b img-responsive"><a href="https://adndevblog.typepad.com/files/forge-training---2-legged.pdf" rel="noopener" target="_blank"><strong>Forge Training - 2-legged.pdf</strong>をダウンロード</a></span></p>
<p><strong>前提：</strong></p>
<ul>
<li>トレーニングでは Node.js と VS Code を利用します。次のブログ記事を事前にご確認の上、必要となるツールや環境のインストールをお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank"><strong>Forge の開発環境</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>Forgeのデベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>Forge API を利用するアプリの登録とキーの取得</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank"><strong>Forge 開発に際して...</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a>&#0160;をご一読いただくことをお勧めします。</li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/how-to-get-started-forge.html" rel="noopener" target="_blank">Forge トライアル</a>期間のアカウントをお使いいただければ、100 クラウドクレジットがアカウントに付与されていますので、Model Derivative API などの使用を実質無償でお使いいただけます（100 クラウドクレジットをすべて消費、または 90 日を経過するまで）。アカウントのステータスは、<a href="https://adndevblog.typepad.com/technology_perspective/2020/09/expired-torge-rials.html">Forge トライアルの終了で起こること</a> の内容をご確認ください。</li>
</ul>
<hr />
<p style="padding-left: 40px;"><strong>はじめに</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/toEyh0EMgrM" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>Forge Viewer 利用手順の理解</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/jXLDR2jbGrc" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>Learn Forge：モデルを表示する</strong></p>
<p style="padding-left: 80px;"><strong>サーバを作成する</strong></p>
<p style="padding-left: 40px; text-align: center;"><strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/kkmYvg3tTks" width="480"></iframe></strong></p>
<p style="padding-left: 80px;"><strong>認証する</strong></p>
<p style="padding-left: 40px; text-align: center;"><strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/iamk_0Y8Jfg" width="480"></iframe></strong></p>
<p style="padding-left: 80px;"><strong>ファイルを OSS にアップロードする</strong></p>
<p style="padding-left: 40px; text-align: center;"><strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/LqmHJuf9UQ0" width="480"></iframe></strong></p>
<p style="padding-left: 80px;"><strong>ファイルを変換する</strong></p>
<p style="padding-left: 40px; text-align: center;"><strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/w8P9PR32LZ8" width="480"></iframe></strong></p>
<p style="padding-left: 80px;"><strong>ビューアに表示する</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/kbyENY0Tdzc" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>付録：Forge Viewer カスタマイズ その１</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/H1r3FukxxlY" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>付録：コストについて</strong></p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/acdoN09YLLU" width="480"></iframe></p>
<hr />
<p>By Toshiaki Isezaki</p>
