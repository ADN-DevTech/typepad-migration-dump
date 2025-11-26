---
layout: "post"
title: "Design Automation API: OAuth スコープの適用 (期限延長 – 2025 年 3 月 31 日)"
date: "2025-02-03 00:23:27"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/design-automation-api-enforcing-oauth-scope.html "
typepad_basename: "design-automation-api-enforcing-oauth-scope"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f83872200d-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f83872200d image-full img-responsive" src="/assets/image_849436.jpg" title="Aps" /></a></p>
<p>2024 年 8 月に、<a href="https://adndevblog.typepad.com/technology_perspective/2024/08/design-automation-api-enforcing-oauth-scope.html" rel="noopener" target="_blank">Design Automation API への OAuth スコープ code:all 適用</a>&#0160;の記事でご案内しました、Design Automation API 利用時の code:all スコープ指定について、一部のアプリで移行完了までに時間を要することが判明しましたので、<strong>期限を 2025 年 3 月 31 日までの延長</strong>する決定がされました。</p>
<p><strong>必要なアクション</strong></p>
<p>Design Automation API をお使いの場合には、アプリの認証プロセスで OAuth <strong>code:all</strong> スコープの使用を再度ご確認ください。正しいスコープを使用していない場合、<strong>2025 年 3 月 31 日までにアプリを更新する</strong>必要があります。期日後、 <strong>code:all</strong> スコープなしで生成されたアクセストークンを使用した Design Automation API アプリはエンドポイントの呼び出しは失敗します。既に <strong>code:all</strong> スコープを使用しされている場合は、これ以上のアクションは不要です。</p>
<p>Design Automation API の OAuth スコープの適用についてご質問をお持ちの場合には、<a  _istranslated="1" href="https://aps.autodesk.com/get-help">APS Support</a>までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/design-automation-api-enforcing-oauth-scope-deadline-extension-march-31-2025" rel="noopener" target="_blank">Design Automation API: Enforcing OAuth Scope (Deadline Extension – March 31, 2025) | Autodesk Platform Services</a>&#0160;から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
