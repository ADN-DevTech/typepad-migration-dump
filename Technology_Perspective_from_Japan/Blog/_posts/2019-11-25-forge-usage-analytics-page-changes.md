---
layout: "post"
title: "Forge Usage Analytics の表示内容の変更"
date: "2019-11-25 22:26:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/forge-usage-analytics-page-changes.html "
typepad_basename: "forge-usage-analytics-page-changes"
typepad_status: "Publish"
---

<p>Design Automation API v3 正式リリースにともない、Forge の API 使用量をレポートする&#0160; <strong><a href="https://forge.autodesk.com/en/analytics" rel="noopener" target="_blank">Usage Analytics ページ</a></strong>が大きく変更されています。</p>
<p>従来、同ページに Autodesk ID を使ってサインインすると、その Autodesk ID 下で作成した複数の Forge App（Client ID と Client Secret）が消費したクラウド クレジットの<span style="text-decoration: underline;">総計</span>が表示されていました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a1243c200c-pi" style="display: inline;"><img alt="Old" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a1243c200c image-full img-responsive" src="/assets/image_843050.jpg" title="Old" /></a></p>
<p>このレポートでは API 別の消費量は把握出来たものの、残念ながら、Forge App 毎のクラウド クレジット消費量を把握することが出来ません。このため、Forge App 毎に消費量を把握するためには、異なる Autodesk ID を使って Forge App（Client ID と Client Secret）を作成、個別に Usage Analytics を参照する必要がありました。</p>
<p>今回の変更では、Usage Analytics が導入された当時と同様に、運用している App（Client ID と Client Secret）毎に使用量を表示するようになっています。App には、既に削除された App の Client ID が凡例とともに表示されるので、過去に運用していた Forge App のクラウド クレジット消費量も参照することが出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cab32a200d-pi" style="display: inline;"><img alt="New" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cab32a200d image-full img-responsive" src="/assets/image_510555.jpg" title="New" /></a></p>
<p>もちろん、課金対象になっている API 別に、Model Derivative API、Design Automation API、Reality Capture API でのクラウド クレジット消費量を Forge App 毎に表示することが出来ます。また、Model Derivative API では、Revit プロジェクト ファイルを変換する Complex Job と、それら以外のファイルを変換する Simple Job の区別して表示出来るほか、Design Automation API では、v3 でサポートされる 4 つのエンジン、AutoCAD、Revit、Inventor、3ds Max 毎の表示が可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cab33d200d-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cab33d200d image-full img-responsive" src="/assets/image_795528.jpg" title="Da" /></a></p>
<p>なお、新しく Forge App を作成する際には、Design Automation API v3 への移行を促進する目的で、使用する API に Design Automation API v2 を選択することが出来なくなっていますのでご注意ください。これは、直ちに Design Automation API v2 を利用した Forge App 運用停止を意味するわけではありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ef0937200b-pi" style="display: inline;"><img alt="New_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ef0937200b image-full img-responsive" src="/assets/image_302120.jpg" title="New_app" /></a></p>
<p>By Toshiaki Isezaki</p>
