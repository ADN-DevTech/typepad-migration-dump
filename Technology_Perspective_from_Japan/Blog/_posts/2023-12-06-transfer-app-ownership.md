---
layout: "post"
title: "アプリ所有者の移行"
date: "2023-12-06 00:01:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/12/transfer-app-ownership.html "
typepad_basename: "transfer-app-ownership"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39fa866200c-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39fa866200c image-full img-responsive" src="/assets/image_363313.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></p>
<p><span style="background-color: #ffff00;">2025年2～3月の&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2025/02/aps-developer-hubs.html" rel="noopener" style="background-color: #ffff00;" target="_blank">Developer Hub</a>&#0160;と<a href="https://adndevblog.typepad.com/technology_perspective/2025/03/aps-introduces-team-assignment.html" rel="noopener" style="background-color: #ffff00;" target="_blank">チーム割り当て</a>の導入にともない、本記事の方法に代わって <a href="https://adndevblog.typepad.com/technology_perspective/2025/03/transfer-aps-app-ownership.html" rel="noopener" style="background-color: #ffff00;" target="_blank"><strong>APS アプリ所有者の移行</strong></a>&#0160;にご紹介する手順が必要になります。</span></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/flex_token_and_billing_eligibility.html" rel="noopener" target="_blank"><span style="text-decoration: line-through;">Flex トークンと課金対象者</span></a><span style="text-decoration: line-through;"> の記事でご紹介したとおり、APS（旧 Forge）の課金対象者となる方は、デベロッパーキー（Client ID と Client Secret）を取得したアプリの所有者です。</span></p>
<p><span style="text-decoration: line-through;">APS アプリの所有者が退職で Flex トークンの補填が出来なくなってしまい、トークン残高がマイナス（ゼロ未満）になってしまうと、14 日間の猶予期間を経て、同所有者のアプリの API アクセスが遮断されてしまいます。</span></p>
<p><span style="text-decoration: line-through;">もし、退職してしまった所有者アカウントにアクセス出来る場合には、猶予期間中に Flex トークンを追加購入してトークン残高をゼロ以上に出来れば、API アクセスを継続することが出来ます。猶予期間中のトークン購入については、<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/purchasing_tokens_while_grace_period.html" rel="noopener" target="_blank">猶予期間中のトークン購入</a>&#0160;のブログ記事を参照にしてみてください。</span></p>
<p><span style="text-decoration: line-through;">多くの場合、アプリ所有者が退職する情報は事前に周知されるはずです。また、場合によっては、アプリ所有者の部署移動で他のアカウントに所有権（課金先）を移行したいこともあるかと思います。そのような場面では、<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/specifying-collaborators-feature-when-creating-app.html" rel="noopener" target="_blank">アプリ作成時のコラボレーター指定</a> でご紹介した方法にいくつかの手順を加えることで、アプリ所有者（デベロッパーキー）を他のアカウントに移行することが出来ます、言い換えれば、課金対象者を他のアカウントに変更することが出来ます。</span></p>
<hr />
<p><span style="text-decoration: line-through;"><strong>ご注意</strong></span></p>
<ul>
<li><span style="text-decoration: line-through;">アプリ所有権の移行が可能なのは、移行元アカウントと移行先アカウントの Flex トークン残数がゼロ以上の Full Access の状態の場合のみです。</span></li>
<li><span style="text-decoration: line-through;">無償 Trial（トライアル）期間のアカウントとはアプリ所有権の移行は出来ません。</span></li>
<li><span style="text-decoration: line-through;">アプリ所有権を移行されたアカウントでは、新たに移行対象となったアプリの API 使用が課金されるようになります。移行元の旧アプリ所有者のすべてのアプリ所有権のが移行されるわけではないので、旧アプリ所有者が所有するすべてのアプリの API 使用が課金されることはありません。</span></li>
</ul>
<hr />
<p><span style="text-decoration: line-through;"><strong>手順</strong></span></p>
<p><span style="text-decoration: line-through;">ここでは、「Mamoru Miyakojima」アカウントから「Toshiaki Isezaki」アカウントに、「App」アプリの所有権を移行する例を見てみます。</span></p>
<ol>
<li><span style="text-decoration: line-through;">アプリ所有者（デベロッパーキー）を他のアカウントに移行するには、まず、アプリ所有者のアカウントで APS ポータルにサインイン後、<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/specifying-collaborators-feature-when-creating-app.html" rel="noopener" target="_blank">アプリ作成時のコラボレーター指定</a> の手順に沿って、アプリ所有権を移行したいアカウントを、Viewer 権限、あるいは Editor 権限でコラボレーター指定してください。</span></li>
<li><span style="text-decoration: line-through;">コラボレーター指定が完了すると、アプリ ページのコラボレーター指定を受けたアカウントに、新しく <strong>Owner</strong> 権限の選択肢が表示されるようになります。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a409cc200d-pi" style="display: inline;"><img alt="Origin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a409cc200d image-full img-responsive" src="/assets/image_412049.jpg" title="Origin" /></a></span></li>
<li><span style="text-decoration: line-through;">Owner 権限の選択肢を選択すると、Owner 権限委譲の招待をするか確認する画面が表示されます。<span style="background-color: #d85529; color: #ffffff; text-decoration: line-through;">[Send Invite]</span> ボタンをクリックします。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3aa07200b-pi" style="display: inline;"><img alt="Send_invitation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3aa07200b image-full img-responsive" src="/assets/image_740504.jpg" title="Send_invitation" /></a></span></li>
<li><span style="text-decoration: line-through;">移譲先に指定されたコラボレーターには、次の招待メールが着信します。コラボレーターは委譲を受け入れるため <span style="background-color: #111111; color: #ffffff; text-decoration: line-through;">[Accept invite]</span> をクリックする必要があります。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3a972200b-pi" style="display: inline;"><img alt="Invitation_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3a972200b image-full img-responsive" src="/assets/image_362297.jpg" title="Invitation_email" /></a></span></li>
<li><span style="text-decoration: line-through;">招待メールに承諾すると、アプリ委譲先アカウントの方は、同アカウントで APS ポータルにサインインを求められます。サインイン後に Owner 権限の委譲を確認する画面が表示されるので、移譲を完了するために <span style="background-color: #d85529; color: #ffffff; text-decoration: line-through;">[Confirm transfer]</span> ボタンをクリックしてください。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a40a90200d-pi" style="display: inline;"><img alt="Confirm_transfer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a40a90200d image-full img-responsive" src="/assets/image_657590.jpg" title="Confirm_transfer" /></a></span></li>
<li><span style="text-decoration: line-through;">移行が完了すると、コラボレーターのアカウントと元アプリ所有者の権限表記が変化しているはずです。移譲先のアカウントが Owner となり、元所有者は Editor となります。</span><br /><span style="text-decoration: line-through;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39fa8f4200c-pi" style="display: inline;"><img alt="Transfered" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39fa8f4200c image-full img-responsive" src="/assets/image_921469.jpg" title="Transfered" /></a></span></li>
</ol>
<hr />
<p>By Toshiaki Isezaki</p>
