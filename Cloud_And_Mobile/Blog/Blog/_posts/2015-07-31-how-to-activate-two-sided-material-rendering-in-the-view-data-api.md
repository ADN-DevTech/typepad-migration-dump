---
layout: "post"
title: "How to activate two-sided material rendering in the View & Data API?"
date: "2015-07-31 01:34:59"
author: "Philippe Leefsma"
categories:
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/how-to-activate-two-sided-material-rendering-in-the-view-data-api.html "
typepad_basename: "how-to-activate-two-sided-material-rendering-in-the-view-data-api"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>This workaround applies if you are working with two-sided materials in your original model: the View &amp; Data extractors do not always set the two sided flag in the output, because they don&#39;t know when they should or should not do that.</p>
<p>There is a way to work around this on the client side, by setting the <em>&quot;side&quot;</em> property of 3d materials that need it&#0160;to <em>THREE.DoubleSide</em> and telling the render context about it. Here is a quick snippet that illustrates how to achieve that:</p>
<p>&#0160;</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; border-width: 0.01mm; border-color: #000000; border-style: solid; padding: 4px; font-size: 12pt;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">function</span><span style="background-color: #ffffff;"> activateTwoSidedRendering(viewer) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 
 3 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> materials = viewer.impl.matman().materials;
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 
 5 </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">for</span><span style="background-color: #ffffff;"> (</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">var</span><span style="background-color: #ffffff;"> matKey </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">in</span><span style="background-color: #ffffff;"> materials) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 
 7 </span><span style="background-color: #ffffff;">    materials[matKey].side = THREE.DoubleSide;
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span><span style="background-color: #ffffff;">    materials[matKey].needsUpdate = </span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;">;
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 </span><span style="background-color: #ffffff;">  }
</span><span style="color: #800000; background-color: #f0f0f0;">10 
11 </span><span style="background-color: #ffffff;">  viewer.impl.renderer().toggleTwoSided(</span><span style="color: #000080; background-color: #ffffff; font-weight: bold;">true</span><span style="background-color: #ffffff;">);
</span><span style="color: #800000; background-color: #f0f0f0;">12 </span><span style="background-color: #ffffff;">}</span></pre>
