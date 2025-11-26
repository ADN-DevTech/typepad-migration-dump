---
layout: "post"
title: "Finding the points along a Polyline using ObjectARX, same concept in .NET too"
date: "2012-06-08 16:47:41"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/finding-the-points-along-a-polyline-using-objectarx-same-concept-in-net-too.html "
typepad_basename: "finding-the-points-along-a-polyline-using-objectarx-same-concept-in-net-too"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Any Curve derived object inside of AutoCAD uses Parameter values to define significant parts of that Curve. These Parameter values are defined as double values, ranging from 0.0 to n.</p>
<p>Lines define their Parameter values as 0.0 to Length. So a Line drawn from A to B would have a start parameter value of 0.0 and an end of Length – the midpoint, for instance, can be obtained by using :</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">double</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> </span><span><span style="color: #010001;">length</span></span><span style="color: #000000;"> = 0.0;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">line-&gt;</span></span></span><span style="font-size: 8pt;"><span><span style="color: #010001;">getEndParam</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">length</span></span><span style="color: #000000;">);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;"><span><span style="color: #010001;"><span style="font-size: 8pt;"><span><span style="color: #010001;"><span style="font-size: 8pt;">line-&gt;</span></span></span></span></span></span></span></span></span><span style="font-size: 8pt;"><span><span style="color: #010001;">getPointAtParam</span></span><span style="color: #000000;">(</span><span><span style="color: #010001;">length</span></span><span style="color: #000000;">/2.0);</span></span></span></p>
</div>
<p>Circles define their Parameter values as 0.0 to 2*PI, as does Arcs and Ellipses. So, by obtaining the start and end params you can very easily get quadrant points using the same technique.</p>
<p>Using the Parameter methods of a Polyline you can get any point on a polyline; the Parameter values work roughly the same as a Line, except the Parameter values only define the vertexes as indexes, no length values - e.g. p1=Param 0.0, p2=Param 1.0, p3=Param 2.0, etc.</p>
<p>There are quite a few functions which work to obtain a point from a Parameter value or visa versa (search the ARX Reference for *Param* to see them all)…</p>
<p>Here’s a few which I use a lot when processing Curve geometry…</p>
<p><strong>AcDbCurve::getParamAtPoint</strong> - get the parameter value at a given point, say the start point = vertex 3 and the end point = vertex 4.</p>
<div style="background: white;">
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// open a polyline and extract the vertex data from it</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// start and end points for instance.....</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">ads_real</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> </span><span><span style="color: #010001;">fromParam</span></span><span style="color: #000000;"> = 0.0; </span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// get the parameter value of vertex 3 start point </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">pPoly</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;">-&gt;</span><span><span style="color: #010001;">getParamAtPoint</span></span><span style="color: #000000;"> (</span><span><span style="color: #010001;">startPoint</span></span><span style="color: #000000;">, </span><span><span style="color: #010001;">fromParam</span></span><span style="color: #000000;">);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">ads_real</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> </span><span><span style="color: #010001;">toParam</span></span><span style="color: #000000;"> = 0.0; </span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// get the parameter value of vertex 4 start point </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">pPoly</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;">-&gt;</span><span><span style="color: #010001;">getParamAtPoint</span></span><span style="color: #000000;"> (</span><span><span style="color: #010001;">endPoint</span></span><span style="color: #000000;">, </span><span><span style="color: #010001;">toParam</span></span><span style="color: #000000;">);&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span><strong style="background-color: white;">AcDbCurve::getPointAtParam</strong><span style="background-color: white;"> - get a point using a parameter value. Using the fromParam and toParam above, we can obtain the midpoint between the startPoint (param 3) and the endPoint (param 4) like this</span></p>
</div>
<div style="background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span><span style="font-family: Consolas;"><span style="font-size: 8pt; color: #008000;">// get the mid point of the start and end points along the pline</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas;"><span><span style="color: #010001;"><span style="font-size: 8pt;">pPoly</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;">-&gt;</span><span><span style="color: #010001;">getPointAtParam</span></span><span style="color: #000000;"> (</span><span><span style="color: #010001;">fromParam</span></span><span style="color: #000000;"> + ((</span><span><span style="color: #010001;">toParam</span></span><span style="color: #000000;">-</span><span><span style="color: #010001;">fromParam</span></span><span style="color: #000000;">) / 2.0), </span><span><span style="color: #010001;">midPoint</span></span><span style="color: #000000;">);</span></span></span><span style="font-size: 8pt; font-family: Consolas; background-color: white;">&#0160;</span></p>
<div>
<p><strong>AcDbCurve::getDistAtParam</strong>&#0160;- get a distance from a Parameter value</p>
</div>
<p style="margin: 0px;">Now because the parameter values of a Polyline are not length related, but vertex related, then the midpoint parameter value of the entire Polyline, and then its actual midpoint could be obtained like this…</p>
<p style="margin: 0px;">&#0160;</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #0000ff;">double</span><span style="color: #000000;"> </span><span style="color: #010001;">startParam</span><span style="color: #000000;">=0.0, </span><span style="color: #010001;">endParam</span><span style="color: #000000;">=0.0, midParam=0.0, dist=0.0;</span></span></p>
<p style="margin: 0px;"><span style="font-size: 8pt;"><span style="font-family: Consolas;"><span style="color: #008000;">// always get the startParam, because it tends to be </span></span></span></p>
<p style="margin: 0px;"><span style="font-size: 8pt;"><span style="font-family: Consolas;"><span style="color: #008000;">// defined slightly differently across different curve types</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #010001;">pPoly</span><span style="color: #000000;">-&gt;</span><span style="color: #010001;">getStartParam</span><span style="color: #000000;">(</span><span style="color: #010001;">startParam</span><span style="color: #000000;">);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #010001;">pPoly</span><span style="color: #000000;">-&gt;</span><span style="color: #010001;">getEndParam</span><span style="color: #000000;">(</span><span style="color: #010001;">endParam</span><span style="color: #000000;">);</span></span></p>
<p style="margin: 0px;"><span style="font-size: 8pt;"><span style="font-family: Consolas;"><span style="color: #008000;">// now get the midpoint of the Polyline usi<span style="font-size: 8pt;">ng params</span></span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #010001;">AcGePoint3d</span><span style="color: #000000;"> </span><span style="color: #010001;">midPoint</span><span style="color: #000000;">;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #000000;">pPoly-&gt;getDistAtParam(endParam, dist);&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Consolas; font-size: 8pt;"><span style="color: #010001;">pPoly</span><span style="color: #000000;">-&gt;</span><span style="color: #010001;">getParamAtDist</span><span style="color: #000000;">(</span><span style="color: #010001;">dist/2.0, midParam</span><span style="color: #000000;">);</span></span></p>
<p style="margin: 0px;"><span style="font-size: 8pt;"><span style="font-family: Consolas; background-color: white; color: #010001;">pPoly</span><span style="font-family: Consolas; background-color: white;">-&gt;<span style="color: #010001;">getPointAtParam</span>(<span style="color: #010001;">midParam, midPoint</span>);</span></span></p>
<div><span style="font-size: 8pt; font-family: Consolas; background-color: white;">&#0160;</span></div>
</div>
</div>
