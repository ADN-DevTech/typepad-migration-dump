---
layout: "post"
title: "Flex トークンと課金対象者"
date: "2023-03-01 00:26:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/flex_token_and_billing_eligibility.html "
typepad_basename: "flex_token_and_billing_eligibility"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">2025年3月の<a href="https://adndevblog.typepad.com/technology_perspective/2025/03/aps-introduces-team-assignment.html" rel="noopener" style="background-color: #ffff00;" target="_blank"><strong> APS にチームの割り当てを導入</strong></a>&#0160;にともない、下記はアプリに設定されたチームにおこなわれるようになりました。このため、チームへのトークン割り当てについては、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2025/03/assigning-tokens-to-team.html" rel="noopener" style="background-color: #ffff00;" target="_blank">チームへのトークン割り当て</a></strong>&#0160;の記事をご確認ください。</span></p>
<p><span style="text-decoration: line-through;">APS アプリの作成時、アプリが APS クラウド リソースにアクセスする認証を得るために、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a>&#0160;の手順で Client ID と Client Secret から成る「デベロッパーキー」を取得する必要があります。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75172e50b200b-pi" style="display: inline;"><img alt="Who_gets_charged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75172e50b200b image-full img-responsive" src="/assets/image_971496.jpg" title="Who_gets_charged" /></a></span></p>
<p><span style="text-decoration: line-through;">デベロッパーキーは、1 つのアカウントで複数個、取得することが出来ます。言い換えれば、アプリ毎に異なる Client ID と Client Secret の組み合わせを使用することが可能です。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75172e513200b-pi" style="display: inline;"><img alt="Keys_on_account" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75172e513200b image-full img-responsive" src="/assets/image_553530.jpg" title="Keys_on_account" /></a></span></p>
<p><span style="text-decoration: line-through;">アカウントが複数のデベロッパーキーを別々のアプリで使用すると、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html" rel="noopener" target="_blank">Usage Analytics ページ：Flex トークン残高と消費量の確認</a> でご案内した方法を使って、アプリ毎に消費した Flex トークン数を把握することが出来ます。</span></p>
<p><span style="text-decoration: line-through;">逆に、別々のアプリで 1 つのデベロッパー（ 1 組の Client ID と Client Secret のペア）を利用することも出来ます。ただし、この場合、消費トークンをアプリ毎に把握することは出来なくなります。</span></p>
<p><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751973741200c-pi" style="display: inline;"><img alt="Token_consumption" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751973741200c image-full img-responsive" src="/assets/image_68942.jpg" title="Token_consumption" /></a></span></p>
<p><span style="text-decoration: line-through;">いずれの場合でも、APS API の課金対象者となる方は、デベロッパーキーを取得したアカウント所有者になります。もし、アプリ毎に課金対象者を変更したい場合には、次の記事をご確認ください。この場合、事前に異なるアカウントからデベロッパーキーを取得して開発に利用する方法も可能です。</span></p>
<ul>
<li><span style="text-decoration: line-through;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/12/transfer-app-ownership.html" rel="noopener" target="_blank">アプリ所有者の移行&#0160;</a></span></li>
</ul>
<p><span style="text-decoration: line-through;">なお、既に APS を使用中の開発者の方が Flex トークンを購入した場合には、お使いになるアカウントにトークンが関連付けられます。もし、複数のユーザを管理されている契約管理者が Flex トークンを購入した場合、または、新規に APS 開発をする目的に Flex トークンを初めて購入した場合には、ご購入後に <a href="https://www.autodesk.co.jp/support/account/admin/plans/flex-access" rel="noopener" target="_blank">Autodesk Flex の設定</a> の内容、または次のリンクでご案内している手順に沿って、開発をされるユーザの方（アカウント）に Autodesk Platform Services を割り当てる必要があります。</span></p>
<ul>
<li><span style="text-decoration: line-through;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/02/assigning-autodesk-flex-to-user.html" rel="noopener" target="_blank">Autodesk Flex のユーザー割り当て</a></span></li>
<li><span style="text-decoration: line-through;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/10/to-assign-eba-users-to-aps.html" rel="noopener" target="_blank">EBA ユーザーへの APS 割り当て</a>（EBA 契約をお持ちで EBA トークン フレックスでの APS 利用をご希望の場合）</span></li>
</ul>
<p>By Toshiaki Isezaki</p>
