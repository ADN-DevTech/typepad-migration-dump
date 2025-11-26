---
layout: "post"
title: "EBA ユーザーへの APS 割り当て"
date: "2024-10-23 01:00:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/10/to-assign-eba-users-to-aps.html "
typepad_basename: "to-assign-eba-users-to-aps"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bf0abe200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d58156200b-pi" style="display: inline;"><img alt="Aps2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d58156200b image-full img-responsive" src="/assets/image_684419.jpg" title="Aps2" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bf0abe200c-pi" style="display: inline;"><br /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>ご存じのとおり、Autodesk&#0160; Platform Services（APS） は、従量課金制を持つ強力なプラットフォームです。通常、課金には Autodesk Flex が利用されますが、Enterprise Business Agreement（EBA）契約下で APS を使用する場合には留意すべきいくつかの重要なポイントがあります。</p>
<p><strong>EBA トークン（フレックス トークン）：</strong>オートデスクとの包括契約である Enterprise Business Agreement をお持ちの場合、EBA 契約に APS エンタイトルメントの追加を、カスタマー サクセス マネージャーにご相談ください。EBA 契約のフレックス トークンから、APS API 使用分のトークンが差し引かれるよう設定することが出来ます。</p>
<p><strong>APS エンタイトルメントの割り当て：</strong>適切なトークン消費と追跡を確保するために、プライマリ管理者、またはセカンダリ管理者の方が、APS エンタイトルメントを特定の開発者に割り当てる必要があります。</p>
<ol>
<li>プライマリ管理者、またはセカンダリ管理者のアカウントで&#0160;<a  _istranslated="1" href="https://manage.autodesk.com/">manage.autodesk.com</a> から&#0160;<strong>Autodesk Account </strong>にサインインします。</li>
<li>
<p><strong>[ユーザ管理] </strong>に移動し、<strong>[ユーザ別]</strong> を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eca667200d-pi" style="display: inline;"><img alt="1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860eca667200d img-responsive" src="/assets/image_738682.jpg" title="1" /></a></p>
</li>
<li>
<p>APS に割り当てる<strong>ユーザを選択または追加</strong>します。ユーザがまだユーザ リストにない場合は、<strong>[ユーザを招待]</strong> をクリックし、詳細を入力して招待を送信します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d580ea200b-pi" style="display: inline;"><img alt="2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d580ea200b image-full img-responsive" src="/assets/image_678888.jpg" title="2" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bf0ac8200c-pi" style="display: inline;"><img alt="3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bf0ac8200c img-responsive" src="/assets/image_970127.jpg" title="3" /></a></p>
</li>
</ol>
<ol start="4">
<li>
<p>選択したユーザに <strong>APS 権限を割り当てます</strong>。APS にアクセスできるようになったというメール通知が届きます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d580f2200b-pi" style="display: inline;"><img alt="4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d580f2200b image-full img-responsive" src="/assets/image_918206.jpg" title="4" /></a><br /><br /></p>
</li>
</ol>
<p>このプロセスにより、EBA に基づく APS アプリの適切なトークン消費と追跡が保証されます。</p>
<p>EBA ユーザを APS に割り当てる方法に関する質問やヘルプについては、<a  _istranslated="1" href="mailto:fpd.admins@autodesk.com">fpd.admins@autodesk.com</a> にお問い合わせください。</p>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/assign-eba-enterprise-business-agreement-users-autodesk-platform-services" rel="noopener" target="_blank">To Assign EBA (Enterprise Business Agreement) users to Autodesk Platform Services | Autodesk Platform Services</a>&#0160;から転写・意訳、補足を加えたものです。</p>
<p>By Toshiaki Isezaki</p>
