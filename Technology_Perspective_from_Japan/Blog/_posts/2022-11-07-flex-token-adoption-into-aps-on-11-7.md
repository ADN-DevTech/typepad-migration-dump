---
layout: "post"
title: "APS へ Flex トークンを導入"
date: "2022-11-07 00:07:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html "
typepad_basename: "flex-token-adoption-into-aps-on-11-7"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/goodbye-cloud-credits-hello-flex-tokens.html" rel="noopener" target="_blank">クラウド クレジットから Autodesk Flex への移行について</a> でご案内しましたとおり、本日、2022年11月7日より、Autodesk Platform Services（旧名 Autodesk Forge）の API 使用にかかる重量課金が <a href="https://www.autodesk.co.jp/benefits/flex" rel="noopener" target="_blank">Autodesk Flex</a> に移行しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180b96b200b-pi" style="display: inline;"><img alt="Launch_flex2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75180b96b200b image-full img-responsive" src="/assets/image_859024.jpg" title="Launch_flex2" /></a></p>
<p>従来からの変更点は、おおまかに次のとおりです。</p>
<ul>
<li>従来の Forge サブスクリプションは廃止</li>
<li>クラウドクレジットによる従量課金は&#0160; Autodesk Flex の Flex トークン消費へ移行される<br />（お手持ちのクラウドクレジットは購入から１年間は有効、かつ、API 利用時に Flex トークンより優先消費される）</li>
<li>API 毎の Flex トークン消費の考え方と消費量はクラウドクレジット時と同一</li>
<li>もともと Autodesk Flex はサブスクリプション契約なしで製品毎に設定されたトークン数を 1 日単位で消費する従量課金制度ですが、Autodesk Platform Services は API の使用量毎に設定による従量課金として利用</li>
<li>製品利用では使用頻度を考慮してサブスクリプション契約をするか Autodesk Flex を利用するかの選択が可能ですが、Autodesk Platform Services では Autodesk Flex の利用のみ（サブスクリプションの選択肢はありません）</li>
</ul>
<p>Flex トークンのご購入については次のようになります。<span style="background-color: #ffff00;">（2023年7月10日更新）</span></p>
<p><a class="asset-img-link" href="https://www.autodesk.com/support/partners?locations=Japan&amp;languages=Japanese&amp;services=Flex" rel="noopener" style="display: inline;" target="_blank"><img alt="How_to_puchase4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cd264d200b image-full img-responsive" src="/assets/image_128218.jpg" title="How_to_puchase4" /></a></p>
<ul>
<li>Flex トークンの購入は Flex 特約を締結済の Platinum/Gold 認定リセラーの 7 社からご購入いただくことが出来ます（直接、Platinum/Gold 認定リセラーにコンタクトいただく）。なお、Flex トークン購入が可能な Platinum/Gold 認定リセラーは、<strong><a href="https://www.autodesk.com/support/partners?locations=Japan&amp;languages=Japanese&amp;services=Flex" rel="noopener" target="_blank">Autodesk Customer Success Hub</a></strong> から検索可能</li>
<li>オートデスク オンラインストア（eStore）からの Flex 販売は2023年7月7日から開始しています。詳しくは、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/autodesk-flex-now-available-in-japan-from-estore.html" rel="noopener" target="_blank">日本で eStore から Autodesk Flex の販売を開始</a></strong>&#0160;をご確認ください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
