---
layout: "post"
title: "Disable osnap points for entities residing in block drawn by custom entity"
date: "2012-07-30 05:45:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/disable-osnap-points-for-entities-residing-in-block-drawn-by-custom-entity.html "
typepad_basename: "disable-osnap-points-for-entities-residing-in-block-drawn-by-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>We have a custom entity derived from AcDbEntity that draws a given block table record:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::Boolean MyBlockEnt::subWorldDraw (AcGiWorldDraw *mode) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; assertReadEnabled();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!m_blockId.isNull())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AcDbBlockTableRecordPointer ptrMB(m_blockId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; mode-&gt;geometry().draw(ptrMB);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> (Adesk::kTrue);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>We also implement the subGetOsnapPoints function to provide our own snap points:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus MyBlockEnt::subGetOsnapPoints(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDb::OsnapMode osnapMode, Adesk::GsMarker gsSelectionMark,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; pickPoint, </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGePoint3d&amp; lastPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeMatrix3d&amp; viewXform, AcGePoint3dArray&amp; snapPoints,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; AcDbIntArray&amp; geomIds, </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> AcGeMatrix3d&amp; insertionMat) </span><span style="color: blue; line-height: 140%;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; snapPoints.append(m_snapPoint);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> Acad::eOk; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>In 2d Wireframe mode only the point we provide can be snapped to, which is the behaviour we&#39;d like to see in the other modes like Shaded view as well. However, there the osnap points of the entities residing in the block our entity is drawing can also be snapped to. How could we avoid that?</p>
<p><strong>Solution</strong></p>
<p>Your entity should also implement the AcDbEntity::subIsContentSnappable like so:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> MyBlockEnt::subIsContentSnappable() </span><span style="color: blue; line-height: 140%;">const</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
