---
layout: "post"
title: "Accessing AcGsView associated with viewport"
date: "2015-03-12 04:03:14"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/accessing-acgsview-associated-with-viewport.html "
typepad_basename: "accessing-acgsview-associated-with-viewport"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In releases prior to AutoCAD 2015, the "acgsGetGsView" method provided access to the AcGsView of a viewport. In AutoCAD 2015, this method is not available and as a replacement, two other methods -&nbsp;"acgsGetCurrentAcGsView" and&nbsp;"acgsGetCurrent3dAcGsView" have been introduced. The sample code in this blog post and the comments are meant to clarify the differences and their usage.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> #include &quot;acgs.h&quot;</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> <span style="color:#008000">// Prior to AutoCAD 2015</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Get the current viewport number</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">struct</span><span style="color:#000000">  <span style="color:#2b91af">resbuf</span><span style="color:#000000">  rb;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  rt = acedGetVar(_T(<span style="color:#a31515">&quot;CVPORT&quot;</span><span style="color:#000000"> ), &amp;rb);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (rt != RTNORM)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(_T(<span style="color:#a31515">&quot;\\nError !&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  vportNum = rb.resval.rint;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Get the GS View associated with the viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGsView *pView1 = acgsGetGsView</pre>
<pre style="margin:0em;">                         (vportNum, <span style="color:#0000ff">false</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> <span style="color:#008000">// If AutoCAD is in a shaded view, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// then pView will be non-null.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (pView1)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span>   <span style="color:#008000">//&#39;re in shaded mode OR </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// a GS view has been already been created </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// and associated with viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     acutPrintf(ACRX_T(<span style="color:#a31515">&quot;We are in shaded mode...&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#008000">//&#39;re in a 2D wireframe or a GsView has not </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// been created for this viewport ...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// To create a GS View and associate with the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// viewport use :</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// AcGsView *pView2 = acgsGetGsView(vportNum, true);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//if(pView2 != NULL)</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//<span style="color:#000000">{</span></span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//    acutPrintf(ACRX_T(&quot;Created a 3D GS View </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//            and associated with viewport..&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//<span style="color:#000000">}</span></span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(<span style="color:#a31515">&quot;We are in 2D wireframe mode...&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// For AutoCAD 2015+</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Get the current viewport number</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">struct</span><span style="color:#000000">  <span style="color:#2b91af">resbuf</span><span style="color:#000000">  rb;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  rt = acedGetVar(_T(<span style="color:#a31515">&quot;CVPORT&quot;</span><span style="color:#000000"> ), &amp;rb);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (rt != RTNORM)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	acutPrintf(_T(<span style="color:#a31515">&quot;\\nError ! &quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">int</span><span style="color:#000000">  vportNum = rb.resval.rint;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Returns a GS View regardless of 2D or 3D</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGsView *pGsView1 = acgsGetCurrentAcGsView(vportNum);</pre>
<pre style="margin:0em;"> ASSERT(pGsView1 != NULL);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Returns a 3D GS view if a view is associated </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// with the viewport. If not this will return null. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// But, a null value does not let us</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// assume it is 2D Wireframe, as a 3d AcGsView can </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// be created and associated with the viewport.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> AcGsView *pGsView2 = acgsGetCurrent3dAcGsView(vportNum);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000"> (pGsView2 != NULL)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//&#39;re in shaded mode OR </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// a 3D GS view has been created and </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// associated with viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(<span style="color:#a31515">&quot;We are in shaded mode...&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//&#39;re in a 2D wireframe and a 3D GS view has </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// not been created yet...</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Lets create a 3D GS View.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// After the 3D GS view is created, both </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// acgsGetCurrentAcGsView and  acgsGetCurrent3dAcGsView </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// will return the newly created GS View.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// To create a GS View and associate it with the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// viewport use :</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">/*</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	AcGsKernelDescriptor desc;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	desc.addRequirement( AcGsKernelDescriptor::k3DDrawing );</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	AcGsView* pView2 = acgsObtainAcGsView(vportNum, desc);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	if(pView2 != NULL)</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	<span style="color:#000000">{</span></span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">		acutPrintf(ACRX_T(&quot;Created a 3D GS View </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">                and associated with viewport..&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	<span style="color:#000000">}</span></span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">	*/</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acutPrintf(ACRX_T(<span style="color:#a31515">&quot;We are in a 2D wireframe mode...&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
