---
layout: "post"
title: "Equivalent to ObjectARX acdbDistF() in AutoCAD.NET"
date: "2013-02-07 09:59:40"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/equivalent-to-objectarx-acdbdistf-in-autocadnet.html "
typepad_basename: "equivalent-to-objectarx-acdbdistf-in-autocadnet"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>I just had one of those frustrating moments where I couldn’t find a function I wanted in .NET <img class="wlEmoticon wlEmoticon-smile" style="border-top-style: none; border-left-style: none; border-bottom-style: none; border-right-style: none" alt="Smile" src="/assets/image_104629.jpg" /> I knew what it was in ObjectARX, but not in .NET… After what seemed hours (5 mins) I ended up searching the AutoCAD source and found it. Anyway, I thought I’d post it here for you guys (and Google) as a reference.</p>  <p>If you want to convert a decimal value to a string <a href="http://www.youtube.com/watch?v=EX18XKDJWTc">UNITS</a>, then in ObjectARX the function is:</p>  <pre class="Element100"><span style="color: #871f78">int</span> <span style="color: #660000">acdbDisToF</span>(<span style="color: #871f78">const</span> ACHAR * <span style="color: #660000">str</span>, <span style="color: #871f78">int</span> <span style="color: #660000"><font color="#000000">unit</font></span>, ads_real * <span style="color: #660000">v</span>);</pre>

<p>the equivalent function in AutoCAD.NET is</p>

<p><font color="#800000" face="Courier New">Autodesk.AutoCAD.Runtime.Converter.DistanceToString()</font></p>

<pre class="Element100"><p>&#160;</p></pre>
