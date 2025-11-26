---
layout: "post"
title: "Creating a solid from a set of SubDMesh"
date: "2014-12-07 23:04:37"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/12/creating-a-solid-from-a-set-of-subdmesh.html "
typepad_basename: "creating-a-solid-from-a-set-of-subdmesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you have a collection of subDMesh and wish to create a Solid from it, first convert each of those mesh to a surface. If the sufaces all put together form a closed volume, then a sculpted solid can be created from these surfaces. Here is a sample code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// For SubDMesh </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;dbSubD.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  AdskMeshToSolid()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  ret;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Select the meshes to create the solid from</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcArray&lt;AcDbObjectId&gt; meshIdArray;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">do</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		ads_name entName;</pre>
<pre style="margin:0em;"> 		ads_point pt;</pre>
<pre style="margin:0em;"> 		ret = acedEntSel(L<span style="color:#a31515">&quot;Select a mesh : &quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 						entName, pt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (RTNORM != ret) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId objId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ( Acad::eOk != acdbGetObjectId(objId, entName)) </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (objId.objectClass() == AcDbSubDMesh::desc())</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			meshIdArray.append(objId);</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span><span style="color:#0000ff">while</span><span style="color:#000000"> (ret == RTNORM);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Convert the meshes to surfaces</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcArray&lt;AcDbEntity*&gt; surfacesArray;</pre>
<pre style="margin:0em;"> 	AcGeIntArray limits;</pre>
<pre style="margin:0em;"> 	AcDbSurface *pSurface = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000"> (<span style="color:#0000ff">int</span><span style="color:#000000">  mesh = 0; mesh &lt; meshIdArray.length(); mesh++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbEntity *pEntity = NULL; </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (acdbOpenAcDbEntity(pEntity, meshIdArray[mesh], </pre>
<pre style="margin:0em;"> 							AcDb::kForRead) != Acad::eOk) </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 		AcDbSubDMesh *pMesh = AcDbSubDMesh::cast(pEntity); </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pMesh)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			es = pMesh-&gt;convertToSurface(</pre>
<pre style="margin:0em;"> 							<span style="color:#0000ff">false</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 							<span style="color:#0000ff">false</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;"> 							pSurface);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (es != Acad::eOk)</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000">  ( es == Acad::eOk &amp;&amp; </pre>
<pre style="margin:0em;"> 				pSurface-&gt;isKindOf(AcDbSurface::desc()))</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				surfacesArray.append(pSurface);</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		pEntity-&gt;close();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Create a sculpted solid from surfaces</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDb3dSolid *pSolid = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDb3dSolid();</pre>
<pre style="margin:0em;"> 	pSolid-&gt;setDatabaseDefaults();</pre>
<pre style="margin:0em;"> 		 </pre>
<pre style="margin:0em;"> 	es = pSolid-&gt;createSculptedSolid(</pre>
<pre style="margin:0em;"> 					surfacesArray, </pre>
<pre style="margin:0em;"> 					limits);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Add sculpted solid to the database </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId solidOid = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 		postToDb(pSolid, &amp;solidOid);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// To show the newly created solid away</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// from the surfaces, we transform it..</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbEntity *pSolidEnt;</pre>
<pre style="margin:0em;"> 		es = acdbOpenAcDbEntity(</pre>
<pre style="margin:0em;"> 				pSolidEnt, solidOid, AcDb::kForWrite);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">   (es == Acad::eOk &amp;&amp;</pre>
<pre style="margin:0em;"> 				pSolidEnt-&gt;isKindOf(AcDb3dSolid::desc()))</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcGeVector3d vec(15.0, 15.0, 0.0);</pre>
<pre style="margin:0em;"> 			AcGeMatrix3d transform </pre>
<pre style="margin:0em;"> 						= transform.setToTranslation(vec);</pre>
<pre style="margin:0em;"> 			es = pSolidEnt-&gt;transformBy(transform);</pre>
<pre style="margin:0em;"> 			pSolidEnt-&gt;close();</pre>
<pre style="margin:0em;"> 			acutPrintf(</pre>
<pre style="margin:0em;"> 				ACRX_T(<span style="color:#a31515">&quot;Solid3d created from mesh !&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(</pre>
<pre style="margin:0em;"> 				ACRX_T(<span style="color:#a31515">&quot;Solid3d creation failed !&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(ACRX_T(<span style="color:#a31515">&quot;Solid3d creation failed !&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pSolid;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Cleanup</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// We do not need the surfaces anymore</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  surfCnt = 0;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000"> (;surfCnt &lt; surfacesArray.length(); surfCnt++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbEntity *pEnt = surfacesArray[surfCnt];</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pEnt;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  postToDb( AcDbEntity* pEnt, AcDbObjectId *pOid) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcDbBlockTable* pBlockTable;</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDb = </pre>
<pre style="margin:0em;"> 		acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> 	pDb-&gt;getBlockTable(pBlockTable, AcDb::kForRead);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecord* pModelSpaceBTR =  NULL; </pre>
<pre style="margin:0em;"> 	pBlockTable-&gt;getAt(</pre>
<pre style="margin:0em;"> 		ACDB_MODEL_SPACE, </pre>
<pre style="margin:0em;"> 		pModelSpaceBTR, </pre>
<pre style="margin:0em;"> 		AcDb::kForWrite);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	AcDbObjectId oid = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pOid != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pModelSpaceBTR-&gt;appendAcDbEntity(*pOid, pEnt);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pModelSpaceBTR-&gt;appendAcDbEntity(oid, pEnt);</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	pEnt-&gt;close(); </pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	pModelSpaceBTR-&gt;close();</pre>
<pre style="margin:0em;"> 	pBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a screenshot of the output</p>

<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07bf01e6970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07bf01e6970d img-responsive" alt="MeshToSolid" title="MeshToSolid" src="/assets/image_693292.jpg" style="margin: 0px 5px 5px 0px;" /></a>
