---
layout: "post"
title: "Design Automation：GET WorkItems/:id 呼び出し回数制限 150 回／分"
date: "2024-11-20 00:44:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/11/design-automationget-workitemsid-rate-limit-150-rpm.html "
typepad_basename: "design-automationget-workitemsid-rate-limit-150-rpm"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da2145200b-pi" style="display: inline;"><img alt="Aps2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da2145200b image-full img-responsive" src="/assets/image_159624.jpg" title="Aps2" /></a></p>
<p>APS Design Automation API を利用するアプリをお持ちで、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noreferrer noopener" target="_blank">GET workItems/:id</a> エンドポイントを使用して WorkItem のステータスを高頻度でチェックしている場合は、今後の Rate Limit（呼び出し数制限）の変更についてご留意ください。</p>
<p><strong>2025 年 5 月 1 日以降、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noreferrer noopener" target="_blank">GET workitems/:id</a> エンドポイントに対して、Client ID あたり 150 RPM の呼び出し数</strong><strong>制限が適用されます。（1 分間の呼び出しは 150 回まで）</strong></p>
<p><strong>Rate Limit（呼び出し数制限）</strong></p>
<p>Rate Limit は、ネットワークによって送受信されるトラフィックの量を制御するために使用されます。アプリが制限数を超えて呼び出しをおこなうと、制限数に達した後に呼び出しが失敗し、429 レスポンスが返されます。これは、データの流れを改善し、DoS 攻撃などを軽減することでセキュリティを強化する目的で一般的に使用される手法です。</p>
<p>スケーラブルなソリューションのコードを作成時には、この Rate Limit を考慮に入れる必要があります。Rate Limit に達した場合には、&quot;Retry-After&quot; レスポンス ヘッダーで指定された時間が経過した後に、再度呼び出しをおこなう必要があります。下記の記述は、Rate Limit（呼び出し数制限）に達した際のレスポンス例です。</p>
<pre style="padding-left: 40px;"><code class=" hljs http"><span class="hljs-status">HTTP/1.1 <span class="hljs-number">429</span> Too Many Requests </span>
<span class="http"><span class="hljs-attribute">Content-Type</span>: <span class="hljs-string">application/json </span>
<span class="hljs-attribute">Retry-After</span>: <span class="hljs-string">25 </span>
<span class="hljs-attribute">Server</span>: <span class="hljs-string">Apigee Router </span>
<span class="hljs-attribute">Content-Length</span>: <span class="hljs-string">44 </span>
<span class="json">{&quot;<span class="hljs-attribute">developerMessage</span>&quot;:<span class="hljs-value"><span class="hljs-string">&quot;Quota limit exceeded.&quot;</span></span>}  </span></span></code></pre>
<p><strong>必要なアクション</strong></p>
<p>アプリが 1 分あたり 150 回以上 GET workitems/:id エンドポイントを呼び出している、または予想される場合は、前述のように 429 ステータス コードを適切に処理するようにしてください。<br />&#0160;</p>
<p>「ポーリング」は、GET workitems/:id エンドポイントを繰り返し呼び出す一般的な方法ですが、 Rate Limit（呼び出し数制限）による APS 環境のパフォーマンス低下の懸念を考慮すると、アプリ改良に際していくつかの代替策が存在します。</p>
<ol>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/callbacks/#oncomplete-callback" rel="noopener noreferrer" target="_blank">OnComplete コールバック</a>を使用する。</li>
<li>複数の作業WorkIten を処理する場合には、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener noreferrer" target="_blank">GET workitems/:id</a> エンドポイントを何度も呼び出す代わりに、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-status-POST/" rel="noopener noreferrer" target="_blank">POST workitems/status</a> を使用して WorkItem の配列のステータスを取得する</li>
<li>OnComplete コールバックがシナリオで機能しない場合には、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/websocket-api/" rel="noreferrer noopener" target="_blank">WebSocket API</a> を使用して WorkItem の状態をフェッチする。</li>
<li><a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noreferrer noopener" target="_blank">GET workitems/:id</a> エンドポイント呼び出しが避けられない場合には、毎分 150 回以下の呼び出しになるよう呼び出し回数を抑制する。</li>
</ol>
<p>Rate Limit（呼び出し数制限）の詳細については、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/" rel="noopener noreferrer" target="_blank">こちら</a>&#0160;の内容をご確認ください。</p>
<p>ご不明な点がございましたら、<a  _istranslated="1" href="https://aps.autodesk.com/contact-support" rel="noopener noreferrer" target="_blank">APS サポート</a>&#0160;お問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/design-automation-get-workitemsid-will-be-enforced-rate-limit-150-rate-minute-rpm" rel="noopener" target="_blank">Design Automation GET WorkItems/:id will be enforced with Rate Limit 150 Rate Per Minute (RPM) | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
