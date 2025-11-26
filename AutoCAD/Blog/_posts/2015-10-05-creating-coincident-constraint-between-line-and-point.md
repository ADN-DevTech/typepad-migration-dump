---
layout: "post"
title: "Creating coincident constraint between Line and Point"
date: "2015-10-05 02:55:11"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/10/creating-coincident-constraint-between-line-and-point.html "
typepad_basename: "creating-coincident-constraint-between-line-and-point"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The <a href="http://adndevblog.typepad.com/autocad/2013/01/a-simplified-net-api-for-accessing-autocad-parameters-and-constraints.html">High-Level API wrapper</a> for creating constraints was created by my colleagues <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html">Philippe Leefsma</a> and <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a> which simplifies the creation of constraints using the API. In a recent query, a developer reported that the library was not helping create a coincident constraint between a Line and a Point. After debugging through it, what was causing it to fail is that the Constraints library was created in a generic way that did not consider that the Point entity did not have edge sub-entities for it. Also, when creating a coincident constraint between two entities and one of them being a Point entity, the subentity path of the vertex is required to be added to the constraint group before the constrained geometry can be created.</p>
<p>Here are the changes to Constraints library :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus AcDbAssoc2dConstraintAPI</pre>
<pre style="margin:0em;"> 		::createCoincidentConstraint(</pre>
<pre style="margin:0em;"> 	AcDbObjectId&amp; entId1, </pre>
<pre style="margin:0em;"> 	AcDbObjectId&amp; entId2, </pre>
<pre style="margin:0em;"> 	AcGePoint3d&amp; ptEnt1, </pre>
<pre style="margin:0em;"> 	AcGePoint3d&amp; ptEnt2)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = Acad::eOk;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = AcDbAssocManager::initialize()) != Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPathArray aPaths;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath edgeEntPath1;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (entId1.objectClass() != AcDbPoint::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 			::getClosestEdgeSubEntPath(</pre>
<pre style="margin:0em;"> 			entId1, ptEnt1, </pre>
<pre style="margin:0em;"> 			edgeEntPath1)) != Acad::eOk)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 		aPaths.append(edgeEntPath1); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcDbFullSubentPathArray aVertexPaths;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath vertEntPath1;</pre>
<pre style="margin:0em;"> 	AcGePoint3d vertexPos1;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 		::getClosestVertexInfo</pre>
<pre style="margin:0em;"> 		(entId1, edgeEntPath1, ptEnt1, </pre>
<pre style="margin:0em;"> 		vertexPos1, vertEntPath1)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 	aVertexPaths.append(vertEntPath1);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (entId1.objectClass() == AcDbPoint::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		aPaths.append(vertEntPath1); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath edgeEntPath2;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (entId2.objectClass() != AcDbPoint::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 			::getClosestEdgeSubEntPath</pre>
<pre style="margin:0em;"> 			(entId2, ptEnt2, edgeEntPath2)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 		aPaths.append(edgeEntPath2); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath vertEntPath2;</pre>
<pre style="margin:0em;"> 	AcGePoint3d vertexPos2;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 		::getClosestVertexInfo</pre>
<pre style="margin:0em;"> 		(entId2, edgeEntPath2, ptEnt2, vertexPos2, </pre>
<pre style="margin:0em;"> 		vertEntPath2)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 	aVertexPaths.append(vertEntPath2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (entId2.objectClass() == AcDbPoint::desc())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		aPaths.append(vertEntPath2); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcArray&lt;AcConstrainedGeometry*&gt; pConsGeoms;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 		::addConstrainedGeometry(aPaths, </pre>
<pre style="margin:0em;"> 		pConsGeoms)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	es = ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 		::addGeomConstraint</pre>
<pre style="margin:0em;"> 		(AcGeomConstraint::kCoincident, aVertexPaths);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 	::getClosestVertexInfo(</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbObjectId&amp; entId, </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcDbFullSubentPath&amp; edgeSubentPath, </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">const</span><span style="color:#000000">  AcGePoint3d&amp; pt, </pre>
<pre style="margin:0em;"> 	AcGePoint3d&amp; closestVertexPos,</pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath&amp; closestVertexSubentPath)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbSmartObjectPointer &lt;AcDbEntity&gt; </pre>
<pre style="margin:0em;"> 		pEntity(entId, AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = pEntity.openStatus()) != Acad::eOk )</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pEntity-&gt;isKindOf(AcDbBlockReference::desc()))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  ADNAssocSampleUtils</pre>
<pre style="margin:0em;"> 			::getClosestVertexInfoBref(entId, </pre>
<pre style="margin:0em;"> 			edgeSubentPath, </pre>
<pre style="margin:0em;"> 			pt, </pre>
<pre style="margin:0em;"> 			closestVertexPos, </pre>
<pre style="margin:0em;"> 			closestVertexSubentPath);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbAssocPersSubentIdPE* <span style="color:#0000ff">const</span><span style="color:#000000">  pAssocPersSubentIdPE = </pre>
<pre style="margin:0em;"> 		AcDbAssocPersSubentIdPE::cast</pre>
<pre style="margin:0em;"> 		(pEntity-&gt;queryX(AcDbAssocPersSubentIdPE::desc()));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcArray&lt;AcDbSubentId&gt; vertexSubentIds;</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (pEntity-&gt;isKindOf(AcDbSpline::desc()))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbSubentId startVertexSubentId;</pre>
<pre style="margin:0em;"> 		AcDbSubentId endVertexSubentId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcArray&lt;AcDbSubentId&gt; controlPointSubentIds;</pre>
<pre style="margin:0em;"> 		AcArray&lt;AcDbSubentId&gt; fitPointSubentIds;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = </pre>
<pre style="margin:0em;"> 			pAssocPersSubentIdPE-&gt;getSplineEdgeVertexSubentities</pre>
<pre style="margin:0em;"> 			(pEntity,</pre>
<pre style="margin:0em;"> 			AcDbSubentId(),</pre>
<pre style="margin:0em;"> 			startVertexSubentId,</pre>
<pre style="margin:0em;"> 			endVertexSubentId,</pre>
<pre style="margin:0em;"> 			controlPointSubentIds,</pre>
<pre style="margin:0em;"> 			fitPointSubentIds)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		vertexSubentIds.append(startVertexSubentId);</pre>
<pre style="margin:0em;"> 		vertexSubentIds.append(endVertexSubentId);</pre>
<pre style="margin:0em;"> 		vertexSubentIds.append(controlPointSubentIds);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  (pEntity-&gt;isKindOf(AcDbPoint::desc()))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcArray&lt;AcDbSubentId&gt; vertexSubentIds;</pre>
<pre style="margin:0em;"> 		es = pAssocPersSubentIdPE-&gt;getAllSubentities</pre>
<pre style="margin:0em;"> 			(pEntity, AcDb::kVertexSubentType, vertexSubentIds);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (vertexSubentIds.length() &gt; 0)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			closestVertexSubentPath </pre>
<pre style="margin:0em;"> 			= AcDbFullSubentPath(entId, vertexSubentIds[0]);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span>	</pre>
<pre style="margin:0em;"> 		AcDbSubentId startVertexSubentId;</pre>
<pre style="margin:0em;"> 		AcDbSubentId endVertexSubentId;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ((es = pAssocPersSubentIdPE-&gt;getEdgeVertexSubentities</pre>
<pre style="margin:0em;"> 			(pEntity,</pre>
<pre style="margin:0em;"> 			edgeSubentPath.subentId(),</pre>
<pre style="margin:0em;"> 			startVertexSubentId,</pre>
<pre style="margin:0em;"> 			endVertexSubentId,</pre>
<pre style="margin:0em;"> 			vertexSubentIds)) != Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		vertexSubentIds.append(startVertexSubentId);</pre>
<pre style="margin:0em;"> 		vertexSubentIds.append(endVertexSubentId);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  minDist = -1.0;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbSubentId closestId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000"> (<span style="color:#0000ff">int</span><span style="color:#000000">  i=0; i&lt;vertexSubentIds.length(); ++i)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcGePoint3d vertexPos;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  ((es = pAssocPersSubentIdPE-&gt;</pre>
<pre style="margin:0em;"> 			getVertexSubentityGeometry(pEntity, </pre>
<pre style="margin:0em;"> 			vertexSubentIds[i],</pre>
<pre style="margin:0em;"> 			vertexPos)) != eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">double</span><span style="color:#000000">  dist = vertexPos.distanceTo(pt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (minDist &lt; 0 || dist &lt; minDist)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			minDist = dist;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			closestId = vertexSubentIds[i];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			closestVertexPos = vertexPos;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	closestVertexSubentPath </pre>
<pre style="margin:0em;"> 		= AcDbFullSubentPath(entId, closestId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  es;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
