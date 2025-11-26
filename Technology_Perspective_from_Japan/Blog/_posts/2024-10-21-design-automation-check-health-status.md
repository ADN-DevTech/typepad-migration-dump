---
layout: "post"
title: "Design Automation：エンジン稼働状況の確認"
date: "2024-10-21 00:03:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/10/design-automation-check-health-status.html "
typepad_basename: "design-automation-check-health-status"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c05f6c200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c063a7200c-pi" style="display: inline;"><img alt="Da_health_check" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c063a7200c image-full img-responsive" src="/assets/image_980572.jpg" title="Da_health_check" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c05f6c200c-pi" style="display: inline;"><br /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/05/integrate-aps-info-onto-hds.html" rel="noopener" target="_blank">ヘルス ダッシュボードへの APS 情報の統合</a> のとおり、Autodesk Platform Services API の稼働状況は、製品サービスと同様に Autodesk Health Dashboard（ヘルス ダッシュボード、<a href="https://health.autodesk.com/" rel="noopener" target="_blank">https://health.autodesk.com/</a>）に表示されます。ただし、障害発生時には、具体的な原因の調査と内容を示す概要文の作成などで人的作業が発生してしまい、ヘルス ダッシュボードへの状態反映に遅延が出てしまう場合があります。</p>
<p>コンピューティング サービスの 1 つである Design Automation API を利用するアプリの場合、ヘルス ダッシュボード ページでの目視ではなく、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/health-engine-GET/" rel="noopener" target="_blank">GET health/:engine</a> エンドポイントを呼び出すことで、プログラム的にエンジン別の稼働状態（ヘルス ステータス）を取得することが出来ます。</p>
<p>2024年10月現在、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/health-engine-GET/" rel="noopener" target="_blank">GET health/:engine</a> エンドポイントのドキュメント更新が一部追い付いていないようですが、次のエンジン別のステータスに加えて、すべてのエンジン ステータスも一度に取得することも出来るようになっています。</p>
<ul>
<li><span style="font-size: 11pt;"><strong>AutoCAD：</strong><br /><a href="https://developer.api.autodesk.com/da/us-east/v3/health/autocad" rel="noopener" target="_blank">https://developer.api.autodesk.com/da/us-east/v3/health/<strong>autocad</strong></a></span></li>
<li><span style="font-size: 11pt;"><strong>Revit：</strong><br /><a href="https://developer.api.autodesk.com/da/us-east/v3/health/revit" rel="noopener" target="_blank">https://developer.api.autodesk.com/da/us-east/v3/health/<strong>revit</strong></a></span></li>
<li><span style="font-size: 11pt;"><strong>Inventor：</strong><br /><a href="https://developer.api.autodesk.com/da/us-east/v3/health/inventor" rel="noopener" target="_blank">https://developer.api.autodesk.com/da/us-east/v3/health/<strong>inventor</strong></a></span></li>
<li><span style="font-size: 11pt;"><strong>3ds Max：</strong><br /><a href="https://developer.api.autodesk.com/da/us-east/v3/health/3dsmax" rel="noopener" target="_blank">https://developer.api.autodesk.com/da/us-east/v3/health/<strong>3dsmax</strong></a></span></li>
<li><span style="font-size: 11pt;"><strong>すべて：</strong><br /><a href="https://developer.api.autodesk.com/da/us-east/v3/health/api" rel="noopener" target="_blank">https://developer.api.autodesk.com/da/us-east/v3/health/<strong>api</strong></a></span></li>
</ul>
<p>いずれの場合も、リクエストの Authorization ヘッダーによるアクセス トークンの指定は不要です。このため、上記リンクをクリックするだけで、ヘルス ステータスを含む JSON レスポンスを得ることが出来るはずです。例えば、を呼び出した場合のレスポンスは次のようになります。</p>
<pre>{&quot;engines&quot;:{&quot;AutoCAD&quot;:{&quot;Status&quot;:&quot;Fully Operational&quot;},&quot;Revit&quot;:{&quot;Status&quot;:&quot;Fully Operational&quot;},&quot;Inventor&quot;:{&quot;Status&quot;:&quot;Fully Operational&quot;},&quot;3dsMax&quot;:{&quot;Status&quot;:&quot;Fully Operational&quot;},&quot;Test&quot;:{&quot;Status&quot;:&quot;Fully Operational&quot;}}}
</pre>
<p>ステータスに含まれる値と意味は下記のとおりです。</p>
<table style="border-style: dotted;">
<tbody>
<tr>
<td style="border-style: dotted;" width="250">
<p><span style="font-size: 10pt;">Fully Operational</span></p>
</td>
<td style="border-style: dotted;" width="433">
<p><span style="font-size: 10pt;">エンジンは稼働中です。</span></p>
</td>
</tr>
<tr>
<td style="border-style: dotted;" width="250">
<p><span style="font-size: 10pt;">Synthetic Check Failed</span></p>
</td>
<td style="border-style: dotted;" width="433">
<p><span style="font-size: 10pt;">オートデスクが利用する AWS 側で問題が発生・サービスが停止したり、フロントエンドのコードに何か問題が発生したりすると、すべてのエンジンに「Synthetic Check Failed」が返されます。1つのエンジンのみに「Synthetic Check Failed」が返される場合、同エンジンがSynthetic Check WorkItemに失敗したり、Synthetic Check WorkItemの処理が間に合わなかったり（トラフィックが多すぎる）する状態を示します。</span></p>
</td>
</tr>
<tr>
<td style="border-style: dotted;" width="250">
<p><span style="font-size: 10pt;">Health status does not exist in S3</span></p>
</td>
<td style="border-style: dotted;" width="433">
<p><span style="font-size: 10pt;">まだ実装されていない未稼働のエンジンがある場合に返されます。</span></p>
</td>
</tr>
</tbody>
</table>
<p>障害発生時には、APS のエンドポイント呼び出しで、Rate Limit（呼び出し数制限）を示す 429 レスポンスが返される場合があります。この際には、即座に再リクエストするのではなく。&quot;retry-after &quot; レスポンス ヘッダーに従って、示された x 秒後に再試行するような実装をお願いします。詳細は、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/#rate-limits" rel="noopener" target="_blank">Rate Limit ドキュメント</a>をご確認ください。</p>
<p>By Toshiaki Isezaki</p>
