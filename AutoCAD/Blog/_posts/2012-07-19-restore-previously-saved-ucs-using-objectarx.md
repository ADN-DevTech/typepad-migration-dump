---
layout: "post"
title: "Restore Previously Saved UCS Using ObjectARX"
date: "2012-07-19 06:01:28"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/restore-previously-saved-ucs-using-objectarx.html "
typepad_basename: "restore-previously-saved-ucs-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The ObjectARX function, acedSetCurrentUCS(), takes an AcGeMatrix3d. This matrix specifies the information about the UCS to set (origin, x-, y- and z-direction). All you need to do is to get this matrix from a named UCS.</p>
<p>The code below does this task:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ACHAR name[133];</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the UCS name to restore</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedGetString(0, ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nUCS to restore: &quot;</span><span style="line-height: 140%;">), name))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbDatabase *pDb </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbUCSTable *pTable = NULL;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Acad::eOk != pDb-&gt;getUCSTable(pTable, AcDb::kForRead))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the UCS table record knowing its name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbUCSTableRecord *pUCS = NULL;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Acad::eOk != pTable-&gt;getAt(name, pUCS, AcDb::kForRead)) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCannot get UCS '%s'.&quot;</span><span style="line-height: 140%;">), name); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pTable-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pTable-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the UCS parameters</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d origin = pUCS-&gt;origin();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d xDirection = pUCS-&gt;xAxis();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d yDirection = pUCS-&gt;yAxis();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d zDirection = xDirection.crossProduct(yDirection);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pUCS-&gt;close();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create the matrix for the UCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeMatrix3d matrix;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">matrix.setCoordSystem(origin, xDirection, yDirection, zDirection);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Activate the UCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedSetCurrentUCS(matrix);</span></p>
</div><p>Note : This solution does not change the UCS name that is displayed by AutoCAD using the UCSNAME system variable.</p>
