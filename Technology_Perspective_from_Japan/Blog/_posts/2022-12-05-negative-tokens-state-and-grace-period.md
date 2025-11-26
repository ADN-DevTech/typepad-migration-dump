---
layout: "post"
title: "Flex トークンの超過消費と猶予期間"
date: "2022-12-05 01:21:07"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/12/negative-tokens-state-and-grace-period.html "
typepad_basename: "negative-tokens-state-and-grace-period"
typepad_status: "Publish"
---

<p>クラウドクレジット残高を含め、Autodesk Platform Services（旧 Forge）でお使いにアカウントにお手持ち Flex トークン残高がなくなってしまうと、Usage Analytics ページに「<strong>Your token balance is negative. To avoid service disruption, purchase more tokens now.our have rub out of tokens.</strong> （トークン残高がマイナスになっています。サービスの停止を避けるため、今すぐトークンを追加購入してください。）」と表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af148bebd7200c-pi" style="display: inline;"><img alt="Negative_token2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af148bebd7200c image-full img-responsive" src="/assets/image_300971.jpg" title="Negative_token2" /></a><br />（2023年1月に画像差し替え）</p>
<p>また、アカウントに登録したメール アドレス宛に「 Notice of negative balance for Autodesk Platform Services (formerly Forge)」のタイトルで、Flex&#0160; トークンの購入を促すメール（英語）が届きます。</p>
<p><strong> <a href="https://adndevblog.typepad.com/technology_perspective/2022/11/managing-your-autodesk-platform-services-account.html">Autodesk Platform Services アカウント管理</a>&#0160;</strong>でご案内のとおり、同アカウントで継続して Autodesk Platform Services の&#0160; API アクセスを維持するには、14 日間の猶予期間以内に Flex トークンを購入して残高をゼロ、またはそれ以上にする必要があります</p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html" rel="noopener" target="_blank">11 月 7 日に Autodesk Flex による課金制度を導入</a></strong> の記事でご案内していますが、Flex トークンについては、Flex 特約を締結済の Platinum/Gold 認定リセラーの８社（11月7日現在）のいずかかに直接コンタクトいただき、ご購入いただくことが出来ます。</p>
<p>猶予期間が過ぎても残高がマイナスのままの場合は、API アクセスが遮断される可能性がありますのでご注意ください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
