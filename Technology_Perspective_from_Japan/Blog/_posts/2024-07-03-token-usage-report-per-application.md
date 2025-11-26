---
layout: "post"
title: "アプリ別 Token Usage レポート"
date: "2024-07-03 00:01:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html "
typepad_basename: "token-usage-report-per-application"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">2025年6月からUsage Analytics ページか廃止されて、下記に <a href="https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html" rel="noopener" style="background-color: #ffff00;" target="_blank">アプリ別 Token Usage レポート</a>&#0160;に移行しています。</span></p>
<p>APS API の消費トークンを得るためには、従来、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html" rel="noopener" target="_blank">Usage Analytics</a></strong> をお使いいただいていました。今回、APS ポータルの Application ページが刷新されて、アプリ単位でトークン消費量を表示する方法が追加されています。</p>
<p>Application ページを表示すると、従来のタイル状の表示から、リスト状の表示に変わっていることがわかります。表示されるリストからアプリ名をクリックすると、そのアプリの Client ID と Client Secret、Callback URL や使用する API を表示させたり、一部設定を変更することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d57421200c-pi" style="display: inline;"><img alt="Application" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d57421200c img-responsive" src="/assets/image_268144.jpg" title="Application" /></a></p>
<p>アプリ設定は [App settings] タブに表示されるようになったの他に、今回追加されたのは [Token usage] タブと [API Usage] タブです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ec2a61200b-pi" style="display: inline;"><img alt="Api_usage_tab" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ec2a61200b img-responsive" src="/assets/image_228706.jpg" title="Api_usage_tab" /></a></p>
<p>[Token usage] タブでは、表示したアプリの消費トークンをグラフ表示させることが出来るようになっています。 [API Usage] タブでは、同アプリの API 別の消費状況を表示させることが出来ます。Design Automation) API の場合には使用しているコアエンジン別の詳細を、Model Derivative API の場合にはシンプル ジョブ/コンプレックス ジョブのジョブ単位でフィルタリングすることも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ec2a49200b-pi" style="display: inline;"><img alt="Api_usage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ec2a49200b image-full img-responsive" src="/assets/image_179308.jpg" title="Api_usage" /></a></p>
<p><span style="background-color: #ffffff;">トークン残高を含む全体のトークン使用状況については、<a href="https://www.autodesk.com/jp/support/account/admin/usage/usage-report" rel="noopener" style="background-color: #ffffff;" target="_blank">使用状況レポート</a>で把握することが出来ます。</span></p>
<p><span style="background-color: #ffffff;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ebeef1200b-pi" style="display: inline;"><img alt="Manage_com_reporting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ebeef1200b image-full img-responsive" src="/assets/image_247327.jpg" title="Manage_com_reporting" /></a><br /></span></p>
<p>By Toshiaki Isezaki</p>
