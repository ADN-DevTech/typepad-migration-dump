---
layout: "post"
title: "【重要】Forge 課金について"
date: "2019-05-27 00:03:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/important-about-forge-charge-of-cost.html "
typepad_basename: "important-about-forge-charge-of-cost"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">下記内容は古くなっています。最新情報は、<a href="https://adndevblog.typepad.com/technology_perspective/2020/02/important-about-forge-charge-of-cost-revised.html" rel="noopener" target="_blank"><strong>【重要】Forge サブスクリプション</strong></a>をご覧ください。（2021年5月27日）</span></p>
<p>4 月末以降、Forge をお使いでトライアルが終了している方に、「Purchase of Autodesk Forge Cloud Credits」のタイトルを持つ次のようなメールが届いているかと思います。ここでいう「トライアルの終了」とは、<strong><a href="https://forge.autodesk.com/" rel="noopener" target="_blank">Forge ポータル</a></strong>で初めて Developer Key（Client ID と Client Secret）を取得してから 1 年が経過したか、同日に付与された 100 クラウド クレジットを使い切ってしまった状態を指しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45f96bb200c-pi" style="display: inline;"><img alt="Notification_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45f96bb200c image-full img-responsive" src="/assets/image_129322.jpg" title="Notification_email" /></a></p>
<p>今後、トライアルの終了が間近の方（残り期間 2 か月、1 か月、ないし、クラウド クレジット残数 50、25、10）になったタイミングでも、同様の通知メールが送られる予定です。</p>
<p>なお、お使いの Autodesk ID がトライアルになっているか否か、また、現在所有しているクラウド クレジットがどれくらいあるかは、<strong><a href="https://forge.autodesk.com/" rel="noopener" target="_blank">Forge ポータル</a> </strong>にサインインして Usage Analytics を選択すると確認することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45f8c73200c-pi" style="display: inline;"><img alt="Usage_analytics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45f8c73200c image-full img-responsive" src="/assets/image_131801.jpg" title="Usage_analytics" /></a></p>
<p>また、トライアル期間の残日数とクラウド クレジットの消費量は、Auodesk Accounts（<strong><a href="https://accounts.autodesk.com/" rel="noopener" target="_blank">https://accounts.autodesk.com/</a></strong>）でも、<strong>体験版</strong> を選択することで確認することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ad698e200b-pi" style="display: inline;"><img alt="Autodesk_accounts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ad698e200b image-full img-responsive" src="/assets/image_192964.jpg" title="Autodesk_accounts" /></a></p>
<p>日本での Forge への課金は昨年から開始されていますが、実質、自己申告のかたちになっていました。今回、オートデスク本社の決定を受けて、クラウド クレジットの消費に応じた通知メール配信が始まった状況です。当面は、クラウドクレジットの購入を促すことが目的となります。</p>
<p>ただ、<span style="text-decoration: underline;">時期は未定</span>なものの、将来、トライアル終了後にクラウド クレジットの購入をされなかった Autodesk ID に対して、API 呼び出しを遮断する処理の実施も予想されます。</p>
<p>このため、前述の通知メールを受信した Autodesk ID をお持ちの方には、クラウド クレジット購入のご検討をお勧めする次第です。現時点での日本でのクラウドクレジットの購入の選択肢は、下記のブログ記事の内容に準じることになります。</p>
<div>
<p dir="auto" style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/01/about-purchasing-cloud-credt.html" rel="noopener" target="_blank"><strong>クラウド クレジットの購入について</strong></a></p>
</div>
<p>通知メールの文面にもありますが、欧米では&#0160; eStore から Forge クラウド クレジット購入が可能になっていますが、残念ながら、日本を含む、アジア各国では利用不可となっています。この点は、次のブログ記事でご案内したとおりです。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2019/01/purchasing-cloud-credit-on-store.html" rel="noopener" target="_blank"><strong>eStore での Forge クラウド クレジット購入</strong></a></p>
<p>このため、日本から Forge クラウド クレジットをご購入いただく場合には、「<strong>オートデスク Forge チームへのコンタクトで購入</strong>」か「<strong>オートデスク ダイレクト セールスからの購入</strong>」のいずれかをお使いいただく必要があります。後者の場合、オートデスクの担当営業を把握されている一部企業に限定されてしまうため、ここでは「<strong>オートデスク Forge チームへのコンタクトで購入</strong>」いついてご紹介しておきたいと思います。</p>
<div>
<ul>
<li><span style="background-color: #ffff00;">ご注意： クラウド クレジットを Forge で利用するためには、Autodesk ID への Forge アカウント（別名 <strong>Forge コントラクト</strong>）の作成と関連付けの処理が必要になるため、<span style="text-decoration: underline;">初回</span>購入時は、オートデスク側で内部的な処理が必要です（トライアル時には Forge アカウントが作成されていない状態）。</span><strong><br /></strong></li>
</ul>
<p><span style="background-color: #ffffff;">初めてのクラウド クレジットを購入される場合で、</span><strong>オートデスク Forge チームへのコンタクトで購入</strong>される場合には、<a href="https://forge.autodesk.com/pricing" rel="noreferrer"><strong>https://forge.autodesk.com/pricing</strong></a> のオンライン フォームを使って、米国の Forge セールス チームにコンタクトをお願いします。オンラインフォームへは、ページ中段の「<strong>First time purchasers</strong>（初回購入者）」下の <span style="color: #ffffff; background-color: #ee8822;">&#0160;[CONTACT TEAM]&#0160;</span> からアクセスすることが出来ます。</p>
</div>
<p><span style="color: #ffffff;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45f9830200c-pi" style="display: inline; color: #ffffff;"><img alt="Contact_team" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45f9830200c image-full img-responsive" src="/assets/image_174434.jpg" title="Contact_team" /></a></span></p>
<p>[Purchase Cloud Credits] とタイトルされたフォームが表示されたら、次の各項目に記入後、フォーム下の&#0160;<span style="background-color: #ee8822; color: #ffffff;"> [SUBMIT]&#0160;</span>&#0160;ボタンでお申込みください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a45f9a86200c-pi" style="display: inline;"><img alt="Onlie_form" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a45f9a86200c image-full img-responsive" src="/assets/image_147196.jpg" title="Onlie_form" /></a></p>
<p>その後、上記データに基づいた見積書の発行（オートデスク）→ 見積書への同意署名（購入者）→ 署名済の見積書のスキャン データのオートデスクへに提出（購入者）→ 最後に注文書（PO、Purchase Order）をオートデスクへ発行（購入者）、の手順を経て、クラウド クレジットをご購入いただくこととなります（Autodesk ID への Forge アカウントが作成され、購入した Forge クラウド クレジットが紐づけられる）。<br /><br />なお、Forge を利用したアプリ/サービスを受託開発される場合には、運用時の課金も意識していただく必要があります。次のブログ記事をご一読ください。</p>
<div>
<p dir="auto" style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/06/development-business-using-forge-and-charging-const.html" rel="noopener" target="_blank"><strong>Forge を使った開発ビジネスと課金について</strong></a></p>
<p dir="auto">また、Forge Platform API のどの API に、どのようにクラウド クレジット消費が実施されるかについては、次のブログ記事をご確認ください。</p>
<p dir="auto" style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener" target="_blank"><strong>Forge 課金について</strong></a></p>
</div>
<p>初回のクラウド クレジットご購入後、Forge アクセスは一般のサブスクリプション契約のように、１年毎の更新のような仕組みをとることになります。更新時には 100 クラウド クレジットの購入で更新手続きが処理されます。<span style="text-decoration: line-through;">この場合、通常時の $100（￥16,000 - 税抜き）ではなく、米国ドル ベースで 5% 増しの $105（￥17,000 - 税抜き）でのご購入（更新含む）が必要です。</span>（2019年10月の改定で更新時も、$100 -￥16,000 - 税抜き となりました）。サブスクリプション期間中に追加のクラウド クレジットをご購入いただく際には、$100（￥16,000 - 税抜き）の価格となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e3a8e5200d-pi" style="display: inline;"><img alt="Forge_subs_lifecycle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e3a8e5200d image-full img-responsive" src="/assets/image_681823.jpg" title="Forge_subs_lifecycle" /></a></p>
<p><strong><a href="https://forge.autodesk.com/pricing" rel="noopener" target="_blank">https://forge.autodesk.com/pricing</a></strong> の次の箇所に追記された記述は、上記を意図したものです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46065d1200c-pi" style="display: inline;"><img alt="Renewal_price" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46065d1200c image-full img-responsive" src="/assets/image_4775.jpg" title="Renewal_price" /></a></p>
<p>By Toshiaki Isezaki</p>
