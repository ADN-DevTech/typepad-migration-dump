---
layout: "post"
title: "Forge Online - Design Automation：タスクの自動化"
date: "2020-07-01 00:02:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-basics.html "
typepad_basename: "forge-online-design-automation-basics"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/about-forge-online.html" rel="noopener" target="_blank">Forge Online</a></strong>、今回から数回にわたってタスクの自動化で利用される Design Automation API についてご案内していきたいと思います。まずは、登場の背景や歴史を含め、何が出来るのかその概要と、少し踏み込んでその仕組みについてご紹介します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9506d3b200b-pi" style="display: inline;"><img alt="Facebook_banner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9506d3b200b image-full img-responsive" src="/assets/image_942235.jpg" title="Facebook_banner" /></a></p>
<hr />
<p>動画で利用しているプレゼンテーション資料（PDF）は次のリンクからダウンロードいただけます。スライド下部にある URL で、スライドで説明した内容のブログ記事、また、参考 Web ページを参照することが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263ec1d7a31200c img-responsive"><a href="https://adndevblog.typepad.com/files/forge-onlineviewer-%E3%82%BD%E3%83%AA%E3%83%A5%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%81%AE%E6%B5%81%E3%82%8C.pdf" rel="noopener" target="_blank"></a><strong><a href="https://adndevblog.typepad.com/files/forge-online.design-automation%E3%82%BF%E3%82%B9%E3%82%AF%E3%81%AE%E8%87%AA%E5%8B%95%E5%8C%96.pdf" rel="noopener" target="_blank">Forge Online：.Design Automation：タスクの自動化</a></strong></span><span class="asset  asset-generic at-xid-6a0167607c2431970b0263ec221048200c img-responsive"><a href="https://adndevblog.typepad.com/files/forge-online.design-automation%E3%82%BF%E3%82%B9%E3%82%AF%E3%81%AE%E8%87%AA%E5%8B%95%E5%8C%96.pdf"> をダウンロード</a></span></p>
<hr />
<p><strong>Design Automation API 概説</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/9L3nGBcLI3w?feature=oembed" width="500"></iframe></p>
<hr />
<p><strong>Design Automation API の理解</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/I6MSC-1VKGQ?feature=oembed" width="500"></iframe></p>
<ul>
<li class="asset-video">タスクの終了時に呼び出される OnComplete コールバック、必要に応じて Forge アプリが追加ファイルを参照する際に使用する OnDemand コールバック、タスクの処理状況を通知する OnProgress コールバックの利用には、WorkItem 登録時にコールバック URL を明記する必要があります。コールバック URL への通知をローカル開発環境で使用する場合には、<a href="https://ngrok.com/docs" rel="noopener" target="_blank"><strong>NGROK</strong></a> を使用することが出来ます。</li>
</ul>
<hr />
<p>Forge Online、次回は Design Automation for AutoCAD の具体的な利用についてご案内します。</p>
<p>By Toshiaki Isezaki</p>
