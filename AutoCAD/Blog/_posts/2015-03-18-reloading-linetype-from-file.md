---
layout: "post"
title: "Reloading linetype from file"
date: "2015-03-18 01:09:05"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/reloading-linetype-from-file.html "
typepad_basename: "reloading-linetype-from-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>The "AcDbDatabase::loadLineTypeFile" can load linetypes from a linetype file. If the linetype with the same name already exists in the database, then the loadLineTypeFile method can return an error code. A way to force the reloading of linetype is to load the linetype in another database and clone the linetype back to the host database to replace it. Here is a code snippet to do that :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbDatabase *pDb = </pre>
<pre style="margin:0em;">     acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> TCHAR szLtFile[MAX_PATH];</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  ( RTNORM != </pre>
<pre style="margin:0em;">     acedFindFile(_T(&quot;TrimCADLinetypes.LIN&quot;), szLtFile) ) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(&quot;\\nLinetype file <span style="color:#0000ff">not</span><span style="color:#000000">  found !&quot;));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbLinetypeTable *pLtTable = NULL;</pre>
<pre style="margin:0em;"> es = pDb-&gt;getLinetypeTable(pLtTable,AcDb::kForRead);</pre>
<pre style="margin:0em;"> ACHAR *szLtype = ACRX_T(&quot;FLATDOTS&quot;);</pre>
<pre style="margin:0em;"> bool isLinetypeLoaded = pLtTable-&gt;has(szLtype);</pre>
<pre style="margin:0em;"> es = pLtTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (isLinetypeLoaded)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	// Already loaded, <span style="color:#0000ff">try</span><span style="color:#000000">  reloading the linetype</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pTempDatabase </pre>
<pre style="margin:0em;">                     = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbDatabase(true, <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> 	es = pTempDatabase-&gt;loadLineTypeFile</pre>
<pre style="margin:0em;">                                  (szLtype, szLtFile);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (Acad::eOk == es)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbLinetypeTable *pTempLtTable;</pre>
<pre style="margin:0em;"> 		AcDbLinetypeTableRecord *pTempLtRec=NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		es = pTempDatabase-&gt;getLinetypeTable(</pre>
<pre style="margin:0em;"> 		                pTempLtTable,AcDb::kForRead);</pre>
<pre style="margin:0em;"> 		AcDbObjectId ltRecId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 		es = pTempLtTable-&gt;getAt(szLtype, ltRecId);</pre>
<pre style="margin:0em;"> 		pTempLtTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectIdArray objIdArray;</pre>
<pre style="margin:0em;"> 		objIdArray.append(ltRecId);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbIdMapping idMap;</pre>
<pre style="margin:0em;"> 		es = pDb-&gt;wblockCloneObjects(</pre>
<pre style="margin:0em;"> 		            objIdArray, </pre>
<pre style="margin:0em;"> 		            pDb-&gt;linetypeTableId(), </pre>
<pre style="margin:0em;">                     idMap, </pre>
<pre style="margin:0em;">                     AcDb::kDrcReplace);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (Acad::eOk == es)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(</pre>
<pre style="margin:0em;"> 			    ACRX_T(&quot;\\nLinetype reloaded !&quot;));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(</pre>
<pre style="margin:0em;">             ACRX_T(&quot;\\nSorry, could <span style="color:#0000ff">not</span><span style="color:#000000">  reload Linetype !&quot;));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(</pre>
<pre style="margin:0em;">         ACRX_T(&quot;\\nError loading linetype <span style="color:#0000ff">from</span><span style="color:#000000">  file !&quot;));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	delete pTempDatabase;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000">  </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span>// <span style="color:#0000ff">Not</span><span style="color:#000000">  loaded, <span style="color:#0000ff">try</span><span style="color:#000000">  loading the linetype</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  ( Acad::eOk == </pre>
<pre style="margin:0em;"> 	    pDb-&gt;loadLineTypeFile(szLtype, szLtFile)) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(</pre>
<pre style="margin:0em;"> 		    ACRX_T(&quot;\\nLinetype loaded <span style="color:#0000ff">from</span><span style="color:#000000">  file !&quot;));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(</pre>
<pre style="margin:0em;"> 		    ACRX_T(&quot;\\nError loading linetype <span style="color:#0000ff">from</span><span style="color:#000000">  file !&quot;));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
