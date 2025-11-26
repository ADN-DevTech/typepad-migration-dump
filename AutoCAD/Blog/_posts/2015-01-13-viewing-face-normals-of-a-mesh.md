---
layout: "post"
title: "Viewing face normals of a mesh"
date: "2015-01-13 01:29:09"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/01/viewing-face-normals-of-a-mesh.html "
typepad_basename: "viewing-face-normals-of-a-mesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently I had a drawing sent over by a developer which had a SubDMesh that was created through code using vertex and face information. In such meshes, the normals of the faces would depend on the order of vertices while defining the face vertices, so wanted to check if the normals were all ok.</p>
<p>Just in case you would like to view the normals for a mesh, here is the code snippet to show the normals of the facets. <a href="http://adndevblog.typepad.com/autocad/2012/05/facet-subd-mesh.html">This</a> blog post by my colleague Adam Nagy draws the edges of the mesh which formed the basis for this code snippet.&nbsp;</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ads_name name;</pre>
<pre style="margin:0em;"> AcGePoint3d pt;</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  ret = acedEntSel(</pre>
<pre style="margin:0em;"> _T(<span style="color:#a31515">&quot;\\nSelect a SubD Mesh: &quot;</span><span style="color:#000000"> ), name, asDblArray(pt));</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (ret != RTNORM)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(_T(<span style="color:#a31515">&quot;\\nNothing selected&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> AcDbObjectId id;</pre>
<pre style="margin:0em;"> acdbGetObjectId(id, name);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (id.objectClass() != AcDbSubDMesh::desc())</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(</pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;\\nSelected entity is not a SubD Mesh&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectPointer&lt;AcDbSubDMesh&gt; mesh(</pre>
<pre style="margin:0em;"> 					id, AcDb::kForRead);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> AcDbFullSubentPathArray subentPaths;</pre>
<pre style="margin:0em;"> es = mesh-&gt;getSubentPath(-1,</pre>
<pre style="margin:0em;"> 		kFaceSubentType, subentPaths);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcGePoint3dArray vertices;</pre>
<pre style="margin:0em;"> es = mesh-&gt;getSubDividedVertices(vertices);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> AcArray&lt;Adesk::Int32&gt; faceInfo;</pre>
<pre style="margin:0em;"> es = mesh-&gt;getSubDividedFaceArray(faceInfo);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// The content of the faces list is like so:</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// [number of vertices of next face],</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// &lt;vertexIndex1, vertexIndex2, etc&gt;,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// e.g. [4], &lt;1, 2, 3, 4&gt;, [3], &lt;1, 2, 3&gt;, etc</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> AcDbDatabase * pDb =</pre>
<pre style="margin:0em;"> acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> AcDbBlockTableRecordPointer</pre>
<pre style="margin:0em;"> ms(ACDB_MODEL_SPACE, pDb, AcDb::kForWrite);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> AcDbVoidPtrArray lineArray;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  face = 0;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  numVerticesInFace = 0;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  i = 0; i &lt; faceInfo.length(); </pre>
<pre style="margin:0em;"> 	i += numVerticesInFace + 1, face++)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	numVerticesInFace = faceInfo.at(i);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePlane facePlane;</pre>
<pre style="margin:0em;"> 	es = mesh-&gt;getFacePlane(</pre>
<pre style="margin:0em;"> 		subentPaths.at(face).subentId(), </pre>
<pre style="margin:0em;"> 		facePlane);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGeBoundBlock3d boundBox;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  j = 0; j &lt; numVerticesInFace; j++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcGePoint3d pt1 =</pre>
<pre style="margin:0em;"> 		vertices.at(faceInfo.at(i + j + 1));</pre>
<pre style="margin:0em;"> 		AcGePoint3d pt2 =</pre>
<pre style="margin:0em;"> 		vertices.at(</pre>
<pre style="margin:0em;"> 		faceInfo.at(i + ((j + 1) % numVerticesInFace) + 1));</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (j == 0)</pre>
<pre style="margin:0em;"> 			boundBox.set(pt1, pt2);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			boundBox.extend(pt1);</pre>
<pre style="margin:0em;"> 			boundBox.extend(pt2);</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				</pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// If the edges are also required</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//AcDbObjectPointer&lt;AcDbLine&gt; line;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//line.create();</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//line-&gt;setStartPoint(pt1);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//line-&gt;setEndPoint(pt2);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//ms-&gt;appendAcDbEntity(line);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d minPt;</pre>
<pre style="margin:0em;"> 	AcGePoint3d maxPt;</pre>
<pre style="margin:0em;"> 	boundBox.getMinMaxPoints(minPt, maxPt);</pre>
<pre style="margin:0em;"> 	AcGeVector3d normalLen = maxPt - minPt;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGeVector3d normal = facePlane.normal();</pre>
<pre style="margin:0em;"> 					</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Arrow</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDbLine *pArrow = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbLine;</pre>
<pre style="margin:0em;"> 	pArrow-&gt;setColorIndex(2);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d sp = minPt + normalLen * 0.5;</pre>
<pre style="margin:0em;"> 	pArrow-&gt;setStartPoint(sp);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGePoint3d ep = sp </pre>
<pre style="margin:0em;"> 		+ normal.normalize() * normalLen.length();</pre>
<pre style="margin:0em;"> 	pArrow-&gt;setEndPoint(ep);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	ms-&gt;appendAcDbEntity(pArrow);</pre>
<pre style="margin:0em;"> 	pArrow-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Arrow head</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  arrowLen = normalLen.length() * 0.2;</pre>
<pre style="margin:0em;"> 	AcDb3dSolid *pArrowHead = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDb3dSolid;</pre>
<pre style="margin:0em;"> 	es = pArrowHead-&gt;createFrustum(</pre>
<pre style="margin:0em;"> 		arrowLen, </pre>
<pre style="margin:0em;"> 		arrowLen / 3.0, </pre>
<pre style="margin:0em;"> 		arrowLen / 3.0, </pre>
<pre style="margin:0em;"> 		0.0);</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 	AcGeVector3d zd = (ep - sp).normalize();</pre>
<pre style="margin:0em;"> 	AcGeVector3d yd = zd.perpVector().normalize();</pre>
<pre style="margin:0em;"> 	AcGeVector3d xd = yd.crossProduct(zd).normalize();</pre>
<pre style="margin:0em;"> 	AcGeMatrix3d mat;</pre>
<pre style="margin:0em;"> 	mat.setCoordSystem(ep, xd, yd, zd);</pre>
<pre style="margin:0em;"> 	pArrowHead-&gt;transformBy(mat);</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 	ms-&gt;appendAcDbEntity(pArrowHead);</pre>
<pre style="margin:0em;"> 	pArrowHead-&gt;close();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<p>Here is a screenshot of the normals for a simple box mesh : </p>

<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07d8f5e0970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07d8f5e0970d img-responsive" alt="Normals" title="Normals" src="/assets/image_655491.jpg" style="margin: 0px 5px 5px 0px;" /></a>
