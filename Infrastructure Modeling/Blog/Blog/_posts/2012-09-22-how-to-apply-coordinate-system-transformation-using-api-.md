---
layout: "post"
title: "How to apply Coordinate system Transformation using API ?"
date: "2012-09-22 04:30:35"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/how-to-apply-coordinate-system-transformation-using-api-.html "
typepad_basename: "how-to-apply-coordinate-system-transformation-using-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Coordinate transformation is converting a coordinate value in one coordinate system into its equivalent in another coordinate system.</p>
<p>&#0160;</p>
<p>Here is a code snippet which demonstrates how to apply Coordinate system Transformation :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> mapSRS = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap().GetMapSRS();</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// create MgCoordinateSystemFactory</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemFactory</span><span style="line-height: 140%;"> coordSysFactory = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemFactory</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystem</span><span style="line-height: 140%;"> coordSys = coordSysFactory.Create(mapSRS);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystem</span><span style="line-height: 140%;"> wgs84Sys = coordSysFactory.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;LL84&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemTransform</span><span style="line-height: 140%;"> transform = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">transform = coordSysFactory.GetTransform(wgs84Sys, coordSys);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinate</span><span style="line-height: 140%;"> coord = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (transform != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> &amp;&amp; !</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;">.IsNaN(pnt.Z))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; coord = transform.Transform(pnt.Z, pnt.Y, pnt.Z);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; coord = transform.Transform(pnt.X, pnt.Y);&#0160; &#0160; </span></p>
</div>
