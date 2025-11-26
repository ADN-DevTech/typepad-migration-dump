---
layout: "post"
title: "How to calculate the length of an entity in ObjectArx/.NET"
date: "2012-05-16 17:40:50"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/how-to-calculate-the-length-of-an-entity-in-objectarxnet.html "
typepad_basename: "how-to-calculate-the-length-of-an-entity-in-objectarxnet"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>
<p>You can use the getStartParam() and getEndParam() to obtain the start and end parameters of the Curve based entity and then use GetDistanceAtParameter() method to get the length of the Spline. Please find the code below which will calculate the Length of the Spline.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">Acad::ErrorStatus es;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">ads_name ename;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">ads_point pt;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span>if</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;"> (RTNORM != acedEntSel(NULL, ename, pt))</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">{</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span style="line-height: 11pt;"><span style="color: #0000ff;">return</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">}</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">AcDbObjectId objid;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">acdbGetObjectId(objid, ename);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">AcDbCurve* pEnt;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">acdbOpenObject(pEnt, objid, AcDb::kForRead);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #0000ff;"><span>double</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;"> startParam, endParam, startDist, endDist;</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">es = pEnt-&gt;getStartParam(startParam);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">es = pEnt-&gt;getEndParam(endParam);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">es = pEnt-&gt;getDistAtParam(startParam, startDist);</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">es = pEnt-&gt;getDistAtParam(endParam, endDist);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>acutPrintf(L</span></span></span><span><span style="line-height: 11pt;"><span style="color: #a31515;">&quot;\nLength = %f&quot;</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">, endDist - startDist);</span></span></span></p>
</div>
<p>Also in .Net, you can use the something like this to get the entity length.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">a = curve.StartParam</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">b = curve.EndParam</span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">length = curve.GetDistanceAtParameter(b) - </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; curve.GetDistanceAtParameter(a)</span></span></span></p>
</div>
