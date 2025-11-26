---
layout: "post"
title: "Extending AssociativeDimension To CustomSolid"
date: "2017-03-24 00:37:00"
author: "Madhukar Moogala"
categories:
  - "2017"
  - "AutoCAD"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/03/extending-associativedimension-to-customsolid.html "
typepad_basename: "extending-associativedimension-to-customsolid"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p> <p>This is a continuation of the <a href="http://adndevblog.typepad.com/autocad/2015/02/making-a-custom-entity-associative-dimension-enabled.html" target="_blank">post</a> written by my colleague.</p> <p>In this blog we will explore the Associative mechanism applied to custom solid, previous blog solution may not be applicable when Dimensions are applied using snap points on the sub-entities of a solid. I have created a Frustum solid with Axis of revolution. </p> <p>To exploit Associative framework, we need to reveal the our subEntities by implementing associative protocol extension <a href="http://help.autodesk.com/view/OARXMAC/2017/ENU/?guid=OREFMAC-AcDbAssocPersSubentIdPE" target="_blank">AcDbAssocPersSubentIdPE</a>.</p> <p>The Associative Framework queries this protocol extension when creating new associative dimensions based on AcDbAssocAnnotationActionBody.</p> <p>Below is only a partial code that exposes sub-entities by implementing persistent protocol.</p><pre style="background: #ffffff; color: #000000">AcDbEntity<span style="color: #808030">*</span> MyEnt1<span style="color: #800080">::</span>subSubentPtr<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">const</span> AcDbFullSubentPath<span style="color: #808030">&amp;</span> path<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">const</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">const</span> AcDbSubentId subEntId <span style="color: #808030">=</span> path<span style="color: #808030">.</span>subentId<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>isCustomSubentId<span style="color: #808030">(</span>subEntId<span style="color: #808030">)</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
AcDbEntity<span style="color: #808030">*</span> pSubEntity <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">nullptr</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">const</span> Adesk<span style="color: #800080">::</span>GsMarker subentIndex <span style="color: #808030">=</span> subEntId<span style="color: #808030">.</span>index<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">switch</span> <span style="color: #808030">(</span>subEntId<span style="color: #808030">.</span>type<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kEdgeSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomEdgeSubentIndex<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		pSubEntity <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> AcDbLine<span style="color: #808030">(</span>m_ptSP<span style="color: #808030">,</span> m_ptEP<span style="color: #808030">)</span><span style="color: #800080">;</span> 
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kVertexSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomVertex1SubentIndex<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		pSubEntity <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> AcDbPoint<span style="color: #808030">(</span>m_ptSP<span style="color: #808030">)</span><span style="color: #800080">;</span> 
		<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">else</span> <span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomVertex2SubentIndex<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		pSubEntity <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> AcDbPoint<span style="color: #808030">(</span>m_ptEP<span style="color: #808030">)</span><span style="color: #800080">;</span>
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
ASSERT<span style="color: #808030">(</span>pSubEntity <span style="color: #808030">!</span><span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">nullptr</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">return</span> pSubEntity<span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #696969">// If not data of the derived class, use the base class implementation</span>
<span style="color: #696969">//</span>
<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subSubentPtr<span style="color: #808030">(</span>path<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

Acad<span style="color: #800080">::</span>ErrorStatus MyEnt1<span style="color: #800080">::</span>subGetSubentPathsAtGsMarker<span style="color: #808030">(</span>AcDb<span style="color: #800080">::</span>SubentType type<span style="color: #808030">,</span>
				        Adesk<span style="color: #800080">::</span>GsMarker gsMark<span style="color: #808030">,</span>
				        <span style="font-weight: bold; color: #800000">const</span> AcGePoint3d <span style="color: #808030">&amp;</span> pickPoint<span style="color: #808030">,</span> 
				        <span style="font-weight: bold; color: #800000">const</span> AcGeMatrix3d <span style="color: #808030">&amp;</span> viewXform<span style="color: #808030">,</span>
				        <span style="font-weight: bold; color: #800000">int</span> <span style="color: #808030">&amp;</span> numPaths<span style="color: #808030">,</span> 
				        AcDbFullSubentPath <span style="color: #808030">*</span><span style="color: #808030">&amp;</span> subentPaths<span style="color: #808030">,</span>
				        <span style="font-weight: bold; color: #800000">int</span> numInserts<span style="color: #808030">,</span> 
				        AcDbObjectId <span style="color: #808030">*</span> entAndInsertStack<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">const</span>
<span style="color: #800080">{</span>
assertReadEnabled<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

numPaths <span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #800080">;</span>
subentPaths <span style="color: #808030">=</span> <span style="color: #7d0045">NULL</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>gsMark <span style="color: #808030">=</span><span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span>
<span style="font-weight: bold; color: #800000">return</span> Acad<span style="color: #800080">::</span>eInvalidInput<span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>isCustomGsMarker<span style="color: #808030">(</span>gsMark<span style="color: #808030">)</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">switch</span> <span style="color: #808030">(</span>type<span style="color: #808030">)</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kEdgeSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>gsMark <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomEdgeGsMarker<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		numPaths <span style="color: #808030">=</span> <span style="color: #008c00">1</span><span style="color: #800080">;</span>
		subentPaths <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> AcDbFullSubentPath<span style="color: #808030">[</span><span style="color: #008c00">1</span><span style="color: #808030">]</span><span style="color: #800080">;</span>
		subentPaths<span style="color: #808030">[</span><span style="color: #008c00">0</span><span style="color: #808030">]</span> <span style="color: #808030">=</span> AcDbFullSubentPath<span style="color: #808030">(</span>objectId<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">,</span>
					AcDb<span style="color: #800080">::</span>kEdgeSubentType<span style="color: #808030">,</span>
					kCustomEdgeSubentIndex<span style="color: #808030">)</span><span style="color: #800080">;</span>
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kVertexSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>gsMark <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomEdgeGsMarker<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		numPaths <span style="color: #808030">=</span> <span style="color: #008c00">2</span><span style="color: #800080">;</span>
		subentPaths <span style="color: #808030">=</span> <span style="font-weight: bold; color: #800000">new</span> AcDbFullSubentPath<span style="color: #808030">[</span><span style="color: #008c00">2</span><span style="color: #808030">]</span><span style="color: #800080">;</span>
		subentPaths<span style="color: #808030">[</span><span style="color: #008c00">0</span><span style="color: #808030">]</span> <span style="color: #808030">=</span> AcDbFullSubentPath<span style="color: #808030">(</span>objectId<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">,</span>
					AcDb<span style="color: #800080">::</span>kVertexSubentType<span style="color: #808030">,</span>
					kCustomVertex1SubentIndex<span style="color: #808030">)</span><span style="color: #800080">;</span>
		subentPaths<span style="color: #808030">[</span><span style="color: #008c00">1</span><span style="color: #808030">]</span> <span style="color: #808030">=</span> AcDbFullSubentPath<span style="color: #808030">(</span>objectId<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">,</span>
					AcDb<span style="color: #800080">::</span>kVertexSubentType<span style="color: #808030">,</span>
					kCustomVertex2SubentIndex<span style="color: #808030">)</span><span style="color: #800080">;</span>
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
ASSERT<span style="color: #808030">(</span>numPaths <span style="color: #808030">!</span><span style="color: #808030">=</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">return</span> Acad<span style="color: #800080">::</span>eOk<span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #696969">// If not data of the derived class, use the base class implementation</span>
<span style="color: #696969">//</span>
<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subGetSubentPathsAtGsMarker<span style="color: #808030">(</span>type<span style="color: #808030">,</span>
				gsMark<span style="color: #808030">,</span> 
				pickPoint<span style="color: #808030">,</span>
				viewXform<span style="color: #808030">,</span>
				numPaths<span style="color: #808030">,</span>
				subentPaths<span style="color: #808030">,</span>
				numInserts<span style="color: #808030">,</span> 
				entAndInsertStack<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

Acad<span style="color: #800080">::</span>ErrorStatus MyEnt1<span style="color: #800080">::</span>subGetGsMarkersAtSubentPath<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">const</span> AcDbFullSubentPath<span style="color: #808030">&amp;</span> path<span style="color: #808030">,</span> 
				AcDbIntPtrArray<span style="color: #808030">&amp;</span>  gsMarkers<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">const</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">const</span> AcDbSubentId subEntId <span style="color: #808030">=</span> path<span style="color: #808030">.</span>subentId<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>isCustomSubentId<span style="color: #808030">(</span>subEntId<span style="color: #808030">)</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
gsMarkers<span style="color: #808030">.</span>removeAll<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">const</span> Adesk<span style="color: #800080">::</span>GsMarker subentIndex <span style="color: #808030">=</span> subEntId<span style="color: #808030">.</span>index<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">switch</span> <span style="color: #808030">(</span>subEntId<span style="color: #808030">.</span>type<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kEdgeSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomEdgeSubentIndex<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		gsMarkers<span style="color: #808030">.</span>append<span style="color: #808030">(</span>kCustomEdgeGsMarker<span style="color: #808030">)</span><span style="color: #800080">;</span>
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kVertexSubentType</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomVertex1SubentIndex <span style="color: #808030">|</span><span style="color: #808030">|</span> 
		subentIndex <span style="color: #808030">=</span><span style="color: #808030">=</span> kCustomVertex2SubentIndex<span style="color: #808030">)</span>
		<span style="color: #800080">{</span>
		gsMarkers<span style="color: #808030">.</span>append<span style="color: #808030">(</span>kCustomEdgeGsMarker<span style="color: #808030">)</span><span style="color: #800080">;</span>
		<span style="color: #800080">}</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
ASSERT<span style="color: #808030">(</span><span style="color: #808030">!</span>gsMarkers<span style="color: #808030">.</span>isEmpty<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">return</span> Acad<span style="color: #800080">::</span>eOk<span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #696969">// If not data of the derived class, use the base class implementation</span>
<span style="color: #696969">//</span>
<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subGetGsMarkersAtSubentPath<span style="color: #808030">(</span>path<span style="color: #808030">,</span> gsMarkers<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

Adesk<span style="color: #800080">::</span>UInt32 MyEnt1<span style="color: #800080">::</span>subSetAttributes<span style="color: #808030">(</span>AcGiDrawableTraits <span style="color: #808030">*</span>traits<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
assertReadEnabled<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subSetAttributes<span style="color: #808030">(</span>traits<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

Acad<span style="color: #800080">::</span>ErrorStatus MyEnt1<span style="color: #800080">::</span>subGetOsnapPoints<span style="color: #808030">(</span>AcDb<span style="color: #800080">::</span>OsnapMode osnapMode<span style="color: #808030">,</span>
Adesk<span style="color: #800080">::</span>GsMarker gsSelectionMark<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">const</span> AcGePoint3d<span style="color: #808030">&amp;</span> pickPoint<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">const</span> AcGePoint3d<span style="color: #808030">&amp;</span> lastPoint<span style="color: #808030">,</span>
<span style="font-weight: bold; color: #800000">const</span> AcGeMatrix3d<span style="color: #808030">&amp;</span> viewXform<span style="color: #808030">,</span>
AcGePoint3dArray<span style="color: #808030">&amp;</span> snapPoints<span style="color: #808030">,</span>
AcDbIntArray<span style="color: #808030">&amp;</span> geomIds<span style="color: #808030">)</span> <span style="font-weight: bold; color: #800000">const</span>
<span style="color: #800080">{</span>
assertReadEnabled<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">if</span> <span style="color: #808030">(</span>MyEnt1<span style="color: #800080">::</span>isCustomGsMarker<span style="color: #808030">(</span>gsSelectionMark<span style="color: #808030">)</span><span style="color: #808030">)</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">switch</span> <span style="color: #808030">(</span>osnapMode<span style="color: #808030">)</span>
	<span style="color: #800080">{</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kOsModeEnd</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	snapPoints<span style="color: #808030">.</span>append<span style="color: #808030">(</span>m_ptSP<span style="color: #808030">)</span><span style="color: #800080">;</span>
	snapPoints<span style="color: #808030">.</span>append<span style="color: #808030">(</span>m_ptEP<span style="color: #808030">)</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="font-weight: bold; color: #800000">case </span><span style="color: #7d0045">AcDb</span><span style="color: #800080">::</span><span style="color: #7d0045">kOsModeNear</span><span style="color: #e34adc">:</span>
	<span style="color: #800080">{</span>
	AcGeVector3d viewDir<span style="color: #808030">(</span>viewXform<span style="color: #808030">(</span>Z<span style="color: #808030">,</span> <span style="color: #008c00">0</span><span style="color: #808030">)</span><span style="color: #808030">,</span> viewXform<span style="color: #808030">(</span>Z<span style="color: #808030">,</span> <span style="color: #008c00">1</span><span style="color: #808030">)</span><span style="color: #808030">,</span> viewXform<span style="color: #808030">(</span>Z<span style="color: #808030">,</span> <span style="color: #008c00">2</span><span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
	AcGeLine3d line3d<span style="color: #808030">(</span>m_ptSP<span style="color: #808030">,</span> m_ptEP<span style="color: #808030">)</span><span style="color: #800080">;</span>
	snapPoints<span style="color: #808030">.</span>append<span style="color: #808030">(</span>line3d<span style="color: #808030">.</span>projClosestPointTo<span style="color: #808030">(</span>pickPoint<span style="color: #808030">,</span> viewDir<span style="color: #808030">)</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
	<span style="font-weight: bold; color: #800000">break</span><span style="color: #800080">;</span>
	<span style="color: #800080">}</span>
<span style="font-weight: bold; color: #800000">return</span> Acad<span style="color: #800080">::</span>eOk<span style="color: #800080">;</span>
<span style="color: #800080">}</span>

<span style="color: #696969">// If not data of the derived class, use the base class implementation</span>
<span style="color: #696969">//</span>
<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subGetOsnapPoints<span style="color: #808030">(</span>osnapMode<span style="color: #808030">,</span>
			gsSelectionMark<span style="color: #808030">,</span>
			pickPoint<span style="color: #808030">,</span> 
			lastPoint<span style="color: #808030">,</span> 
			viewXform<span style="color: #808030">,</span>
			snapPoints<span style="color: #808030">,</span>
			geomIds<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>

Acad<span style="color: #800080">::</span>ErrorStatus MyEnt1<span style="color: #800080">::</span>subTransformBy<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">const</span> AcGeMatrix3d<span style="color: #808030">&amp;</span> xform<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
assertWriteEnabled<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>

m_ptSP<span style="color: #808030">.</span>transformBy<span style="color: #808030">(</span>xform<span style="color: #808030">)</span><span style="color: #800080">;</span>
m_ptEP<span style="color: #808030">.</span>transformBy<span style="color: #808030">(</span>xform<span style="color: #808030">)</span><span style="color: #800080">;</span>

<span style="font-weight: bold; color: #800000">return</span> AcDb3dSolid<span style="color: #800080">::</span>subTransformBy<span style="color: #808030">(</span>xform<span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="color: #800080">}</span>


<span style="color: #696969">//Always check iff 100000 &lt;= gsMarker &lt;= 1000002</span>
<span style="font-weight: bold; color: #800000">bool</span> MyEnt1<span style="color: #800080">::</span>isCustomGsMarker<span style="color: #808030">(</span>Adesk<span style="color: #800080">::</span>GsMarker gsMarker<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">return</span> kCustomGsMarkerMin <span style="color: #808030">&lt;</span><span style="color: #808030">=</span> gsMarker <span style="color: #808030">&amp;</span><span style="color: #808030">&amp;</span>
	gsMarker <span style="color: #808030">&lt;</span><span style="color: #808030">=</span> kCustomGsMarkerMax<span style="color: #800080">;</span>
<span style="color: #800080">}</span>


<span style="font-weight: bold; color: #800000">bool</span> MyEnt1<span style="color: #800080">::</span>isCustomSubentId<span style="color: #808030">(</span><span style="font-weight: bold; color: #800000">const</span> AcDbSubentId<span style="color: #808030">&amp;</span> subentId<span style="color: #808030">)</span>
<span style="color: #800080">{</span>
<span style="font-weight: bold; color: #800000">const</span> Adesk<span style="color: #800080">::</span>GsMarker index <span style="color: #808030">=</span> subentId<span style="color: #808030">.</span>index<span style="color: #808030">(</span><span style="color: #808030">)</span><span style="color: #800080">;</span>
<span style="font-weight: bold; color: #800000">return</span> kCustomSubentIdIndexMin <span style="color: #808030">&lt;</span><span style="color: #808030">=</span> index <span style="color: #808030">&amp;</span><span style="color: #808030">&amp;</span>
	index <span style="color: #808030">&lt;</span><span style="color: #808030">=</span> kCustomSubentIdIndexMax<span style="color: #800080">;</span>
<span style="color: #800080">}</span>
</pre>
<p>&nbsp;</p>
<p>Here is a screencast and source project is available at <a href="https://github.com/MadhukarMoogala/MyBlogs/tree/master/MyEnt" target="_blank">GitHub</a>.</p><iframe height="655" src="https://screencast.autodesk.com/Embed/Timeline/301757ac-0875-4d5c-b7f7-e849cfd411f4" frameborder="0" width="696" webkitallowfullscreen allowfullscreen></iframe>
