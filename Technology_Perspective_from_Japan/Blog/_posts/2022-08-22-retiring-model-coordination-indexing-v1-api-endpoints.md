---
layout: "post"
title: "Model Coordination Indexing (v1) API エンドポイント廃止について"
date: "2022-08-22 00:41:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/08/retiring-model-coordination-indexing-v1-api-endpoints.html "
typepad_basename: "retiring-model-coordination-indexing-v1-api-endpoints"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed1d514200d-pi" style="display: inline;"><img alt="Office-SF-300_Mission-8276_with_overlay" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed1d514200d image-full img-responsive" src="/assets/image_979194.jpg" title="Office-SF-300_Mission-8276_with_overlay" /></a></p>
<p>BIM 360 または Autodesk BIM Collaborate で Model Coordination の Indexing (v1) API をお使いでしょうか？&#0160; もし、お使いの場合には、こちらの記事をご確認ください。&#0160;&#0160;</p>
<p>オートデスクは、今年初めに Model Properties API をリリースしました。これは、モデルのプロパティを照会、フィルタリング、比較するための強力な機能を備えています。この Model Properties API は Model Coordination の Indexing (v1) API の後継にあたるもので、Model Coordination API の機能を拡張しているため、Autodesk/BIM 360 Docs ベースのあらゆる製品で使用することができます。&#0160;</p>
<p>Model Properties API の導入にともない、Model Coordination 固有の Indexing (v1) API エンドポイント 4 つを廃止する予定です。</p>
<p><u><strong>必要なアクション&#0160;</strong></u></p>
<p>Model Coordination&#39;s Indexing (v1) API をご利用の方は、2023 年 1 月 9 日(*1)までに Model Properties API へ移行をお願いいたしま。その時点で、Model Coordination&#39;s Indexing (v1) API エンドポイントは非推奨となり機能を停止する予定です。&#0160;&#0160;</p>
<p><strong>Model Coordination Indexing (v1) API エンドポイント（廃止対象）：&#0160;</strong></p>
<ul>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/mc-index-service-v1-query-model-set-version-index-manifest-GET/" rel="noopener" target="_blank">GET &#0160;&#0160; &#0160;modelsets/:modelSetId/versions/:version/manifest</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/mc-index-service-v1-query-model-set-version-index-fields-GET/" rel="noopener" target="_blank">GET &#0160;&#0160; &#0160;modelsets/:modelSetId/versions/:version/fields</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/mc-index-service-v1-query-model-set-version-index-POST/" rel="noopener" target="_blank">POST&#0160; &#0160;modelsets/:modelSetId/versions/:version/indexes:query</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/mc-index-service-v1-get-model-set-job-GET/" rel="noopener" target="_blank">GET &#0160;&#0160; &#0160;modelsets/:modelSetId/jobs/:jobId</a>&#0160;</li>
</ul>
<p>上記エンドポイントは、次にご案内するエンドポイントで置き換えをお願いいたします。&#0160;</p>
<p><strong>Model Properties API エンドポイント（代替）：&#0160;</strong></p>
<ul>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/acc/v1/reference/http/index-v2-index-manifest-get/" rel="noopener" target="_blank">GET&#0160;&#0160; &#0160;indexes/:indexId/manifest</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/acc/v1/reference/http/index-v2-index-fields-get/" rel="noopener" target="_blank">GET&#0160;&#0160; &#0160;indexes/:indexId/fields</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/acc/v1/reference/http/index-v2-index-query-job-status-get/" rel="noopener" target="_blank">POST&#0160; indexes/:indexId/queries</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://forge.autodesk.com/en/docs/acc/v1/reference/http/index-v2-index-status-get/" rel="noopener" target="_blank">GET &#0160;&#0160; indexes/:indexId</a>&#0160;</li>
</ul>
<p>（上記リンクでは ACC ページへリンクしていますが、BIM 360 と ACC の両方の環境で動作します。）</p>
<p>Model Properties API の詳細については、こちらの<a href="https://forge.autodesk.com/blog/bim-360acc-model-properties-api" rel="noopener" target="_blank">ブログ記事</a>をご覧ください。&#0160; &#0160;</p>
<p>移行に関して問題をお持ち、または、ご質問がある場合は、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a> までご連絡ください。&#0160; &#0160; &#0160; &#0160;&#0160;</p>
<p>(*1) 該当する開発者がすべて移行したことが確認された場合、予定よりも早期に非推奨とする可能性があります。&#0160; &#0160;</p>
<p>※ 本記事は <a href="https://forge.autodesk.com/blog/retiring-model-coordination-indexing-v1-api-endpoints" rel="noopener" target="_blank">Retiring Model Coordination Indexing (v1) API Endpoints | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</p>
<p>By Toshiaki Isezaki</p>
