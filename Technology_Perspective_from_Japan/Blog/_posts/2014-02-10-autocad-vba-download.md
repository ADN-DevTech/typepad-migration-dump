---
layout: "post"
title: "AutoCAD VBA コンポーネントのダウンロードについて"
date: "2014-02-10 17:45:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/01/autocad-vba-download.html "
typepad_basename: "autocad-vba-download"
typepad_status: "Publish"
---

<p>AutoCAD 2010 から AutoCAD 2013 までの VBA コンポーネントは、従来、<a href="http://www.autodesk.com/vba-download" target="_blank">http://www.autodesk.com/vba-download</a>&#0160;から 32 ビット版と 64 ビット版に分けてダウンロード提供されていました。このダウンロードは、1月31日をもって終了されています。理由については、同ダウンロード ページからリンクされている&#0160;<a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=770215">frequently asked questions</a>（よくある質問）の 3 つめの項目「<strong>What version of VBA does AutoCAD support?</strong>」で説明されているとおりです。&#0160;</p>
<p>非常に残念な決定ですが、もともと Microsoft 社との契約に基づくダウンロード提供だったようで、現段階では&#0160;AutoCAD 2010 から AutoCAD 2013 までの VBA 6.x コンポーネントのダウンロードを再開する予定はありません。なお、前述の FAQ にもありますように、AutoCAD 2014 で採用された VBA 7.1 コンポーネントのダウンロード提供は、今後も継続することが記されています。</p>
<p>AutoCAD 2010 から AutoCAD 2013 までの製品をお使いで、既に対応する&#0160;VBA コンポーネントをダウンロードされている場合は、そのままお使いいただいても問題はありません。今回の処置は、オートデスクの　Web サイトからのダウンロード提供にかかわるものです。</p>
<p>VBA マクロをお持ちで、将来も VBA マクロを使い続けることをお考えのお客様には、当面の運用も考慮して、次の 2 つの対応をお決めいただくことになるかと思います。</p>
<ol>
<li>
<p>VBA 7.1 が利用可能な AutoCAD 2014 にアップグレードしていただき、VBA を使い続けていただく。</p>
</li>
<li>
<p>FAQ の他の項目にあるように、VBA コードを AutoCAD .NET API に移植していただく。</p>
</li>
</ol>
<p>1. の方策をご検討いただけるお客様で、かつ、32 ビット版 Windows 上で AutoCAD VBA をお使いの場合には、過去のブログ記事、</p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/04/vba-performance-differences-per-platform.html" target="_blank">プラットフォーム差による VBA の問題と AutoCAD 2014</a></strong></p>
<p>でご案内したパフォーマンス上の改善を見込んでいだくことが出来ます。</p>
<p>2. の方策をご検討いただく場合には、次に公開している Autodesk Knowledge Network やドキュメントをご参照いただき、お手持ちの VBA マクロを AutoCAD .NET API のコマンドに移植していただくことが出来ます。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u03U.html" target="_blank">VBA プログラムの .NET API への移行方法について</a></strong></p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b4b3d9970d img-responsive"><a href="http://adndevblog.typepad.com/files/qa-8762.pdf" target="_blank">QA-8762 AutoCAD 2010と2011 VBA の VB.NET マイグレーション手順</a></span></strong></p>
<p>AutoCAD 2014 に移行していただく場合でも、VBA 7.1 上で VBA マクロ をそのまま使い続けるのではなく、AutoCAD .NET API への移植を希望される場合には、過去のブログ記事、</p>
<p style="padding-left: 30px;"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/autocad-2014-vba2dotnet_migration.html" target="_blank">AutoCAD 2014 VBA の VB.Netへのマイグレーション手順の公開</a></strong></p>
<p>をご参照ください。</p>
<p>プロフェッショナル開発者による、移植業務の請負をご検討のお客様には、<a href="http://partnerproducts.autodesk.com/catalog/portingservices.asp" target="_blank">http://partnerproducts.autodesk.com/catalog/portingservices.asp</a> をご覧いただくことで、移植サービスを実施している開発会社を検索していただくことが出来ます。</p>
<p>ご不便をお掛けいたしますが、上記の方策をご検討いただければと思います。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
