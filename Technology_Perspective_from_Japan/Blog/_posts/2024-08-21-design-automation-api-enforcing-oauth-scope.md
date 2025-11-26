---
layout: "post"
title: "Design Automation API への OAuth スコープ code:all 適用"
date: "2024-08-21 00:18:58"
author: "Toshiaki Isezaki"
categories: []
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/design-automation-api-enforcing-oauth-scope.html "
typepad_basename: "design-automation-api-enforcing-oauth-scope"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bcf36e200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bcf36e200b image-full img-responsive" src="/assets/image_201288.jpg" title="Aps" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>Design Automation API をお使い場合は、アクセストークンの取得時に&#0160;<strong>code:all </strong>の&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2019/06/scopes-on-oauth.html" rel="noopener" target="_blank"><strong>Scope</strong></a> 値が設定されているか、今一度ご確認ください。</p>
<p>この Scope 値は Design Automation API では元々必須なものですが、<strong>2025年2月1日</strong>以降、同値が設定されていないアクセス トークンを使ってしまうと、Design Automation API のすべてのエンドポイントがアクセスを拒否するようになります。</p>
<p>デバッグ環境も含め、念のため、ご注意ください。</p>
<p>By Toshiaki Isezaki</p>
</div>
