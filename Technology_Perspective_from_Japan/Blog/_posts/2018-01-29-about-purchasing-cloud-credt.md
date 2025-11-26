---
layout: "post"
title: "クラウド クレジットの購入について"
date: "2018-01-29 00:01:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/01/about-purchasing-cloud-credt.html "
typepad_basename: "about-purchasing-cloud-credt"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/forge-trial-and-future.html" rel="noopener noreferrer" target="_blank">Forge トライアルと今後</a></strong> でも触れた <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c94a411c970b-pi" style="float: right;"><img alt="Cloud_credit" class="asset  asset-image at-xid-6a0167607c2431970b01b7c94a411c970b img-responsive" src="/assets/image_129808.jpg" style="width: 100px; margin: 0px 0px 5px 5px;" title="Cloud_credit" /></a>とおり、今春を目途に、トライアル期間が終了したアカウントから、順次 Forge への課金が日本でも開始されていく予定です。課金といっても、具体的に消費されていくのは物理的な金銭ではなく、オートデスクがクラウド サービスに適用している <strong><a href="https://knowledge.autodesk.com/ja/customer-service/account-management/subscription/cloud-services/autodesk-360-cloud-credits-faq" rel="noopener noreferrer" target="_blank">クラウド クレジット</a></strong> と呼ばれる消費単位です。Forge プラットフォーム API によって消費するクラウド クレジットの数が異なりますが、その考え方は <strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> でご案内していますので、ご存じのない方は同ブログ記事をご確認ください。</p>
<p>さて、このクラウド クレジットですが、アカウント（Autodesk ID）に関連付けられているクラウド クレジットをすべて消費して無くなってしまたった場合には、別途、クラウド クレジットを購入していく必要があります。</p>
<p>現在はトライアル期間が有効になっているはずなので、クラウド クレジットを購入する必要はないはずですが、今回は、今現在、クラウド クレジットをどのように購入することが出来るのか、また、将来、どのような購入オプションが他に考えれれているのかをご紹介していきます。</p>
<p><strong>Autodesk Accounts ページからの購入</strong></p>
<p style="padding-left: 30px;">クラウド クレジットは、もともと、<strong><a href="https://www.autodesk.co.jp/products/rendering/overview" rel="noopener noreferrer" target="_blank">レンダリング サービス</a>&#0160;</strong>など、オートデスクのクラウド サービスの消費単位として登場した経緯から、サブスクリプション契約アカウント用に購入方法が用意されています。具体的な購入方法は、Autodesk Knowledge Network に&#0160;<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/JPN/How-to-purchase-Cloud-Credits.html" rel="noopener noreferrer" target="_blank">クラウド クレジットの購入方法 </a></strong>で説明されていますので割愛しますが、この購入方法は、あくまで既存のサブスクリプション契約を保持しているユーザ向けです。純粋に Forge のみで利用する場合には、Autodesk Accounts ページに購入のための [クラウドクレジットを取得] ボタンは表示されないのでご注意ください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2d48da1970c-pi" style="display: inline;"><img alt="Autodesk_account" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2d48da1970c image-full img-responsive" src="/assets/image_317389.jpg" title="Autodesk_account" /></a></p>
<p style="padding-left: 30px;">[クラウドクレジットを取得] ボタンをクリックすると、Autodesk ストアへジャンプしてクレジットカードでの購入が<strong><span style="text-decoration: underline;">日本円</span></strong>で可能です。購入可能なのは 100 パック（100 クラウド クレジット）単位のみで、金額は消費税なしで 16,000円（税込み 17,280円）です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ed3df1970d-pi" style="display: inline;"><img alt="Autodesk_store" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ed3df1970d image-full img-responsive" src="/assets/image_158768.jpg" title="Autodesk_store" /></a></p>
<p style="padding-left: 30px;">なお、直接 Autodesk ストアを表示した場合には、クラウド クレジット の購入オプションは表示されません。&#0160;いまのとろ、購入したクラウド クレジットはサブスクリプション契約もしくは保守プラン契約のアカウント用にのみ適用可能です。Forge デベロッパ キーを取得・利用している Forge アカウント（Autodesk ID）には転送出来ません。</p>
<p><strong>オートデスク認定販売パートナーからの購入</strong></p>
<p style="padding-left: 30px;">オートデスクの&#0160;<strong><a href="https://www.autodesk.co.jp/partners/locate-a-reseller" rel="noopener noreferrer" target="_blank">認定販売パートナー</a>&#0160;</strong>から 100 パック、5,000 パック、10,000 パック、25,000 パック、50,000 パック 単位でのクラウド クレジットを日本円で購入することが出来ます。5,000 パック以上の場合、1 クラウド クレジットあたりの金額は、100 パック単位で購入するよりも若干ディスカウントされた金額での購入が可能になります。詳しくは、認定販売パートナーにご確認ください。いまのとろ、購入したクラウド クレジットはサブスクリプション契約もしくは保守プラン契約のアカウント用にのみ適用可能です。こちらも、Forge デベロッパ キーを取得・利用している Forge アカウント（Autodesk ID）には転送出来ません。</p>
<p><strong>オートデスク ダイレクト セールスからの購入（一部、将来予定）</strong></p>
<p style="padding-left: 30px;">エンタープライズ カスタマーの方は、オートデスクのセールス チームからクラウド クレジットを日本円で購入する方ことが可能です。それ以外の方も、将来、同様の方法でクラウド クレジットをご購入出来るようになる予定です。</p>
<p style="padding-left: 30px;">この場合も、100 パック（100 クラウド クレジット）単位以外に、5,000 パック、10,000 パック、25,000 パック、50,000 パック 単位での購入オプションがあり、1 クラウド」クレジットあたりの金額は、100 パック単位で購入するよりも若干ディスカウントされた金額での購入が可能になります。また、見積書の作成も考慮されています。</p>
<p><strong>オートデスク Forge チームへのコンタクトで購入</strong></p>
<p style="padding-left: 30px;">Autodesk Forge の Pricing ページ（<a href="https://forge.autodesk.com/pricing" rel="noopener noreferrer" target="_blank"><strong>https://forge.autodesk.com/pricing</strong></a>）から [BUY CLOUD CREDIT] ボタンをクリックして、オートデスクの&#0160; Forge チームにコンタクトして購入する方法です。コミュニケーション中に日本円での購入希望を伝えることも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ed3e66970d-pi" style="display: inline;"><img alt="Forge_pricing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ed3e66970d image-full img-responsive" src="/assets/image_104985.jpg" title="Forge_pricing" /></a></p>
<p><strong>&#0160;Autodesk ストアからの購入（将来予定）</strong></p>
<p style="padding-left: 30px;">Forge アカウントに直接適用可能なオプションとして、将来、Autodesk ストアからクレジット カードで 100 パックの購入オプションが検討されています。現在でも、<strong><a href="https://www.autodesk.co.jp/buy-online" rel="noopener noreferrer" target="_blank">Autodesk ストア </a></strong>には「すべての製品」欄に Forge が表示されますが、まだ、購入そのものは出来ません。</p>
<p style="padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2d442a1970c-pi" style="display: inline;"><img alt="Autodesk_store_forge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2d442a1970c image-full img-responsive" src="/assets/image_693549.jpg" title="Autodesk_store_forge" /></a></p>
<p>現在、このように有償化へのシステム作りが進めている状況です。もちろん、正式な購入オプションが利用可能になりましたら、このブログでお知らせする予定です。</p>
<p>※ Forge はサブスクリプション モデルではなく、単純にクラウド クレジットを消費するめ、利用に特別な契約は不要です。</p>
<p>※ Forge で消費されるクラウド クレジットは、クラウド サービスで消費されるクラウド クレジットと同等です。</p>
<p>※ 同一企業内であっても、クラウド クレジットのアカウント間の転送は出来ません。同様に、他のアカウントへの譲渡も出来ません。</p>
<p>※ 購入したクラウド クレジットの有効期限は 1 年間です。繰り越しのような仕組みはありません。</p>
<p>By Toshiaki Isezaki</p>
