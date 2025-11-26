---
layout: "post"
title: "Forge Reality Capture API の一部課金ポリシーの変更について"
date: "2018-10-02 17:24:27"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/portional-change-on-forge-reality-capture-api.html "
typepad_basename: "portional-change-on-forge-reality-capture-api"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad36fc8e5200c-pi" style="float: right;"><img alt="API-REcap-Blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad36fc8e5200c img-responsive" src="/assets/image_320971.jpg" style="margin: 0px 0px 5px 5px;" title="API-REcap-Blue" /></a>Forge Reality Capture API の課金について、一部課金ポリシーが変更されています。</p>
<p>10月2日（米国太平洋時間）から、<a href="https://forge.autodesk.com/en/docs/reality-capture/v1/reference/http/photoscene-:photosceneid-cancel-POST/" rel="noopener noreferrer" target="_blank"><strong>POST photoscene/:photosceneid/cance</strong>l</a>&#0160;&#0160;endpoint を使って、写真から 3D 生成プロセスを途中でキャンセルした場合でも、演算ジョブが初期化、開始された時点で処理完了時と同じクラウドクレジットを消費するようポリシーが変更されています。</p>
<p>以前は、キャンセルされた演算ジョブへのクラウドクレジット消費はおこなわれていませんでした。Reality Capture API で消費するクラウドクレジットについては、過去の本ブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> をご確認ください。</p>
<p>なお、演算ジョブが失敗した場合には、引き続き、クラウドクレジット消費はおこなわれません。今回のポリシー変更について、ご質問等ございましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a> までメールでお問い合わせください。</p>
<p>By Toshiaki Isezaki</p>
