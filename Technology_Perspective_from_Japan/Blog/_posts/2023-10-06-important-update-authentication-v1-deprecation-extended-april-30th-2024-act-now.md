---
layout: "post"
title: "重要：OAuth API v1 非推奨日を2024年4月30日まで延長"
date: "2023-10-06 00:18:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/10/important-update-authentication-v1-deprecation-extended-april-30th-2024-act-now.html "
typepad_basename: "important-update-authentication-v1-deprecation-extended-april-30th-2024-act-now"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39f9b0d200d-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39f9b0d200d image-full img-responsive" src="/assets/image_18069.jpg" title="Placeholder - Blog images_Lifestyle 16x9" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/05/migration-guide-oauth2-v1-to-v2.html" rel="noopener" target="_blank"><strong>OAuth2 v1 から v2 への移行ガイド</strong></a> でご案内した Autodesk Platform Services（旧 Forge）の OAuth v1 API（Authentication API v1）の廃止に関して、お伝えしたい重要なお知らせがあります。ログから得られる移行状況や皆様からのフィードバックに鑑みて、円滑な移行を実現するため、Authentication v1 廃止日の延長を決定しました。</p>
<p><strong>非推奨日の延長</strong></p>
<p style="padding-left: 40px;">新しい期限は <strong>2024 年 4 月 30 日</strong>です。この延長により、お客様のアプリケーションやワークフローを Authentication API v2 にシームレスに移行するための時間を確保することができます。 OAuth（Authentication） は、Autodesk Platform Services の基礎となるサービスであり、OpenID 標準などの新しい Web 標準を採用することで、時代とともに進化してきました。最先端のツールをお客様に提供するための取り組みの一環として、<strong>PCKE</strong> のサポートや <a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/revoke-POST/" rel="noopener" target="_blank">/revoke</a> および <a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/logout-GET/" rel="noopener" target="_blank">/logout</a> エンドポイント導入などの機能強化を特徴とする Authentication API v2 を導入しました。</p>
<p><strong>なぜ重要なのか</strong></p>
<p style="padding-left: 40px;"><strong>2024年4月30日が Authentication API v1 の最終利用日となります。</strong>サービスの中断を避けるため、この日までに移行を完了することの重要性を強調したいと思います。この延長は、円滑な移行を確実にするための一度限りの機会であり、この機会を最大限にご活用ください。</p>
<p><strong>既存アプリの変更点</strong></p>
<p style="padding-left: 40px;">スムーズな移行プロセスのため、ご理解いただきたい既存アプリに必要な変更点は次のとおりです。</p>
<ul>
<li><strong>2-legged ワークフロー：</strong>Client ID と Client Secret をヘッダーに持つ新しい&#0160;<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank"><strong>/token</strong></a> エンドポイントをお使いください。詳しいチュートリアルは<strong><a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank">こちら</a></strong>。</li>
<li><strong>3-legged ワークフロー：</strong> 新しい&#0160; <a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/authorize-GET/" rel="noopener" target="_blank"><strong>/authorize</strong></a> エンドポイントでオーソライゼーション コードを取得し、取得したコードから&#0160;<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank"><strong>/token</strong>&#0160;</a> エンドポイントでアクセス トークンを得るようにしてください。包括的なチュートリアルは<a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">こちら</a>。</li>
</ul>
<p style="padding-left: 40px;">このプロセスの step-by-step ウォークスルーについては、詳細な&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2023/05/migration-guide-oauth2-v1-to-v2.html" rel="noopener" target="_blank"><strong>OAuth2 v1 から v2 への移行ガイド</strong></a>&#0160;を参照することを強くお勧めします。</p>
<p><strong>サポートが必要な場合</strong></p>
<p style="padding-left: 40px;">この移行期間中にご不明な点や問題が発生した場合は、専任のサポートチームがお手伝いいたします。&#0160;<strong><a href="mailto:aps.help@autodesk.com" rel="noopener" target="_blank" title="mailto:aps.help@autodesk.com">aps.help@autodesk.com</a>&#0160;</strong>までお気軽にお問い合わせください。</p>
<p><strong>まとめ</strong></p>
<p style="padding-left: 40px;">Authentication v2 がもたらすイノベーションに期待しています。Autodesk Platform Services コミュニティにご参加いただきありがとうございます。皆様の継続的なご協力に感謝するとともに、次世代の画期的なアプリケーションを共に開発できることを楽しみにしています。</p>
<p style="padding-left: 40px;">アプリケーションを将来に備えるために、今すぐ移行作業への着手をお願いします。<strong>2024年4月30日が Authentication v1 の最終期限</strong>です。&#0160;</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/important-update-authentication-v1-deprecation-extended-april-30th-2024-act-now" rel="noopener" target="_blank">Important Update: Authentication v1 Deprecation Extended to April 30th, 2024 – Act Now! | Autodesk Platform Services</a>&#0160;から転写・翻訳、一部補足を加味したものです。</p>
<p>By Toshiaki Isezaki</p>
