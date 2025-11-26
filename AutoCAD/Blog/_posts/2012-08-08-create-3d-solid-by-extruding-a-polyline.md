---
layout: "post"
title: "Create 3D solid by extruding a polyline"
date: "2012-08-08 06:16:55"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/create-3d-solid-by-extruding-a-polyline.html "
typepad_basename: "create-3d-solid-by-extruding-a-polyline"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>3D solid entity can be extruded by region object. So we can create a temporary region according to the boundary information of the existing polyline object.Here we explode the polyline to get the boundary curves, and create a temporary region with these boundary curves.</p>
<p>Please see the code snippet below as a reference.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateExtrude()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;ads_name polyName;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;ads_point ptres;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//select the polyline</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">( acedEntSel(L</span><span style="color: #a31515; line-height: 140%;">&quot;Please select a polyline&quot;</span><span style="line-height: 140%;">, polyName, ptres) != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;Failed to select a polyline&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">//get the boundary curves of the polyline</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbObjectId idPoly;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;acdbGetObjectId(idPoly, polyName);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbEntity *pEntity = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenAcDbEntity(pEntity, idPoly, AcDb::kForRead) != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbPolyline *pPoly = AcDbPolyline::cast(pEntity);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pPoly == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; pEntity-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbVoidPtrArray lines;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPoly-&gt;explode(lines);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;pPoly-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">// Create a region from the set of lines.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbVoidPtrArray regions;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;es&nbsp; =&nbsp; AcDbRegion::createFromCurves(lines, regions);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(Acad::eOk != es)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; pPoly-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nFailed to create region\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbRegion *pRegion = AcDbRegion::cast((AcRxObject*)regions[0]);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: green; line-height: 140%;">// Extrude the region to create a solid.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDb3dSolid *pSolid = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDb3dSolid();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;es&nbsp; =&nbsp; pSolid-&gt;extrude(pRegion, 10.0, 0.0);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; lines.length(); i++) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> (AcRxObject*)lines[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ii = 0; ii &lt; regions.length(); ii++) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> (AcRxObject*)regions[ii];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;AcDbObjectId savedExtrusionId = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(Acad::eOk == es)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDatabase *pDb = curDoc()-&gt;database();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId modelId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; modelId = acdbSymUtil()-&gt;blockModelSpaceId(pDb);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbBlockTableRecord *pBlockTableRecord;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acdbOpenAcDbObject((AcDbObject*&amp;)pBlockTableRecord, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; modelId, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pBlockTableRecord-&gt;appendAcDbEntity(pSolid);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pBlockTableRecord-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pSolid-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;</span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pSolid;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
