---
layout: "post"
title: "Forge OAuth API アプリ ユーザの電子メール検証"
date: "2020-03-18 00:14:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/03/email-verification-requirements-forge-authentication-api-application-users.html "
typepad_basename: "email-verification-requirements-forge-authentication-api-application-users"
typepad_status: "Publish"
---

<div class="node__content adskf__section-group">
<div class="node__image"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b363a42200c-pi" style="display: inline;"><img alt="Shutterstock_699442621" border="0" class="asset  asset-image at-xid-6a0167607c2431970b025d9b363a42200c image-full img-responsive" src="/assets/image_414072.jpg" title="Shutterstock_699442621" /></a></div>
<div class="node__body">
<p>Forge 認証サービス endpoint 用のメール アカウント検証が、<strong>2020年3月30日</strong>に施行される予定です。具体的には、新規ユーザにはサインアップ時に、既存ユーザにはサインイン時に、それぞれ電子メール アドレスを検証するように求められます。&#0160;</p>
<h4>なぜメール アカウント検証が必要なのか？</h4>
<p>Forge ユーザの皆様のセキュリティとカスタマーサポートの両方を改善し、また、同時に、 他のオートデスクのサービス アクセス要件との整合性をとる一として、お使いの電子メール アドレスの確認（検証）を要求ことになりました。</p>
<p>これには次の利点があります。</p>
<ul>
<li>アクセス サポートを改善し、ユーザが誤ってアカウントから誤って排除されてしまうことを防止します。</li>
<li>オートデスクのクラウド サービスへのアクセスとの整合性をあわせます。</li>
<li>ユーザの詳細情報が第三者によって「ハイジャック」されることを抑止します。</li>
</ul>
<h4>お客様にとっての利点は？</h4>
<p>お客様が新しい Autodesk Account にサインアップすると、次の手順が必要になります。:</p>
<ol>
<li>メールアドレスを確認するように求められます｡</li>
<li>サインアップ時ﾆ使用したメール アドレスにメールが送信され、プロファイルの作成を完了することができます。</li>
<li>14日以内に、確認メールにアクセスし、リンクに従ってアカウントを確認することが出来ます*。</li>
</ol>
<p>* 14日後にまだ電子メールが検証されていない場合は、検証電子メールを再送信してプロセスを再開する必要があります。</p>
<p>アカウントは未確認の場合でも、ユーザは、認証サービス エendpoint や Fusion 360 などのクラウド サービスにアクセスするために、メールを確認するよう求められます。 メール アドレスの検証はアクセスの要件の１つです。</p>
<p>既存の Autodesk Account を持つユーザは、Forge ベースのアプリケーションへのログイン時にご自身のメールアドレスを確認するように求められます（3-legged の場合）。 新規ユーザの場合と同様に、アカウントを確認するためのリンクが記載されたメールが送信されます。 リンクをクリックすると、アカウントが自動的に更新され、[続行]/[Continue] を選択するとアプリケーションにアクセスできます。</p>
<p>電子メール検証プロセスの詳細については、Autodesk Knowledge Network の<strong><a href="https://knowledge.autodesk.com/ja/customer-service/account-management/account-profile/account-security/account-verification" rel="noopener" target="_blank">記事</a></strong>をご参照ください。</p>
</div>
</div>
<div class="node__sidebar adskf__section-aside">
<div class="view view-blog-featured">
<div class="views-element-container">
<div class="js-view-dom-id-c646f942466c1fb54098565145325bb64105ac92de28348d2c340bb6371a7e62 view view--blog view-id--featured">
<div class="views-row">
<div class="blog view_mode__list">By Toshiaki Isezaki</div>
</div>
</div>
</div>
</div>
</div>
