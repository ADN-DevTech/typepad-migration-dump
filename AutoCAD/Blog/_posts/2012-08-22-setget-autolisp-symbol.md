---
layout: "post"
title: "Set/Get AutoLISP symbol"
date: "2012-08-22 02:50:12"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/setget-autolisp-symbol.html "
typepad_basename: "setget-autolisp-symbol"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
You can use "acedPutSym" API to set the AutoLISP symbol and "acedGetSym" API to retrieves the value of a AutoLISP symbol.
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//set the MySymbol value</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf *rb = acutBuildList(RTSTR, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; _T(</span><span style="color: #a31515; line-height: 140%;">&quot;Unicode String&quot;</span><span style="line-height: 140%;">), RTNONE);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> res = acedPutSym(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;MySymbol&quot;</span><span style="line-height: 140%;">), rb); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutRelRb(rb); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//get back the text</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">rb = NULL; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">&nbsp; rc = acedGetSym(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;MySymbol&quot;</span><span style="line-height: 140%;">), &amp;rb); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;MySymbol value is %s\n&quot;</span><span style="line-height: 140%;">, rb-&gt;resval.rstring);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutRelRb(rb); </span></p>
</div>
