---
layout: "post"
title: "Drawing graphics from block table record in a custom entity's worlddraw"
date: "2012-07-26 02:30:00"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/drawing-graphics-from-block-table-record-in-a-custom-entitys-worlddraw.html "
typepad_basename: "drawing-graphics-from-block-table-record-in-a-custom-entitys-worlddraw"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>You can use pushModelTransform() method to transform the graphics that is drawn at the desired location. The following psuedo code shows how to draw graphics from a block table record.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//Call this function in the subWorldDraw() </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//override of your custom entity</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Implement a getBlockTableRecordId function that returns</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// the id of the block table record you wish to draw</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId btrId = getBlockTableRecordId();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecord *pBtr=NULL; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenObject(pBtr,btrId,AcDb::kForRead)==Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//Define you matrix here, currently set to translation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGeMatrix3d mat; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; mat.setToTranslation(AcGeVector3d(10,10,0));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; mode-&gt;geometry().pushModelTransform(mat); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; mode-&gt;geometry().draw(pBtr); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// pop the transform matrix</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; mode-&gt;geometry().popModelTransform(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pBtr-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
