---
layout: "post"
title: "Rate Limit（レート制限、呼び出し回数制限）"
date: "2025-01-20 00:07:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/01/rate-limit.html "
typepad_basename: "rate-limit"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dd3b5c200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dd3b5c200b image-full img-responsive" src="/assets/image_974064.jpg" title="Aps" /></a></p>
<p><strong>Rate Limit（レート制限）とは</strong></p>
<p style="padding-left: 40px;">Autodesk Platform Services（APS）のエンドポイントには、Rate Limit（レート制限）と呼ばれる呼び出し回数制限が設定されています。</p>
<p style="padding-left: 40px;">アプリが 1 分あたりに実行できるエンドポイントの呼び出しの最大数がレート制限の実態です。呼び出し数をサービスの容量内に制限することで、システム全体の安定性の維持と <a href="https://ja.wikipedia.org/wiki/DoS%E6%94%BB%E6%92%83" rel="noopener" target="_blank">DoS 攻撃</a>、<a href="https://ja.wikipedia.org/wiki/DoS%E6%94%BB%E6%92%83#DDoS%E6%94%BB%E6%92%83" rel="noopener" target="_blank">DDos 攻撃</a>などの攻撃からシステムを保護、影響を低減させることが目的です。</p>
<p style="padding-left: 40px;">APS がオートデスクのクラウド サービスの基盤であると同時に、APS を利用する多くの開発者との共有環境である点をご理解ください。言い換えれば、APS はパブリック クラウドのインフラ（AWS）上に構築されたマルチテナント システムであり、APS を利用する世界中のアプリとの共有リソースです。一部アプリの突出した数の呼び出しが、システムを不安定化させてしまい、他のアプリのパフォーマンスに悪影響を与えてしまう<span style="text-decoration: underline;">可能性</span>があります。</p>
<p style="padding-left: 40px;">Microsoft 社の「<a href="https://learn.microsoft.com/ja-jp/azure/architecture/antipatterns/noisy-neighbor/noisy-neighbor">うるさい隣人のアンチパターン - Azure Architecture Center | Microsoft Learn</a>」記事にあるような、過度なエンドポイント呼び出しによるクラウド リソースの占有も考慮すべき点にもなっています。例外的ですが、<a href="https://adndevblog.typepad.com/technology_perspective/2021/06/post-command-createfolder-deprecated.html">非推奨の POST CreateFolder Command endpoint</a> の記事にあるような、複合的な処理を必要とするエンドポイントを廃止する、といった対応も過去に存在しました。</p>
<p style="padding-left: 40px;">この&#0160;<a href="https://ja.wikipedia.org/wiki/DoS%E6%94%BB%E6%92%83" rel="noopener" target="_blank">レート制限</a>&#0160;自体は、マルチテナント システムの世界では最も一般的な制御手法と捉えることも出来ます。</p>
<p style="padding-left: 40px;">なお、Rate Limit（レート制限）には、すべてのエンドポイントに共通する指標（<a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/rate-limiting/forge-rate-limits/" rel="noopener" target="_blank">APS Rate Limits and Quotas</a>）が適用されるだけでなく、 API タイプ、モジュール、エンドポイント毎に異なるレート制限数が設定されている場合がありますのでご注意ください。代表的な API のレート制限は、次のとおりです。</p>
<ul>
<li>Authentication API：<a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/rate-limiting/oauth-rate-limits/" rel="noopener" target="_blank">Authentication Rate Limits</a></li>
<li>Data Management API：<a href="https://aps.autodesk.com/en/docs/data/v2/developers_guide/rate-limiting/dm-rate-limits/" rel="noopener" target="_blank">Data Management Rate Limits</a></li>
<li>Model Derivative API：<a href="https://aps.autodesk.com/en/docs/model-derivative/v2/developers_guide/rate-limiting/md-rate-limits/" rel="noopener" target="_blank">Model Derivative Rate Limits</a></li>
<li>Design Automation API：<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/da-rate-limits/" rel="noopener" target="_blank">Design Automation Rate Limits</a></li>
<li>Reality Capture API：<a href="https://aps.autodesk.com/en/docs/reality-capture/v1/developers_guide/rate-limits/recap_quotas/" rel="noopener" target="_blank">Reality Capture Quotas</a></li>
<li>Autodesk Construction API
<ul>
<li>Admin API：<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/admin-rate-limits/" rel="noopener" target="_blank">Admin Rate Limits</a></li>
<li>Cost Management API：<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/cost-management-rate-limits%20/" rel="noopener" target="_blank">Cost Management Rate Limits</a></li>
<li>Files API：<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/files-rate-limits/" rel="noopener" target="_blank">Files Rate Limits</a></li>
<li>Issues API：<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/issues-rate-limits/" rel="noopener" target="_blank">Issues Rate Limits</a></li>
<li>Model Coordination API：<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/model-coordination-rate-limits/" rel="noopener" target="_blank">Model Coordination Rate Limits</a></li>
<li>...</li>
</ul>
</li>
<li>...</li>
<li>AEC Data Model API ：<a href="https://aps.autodesk.com/en/docs/aecdatamodel/v1/developers_guide/ratelimit/" rel="noopener" target="_blank">AEC Data Model Rate Limits</a></li>
<li>Manufacturing Data Model API：<a href="https://aps.autodesk.com/en/docs/mfgdataapi/v2/developers_guide/rate-limits/" rel="noopener" target="_blank">Manufacturing Data Model API </a><a href="https://aps.autodesk.com/en/docs/mfgdataapi/v2/developers_guide/rate-limits/">Rate Limits</a><a href="https://aps.autodesk.com/en/docs/mfgdataapi/v2/developers_guide/rate-limits/"></a></li>
<li>...</li>
</ul>
<p><strong>Rate Limit（レート制限）に達した場合の対応</strong></p>
<p style="padding-left: 40px;">エンドポイント呼び出しの回数が Rate Limit（レート制限）に達した場合、429 ステータス &quot;Too many requests&quot; レスポンスが返されます。この場合、再呼び出しは、レスポンス ヘッダーにある retry-after 値で指定されている <span style="text-decoration: underline;"><span style="background-color: #ffff00;">xx</span> 秒後</span>に再試行する必要があります。（<span style="background-color: #ffff00;">xx</span> は数値）</p>
<section id="response-header-429">
<h4 style="padding-left: 40px;">429 レスポンス ヘッダー例</h4>
<blockquote>
<p><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">HTTP/1.1 <span style="color: #111111;">429</span></span><br /><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">Too Many Requests</span><br /><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">Content-Type: application/json</span><br /><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #111111;">Retry-After: <span style="background-color: #ffff00;">xx</span></span><br /><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">Server: Apigee Router</span><br /><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">Content-Length: 44</span></p>
</blockquote>
</section>
<section id="response-body-429" style="padding-left: 40px;">
<h4>429 レスポンス ボディ例</h4>
</section>
<blockquote>
<p><span style="font-size: 10pt; font-family: tahoma, arial, helvetica, sans-serif; color: #bf005f;">{&quot;developerMessage&quot;:&quot;Quota limit exceeded.&quot;}</span></p>
</blockquote>
<ul>
<li>429 レスポンスを無視してエンドポイント呼び出しを繰り返してしまうと、結果として DoS 攻撃のような状態にしてしまい、意図せずにシステムを不安定化、<a href="https://health.autodesk.com/" rel="noopener" target="_blank">インシデント</a>の原因になってしまう可能性があります。</li>
<li>429 レスポンスは、他のサービスへの依存や予期しないトラフィックの急増など、さ まざまな要因で発生する可能性があります。このため、アプリからのエンドポイント呼び出し回数がレート制限に達していない場合でも発生する可能性があります。アプリ実装時には、 後述する 429 レスポンスへの対応（retry-after 値以後の再呼び出し）をお願いします。</li>
</ul>
<p><strong>Rate Limit（レート制限）変更リクエスト</strong></p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/rate-limiting/forge-rate-limits/" rel="noopener" target="_blank">APS Rate Limits and Quotas</a> にも記載のあるとおり、エンドポイントに設定されたレート制限の値を増加変更するリクエストが可能です。</p>
<p style="padding-left: 40px;">ただし、前述のとおり、APS はマルチテナント システムを採用している関係で、オートデスク製品を含めた他のアプリの運用と実行環境に悪影響を及ぼさないよう、慎重に検討する必要があります。このため、リクエスト内容の受付け可否は、本社判断で決定される点にご注意ください。した上で、</p>
<p style="padding-left: 40px;">リクエストに対する判断材料とされる点には、リクエストされたアプリ（Client ID）毎の通信ログの調査があります。具体的には、リクエストされたエンドポイントが既定のレート制限に対して、どの程度の実績があるのかが調査されます。</p>
<p style="padding-left: 40px;">解析の上、既定のレート制限より呼び出し数が大幅に下回っていたり、429 レスポンスに適切に対応していない状況が明らかになると、リクエストされたレート制限値が許可されない場合があります。</p>
<p>By Toshiaki Isezaki</p>
