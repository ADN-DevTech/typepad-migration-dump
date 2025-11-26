---
layout: "post"
title: "Automation API: Activity と AppBundle の共有"
date: "2025-09-10 01:03:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/09/automation-api-shared-activity-and-appbundle.html "
typepad_basename: "automation-api-shared-activity-and-appbundle"
typepad_status: "Future"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3da8503200c-pi" style="float: right;"><img alt="Shared_activities" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3da8503200c img-responsive" src="/assets/image_460834.jpg" style="margin: 0px 0px 5px 5px;" title="Shared_activities" /></a></p>
<p>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/workitem-test-on-vs-code-forge-extension.html" rel="noopener" target="_blank">Design Automation API：VS Code を使った WorkItem テスト</a>&#0160;でご紹介した <strong>AutoCAD.PlotToPDF.prod</strong> Activity のように、独自に定義した Activity や AppBundle とは別に、初めから定義、利用することが出来る Activity や AppBundle が存在します。</p>
<p>これらは、オートデスクがテスト目的や要望の多い定義（特に Activity）を事前に用意したものです。あいにく、Automation API（旧名 Design Automation API）を利用するすべてのユーザーに Activity や AppBundle をパブリック共有する方法は提供されていません。</p>
<p>前述の <strong>AutoCAD.PlotToPDF.prod</strong> Activity のようなパブクリック共有は、現時点ではオートデスクのみが実現出来る機能になっています。</p>
<p>ただし、特定のアプリに限定したかたちでは、Activity や AppBundle を共有する方法が提供されてます。<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Faps.autodesk.com%2Fen%2Fdocs%2Fdesign-automation%2Fv3%2Freference%2Fhttp%2Factivities-id-aliases-POST%2F&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7Ceb967300833c4705939108ddd7ed0d86%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638904135514609107%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&amp;sdata=0VFUqAka%2FqibD8xsOivw1nvicqisv9yn5LPvxEBS2Vo%3D&amp;reserved=0" rel="noopener" target="_blank">POST activities/:id/aliases</a>、<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Faps.autodesk.com%2Fen%2Fdocs%2Fdesign-automation%2Fv3%2Freference%2Fhttp%2Fappbundles-id-aliases-POST%2F&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7Ceb967300833c4705939108ddd7ed0d86%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638904135514620329%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&amp;sdata=F8l%2BH87Q80XRl0fut1o9c2TP%2FDoPSVHYrhcVGOGQ7ag%3D&amp;reserved=0" rel="noopener" target="_blank">POST appbundles/:id/aliases</a> エンドポイントのリクエスト ボディに&#0160; ”<strong>receiver</strong>” パラメーターを利用して、共有先としたいアプリの Client ID またはニックネームを単一指定、または配列指定で複数指定する方法です。</p>
<p>配列指定で複数アプリと共有する場合には、次の点に注意してください。</p>
<ul>
<li>配列指定で複数アプリと共有する場合、Activity/AppBundle 毎に共有指定出来るアプリ（Client ID またはニックネーム）は最大 30 になります。共有数が 30 以上必要な場合には、同じ内容の Activity/AppBundle を複数用意する必要があります。</li>
<li>共有先に指定する Client ID<strong>/</strong>ニックネームをすべてのアプリに、少なくとも 1 つの Avtivity、または AppBundleなどの Automation API リソースがある必要があります。</li>
</ul>
<p>次の例は、Client ID にそれぞれ&#0160; &quot;mDyW9ps2613NZ8AIaQVrllFYueRi6dwS&quot; と &quot;AjFukUWeRk05eA9XpH8Nnh62BzPD60mg&quot; を持つ 2 つのアプリに Avtivity を共有する、<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Faps.autodesk.com%2Fen%2Fdocs%2Fdesign-automation%2Fv3%2Freference%2Fhttp%2Factivities-id-aliases-POST%2F&amp;data=05%7C02%7Ctoshiaki.isezaki%40autodesk.com%7Ceb967300833c4705939108ddd7ed0d86%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638904135514609107%7CUnknown%7CTWFpbGZsb3d8eyJFbXB0eU1hcGkiOnRydWUsIlYiOiIwLjAuMDAwMCIsIlAiOiJXaW4zMiIsIkFOIjoiTWFpbCIsIldUIjoyfQ%3D%3D%7C0%7C%7C%7C&amp;sdata=0VFUqAka%2FqibD8xsOivw1nvicqisv9yn5LPvxEBS2Vo%3D&amp;reserved=0" rel="noopener" target="_blank">POST activities/:id/aliases</a> エンドポイントに指定するリクエスト ボディです。</p>
<pre>{
&#0160; &#0160; &quot;version&quot;: 1,
&#0160; &#0160; &quot;id&quot;: &quot;dev&quot;,
&#0160; &#0160; &quot;receiver&quot;: [ 
&#0160; &#0160; &#0160; &#0160; &quot;mDyW9ps2613NZ8AIaQVrllFYueRi6dwS&quot;,
&#0160; &#0160; &#0160; &#0160; &quot;AjFukUWeRk05eA9XpH8Nnh62BzPD60mg&quot;
&#0160; &#0160; ]
}
</pre>
<p>By Toshiaki Isezaki</p>
