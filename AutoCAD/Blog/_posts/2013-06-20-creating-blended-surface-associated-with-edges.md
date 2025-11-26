---
layout: "post"
title: "Creating blended surface associated with edges"
date: "2013-06-20 22:30:47"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/creating-blended-surface-associated-with-edges.html "
typepad_basename: "creating-blended-surface-associated-with-edges"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to create a blended surface that is associated with the edges from two other surfaces. The associativity ensures that the blended surface is suitably modified by AutoCAD when any of those surfaces are modified.</p>
<p>In this sample code, two extruded surfaces are created. The edge information from those surfaces are used to create the loft profile. The loft profiles are used to create a blended surface.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;dbextrudedsurf.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;dbBlendOptions.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;AcDbAssocVariable.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;AcDbAssocDependency.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;AcDbAssocPersSubentIdPE.h&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">#include &quot;acarray.h&quot;</span></p>
</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbDatabase *pDb </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = acdbHostApplicationServices()-&gt;workingDatabase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId surfaceId1 = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId surfaceId2 = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbSweepOptions sweepOptions;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create the first cylindrical surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d center1 = AcGePoint3d::kOrigin;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius1 = 10.0;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> height1 = 5.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbCircle *pCircle1 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbCircle(center1, AcGeVector3d::kZAxis, radius1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDb3dProfile circularProfile1(pCircle1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbExtrudedSurface *pExtrudedSurface1 = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = AcDbSurface::createExtrudedSurface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;circularProfile1, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcGeVector3d(0.0, 0.0, height1), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; sweepOptions, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pExtrudedSurface1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create the second cylindrical surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d center2(0.0, 0.0, 10.0);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius2 = 5.0;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> height2 = 5.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbCircle *pCircle2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbCircle(center2,&nbsp; AcGeVector3d::kZAxis, radius2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDb3dProfile circularProfile2(pCircle2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbExtrudedSurface *pExtrudedSurface2 = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = AcDbSurface::createExtrudedSurface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;circularProfile2, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcGeVector3d(0.0, 0.0, height2), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; sweepOptions, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pExtrudedSurface2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTable *pBlockTable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlockTableRecord *pMS = NULL;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pDb-&gt;getBlockTable(pBlockTable, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pBlockTable-&gt;getAt(ACDB_MODEL_SPACE, pMS, AcDb::kForWrite);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Add both the cylindrical surfaces to the database</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pMS-&gt;appendAcDbEntity(surfaceId1, pExtrudedSurface1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pMS-&gt;appendAcDbEntity(surfaceId2, pExtrudedSurface2);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pMS-&gt;close(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pBlockTable-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the Protocol extension associated with the </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// first cylindrical surface.</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// This will be used to fetch the edge information.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbAssocPersSubentIdPE* </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> pAssocPersSubentIdPE1 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = AcDbAssocPersSubentIdPE::cast(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pExtrudedSurface1-&gt;queryX(AcDbAssocPersSubentIdPE::desc()));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">( pAssocPersSubentIdPE1 == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get all the edge subentities from the first cylindrical surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcDbSubentId&gt; edgeSubentIds1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pAssocPersSubentIdPE1-&gt;getAllSubentities</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pExtrudedSurface1, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kEdgeSubentType, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; edgeSubentIds1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the edge subent for the edge that will be used for </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// the blended surfaces.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbFullSubentPath path1(surfaceId1, edgeSubentIds1[0]);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create a loft profile using the edge</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEdgeRef edgeRef1(path1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcDbEdgeRef&gt; edgeArray1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">edgeArray1.append(edgeRef1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbPathRef pathRef1(edgeArray1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbLoftProfile startProfile(pathRef1);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the Protocol extension associated with the </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// second cylindrical surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbAssocPersSubentIdPE* </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> pAssocPersSubentIdPE2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = AcDbAssocPersSubentIdPE::cast(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pExtrudedSurface2-&gt;queryX(AcDbAssocPersSubentIdPE::desc()));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">( pAssocPersSubentIdPE2 == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get all the edge subentities from the second cylindrical surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcDbSubentId&gt; edgeSubentIds2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pAssocPersSubentIdPE2-&gt;getAllSubentities</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pExtrudedSurface2, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kEdgeSubentType, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; edgeSubentIds2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the edge subent for the edge that will be used for</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// the blended surfaces.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbFullSubentPath path2(surfaceId2, edgeSubentIds2[1]);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create a loft profile using the edge</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEdgeRef edgeRef2(path2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcArray&lt;AcDbEdgeRef&gt; edgeArray2;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">edgeArray2.append(edgeRef2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbPathRef pathRef2(edgeArray2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbLoftProfile endProfile(pathRef2);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pExtrudedSurface1-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pExtrudedSurface2-&gt;close(); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create the blended surface using the loft profiles</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Ensure that we create an associative blended surface.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbBlendOptions blendOptions;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId blendSurfaceId = AcDbObjectId::kNull;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbSurface *pBlendSurface = NULL;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = AcDbSurface::createBlendSurface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;startProfile, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;endProfile, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &amp;blendOptions, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; blendSurfaceId</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ); </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(es == Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Created blended surface !&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
</p>
<p>The cylindrical surfaces and the blended surface appear as shown in this image :</p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191039e99c5970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0191039e99c5970c" alt="BlendedSurface" title="BlendedSurface" src="/assets/image_441727.jpg" style="margin: 0px 5px 5px 0px;" /></a>
