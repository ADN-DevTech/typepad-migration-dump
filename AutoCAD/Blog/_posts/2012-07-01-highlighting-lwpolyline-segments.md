---
layout: "post"
title: "Highlighting lwpolyline segments"
date: "2012-07-01 07:39:30"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/highlighting-lwpolyline-segments.html "
typepad_basename: "highlighting-lwpolyline-segments"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample ObjectARX code to highlight / unhighlight polyline segments. The GS marker of the selected segment is determined and the "getSubentPathsAtGsMarker" is used to obtain the sub entity path. Using the sub entity path, the selected segment is then highlighted.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId objId;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> marker;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name sset; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(RTNORM != acedSSGet(L</span><span style="color: #a31515; line-height: 140%;">&quot;_:S&quot;</span><span style="line-height: 140%;">, NULL, NULL, NULL, sset))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(L</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelection failed :(&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">resbuf *rb = NULL;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (RTNORM != acedSSNameX(&amp;rb, sset, 0))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acedSSFree(sset);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">struct</span><span style="line-height: 140%;"> resbuf *pTemp;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (i=1, pTemp = rb;i&lt;3;i++, pTemp = pTemp-&gt;rbnext)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; { ; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name ename;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name_set(pTemp-&gt;resval.rlname, ename);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Move on to the 4th list element which is the GsMarker</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pTemp = pTemp-&gt;rbnext;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">marker = pTemp-&gt;resval.rint;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutRelRb(rb);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus es;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = acdbGetObjectId(objId, ename);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(es != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity *pEnt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = acdbOpenAcDbEntity(pEnt, objId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(es != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the subentity ID for the edge picked</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d pickpnt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcGeMatrix3d xform;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> numIds;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbFullSubentPath *subentIds;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = pEnt-&gt;getSubentPathsAtGsMarker(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; AcDb::kEdgeSubentType, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; marker, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pickpnt, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; xform, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; numIds, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; subentIds</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(es != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// At this point the subentIds variable contains the</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// address of an array of AcDbFullSubentPath objects.</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// The array should be one element long, so the picked</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// edge's AcDbFullSubentPath is in subentIds[0]</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// For objects with no edges (such as a sphere) the</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// code to highlight an edge is meaningless and must</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// be skipped.</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (numIds &gt; 0) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Highlight the picked edge.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pEnt-&gt;highlight(subentIds[0]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// OR</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Unhighlight the picked edge.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//pEnt-&gt;unhighlight(subentIds[0]);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> []subentIds;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEnt-&gt;close();</span></p>
</div>
