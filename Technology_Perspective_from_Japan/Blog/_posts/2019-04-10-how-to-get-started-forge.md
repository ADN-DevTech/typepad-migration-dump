---
layout: "post"
title: "Forge の始め方"
date: "2019-04-10 00:16:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/how-to-get-started-forge.html "
typepad_basename: "how-to-get-started-forge"
typepad_status: "Publish"
---

<p>最近、「Forge を利用するためには、どのような契約が必要か？」、「Forge を評価してみたいが、どうすれば良いのか？」、「クラウド クレジットを事前の購入する必要があるのか？」といったお問合せが増えています。Forge を A360 や BIM 360 のようなクラウド サービスと誤解されているケースも散見されますので、今日は、そういった「はじめの一歩」に関する諸々の疑問にお答えしていきたいと思います。</p>
<p><strong><span style="font-size: 18pt;">Q.</span> Forge を購入したいのですが、販売店を紹介してください。</strong></p>
<p style="padding-left: 40px;">Forge はエンドユーザ向けの SaaS（Software As A Service ＝ クラウド サービス）ではなく、デベロッパ（開発者）向けの Web API（Application Programming Interface）のブランド名です。利用にはプログラムの作成が必須です。その性格上、オートデスク認定販売店での販売はしていません。Web 開発の環境・知識をお持ちであれば、Forge ポータルからデベロッパキーを取得するだけで、トライアル期間（最大90日間）、無償でお使いいただくことが出来ます。後述する説明もご確認ください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a494cc1e200b-pi" style="display: inline;"><img alt="Gui_vs_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a494cc1e200b image-full img-responsive" src="/assets/image_671436.jpg" title="Gui_vs_api" /></a></p>
<p><strong><span style="font-size: 18pt;">Q.</span> Forge を使ってみたいのですが、どのような契約が必要か教えてください。</strong></p>
<p style="padding-left: 40px;">Forge の利用に際して、事前に契約や申請、クラウド クレジットの購入、あるいは、クレジットカードや決済アカウントの登録等をおこなっていただく必要はありません。強いて言えば、<strong><a href="https://forge.autodesk.com/" rel="noopener" target="_blank">Forge ポータル</a></strong>でデベロッパキーを取得する際に<strong> Autodesk ID</strong>（別名：<strong><a href="https://knowledge.autodesk.com/ja/customer-service/account-management/account-profile/get-started" rel="noopener" target="_blank">オートデスク アカウント</a></strong>）が必要になりますので、Auodesk Accounts（<strong><a href="https://accounts.autodesk.com/" rel="noopener" target="_blank">https://accounts.autodesk.com/</a></strong>）から Autodesk ID を作成をお願いします（無償）。既に Autodesk ID をお持ちの場合は、そちらをお使いいただけます。</p>
<p><strong><span style="font-size: 18pt;">Q.</span> Forge を評価してみたいのですが、どうすれば良いでしょうか？</strong></p>
<p style="padding-left: 40px;">Forge には <strong><a href="https://forge.autodesk.com/developer/documentation" rel="noopener" target="_blank">Forge Platform API</a></strong> として複数の API が用意されています。Viewer はクライアントの Web ブラウザ内で動作する JavaScript ライブラリですが、それ以外の API はクラウドとコミュニケーションするための <strong><a href="https://ja.wikipedia.org/wiki/Representational_State_Transfer" rel="noopener" target="_blank">RESTful API</a> </strong>です。Forge を評価いただくには、この RESTful API を利用するデベロッパキー（Client ID と Client Secret のペア）を取得する必要があります（無償）。デベロッパーキーの取得手順は、別のブログ記事 <a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank"><strong>Forge API を利用するアプリの登録とキーの取得</strong></a> でご紹介していますのでご参照ください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4470ac0200c-pi" style="display: inline;"><img alt="Retrieving_developer_keys" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4470ac0200c image-full img-responsive" src="/assets/image_6414.jpg" title="Retrieving_developer_keys" /></a></p>
<p style="padding-left: 40px;">デベロッパキーは、プログラム内で Access Token（アクセス トークン）を得るために使用されます。デベロッパキーと Access Token の役割は、別のブログ記事&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html" rel="noopener" target="_blank"> Access Token について</a></strong> ご案内しています。</p>
<p><a name="_free_trial"></a></p>
<p><strong><span style="font-size: 18pt;">Q.</span> Forge トライアルを行使して無償で評価するには、どうすれば良いでしょうか？</strong></p>
<p style="padding-left: 40px;"><strong><a href="https://forge.autodesk.com/" rel="noopener" target="_blank">Forge ポータル</a></strong>に初めてサインインした Autodesk ID に<span style="text-decoration: underline;">自動的に</span>トライアルとして<span style="background-color: #ffff00;"><strong>90 日間</strong></span>の有効期限が適用された 100 クラウド クレジットが提供されます。このため、トライアルの適用になんらかの作業や操作は不要です。</p>
<p style="padding-left: 40px;">Forge は 、API の利用に応じてクラウド クレジットを使った従量課金制をとっています。API 利用に応じたクラウド クレジットの消費量は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener" target="_blank">Forge 課金について</a></strong> でご説明しています。</p>
<p style="padding-left: 40px;">トライアルは、最初に提供された 100 クラウド クレジットを使い切ってしまうか、クラウド クレジットに残数があっても、提供から &#0160;<span style="background-color: #ffff00;"><strong>90 日</strong></span>経過した時点で自動的に終了します。</p>
<p style="padding-left: 80px;"><span style="background-color: #ffff00;"><strong>※ご注意：</strong>2020年5月28日（米国太平洋時間）から Forge トライアルの期間が１年間から90日間に変更されています。</span></p>
<p style="padding-left: 40px;">トライアル期間がいつまでなのか、クラウド クレジットをどの程度消費しているかは、Forge ポータルにサインインして Usage Analytics を選択すると確認できます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a494d36a200b-pi" style="display: inline;"><img alt="Cloud_credit_usage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a494d36a200b image-full img-responsive" src="/assets/image_512748.jpg" title="Cloud_credit_usage" /></a></p>
<p style="padding-left: 40px;">トライアル期間の残日数とクラウド クレジットの消費量は、Auodesk Accounts（<strong><a href="https://accounts.autodesk.com/" rel="noopener" target="_blank">https://accounts.autodesk.com/</a></strong>）でも <strong>体験版</strong> を選択して確認することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4471271200c-pi" style="display: inline;"><img alt="Cloud_credit_usage_on_autodesk_accounts" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4471271200c image-full img-responsive" src="/assets/image_10116.jpg" title="Cloud_credit_usage_on_autodesk_accounts" /></a></p>
<p style="padding-left: 40px;"><span style="text-decoration: underline;">現在</span>、トライアル期間が終了しても、API 呼び出しを拒否したりするようなことはしていませんのでご安心ください。可能であれば、クラウド クレジットの購入を <strong><a href="https://forge.autodesk.com/pricing" rel="noopener" target="_blank">ご検討</a> </strong>ください。</p>
<p>By Toshiaki Isezaki</p>
