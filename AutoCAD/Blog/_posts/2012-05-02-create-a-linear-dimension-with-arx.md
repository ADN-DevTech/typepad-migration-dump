---
layout: "post"
title: "Create a Linear Dimension with ARX"
date: "2012-05-02 04:50:50"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/create-a-linear-dimension-with-arx.html "
typepad_basename: "create-a-linear-dimension-with-arx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>

<p>Below code shows the procedure to create linear dimension with ObjectARX</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus postToDatabase (AcDbEntity *pEnt) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbObjectId idObj;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbBlockTable *pBlockTable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbBlockTableRecord *pSpaceRecord;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbDatabase *pDb = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; es = pDb-&gt;getBlockTable(pBlockTable, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kForRead);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (es == Acad::eOk ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; es = pBlockTable-&gt;getAt(ACDB_MODEL_SPACE, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pSpaceRecord, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> ( es == Acad::eOk ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; es = pSpaceRecord-&gt;appendAcDbEntity (idObj, pEnt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pSpaceRecord-&gt;close ();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pBlockTable-&gt;close ();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> es;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateLinDim()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcGePoint3d pnt1, pnt2;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// pick start point</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acedGetPoint(NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nPick bottom left point &quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; asDblArray(pnt1));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; acedGetPoint(asDblArray(pnt1), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; L</span><span style="color: #a31515; line-height: 140%;">&quot;\nPick top right point &quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; asDblArray(pnt2));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// now create our dimensions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbRotatedDimension *dimH = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbRotatedDimension(0.0, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pnt1, pnt2, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pnt1 - AcGeVector3d&nbsp; (0, 10, 0));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; AcDbRotatedDimension *dimV = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbRotatedDimension((4*atan(1.0))/2.0, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pnt1, pnt2, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pnt1 - AcGeVector3d (10, 0, 0));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// add to model space</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; postToDatabase (dimH);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; postToDatabase (dimV);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: green; line-height: 140%;">// close after use</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; dimH-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; dimV-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
