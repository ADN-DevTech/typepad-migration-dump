---
layout: "post"
title: "Reality Capture API の価格変更について"
date: "2022-08-12 00:49:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/08/pricing-change-for-reality-capture-api.html "
typepad_basename: "pricing-change-for-reality-capture-api"
typepad_status: "Publish"
---

<div class="node__image"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed07dbf200d-pi" style="display: inline;"><img alt="Screen Shot 2022-07-06 at 7.33.29 AM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed07dbf200d image-full img-responsive" src="/assets/image_296149.jpg" title="Screen Shot 2022-07-06 at 7.33.29 AM" /></a></div>
<div class="node__body">
<div class="field field--name-field-body field--type-text-with-summary field--label-hidden field-body">
<h2>Reality Capture APIの新価格設定について&#0160;</h2>
<p>3 月 29 日、1 ギガピクセルにつき 1 クラウドクレジットの（暫定的な）価格を変更を<a href="https://adndevblog.typepad.com/technology_perspective/2022/02/upcoming-forge-pricing-changes.html" rel="noopener" target="_blank">アナウンス</a>いたしました。これは、Reality Capture API の変更計画の第 1 弾でした。現在、第 2 段階となる変更を 2022 年 9 月 7 日午前 0 時（UTC - 協定世界時間）に実施いたします。</p>
<h2>Reality Capture API に「写真数単位」の価格設定を導入&#0160;</h2>
<p>9 月 7 日、Reality Capture API の価格をギガピクセル単位から写真単位に更新いたします。&#0160;</p>
<table aria-rowcount="2" border="1" data-tablelook="1696" data-tablestyle="MsoTableGrid">
<tbody>
<tr aria-rowindex="1" role="row">
<td data-celllook="0" role="rowheader">
<p>現在の価格</p>
</td>
<td data-celllook="0" role="columnheader">
<p>新しい価格</p>
</td>
</tr>
<tr aria-rowindex="2" role="row">
<td data-celllook="0" role="rowheader">
<p>ギガピクセル毎に 1 クラウドクレジット&#0160;</p>
</td>
<td data-celllook="0">
<p>写真画像 50 枚毎に 1 クラウドクレジット&#0160;</p>
</td>
</tr>
</tbody>
</table>
<p>* EBA のお客様は、50 枚の写真につき1トークンとなります。</p>
<p>Reality Capture API 呼び出しの課金処理において、写真のサイズが判断材料から除外するようになります。新しい「写真単位」の価格設定は、画像サイズに関係なく、コストと使用量の予測を合理化に役立てることが出来ます。</p>
<h2>9 月 7 日までに Reality Capture API データをダウンロードください&#0160;</h2>
<p>この新しいアプローチへの移行にともない、9 月 7 日以降、<a href="https://adndevblog.typepad.com/technology_perspective/2017/02/usage-analytics-on-forge.html" rel="noopener" target="_blank">Usage Analytics</a>（使用状況分析）ページで写真単位での使用量データしかレポートできなくなります。具体的には、従来のギガピクセル単位の使用量データは表示されなくなり、過去のデータも取得できなくなります。このため、9 月 7 日午前 0 時（UTC）までにデータをダウンロードいただきますようお願いいたします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d47f3a7200b-pi" style="display: inline;"><img alt="Usage_analytics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d47f3a7200b image-full img-responsive" src="/assets/image_39209.jpg" title="Usage_analytics" /></a></p>
<p>データをダウンロードするには、Forge アカウントにログインして、Usage Analytics ページにアクセスしてください。サポートが必要な場合は、<a href="mailto:forge.help@autodesk.com" rel="noopener" target="_blank">forge.help@autodesk.com</a> までご連絡ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d47f412200b-pi" style="display: inline;"><img alt="Export_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d47f412200b image-full img-responsive" src="/assets/image_379833.jpg" title="Export_data" /></a></p>
<h2>Reality Capture API の価格変更についての次のステップは？&#0160;</h2>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">Reality Capture API をお使いの皆様には、8 月から 9 月の変更実施時に、（英語）メールでご連絡を差し上げる予定です。&#0160;</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>9 月 7 日までに、（必要に応じて）前述のデータのダウンロードをお願いいたします。&#0160;</p>
</li>
</ul>
<p>この変更についてご質問がある場合は、<a href="mailto:forge.sales@autodesk.com" rel="noreferrer noopener" target="_blank">forge.marketing@autodesk.com</a> までご連絡ください。&#0160;</p>
<p>※ 本記事は <a href="https://forge.autodesk.com/ja/node/2270" rel="noopener" target="_blank">Pricing changes for Reality Capture API | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
</div>
