---
layout: "post"
title: "AEC Data Model API への OAuth data:read スコープを適用義務化"
date: "2025-02-17 00:03:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/aec-data-model-api-enforcing-oauth-dataread-scope-beginning-october-1-2025.html "
typepad_basename: "aec-data-model-api-enforcing-oauth-dataread-scope-beginning-october-1-2025"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e2c4da200b-pi" style="display: inline;"><img alt="6a0167607c2431970b02e860f9cb5b200d" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e2c4da200b image-full img-responsive" src="/assets/image_404313.jpg" title="6a0167607c2431970b02e860f9cb5b200d" /></a></p>
<p>オートデスクは、<strong>AEC Data Model API</strong> の OAuth スコープの使用を必須にすることを計画しています。現在使用出来るすべての読み取り用 GraphQL クエリの実行には、今後、アプリが使用するアクセス トークンに OAuth <strong>data:read&#0160;</strong>スコープが指定されている必要があります。</p>
<h3><strong>必要なアクション</strong></h3>
<p>AEC Data Model API をお使いの場合、GraphQL クエリに OAuth <strong>data:read</strong> スコープが指定されていることをご確認ください。</p>
<p>もし、<strong>data:read</strong> スコープが未指定な場合には、<strong>2025 年 10 月 1</strong> 日までにアプリを更新する必要があります。OAuth <strong>data:read</strong> スコープなしで取得したアクセス トークンを使用すると、AEC Data Model クエリが失敗するようになります。</p>
<p>この記事の執筆時点（2025年2月）では、AEC Data Model の書き込み用 API は、まだ用意されていない点にご注意ください。将来、書き込み用 API が利用可能になった場合には、OAuth スコープと同じ要件が適用されます。アプリは、クエリを実行するための正しい OAuth スコープ <em>(data:write や data:create など) を持つ必要があります。</em></p>
<p>AEC Data Model APIの OAuth スコープの詳細については、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/scopes/" rel="noopener noreferrer" target="_blank">こちらのページ</a> をご確認ください。</p>
<p>AEC Data Model API の&#0160; OAuth スコープの適用についてご質問をお持ちの場合は、<a  _istranslated="1" href="https://aps.autodesk.com/get-help" rel="noreferrer noopener" target="_blank">APS サポート</a> までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/aec-data-model-api-enforcing-oauth-dataread-scope-beginning-october-1-2025" rel="noopener" target="_blank">AEC Data Model API enforcing OAuth data:read scope beginning October 1, 2025 | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
