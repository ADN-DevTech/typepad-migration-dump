---
layout: "post"
title: "Converting string to double in ObjectARX"
date: "2012-07-24 04:54:35"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/converting-string-to-double-in-objectarx.html "
typepad_basename: "converting-string-to-double-in-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>In ObjectARX, you can use acdbDisToF API to convert the string value to a double value. This API also takes different format string input (Decimal/Engineering/Architectural/Fractional) as shown in below code and converts the string to double value.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> convertStringToDouble()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//-1 to use current database units</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> unit = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// 5 is pr</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> value = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;15.0&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;double is %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for Scientific use 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; unit = 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;1.5000000000E+01&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Scientific : %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for Decimal&nbsp; use 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; unit = 2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;15.0000000000&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Decimal&nbsp; : %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for Engineering&nbsp;&nbsp; use 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; unit = 3;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;1'-3.0000000000\&quot;&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Engineering&nbsp; : %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for Architectural use 4</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; unit = 4;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;1'-3\&quot;&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Architectural&nbsp; : %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for Fractional use 5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; unit = 5;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbDisToF(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;15&quot;</span><span style="line-height: 140%;">), unit, &amp;value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Fractional&nbsp;&nbsp; : %f\n&quot;</span><span style="line-height: 140%;">), value);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
