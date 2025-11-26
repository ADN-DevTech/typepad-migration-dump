---
layout: "post"
title: "WebHooks API：VS Code から ACC 監視を登録"
date: "2024-12-18 00:02:36"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/register-watching-acc-from-vs-code.html "
typepad_basename: "register-watching-acc-from-vs-code"
typepad_status: "Publish"
---

<p>APS の WebHooks API を利用すると、オートデスクのクラウド サービスで発生したイベントを検出、APS アプリに通知してくれます。この手法で Autodesk Construction Cloud（ACC）上の挙動を把握することが出来ます。</p>
<p>&#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860da9a9c200b-pi" style="display: inline;"><img alt="Webhooks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860da9a9c200b image-full img-responsive" src="/assets/image_407627.jpg" title="Webhooks" /></a></p>
<p>APS の WebHooks API は<a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/" rel="noopener" target="_blank">様々なイベントをサポート</a>していますが、この内、ACC 上の監視で取得することが出来るイベントは <a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/#data-management-events" rel="noopener" target="_blank">Data Management Events</a> に記載のあるとおりです。</p>
<p>WebHooks API は、VS Code APS エクステンションで簡単に登録することが出来るので、実際のアプリ実装前に動作の評価・検証が可能です。ここでは、ACC 上のフォルダ追加を例に、テストの手順をご紹介しておきたいと思います。</p>
<hr />
<p><strong>事前把握が必要な情報</strong></p>
<p>ACC プロジェクトにフォルダが追加された通知を得るためには、<a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/http/webhooks/systems-system-events-event-hooks-POST/" rel="noopener" target="_blank">POST systems/:system/events/:event/hooks</a> エンドポイントで <a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/data_management_events/dm.folder.added/" rel="noopener" target="_blank">dm.folder.added</a> イベントと一緒に通知を得るためのコールバック URL を登録する必要があります。</p>
<p>この時必要になるのが、監視対象のプロジェクトの Project Id と、監視対象のフォルダーの Folder Id（URN）です。言い換えれば、Postman などを使って、事前に Project Id と Folder Id（URN）を把握しておいてください。</p>
<p>ここでは、次の Project Id と Folder Id の値を想定して、イベント通知をテストしてみます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dca483200b-pi" style="display: inline;"><img alt="Acc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dca483200b image-full img-responsive" src="/assets/image_349097.jpg" title="Acc" /></a></p>
<hr />
<p><strong>WebHook の登録（作成）</strong></p>
<ol>
<li>ここでは、コールバック URL に <strong>Webhook.site</strong> という WebHook のテストに便利なサイトを無償枠で使用します。Web ブラウザ で <a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwebhook.site%2F&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7C0f2c65e1d6db48b4bf7c08dd1834fc11%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638693338041890995%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&amp;sdata=%2Bd%2BJ5dUHbkE%2FpIs67L88Y46mPMuY6hpNviplEBAK8vA%3D&amp;reserved=0">https://webhook.site/</a> を表示すると、<strong>Your unique URL</strong> にコールバック URL に使用出来る URL が表示されますので、クリップボードにコピーしておきます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c613df200c-pi" style="display: inline;"><img alt="Webhook.site" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c613df200c image-full img-responsive" src="/assets/image_101965.jpg" title="Webhook.site" /></a></li>
<li>VS Code を起動して、APS エクステンション サイドバーの WebHooks パネルから <strong>Folder Added</strong> を見つけて、マウスの右クリック ボタンで [Create Webhook] を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dca499200b-pi" style="display: inline;"><img alt="Create_webhook" class="asset  asset-image at-xid-6a0167607c2431970b02e860dca499200b img-responsive" src="/assets/image_544181.jpg" title="Create_webhook" /></a></li>
<li>VS Code 上に [Create Webhook] タブが表示されるので、あらかじめ設定されている System 項と Event 項を除いた項目に値を指定します。Scope 項には、監視対象の Folder Id（<span style="color: #ff0000;"><strong>urn:adsk.wipprod:fs.folder:co.FtKfsRS2Re6g0keqduq8tw</strong></span>）を、Callback URL 項には Webhook.site からコピーした URL（<span style="color: #ff0000;"><strong>https://webhook.site/080759ea-11f8-4df1-a50b-297d60044e65</strong></span>）を、また、Attribute 項には Project Id を示す JSON 値（<strong>{&quot;projectId&quot;:&quot;<span style="color: #ff0000;">b.2bff3069-8411-49ae-ab1b-7d6d5747dfd7</span>&quot;}</strong>）を指定します。すべの値が指定出来たら、タブ画面下部の [Create] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c61403200c-pi" style="display: inline;"><img alt="Webhook_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c61403200c image-full img-responsive" src="/assets/image_195860.jpg" title="Webhook_settings" /></a></li>
<li>WebHook の登録が完了すると、WebHooks パネルから Folder Added 下に作成した WebHook が表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c6141a200c-pi" style="display: inline;"><img alt="Created_webhook" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c6141a200c img-responsive" src="/assets/image_476416.jpg" title="Created_webhook" /></a></li>
</ol>
<hr />
<p><strong>補足・注意事項</strong></p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/data_management_events/dm.folder.added/" rel="noopener" target="_blank">dm.folder.added</a> イベントの通知を得るためには、Autodesk Construction Cloud にアクセスする必要があることを意味します。つまり、同イベントの WebHook を登録する APS アプリの Client ID は、事前に「カスタム統合」が必要になります。「カスタム統合」の具体的な手順は、次の記事でご案内しています。<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank"></a>&#0160;
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html" rel="noopener" target="_blank">ACC：新しいカスタム統合 UI</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a></li>
</ul>
</li>
<li>WebHooks の登録には、2-legged、または、3-legged 認証フロー共に利用することが出来ます。VS Code APS エクステンションでは、複数の Client ID の切り替えをサポートしています。<a href="https://adndevblog.typepad.com/technology_perspective/2024/12/switch-client-id-on-vs-code-aps-extension.html" rel="noopener" target="_blank">VS Code APS エクステンションの Client ID 切り替え</a><a href="https://adndevblog.typepad.com/technology_perspective/2021/09/switching-environment-on-vs-code-forge-tools.html" rel="noopener" target="_blank"></a>&#0160;の内容を確認してみてください。APS エクステンションでも利用することが出来ます。</li>
<li><a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/data_management_events/dm.folder.added/" rel="noopener" target="_blank">dm.folder.added</a> イベントを含む <a href="https://aps.autodesk.com/en/docs/webhooks/v1/reference/events/#data-management-events" rel="noopener" target="_blank">Data Management イベント</a> の監視に必要なエンドポイントの呼び出し手順、および、リクエスト ボディ等の詳細は、<a class="adskf__sidebar-link  active  " href="https://aps.autodesk.com/en/docs/webhooks/v1/tutorials/create-a-hook-data-management" id="c777be51-0266-32f3-90b6-be1aa759e2cb" rel="noopener" target="_blank">Creating a Webhook (Data Management)</a>&#0160;で紹介されています。</li>
</ul>
<hr />
<p><strong>動作確認</strong></p>
<p>ACC 上の監視対象フォルダにサブフォルダを追加すると（下図、左手）、Webhook.site が生成して WebHook 登録時に指定したコールバック URL に、フォルダの追加イベントが通知されます。同時にペイロードに詳細が返されます（下図、右手）。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f3c1cf200d-pi" style="display: inline;"><img alt="Folder_added" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f3c1cf200d image-full img-responsive" src="/assets/image_220977.jpg" title="Folder_added" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>
