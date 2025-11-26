---
layout: "post"
title: "BIM 360 Issues (v1) エンドポイントの廃止予告"
date: "2023-01-25 00:06:02"
author: "Toshiaki Isezaki"
categories: []
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/01/retiring-bim-360-issues-v1-endpoints.html "
typepad_basename: "retiring-bim-360-issues-v1-endpoints"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af148d9847200c-pi" style="display: inline;"><img alt="Default-blog-image-3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af148d9847200c image-full img-responsive" src="/assets/image_355226.jpg" title="Default-blog-image-3" /></a></div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">お使いの APS アプリで BIM 360 Issues (v1) APIをお使いの場合には、本記事をご一読ください。&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">BIM 360 Issues (<strong>v2</strong>) API の公開開始にともない、オートデスクでは 、BIM 360 Issues (<strong>v1</strong>) API に含まれる 11 個のエンドポイントを廃止する予定にしています。Issues API は BIM 360 全体で共有されているコンポーネントで、さまざまなモジュールで使用されています。BIM 360 の Issues（指摘事項）は、BIM 360 Docs が最初に導入された際に存在した機能の 1 つで、以降、他のサービスによって拡張され、適応されてきた経緯があります。BIM 360 Issues (<strong>v2</strong>) API の詳細については、こちらの<a href="https://aps.autodesk.com/blog/bim-360-issues-version-2-api-released-1" rel="noopener" target="_blank">ブログ記事</a>をご確認ください。&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">BIM 360 Issues (<strong>v1</strong>) API エンドポイントを廃止する計画は、BIM 360 にのみ適用されます。Autodesk Construction Cloud (ACC) ユニファイド製品は、影響を受けません。&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><strong>必要なアクション：</strong></div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">次の BIM 360 Issues (v1) API エンドポイントをお使いの場合には、<strong>2023 年 7 月 5 日まで</strong>に Issues (v2) API に移行していたただくようお願いいたします。7 月 5 日以降 Issues (v1) API エンドポイントは非推奨となり、機能停止してしまうため、アプリが期待した動作をしなくなります。</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item">&#0160;</div>
<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><strong>BIM 360 Issues (v1) API （廃止予定のエンドポイント）：&#0160;&#0160;</strong></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<ul>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/users-me-GET/" rel="noopener" target="_blank">GET users/me</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-GET/" rel="noopener" target="_blank">GET issues</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-:id-GET/" rel="noopener" target="_blank">GET issues/:id</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-POST/" rel="noopener" target="_blank">POST issues</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-:id-PATCH/" rel="noopener" target="_blank">PATCH issues/:id</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-:id-comments-GET/" rel="noopener" target="_blank">GET issues/:id/comments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-comments-POST/" rel="noopener" target="_blank">POST issues/comments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-attachments-GET/" rel="noopener" target="_blank">GET issues/attachments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/field-issues-attachments-POST/" rel="noopener" target="_blank">POST issues/attachments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/root-causes-GET/" rel="noopener" target="_blank">GET root-causes</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/ng-issue-types-GET/" rel="noopener" target="_blank">GET ng-issue-types</a></li>
</ul>
<p><strong>BIM 360 Issues (v2) API（代替すべきエンドポイント）</strong></p>
<ul>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-issues-issueId-GET/" rel="noopener" target="_blank">GET issues/:issueId</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-issues-POST/" rel="noopener" target="_blank">POST issues</a>&#0160;</li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-issues-issueId-PATCH/" rel="noopener" target="_blank">PATCH issues/:issueId</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-comments-GET/" rel="noopener" target="_blank">GET comments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-comments-POST/" rel="noopener" target="_blank">POST comments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-attachments-GET/" rel="noopener" target="_blank">GET attachments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-attachments-POST/" rel="noopener" target="_blank">POST attachments</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-issue-root-cause-categories-GET/" rel="noopener" target="_blank">GET issue-root-cause-categories</a></li>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/reference/http/issues-v2-issue-types-GET/" rel="noopener" target="_blank">GET issue-types</a></li>
</ul>
<p>BIM 360 Issues API (v1) から (v2) への移行ガイドの詳細については、次のドキュメントをご確認ださい。</p>
<ul>
<li><a class="adskf__link-dashed" href="https://aps.autodesk.com/en/docs/bim360/v1/overview/migration-guides/Issues_v1_to_v2/" rel="noopener" target="_blank" title="BIM 360 Issues API v1 to v2 migration guide">BIM 360 Issues (v1) to (v2) Migration Guide</a>&#0160;</li>
</ul>
<p>移行作業で問題が発生した場合、または、ご質問をお持ちの場合には、<a href="mailto://aps.help@autodesk.com">aps.help@autodesk.com</a> までご連絡ください。&#0160; &#0160;</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/retiring-bim-360-issues-v1-endpoints" rel="noopener" target="_blank">Retiring BIM 360 Issues (v1) Endpoints | Autodesk Platform Services</a> から転写・翻訳して一部加筆。修正したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
