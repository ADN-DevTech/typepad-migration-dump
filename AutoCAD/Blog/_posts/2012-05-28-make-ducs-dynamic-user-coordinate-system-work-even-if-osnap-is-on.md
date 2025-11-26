---
layout: "post"
title: "Make DUCS (Dynamic User Coordinate System) work even if OSNAP is ON"
date: "2012-05-28 04:58:22"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/make-ducs-dynamic-user-coordinate-system-work-even-if-osnap-is-on.html "
typepad_basename: "make-ducs-dynamic-user-coordinate-system-work-even-if-osnap-is-on"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have a custom entity (a polygon) and I enabled DUCS on it, which works fine when OSNAP is switched off. I can see that in case of e.g. a face of a solid entity the DUCS works even if OSNAP is on. How could I make DUCS work like that for my custom entity?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>If OSNAP succeeds at a certain cursor position, then AcDbDynamicUCSPE::getCandidatePlanes() will not be called so DUCS will not work.</p>
<p>As you said, in case of a face DUCS works even if OSNAP is on - but please note, that it only does so when the cursor moves off the edges and goes towards the center of the face. In other words DUCS works on a face even if OSNAP is on, when the cursor is at a position where OSNAP fails. The problem is that a polyline (or a polygon, i.e. closed polyline) does not have an area where OSNAP could fail, because it only consists of the lines but not the enclosed area.</p>
<p>So, the simplest solution is to draw your lines plus draw the enclosed area as well that OSNAP would be called on and could fail, so that getCandidatePlanes() would be called.</p>
<p>In the attached sample I have a custom entity derived from AcDbCircle - first it draws an empty circle and then a filled one:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::Boolean AsdkEntity::subWorldDraw (AcGiWorldDraw *mode) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; assertReadEnabled () ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mode-&gt;subEntityTraits().setSelectionMarker (1) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbCircle::subWorldDraw (mode);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mode-&gt;subEntityTraits ().setSelectionMarker (2) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mode-&gt;subEntityTraits ().setColor (1) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mode-&gt;subEntityTraits ().setFillType (kAcGiFillAlways) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; mode-&gt;geometry().circle (center (), radius (), normal()) ;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Then I make sure that if OSNAP is called on the filled circle (face/enclosed area) then it will fail:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus AsdkEntity::subGetOsnapPoints (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDb::OsnapMode osnapMode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> gsSelectionMark,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d &amp;pickPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d &amp;lastPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeMatrix3d &amp;viewXform,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcGePoint3dArray &amp;snapPoints,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbIntArray &amp;geomIds) </span><span style="color: blue; line-height: 140%;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; assertReadEnabled () ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// do not allow snapping on the internal circle/face</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (gsSelectionMark == 2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> Acad::eOk;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (AcDbCircle::subGetOsnapPoints (osnapMode, gsSelectionMark, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; pickPoint, lastPoint, viewXform, snapPoints, geomIds)) ;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b016766dde360970b"><a href="http://adndevblog.typepad.com/files/ducs.zip">Download Ducs</a></span></p>
