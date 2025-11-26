---
layout: "post"
title: "Autodesk Platform Services の始め方"
date: "2023-03-20 00:06:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/how-to-get-started-aps.html "
typepad_basename: "how-to-get-started-aps"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（以下、APS）は、オートデスクが提供する Web API（Web サービス&#0160; API）セットです。API エコノミーに参画すべく、オートデスクが注力するデザイン データの活用を主眼とした複数の API を提供しています。</p>
<p>API セットは、Authentication API、Data Management API、Webhooks API、Viewer などの無償 API に加え、課金対象（有償）の Model Derivayive API、Design Automation API、Reality Capture API で構成されています。有償 API の場合、API の使用量に応じて課金する従量課金制を採用しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519b2d6d200c-pi" style="display: inline;"><img alt="Api_economy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7519b2d6d200c image-full img-responsive" src="/assets/image_668542.jpg" title="Api_economy" /></a></p>
<p>APS を評価するにあたって、事前に契約や申請、購入、あるいは、クレジットカードの登録等をする必要はありません。初めてAPS を利用する場合には、90 日間の無償トライアルを活用することが出来ます。</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>APS API を利用するアプリの登録とキーの取得</strong></a> の手順で開発に必要なデベロッパーキーを取得（アプリの登録）をおこなうと、すべての API を使用出来るトライアル期間が自動的に始まります。トライアル期間デベロッパーキーを取得したアカウント（無償で登録出来る Autodesk ID）で <strong><a href="https://aps.autodesk.com/" rel="noopener" target="_blank">APS ポータル（https://aps.autodesk.com/）</a></strong>にサインインすると、ページ右上のアカウント メニューに <strong>My APS Trial</strong> と表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852ddfb1200d-pi" style="display: inline;"><img alt="My_trial_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852ddfb1200d image-full img-responsive" src="/assets/image_22125.jpg" title="My_trial_menu" /></a></p>
<p><strong>My APS Trial</strong>&#0160;をクリックすると、トライアルの有効期限が<span style="background-color: #ffffff;">&#0160;<span style="color: #ffffff; background-color: #4040ff;"> Free Trial&#0160; </span></span>に右側に表示されます。この有効期限までの間、課金対象の API を含めて、すべての API を制限なく<span style="text-decoration: underline;">無償で</span>使用・評価することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75176aff0200b-pi" style="display: inline;"><img alt="Trial_usage_analytics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75176aff0200b image-full img-responsive" src="/assets/image_961342.jpg" title="Trial_usage_analytics" /></a></p>
<p>API アクセスとアカウント状態の関係については、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/managing-your-autodesk-platform-services-account.html" rel="noopener" target="_blank">Autodesk Platform Services アカウント管理</a></strong>&#0160;の記事を参考にしてみてください。</p>
<p>一点、トライアル期間で注意すべき点が存在します。2022 年 11 月 7 日に改定された<strong><a href="https://www.autodesk.com/company/legal-notices-trademarks/terms-of-service-autodesk360-web-services/forge-platform-web-services-api-terms-of-service" rel="noopener" target="_blank">利用規約（Terms Of Service）</a></strong>では、それまで特に明記のなかった、トライアル期間中（90 日間）の商用利用が禁止されています。アカウントを新しくすることを繰り返して、90 日間の無償トライアルでビジネスすることは出来ません。（<strong>3.3 Trial Versions Not for Production Use&#0160;</strong>項）</p>
<p>もし、トライアル有効期間内に開発したアプリでビジネスを開始したい場合には、上記ページの&#0160;<span style="background-color: #111111; color: #ffffff;"> Start commercial use&#0160;</span>&#0160; をクリックしてトライアルを終了してください。トライアル終了後に課金対象 API を使用すると、<strong><a href="https://aps.autodesk.com/pricing" rel="noopener" target="_blank">価格（Flex トークン）</a></strong> ページに記載のある Flex トークン数がアカウントから差し引かれます。</p>
<p>アカウントの Flex トークン残数がマイナスになると、アカウントで登録したメール アドレスに、Flex トークンの購入を促すメールが自動配信されます。14 日間の猶予期間内に、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html" rel="noopener" target="_blank"><strong>APS へ Flex トークンを導入</strong></a> でご案内している方法で Autodesk Flex の購入をご検討ください。なお、アカウントの Flex トークン残量は、<a href="https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html" rel="noopener" target="_blank"><strong>アプリ別 Token Usage レポート</strong></a> の記事でご案内しています。</p>
<ul>
<li>14 日間の猶予期間内に Flex トークンの購入実績が確認出来ない場合には、API アクセスが遮断されてしまいますのでご注意ください。</li>
<li>Flex トークンをご購入いただいた場合、マイナス分の Flex トークン数がご購入のトークン数から自動的に差し引かれます。</li>
<li>トライアル期間中は、Usage Analytics ページを参照することは出来ません。</li>
</ul>
<p>By Toshiaki Isezaki</p>
