---
layout: "post"
title: "Forge Online Training - Design Automation AutoCAD 収録公開"
date: "2022-10-05 00:42:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/10/forge-online-training-design-automation-autocad-materials.html "
typepad_basename: "forge-online-training-design-automation-autocad-materials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed7c914200d-pi" style="display: inline;"><img alt="AUTODESK_Forge_Training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed7c914200d image-full img-responsive" src="/assets/image_3772.jpg" title="AUTODESK_Forge_Training" /></a></p>
<p>2022 年 9 月 28 日に、クラウド上の仮想環境に用意した AutoCAD コアエンジンを利用して、DWG の作成や編集、情報の収集などの処理を自動化する Design Automation API for AutoCAD を把握いただくオンライン トレーニングを開催しました。</p>
<p>当日は、補足も交え、<a href="https://learnforge.autodesk.io/#/ja-JP/" rel="noopener" target="_blank">Learn Forge</a> コンテンツを利用しています。Web サーバー実装に Node.js と Forge SDK&#0160; 使って Forge アプリを構築しています。</p>
<p>使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。ページ中には後日参照可能な数多くのリンクが埋め込まれていますので、このページに記載した収録動画とともにご確認ください。</p>
<p style="padding-left: 40px;"><strong> <span class="asset  asset-generic at-xid-6a0167607c2431970b02a308e1c212200c img-responsive"><a href="https://adndevblog.typepad.com/files/forge-training---design-automation-autocad.pdf" rel="noopener" target="_blank">Forge Training - Design Automation AutoCADをダウンロード</a></span></strong></p>
<p><strong>前提：</strong></p>
<ul>
<li>トレーニングでは Node.js と VS Code を利用します。次のブログ記事を事前にご確認の上、必要となるツールや環境のインストールをお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank"><strong>Forge の開発環境</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>Forgeのデベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、デベロッパキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお薦めします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>Forge API を利用するアプリの登録とキーの取得</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank"><strong>Forge 開発に際して...</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a>&#0160;をご一読いただくことをお勧めします。</li>
<li>クラウド上での AutoCAD .NET API アドイン実装の把握には、<a href="https://adndevblog.typepad.com/technology_perspective/2022/07/autocad-2023-dotnet_api-training-materials.html"><strong>AutoCAD 2023 .NET API トレーニング マテリアル</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a>&#0160;の参照をお薦めします。</li>
</ul>
<hr />
<p><strong>はじめに</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/-o4uXu4FasI" width="480"></iframe></p>
<p><strong>DesignAutomation APIの理解</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/zjUTqe1xVWY" width="480"></iframe></p>
<p><strong>Learn Forge：モデルを修正する</strong></p>
<p style="padding-left: 40px;"><strong>サーバを作成する</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/wV0sTpaz6zI" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>基本アプリの UI</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Kr8DIYhZmHE" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>プラグインを準備する</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/kSgRwWuWXw8" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>Activity を定義する</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/zk_CVpJJTc8" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>WorkItem を実行する</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/QFZJp6VEpGU" width="480"></iframe></p>
<p><strong>付録：DesignAutomation API の注意点</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/FzHQRCvRq9k" width="480"></iframe></p>
<p><strong>付録：コストについて</strong></p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Gq6lRMyjaH0" width="480"></iframe></p>
<hr />
<p>By Toshiaki Isezaki</p>
