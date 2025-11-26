---
layout: "post"
title: "Implementing AcDbCustomOsnapMode"
date: "2015-07-29 02:55:15"
author: "Madhukar Moogala"
categories:
  - "2016"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/implementing-acdbcustomosnapmode.html "
typepad_basename: "implementing-acdbcustomosnapmode"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>While I was working on customer query regarding Customosnapmode2, to embed icons in OSNAP right click menu, I felt the need to migrate a very old sample of ours, in my next blog I will updating on how to update\load OSNAP with icon in right click menu.</p>
<p>Following sample code shows how to implement a simple custom object snap to divide curves of all types <br />into thirds. If the mode is active, then two &quot;1/3&quot; snap points will exist for each open curve, <br />and three for each closed curve.</p>
<div style="font-size: 8pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdGlyph</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AcGiGlyph</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span> setLocation(<span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp; <span style="color: gray;">dcsPoint</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">m_center = <span style="color: gray;">dcsPoint</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">void</span> subViewportDraw(<span style="color: #2b91af;">AcGiViewportDraw</span>* <span style="color: gray;">vportDrawContext</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// Calculate the size of the glyph in WCS (use for text height factor)</span></p>
<p style="margin: 0px;"><span style="color: blue;">int</span> glyphPixels = acdbCustomOsnapManager()-&gt;osnapGlyphSize();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint2d</span> glyphSize;</p>
<p style="margin: 0px;"><span style="color: gray;">vportDrawContext</span>-&gt;viewport().getNumPixelsInUnitSquare( m_center, glyphSize );</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> glyphHeight = glyphPixels / glyphSize[ <span style="color: #2f4f4f;">Y</span> ];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Get the extents of the glyph text, so we can centre it</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGiTextStyle</span> style;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint2d</span> ptExt = style.extents( <span style="color: #6f008a;">ASDK_GLYPH_TEXT</span>, <span style="color: #2b91af;">Adesk</span>::<span style="color: #2f4f4f;">kFalse</span>, -1, <span style="color: #2b91af;">Adesk</span>::<span style="color: #2f4f4f;">kFalse</span> );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">struct</span> <span style="color: #2b91af;">resbuf</span> rbFrom, rbTo;</p>
<p style="margin: 0px;">rbFrom.restype = <span style="color: #6f008a;">RTSHORT</span>;</p>
<p style="margin: 0px;">rbFrom.resval.rint = 2; <span style="color: green;">// From DCS</span></p>
<p style="margin: 0px;">rbTo.restype = <span style="color: #6f008a;">RTSHORT</span>;</p>
<p style="margin: 0px;">rbTo.resval.rint = 0; <span style="color: green;">// To WCS</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Translate the X-axis of the DCS to WCS co-ordinates (as a displacement vector)</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeVector3d</span> ptDir;</p>
<p style="margin: 0px;">acedTrans( asDblArray( <span style="color: #2b91af;">AcGeVector3d</span>::kXAxis ),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;rbFrom,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;rbTo,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 1,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asDblArray( ptDir ));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Translate the centre of the glyph from DCS to WCS co-ordinates</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> ptPos, ptCen;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeVector3d</span> vecExt( ptExt[ <span style="color: #2f4f4f;">X</span> ] / 2, ptExt[ <span style="color: #2f4f4f;">Y</span> ] / 2, 0 );</p>
<p style="margin: 0px;">ptPos = m_center - vecExt / 2;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( <span style="color: #6f008a;">RTNORM</span> != acedTrans( asDblArray( ptPos ),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;rbFrom,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &amp;rbTo,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; asDblArray( ptCen )))</p>
<p style="margin: 0px;">ptCen = m_center;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Draw the centred text representing the glyph</span></p>
<p style="margin: 0px;"><span style="color: gray;">vportDrawContext</span>-&gt;geometry().text( ptCen,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">vportDrawContext</span>-&gt;viewport().viewDir(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ptDir,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; glyphHeight,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 1.0,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 0.0,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #6f008a;">ASDK_GLYPH_TEXT</span> );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">private</span>:</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> m_center;</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdOsnapInfo</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AcDbCustomOsnapInfo</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;"><span style="color: #6f008a;">ACRX_DECLARE_MEMBERS</span>(<span style="color: #2b91af;">AsdkThirdOsnapInfo</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span>&#0160;&#0160; getOsnapInfo(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbEntity</span>*&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pickedObject,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Adesk</span>::<span style="color: #2b91af;">GsMarker</span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; gsSelectionMark,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pickPoint,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lastPoint,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGeMatrix3d</span>&amp;&#0160;&#0160;&#0160; viewXform,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGePoint3d</span>&gt;&amp;&#0160;&#0160;&#0160; snapPoints,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geomIdsForPts,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGeCurve3d</span>*&gt;&amp; snapCurves,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geomIdsForLines) = 0;</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;"><span style="color: #6f008a;">ACRX_NO_CONS_DEFINE_MEMBERS</span>( <span style="color: #2b91af;">AsdkThirdOsnapInfo</span>, <span style="color: #2b91af;">AcDbCustomOsnapInfo</span> );</p>
<p style="margin: 0px;"><span style="color: green;">// AcDbEntity level protocol extension</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdOsnapEntityInfo</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AsdkThirdOsnapInfo</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span>&#0160;&#0160; getOsnapInfo(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbEntity</span>*&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickedObject</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Adesk</span>::<span style="color: #2b91af;">GsMarker</span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">gsSelectionMark</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">lastPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGeMatrix3d</span>&amp;&#0160;&#0160;&#0160; <span style="color: gray;">viewXform</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGePoint3d</span>&gt;&amp;&#0160;&#0160;&#0160; <span style="color: gray;">snapPoints</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForPts</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGeCurve3d</span>*&gt;&amp; <span style="color: gray;">snapCurves</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForLines</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// Base definition with no functionality</span></p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// AcDbCurve level protocol extension</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdOsnapCurveInfo</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AsdkThirdOsnapInfo</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span>&#0160;&#0160; getOsnapInfo(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbEntity</span>*&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickedObject</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Adesk</span>::<span style="color: #2b91af;">GsMarker</span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">gsSelectionMark</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">lastPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGeMatrix3d</span>&amp;&#0160;&#0160;&#0160; <span style="color: gray;">viewXform</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGePoint3d</span>&gt;&amp;&#0160;&#0160;&#0160; <span style="color: gray;">snapPoints</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForPts</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGeCurve3d</span>*&gt;&amp; <span style="color: gray;">snapCurves</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForLines</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// Generic curve function for all AcDbCurves (except AcDbPolylines)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Protocol Extension insures that the following assertion is always</span></p>
<p style="margin: 0px;"><span style="color: green;">// true, but check in non-prod versions just to be safe.</span></p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>( pickedObject-&gt;isKindOf( <span style="color: #2b91af;">AcDbCurve</span>::desc() ));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// but in production, a hard cast is fastest</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbCurve</span> *pCurve = (<span style="color: #2b91af;">AcDbCurve</span>*)<span style="color: gray;">pickedObject</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> startParam, endParam;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> pt;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span> es;</p>
<p style="margin: 0px;">es=pCurve-&gt;getStartParam( startParam);</p>
<p style="margin: 0px;">es=pCurve-&gt;getEndParam( endParam );</p>
<p style="margin: 0px;">es=pCurve-&gt;getPointAtParam( startParam + ((endParam - startParam) / 3), pt );</p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>( <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span> == es);</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;">es=pCurve-&gt;getPointAtParam( startParam + ((endParam - startParam) * 2 / 3), pt );</p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>(<span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>==es);</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( pCurve-&gt;isClosed() )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">es=pCurve-&gt;getStartPoint( pt );</p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>(<span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>==es);</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// AcDbPolyline level protocol extension</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdOsnapPolylineInfo</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AsdkThirdOsnapInfo</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span>&#0160;&#0160; getOsnapInfo(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcDbEntity</span>*&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickedObject</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Adesk</span>::<span style="color: #2b91af;">GsMarker</span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">gsSelectionMark</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">pickPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGePoint3d</span>&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">lastPoint</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">AcGeMatrix3d</span>&amp;&#0160;&#0160;&#0160; <span style="color: gray;">viewXform</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGePoint3d</span>&gt;&amp;&#0160;&#0160;&#0160; <span style="color: gray;">snapPoints</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForPts</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: #2b91af;">AcGeCurve3d</span>*&gt;&amp; <span style="color: gray;">snapCurves</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">AcArray</span>&lt;<span style="color: blue;">int</span>&gt;&amp;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: gray;">geomIdsForLines</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// Specialised implementation for AcDbPolylines:</span></p>
<p style="margin: 0px;"><span style="color: green;">//&#0160; Parametrisation of AcDbPolylines is different: each whole numbered paramater appears</span></p>
<p style="margin: 0px;"><span style="color: green;">//&#0160; at a vertex, so we cannot simply divide by three to get the correct parameter.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Protocol Extension insures that the following assertion is always</span></p>
<p style="margin: 0px;"><span style="color: green;">// true, but check in non-prod versions just to be safe.</span></p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>( pickedObject-&gt;isKindOf( <span style="color: #2b91af;">AcDbPolyline</span>::desc() ));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// but in production, a hard cast is fastest</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbPolyline</span> *pPline = (<span style="color: #2b91af;">AcDbPolyline</span>*)<span style="color: gray;">pickedObject</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span> es;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( bSnapToSegments )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: green;">// Snap to a third of each of the segments</span></p>
<p style="margin: 0px;"><span style="color: blue;">unsigned</span> <span style="color: blue;">int</span> numSegs = pPline-&gt;numVerts() - 1;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeLineSeg3d</span> segLn;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeCircArc3d</span> segArc;</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> startParam, endParam, newParam, dist;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> pt;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">for</span> ( <span style="color: blue;">unsigned</span> <span style="color: blue;">int</span> idx = 0; idx &lt; numSegs; idx++ )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">switch</span>( pPline-&gt;segType( idx ))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">case</span> <span style="color: #2b91af;">AcDbPolyline</span>::<span style="color: #2f4f4f;">kLine</span>:</p>
<p style="margin: 0px;">es=pPline-&gt;getLineSegAt( idx, segLn );</p>
<p style="margin: 0px;">startParam = segLn.paramOf( segLn.startPoint() );</p>
<p style="margin: 0px;">endParam = segLn.paramOf( segLn.endPoint() );</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append(segLn.evalPoint( startParam + ((endParam - startParam) / 3 )));</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append(segLn.evalPoint( startParam + ((endParam - startParam) * 2 / 3 )));</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">case</span> <span style="color: #2b91af;">AcDbPolyline</span>::<span style="color: #2f4f4f;">kArc</span>:</p>
<p style="margin: 0px;">es=pPline-&gt;getArcSegAt( idx, segArc );</p>
<p style="margin: 0px;">startParam = segArc.paramOf( segArc.startPoint() );</p>
<p style="margin: 0px;">endParam = segArc.paramOf( segArc.endPoint() );</p>
<p style="margin: 0px;">dist = segArc.length( startParam, endParam );</p>
<p style="margin: 0px;">newParam = segArc.paramAtLength( startParam, dist / 3 );</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( segArc.evalPoint( newParam ));</p>
<p style="margin: 0px;">newParam = segArc.paramAtLength( startParam, dist * 2 / 3 );</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( segArc.evalPoint( newParam ));</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">default</span>:</p>
<p style="margin: 0px;"><span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">else</span> {</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> endParam;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> pt;</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> dist;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">es=pPline-&gt;getEndParam( endParam );</p>
<p style="margin: 0px;">es=pPline-&gt;getDistAtParam( endParam, dist );</p>
<p style="margin: 0px;">es=pPline-&gt;getPointAtDist( dist / 3, pt );</p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>(<span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>==es);</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;">es=pPline-&gt;getPointAtDist( dist * 2 / 3, pt );</p>
<p style="margin: 0px;"><span style="color: #6f008a;">assert</span>(<span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>==es);</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> ( pPline-&gt;isClosed() )</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">es=pPline-&gt;getStartPoint( pt );</p>
<p style="margin: 0px;"><span style="color: gray;">snapPoints</span>.append( pt );</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">class</span> <span style="color: #2b91af;">AsdkThirdOsnapMode</span> : <span style="color: blue;">public</span> <span style="color: #2b91af;">AcDbCustomOsnapMode</span></p>
<p style="margin: 0px;">{&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span>:</p>
<p style="margin: 0px;">AsdkThirdOsnapMode()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">m_pGlyph = <span style="color: blue;">new</span> <span style="color: #2b91af;">AsdkThirdGlyph</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> ~AsdkThirdOsnapMode()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">delete</span> m_pGlyph;</p>
<p style="margin: 0px;">m_pGlyph = <span style="color: #6f008a;">NULL</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">TCHAR</span>* localModeString() <span style="color: blue;">const</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;THIrd&quot;</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">TCHAR</span>* globalModeString() <span style="color: blue;">const</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;_THIrd&quot;</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">AcRxClass</span>* entityOsnapClass() <span style="color: blue;">const</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #2b91af;">AsdkThirdOsnapInfo</span>::desc();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: #2b91af;">AcGiGlyph</span>* glyph() <span style="color: blue;">const</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> m_pGlyph;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">TCHAR</span>* tooltipString() <span style="color: blue;">const</span>{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;Third of length&quot;</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">virtual</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">ACHAR</span> * displayString()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> localModeString();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">private</span>:</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AsdkThirdGlyph</span> *m_pGlyph;</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AsdkThirdOsnapMode</span> thirdMode;</p>
<p style="margin: 0px;"><span style="color: blue;">bool</span> bSnapToSegments = <span style="color: blue;">false</span>;</p>
</div>
<p>You can download the sample <a href="https://github.com/MadhukarMoogala/MyBlogs/tree/master/CustomOsnap" target="_blank">here</a></p>
<p><iframe allowfullscreen="allowfullscreen" frameborder="0" height="200" src="https://screencast.autodesk.com/Embed/ebc98207-56f3-45c2-bdc5-0afc814db321" webkitallowfullscreen="webkitallowfullscreen" width="320"></iframe></p>
<p>Commands: <br />--------- <br />SNAP2PLINE&#0160;&#0160;&#0160;&#0160;&#0160; Chooses to snap to a third of each polyline segment <br />SNAP2SEG&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Chooses to snap to a third of the whole polyline</p>
<p>Classes: <br />-------- <br />AsdkThirdOsnapInfo&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Abstract base class for the protocol extension. <br />AsdkThirdOsnapEntityInfo&#0160;&#0160;&#0160; Generic definition providing no functionality. <br />AsdkThirdOsnapCurveInfo&#0160;&#0160;&#0160;&#0160; Generic function for all curves, except AcDbPolylines. <br />AsdkThirdOsnapPolylineInfo&#0160; Specialized function for AcDbPolylines and derived classes.</p>
<p>AsdkThirdOsnapMode&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Object snap mode describing the third object snap. <br />AsdkThirdGlyph&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Glyph definition.</p>
<p>To use test this sample applilcation do the following: <br />1. Draw a polyline. <br />2. Run the APPLOAD command and Load the .arx <br />3. Run the line command, before selecting a point move the cursor over the previously drawn polyline</p>
<p>Results. <br />A snap point with the text of 1/3 appears on the polyline</p>
