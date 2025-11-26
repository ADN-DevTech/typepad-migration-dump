---
layout: "post"
title: "Zooming to drawing extents for a viewport without sending commands"
date: "2012-10-25 23:12:02"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/zooming-to-drawing-extents-for-a-viewport-without-sending-commands.html "
typepad_basename: "zooming-to-drawing-extents-for-a-viewport-without-sending-commands"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>How to do zoom drawing extents for a viewport without sending commands?</p>
<!--stopindex-->
<div><a name="section2"> </a><!--startindex-->
<div><strong>Solution</strong></div>
<p>For every viewport, you need three things to zoom correctly. </p>
<p> 1. Its View center ( using setViewCenter( ) ) - and not its center point. View center is the 2D point that you center your view on, while center point (using setCenterPoint( ) ) is the viewport's center point in PaperSpace Layout. Center point is simply used to position viewports in a layout. The view center would depend on the drawing's extent.</p>
<p>2. View Direction.</p>
<p>3. View's height, and not the Viewport's height. Viewport's width and height would apply to paper space. The width and height once again will depend on draiwing's extent. </p>
<p>The following code creates 3 layouts that is zoomed to drawing's extents programatically. They are placed arbitratily in paper space layout, but you may set your own center point and view's height. The view's width is set in the code to be proportional the extent's windows width:height ratio. If you don't do this, the layout won't be wide enough to accomodate the whole drawing although it may be tall enough!</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AdskTestCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDatabase *pDbUse </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbHostApplicationServices()-&gt;workingDatabase(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDictionary *pNOD; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Acad::ErrorStatus eStat;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; eStat = pDbUse-&gt;getNamedObjectsDictionary(pNOD, AcDb::kForRead); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (eStat != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId idDict; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; eStat = pNOD-&gt;getAt(L</span><span style="color: #a31515; line-height: 140%;">&quot;ACAD_LAYOUT&quot;</span><span style="line-height: 140%;">, idDict); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pNOD-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (eStat != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDictionary *pDict; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; eStat = acdbOpenObject(pDict, idDict, AcDb::kForRead); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (eStat != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId idLyt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; eStat = pDict-&gt;getAt(L</span><span style="color: #a31515; line-height: 140%;">&quot;Layout1&quot;</span><span style="line-height: 140%;">, idLyt); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pDict-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcApLayoutManager *pLayM </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; =(AcApLayoutManager*)acdbHostApplicationServices()-&gt;layoutManager(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLayM-&gt;setCurrentLayout(L</span><span style="color: #a31515; line-height: 140%;">&quot;Layout1&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbLayout* pLyt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (acdbOpenObject(pLyt, idLyt, AcDb::kForRead) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbObjectId idBtr = pLyt-&gt;getBlockTableRecordId(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pLyt-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AddViewPort(idBtr, AcGePoint3d(2,5,0), AcGeVector3d( 0,0,1)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for sw-iso </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AddViewPort(idBtr, AcGePoint3d(5,5,0), AcGeVector3d( -1,-1,1)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//for ne-iso </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AddViewPort(idBtr, AcGePoint3d(8,5,0), AcGeVector3d( 1,1,1));&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Gets a rectangular window in viewport's x-y plane </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// that represents drawing's extents</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> getUcsExts(AcGePoint2d &amp;maxExt, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGePoint2d &amp;minExt, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGeMatrix3d UcsMat ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDatabase* pDb </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbHostApplicationServices()-&gt;workingDatabase(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGePoint3d max = pDb-&gt;extmax(), min = pDb-&gt;extmin(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Make extents box</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbExtents ext(min, max);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Transform extents to UCS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ext.transformBy(UcsMat);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; max = ext.maxPoint();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; min = ext.minPoint();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; maxExt[X] = max[X];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; maxExt[Y] = max[Y];</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; minExt[X] = min[X];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; minExt[Y] = min[Y];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> SetViewportExtents(AcDbViewport *pVp, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; AcGePoint2d &amp;max, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; AcGePoint2d &amp;min,&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> height) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Set View center</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGePoint2d ViewCenter;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ViewCenter[X] = (max[X]+min[X])/2.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ViewCenter[Y] = (max[Y]+min[Y])/2.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pVp-&gt;setViewCenter(ViewCenter);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//set View's height + 0.5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pVp-&gt;setViewHeight((max[Y]-min[Y])+0.5);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Get width proportional to height</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> WidthHeightRatio = (max[X]-min[X])/(max[Y]-min[Y]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pVp-&gt;setHeight(height);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//The viewport's width maintains the extent windows proportions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pVp-&gt;setWidth(height * WidthHeightRatio);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get WCS to UCS transformation matrix</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> getTrnsMatrix (AcGeMatrix3d &amp;ucsMatrix, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; AcGeVector3d ViewDirection, AcGePoint3d origin) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGePlane XYPlane(origin, ViewDirection);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ucsMatrix.setToWorldToPlane(XYPlane);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AddViewPort(AcDbObjectId idToBtr, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; AcGePoint3d centerPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp; AcGeVector3d vecVPoint) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbDatabase* pDb </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; = acdbHostApplicationServices()-&gt;workingDatabase(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbViewport* pVp = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbViewport; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Append new viewport to paper space </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcDbBlockTableRecord *pBTR; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (acdbOpenObject(pBTR, idToBtr, AcDb::kForWrite) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nCannot access paper space.&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pVp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pBTR-&gt;appendAcDbEntity( pVp) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; { </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nCannot append viewport to paper space.&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; pBTR-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pVp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; } </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; pBTR-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; pVp-&gt;setCenterPoint(centerPoint); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//Set View direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; pVp-&gt;setViewDirection(vecVPoint); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//Assume target point is WCS (0,0,0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; AcGeMatrix3d ucsMatrix;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; getTrnsMatrix (ucsMatrix, vecVPoint,AcGePoint3d(0,0,0));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Get a rectangular window in viewport's x-y plane </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// that represents drawing's extents</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; AcGePoint2d maxExt, minExt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; getUcsExts(maxExt, minExt, ucsMatrix);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Here 2 is the view's height. You may change it to any height</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; SetViewportExtents(pVp, maxExt, minExt, 2); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; pVp-&gt;setOn(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; pVp-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
</div>
