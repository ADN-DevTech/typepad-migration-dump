---
layout: "post"
title: "Setting polygonal clip boundary for a raster image"
date: "2012-09-09 08:11:35"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/setting-polygonal-clip-boundary-for-a-raster-image.html "
typepad_basename: "setting-polygonal-clip-boundary-for-a-raster-image"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p><p>Here is a sample ObjectARX code to set the clip boundary of a raster image based on a selected polyline.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Select a closed polyline that defines the clip boundary</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name name2; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt2; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> nReturn = acedEntSel(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a closed polyline\n&quot;</span><span style="line-height: 140%;">), name2, pt2); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(nReturn != RTNORM) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//get the object Id of the entity </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId Id2; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbGetObjectId(Id2, name2) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//open the selected entity </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity *pEntity2 = NULL; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenAcDbEntity(pEntity2, Id2, AcDb::kForRead) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbPolyline *pPline = AcDbPolyline::cast(pEntity2); </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pPline == NULL) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbExtents exts;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pPline-&gt;bounds(exts); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d minPt = exts.minPoint();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d maxPt = exts.maxPoint();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d vecx = (maxPt.x - minPt.x) * AcGeVector3d::kXAxis;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeVector3d vecy = (maxPt.y - minPt.y) * AcGeVector3d::kYAxis;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint2dArray ptRect; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> numVerts = pPline-&gt;numVerts();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; numVerts; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; AcGePoint2d pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pPline-&gt;getPointAt(i, pt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ptRect.append( pt); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint2d ptBase( ptRect[0] ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ptRect.append( ptBase );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Select a raster image</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name name1; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt3; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">nReturn = acedEntSel(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a raster image\n&quot;</span><span style="line-height: 140%;">), name1, pt3); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(nReturn != RTNORM) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//get the object Id of the entity </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId Id1; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbGetObjectId(Id1, name1) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//open the selected entity </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity *pEntity1 = NULL; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(acdbOpenAcDbEntity(pEntity1, Id1, AcDb::kForWrite) != Acad::eOk) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbRasterImage *pImage = AcDbRasterImage::cast(pEntity1); </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(pImage == NULL) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Set the clip boundary of the raster image</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeMatrix3d mat; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;getPixelToModelTransform(mat); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">mat = mat.inverse(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i=0;i &lt; ptRect.length();i++ ) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcGePoint3d pt = AcGePoint3d( ptRect[i].x, ptRect[i].y,0.0 ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pt = mat*pt;&nbsp;&nbsp;&nbsp; &nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ptRect.setAt(i, AcGePoint2d(pt.x, pt.y)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;setClipBoundary( AcDbRasterImage::kPoly, ptRect ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;setDisplayOpt( AcDbRasterImage::kShow, Adesk::kTrue ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;setDisplayOpt( AcDbRasterImage::kClip, Adesk::kTrue ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;setDisplayOpt( AcDbRasterImage::kShowUnAligned, Adesk::kTrue ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;setDisplayOpt( AcDbRasterImage::kTransparent, Adesk::kTrue ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pImage-&gt;close(); </span></p>
</div>
<p></p>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c31c01e20970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b017c31c01e20970b image-full" alt="1" title="1" src="/assets/image_257245.jpg" border="0" /></a>
