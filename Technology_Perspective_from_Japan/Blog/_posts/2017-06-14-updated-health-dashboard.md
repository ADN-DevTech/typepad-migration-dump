---
layout: "post"
title: "ヘルス ダッシュボードの更新"
date: "2017-06-14 00:07:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/updated-health-dashboard.html "
typepad_basename: "updated-health-dashboard"
typepad_status: "Publish"
---

<p>オートデスクのクラウド サービスの稼働状況を示すヘルス ダッシュボード (<a href="https://health.autodesk.com/" rel="noopener" target="_blank"><strong>Health Dashboard</strong> サイト)&#0160;</a>が 2015 年の夏から <strong><a href="https://health.autodesk.com/" rel="noopener" target="_blank">https://health.autodesk.com/</a></strong> から参照出来るようになっています。このサイトでは、いままでエンドユーザ向けのクラウド サービスのみを表示していましたが、今回、Forge も参照出来るようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec295053200c-pi" style="display: inline;"><img alt="Health_dashboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec295053200c image-full img-responsive" src="/assets/image_882916.jpg" title="Health_dashboard" /></a></p>
<p>あくまで、Forge 全体の稼働状況を示すもので、Data Management API や Model Derivative API など、Forge Platform API に含まれる個々の API の稼働状況を示すものではありませんが（除く、Design Automation API ）、他のクラウド サービスと同一に表示されるので便利です。</p>
<p>また、ページ上部の <strong>History</strong> をクリックすると、過去の稼働状況も把握いただけるようになっています。日付左右のスライダ（<strong>&lt;</strong> と <strong>&gt;</strong>）で表示対象を前後することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e957b4e9200b-pi" style="display: inline;"><img alt="History" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e957b4e9200b image-full img-responsive" src="/assets/image_997638.jpg" title="History" /></a></p>
<p>このサイトに Forge が記載された一番のメリットは、Forge の稼働状況に変化があった場合に、通知メールを受け取ることができるようになる点です。通知メールを受け取るには、お手持ちの Autodesk ID でページ右上にある [Sign In] リンクからサインインする必要があります。サインインすると [Health Subscription] ボタンが表示されるので、通知を受けたいサービスを見つけて [Yes] を選択してください。通知メールは、Autodesk ID に関連付けられたメール アドレスに送信されることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec29509a200c-pi" style="display: inline;"><img alt="Subscribe_health_dashboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec29509a200c image-full img-responsive" src="/assets/image_216669.jpg" title="Subscribe_health_dashboard" /></a></p>
<p>ここでクラウド サービスや Forge を Subscribe して通知メールをオンに設定しておくと、週末などに稀に実施されるシステム メインテナンスも通知されるので、スケジュール把握が可能です。</p>
<p>なお、個々の Forge Platform API の API の稼働状況は <strong>Forge ポータル (<a href="https://forge.autodesk.com/" rel="noopener" target="_blank">https://forge.autodesk.com/</a>)&#0160;</strong>で参照することが出来ます。ただし、残念ながら、こちらのページには通知メール設定の機能はありません。</p>
<p>いずれのサイトもブックマークしておくと便利かと思います。</p>
<p>By Toshiaki Isezaki</p>
