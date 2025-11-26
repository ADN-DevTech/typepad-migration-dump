---
layout: "post"
title: "Rendering using RenderToImage API "
date: "2015-09-24 00:08:37"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/09/rendering-using-rendertoimage-api-.html "
typepad_basename: "rendering-using-rendertoimage-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>As you may already know, the API for the graphics system changed in AutoCAD 2015 as explained in this blog post : <a href="http://adndevblog.typepad.com/autocad/2014/04/graphic-changes-in-autocad-2015.html">Graphic changes in AutoCAD 2015</a>. Also, AutoCAD 2016 renders using the RapidRT renderer that replaced the MentalRay renderer that was used by the previous AutoCAD releases. To account for this, the "AcDbRapidRTRenderSettings" class was introduced in ObjectARX 2016. All these changes requires changes in your code if you rely on "AcGsView::RenderToImage" method to generate an image of an AutoCAD model.</p>
<p>Here is a code that should help generate the rendered image in previous releases and in AutoCAD 2016. To accommodate the various graphics system changes and the renderer, the below code makes extensive use of conditional compilation.</p>
<p></p>
<p>To try this, open a drawing and set the viewing direction in AutoCAD based on how you wish to generate the rendered image. Setup the rendering presets and render it in AutoCAD. After you are convinced with the results, make the render settings as current. This should save the render presets as Active. The above code, retrieves the render settings based on the active render preset and uses it to generate the rendered image.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  AdskMyTestRTITest(<span style="color:#0000ff">void</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDb </pre>
<pre style="margin:0em;"> 		= acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGsManager *gsManager = acgsGetGsManager();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (! gsManager)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#ifdef</span><span style="color:#000000">  ACAD2016 <span style="color:#008000">// 2016 and later uses RapidRT renderer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsManager2 *gsManager2 </pre>
<pre style="margin:0em;"> 			= <span style="color:#0000ff">dynamic_cast</span><span style="color:#000000"> &lt;AcGsManager2 *&gt;(gsManager);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! gsManager2)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		AcGsKernelDescriptor descriptor;</pre>
<pre style="margin:0em;"> 		descriptor.addRequirement(</pre>
<pre style="margin:0em;"> 			AcGsKernelDescriptor::k3DRapidRTRendering);</pre>
<pre style="margin:0em;"> 		AcGsGraphicsKernel *pGraphicsKernel </pre>
<pre style="margin:0em;"> 			= AcGsManager::acquireGraphicsKernel(descriptor);</pre>
<pre style="margin:0em;"> 		AcGsDevice *offDevice </pre>
<pre style="margin:0em;"> 			= gsManager2-&gt;getOffScreenDevice(*pGraphicsKernel); </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! offDevice)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#elif</span><span style="color:#000000">  ACAD2015 </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// 2015 uses Mental Ray renderer. Includes GS API changes </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsKernelDescriptor descriptor;</pre>
<pre style="margin:0em;"> 		descriptor.addRequirement</pre>
<pre style="margin:0em;"> 			(AcGsKernelDescriptor::k3DDrawing);</pre>
<pre style="margin:0em;"> 		AcGsGraphicsKernel *pGraphicsKernel </pre>
<pre style="margin:0em;"> 			= AcGsManager::acquireGraphicsKernel(descriptor);</pre>
<pre style="margin:0em;"> 		AcGsDevice *offDevice </pre>
<pre style="margin:0em;"> 			= pGraphicsKernel-&gt;createOffScreenDevice();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! offDevice)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#else</span><span style="color:#000000">  <span style="color:#008000">// 2014 and earlier releases uses Mental ray renderer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsClassFactory *factory </pre>
<pre style="margin:0em;"> 			= gsManager-&gt;getGSClassFactory();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! factory)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		AcGsDevice *offDevice </pre>
<pre style="margin:0em;"> 			= factory-&gt;createOffScreenDevice();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! offDevice)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObjectId curVportId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  width = 10, height = 10;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	Adesk::IntDbId viewportObjectId;</pre>
<pre style="margin:0em;"> 	LONG_PTR acadWindowId;</pre>
<pre style="margin:0em;"> 	LONG_PTR viewportId;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (pDb-&gt;tilemode()) </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">// Modelspace</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		curVportId = acedActiveViewportId();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">struct</span><span style="color:#000000">  resbuf rb;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  rt = acedGetVar(_T(<span style="color:#a31515">&quot;CVPORT&quot;</span><span style="color:#000000"> ), &amp;rb);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (rt != RTNORM)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  vportNum = rb.resval.rint;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbViewportTableRecord&gt; </pre>
<pre style="margin:0em;"> 			curVTR (curVportId,AcDb::kForRead);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (curVTR.openStatus() == Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			curVTR.close();</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  l,r,b,t;</pre>
<pre style="margin:0em;"> 		acgsGetViewportInfo(vportNum,l,b,r,t);</pre>
<pre style="margin:0em;"> 		height = t - b - 1;</pre>
<pre style="margin:0em;"> 		width = r - l - 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		viewportObjectId = curVportId.asOldId();</pre>
<pre style="margin:0em;"> 		acadWindowId = vportNum;</pre>
<pre style="margin:0em;"> 		viewportId = curVportId.asOldId();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000">  </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span><span style="color:#008000">// Paperspace, but with a modelspace </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		<span style="color:#008000">// activated in viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		curVportId = acedGetCurViewportObjectId();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectPointer&lt;AcDbViewport&gt; </pre>
<pre style="margin:0em;"> 			curVport (curVportId,AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (curVport-&gt;number() &lt; 2)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AfxMessageBox(_T(<span style="color:#a31515">&quot;For Render to work, </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				modelspace in a viewport must be activated.<span style="color:#a31515">&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">int</span><span style="color:#000000">  l,r,b,t;</pre>
<pre style="margin:0em;"> 		acgsGetViewportInfo(curVport-&gt;number(),l,b,r,t);</pre>
<pre style="margin:0em;"> 		height = t - b - 1;</pre>
<pre style="margin:0em;"> 		width = r - l - 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		viewportObjectId = curVportId.asOldId();</pre>
<pre style="margin:0em;"> 		acadWindowId = curVport-&gt;number();</pre>
<pre style="margin:0em;"> 		viewportId = curVportId.asOldId();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	offDevice-&gt;onSize(width, height);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#ifdef</span><span style="color:#000000">  ACAD2016</pre>
<pre style="margin:0em;"> 		AcGsClientViewInfo info;</pre>
<pre style="margin:0em;"> 		info.viewportId = viewportId;</pre>
<pre style="margin:0em;"> 		info.acadWindowId = acadWindowId;</pre>
<pre style="margin:0em;"> 		info.viewportObjectId = viewportObjectId;</pre>
<pre style="margin:0em;"> 		AcGsView* pView = </pre>
<pre style="margin:0em;"> 		gsManager2-&gt;getOffScreenView(*pGraphicsKernel, info);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#elif</span><span style="color:#000000">  ACAD2015</pre>
<pre style="margin:0em;"> 		AcGsView *pView = pGraphicsKernel-&gt;createView();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! pView)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsView *pView = factory-&gt;createView();</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! pView)</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	acgsGetViewParameters(acadWindowId, pView);</pre>
<pre style="margin:0em;"> 	offDevice-&gt;setDeviceRenderer(AcGsDevice::kFullRender);</pre>
<pre style="margin:0em;"> 	offDevice-&gt;add(pView);</pre>
<pre style="margin:0em;"> 	offDevice-&gt;update();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#if</span><span style="color:#000000">  <span style="color:#0000ff">defined</span><span style="color:#000000"> (ACAD2016) || <span style="color:#0000ff">defined</span><span style="color:#000000"> (ACAD2015)</pre>
<pre style="margin:0em;"> 		AcGsModel *pModel </pre>
<pre style="margin:0em;"> 		= gsManager-&gt;createAutoCADModel(*pGraphicsKernel);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsModel *pModel = gsManager-&gt;createAutoCADModel();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (! pModel)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Model space</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTable *pBT = NULL;</pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecord *pBTR = NULL;</pre>
<pre style="margin:0em;"> 	AcDbObjectId msId;</pre>
<pre style="margin:0em;"> 	pDb-&gt;getBlockTable(pBT, AcDb::kForRead);</pre>
<pre style="margin:0em;"> 	pBT-&gt;getAt(ACDB_MODEL_SPACE, msId);</pre>
<pre style="margin:0em;"> 	pBT-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbBlockTableRecordPointer spaceRec(msId, AcDb::kForRead);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (spaceRec.openStatus() != Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	pView-&gt;add(spaceRec, pModel);</pre>
<pre style="margin:0em;"> 	spaceRec.close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (pView != NULL)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		pView-&gt;invalidate();</pre>
<pre style="margin:0em;"> 		pView-&gt;update();</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;">               </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// get the filename to output</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">struct</span><span style="color:#000000">  resbuf *result = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">int</span><span style="color:#000000">  status = acedGetFileNavDialog(</pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;Render Image&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		NULL, </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;jpg;png;tif;bmp&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 		_T(<span style="color:#a31515">&quot;RenderImageDialog&quot;</span><span style="color:#000000"> ), 1, &amp;result);  </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (status == RTNORM)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		ACHAR *pFileName = result-&gt;resval.rstring;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (! CreateAtilImage(</pre>
<pre style="margin:0em;"> 			pView, width, height, 32, 0, pFileName))</pre>
<pre style="margin:0em;"> 		AfxMessageBox(_T(<span style="color:#a31515">&quot;Failed to create image...&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// now do the various GS clean up ops</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	pView-&gt;eraseAll();</pre>
<pre style="margin:0em;"> 	offDevice-&gt;erase(pView);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#if</span><span style="color:#000000">  <span style="color:#0000ff">defined</span><span style="color:#000000"> (ACAD2016)</pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//pGraphicsKernel-&gt;deleteView(pView);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		pGraphicsKernel-&gt;deleteModel(pModel);</pre>
<pre style="margin:0em;"> 		<span style="color:#008000">//pGraphicsKernel-&gt;deleteDevice(offDevice);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcGsManager::releaseGraphicsKernel(pGraphicsKernel);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#elif</span><span style="color:#000000">  ACAD2015</pre>
<pre style="margin:0em;"> 		pGraphicsKernel-&gt;deleteView(pView);</pre>
<pre style="margin:0em;"> 		pGraphicsKernel-&gt;deleteModel(pModel);</pre>
<pre style="margin:0em;"> 		pGraphicsKernel-&gt;deleteDevice(offDevice);</pre>
<pre style="margin:0em;"> 		AcGsManager::releaseGraphicsKernel(pGraphicsKernel);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		factory-&gt;deleteView(pView);</pre>
<pre style="margin:0em;"> 		factory-&gt;deleteModel(pModel);</pre>
<pre style="margin:0em;"> 		factory-&gt;deleteDevice(offDevice);</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#ifdef</span><span style="color:#000000">  ACAD2016</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">   Acad::ErrorStatus GetActiveRapidRTRenderSetting</pre>
<pre style="margin:0em;"> 	(AcDbRapidRTRenderSettings *&amp;pRenderSetting)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcApDocument *pActiveDoc = acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbDictionary *pNODContainer = NULL;</pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es </pre>
<pre style="margin:0em;"> 		= pDB-&gt;getNamedObjectsDictionary</pre>
<pre style="margin:0em;"> 		(pNODContainer, </pre>
<pre style="margin:0em;"> 		AcDb::OpenMode::kForRead); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObject *pMyDictObject = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pNODContainer-&gt;has(</pre>
<pre style="margin:0em;"> 		ACRX_T(<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_RAPIDRT_SETTINGS&quot;</span><span style="color:#000000"> )))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		es = pNODContainer-&gt;getAt(ACRX_T(</pre>
<pre style="margin:0em;"> 			<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_RAPIDRT_SETTINGS&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 			pMyDictObject, AcDb::OpenMode::kForRead); </pre>
<pre style="margin:0em;"> 		AcDbObjectId myObjectId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcDbRapidRTRenderSettings *pRapidRTActiveSetting </pre>
<pre style="margin:0em;"> 				= AcDbRapidRTRenderSettings::cast(pMyDictObject);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (pRapidRTActiveSetting != NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				pRenderSetting = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbRapidRTRenderSettings();</pre>
<pre style="margin:0em;"> 				es = pRenderSetting-&gt;copyFrom(pRapidRTActiveSetting);</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			es = pRapidRTActiveSetting-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(</pre>
<pre style="margin:0em;"> 		ACRX_T(<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_RAPIDRT_SETTINGS not found !!&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	pNODContainer-&gt;close();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">   Acad::ErrorStatus GetActiveRenderSetting</pre>
<pre style="margin:0em;"> 	(AcDbMentalRayRenderSettings *&amp;pRenderSetting)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	AcApDocument *pActiveDoc = acDocManager-&gt;mdiActiveDocument();</pre>
<pre style="margin:0em;"> 	AcDbDatabase *pDB = pActiveDoc-&gt;database();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbDictionary *pNODContainer = NULL;</pre>
<pre style="margin:0em;"> 	Acad::ErrorStatus es = pDB-&gt;getNamedObjectsDictionary</pre>
<pre style="margin:0em;"> 		(pNODContainer, AcDb::OpenMode::kForRead); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcDbObject *pMyDictObject = NULL;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000"> (pNODContainer-&gt;has(ACRX_T(<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_SETTINGS&quot;</span><span style="color:#000000"> )))</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		es = pNODContainer-&gt;getAt(</pre>
<pre style="margin:0em;"> 			ACRX_T(<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_SETTINGS&quot;</span><span style="color:#000000"> ), </pre>
<pre style="margin:0em;"> 			pMyDictObject, AcDb::OpenMode::kForRead); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		AcDbObjectId myObjectId = AcDbObjectId::kNull;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (es == Acad::eOk)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcDbMentalRayRenderSettings </pre>
<pre style="margin:0em;"> 				*pMentalRayActiveSetting = </pre>
<pre style="margin:0em;"> 				AcDbMentalRayRenderSettings::cast(pMyDictObject);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">if</span><span style="color:#000000"> (pMentalRayActiveSetting != NULL)</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 				pRenderSetting = <span style="color:#0000ff">new</span><span style="color:#000000">  AcDbMentalRayRenderSettings();</pre>
<pre style="margin:0em;"> 				es = pRenderSetting-&gt;copyFrom(pMentalRayActiveSetting);</pre>
<pre style="margin:0em;"> 			<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 			es = pMentalRayActiveSetting-&gt;close();</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		acutPrintf(ACRX_T(<span style="color:#a31515">&quot;ACAD_RENDER_ACTIVE_SETTINGS not found !!&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	pNODContainer-&gt;close();</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  Acad::eOk;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  CreateAtilImage(AcGsView *pView, </pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">int</span><span style="color:#000000">  width, <span style="color:#0000ff">int</span><span style="color:#000000">  height, </pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">int</span><span style="color:#000000">  colorDepth, <span style="color:#0000ff">int</span><span style="color:#000000">  paletteSize, </pre>
<pre style="margin:0em;"> 						ACHAR *pFileName)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">bool</span><span style="color:#000000">  done = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	AcGsDCRect screenRect(0,width-1,0, height-1);</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// we want colorDepth to be either 24 or 32</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (colorDepth &lt; 24)</pre>
<pre style="margin:0em;"> 		colorDepth = 24;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (colorDepth &gt; 24)</pre>
<pre style="margin:0em;"> 		colorDepth = 32;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// create rbgmodel 32 bit true color</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	Atil::RgbModel rgbModel(colorDepth);</pre>
<pre style="margin:0em;"> 	Atil::ImagePixel initialColor(rgbModel.pixelType());</pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// create the Atil image on the stack</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	Atil::Image imgSource</pre>
<pre style="margin:0em;"> 		(Atil::Size(width, height), </pre>
<pre style="margin:0em;"> 		&amp;rgbModel, initialColor);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">bool</span><span style="color:#000000">  ok = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#ifdef</span><span style="color:#000000">  ACAD2016 <span style="color:#008000">// 2016 uses Rapid RT renderer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbRapidRTRenderSettings </pre>
<pre style="margin:0em;"> 			*pCurrentSetting = NULL;</pre>
<pre style="margin:0em;"> 		Acad::ErrorStatus es </pre>
<pre style="margin:0em;"> 			= GetActiveRapidRTRenderSetting(pCurrentSetting);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pCurrentSetting != NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			ok = pView-&gt;renderToImage</pre>
<pre style="margin:0em;"> 				(&amp;imgSource, pCurrentSetting, </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">nullptr</span><span style="color:#000000"> , screenRect);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">delete</span><span style="color:#000000">  pCurrentSetting;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#else</span><span style="color:#000000">  <span style="color:#008000">// 2015 and earlier releases uses Mental Ray renderer</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 		AcDbMentalRayRenderSettings *pCurrentSetting = NULL;</pre>
<pre style="margin:0em;"> 		Acad::ErrorStatus es </pre>
<pre style="margin:0em;"> 			= GetActiveRenderSetting(pCurrentSetting);</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000"> (pCurrentSetting != NULL)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			ok = pView-&gt;RenderToImage</pre>
<pre style="margin:0em;"> 				(&amp;imgSource, pCurrentSetting, </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">nullptr</span><span style="color:#000000"> , screenRect);</pre>
<pre style="margin:0em;"> 			<span style="color:#0000ff">delete</span><span style="color:#000000">  pCurrentSetting;</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">#endif</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (!ok)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AfxMessageBox(_T(<span style="color:#a31515">&quot;Failed to RenderToImage&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		done = WriteImageToFile(&amp;imgSource, pFileName); </pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000">  done;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>The sample project and a drawing with render presets configured can be downloaded here :</p>
<p></p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08772717970d img-responsive"><a href="http://adndevblog.typepad.com/files/rti.zip">Download RTI</a></span>
<p></p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c7d2f893970b img-responsive"><a href="http://adndevblog.typepad.com/files/torus_2016.dwg">Download Torus_2016</a></span>
<p></p>
<p>Here is a screenshot of the rendered image that was created in AutoCAD 2016 using the RapidRT renderer :</p>
<a class="asset-img-link"   href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d2f8af970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d2f8af970b img-responsive" alt="Torus_2016" title="Torus_2016" src="/assets/image_771718.jpg" style="margin: 0px 5px 5px 0px;" /></a>
