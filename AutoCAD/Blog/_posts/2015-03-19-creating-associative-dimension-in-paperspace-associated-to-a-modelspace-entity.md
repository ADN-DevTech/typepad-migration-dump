---
layout: "post"
title: "Creating associative dimension in paperspace associated to a modelspace entity"
date: "2015-03-19 03:30:26"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/creating-associative-dimension-in-paperspace-associated-to-a-modelspace-entity.html "
typepad_basename: "creating-associative-dimension-in-paperspace-associated-to-a-modelspace-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Most of the code in this blog post is from a code snippet that my colleague <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html">Philippe Leefsma</a> implemented. While his original code created an aligned dimension in paperspace, i have modified it slightly to create an ordinate dimension to cater to a recent developer request.</p>
<p>Here are the code snippets to create aligned and ordinate dimensions in paperspace while retaining their associativity with a reference point on an entity that is in modelspace.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">//For AcDbDimAssoc</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;dbdimassoc.h&quot;</span><span style="color:#000000">  </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">//For AcDbOsnapPointRef</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;dbdimptref.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  paperRotatedDimAssoc(<span style="color:#0000ff">void</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcDbDatabase* pDb </pre>
<pre style="margin:0em;"> 		= acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecordPointer ms(</pre>
<pre style="margin:0em;"> 		ACDB_MODEL_SPACE, pDb, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecordPointer ps(</pre>
<pre style="margin:0em;"> 		ACDB_PAPER_SPACE, pDb, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId lineId, vpId, dimId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d pt1(AcGePoint3d::kOrigin);</pre>
<pre style="margin:0em;"> 	AcGePoint3d	pt2(5,0,0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates a line</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbLine&gt; line;</pre>
<pre style="margin:0em;"> 		line.create();</pre>
<pre style="margin:0em;"> 		line-&gt;setStartPoint(pt1);</pre>
<pre style="margin:0em;"> 		line-&gt;setEndPoint(pt2);</pre>
<pre style="margin:0em;"> 		ms-&gt;appendAcDbEntity(lineId, line);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates a viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbViewport&gt; vp;</pre>
<pre style="margin:0em;"> 		vp.create();</pre>
<pre style="margin:0em;"> 		ps-&gt;appendAcDbEntity(vpId, vp);</pre>
<pre style="margin:0em;"> 		vp-&gt;setWidth(10);</pre>
<pre style="margin:0em;"> 		vp-&gt;setHeight(10);</pre>
<pre style="margin:0em;"> 		vp-&gt;setCenterPoint(AcGePoint3d(5,5,0));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		vp-&gt;setViewDirection(AcDb::kTopView);</pre>
<pre style="margin:0em;"> 		vp-&gt;setViewTarget(AcGePoint3d(2.5,0,0));</pre>
<pre style="margin:0em;"> 		vp-&gt;setViewCenter(AcGePoint2d(2.5,0));</pre>
<pre style="margin:0em;"> 		vp-&gt;setUnlocked();</pre>
<pre style="margin:0em;"> 		vp-&gt;setOn();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGeMatrix3d ms2ps(</pre>
<pre style="margin:0em;"> 			AcDbPointRef::mswcsToPswcs(vp));</pre>
<pre style="margin:0em;"> 		pt1 = pt1.transformBy(ms2ps);</pre>
<pre style="margin:0em;"> 		pt2 = pt2.transformBy(ms2ps);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates the dimension object</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbRotatedDimension&gt; dim;</pre>
<pre style="margin:0em;"> 		dim.create();</pre>
<pre style="margin:0em;"> 		dim-&gt;setXLine1Point(pt1);</pre>
<pre style="margin:0em;"> 		dim-&gt;setXLine2Point(pt2);</pre>
<pre style="margin:0em;"> 		dim-&gt;setDimLinePoint(AcGePoint3d(2,2,0));</pre>
<pre style="margin:0em;"> 		ps-&gt;appendAcDbEntity(dimId, dim);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectPointer&lt;AcDbDimAssoc&gt; assoc;</pre>
<pre style="margin:0em;"> 	assoc.create();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectIdArray arr(2);</pre>
<pre style="margin:0em;"> 	arr.append(vpId);</pre>
<pre style="margin:0em;"> 	arr.append(lineId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath path1(</pre>
<pre style="margin:0em;"> 		arr, </pre>
<pre style="margin:0em;"> 		AcDbSubentId(AcDb::kVertexSubentType, 0));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath path2(</pre>
<pre style="margin:0em;"> 		arr, </pre>
<pre style="margin:0em;"> 		AcDbSubentId(AcDb::kVertexSubentType, 1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d refPt1(pt1);</pre>
<pre style="margin:0em;"> 	AcDbOsnapPointRef *ref1 = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbOsnapPointRef(</pre>
<pre style="margin:0em;"> 		AcDbPointRef::kOsnapNear, &amp;path1, &amp;path1, &amp;refPt1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d refPt2(pt2);</pre>
<pre style="margin:0em;"> 	AcDbOsnapPointRef *ref2 = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbOsnapPointRef(</pre>
<pre style="margin:0em;"> 		AcDbPointRef::kOsnapNear, &amp;path2, &amp;path2, &amp;refPt2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	assoc-&gt;setDimObjId(dimId);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setTransSpatial(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	assoc-&gt;setPointRef(AcDbDimAssoc::kXline1Point, ref1);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setPointRef(AcDbDimAssoc::kXline2Point, ref2);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setAssocFlag(<span style="color:#0000ff">static_cast</span><span style="color:#000000"> &lt;<span style="color:#0000ff">int</span><span style="color:#000000"> &gt;(</pre>
<pre style="margin:0em;"> 		AcDbDimAssoc::kFirstPointRef</pre>
<pre style="margin:0em;"> 		|AcDbDimAssoc::kSecondPointRef));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	assoc-&gt;updateDimension();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId dimAssocId;</pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = acdbPostDimAssoc(</pre>
<pre style="margin:0em;"> 		dimId, assoc, dimAssocId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acutPrintf(_T(<span style="color:#a31515">&quot;\\npost dim: %s\\n&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		acadErrorStatusText(es));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (assoc.open(dimAssocId, AcDb::kForWrite)</pre>
<pre style="margin:0em;"> 		==Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		assoc-&gt;startCmdWatcher();</pre>
<pre style="margin:0em;"> 		assoc-&gt;addToPointRefReactor();</pre>
<pre style="margin:0em;"> 		assoc-&gt;addToDimensionReactor();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  paperOrdinateDimAssoc(<span style="color:#0000ff">void</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcDbDatabase* pDb </pre>
<pre style="margin:0em;"> 		= acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecordPointer ms(</pre>
<pre style="margin:0em;"> 		ACDB_MODEL_SPACE, pDb, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecordPointer ps(</pre>
<pre style="margin:0em;"> 		ACDB_PAPER_SPACE, pDb, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId lineId, vpId, dimId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d pt1(AcGePoint3d::kOrigin);</pre>
<pre style="margin:0em;"> 	AcGePoint3d	pt2(5,0,0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates a line</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbLine&gt; line;</pre>
<pre style="margin:0em;"> 		line.create();</pre>
<pre style="margin:0em;"> 		line-&gt;setStartPoint(pt1);</pre>
<pre style="margin:0em;"> 		line-&gt;setEndPoint(pt2);</pre>
<pre style="margin:0em;"> 		ms-&gt;appendAcDbEntity(lineId, line);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates a viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbViewport&gt; vp;</pre>
<pre style="margin:0em;"> 		vp.create();</pre>
<pre style="margin:0em;"> 		ps-&gt;appendAcDbEntity(vpId, vp);</pre>
<pre style="margin:0em;"> 		vp-&gt;setWidth(10);</pre>
<pre style="margin:0em;"> 		vp-&gt;setHeight(10);</pre>
<pre style="margin:0em;"> 		vp-&gt;setCenterPoint(AcGePoint3d(5,5,0));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		vp-&gt;setViewDirection(AcDb::kTopView);</pre>
<pre style="margin:0em;"> 		vp-&gt;setViewTarget(AcGePoint3d(2.5,0,0));</pre>
<pre style="margin:0em;"> 		vp-&gt;setViewCenter(AcGePoint2d(2.5,0));</pre>
<pre style="margin:0em;"> 		vp-&gt;setUnlocked();</pre>
<pre style="margin:0em;"> 		vp-&gt;setOn();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGeMatrix3d ms2ps(</pre>
<pre style="margin:0em;"> 			AcDbPointRef::mswcsToPswcs(vp));</pre>
<pre style="margin:0em;"> 		pt1 = pt1.transformBy(ms2ps);</pre>
<pre style="margin:0em;"> 		pt2 = pt2.transformBy(ms2ps);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">//creates the dimension object</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbOrdinateDimension&gt; dim;</pre>
<pre style="margin:0em;"> 		dim.create();</pre>
<pre style="margin:0em;"> 		dim-&gt;setUsingXAxis(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 		dim-&gt;setOrigin(pt1);</pre>
<pre style="margin:0em;"> 		dim-&gt;setDefiningPoint(pt2);</pre>
<pre style="margin:0em;"> 		dim-&gt;setLeaderEndPoint(pt2 </pre>
<pre style="margin:0em;"> 			+ AcGeVector3d(1.0, 0.0, 0.0));</pre>
<pre style="margin:0em;"> 		ps-&gt;appendAcDbEntity(dimId, dim);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectPointer&lt;AcDbDimAssoc&gt; assoc;</pre>
<pre style="margin:0em;"> 	assoc.create();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectIdArray arr(2);</pre>
<pre style="margin:0em;"> 	arr.append(vpId);</pre>
<pre style="margin:0em;"> 	arr.append(lineId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath path1(</pre>
<pre style="margin:0em;"> 		arr, </pre>
<pre style="margin:0em;"> 		AcDbSubentId(AcDb::kVertexSubentType, 0));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath path2(</pre>
<pre style="margin:0em;"> 		arr,</pre>
<pre style="margin:0em;"> 		AcDbSubentId(AcDb::kVertexSubentType, 1));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d refPt1(pt1);</pre>
<pre style="margin:0em;"> 	AcDbOsnapPointRef *ref1 = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbOsnapPointRef(</pre>
<pre style="margin:0em;"> 		AcDbPointRef::kOsnapNear, &amp;path1, &amp;path1, &amp;refPt1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d refPt2(pt2);</pre>
<pre style="margin:0em;"> 	AcDbOsnapPointRef *ref2 = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbOsnapPointRef(</pre>
<pre style="margin:0em;"> 		AcDbPointRef::kOsnapNear, &amp;path2, &amp;path2, &amp;refPt2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	assoc-&gt;setDimObjId(dimId);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setTransSpatial(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	assoc-&gt;setPointRef(AcDbDimAssoc::kOriginPoint, ref1);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setPointRef(AcDbDimAssoc::kDefiningPoint, ref2);</pre>
<pre style="margin:0em;"> 	assoc-&gt;setAssocFlag(<span style="color:#0000ff">static_cast</span><span style="color:#000000"> &lt;<span style="color:#0000ff">int</span><span style="color:#000000"> &gt;(</pre>
<pre style="margin:0em;"> 		AcDbDimAssoc::kFirstPointRef|</pre>
<pre style="margin:0em;"> 		AcDbDimAssoc::kSecondPointRef));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	assoc-&gt;updateDimension();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId dimAssocId;</pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = </pre>
<pre style="margin:0em;"> 		acdbPostDimAssoc</pre>
<pre style="margin:0em;"> 		(dimId, assoc, dimAssocId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acutPrintf(_T(<span style="color:#a31515">&quot;\\npost dim: %s\\n&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		acadErrorStatusText(es));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (assoc.open(dimAssocId, AcDb::kForWrite)==Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		assoc-&gt;startCmdWatcher();</pre>
<pre style="margin:0em;"> 		assoc-&gt;addToPointRefReactor();</pre>
<pre style="margin:0em;"> 		assoc-&gt;addToDimensionReactor();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
