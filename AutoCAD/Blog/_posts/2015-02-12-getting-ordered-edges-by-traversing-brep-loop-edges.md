---
layout: "post"
title: "Getting ordered edges by traversing BRep Loop edges"
date: "2015-02-12 23:35:50"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/02/getting-ordered-edges-by-traversing-brep-loop-edges.html "
typepad_basename: "getting-ordered-edges-by-traversing-brep-loop-edges"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When traversing the edges of a face loop using BRep API, the edges may not be in order such that the end point of the previous edge coincides with the start point of the next edge. This is because, the edges are shared between faces, and the same edge is returned when the face's edge loop is traversed. As the start and end points of the edge remains unchanged, it will only match the orientation for one of the faces.</p>
<p>Here is a sample code to retrieve the edge information and orient them as per the loop being iterated.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> ads_name eName; </pre>
<pre style="margin:0em;"> ads_point pt; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> ( RTNORM != </pre>
<pre style="margin:0em;"> 	acedEntSel( L<span style="color:#a31515">&quot;\\nSelect a Solid: &quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 				eName, pt)) </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">return</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectId id; </pre>
<pre style="margin:0em;"> acdbGetObjectId( id, eName); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDb3dSolid* pSolid; </pre>
<pre style="margin:0em;"> acdbOpenObject(pSolid, id, AcDb::kForRead); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (pSolid == NULL ) <span style="color:#0000ff">return</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcBrBrep pBrep; </pre>
<pre style="margin:0em;"> pBrep.setSubentPath( </pre>
<pre style="margin:0em;"> 	AcDbFullSubentPath( id, kNullSubentId )); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcBr::ErrorStatus returnValue = AcBr::eOk; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcBrBrepFaceTraverser brepFaceTrav; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (brepFaceTrav.setBrep(pBrep) != AcBr::eOk) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(</pre>
<pre style="margin:0em;"> 		<span style="color:#a31515">&quot;\\n Error in AcBrBrepFaceTraverser::setBrep:&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  faceCount = 0; </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">while</span><span style="color:#000000">  (!brepFaceTrav.done() &amp;&amp; (returnValue == AcBr::eOk))</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 	faceCount++; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcBrFace face; </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  ( brepFaceTrav.getFace(face) </pre>
<pre style="margin:0em;"> 		!= AcBr::ErrorStatus::eOk) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">continue</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGeSurface *pGeSurface = NULL;</pre>
<pre style="margin:0em;"> 	face.getSurface(pGeSurface);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGeInterval intervalU, intervalV;</pre>
<pre style="margin:0em;"> 	pGeSurface-&gt;getEnvelope(intervalU, intervalV);</pre>
<pre style="margin:0em;"> 						</pre>
<pre style="margin:0em;"> 	AcBrFaceLoopTraverser faLoTrav; </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000"> ( faLoTrav.setFace(face); </pre>
<pre style="margin:0em;"> 		!faLoTrav.done(); faLoTrav.next() ) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 		AcBrLoop lp; </pre>
<pre style="margin:0em;"> 		faLoTrav.getLoop(lp); </pre>
<pre style="margin:0em;"> 		AcBr::LoopType type; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\nFACE : %d&quot;</span><span style="color:#000000"> ), faceCount);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcBrLoopEdgeTraverser loEdTrav; </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ( loEdTrav.setLoop(faLoTrav) == AcBr::eOk) </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">int</span><span style="color:#000000">  edgeCount = 0;</pre>
<pre style="margin:0em;"> 			AcGePoint2dArray verts; </pre>
<pre style="margin:0em;"> 			AcGeVoidPointerArray edgeArray; </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">for</span><span style="color:#000000"> (;! loEdTrav.done(); loEdTrav.next()) </pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 				edgeCount++;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcBrEdge edge; </pre>
<pre style="margin:0em;"> 				loEdTrav.getEdge(edge); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcBrVertex start;      </pre>
<pre style="margin:0em;"> 				edge.getVertex1( start );   </pre>
<pre style="margin:0em;"> 				AcGePoint3d stPt3d;            </pre>
<pre style="margin:0em;"> 				start.getPoint( stPt3d );  </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcBrVertex end; </pre>
<pre style="margin:0em;"> 				edge.getVertex2( end );   </pre>
<pre style="margin:0em;"> 				AcGePoint3d endPt3d;            </pre>
<pre style="margin:0em;"> 				end.getPoint( endPt3d ); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				Adesk::Boolean orient </pre>
<pre style="margin:0em;"> 					= Adesk::kFalse;</pre>
<pre style="margin:0em;"> 				loEdTrav.getEdgeOrientToLoop(orient);</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">if</span><span style="color:#000000"> (orient)</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\nEdge %d: </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					(%3.1f %3.1f %3.1f) </pre>
<pre style="margin:0em;"> 					- (%3.1f %3.1f %3.1f)<span style="color:#a31515">&quot;), </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					edgeCount, stPt3d.x, </pre>
<pre style="margin:0em;"> 					stPt3d.y, stPt3d.z, </pre>
<pre style="margin:0em;"> 					endPt3d.x, endPt3d.y, </pre>
<pre style="margin:0em;"> 					endPt3d.z);</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				acutPrintf(ACRX_T(<span style="color:#a31515">&quot;\\nEdge %d: </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					(%3.1f %3.1f %3.1f) </pre>
<pre style="margin:0em;"> 					- (%3.1f %3.1f %3.1f)<span style="color:#a31515">&quot;),</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					edgeCount, endPt3d.x, </pre>
<pre style="margin:0em;"> 					endPt3d.y, endPt3d.z, </pre>
<pre style="margin:0em;"> 					stPt3d.x, stPt3d.y, </pre>
<pre style="margin:0em;"> 					stPt3d.z);</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 	returnValue = brepFaceTrav.next(); </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>As an example, here is an output of a face loop :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Considering edge orientation to loop</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> FACE : 3</pre>
<pre style="margin:0em;"> Edge 1: (0.0 0.0 30.0) - (0.0 0.0 0.0)</pre>
<pre style="margin:0em;"> Edge 2: (0.0 0.0 0.0) - (10.0 0.0 0.0)</pre>
<pre style="margin:0em;"> Edge 3: (10.0 0.0 0.0) - (10.0 0.0 30.0)</pre>
<pre style="margin:0em;"> Edge 4: (10.0 0.0 30.0) - (0.0 0.0 30.0)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Without considering edge orientation to loop</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> FACE : 3</pre>
<pre style="margin:0em;"> Edge 1: (0.0 0.0 30.0) - (0.0 0.0 0.0)</pre>
<pre style="margin:0em;"> Edge 2: (10.0 0.0 0.0) - (0.0 0.0 0.0)</pre>
<pre style="margin:0em;"> Edge 3: (10.0 0.0 30.0) - (10.0 0.0 0.0)</pre>
<pre style="margin:0em;"> Edge 4: (0.0 0.0 30.0) - (10.0 0.0 30.0)</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
