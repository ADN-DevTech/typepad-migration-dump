---
layout: "post"
title: "Disable Auto save option through .NET/ObjectARX"
date: "2012-06-04 05:35:08"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/disable-auto-save-option-through-netobjectarx.html "
typepad_basename: "disable-auto-save-option-through-netobjectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Set the system variable “SAVETIME” to 0 to disable the Auto save option in AutoCAD as shown below.</p>
<p>.NET</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;SAVETIME&quot;</span><span style="line-height: 140%;">, 0);</span></p>
</div>
<p>ObjectARX</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf res; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">res.restype=RTSHORT; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">res.resval.rint=0; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedSetVar(L</span><span style="color: #a31515; line-height: 140%;">&quot;SAVETIME&quot;</span><span style="line-height: 140%;">,&amp;res); </span></p>
</div>
