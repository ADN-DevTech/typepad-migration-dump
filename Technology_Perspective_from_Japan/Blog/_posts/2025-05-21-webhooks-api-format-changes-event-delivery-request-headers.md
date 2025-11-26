---
layout: "post"
title: "Webhooks API： イベント配信リクエストヘッダーのフォーマット変更"
date: "2025-05-21 00:18:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/05/webhooks-api-format-changes-event-delivery-request-headers.html "
typepad_basename: "webhooks-api-format-changes-event-delivery-request-headers"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eaaf1d200b-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860eaaf1d200b image-full img-responsive" src="/assets/image_822673.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>オートデスクは、WebHooks&#0160; API で検出したイベントを外部コールバック URL に配信する際の Webhooks API のイベント配信リクエスト ヘッダーの形式に変更を加える予定です。具体的には、既存のヘッダー値の一部を変換します。これらのヘッダー フィールドは以前に公開されておらず、トレースを目的としてで内部のみで使用することを意図しているため、これは便宜上の注意です。</p>
<p>フォーマットの変更は、以下のドキュメント化されていないヘッダーフィールドに影響します。</p>
<ul>
<li>x-ads-ul-ctx-tracking-id</li>
<li>x-ads-ul-ctx-head-span-id</li>
<li>x-adsk-delivery-id</li>
</ul>
<p>次の表は、変更前と変更後の違いを示しています。</p>
<table class="Table" style="border-style: dotted; width: 624px;" width="624">
<tbody>
<tr>
<td style="width: 100px; border-style: dotted;" valign="top">&#0160;</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>変更前（現在）</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>変更後（2025年6月24日～）</p>
</td>
</tr>
<tr>
<td style="width: 100px; border-style: dotted;" valign="top">
<p>x-ads-ul-ctx-tracking-id</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>httpadapter~autodesk.data.fevnt.example~1-68224086-e98bac01d35d6b918ce393c1</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>httpadapter~autodesk.data.fevnt.example~<br />5059ff4f452f4a3ebb50677da14869e2</p>
</td>
</tr>
<tr>
<td style="width: 100px; border-style: dotted;" valign="top">
<p>x-ads-ul-ctx-head-span-id</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>1-68224086-e98bac01d35d6b918ce393c1</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>50851c1ea26c9cdf</p>
</td>
</tr>
<tr>
<td style="width: 100px; border-style: dotted;" valign="top">
<p>x-adsk-delivery-id</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>httpadapter~autodesk.data.fevnt.example~1-68224086-e98bac01d35d6b918ce393c1/19234e4a-df4f-4fed-9b65-9ed18585727b</p>
</td>
<td style="width: 350px; border-style: dotted;" valign="top">
<p>httpadapter~autodesk.data.fevnt.example~<br />5059ff4f452f4a3ebb50677da14869e2/269e11c4-3c93-4458-ab50-7abecc3df2ec</p>
</td>
</tr>
</tbody>
</table>
<p><strong>必要なアクション</strong></p>
<p>上記のヘッダーフィールドの値に返信する場合は、<strong>2025年6月24</strong>日 から適用される形式の変更を処理するようにコードを更新をお願いしますい。注: リクエスト自体の配信ボディに変更はありません。API の意図された機能は影響を受けません。</p>
<p>この変更についてご不明な点がございましたら、<a href="https://aps.autodesk.com/get-help">APS support</a>&#0160;までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/webhooks-api-format-changes-event-delivery-request-headers" rel="noopener" target="_blank">Webhooks API: format changes to the event delivery request headers | Autodesk Platform Services</a> から転写・意訳したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
