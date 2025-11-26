---
layout: "post"
title: "GET workitem/:id endpoint の Rate Limit 変更について"
date: "2022-03-07 00:00:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/03/rate-limit-change-for-da-endpoint-get-workitemid.html "
typepad_basename: "rate-limit-change-for-da-endpoint-get-workitemid"
typepad_status: "Publish"
---

<p lang="EN-US" xml:lang="EN-US"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1478339200b-pi" style="display: inline;"><img alt="Brand-Portrait-SF-Gallery-2381_with_overlay" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1478339200b image-full img-responsive" src="/assets/image_512310.jpg" title="Brand-Portrait-SF-Gallery-2381_with_overlay" /></a></p>
<p lang="EN-US" xml:lang="EN-US">多くの Web サービスと同様、Design Automation の各 endpoint には、サービスを保護して可用性を高めるために <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/da-rate-limits/" rel="noopener" target="_blank">Rate Limit（呼び出し数制限）</a>が設定されています。</p>
<p lang="EN-US" xml:lang="EN-US">すべての Forge 利用者に最適なサービスを提供するため、現在の の呼び出し数制限を、現在の 10,000 回（リクエスト/分）から、次の日程で 2 段階調整していく予定です。&#0160;<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3be9b00200c-pi" style="display: inline;"><img alt="名称未設定" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3be9b00200c image-full img-responsive" src="/assets/image_544256.jpg" title="名称未設定" /></a></p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="1">
<p lang="EN-US" xml:lang="EN-US"><strong>2022 年 5 月 23 日 ～: 1,000 回（リクエスト/分） </strong></p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-listid="1">
<p lang="EN-US" xml:lang="EN-US"><strong>2022 年 8 月 22 日～: 150 回（リクエスト/分） </strong></p>
</li>
</ul>
<p lang="EN-US" xml:lang="EN-US">現在、同 endpoint の呼び出し数が 150 回（リクエスト/分）を超えるアプリをお持ちの方に、通知メールを配信しています。通知を受け取られた場合には、アプリが 429 エラーコードを適切に処理していることをご確認ださい。詳細については、<a href="https://adndevblog.typepad.com/technology_perspective/2021/07/improving-app-resilience.html" rel="noopener" target="_blank">アプリ耐障害性向上の考察</a> のブログ記事を参照いただけます。</p>
<h2 lang="EN-US" xml:lang="EN-US">アプリの調整方法は ?&#0160;</h2>
<p lang="EN-US" xml:lang="EN-US">Activity の定義後、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/">POST workitems</a> endpoint で自動化タスクを実行し、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> endpoint を使って進捗を確認することが出来ます。この方法は引き続き機能しますが、推奨はされません。Workitem 完了までにかかる時間によっては、アプリが数千回も <a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> endpoint を呼び出してしまう可能性があるためです。現在提案可能な解決策は、次の 3 つの方法です。&#0160;</p>
<h3 lang="EN-US" xml:lang="EN-US">1. 429 エラーの対応処理</h3>
<p lang="EN-US" xml:lang="EN-US">現在のアプリは、一定時間毎に <a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> endpoint を呼び出しているので、429 のエラーチェックを追加して、レスポンス ヘッダーから再試行までの時間（秒）を示す retry-after 値をチェックしてください。この制限は、すべての Workitem に適用されることに注意してください。</p>
<h3 lang="EN-US" xml:lang="EN-US">2. onComplete コールバックの実装 - サーバー アプリに推奨&#0160;</h3>
<p lang="EN-US" xml:lang="EN-US">最高のパフォーマンスを得るには、onComplete コールバック URL を持つ Workitem を利用するのが最適な方法です。Workitem の実行が完了すると、Design Automation API は、Workitem Id やレポートなど、必要なすべての情報を onComplete コールバックに通知します。 onComplete コールバックの詳細は、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/callbacks/#oncomplete-callback" rel="noopener" target="_blank">こちら</a>をご確認ください。ファイアウォールに特定の権限が必要な場合は、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/restrictions/" rel="noopener" target="_blank">Design Automation IP のリスト</a>を参照してください。&#0160;</p>
<h3 lang="EN-US" xml:lang="EN-US">3. WebSocket の実装 - モバイルや OS ネイティブのクライアント アプリに推奨</h3>
<p lang="EN-US" xml:lang="EN-US">Forge Design Automation をクライアント デバイス（デスクトップやラップトップ コンピュータ、スマートフォン、タブレットなど）から直接使用する場合、クライアント デバイスで <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/callbacks/#oncomplete-callback" rel="noreferrer noopener" target="_blank">onComplete</a>&#0160; または <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/callbacks/#onprogress-callback" rel="noreferrer noopener" target="_blank">onProgress</a> コールバックを設定することは出来ません。WebSocket を使用すると、Design Automation API サーバーとクライアントアプリの間に直接接続を持つことが出来ます。 WebSocket API&#0160; の詳細については、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/websocket-api/" rel="noopener" target="_blank">こちら</a>を参照してください。</p>
<p lang="EN-US" xml:lang="EN-US">By Toshiaki Isezaki</p>
