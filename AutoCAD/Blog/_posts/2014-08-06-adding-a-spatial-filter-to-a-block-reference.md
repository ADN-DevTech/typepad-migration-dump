---
layout: "post"
title: "Adding a spatial filter to a block reference"
date: "2014-08-06 05:01:48"
author: "Madhukar Moogala"
categories:
  - "2015"
  - "AutoCAD"
  - "Madhukar Moogala"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/08/adding-a-spatial-filter-to-a-block-reference.html "
typepad_basename: "adding-a-spatial-filter-to-a-block-reference"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>This blog post is minor tweak to the existing <a href="http://adndevblog.typepad.com/autocad/2013/03/xclip-xrefs-using-objectarx.html">post</a> written by my colleague <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html">Xiaodong</a> back in 2013, with the recent development changes internally, the code flow is causing crash in ACAD 2015 -32 machines, to avoid such mishap and to have seamless code across both the machines I have re written the code.</p>
<p>Reason for new change in the code :</p>
<p>The existing code sets the filter&#39;s definition (including clip boundary) before the filter is added to the database.&#0160; That causes the wrong type of internal clip boundary object to be created.&#0160; There are two types of clip boundaries possible, one is only for use in databases that are open in the Acad editor and the other is for databases that are not open in the Acad editor.&#0160;If the filter doesn&#39;t have a database, then we create the clip boundary type that is for non-editor databases.&#0160;Then, if the filter is added to a database that is open in the editor, it has the wrong type of clip boundary and that causes problems.</p>
<p>&#0160;</p>
<div style="font-family: Consolas; font-size: 9pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">void</span> Test_Clip ()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ads_point</span> pt1, pt2;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ads_name</span> ent;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (acedEntSel(<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;Select xref:&quot;</span>), ent, pt1) != <span style="color: #6f008a;">RTNORM</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectId</span> idXref;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (acdbGetObjectId(idXref,ent) != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbObjectPointer</span>&lt;<span style="color: #2b91af;">AcDbBlockReference</span>&gt; pRef(idXref, <span style="color: #2b91af;">AcDb</span>::<span style="color: #2f4f4f;">kForRead</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pRef.openStatus() != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>) {</p>
<p style="margin: 0px;">acutPrintf(<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;Not an xref!\n&quot;</span>));</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint2dArray</span> pts;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (acedGetPoint(<span style="color: #6f008a;">NULL</span>,<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;First point:&quot;</span>), pt1) != <span style="color: #6f008a;">RTNORM</span>) {</p>
<p style="margin: 0px;">pRef-&gt;close();</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: green;">//the ECS of the vertices must be defined in the</span></p>
<p style="margin: 0px;"><span style="color: green;">//coordinate system of the _block_ so let&#39;s calculate</span></p>
<p style="margin: 0px;"><span style="color: green;">//transform all points to that coordinate system</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeMatrix3d</span> mat(pRef-&gt;blockTransform());</p>
<p style="margin: 0px;">mat.invert();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGePoint3d</span> pt3d(asPnt3d(pt1));</p>
<p style="margin: 0px;">pt3d.transformBy(mat);</p>
<p style="margin: 0px;">pts.append(<span style="color: #2b91af;">AcGePoint2d</span>(pt3d.x, pt3d.y));</p>
<p style="margin: 0px;"><span style="color: blue;">while</span> (acedGetPoint(pt1,<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;Next point:&quot;</span>), pt2) == <span style="color: #6f008a;">RTNORM</span>) {</p>
<p style="margin: 0px;">acedGrDraw(pt1, pt2, 1, 1);</p>
<p style="margin: 0px;">pt3d = asPnt3d(pt2);</p>
<p style="margin: 0px;">pt3d.transformBy(mat);</p>
<p style="margin: 0px;">pts.append(<span style="color: #2b91af;">AcGePoint2d</span>(pt3d.x, pt3d.y));</p>
<p style="margin: 0px;">memcpy(pt1, pt2, <span style="color: blue;">sizeof</span>(<span style="color: #2b91af;">ads_point</span>));</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">acedRedraw(<span style="color: #6f008a;">NULL</span>,0);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbDatabase</span>* pDb = acdbHostApplicationServices()-&gt;workingDatabase();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcGeVector3d</span> normal;</p>
<p style="margin: 0px;"><span style="color: blue;">double</span> elev;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pDb-&gt;tilemode()) {</p>
<p style="margin: 0px;">normal = pDb-&gt;ucsxdir().crossProduct(pDb-&gt;ucsydir());</p>
<p style="margin: 0px;">elev = pDb-&gt;elevation();</p>
<p style="margin: 0px;">} <span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">normal = pDb-&gt;pucsxdir().crossProduct(pDb-&gt;pucsydir());</p>
<p style="margin: 0px;">elev = pDb-&gt;pelevation();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">normal.normalize();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Acad</span>::<span style="color: #2b91af;">ErrorStatus</span> es = pRef.object()-&gt;upgradeOpen();</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (es != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>) {</p>
<p style="margin: 0px;">pRef-&gt;close();</p>
<p style="margin: 0px;"><span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: green;">//create the filter</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcDbSpatialFilter</span>* pFilter = <span style="color: blue;">new</span> <span style="color: #2b91af;">AcDbSpatialFilter</span>;</p>
<p style="margin: 0px;"><span style="color: green;">//add it to the extension dictionary of the block reference</span></p>
<p style="margin: 0px;"><span style="color: green;">//the AcDbIndexFilterManger class provides convenient utility functions</span></p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (AcDbIndexFilterManager::addFilter(pRef.object(), pFilter) != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>)</p>
<p style="margin: 0px;"><span style="color: blue;">delete</span> pFilter;</p>
<p style="margin: 0px;"><span style="color: blue;">else</span> {</p>
<p style="margin: 0px;">acutPrintf(<span style="color: #6f008a;">_T</span>(<span style="color: #a31515;">&quot;Filter has been succesfully added!\n&quot;</span>));</p>
<p style="margin: 0px;">pRef.object()-&gt;downgradeOpen();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (pFilter-&gt;setDefinition(pts,normal,elev,</p>
<p style="margin: 0px;"><span style="color: #6f008a;">ACDB_INFINITE_XCLIP_DEPTH</span>,-<span style="color: #6f008a;">ACDB_INFINITE_XCLIP_DEPTH</span>, <span style="color: blue;">true</span>) != <span style="color: #2b91af;">Acad</span>::<span style="color: #2f4f4f;">eOk</span>)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">acutPrintf(L<span style="color: #a31515;">&quot;Filter setDefinition failed.&quot;</span>);</p>
<p style="margin: 0px;"><span style="color: green;">//remove the filter if setDefinition fails.</span></p>
<p style="margin: 0px;">pFilter-&gt;erase();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">es = pFilter-&gt;erase();</p>
<p style="margin: 0px;">pFilter-&gt;close();</p>
<p style="margin: 0px;">pRef-&gt;close();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
