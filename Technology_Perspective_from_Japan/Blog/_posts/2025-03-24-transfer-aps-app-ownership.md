---
layout: "post"
title: "APS アプリ所有者の移行"
date: "2025-03-24 00:32:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/transfer-aps-app-ownership.html "
typepad_basename: "transfer-aps-app-ownership"
typepad_status: "Publish"
---

<div><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc8662200d-pi"><img alt="Aps" border="0" src="/assets/image_659713.jpg" title="Aps" /></a></div>
<div>&#0160;</div>
<div>退職等で APS アプリの所有者が Flex トークンの補填が出来なくなってしまい、トークン残高がマイナス（ゼロ未満）になってしまうと、14 日間の猶予期間を経て、同所有者のアプリの API アクセスが遮断されてしまいます。</div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>多くの場合、アプリ所有者が退職する情報は事前に周知されるはずです。また、場合によっては、アプリ所有者の部署移動で他のアカウントに所有権（課金先）を委譲したいこともあるかと思います。そのような場面では、アプリ所有者（デベロッパーキー）を他のアカウントに移行することが出来ます。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2025/02/aps-developer-hubs.html" rel="noopener" target="_blank">Developer Hub</a> と<a href="https://adndevblog.typepad.com/technology_perspective/2025/03/aps-introduces-team-assignment.html" rel="noopener" target="_blank">チーム割り当て</a>の導入にともない、過去に&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2023/12/transfer-app-ownership.html" rel="noopener" target="_blank">アプリ所有者の移行</a> でご案内した方法に代わって、下記にご紹介する手順が必要になります。</li>
</ul>
<hr />
<p><strong>ご注意</strong></p>
<ul>
<li>アプリ所有権の移譲が可能なのは、委譲元アカウントと委譲先アカウントの Flex トークン残数がゼロ以上の Full Access の状態の場合のみです。</li>
<li>無償 Trial（トライアル）期間のアカウントとはアプリ所有権の委譲は出来ません。</li>
</ul>
<hr />
<p><strong>手順</strong></p>
<p>ここでは、「Mamoru Miyakojima」アカウントから「Toshiaki Isezaki」アカウントに、「APS チュートリアル：Hub Browser」アプリの所有権を委譲する例を見てみます。</p>
<ol>
<li>アプリ所有者（デベロッパーキー）を他のアカウントに委譲するには、まず、アプリ所有者のアカウントで APS ポータルにサインイン後、My applications<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/specifying-collaborators-feature-when-creating-app.html" rel="noopener" target="_blank"></a> ページを表示します。</li>
<li>アプリ一覧から自身が所有していてアプリ所有権を移行したいアプリを見つけて、右端の <strong>&#0160;⋮&#0160; </strong>記号をクリック、続いて「Transfer Ownership」をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced914200c-pi" style="display: inline;"><img alt="Start_transfer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ced914200c image-full img-responsive" src="/assets/image_756025.jpg" title="Start_transfer" /></a></li>
<li>アプリ所有（Owner 権限）の委譲先となる方のメールアドレスを入力して招待メールを送信します。まず、入力が完了したら <span style="background-color: #111111; color: #ffffff;">[Next]</span> をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58857200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced7b2200c-pi" style="display: inline;"><img alt="Send_invitation" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ced7b2200c img-responsive" src="/assets/image_891836.jpg" title="Send_invitation" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58857200b-pi" style="display: inline;"><br /></a></li>
<li>確認画面が表示されるので、招待メールのメールアドレスを確認して <span style="background-color: #111111; color: #ffffff;">[Confirm]</span> をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc883c200d-pi" style="display: inline;"><img alt="Confirm_invitation" class="asset  asset-image at-xid-6a0167607c2431970b02e860fc883c200d img-responsive" src="/assets/image_508070.jpg" style="width: 550px;" title="Confirm_invitation" /></a></li>
<li>招待メールが正常に送信されると、ページ右上に「Your app transfer invite was sent.」メッセージが表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58885200b-pi" style="display: inline;"><img alt="Succeeded_send" class="asset  asset-image at-xid-6a0167607c2431970b02e860e58885200b img-responsive" src="/assets/image_805844.jpg" style="width: 360px;" title="Succeeded_send" /></a><br />もし、招待メール送信の送信に失敗した場合には、次のように表示されます。サインイン状態が有効期限切れになっている可能性がありますので、<span style="background-color: #111111; color: #ffffff;">[Got it]</span> をクリック後、再度、サインインしてお試しください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e5888b200b-pi" style="display: inline;"><img alt="Failed_send" class="asset  asset-image at-xid-6a0167607c2431970b02e860e5888b200b img-responsive" src="/assets/image_836171.jpg" title="Failed_send" /></a></li>
<li>移譲先に指定されたアカウントには招待メールが着信します。委譲を受け入れるため <span style="background-color: #111111; color: #ffffff;">[Accept invite]</span> をクリックしてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced821200c-pi" style="display: inline;"><img alt="Invitation_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ced821200c image-full img-responsive" src="/assets/image_498872.jpg" title="Invitation_email" /></a></li>
<li>委譲先のアカウントが Developer&#0160; Hub に登録されている場合には、移譲先を 「Toshiaki Isezaki」アカウント（Personal）にするか、Hub にするかの選択を求められます。適切な場所を指定してください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc889a200d-pi" style="display: inline;"><img alt="Destination" class="asset  asset-image at-xid-6a0167607c2431970b02e860fc889a200d img-responsive" src="/assets/image_203251.jpg" style="width: 600px;" title="Destination" /></a></li>
<li>続けて、チームを割り当てます。移譲先アカウントが<span style="text-decoration: underline;">複数のチームに参加している場合</span>には、課金先となるチームを選択します。選択後、<span style="background-color: #111111; color: #ffffff;">[Next]</span> をクリックしてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced8a0200c-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58934200b-pi" style="display: inline;"><img alt="Team" class="asset  asset-image at-xid-6a0167607c2431970b02e860e58934200b img-responsive" src="/assets/image_636611.jpg" style="width: 600px;" title="Team" /></a></li>
<li>確認画面が表示されるので、内容を確認して問題がなければ <span style="background-color: #111111; color: #ffffff;">[Confirm]</span> をクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc8912200d-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58947200b-pi" style="display: inline;"><img alt="Confirm_final" class="asset  asset-image at-xid-6a0167607c2431970b02e860e58947200b img-responsive" src="/assets/image_897993.jpg" style="width: 600px;" title="Confirm_final" /></a></li>
<li>アプリ所有者の委譲が成功すると、9. の後で「You&#39;re now the owner of APS チュートリアル：Hub Browser」と表示されます。<br />&#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced94d200c-pi" style="display: inline;"><img alt="Succeeded_transfer" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ced94d200c img-responsive" src="/assets/image_204623.jpg" title="Succeeded_transfer" /></a><br />1. ～ 8. のアプリ所有者の作業と 6.～ 9. の移譲先アカウントの作業を同じブラウザで実行すると、エラーが表示されてアプリの委譲が失敗してしまいますのでご注意ください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e5895f200b-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860fc8929200d-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ced8dc200c-pi" style="display: inline;"><img alt="Error" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ced8dc200c img-responsive" src="/assets/image_342803.jpg" style="width: 600px;" title="Error" /></a></li>
<li>アプリ所有者の委譲が完了すると、元の所有者にになる「Mamoru Miyakojima」アカウントの My applications<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/specifying-collaborators-feature-when-creating-app.html" rel="noopener" target="_blank"></a> ページの Owner が変化していることを確認することが出来ます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e58985200b-pi" style="display: inline;"><img alt="After_transfer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e58985200b image-full img-responsive" src="/assets/image_251211.jpg" title="After_transfer" /></a></li>
</ol>
</div>
<p>By Toshiaki Isezaki</p>
