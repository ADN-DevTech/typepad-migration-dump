---
layout: "post"
title: "Forge Online - Design Automation：AutoCAD タスクの自動化"
date: "2020-07-08 00:38:59"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-autocad.html "
typepad_basename: "forge-online-design-automation-api-for-autocad"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/about-forge-online.html" rel="noopener" target="_blank">Forge Online</a></strong>、今回は、AutoCAD タスクの自動化と題して、Design Automation API for AutoCAD（DA4A）についてご案内したいと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2e78b2a200d-pi" style="display: inline;"><img alt="Facebook_banner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2e78b2a200d image-full img-responsive" src="/assets/image_218867.jpg" title="Facebook_banner" /></a></p>
<hr />
<p>動画で利用しているプレゼンテーション資料（PDF）は次のリンクからダウンロードいただけます。スライド下部にある URL で、スライドで説明した内容のブログ記事、また、参考 Web ページを参照することが出来ます。</p>
<p style="padding-left: 40px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263ec25b5ca200c img-responsive"><a href="https://adndevblog.typepad.com/files/Forge%20Online%EF%BC%9A.AutoCAD%20%E3%82%BF%E3%82%B9%E3%82%AF%E3%81%AE%E8%87%AA%E5%8B%95%E5%8C%96.pdf" rel="noopener" target="_blank"> </a><a href="https://adndevblog.typepad.com/files/forge-online.autocad-%E3%82%BF%E3%82%B9%E3%82%AF%E3%81%AE%E8%87%AA%E5%8B%95%E5%8C%96.pdf" rel="noopener" target="_blank"><strong>Forge Online：.AutoCAD タスクの自動化 </strong>をダウンロード</a></span></p>
<hr />
<p><strong>Design Automation API for AutoCAD の理解</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/j814_DDEkGU?feature=oembed" width="500"></iframe></p>
<ul>
<li class="asset-video">
<p><span style="background-color: #ffff00;">Design Autonation API&#0160; for AutoCAD（AcCoreConsole.exe）で実行させる AppBundle では、アドイン実装内で現在の図面を切り替える実装は許可、サポートされていませんのでご注意ください。この処理には、OPEN コマンドや NEW コマンドの実行、あるいは、アプリケーション実行コンテキストが必要な既存図面オープン、新規図面作成の API 実装が含まれます。また、この制限には、Database.ReadDwgFile メソッド/AcDbDatabase::readDwgFile() の使用も同様にサポートされていません。Design Automation API for AutoCAD（AcCoreConsole.exe）では、AcCoreConsole.exe /i オプションのみがサポートされています。</span></p>
</li>
</ul>
<hr />
<p><strong>Activity のみの Design Automation</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/hnWoloi4t1s?feature=oembed" width="500"></iframe></p>
<hr />
<p style="text-align: left;"><strong>AppBundle での Design Automation</strong></p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/s-fZtNeU6WM?feature=oembed" width="500"></iframe></p>
<hr />
<p>Forge Online、次回は Design Automation for Revit の具体的な利用についてご案内します。</p>
<p>By Toshiaki Isezaki</p>
