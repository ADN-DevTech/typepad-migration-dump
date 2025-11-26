---
layout: "post"
title: "Exporting dynamic block from current drawing as an authoring element"
date: "2015-04-29 22:17:07"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/exporting-dynamic-block-from-current-drawing-as-an-authoring-element.html "
typepad_basename: "exporting-dynamic-block-from-current-drawing-as-an-authoring-element"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Dynamic blocks from a drawing can be wblocked to a new drawing, as an authoring element. This helps archive the dynamic blocks and reuse them when required. The new drawing can be inserted to get the dynamic block into any other drawing. In AutoCAD UI, an authoring element can be created by using the WBLOCK command and selecting a dynamic block from the list of available ones from the current drawing. The same can also be done using code as shown in the following code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Wblock a dynamic block as an AuthoringElement</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcApDocument *pActiveDoc </pre>
<pre style="margin:0em;"> 	= acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin:0em;"> AcDbDatabase *pCurDb = pActiveDoc-&gt;database();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTable* pCurentDwgBlockTable;</pre>
<pre style="margin:0em;"> es = pCurDb-&gt;getBlockTable(</pre>
<pre style="margin:0em;"> 	pCurentDwgBlockTable, kForRead);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  ( es == eOk) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecord* pRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Assuming &quot;Test&quot; dynamic block</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// being present in the current drawing</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	es = pCurentDwgBlockTable-&gt;getAt(</pre>
<pre style="margin:0em;"> 		ACRX_T(<span style="color:#a31515">&quot;Test&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		pRecord, </pre>
<pre style="margin:0em;"> 		kForRead, <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (es != eOk) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pCurentDwgBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId btrId = pRecord-&gt;objectId();</pre>
<pre style="margin:0em;"> 	pRecord-&gt;close();</pre>
<pre style="margin:0em;"> 	pCurentDwgBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Create the destination database</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDbDatabase *pNewDb = NULL;</pre>
<pre style="margin:0em;"> 	es = pCurDb-&gt;wblock(pNewDb, btrId);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		es = pNewDb-&gt;saveAs(</pre>
<pre style="margin:0em;"> 			_T(<span style="color:#a31515">&quot;D://Temp//TestBlock.dwg&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pNewDb;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
