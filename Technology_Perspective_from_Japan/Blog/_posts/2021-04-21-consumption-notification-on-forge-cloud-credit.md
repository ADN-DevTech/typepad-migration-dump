---
layout: "post"
title: "Forge クラウドクレジットの消費通知機能について"
date: "2021-04-21 00:01:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/consumption-notification-on-forge-cloud-credit.html "
typepad_basename: "consumption-notification-on-forge-cloud-credit"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2017/02/usage-analytics-on-forge.html" rel="noopener" target="_blank"><strong>Forge 使用量の分析</strong></a> のブログ記事内のとおり、<strong>Forge ポータル</strong>（<a href="https://forge.autodesk.com/" rel="noopener" target="_blank"><strong>https://forge.autodesk.com/</strong></a>）の <a href="https://forge.autodesk.com/en/analytics/" rel="noopener" target="_blank"><strong>Usage Analytics</strong></a> ページには、Forge でお使いのクラウド クレジット消費量がアプリ単位で集計、表示されるようになっています。</p>
<p>クラウド クレジットは、オートデスクのクラウド サービスで作業する際のオートデスクの仮想通貨のようもので、Forge API にも使用にも適用されています。Forge サブスクリプションには、1&#0160; 年間有効な 100 クラウド クレジットが付属していますが、お手持ちのクラウド クレジットがなくなってしまうと、追加のクラウド クレジットをご購入いただく必要があります。</p>
<p>クラウド クレジットの残高をプラスに保つ必要があるため、定期的に Usage Analytics ページを確認することをお勧めしています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeccd874200c-pi" style="display: inline;"><img alt="Cc_notification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeccd874200c image-full img-responsive" src="/assets/image_694998.jpg" title="Cc_notification" /></a></p>
<p>この Usage Analytics ページに、クラウド クレジット残高が少なくなってしまった場合の通知を表示する機能が追加されましたので、ご紹介しておきたいと思います。</p>
<p>Usage Analytics&#0160; ページ右上の「アラートベル」アイコンをクリックすると、クラウドクレジットの残高が少なくなったときに通知を表示するための&#0160;<span style="text-decoration: underline;">しきい値&#0160;</span>を設定出来るようになっています。しきい値 は既定値で 30 クレジットに設定されていますが、クラウド クレジットの使用速度に応じて、しきい値 を最適な数値に変更することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e99f8f68200b-pi" style="display: inline;"><img alt="Cc_notification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e99f8f68200b img-responsive" src="/assets/image_78491.jpg" title="Cc_notification" /></a></p>
<p>この通知設定によって、クラウド クレジット残高が指定した値に達すると、アラートバナーが Usage Analytics ページ上に表示され、クラウドクレジットの追加購入を促すようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788024c532200d-pi" style="display: inline;"><img alt="Warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788024c532200d image-full img-responsive" src="/assets/image_371824.jpg" title="Warning" /></a></p>
<p>Forge サブスクリプション期間内に追加のクラウド クレジットをご購入いただく方法は、<a href="https://adndevblog.typepad.com/technology_perspective/2020/02/important-about-forge-charge-of-cost-revised.html#buy_addcc" rel="noopener" target="_blank"><strong>【重要】Forge 課金について（最新）</strong> </a>でご紹介しています。</p>
<ul style="list-style-type: disc;">
<li>アラートバナー右の [Buy Cloud Credit] からも追加のクラウド クレジットを eStore でご購入いただくことが出来ますが、Forge サブスクリプションの購入項目も一緒に表示されてしまうので、二重にサブスクリプションを購入しないようご注意ください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
