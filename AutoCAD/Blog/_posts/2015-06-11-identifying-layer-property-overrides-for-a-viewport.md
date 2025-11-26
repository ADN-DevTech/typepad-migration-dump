---
layout: "post"
title: "Identifying layer property overrides for a viewport"
date: "2015-06-11 03:42:38"
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
original_url: "https://adndevblog.typepad.com/autocad/2015/06/identifying-layer-property-overrides-for-a-viewport.html "
typepad_basename: "identifying-layer-property-overrides-for-a-viewport"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to identify the layers that have their properties overridden for a viewport and to list the property overrides. The "AcDbLayerTableRecord::hasAnyOverrides" should quickly let us know if there are any overrides for any of the viewports. If it does exist, we can get to the details using "AcDbLayerTableRecord::hasOverrides" and provide the ObjectId of the viewport for which we want to know the overrides.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbDatabase *pDb </pre>
<pre style="margin:0em;"> 	= acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> ads_point pt;</pre>
<pre style="margin:0em;"> ads_name ename;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  ( RTNORM != </pre>
<pre style="margin:0em;"> 	acedEntSel(ACRX_T(<span style="color:#a31515">&quot;Select a viewport :&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 	ename, pt))</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectId vpid = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  ( Acad::eOk != acdbGetObjectId(vpid, ename ))</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbObjectId layerId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> AcDbLayerTable* pLayerTable;</pre>
<pre style="margin:0em;"> pDb-&gt;getSymbolTable(pLayerTable, AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbLayerTableIterator *pIter = NULL;</pre>
<pre style="margin:0em;"> AcDbLayerTableRecord *pLTR = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> pLayerTable-&gt;newIterator(pIter);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">for</span><span style="color:#000000">  (;! pIter-&gt;done(); pIter-&gt;step()) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	es = pIter-&gt;getRecord(pLTR, AcDb::kForRead);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (! pLTR-&gt;hasAnyOverrides())</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pLTR-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">continue</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (! pLTR-&gt;hasOverrides(vpid))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pLTR-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">continue</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	TCHAR *lname;</pre>
<pre style="margin:0em;"> 	pLTR-&gt;getName(lname);</pre>
<pre style="margin:0em;"> 	acutPrintf(L<span style="color:#a31515">&quot;\\n Layer : %s has the following overrides :&quot;</span><span style="color:#000000"> ,</pre>
<pre style="margin:0em;"> 		lname);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">bool</span><span style="color:#000000">  isOverriddenForVP = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 	AcCmColor clr = pLTR-&gt;color(vpid, isOverriddenForVP);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (isOverriddenForVP)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(L<span style="color:#a31515">&quot;\\n Color override&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					</pre>
<pre style="margin:0em;"> 	isOverriddenForVP = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 	AcDbObjectId ltypeId </pre>
<pre style="margin:0em;"> 		= pLTR-&gt;linetypeObjectId(</pre>
<pre style="margin:0em;"> 		vpid, isOverriddenForVP);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (isOverriddenForVP)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(L<span style="color:#a31515">&quot;\\n Linetype override&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	isOverriddenForVP = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 	AcDb::LineWeight lw </pre>
<pre style="margin:0em;"> 		= pLTR-&gt;lineWeight</pre>
<pre style="margin:0em;"> 		(vpid, isOverriddenForVP);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (isOverriddenForVP)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(L<span style="color:#a31515">&quot;\\n Lineweight override&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					</pre>
<pre style="margin:0em;"> 	isOverriddenForVP = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 	ACHAR *psName = pLTR-&gt;plotStyleName</pre>
<pre style="margin:0em;"> 		(vpid, isOverriddenForVP);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (isOverriddenForVP)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(L<span style="color:#a31515">&quot;\\n Plotstyle override&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	pLTR-&gt;close();</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">delete</span><span style="color:#000000">  pIter;</pre>
<pre style="margin:0em;"> pLayerTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
