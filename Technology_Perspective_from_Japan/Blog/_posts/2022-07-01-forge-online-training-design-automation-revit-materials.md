---
layout: "post"
title: "Forge Online Training - Design Automation Revit 収録公開"
date: "2022-07-01 01:10:06"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/06/forge-online-training-design-automation-revit-materials.html "
typepad_basename: "forge-online-training-design-automation-revit-materials"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308d463ec200c-pi" style="display: inline;"><img alt="Forge Online Training Recordings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308d463ec200c image-full img-responsive" src="/assets/image_746527.jpg" title="Forge Online Training Recordings" /></a></p>
<p>去る 2022 年 6 月 22 日に、オートデスクがクラウド上に用意した Revit コアエンジンでアドイン アプリを実行して成果を生成するオンライン トレーニングを開催しました。</p>
<p>当日は、補足も交えて、Learn Forge コンテンツを利用し、Web サーバー実装に .NET Core と Forge SDK、Revit アドイン（プラグイン）の実装に Revit API を使用して Forge アプリを構築しています。</p>
<p>クライアント画面から OSS Bucket に Revit モデルをアップロードし、パラメータを入力して、クラウド上でモデルを編集してダウンロードするワークフローをご紹介しました。</p>
<p>使用したプレゼンテーション資料（PDF ファイル）は、次のリンクからダウンロードすることが出来ます。ページ中には後日参照可能な数多くのリンクが埋め込まれていますので、このページに記載した収録動画とともにご確認ください。</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b02a30d41c64c200b img-responsive"><a href="https://adndevblog.typepad.com/files/forge-training---design-automation-revit.pdf">Forge Training - Design Automation Revitをダウンロード</a></span></p>
<p><strong>前提：</strong></p>
<ul>
<li>実習では<span style="color: #111111;">Visual Studio 2022 Community（無償）</span>を利用します。<br /><span style="color: #111111;">次のブログ記事を事前にご確認の上、必要となるツールや環境のインストールをお願いします。</span><br /><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/setup-design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample.html">Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample のセットアップ</a></strong></li>
<li>Forgeのデベロッパキー（Client Id と Client Secret）の取得が必要です。また、キーの取得には、Autodesk ID が必要となります。Autodesk ID、アクセスキーをお持ちでない場合には、次のブログ記事に沿 って、それらを事前に取得しておくようお願いします。<br /><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>Forge API を利用するアプリの登録とキーの取得</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a></li>
<li>HTML、CSS、JavaScript、RESTful API の概要を把握されていると理解が深まります。もし、Web 開発が初めての場合には、<a href="https://developer.mozilla.org/ja/docs/Learn/Getting_started_with_the_web" rel="noopener" target="_blank"><strong>ウェブ入門 - ウェブ開発を学ぶ</strong>&#0160;| MDN (mozilla.org)</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html" rel="noopener" target="_blank"><strong>Forge 開発に際して...</strong>&#0160;- Technology Perspective from Japan (typepad.com)</a>&#0160;をご一読いただくことをお勧めします。</li>
<li>IIS は有効化する必要ございません。今回は、Visual Studio 2022 のデバッグ環境でサーバーを起動します。</li>
<li>Revit アドインの開発経験がない方は、下記のブログ記事で公開している<a href="https://adndevblog.typepad.com/technology_perspective/2018/12/revit-api-bim-seminar-summary.html"><strong>「Revit API &amp; BIM セミナーのサマリー」</strong></a>の「Revit アドイン基礎」のセッションを事前にご覧いただけると理解が深まります。</li>
</ul>
<hr />
<p><strong>はじめに<br /></strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/KVXEJpVV44g?feature=oembed" title="Forge Online Training   Design Automation Revit   はじめに" width="712"></iframe></p>
<p><strong>Design Automation を利用するソリューション</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/LBxq_OemdHI?feature=oembed" title="Forge Online Training   Design Automation Revit   Design Automation を利用するソリューション" width="712"></iframe></p>
<p><strong>サーバーを作成する</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/CH4X1K7M_gI?feature=oembed" title="Forge Online Training   Design Automation Revit   サーバーを作成する" width="712"></iframe></p>
<p><strong>Design Automation の仕組み &amp; 基本アプリの UI</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/jl4eGL1megk?feature=oembed" title="Forge Online Training   Design Automation Revit   Design Automation の仕組み &amp; 基本アプリの UI" width="712"></iframe></p>
<p><strong>プラグインを準備する</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/GlQMKN0CbdQ?feature=oembed" title="Forge Online Training   Design Automation Revit   プラグインを準備する" width="712"></iframe></p>
<p><strong>AppBundle をアップロードする &amp; Activity を定義する</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/EBfJaW2y45M?feature=oembed" title="Forge Online Training   Design Automation Revit   AppBundle をアップロードする &amp; Activity を定義する" width="712"></iframe></p>
<p><strong>WorkItem を実行する</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/grIW2xa9SAI?feature=oembed" title="Forge Online Training   Design Automation Revit   WorkItem を実行する" width="712"></iframe></p>
<p><strong>補足事項 &amp; 付録</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="400" src="https://www.youtube.com/embed/AajPMRYoHvk?feature=oembed" title="Forge Online Training   Design Automation Revit   補足事項 &amp; 付録" width="712"></iframe></p>
<hr />
<p>By Ryuji Ogasawara</p>
