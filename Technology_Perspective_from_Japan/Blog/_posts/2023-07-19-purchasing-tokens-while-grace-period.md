---
layout: "post"
title: "猶予期間中のトークン購入"
date: "2023-07-19 00:04:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/07/purchasing_tokens_while_grace_period.html "
typepad_basename: "purchasing_tokens_while_grace_period"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/managing-your-autodesk-platform-services-account.html" rel="noopener" target="_blank">Autodesk Platform Services アカウント管理</a> でも触れていますが、Autodesk Platform Services を利用するにあたって、アカウントの Autodesk Flex トークン残高が常にプラス値になっている必要はありません。トークン残高がゼロの場合でも 、（2023年7月現在）課金対象の Model Derivative API、Design Automation API、Reality Capture API を使用しない限り、API アクセスを継続することが出来ます。言い換えるなら、トークン残高がゼロ以上であれば API 使用を継続することが出来ます。</p>
<p>ただし、課金対象の API を使用してトークン残高がマイナスになってしまった場合には、トークンをゼロ以上に保つために、トークンの追加購入が必要になります。トークン残高がマイナスになった時点で、トークンの追加購入を促す英語のメールが配信されて、同時に、14 日間の猶予期間が始まります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa6ae2200c-pi" style="display: inline;"><img alt="Before_purchase" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa6ae2200c image-full img-responsive" src="/assets/image_34928.jpg" style="width: 757.37px;" title="Before_purchase" /></a></p>
<p>14 日間（2 週間）の猶予期間内に Flex トークンを購入して残高をゼロ、またはゼロ以上にすれば、 API アクセスが維持することが出来ます。逆に、猶予期間が過ぎても残高がマイナスのままの場合は、API アクセスが遮断される可能性があります。お早めにトークンの購入をご検討ください。</p>
<p>猶予期間中にトークンを追加購入すると、購入したトークンが Usage Analytics ページに反映されるようになります。（反映には、最大で 24 時間かかる場合があります。）</p>
<p>もし、トークンの追加購入前にトークン残高（「Available」値）がマイナスになっていた場合、購入したトークン数からマイナス消費分が相殺されて表示されてきます。例えば、追加購入前のトークン残高が -5（マイナス 5）で 100 トークンを購入した場合、100 トークンから -5&#0160; トークン分が差し引かれて、Usage Analytics に表示されるトークン残高が 95 となります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aa6aea200c-pi" style="display: inline;"><img alt="After_purchase" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751aa6aea200c image-full img-responsive" src="/assets/image_673908.jpg" title="After_purchase" /></a></p>
<p>&#0160;</p>
<p>猶予期間中にトークンを購入追加される場合には、マイナス消費分のトークン数をチェックいただくことをお薦めします。</p>
<p>追加購入前のトークン残高が -101（マイナス 101）で 100 トークンを購入した場合、購入後のトークン残高は1（マイナス 1）となり、引き続き API アクセスの遮断対象のままになってしまいます。</p>
<p>Usage Analytics については、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html">Usage Analytics ページ：Flex トークン残高と消費量の確認&#0160;</a>&#0160;の記事でご案内しています。</p>
<p>By Toshiaki Isezaki</p>
