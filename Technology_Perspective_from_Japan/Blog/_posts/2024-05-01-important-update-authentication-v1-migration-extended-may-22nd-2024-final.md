---
layout: "post"
title: "重要な更新：OAuth API v1 移行延長-2024年5月22日 - 最終！"
date: "2024-05-01 00:01:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/important-update-authentication-v1-migration-extended-may-22nd-2024-final.html "
typepad_basename: "important-update-authentication-v1-migration-extended-may-22nd-2024-final"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b17dcc200b-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b17dcc200b img-responsive" src="/assets/image_20097.jpg" title="Placeholder - Blog images_Lifestyle 16x9" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>Autodesk Platform Services コミュニティの皆様</p>
<p>Autodesk Platform Services（旧 Forge）の Authentication v1 の廃止に関して、皆様にお伝えしたい重要なお知らせがあります。皆様からのフィードバックにお応えし、円滑な移行を実現するため、Authentication v1 の廃止日を延長することを決定しました。</p>
<p><strong>廃止日の延長</strong></p>
<p>新しい期限は<strong> 2024 年 5 月 22 日&#0160;</strong>に設定されます。この延長により、アプリとワークフローの Authentication v2 への移行を最終的に完了するための期間が<strong> 3 週間延長</strong>されることになります。</p>
<p><strong>なぜ重要なのか</strong></p>
<p><strong>2024年5月22日が Authentication v1 の最終利用日となります。</strong>サービスの中断を避けるため、この期日までに移行を完了することの重要性を強調したいと思います。この延長は、移行を円滑に行うための最後の機会であり、この機会を最大限に活用されることを強くお勧めします。</p>
<p><strong>既存アプリへの変更</strong></p>
<p>スムーズな移行プロセスのため、ご理解いただきたい既存アプリに必要な変更点は次のとおりです。</p>
<ul>
<li><strong>2-legged ワークフロー：</strong>Client ID と Client Secret をヘッダーに持つ新しい&#0160;<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank"><strong>/token</strong></a>&#0160;エンドポイントをお使いください。詳しいチュートリアルは<strong><a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank">こちら</a></strong>。</li>
<li><strong>3-legged ワークフロー：</strong>&#0160;新しい&#0160;&#0160;<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/authorize-GET/" rel="noopener" target="_blank"><strong>/authorize</strong></a>&#0160;エンドポイントでオーソライゼーション コードを取得し、取得したコードから&#0160;<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank"><strong>/token</strong>&#0160;</a>&#0160;エンドポイントでアクセス トークンを得るようにしてください。包括的なチュートリアルは<a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">こちら</a>。</li>
</ul>
<p>このプロセスの step-by-step ウォークスルーについては、詳細な&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2023/05/migration-guide-oauth2-v1-to-v2.html" rel="noopener" target="_blank"><strong>OAuth2 v1 から v2 への移行ガイド</strong></a>&#0160;を参照することを強くお勧めします。</p>
<p><strong>サポートが必要な場合</strong></p>
<p>この移行期間中にご不明な点や問題が発生した場合は、専任のサポートチームがお手伝いいたします。&#0160;<strong><a href="mailto:aps.help@autodesk.com" rel="noopener" target="_blank" title="mailto:aps.help@autodesk.com">aps.help@autodesk.com</a>&#0160;</strong>までお問い合わせください。</p>
<p><strong>まとめ</strong></p>
<p>Autodesk Platform Services コミュニティにご参加いただきありがとうございます。皆様の継続的なご協力に感謝するとともに、次世代の画期的なアプリケーションを共に開発できることを楽しみにしています。一緒にスムーズな移行を実現しましょう！</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/important-update-authentication-v1-migration-extended-may-22nd-2024-final" rel="noopener" target="_blank">Important Update: Authentication V1 migration extended - May 22nd, 2024 - Final! | Autodesk Platform Services</a> から転写・翻訳したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
