---
layout: "post"
title: "APS（旧 Forge）利用時のプロキシ要件"
date: "2020-03-11 00:27:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/03/forge-proxy-requirement-for-use.html "
typepad_basename: "forge-proxy-requirement-for-use"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f0c790200d-pi" style="float: right;"><img alt="Cloud_security" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f0c790200d img-responsive" src="/assets/image_253761.jpg" style="width: 210px; margin: 0px 0px 5px 5px;" title="Cloud_security" /></a>Autodesk Platform Services（APS、旧 Forge）を使ったアプリの開発時、あるいは、利用時のプロキシやポートなどの通信環境について、ご質問をいただくことがあります。ここでは、それらについて、最低限必要な情報をお伝えしておきたいと思います。</p>
<p>まず、念のため、APS を含むオートデスク製品の利用時には、*.autodesk.com ドメインをホワイトリストに加えていただくことを推奨しています。次の Autodesk Knowledge Network 記事をご確認ください。</p>
<p style="padding-left: 40px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/sfdcarticles/sfdcarticles/JPN/What-URLs-protocols-should-be-accessible-for-Desktop-Subscription-to-work-html.html" rel="noopener" target="_blank">許可する必要があるオートデスク サブスクリプション ライセンスの URL およびプロトコル (autodesk.co.jp)</a></strong></p>
<p>次に開放すべきポート番号についてです。APS をお使いいただく際には、https プロトコルで使用する <strong>443 ポート </strong>と、Viewer がストリーミング表示で使用する <strong>7124 ポート</strong>、また、<strong>7125 ポート</strong> をオープンにする必要があります。</p>
<p>もし、Design Automation API をご利用いただく場合には、次の制限事項もご確認ください。</p>
<p style="padding-left: 40px;"><strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/restrictions/" rel="noopener" target="_blank">Restrictions for Design Automation API</a></strong></p>
<p><strong>Design Automation API</strong> をご利用いただく場合には、コールバック処理を受信するために、次の IP アドレスをホワイトリスト化が必要となります。（<span style="background-color: #ffff00;">2023年2月更新</span>）</p>
<p>Design Automation API for Revit</p>
<ul>
<li>3.229.167.149</li>
<li>44.207.230.78</li>
<li>54.175.193.194</li>
</ul>
<p>Design Automation API for Inventor</p>
<ul>
<li>52.20.163.163</li>
<li>18.210.124.48</li>
<li>3.90.98.123</li>
</ul>
<p>Design Automation API for AutoCAD</p>
<ul>
<li>35.170.46.104</li>
<li>3.211.89.243</li>
<li>3.210.87.162</li>
</ul>
<p>Design Automation API for 3ds Max</p>
<ul>
<li>18.213.152.234</li>
<li>54.85.240.25</li>
<li>18.208.26.46</li>
</ul>
<p>※ この変更は、稀に当社のインフラの再構築や増強に起因して変更されることがあります。この変更は定期的に起こるものではありません。変更は、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/restrictions/" rel="noopener" target="_blank">Restrictions</a>&#0160;に反映されていきます。</p>
<p>By Toshiaki Isezaki&#0160;</p>
