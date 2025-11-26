---
layout: "post"
title: "Zooming entities using acedCommand in ObjectARX"
date: "2012-05-16 04:23:29"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/zooming-entities-using-acedcommand-in-objectarx.html "
typepad_basename: "zooming-entities-using-acedcommand-in-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Few days back Fenton Webb showed me the below ObjectARX way of zooming the selected entities.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> zoomTest()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ads_name ss;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> err = acedSSGet(NULL, NULL, NULL, NULL, ss);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (err != RTNORM) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedCommand(RTSTR, _T(</span><span style="color: #a31515; line-height: 140%;">&quot;_.zoom&quot;</span><span style="line-height: 140%;">), RTSTR, _T(</span><span style="color: #a31515; line-height: 140%;">&quot;_Object&quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; RTPICKS, ss, RTSTR, _T(</span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span><span style="line-height: 140%;">), RTNONE);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedSSFree( ss );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
