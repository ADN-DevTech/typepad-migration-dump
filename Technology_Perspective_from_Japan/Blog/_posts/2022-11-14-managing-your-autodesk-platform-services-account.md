---
layout: "post"
title: "Autodesk Platform Services アカウント管理"
date: "2022-11-14 00:05:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/managing-your-autodesk-platform-services-account.html "
typepad_basename: "managing-your-autodesk-platform-services-account"
typepad_status: "Publish"
---

<div class="node__image"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a52637200b-pi" style="display: inline;"><img alt="Screen Shot 2022-11-03 at 4.27.53 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a52637200b img-responsive" src="/assets/image_777184.jpg" title="Screen Shot 2022-11-03 at 4.27.53 PM" /></a></div>
<div class="node__body">
<div class="field field--name-field-body field--type-text-with-summary field--label-hidden field-body">
<p>この記事は、Autodesk Platform Services（旧Forge）アカウントの管理について知っておくべきことをお伝えすることを目的としています。</p>
<p>2022 年 11 月 7 日から年間サブスクリプションの提供を終了し、Autodesk Flex を用いた新しい課金体系を導入しています。トライアル（試用）版、フルアクセス、制限付きアクセスのいずれかをお使いいただけます。Flex トークン（含む、お手持ちのクラウド クレジット）残高を維持するだけで、フル アクセス（Full Access）の状態を維持して、無制限に無償 API、およびプレミアム API（課金対象 API）にアクセスすることが出来ます。</p>
<h2>Autodesk Platform Services へのフルアクセスとは何ですか？</h2>
<p>フル アクセスとは、Autodesk Platform Services 上のすべての無償および課金対象 API に制限なしでアクセス可能な状態を指すもので、以前の Forge サブスクリプション契約中の状態に似ています。ただし、サブスクリプションとは異なり、フルアクセスを購入する必要はありません。無償トライアル期間を終了すると、お客様のアカウントは自動的にフルアクセスに切り替わり、API を必要なだけ使用し続けることができます。ただし、課金対象 API を呼び出す場合には、課金が発生する点に注意が必要です。API 毎の消費 Flex トークン数については、<a href="https://forge.autodesk.com/pricing" rel="noopener" target="_blank">価格ページ</a>をご確認ください。</p>
<h2>無償 API のみを使いたい場合はどうすればよいですか？</h2>
<p>問題ありません。フルアクセスでは、無償 API を無制限に使用することができます。Flex トークン（含む、クラウドクレジット）残高がゼロまたはそれ以上を維持している場合は、フルアクセスの状態が維持されます。</p>
<h2>保有する残高がゼロになった場合はどうなりますか？</h2>
<p>繰り返しますが、問題ありません。残高がマイナスになった場合には、14 日間の猶予期間に入ります。2 週間以内に Flex トークンを購入して残高をゼロまたはそれ以上にすれば、 API アクセスが維持出来ます。猶予期間が過ぎても残高がマイナスのままの場合は、API アクセスが遮断される可能性があります。</p>
<h2>Flex トークンとクラウドクレジットのどちらで消費をすればいいですか？</h2>
<p>アカウントにクラウドクレジットと Flex トークンの両方の残高がある場合、クラウドクレジットが優先的に消費されますす。クラウドクレジットが無くなると、Flex トークンを消費し始めます。Flexトークンを未導入の国では、引き続き、クラウドクレジットのみを使用することになります。</p>
<h2>Flex トークンとクラウドクレジットのどちらを購入すればよいですか？</h2>
<p><a href="https://forge.autodesk.com/pricing" rel="noopener" target="_blank">価格ページ</a>にアクセスして、お住まいの国を選択するだけです。Flexトークンかクラウドクレジットのどちらを購入すべきか、このページで確認できます。（<span style="background-color: #ffff00;">日本では Flex トークンをご購入ください。</span>）</p>
<p>ご不明な点がございましたら、いつでも<a href="mailto:%20forge.help@autodesk.com" rel="noreferrer noopener" target="_blank">サポーチーム</a>までご連絡ください。&#0160;</p>
</div>
</div>
<p>※ 本記事は <a href="https://forge.autodesk.com/ja/node/2532">Managing your Autodesk Platform Services Account | Autodesk Forge</a><a href="https://forge.autodesk.com/blog/goodbye-cloud-credits-hello-flex-tokens" rel="noopener" target="_blank"></a>&#0160;から転写・翻訳して一部加筆。修正したものです。</p>
<p>By Toshiaki Isezaki</p>
