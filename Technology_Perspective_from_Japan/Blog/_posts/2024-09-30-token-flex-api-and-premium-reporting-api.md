---
layout: "post"
title: "Token Flex API と Premium Reporting API の違いと注意点"
date: "2024-09-30 00:26:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/token-flex-api-and-premium-reporting-api.html "
typepad_basename: "token-flex-api-and-premium-reporting-api"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（APS）の API には、よく似た機能を持つ Token Flex API と Premium Reporting API が存在します。どちらも組織内でのオートデスク製品の使用状況分析とレポートを得るための管理者用 API ですが、対象が異なります。ここでは、両 API の概要と違い、注意点をご案内しておきたいと思います。</p>
<hr />
<p><strong>対象</strong></p>
<p style="padding-left: 40px;">オートデスクが提供する<a href="https://www.autodesk.com/jp/buying/plans" rel="noopener" target="_blank">サブスクリプション プラン</a>には、Standard、Premium、Enterprise の 3 つがあり、 Token Flex API が Enterprise プラン、Premium Reporting API が Premium プランの各契約をお持ちの管理者が対象になります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d47299200b-pi" style="display: inline;"><img alt="Usage_report_apis" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d47299200b image-full img-responsive" src="/assets/image_187835.jpg" title="Usage_report_apis" /></a></p>
<p style="padding-left: 40px;"><strong>Token Flex API</strong></p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eb9570200d-pi" style="float: right;"><img alt="Token-flex-black" class="asset  asset-image at-xid-6a0167607c2431970b02e860eb9570200d img-responsive" src="/assets/image_238419.jpg" style="width: 64px; margin: 0px 0px 5px 5px;" title="Token-flex-black" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bdfed1200c-pi" style="float: right;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bdfec6200c-pi" style="float: right;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bdfea0200c-pi" style="float: right;"></a>正式名は Token Flex Usage Data API です。Enterprise プラン、Autodesk Enterprise Business Agreement（EBA）と呼ばれる包括契約下で使用される、オートデスク製品の使用状況分析とレポートを得るための API を提供します。</p>
<p style="padding-left: 80px;">Token Flex API は 2018 年末に登場した API で、このブログでも、<a href="https://adndevblog.typepad.com/technology_perspective/2019/01/about-token-flex-api.html" rel="noopener" target="_blank">Token Flex API について</a> の記事でご案内したことがあります。契約管理者向けに、EBA 契約の製品使用状況分析とレポート用 RESTful API を用意しています。</p>
<p style="padding-left: 40px;"><strong>Premium Reporting API</strong></p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eb9559200d-pi" style="float: right;"><img alt="Premium-reporting-black" class="asset  asset-image at-xid-6a0167607c2431970b02e860eb9559200d img-responsive" src="/assets/image_406935.jpg" style="width: 64px; margin: 0px 0px 5px 5px;" title="Premium-reporting-black" /></a>Premium プランのサブスクリプション契約下で使用される、オートデスク製品の使用状況分析とレポートを得るための API を提供します。</p>
<p style="padding-left: 80px;">Token Flex API に遅れること約 3 年半、2022 年に正式に導入された API です。同契約の製品使用状況分析とレポート用 RESTful API を用意しています。</p>
<hr />
<p><strong>違い</strong></p>
<p style="padding-left: 40px;">両 API とも、従来、Autodesk Account から手動で実行していたワークフローを自動化することが出来ます。ただし、使用するエンドポイントはまったく異なります。</p>
<ul>
<li>Token Flex API：<a href="https://aps.autodesk.com/en/docs/tokenflex/v1/reference/http/" rel="noopener" target="_blank">REST API Reference | Token Flex Usage Data API</a></li>
<li>Premium Reporting API：<a href="https://aps.autodesk.com/en/docs/insights/v1/reference/http/" rel="noopener" target="_blank">REST API Reference | Premium Reporting API</a></li>
</ul>
<p style="padding-left: 40px;">同様に、レポートに指定する特定期間や条件の記述方法も異なります。</p>
<ul>
<li>Token Flex API：<a href="https://aps.autodesk.com/en/docs/tokenflex/v1/reference/fields-and-metrics/" rel="noopener" target="_blank">Fields and Metrics | Token Flex Usage Data API</a></li>
<li>Premium Reporting API：<a href="https://aps.autodesk.com/en/docs/insights/v1/reference/fields-and-metrics/" rel="noopener" target="_blank">Fields and Metrics | Premium Reporting API</a></li>
</ul>
<hr />
<p><strong>運用</strong></p>
<p style="padding-left: 40px;">Token Flex API と Premium Reporting API 共、エンドポイント呼び出しには 3-legged 認証・認可を経て取得したアクセストークンが必要です。この際、認可するアカウントは、プライマリ管理者かセカンダリ管理者である必要があります。（プライマリ管理者かセカンダリ管理者がアプリにサインインして、アプリのリソースへのアクセスを許可し、アクセストークンを得る必要があります。）</p>
<p style="padding-left: 40px;">Premium Reporting API では、<a href="https://profile.autodesk.com/security" rel="noopener" target="_blank">https://profile.autodesk.com/security</a> からパーソナル アクセス トークン（<a href="https://en.wikipedia.org/wiki/Personal_access_token" rel="noopener" target="_blank">Personal Access Token</a>、PAT ）を生成して利用することが出来ます。ただし、この場合も、パーソナル アクセス トークンの生成には、プライマリ管理者かセカンダリ管理者のアカウントで <a href="https://profile.autodesk.com/security" rel="noopener" target="_blank">https://profile.autodesk.com/security</a>&#0160;にサインインする必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d474cb200b-pi" style="display: inline;"><img alt="Pat" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d474cb200b img-responsive" src="/assets/image_561649.jpg" title="Pat" /></a></p>
<hr />
<p><strong>注意点</strong></p>
<p style="padding-left: 40px;">Token Flex API と Premium Reporting API は、Enterprise 契約、または Premium 契約 をお持ちの管理者用に用意された API です。その性格上、3rd party がアプリを作成しても、検証には管理者アカウントが必須になってしまいます。このため、セキュリティ保護の観点から、3rd party、主に ISV による独自アプリ ビジネスでの利用はお勧めしていません。</p>
<p style="padding-left: 40px;">Token Flex API と Premium Reporting API 自体には、<a href="https://www.microsoft.com/ja-jp/power-platform/products/power-bi" rel="noopener" target="_blank">Microsoft Power BI</a>、あるいはオープンソースの&#0160;<a href="https://www.chartjs.org/" rel="noopener" target="_blank">Chart.js</a> のようなグラフ化機能の提供はありません。</p>
<hr />
<p>By Toshiaki Isezaki</p>
