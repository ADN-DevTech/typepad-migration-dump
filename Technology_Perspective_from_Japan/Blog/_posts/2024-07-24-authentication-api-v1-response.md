---
layout: "post"
title: "OAuth API v1（Authentication API v1）呼び出し時のレスポンス"
date: "2024-07-24 00:26:32"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/07/authentication-api-v1-response.html "
typepad_basename: "authentication-api-v1-response"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af3f74200c-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af3f74200c image-full img-responsive" src="/assets/image_28034.jpg" title="Aps" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/05/important-update-authentication-v1-migration-extended-may-22nd-2024-final.html" rel="noopener" target="_blank">重要な更新：OAuth API v1 移行延長-2024年5月22日 - 最終！</a> 等でご案内のとおり、アクセス トークンを取得するための OAuth API <strong>v1</strong>（Authentication API <strong>v1</strong>）が廃止され、OAuth API <strong>v2</strong>（Authentication API <strong>v2</strong>）に移行しています。</p>
<p>現在、OAuth API <strong>v1</strong>（Authentication API <strong>v1</strong>）のエンドポイントを呼び出すと、次のようなエラーが返されますのでご注意ください。</p>
<p>具体的には、<strong>2</strong>-legged で <a class="reference external" href="https://aps.autodesk.com/en/docs/oauth/v1/reference/http/authenticate-POST" rel="noopener" target="_blank">POST authenticate</a> エンドポイント呼び出し時、または、3-legged で <a class="reference external" href="https://aps.autodesk.com/en/docs/oauth/v1/reference/http/authorize-GET" rel="noopener" target="_blank">GET authorize</a> エンドポイント呼び出し時、<strong>401 Unauthorized</strong><span class="response-meta-status-code-desc" data-testid="response-status-code-desc">&#0160;エラー ステータス値とともに、次のレスポンス ボディが返されます。</span></p>
<blockquote>
<p>{<br />&#0160; &#0160; &quot;developerMessage&quot;: &quot;Deprecated Service, please contact aps.help@autodesk.com to learn more on migrating to Oauth v2.&quot;,<br />&#0160; &#0160; &quot;moreInfo&quot;: &quot;https://forge.autodesk.com/en/docs/oauth/v2/developers_guide/error_handling/&quot;,<br />&#0160; &#0160; &quot;errorCode&quot;: &quot;AUTH-666&quot;<br />}</p>
</blockquote>
<p>OAuth API <strong>v1</strong>（Authentication API <strong>v1</strong>）をお使いの場合には、<a href="https://adndevblog.typepad.com/technology_perspective/2023/05/migration-guide-oauth2-v1-to-v2.html" rel="noopener" target="_blank">OAuth2 v1 から v2 への移行ガイド</a> をご確認ください。</p>
<p>By Toshiaki Isezaki</p>
