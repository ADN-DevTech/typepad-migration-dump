---
layout: "post"
title: "Selected point on solid during entity selection"
date: "2015-06-21 23:20:48"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/06/selected-point-on-solid-during-entity-selection.html "
typepad_basename: "selected-point-on-solid-during-entity-selection"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>The acedEntSel method provides a way for the user to select an entity and it also provides the point that was used during the entity selection. When used on a 2D entity, the point returned by acedEntSel is quite useful if you need that information to further work on the entity and perform tasks such as break, trim etc.&nbsp;</p>
<p>However, when acedEntSel is used to select a solid, the point returned is in the UCS XY Plane regardless of where on the solid you pick. if you use an OSNAP while selecting the solid entity, the point will be on the solid. If the point on the solid that was used while selecting the solid is important for doing further processing in your code, here is a way to get that.</p>
<p>In the following code, a ray is created along the viewing direction with which we will find the intersection points of the solid. The code displays all the intersection points as though the ray pierced the solid. But the first intersection point should provide the selection point, in case you are only interested in that.</p>
<p>Please note that, in AutoCAD 2015 and AutoCAD 2016, the "AcBrBrep::getLineContainment" returns lesser number of hits during an intersection test. This behavior has been logged with our engineering team. AutoCAD 2014 and previous releases provide the right number of hits.</p>
<p>Here is a recording which demonstrates the entity selection and the sample code :</p>
<iframe height="200" src="https://screencast.autodesk.com/Embed/4ecb9c0d-200b-4341-b276-704795141d2d" frameborder="0" width="320" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// pt is in UCS coordinates and in the UCS XY plane </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// unless a osnap is used while selecting the entity</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// In that case, the point will be have all </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// the three coordinate values in UCS.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> ads_name  ename;</pre>
<pre style="margin:0em;"> ads_point pt;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  ret = acedEntSel(_T(<span style="color:#a31515">&quot;\\nPick a solid : &quot;</span><span style="color:#000000"> ), ename, pt);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (ret != RTNORM)</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> AcDbObjectId entId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> es = acdbGetObjectId(entId, ename);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbEntity *pEnt = NULL;</pre>
<pre style="margin:0em;"> AcDb3dSolid *pSolid = NULL;</pre>
<pre style="margin:0em;"> es = acdbOpenAcDbEntity(pEnt, entId, AcDb::kForRead);</pre>
<pre style="margin:0em;"> pSolid = AcDb3dSolid::cast(pEnt);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (pSolid == NULL)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(L<span style="color:#a31515">&quot;\\nSelected entity was not a solid.\\n&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Get the WCS coordinates of the selected point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGePoint3d wcsPt = AcGePoint3d::kOrigin;</pre>
<pre style="margin:0em;"> acdbUcs2Wcs(pt, pt, 0);</pre>
<pre style="margin:0em;"> wcsPt = asPnt3d(pt);</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> resbuf viewRb;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcGePoint3d  target = AcGePoint3d::kOrigin; </pre>
<pre style="margin:0em;"> <span style="color:#008000">// TARGET is in UCS</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> acedGetVar(_T(<span style="color:#a31515">&quot;TARGET&quot;</span><span style="color:#000000"> ), &amp;viewRb); </pre>
<pre style="margin:0em;"> target = asPnt3d(viewRb.resval.rpoint);</pre>
<pre style="margin:0em;"> acdbUcs2Wcs(asDblArray(target), asDblArray(target), 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcGeVector3d dir = AcGeVector3d::kIdentity; </pre>
<pre style="margin:0em;"> <span style="color:#008000">// VIEWDIR is in UCS</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> acedGetVar( ACRX_T(<span style="color:#a31515">&quot;VIEWDIR&quot;</span><span style="color:#000000"> ), &amp;viewRb ); </pre>
<pre style="margin:0em;"> dir = asVec3d(viewRb.resval.rpoint);</pre>
<pre style="margin:0em;"> acdbUcs2Wcs(asDblArray(dir), asDblArray(dir), 1);</pre>
<pre style="margin:0em;"> AcGePoint3d  position = target + dir;</pre>
<pre style="margin:0em;"> dir = dir.normalize().negate();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcGeLine3d testRay(</pre>
<pre style="margin:0em;"> 	wcsPt.project(AcGePlane(position, dir), dir.negate()),</pre>
<pre style="margin:0em;"> 	dir);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcBrBrep*pBrep = <span style="color:#0000ff">new</span><span style="color:#000000">  AcBrBrep();</pre>
<pre style="margin:0em;"> pBrep-&gt;setSubentPath( AcDbFullSubentPath</pre>
<pre style="margin:0em;"> 	( pEnt-&gt;objectId(),  AcDbSubentId()));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcBr::ErrorStatus bres;</pre>
<pre style="margin:0em;"> AcBrHit *pHits = NULL;</pre>
<pre style="margin:0em;"> Adesk::UInt32 nHitsFound = 0;</pre>
<pre style="margin:0em;"> <span style="color:#008000">// Find all hits</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> bres = pBrep-&gt;getLineContainment(</pre>
<pre style="margin:0em;"> 	testRay, 0, nHitsFound, pHits );</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (bres == AcBr::eOk)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000"> ( Adesk::UInt32 i = 0; i &lt; nHitsFound; i++) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcBrEntity *pHitEntity = NULL;</pre>
<pre style="margin:0em;"> 		pHits[i].getEntityHit( pHitEntity );</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ( pHitEntity == NULL )</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">continue</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> ( ! pHitEntity-&gt;isKindOf(AcBrBrep::desc()))</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcGePoint3d hitPt;</pre>
<pre style="margin:0em;"> 			pHits[i].getPoint(hitPt);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			AcDbPoint *pDbPt = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbPoint(hitPt);</pre>
<pre style="margin:0em;"> 			PostToDb(pDbPt);</pre>
<pre style="margin:0em;"> 			es = pDbPt-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pHitEntity;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">delete</span><span style="color:#000000">  [] pHits;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">delete</span><span style="color:#000000">  pBrep;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> pEnt-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
