---
layout: "post"
title: "Autodesk Flex のユーザー割り当て"
date: "2024-02-19 00:17:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/02/assigning-autodesk-flex-to-user.html "
typepad_basename: "assigning-autodesk-flex-to-user"
typepad_status: "Publish"
---

<p>Autodesk Platform Servicees（旧 Forge）は、<a href="https://www.autodesk.co.jp/flex" rel="noopener" target="_blank">Autodesk Flex</a> を用いた<a href="https://aps.autodesk.com/pricing" rel="noopener" target="_blank">従量課金制</a>を採用しています。特定の API を使用すると、API によって定められたトークン数を消費する仕組みです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a9d91c200b-pi" style="display: inline;"><img alt="Consumed_tokens" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a9d91c200b image-full img-responsive" src="/assets/image_890602.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Consumed_tokens" /></a><span style="font-size: 8pt;">&lt;2024年2月1日現在&gt;</span></p>
<p>Autodesk Flex は、Flex 特約を持つ一部の<a href="https://www.autodesk.com/support/partners?locations=Japan&amp;languages=Japanese&amp;services=Flex" rel="noopener" target="_blank">認定リセラー</a>、あるいは、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/autodesk-flex-now-available-in-japan-from-estore.html" rel="noopener" target="_blank">eStore</a> からご購入いただくことが出来ます。もし、Autodesk Flex を購入した管理者アカウントと、実際にトークンを消費することになるアカウントが異なる場合には、開発者アカウントが正しくトークン消費出来るよう、ユーザ割り当てをおこなう必要があります。</p>
<p>ユーザ割り当ての方法は、他のオートデスク製品のユーザ割り当てと同様に、<a href="https://www.autodesk.co.jp/support/account/admin/plans/flex-access" rel="noopener" target="_blank">オートデスク管理者 | サブスクリプション プラン | Autodesk Flex</a> でご紹介している方法で実施することが出来ます。</p>
<p>おおまかな手順は次のとおりです。（2024年2月1日現在）</p>
<ol>
<li>Flex トークンを購入した管理者アカウントで <a href="https://manage.autodesk.com/" rel="noopener" target="_blank">https://manage.autodesk.com</a> にサインインして、「ユーザ別ユーザ管理」ページで Flex トークンを割り当てるユーザ（トークンを消費する開発者のアカウント）を招待します（下図の例では「割り当てられた製品」が 0）。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a9d77b200b-pi" style="display: inline;"><img alt="Before_assigning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a9d77b200b image-full img-responsive" src="/assets/image_810556.jpg" title="Before_assigning" /></a></li>
<li>Flex トークンを割り当てるユーザの左側にある <strong>➜</strong> アイコンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5f983200c-pi" style="display: inline;"><img alt="Before_assigning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5f983200c image-full img-responsive" src="/assets/image_532884.jpg" title="Before_assigning" /></a></li>
<li>割り当てページが表示されたら、[割り当て] ボタン右の <strong>〉</strong>アイコンをクリックして、右手に Autodesk Flex の割り当て対象が表示されたら、[カスタマイズ] ボタンをクリックします。（[割り当て] ボタンをクリックしてしまうと、割り当て可能製品すべてが割り当てられてしまいます。）<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a9d7ca200b-pi" style="display: inline;"><img alt="Assign_flex" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a9d7ca200b image-full img-responsive" src="/assets/image_650850.jpg" title="Assign_flex" /></a></li>
<li>Autodesk Flex の割り当て対象の製品一覧から「Platform Services」を見つけてチェックボックスに <strong>☑</strong> して、[選択した製品を割り当てる] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5f76c200c-pi" style="display: inline;"><img alt="Assign_aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5f76c200c image-full img-responsive" src="/assets/image_778161.jpg" title="Assign_aps" /></a></li>
<li>「ユーザ別ユーザ管理」ページで、Flex トークンを割り当てたユーザの割り当て製品数を確認します（下図の例では「割り当てられた製品」が 1 に変化）。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aa3e10200d-pi" style="display: inline;"><img alt="After_assigning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aa3e10200d image-full img-responsive" src="/assets/image_328836.jpg" title="After_assigning" /></a></li>
</ol>
<p>Flex トークンを割り当てられたユーザが（作成したアプリで）課金対象 API を使用すると、それをトリガーになり、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html" rel="noopener" target="_blank">Usage Analytics</a> ページに消費トークン数が反映されます。</p>
<p>By Toshiaki Isezaki</p>
