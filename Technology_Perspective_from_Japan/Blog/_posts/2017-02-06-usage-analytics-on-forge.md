---
layout: "post"
title: "Forge 使用量の分析"
date: "2017-02-06 21:54:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/usage-analytics-on-forge.html "
typepad_basename: "usage-analytics-on-forge"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">&lt;2020年9月10日更新&gt;</span>Forge デベロッパ ポータルには、Forge Platform API 毎の Overview（概要）や Step by Step Tutorial（チュートリアル）、API Reference（リファレンス）の他に、Resources メニュー内にデベロッパ キー（Client ID とClient Secret）を取得するための My Apps などを表示するためのアカウント メニューが用意されています。このメニューには、クラウドクレジットによる課金対象となる API について、その使用状況を解析してグラフ化する Usage Analytics というメニューが用意されています。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4101650200d-pi" style="display: inline;"><img alt="Usage_analytics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4101650200d image-full img-responsive" src="/assets/image_604158.jpg" title="Usage_analytics" /></a></p>
<p>Usage Analytics を参照することで、将来、どれくらいのクラウド クレジットを消費することになるか、API 毎に把握することが出来ます。</p>
<p>Model Derivative API &#0160;の消費グラフは、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong>&#0160;でご紹介した <strong>シンプルジョブ</strong> と <strong>コンプレックスジョブ</strong> 別に表示出来るように工夫されています。それぞれの表示を切り替えるには、Simple jobs または、Complex jobs をクリックする必要があります。また、表示は、現在サインインしているアカウントで取得したアプリ（デベロッパキー）毎に区別されるので、消費量をアプリ別に判断することも出来ます。なお、デベロッパポータルから削除してしまったアプリが存在しても、Service 3 のようにアプリのダミー名で過去の使用量を判断することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde914b64200c-pi" style="display: inline;"><img alt="Graph" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde914b64200c image-full img-responsive" src="/assets/image_471211.jpg" title="Graph" /></a></p>
<p>なお、API 利用がこのページの内容が実際に反映されるのは、1 日経過した翌日となりますので注意してください。Forge トライアル期間中に使用したクラウドクレジットも表示されますので、開発したアプリの正式運用時のクラウド クレジット消費量を、ある程度把握、推測することが出来るはずです。</p>
<p>By&#0160;Toshiaki Isezaki</p>
