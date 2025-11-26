---
layout: "post"
title: "Usage Analytics ページ：Flex トークン残高と消費量の確認"
date: "2022-11-21 00:08:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html "
typepad_basename: "updated-usage-analytics-page-for-flex"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">2025年6月からUsage Analytics ページか廃止されて、<a href="https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html" rel="noopener" style="background-color: #ffff00;" target="_blank">アプリ別 Token Usage レポート</a>&#0160;に移行しています。トークン残高を含む全体のトークン使用状況については、<a href="https://www.autodesk.com/jp/support/account/admin/usage/usage-report" rel="noopener" style="background-color: #ffff00;" target="_blank">使用状況レポート</a>&#0160; をお使いください。</span></p>
<p><span style="text-decoration: line-through;">Autodesk Platform Services（旧 Forge）デベロッパ ポータルには、API 毎の使用状況を解析してグラフ化する Usage Analytics というメニューが用意されています。&#0160;</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7517212be200b-pi" style="display: inline;"><img alt="Menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7517212be200b image-full img-responsive" src="/assets/image_148102.jpg" title="Menu" /></a></span></p>
<p><span style="text-decoration: line-through;"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html" rel="noopener" target="_blank">11 月 7 日に Autodesk Flex による課金制度を導入</a></strong>したことで、Usage Analytics にも一部変更が加えられています。すなわち、API 消費の単位が、すべて従来のクラウドクレジット（Cloud Credits）から Flex トークンを表す Token になっています。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75194845d200c-pi" style="display: inline;"><img alt="Token_representation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75194845d200c image-full img-responsive" src="/assets/image_810452.jpg" title="Token_representation" /></a></span></p>
<ul>
<li><span style="text-decoration: line-through;">Summary ページの「Tokens Consumption Summary」の「Available」には、クラウドクレジットと Flex トークンの合計残高の値が示されます。</span></li>
<li><span style="text-decoration: line-through;">Summary ページの「Tokens Consumed」には、アプリ（Client Id）毎の消費トークンの遷移が棒グラフで表示されます。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751714c7f200b-pi" style="display: inline;"><img alt="Token_consumed" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751714c7f200b image-full img-responsive" src="/assets/image_6258.jpg" title="Token_consumed" /></a></span></li>
<li><span style="text-decoration: line-through;">残高にクラウドクレジットしかなくても（まだ Flex トークンを購入していなくても）、すべて Tokens と表現されます。クラウドクレジットと&#0160; Flex トークンの残高を個別に把握することは出来ません。</span></li>
<li><span style="text-decoration: line-through;">アカウントにクラウドクレジットと Flex トークンの両方を保持していると、クラウドクレジットが優先的に消費されて、クラウドクレジットがゼロになると、Flex トークンを消費し始めます。</span></li>
</ul>
<p><span style="text-decoration: line-through;">Usage Analytics ページ上部のドロップダウンから API と期間を選択することで、API 毎に、どれくらいの Flex トークンを消費したかを把握することが出来ます。また、この情報から、将来の消費量を推測出来るようになります。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751703438200b-pi" style="display: inline;"><img alt="Select_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751703438200b image-full img-responsive" src="/assets/image_301954.jpg" title="Select_api" /></a></span></p>
<p><span style="text-decoration: line-through;">Model Derivative API &#0160;の消費グラフは、変換するデザインファイルの種類に応じて、<strong>コンプレックスジョブ</strong>（RVT、NWD/NWC、IFC ファイル）と <strong>シンプルジョブ</strong>（RVT、NWD/NWC、IFC ファイル<span style="text-decoration: underline;">以外</span>）別に消費されたトークン数が表示されます。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751948473200c-pi" style="display: inline;"><img alt="Md1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751948473200c image-full img-responsive" src="/assets/image_907440.jpg" title="Md1" /></a></span></p>
<p><span style="text-decoration: line-through;">棒グラフの下には、アプリ（Client Id）毎の線グラフが表示されます。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75194847b200c-pi" style="display: inline;"><img alt="Md2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75194847b200c image-full img-responsive" src="/assets/image_462379.jpg" title="Md2" /></a></span></p>
<p><span style="text-decoration: line-through;">Design Automation API では、使用したコアエンジン毎に消費トークンが表示されます。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b685277911200d-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b685277911200d image-full img-responsive" src="/assets/image_369004.jpg" title="Da" /></a></span></p>
<p><span style="text-decoration: line-through;">なお、API 利用がこのページの内容が実際に反映されるのは、最大 1 日経過した翌日となりますので注意してください。</span></p>
<p>By&#0160;Toshiaki Isezaki</p>
