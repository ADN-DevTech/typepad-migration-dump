---
layout: "post"
title: "課金出来ない状態の APS 使用量"
date: "2025-07-30 01:34:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/uncharged-aps-usage.html "
typepad_basename: "uncharged-aps-usage"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/07/token-usage-report-per-application.html" rel="noopener" target="_blank">アプリ別 Token Usage レポート</a> の [API usage] タブに、課金対象 API の使用で残高が不足した際に、未課金の状態を可視化する機能が追加されています。未課金とは、アプリに割り当てられているチームのトークンが有効期限の１年を過ぎてしまったり、トークン残高を超えて API 使用してしまい、残高がマイナスになっている状態を指します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861065c87200d-pi" style="display: inline;"><img alt="Api_usage_tab" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861065c87200d img-responsive" src="/assets/image_746068.jpg" title="Api_usage_tab" /></a></p>
<p>３月の APS へのチーム導入でアプリへの&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2025/03/assigning-tokens-to-team.html" rel="noopener" target="_blank">チームへのトークン割り当て</a> が必須になっていますが、アプリを所有するアカウントの My applications ページ（<strong><a href="https://aps.autodesk.com/hubs/@personal/applications/" rel="noopener" target="_blank">https://aps.autodesk.com/hubs/@personal/applications/</a></strong>）に <strong>Uncharged APS usage&#0160;</strong> のメッセージが表示されたり、「<strong>Uncharged usage for your Autodesk team</strong>」のタイトルを持つメールが届いても、どれくらいのトークンが不足しているか（未課金の状態）わかりませんでした。この機能で不足分を把握することが出来るようになります。</p>
<p>具体的には、表示される棒グラフが色分けして表示されるようになります。</p>
<ul>
<li>
<p><strong>朱色</strong>：未課金のトークン使用量（不足しているマイナス トークン）</p>
</li>
<li>
<p><strong>青色</strong>：正常にトークンが消費された使用量</p>
</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d8c88d200c-pi" style="display: inline;"><img alt="Uncharged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d8c88d200c image-full img-responsive" src="/assets/image_467033.jpg" title="Uncharged" /></a></p>
<p>Autodesk Flex トークンを購入してチームに正しく割り当てられると、朱色の棒グラフも青色に変化します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ef7c49200b-pi" style="display: inline;"><img alt="Charged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ef7c49200b img-responsive" src="/assets/image_835440.jpg" title="Charged" /></a></p>
<p>この際、購入したトークン数からマイナス分のトークン数（未課金のトークン 使用量）が相殺されることになります。</p>
<p>Autodesk Account（<a href="https://manage.autodesk.com/balances" rel="noopener" target="_blank">https://manage.autodesk.com/balances</a>）でトークン使用状況を確認すると、トークンの購入直後であっても相殺されたトークン数が「使用済み」として表示されます。</p>
<ul>
<li>Autodesk Account のトークンの使用状況表示は、課金対象 API 全体の消費量を示します。冒頭のグラフは、「Viewer 2-legged Basic Process」 アプリが Model Derivative API の Complex ジョブのみを消費状態を示すものです。ご注意ください。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861065f0a200d-pi" style="display: inline;"><img alt="Balance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861065f0a200d img-responsive" src="/assets/image_412518.jpg" title="Balance" /></a></p>
<p>By Toshiaki Isezaki</p>
