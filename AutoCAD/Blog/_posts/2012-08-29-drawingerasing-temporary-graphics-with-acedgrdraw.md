---
layout: "post"
title: "Drawing/erasing temporary graphics with acedGrDraw"
date: "2012-08-29 04:36:51"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/drawingerasing-temporary-graphics-with-acedgrdraw.html "
typepad_basename: "drawingerasing-temporary-graphics-with-acedgrdraw"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can use acedGrDraw to Drawing/erasing temporary graphics. below code shows drawing a color vector and eraing the same. Code also shows the drawing of XOR vector (by passing color as -1)</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> acedGrDrawTest(</span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> result = 0; </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//draw color line</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGrDraw (asDblArray (AcGePoint3d (0,0,0)), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray (AcGePoint3d (100,100,0)), 5, 0); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get user input </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetInt (_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPress to erase blue line&quot;</span><span style="line-height: 140%;">), &amp;result);</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now erase it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGrDraw (asDblArray (AcGePoint3d (0,0,0)), asDblArray </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (AcGePoint3d (100,100,0)), 0, 0); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get user input again</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetInt (_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPress to draw in XOR ink&quot;</span><span style="line-height: 140%;">), &amp;result); </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// create a line in -1 XOR ink&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGrDraw (asDblArray (AcGePoint3d (0,0,0)), asDblArray </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (AcGePoint3d (100,100,0)), -1, 0); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get user input again</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetInt (_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPress to erase XOR ink&quot;</span><span style="line-height: 140%;">), &amp;result); </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// erase XOR ink</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGrDraw (asDblArray (AcGePoint3d (0,0,0)), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray (AcGePoint3d (100,100,0)), -1, 0); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
</div>
