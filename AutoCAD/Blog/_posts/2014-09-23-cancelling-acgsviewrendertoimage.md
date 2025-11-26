---
layout: "post"
title: "Cancelling AcGsView::RenderToImage"
date: "2014-09-23 04:11:38"
author: "Balaji"
categories:
  - "2014"
  - "2015"
  - "AutoCAD OEM"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2014/09/cancelling-acgsviewrendertoimage.html "
typepad_basename: "cancelling-acgsviewrendertoimage"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To interrupt and cancel the AcGsView::RenderToImage call while its doing the rendering, create a custom progress monitor class. In the class derived from AcGsRenderProgressMonitor, override the OnProgress method and return true to stop the RenderToImage. AutoCAD UI may not react to mouse events when the rendering is underway. To ensure that the AutoCAD UI is responsive during the rendering and to let the user cancel the operation, pump the Windows messages from OnProgress method.</p>
<p>Here is a sample code snippet :</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">#include</span><span style="color:#000000">  <span style="color:#a31515">&quot;acgsRender.h&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">class</span><span style="color:#000000">  MyRenderProgressMonitor :</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">public</span><span style="color:#000000">  AcGsRenderProgressMonitor</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000"> :</pre>
<pre style="margin:0em;"> 	MyRenderProgressMonitor(<span style="color:#0000ff">void</span><span style="color:#000000"> )<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	~MyRenderProgressMonitor(<span style="color:#0000ff">void</span><span style="color:#000000"> )<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">static</span><span style="color:#000000">  Adesk::Boolean gAbortRender;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  OnProgress(Phase ePhase, </pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">float</span><span style="color:#000000">  fProgress)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// Pump windows message to let AutoCAD</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// respond to user interaction. </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// User may want to cancel the rendering</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		CWinApp *app = acedGetAcadWinApp();</pre>
<pre style="margin:0em;"> 		CWnd *wnd = app-&gt;GetMainWnd ();</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		MSG msg; </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">while</span><span style="color:#000000">  (::PeekMessage (&amp;msg, wnd-&gt;m_hWnd, </pre>
<pre style="margin:0em;"> 							0, 0, PM_NOREMOVE)) </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000">  (!app-&gt;PumpMessage()) </pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">break</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		LONG lIdle = 0;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">while</span><span style="color:#000000">  (app-&gt;OnIdle (lIdle++));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (gAbortRender)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  OnTile(<span style="color:#0000ff">int</span><span style="color:#000000">  x, <span style="color:#0000ff">int</span><span style="color:#000000">  y, <span style="color:#0000ff">int</span><span style="color:#000000">  w, </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">int</span><span style="color:#000000">  h, <span style="color:#0000ff">const</span><span style="color:#000000">  BYTE* pPixels)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SetStatistics(</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">const</span><span style="color:#000000">  AcGsRenderStatistics* pStats)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">virtual</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  ShouldReuseDatabase()</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span><span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Adesk::Boolean </pre>
<pre style="margin:0em;"> 	MyRenderProgressMonitor::gAbortRender</pre>
<pre style="margin:0em;"> 							= Adesk::kFalse;</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<p>To use it, create an instance of the progress monitor class and use it with RenderToImage as in this code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// pView is an AcGsView </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// screenRect is an AcGsDCRect</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> MyRenderProgressMonitor pm;</pre>
<pre style="margin:0em;"> AcDbMentalRayRenderSettings mentalRayRenderer;</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">bool</span><span style="color:#000000">  ok = pView-&gt;RenderToImage(</pre>
<pre style="margin:0em;"> 						&amp;imgSource, </pre>
<pre style="margin:0em;"> 						&amp;mentalRayRenderer, </pre>
<pre style="margin:0em;"> 						&amp;pm, </pre>
<pre style="margin:0em;"> 						screenRect);</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
