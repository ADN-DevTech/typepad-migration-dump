---
layout: "post"
title: "Creating a solid from a set of “water-tight” surfaces"
date: "2012-03-27 00:07:07"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/03/creating-a-solid-from-a-set-of-water-tight-surfaces.html "
typepad_basename: "creating-a-solid-from-a-set-of-water-tight-surfaces"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>A collection of surfaces that together form a closed volume and which do not have gaps, overlaps, such surfaces can be used to create a solid. Here is a sample code to do this using the "createSculptedSolid" method of the "AcDb3dSolid" class.</p>
<div style="font-family: Courier New; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">// surfaceOids : </span></p>
<p style="margin: 0px;"><span style="color: green;">// A collection of the objectId of the surfaces that </span></p>
<p style="margin: 0px;"><span style="color: green;">// form the bounds of the solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">Acad::ErrorStatus es;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">AcDb3dSolid *pSolid = <span style="color: blue;">new</span> AcDb3dSolid();</p>
<p style="margin: 0px;">pSolid-&gt;setDatabaseDefaults();</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">AcArray&lt;AcDbEntity*&gt; surfacesArray;</p>
<p style="margin: 0px;">AcGeIntArray limits;</p>
<p style="margin: 0px;">AcDbEntity *pSurfaceEnt;</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue;">for</span>(<span style="color: blue;">int</span> surfCnt = 0;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;surfCnt &lt; surfaceOids.size(); surfCnt++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; es = acdbOpenAcDbEntity(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pSurfaceEnt, surfaceOids[surfCnt],</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kForRead);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> ( es == Acad::eOk &amp;&amp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; pSurfaceEnt-&gt;isKindOf(AcDbSurface::desc()))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; surfacesArray.append(pSurfaceEnt);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">es = pSolid-&gt;createSculptedSolid(surfacesArray, limits);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span>(es == Acad::eOk)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; AcDbObjectId solidOid = AcDbObjectId::kNull;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; Add2Db(pSolid, solidOid);</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// To show the newly created solid away </span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// from the surfaces, we transform it..</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; AcDbEntity *pSolidEnt;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; es = acdbOpenAcDbEntity(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pSolidEnt, solidOid, AcDb::kForWrite);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span>&nbsp; (es == Acad::eOk &amp;&amp;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pSolidEnt-&gt;isKindOf(AcDb3dSolid::desc()))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcGeVector3d vec(15.0, 15.0, 0.0);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcGeMatrix3d transform =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; transform.setToTranslation(vec);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; es = pSolidEnt-&gt;transformBy(transform);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pSolidEnt-&gt;close();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutPrintf(ACRX_T(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <span style="color: #a31515;">"Solid3d created from surfaces !"</span>));</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; acutPrintf(ACRX_T(<span style="color: #a31515;">"Solid3d creation failed !"</span>));</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">else</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; acutPrintf(ACRX_T(<span style="color: #a31515;">"Solid3d creation failed !"</span>));</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; delete pSolid;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green;">// Close the surfaces</span></p>
<p style="margin: 0px;"><span style="color: blue;">int</span> surfCnt = 0;</p>
<p style="margin: 0px;"><span style="color: blue;">for</span>(; surfCnt &lt; surfacesArray.length(); surfCnt++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; pSurfaceEnt = surfacesArray[surfCnt];</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; pSurfaceEnt-&gt;close();</p>
<p style="margin: 0px;">}</p>
</div>
