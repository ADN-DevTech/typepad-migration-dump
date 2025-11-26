---
layout: "post"
title: "クラウド クレジットから Autodesk Flex への移行について"
date: "2022-09-09 00:19:55"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/goodbye-cloud-credits-hello-flex-tokens.html "
typepad_basename: "goodbye-cloud-credits-hello-flex-tokens"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308deea4d200c-pi" style="display: inline;"><img alt="Autodesk_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308deea4d200c image-full img-responsive" src="/assets/image_639360.jpg" title="Autodesk_token" /></a></p>
<h2 aria-level="1" role="heading">Forge は 11 月 7 日にクラウド クレジットから Flexトークンに移行</h2>
<p><span style="background-color: #ffff00;">この情報は、Token Flex Enterprise Business Agreement (EBA) を介して Forge にアクセスするお客様を対象としたものではありません。ご質問をお持ちの場合には、<em>契約管理者の方</em>にご確認ください。</span></p>
<p>今年 3 月、クラウド クレジットの価格変更とともに、Forge に対する課金制度を Forge サブスクリプション下のクラウド クレジット消費から、<strong><a href="https://www.autodesk.co.jp/benefits/flex" rel="noopener" target="_blank">Autodesk Flex</a></strong> の Flexトークン消費へ移行することを<strong><a href="https://adndevblog.typepad.com/technology_perspective/2022/02/upcoming-forge-pricing-changes.html" rel="noopener" target="_blank">発表</a></strong>しました。その際、具体的な移行時期を「未定」となっていましたが、この度、11 月 7&#0160; 日午前 0 時（GMT）に、日本を含む一部の国を対象に、Forge 用途のクラウド クレジット販売を停止し、新しく Flex トークンの販売を開始、Autodesk Flex へ移行することを決定しました。</p>
<p>なお、現在お手持ちのクラウド クレジットは、有効期限（ご購入後 1 年間）までご利用いただけます。クラウド クレジットを消費し尽くした、あるいは、クラウド クレジットの有効期限が切れた場合には、Flex トークンをご購入いただく必要があります。Flex トークンはクラウド クレジットと同じコストでご購入いただくことが出来ます。下記に、今回の変更について詳細をご案内していきます。&#0160;</p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">Flex トークンを購入出来る国？</h2>
<p>Flex トークンは、北米、ヨーロッパ、日本など、20 カ国以上で販売されています。 Flex トークンが販売されていない国では、お客様はクラウド クレジットのままお使いいただくことになります。クラウド クレジットの購入のプロセスも従来通りです。&#0160;</p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">Flex について知っておくべきこと&#0160;</h2>
<p>Flex トークンは、クラウドクレジットと同様、オートデスクの仮想通貨と考えることが出来ます。ただし、クラウド クレジットにはない価値も提供します。Flex トークンを保有することで、さまざまなオートデスクのソフトウェアをも利用できるだけでなく、Forge プラットフォームにもアクセスできるようになります。詳しくは<strong><a href="https://www.autodesk.co.jp/buying/flex/flex-rate-sheet" rel="noopener" target="_blank">レートシート</a></strong>をご確認ださい（Forge はまだ未反映）。&#0160;</p>
<p>Flexトークンの詳細、および、Flexトークンの購入については、<strong><a href="https://www.autodesk.co.jp/benefits/flex" rel="noopener" target="_blank">https://www.autodesk.co.jp/benefits/flex</a> </strong>をご覧ください。</p>
<p>Flex トークンはクラウド クレジットと同じように動作し、同じコストがかかります。そのため、ほとんどの API は無料で利用できますが、課金対象の API に対する消費量は変更されず、次のようになります。:</p>
<table aria-rowcount="4" border="1" data-tablelook="1696" data-tablestyle="MsoTableGrid">
<tbody>
<tr aria-rowindex="1" role="row">
<td data-celllook="0" role="rowheader">
<p><strong>課金対象 API&#0160;</strong></p>
</td>
<td data-celllook="0" role="columnheader">
<p><strong>クラウド クレジット （略称 CC）コスト&#0160;</strong></p>
</td>
<td data-celllook="0" role="columnheader">
<p><strong>Flex トークン（略称 FT）コスト&#0160;</strong></p>
</td>
</tr>
<tr aria-rowindex="2" role="row">
<td data-celllook="0" role="rowheader">
<p>Design Automation API&#0160;</p>
</td>
<td data-celllook="0">
<p>2 CC（1 時間当たりの処理時間毎）&#0160;</p>
</td>
<td data-celllook="0">
<p>2 FT（1 時間当たりの処理時間毎）</p>
</td>
</tr>
<tr aria-rowindex="3" role="row">
<td data-celllook="0" role="rowheader">
<p>Model Derivative API&#0160;</p>
</td>
<td data-celllook="0">
<p>0.5 CC ー コンプレックス ジョブ&#0160;</p>
<p>0.1 CC ー シンプル ジョブ</p>
</td>
<td data-celllook="0">
<p>0.5 FT ー コンプレックス ジョブ</p>
<p>0.1 FT ー シンプル ジョブ</p>
</td>
</tr>
<tr aria-rowindex="4" role="row">
<td data-celllook="0" role="rowheader">
<p>Reality Capture API&#0160;</p>
</td>
<td data-celllook="0">
<p>1 CC（アップロード写真画像 50 枚毎）</p>
</td>
<td data-celllook="0">
<p>1 FT（アップロード写真画像 50 枚毎）</p>
</td>
</tr>
</tbody>
</table>
<h6>&#0160;</h6>
<h2>Flex トークンの初期効果</h2>
<p>Flex トークンを使うことで、今実感できるメリットがあります。例えば、もし、クラウド クレジットと Flex トークン（現時点では製品利用目的で）の両方を保有している場合、クラウド クレジットを使い切ると、自動的に Flex トークンが消費されるようになるので、API 使用時の消費がマイナスになったり、API アクセスが停止されてしまうリスクがなくなります。なお、API あたりのコストは、クラウド クレジットと Flex トークンのどちらをも同じです。料金の詳細は上記の表を参照してください。</p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">11 月 7 日にクラウドクレジットはどうなるのか？&#0160;</h2>
<p>クラウド クレジットを使い切るか、有効期限が切れるまでは、既存のクラウド クレジット残高から API 使用に対する課金がおこなわれます（お手持ちのクラウド クレジットを使用することができます）。クラウド クレジットを使い切ると、自動的に Flex トークンの使用が開始されます。&#0160;&#0160;</p>
<p>未使用のクラウド クレジットは、ごから 1 年後に失効します。</p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">Forge サブスクリプションの変更 - フルアクセスの導入&#0160;</h2>
<p>Flex トークンへの移行による真の従量課金モデルの実現にともない、Forge 年間サブスクリプションの提供は終了します。代わりに、Flex トークン残高を持つお客様には、API への「フルアクセス」が付与されます。今後、Forge を毎年更新する必要はありません。Flexト ークン（または残りのクラウドクレジット）残高がマイナスにならないように維持するだけで、すべての Forge API とサービスへのアクセス、つまり「フルアクセス」が保証されます。&#0160;</p>
<p>Flex トークン残高を使い果たしてしまった場合、14 日間の猶予期間が設けられます。この 2 週間の猶予期間に Flex トークンを追加購入して残高をプラスに戻すことが出来ます。猶予期間が過ぎてしまうと、同アカウントは制限付きアクセスに設定され、無料の API へのアクセスしか出来なくなってしまいます。課金対象の API （今後、Premium API と呼称）へのアクセスは遮断されます。フルアクセスを再開するには、<a href="mailto:%20forge.help@autodesk.com" rel="noreferrer noopener" target="_blank">サポートチーム</a>にご連絡の上、Flex トークンをご購入いただき（含む、マイナス消費分の補完）、アカウントのブロックを解除するようにしてください。<span style="background-color: #ffff00;">日本は Autodesk Flex 対象国になっているので、Forge 利用も Flex トークン消費に移行します。</span></p>
<p>Autodesk Flex 未導入の国（クラウド クレジットをまだ使用する国）にお住まいの場合、このサブスクリプションの変更は適用されませんのでご注意ください。Forge API やサービスへの継続的なアクセスを維持するためには、引き続き年間サブスクリプションとクラウド クレジット残高を維持する必要があります。</p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">Forge 90 日間トライアルに関する変更点&#0160;</h2>
<p>90 日間の Forge トライアル期間中には、クラウド クレジット や Flex トークンは消費されません。期間中の API アクセスは課金対象の API も含め、すべてのアクセスが可能です。ただし、その利用は非商用利用に制限されるようになります。非商用利用とは、テストや評価のみを目的とした利用を指します。詳しくは <a href="https://www.autodesk.com/company/legal-notices-trademarks/terms-of-service-autodesk360-web-services/forge-platform-web-services-api-terms-of-service" rel="noopener" target="_blank">Term of Services（利用規約）</a>をご確認ださい。&#0160;</p>
<p>90 日間のトライアル期間が終了すると、自動的にフル アクセスに切り替わります。これは、商用利用ですべての Forge API&#0160; にアクセスできることを意味します。無料の API は引き続きそのまま呼び出すことができますが、課金対象 API をお使いの場合は、Flex トークンを消費するようになります。繰り返しになりますが、無料の API のみを使用している場合は、フルアクセスで商業目的であっても無料で使用し続けることができます。</p>
<p><span style="background-color: #ffffff;">アプリケーションの商用利用を早く開始したい場合は、いつでもアカウントをトライアルからフル アクセスに切り替え、Forge の商用利用を開始することができるスイッチがページに追加される予定です。&#0160;</span></p>
<p>&#0160;</p>
<h2 aria-level="1" role="heading">ご質問は？</h2>
<p>オートデスクは、この移行がスムーズに実施されるようにしたいと考えています。ご不明な点がございましたら、いつでも<a href="mailto:%20forge.help@autodesk.com" rel="noreferrer noopener" target="_blank">サポーチーム</a>までご連絡ください。&#0160;<a href="mailto:forge.help@autodesk.com" rel="noreferrer noopener" target="_blank">forge.help@autodesk.com</a>&#0160; でお待ちしております。&#0160;</p>
<p>※ 本記事は <a href="https://forge.autodesk.com/blog/goodbye-cloud-credits-hello-flex-tokens" rel="noopener" target="_blank">Goodbye Cloud Credits. Hello Flex tokens. | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</p>
<p>By Toshiaki Isezaki</p>
