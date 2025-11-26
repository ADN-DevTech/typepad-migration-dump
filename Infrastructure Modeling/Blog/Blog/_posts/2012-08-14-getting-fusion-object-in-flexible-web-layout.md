---
layout: "post"
title: "Getting Fusion object in flexible web layout"
date: "2012-08-14 15:12:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/getting-fusion-object-in-flexible-web-layout.html "
typepad_basename: "getting-fusion-object-in-flexible-web-layout"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>When developing with flexible web layout, we always need to get the global object Fusion. Sometimes you may get error message: &#39; Fusion is undefined&#39; if you do not get it correctly.</p>
<p>Actually, how to get the Fusion object depends on how you embed the fusion viewer into your webpage and where you call your JavaScript to get it. Generally speaking, if you are not using &lt;frame&gt; or &lt;iframe&gt; when embedding fusion viewer, following code snippet should work for you.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span style="font-size: 8pt;">var</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> MainFusionWindow = GetFusionWindow();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span style="font-size: 8pt;">var</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> OpenLayers = MainFusionWindow.OpenLayers;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span style="font-size: 8pt;">var</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> Fusion = MainFusionWindow.Fusion;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">alert(Fusion);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #006400;">/* locate the Fusion window */</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span style="font-size: 8pt;">function</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> GetFusionWindow() {</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">var</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> curWindow = window;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">while</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> (!curWindow.Fusion) {</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">if</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> (curWindow.parent &amp;&amp; curWindow != curWindow.parent) {</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; curWindow = curWindow.parent;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; } </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">else</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">if</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> (curWindow.opener) {</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; curWindow = curWindow.opener;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; } </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">else</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> {</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alert(</span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #800000;">&#39;Could not find Fusion instance&#39;</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">break</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; }</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="line-height: 11pt;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span></span><span style="font-size: 8pt;"><span style="line-height: 11pt;"><span style="color: #0000ff;">return</span></span></span><span style="line-height: 11pt;"><span style="font-size: 8pt; color: #000000;"> curWindow;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">} </span></span></span></p>
</div>
<p>Hope this helps.</p>
