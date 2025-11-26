---
layout: "post"
title: "Enable UCS >> OBject for custom entity"
date: "2012-07-16 03:47:33"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/enable-ucs-object-for-custom-entity.html "
typepad_basename: "enable-ucs-object-for-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Depending on what your entity is derived from, UCS &gt;&gt; OBject tries to get the UCS from the entity in different ways. If your entity is derived from AcDbEntity and it does not provide a NEAR snap point through getOsnapPoints(), then getEcs() will be called.</p>
<p>One thing to note is that the origin will be further transformed, which we can counter act so that if _ecs in the below example contains the exact coordinate system that we want, we can transform it before passing it back:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MyEnt::getEcs(AcGeMatrix3d&amp; retVal) </span><span style="color: blue; line-height: 140%;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; assertReadEnabled();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// UCS &gt;&gt; OBject will do an </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// orig.transformBy(AcGeMatrix3d::worldToPlane(normal));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// so we&#39;ll do here the reverse</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGeVector3d x, y, z;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGePoint3d orig;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 15px;">&#0160; _ecs.getCoordSystem(orig, x, y, z);</span></p>
<p style="margin: 0px;"><span style="color: #0000ff;"><span style="line-height: 15px;"><br /></span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; orig = orig.transformBy(AcGeMatrix3d::worldToPlane(z).invert());&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcGeMatrix3d mx;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; retVal = mx.setCoordSystem(orig, x, y, z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
