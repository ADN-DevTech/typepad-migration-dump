---
layout: "post"
title: "Viewer の「一人称視点ツール」"
date: "2023-03-08 00:06:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/03/aps-viewer-first-person-tool.html "
typepad_basename: "aps-viewer-first-person-tool"
typepad_status: "Publish"
---

<p>APS Viewer（旧 Forge Viewer）と APS Viewer を利用した <a href="https://viewer.autodesk.com/" rel="noopener" target="_blank">Autodesk Viewer</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2020/09/bim-360-viewer-vs-forge-viewer.html" rel="noopener" target="_blank">BIM 360 Viewer</a> などでは、さまざまな標準ツールが用意されています。その中には、<a href="https://adndevblog.typepad.com/technology_perspective/2023/02/aps-viewer-screen-capture.html" rel="noopener" target="_blank">APS Viewer：スクリーンキャプチャ</a> で少し触れた「一人称ツール」があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75198e1b3200c-pi" style="display: inline;"> </a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852bc1e4200d-pi" style="display: inline;"><img alt="First_person" class="asset  asset-image at-xid-6a0167607c2431970b02b6852bc1e4200d img-responsive" src="/assets/image_685268.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="First_person" /></a></p>
<p>一人称ツールを利用すると、キーボードやマウス操作を利用して、カンバスに表示中の 3D モデル内をウォークスルーしたり、周囲を見回したり、上下に移動したりするなど、自由に視点を変更することが出来ます。</p>
<p>ツールバーから一人称ツールのボタンをクリックして起動すると、操作可能なキーボード操作が画面に表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75198dce9200c-pi"><img alt="Navigation_guide" class="asset  asset-image at-xid-6a0167607c2431970b02b75198dce9200c img-responsive" src="/assets/image_943752.jpg" style="width: 600px; display: block; margin-left: auto; margin-right: auto;" title="Navigation_guide" /></a></p>
<p>表示中の視点で [↑] キーを押し続けると、画面に表示される ✛ カーソルに向かってウォークスルーが始まります。[←] キーと [→] キーで左右に進行方向を変えたり、[↓] キーで後退することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852bbddf200d-pi" style="display: inline;"><img alt="Walkthrough" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852bbddf200d image-full img-responsive" src="/assets/image_848840.jpg" title="Walkthrough" /></a></p>
<p>ウォークスルー中に視点が上下に移動してしまったり、[Q] キーや [E] キーで始点を上下に移動しようとしても元に戻ってしまう現象が出る場合には、「設定ツール」で [ナビゲーション] タブの「重力を有効にする」をオフにしてみてください。この設定がオンになっていると、モデルの底面に視点が拘束されてしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75198e40c200c-pi"><img alt="Disable_gravity" class="asset  asset-image at-xid-6a0167607c2431970b02b75198e40c200c img-responsive" src="/assets/image_119727.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Disable_gravity" /></a></p>
<p>✛ カーソルはマウスの左ボタンやタッチパッドの左ボタンを押し続けて変更することが出来ます。この操作で周囲を見回す動作が得られます。用途にも依りますが、事前にパノラマ画像を作成して Web ブラウザでパノラマを実現するよりも容易です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751748797200b-pi" style="display: inline;"><img alt="Lookaround" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751748797200b image-full img-responsive" src="/assets/image_641499.jpg" title="Lookaround" /></a></p>
<p>By Toshiaki Isezaki</p>
