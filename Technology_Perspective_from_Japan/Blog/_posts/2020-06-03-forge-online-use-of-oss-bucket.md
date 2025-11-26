---
layout: "post"
title: "Forge Online - OSS Bucket の利用"
date: "2020-06-03 00:02:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/06/forge-online-use-of-oss-bucket.html "
typepad_basename: "forge-online-use-of-oss-bucket"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/about-forge-online.html" rel="noopener" target="_blank">Forge Online</a></strong>、今回は、共有ストレージに Bucket と呼ばれる領域を作成し、デザインファイルのアップロード、変換、Viewer での表示をおこなう過程をご案内します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2dfe058200d-pi" style="display: inline;"><img alt="Facebook_banner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2dfe058200d image-full img-responsive" src="/assets/image_105137.jpg" title="Facebook_banner" /></a></p>
<hr />
<p>動画で利用しているプレゼンテーション資料（PDF）は次のリンクからダウンロードいただけます。スライド下部にある URL で、スライドで説明した内容のブログ記事、また、参考 Web ページを参照することが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0264e2dfd347200d img-responsive"><a href="https://adndevblog.typepad.com/files/forge-onlineoss-bucket-%E3%81%AE%E5%88%A9%E7%94%A8.pdf"><strong>Forge Online：OSS Bucket の利用 </strong>をダウンロード</a></span></p>
<hr />
<p><strong>Postman でのアクセス トークンの取得</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/Ia_uBqvm7bM?feature=oembed" width="500"></iframe></p>
<ul>
<li class="asset-video">Forge ポータルで作成（登録）した Client Secret は 、セキュリティ維持のため、必要に応じて [REGENERATE] ボタンで再生成することが出来ます。この際、Client ID は同じ維持します。変更後には、同 Client Secret を埋め込んだ（使った） Forge アプリの Client Secret も更新する必要があります。</li>
</ul>
<hr />
<p><strong>OSS Bucket の利用と SVF 変換</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/iJSMDzcog5M?feature=oembed" width="500"></iframe></p>
<ul>
<li class="asset-video">Bucket は Client ID に関連付けられるため、Bucket を作成した Forge アプリでしかアクセスすることが出来ません（同 Client ID から生成されたアクセストークンを利用しなければなりません）。つまり、他の開発者からは隠蔽されます。</li>
<li class="asset-video">Forge ポータルで作成した当該アプリを削除してしまうと、それまでに作成した Bucket と、そのデータにはアクセスすることが出来なくなります。ご注意ください。</li>
<li class="asset-video">Forge の Bucket には、アクセス権限の設定や Bucket 閲覧をする AWS S3 のような UI ページの提供はありません。</li>
</ul>
<hr />
<p><strong>Forge SDK を使った Viewer 実装</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/uU5obSODurY?feature=oembed" width="500"></iframe></p>
<hr />
<p>Forge Online、次回は オートデスク SaaS ストレージ の利用をご案内します。</p>
<p>By Toshiaki Isezaki</p>
