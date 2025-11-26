---
layout: "post"
title: "Synchronizing model space viewports"
date: "2014-07-23 01:40:33"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
  - "UI"
original_url: "https://adndevblog.typepad.com/autocad/2014/07/synchronizing-model-space-viewports.html "
typepad_basename: "synchronizing-model-space-viewports"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to synchronize the view parameters between two model space viewports. For simplicity, this sample code assumes that the drawing already has two vertically split model space viewports of equal width. If the model space viewports are manually resized to different widths, the code resizes them to be of equal width before synchronizing the view parameters.</p>
<p>To get view change notification, the sample code uses the "AcEditorReactor::viewChanged" event handler. Because a view changed notification by pan or mouse wheel does not fire the "viewChanged" notification, we also need to monitor mouse related windows messages using a message filter. After the view change occurs, we will sync the view parameters at the next right oppurtunity by waiting for AutoCAD to reach its quiescent state.</p>
<iframe width="500" height="281" src="http://www.youtube.com/embed/NgriSF-7A00?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;
<p>Here are the relevant code snippets. The full sample project is available for download here : 
<span class="asset  asset-generic at-xid-6a0167607c2431970b01a3fd379580970b img-responsive"><a href="http://adndevblog.typepad.com/files/sampleproject-1.zip"> SampleProject</a></span></p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Command to sync the model space viewport parameters</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  AdskMyTestSyncVTR()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Get the VTR updated </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	acedVports2VportTableRecords();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// We will update the other VTR only if </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// view parameters change</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	Adesk::Boolean updateNeeded = Adesk::kFalse;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDb </pre>
<pre style="margin:0em;"> 		= acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcApDocument *pDoc = acDocManager-&gt;document(pDb);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> ( pDoc == NULL )</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	es = acDocManager-&gt;lockDocument(pDoc);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// This code at present can only deal with 2 Modelspace</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// viewports split vertically in half</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pDb-&gt;tilemode() == Adesk::kFalse)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">struct</span><span style="color:#000000">  resbuf rb;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ads_getvar(_T(<span style="color:#a31515">&quot;cvport&quot;</span><span style="color:#000000"> ), &amp;rb) != RTNORM)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			acutPrintf(_T(<span style="color:#a31515">&quot;\\nError using ads_getvar().\\n&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (rb.resval.rint == 1)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#008000">// Can only work with model space viewports.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbViewportTable *pVT = NULL; </pre>
<pre style="margin:0em;"> 	pDb-&gt;getViewportTable(pVT,AcDb::kForRead);</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Identify the left and right modelspace viewports</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDbViewportTableRecord *pLeftVTR = NULL;</pre>
<pre style="margin:0em;"> 	AcDbViewportTableRecord *pRightVTR = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbViewportTableIterator *pIter = NULL;</pre>
<pre style="margin:0em;"> 	es = pVT-&gt;newIterator(pIter);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">for</span><span style="color:#000000">  (;!pIter-&gt;done();pIter-&gt;step())</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcDbViewportTableRecord *pVTR = NULL;</pre>
<pre style="margin:0em;"> 			es = pIter-&gt;getRecord(pVTR, AcDb::kForRead);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				AcGePoint2d ll = pVTR-&gt;lowerLeftCorner();</pre>
<pre style="margin:0em;"> 				AcGePoint2d ur = pVTR-&gt;upperRightCorner();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">if</span><span style="color:#000000"> (ll.isEqualTo(AcGePoint2d(0, 0)))</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span><span style="color:#008000">// Left modelspace viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					pLeftVTR = pVTR;</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000"> (ur.isEqualTo(AcGePoint2d(1.0, 1.0)))</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span><span style="color:#008000">// Right modelspace viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					pRightVTR = pVTR;</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 					pVTR-&gt;close();</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// If for some reason, we did not have </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// two modelspace viewports, lets stop here.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pLeftVTR == NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (pRightVTR != NULL)</pre>
<pre style="margin:0em;"> 				pRightVTR-&gt;close();</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pRightVTR == NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (pLeftVTR != NULL)</pre>
<pre style="margin:0em;"> 				pLeftVTR-&gt;close();</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Ensure that the two viewports are split </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// vertically in half.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// If not, the view parameters when applied </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// from one to another may not apply directly </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// using this code.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// If the viewports were resized manually, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// we will set them right.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGePoint2d ll1 = pLeftVTR-&gt;lowerLeftCorner();</pre>
<pre style="margin:0em;"> 		AcGePoint2d ur1 = pLeftVTR-&gt;upperRightCorner();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGePoint2d ll2 = pRightVTR-&gt;lowerLeftCorner();</pre>
<pre style="margin:0em;"> 		AcGePoint2d ur2 = pRightVTR-&gt;upperRightCorner();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ll1.isEqualTo(AcGePoint2d(0.0, 0.0)) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pLeftVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pLeftVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pLeftVTR-&gt;setLowerLeftCorner</pre>
<pre style="margin:0em;"> 								(AcGePoint2d(0.0, 0.0));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ur1.isEqualTo(AcGePoint2d(0.5, 1.0)) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pLeftVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pLeftVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pLeftVTR-&gt;setUpperRightCorner</pre>
<pre style="margin:0em;"> 								(AcGePoint2d(0.5, 1.0));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ll2.isEqualTo(AcGePoint2d(0.5, 0.0)) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pRightVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pRightVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pRightVTR-&gt;setLowerLeftCorner</pre>
<pre style="margin:0em;"> 								(AcGePoint2d(0.5, 0.0));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (ur2.isEqualTo(AcGePoint2d(1.0, 1.0)) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pRightVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pRightVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pRightVTR-&gt;setUpperRightCorner</pre>
<pre style="margin:0em;"> 								(AcGePoint2d(1.0, 1.0));</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Get the active model space viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">struct</span><span style="color:#000000">  resbuf res;</pre>
<pre style="margin:0em;"> 		acedGetVar(L<span style="color:#a31515">&quot;CVPORT&quot;</span><span style="color:#000000"> , &amp;res); </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">short</span><span style="color:#000000">  vpnumber = res.resval.rint;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Identify the model space viewports  from/to which</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// settings will be copied.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// The active modelspace viewport is the viewport </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// from which settings will be copied</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbViewportTableRecord *pFromVTR = NULL;</pre>
<pre style="margin:0em;"> 		AcDbViewportTableRecord *pToVTR = NULL;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pLeftVTR-&gt;number() == vpnumber)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			pFromVTR = pLeftVTR;</pre>
<pre style="margin:0em;"> 			pToVTR = pRightVTR;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pRightVTR-&gt;number() == vpnumber)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			pFromVTR = pRightVTR;</pre>
<pre style="margin:0em;"> 			pToVTR = pLeftVTR;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Sorry, we did not identify the active viewport </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// from which settings need to be copied.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR == NULL || pToVTR == NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Copy the VTR settings from one modelspace viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// to another only if they are different. We will use</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// a tolerance to ensure very small differences do not</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// get us in a soup. I meant loop :)</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcGeTol newTol;</pre>
<pre style="margin:0em;"> 		newTol.setEqualPoint (0.00001);</pre>
<pre style="margin:0em;"> 		newTol.setEqualVector(0.00001);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// ViewDirection</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGeVector3d fromViewDir = pFromVTR-&gt;viewDirection();</pre>
<pre style="margin:0em;"> 		AcGeVector3d toViewDir = pToVTR-&gt;viewDirection();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR-&gt;viewDirection().isEqualTo(</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;viewDirection(), newTol) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setViewDirection(</pre>
<pre style="margin:0em;"> 							pFromVTR-&gt;viewDirection()); </pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// ViewTwist</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;viewTwist() </pre>
<pre style="margin:0em;"> 						- pToVTR-&gt;viewTwist()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setViewTwist(pFromVTR-&gt;viewTwist());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Target</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR-&gt;target().isEqualTo(</pre>
<pre style="margin:0em;"> 					pToVTR-&gt;target(), newTol) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setTarget(pFromVTR-&gt;target()); </pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// BackClipEnabled</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR-&gt;backClipEnabled() </pre>
<pre style="margin:0em;"> 						!= pToVTR-&gt;backClipEnabled())</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setBackClipEnabled(</pre>
<pre style="margin:0em;"> 						pFromVTR-&gt;backClipEnabled());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// BackClipDistance</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;backClipDistance() </pre>
<pre style="margin:0em;"> 				- pToVTR-&gt;backClipDistance()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setBackClipDistance(</pre>
<pre style="margin:0em;"> 						pFromVTR-&gt;backClipDistance());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// FrontClipEnabled</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR-&gt;frontClipEnabled() </pre>
<pre style="margin:0em;"> 						!= pToVTR-&gt;frontClipEnabled())</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setFrontClipEnabled(</pre>
<pre style="margin:0em;"> 						pFromVTR-&gt;frontClipEnabled());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// FrontClipDistance</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;frontClipDistance() </pre>
<pre style="margin:0em;"> 					- pToVTR-&gt;frontClipDistance()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setFrontClipDistance(</pre>
<pre style="margin:0em;"> 							pFromVTR-&gt;frontClipDistance());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Elevation</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;elevation() </pre>
<pre style="margin:0em;"> 							- pToVTR-&gt;elevation()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setElevation(pFromVTR-&gt;elevation());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// centerPoint</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pFromVTR-&gt;centerPoint().isEqualTo(</pre>
<pre style="margin:0em;"> 					pToVTR-&gt;centerPoint(), newTol) == <span style="color:#0000ff">false</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setCenterPoint(pFromVTR-&gt;centerPoint()); </pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Height</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;height() - pToVTR-&gt;height()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setHeight(pFromVTR-&gt;height());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Width</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (abs(pFromVTR-&gt;width() - pToVTR-&gt;width()) &gt; 0.01)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (! pToVTR-&gt;isWriteEnabled())</pre>
<pre style="margin:0em;"> 				pToVTR-&gt;upgradeOpen();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			pToVTR-&gt;setWidth(pFromVTR-&gt;width());</pre>
<pre style="margin:0em;"> 			updateNeeded = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Done with the VTR</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		pLeftVTR-&gt;close();</pre>
<pre style="margin:0em;"> 		pRightVTR-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pIter;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	es = pVT-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	es = acDocManager-&gt;unlockDocument(pDoc);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Update the Vports if we did change any of </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// the VTR parameters</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (updateNeeded)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acedVportTableRecords2Vports();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Viewchanged notification is not received during </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// a pan or zoom using mouse wheel.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// So identify those using WM messages.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> BOOL WinCallBack(MSG *pMsg)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (	pMsg-&gt;message == WM_VSCROLL		 || </pre>
<pre style="margin:0em;"> 		pMsg-&gt;message == WM_HSCROLL		 ||</pre>
<pre style="margin:0em;"> 		pMsg-&gt;message == WM_MOUSEWHEEL   ||</pre>
<pre style="margin:0em;"> 		pMsg-&gt;message == WM_MBUTTONUP)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Sync the modelspace viewports</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		acDocManager-&gt;sendStringToExecute(</pre>
<pre style="margin:0em;"> 			acDocManager-&gt;mdiActiveDocument(), </pre>
<pre style="margin:0em;"> 			ACRX_T(<span style="color:#a31515">&quot;SyncVTR &quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> ); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  FALSE;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Setting up reactors and message filter</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">virtual</span><span style="color:#000000">  AcRx::AppRetCode On_kInitAppMsg (<span style="color:#0000ff">void</span><span style="color:#000000">  *pkt) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcRx::AppRetCode retCode </pre>
<pre style="margin:0em;"> 				= AcRxArxApp::On_kInitAppMsg (pkt) ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Editor reactor to receive to ViewChanged notification</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	pEditorReactor = <span style="color:#0000ff">new</span><span style="color:#000000">  AcMyEditorReactor(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// InputContext reactor to receive quiescent state</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// change notification</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	pInputContextReactor = <span style="color:#0000ff">new</span><span style="color:#000000">  AcMyInputContextReactor();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Viewchanged notification is not received during a </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// pan or zoom using mouse wheel.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// So identify those using WM messages.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	acedRegisterFilterWinMsg(WinCallBack);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  (retCode);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Cleanup of the reactors and message filter</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">virtual</span><span style="color:#000000">  AcRx::AppRetCode On_kUnloadAppMsg (<span style="color:#0000ff">void</span><span style="color:#000000">  *pkt) </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcRx::AppRetCode retCode </pre>
<pre style="margin:0em;"> 				=AcRxArxApp::On_kUnloadAppMsg (pkt) ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// cleanup</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pEditorReactor)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pEditorReactor;</pre>
<pre style="margin:0em;"> 		pEditorReactor = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pInputContextReactor)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">delete</span><span style="color:#000000">  pInputContextReactor;</pre>
<pre style="margin:0em;"> 		pInputContextReactor = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acedRemoveFilterWinMsg(WinCallBack);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  (retCode);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Overridden beginQuiescentState method to initiate a sync</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  AcMyInputContextReactor::beginQuiescentState()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// See if the view has changed</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (_viewChanged)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Sync the view once</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		_viewChanged = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Send a command to Sync the modelspace viewports</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		acDocManager-&gt;sendStringToExecute(</pre>
<pre style="margin:0em;"> 			acDocManager-&gt;mdiActiveDocument(), </pre>
<pre style="margin:0em;"> 			ACRX_T(<span style="color:#a31515">&quot;SyncVTR &quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">false</span><span style="color:#000000"> , <span style="color:#0000ff">true</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> ); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Overridden viewChanged method to know if the view changed</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">void</span><span style="color:#000000">  AcMyEditorReactor::viewChanged()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// View changed notification is received several times</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// and we only want to sync the view parameters when </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// AutoCAD attains a quiescent state next time.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// So only flaggin to sync in </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// InputContextReactor::beginQuiescentState event handler.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcMyInputContextReactor::_viewChanged = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
