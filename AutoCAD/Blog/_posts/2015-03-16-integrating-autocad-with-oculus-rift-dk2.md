---
layout: "post"
title: "Integrating AutoCAD with Oculus Rift DK2"
date: "2015-03-16 04:36:58"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/03/integrating-autocad-with-oculus-rift-dk2.html "
typepad_basename: "integrating-autocad-with-oculus-rift-dk2"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p><a href="https://www.oculus.com/dk2">Oculus Rift DK2</a> provides a great virtual reality experience that is very immersive. In this blog post, I have attached a sample project that enables viewing solids from the drawing database for viewing in Oculus Rift. The code makes use of helper methods from the "Win32_DX11AppUtil" of the "OculusRoomTiny" sample which is included in the Oculus SDK. Some of the helper methods have also been modified to extend it for AutoCAD solids. Here is a short video before we look at the sample code.</p>
<iframe height="200" src="https://screencast.autodesk.com/Embed/7fd08170-2410-4b34-aa61-c0a20a3ef528" frameborder="0" width="320" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>
<p>Here is the relevant code snippet that renders the view. The full sample project can be downloaded here : <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0ed3b8c970c img-responsive"><a href="http://adndevblog.typepad.com/files/d3drendersolids.zip">Download OVRAutoCADSolids</a></span></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> CAcModuleResourceOverride resourceOverride;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> gpDX11 = <span style="color:#0000ff">new</span><span style="color:#000000">  DirectX11();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Initializes LibOVR, and the Rift</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> ovrBool ovrInitialized = ovr_Initialize();</pre>
<pre style="margin:0em;"> HMD = ovrHmd_Create(0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (!HMD)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span> </pre>
<pre style="margin:0em;"> 	MessageBoxA(NULL,<span style="color:#a31515">&quot;Oculus Rift not detected.&quot;</span><span style="color:#000000"> ,<span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> , MB_OK); </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (HMD-&gt;ProductName[0] == <span&#39;</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	MessageBoxA(NULL,</pre>
<pre style="margin:0em;">     <span style="color:#a31515">&quot;Rift detected, display not enabled.&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     <span style="color:#a31515">&quot;&quot;</span><span style="color:#000000"> , MB_OK);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">bool</span><span style="color:#000000">  windowed = (HMD-&gt;HmdCaps &amp; ovrHmdCap_ExtendDesktop) </pre>
<pre style="margin:0em;">                         ? <span style="color:#0000ff">false</span><span style="color:#000000">  : <span style="color:#0000ff">true</span><span style="color:#000000"> ; </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> HINSTANCE hInstance = GetModuleHandle(NULL);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> CWinApp *app = acedGetAcadWinApp();</pre>
<pre style="margin:0em;"> CWnd *pWnd = app-&gt;GetMainWnd ();</pre>
<pre style="margin:0em;"> HWND hWndParent = pWnd-&gt;m_hWnd;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> gpDX11-&gt;InitWindowAndDevice(</pre>
<pre style="margin:0em;">                 hInstance, </pre>
<pre style="margin:0em;">                 hWndParent, </pre>
<pre style="margin:0em;">                 Recti(HMD-&gt;WindowsPos, HMD-&gt;Resolution), </pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> EnableWindow(hWndParent, FALSE);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> gpDX11-&gt;SetMaxFrameLatency(1);</pre>
<pre style="margin:0em;"> ovrHmd_AttachToWindow(HMD, gpDX11-&gt;Window, NULL, NULL);</pre>
<pre style="margin:0em;"> ovrHmd_SetEnabledCaps(</pre>
<pre style="margin:0em;">             HMD, </pre>
<pre style="margin:0em;">             ovrHmdCap_LowPersistence | </pre>
<pre style="margin:0em;">             ovrHmdCap_DynamicPrediction);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ovrHmd_ConfigureTracking(HMD, </pre>
<pre style="margin:0em;">             ovrTrackingCap_Orientation | </pre>
<pre style="margin:0em;">             ovrTrackingCap_MagYawCorrection |</pre>
<pre style="margin:0em;">             ovrTrackingCap_Position, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  eye=0; eye&lt;2; eye++)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	Sizei idealSize = ovrHmd_GetFovTextureSize(</pre>
<pre style="margin:0em;">                     HMD, (ovrEyeType)eye,</pre>
<pre style="margin:0em;"> 					HMD-&gt;DefaultEyeFov[eye], 1.0f);</pre>
<pre style="margin:0em;"> 	pEyeRenderTexture[eye] </pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  ImageBuffer(<span style="color:#0000ff">true</span><span style="color:#000000"> , <span style="color:#0000ff">false</span><span style="color:#000000"> , idealSize);</pre>
<pre style="margin:0em;"> 	pEyeDepthBuffer[eye]   </pre>
<pre style="margin:0em;">                 = <span style="color:#0000ff">new</span><span style="color:#000000">  ImageBuffer(</pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">true</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                         <span style="color:#0000ff">true</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">                         pEyeRenderTexture[eye]-&gt;Size);</pre>
<pre style="margin:0em;"> 	EyeRenderViewport[eye].Pos  = Vector2i(0, 0);</pre>
<pre style="margin:0em;"> 	EyeRenderViewport[eye].Size </pre>
<pre style="margin:0em;">                 = pEyeRenderTexture[eye]-&gt;Size;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ovrD3D11Config d3d11cfg;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.Header.API            </pre>
<pre style="margin:0em;">         = ovrRenderAPI_D3D11;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.Header.BackBufferSize </pre>
<pre style="margin:0em;">         = Sizei(HMD-&gt;Resolution.w, HMD-&gt;Resolution.h);</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.Header.Multisample    </pre>
<pre style="margin:0em;">         = 1;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.pDevice               </pre>
<pre style="margin:0em;">         = gpDX11-&gt;Device;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.pDeviceContext        </pre>
<pre style="margin:0em;">         = gpDX11-&gt;Context;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.pBackBufferRT         </pre>
<pre style="margin:0em;">         = gpDX11-&gt;BackBufferRT;</pre>
<pre style="margin:0em;"> d3d11cfg.D3D11.pSwapChain            </pre>
<pre style="margin:0em;">         = gpDX11-&gt;SwapChain;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (!ovrHmd_ConfigureRendering(HMD, &amp;d3d11cfg.Config,</pre>
<pre style="margin:0em;"> 	ovrDistortionCap_Chromatic | ovrDistortionCap_Vignette |</pre>
<pre style="margin:0em;"> 	ovrDistortionCap_TimeWarp | ovrDistortionCap_Overdrive,</pre>
<pre style="margin:0em;"> 	HMD-&gt;DefaultEyeFov, EyeRenderDesc))</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> ovrHmd_DismissHSWDisplay(HMD);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create the scene based on AutoCAD model </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> Acad::ErrorStatus es;</pre>
<pre style="margin:0em;"> AcDbDatabase *pDb </pre>
<pre style="margin:0em;">     = acdbHostApplicationServices()-&gt;workingDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTable *pBlockTable;</pre>
<pre style="margin:0em;"> es = pDb-&gt;getSymbolTable(pBlockTable, AcDb::kForRead);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTableRecord *pMS = NULL;</pre>
<pre style="margin:0em;"> es = pBlockTable-&gt;getAt(</pre>
<pre style="margin:0em;">         ACDB_MODEL_SPACE, pMS, AcDb::kForRead);</pre>
<pre style="margin:0em;"> pBlockTable-&gt;close();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> Scene acadScene;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> AcDbBlockTableRecordIterator *pBTRIterator = NULL;</pre>
<pre style="margin:0em;"> es = pMS-&gt;newIterator(pBTRIterator);</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">if</span><span style="color:#000000">  (es == Acad::eOk)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">for</span><span style="color:#000000">  (pBTRIterator-&gt;start(); </pre>
<pre style="margin:0em;">         ! pBTRIterator-&gt;done(); </pre>
<pre style="margin:0em;">         pBTRIterator-&gt;step())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		AcDbEntity *pEnt = NULL;</pre>
<pre style="margin:0em;">         es = pBTRIterator-&gt;getEntity(</pre>
<pre style="margin:0em;">                         pEnt, AcDb::kForRead);</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (es == Acad::eOk)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			AcDb3dSolid *pSolid = NULL;</pre>
<pre style="margin:0em;"> 			AcDbSubDMesh *pSubDMesh = NULL;</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (pSolid = AcDb3dSolid::cast(pEnt))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span><span style="color:#008000">//solid</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				AcDbFaceterSettings settings;</pre>
<pre style="margin:0em;"> 				settings.faceterMeshType = 2;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcDbExtents sldExtents;</pre>
<pre style="margin:0em;"> 				es = pSolid-&gt;getGeomExtents(sldExtents);</pre>
<pre style="margin:0em;"> 				AcGeVector3d dir = </pre>
<pre style="margin:0em;">                 sldExtents.maxPoint() - sldExtents.minPoint();</pre>
<pre style="margin:0em;"> 				</pre>
<pre style="margin:0em;">                 settings.faceterMaxEdgeLength </pre>
<pre style="margin:0em;">                                 = dir.length() * 0.02;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcGePoint3dArray vertices;</pre>
<pre style="margin:0em;"> 				AcArray&lt;Adesk::Int32&gt; faceArr; </pre>
<pre style="margin:0em;"> 				AcGiFaceData* faceData;</pre>
<pre style="margin:0em;"> 				es = acdbGetObjectMesh(</pre>
<pre style="margin:0em;">                         pSolid, </pre>
<pre style="margin:0em;">                         &amp;settings, </pre>
<pre style="margin:0em;">                         vertices, </pre>
<pre style="margin:0em;">                         faceArr, </pre>
<pre style="margin:0em;">                         faceData);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">if</span><span style="color:#000000">  (faceData) </pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					delete [] faceData-&gt;trueColors(); </pre>
<pre style="margin:0em;"> 					delete [] faceData-&gt;materials();</pre>
<pre style="margin:0em;"> 					delete faceData;</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcArray&lt;MeshVertex&gt; modelVertices;</pre>
<pre style="margin:0em;"> 				AcArray&lt;Adesk::Int32&gt; modelFaceInfo;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">try</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					AcArray&lt;Adesk::Int32&gt; faceVertices;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">int</span><span style="color:#000000">  verticesInFace = 0;</pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">int</span><span style="color:#000000">  facecnt = 0;</pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  x = 0; x &lt; faceArr.length(); </pre>
<pre style="margin:0em;">                         facecnt++, x = x + verticesInFace + 1)</pre>
<pre style="margin:0em;"> 					<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 						faceVertices.removeAll();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 						verticesInFace = faceArr[x];</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  y = x + 1; </pre>
<pre style="margin:0em;">                             y &lt;= x + verticesInFace; y++)</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 							faceVertices.append(faceArr[y]);</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 						Adesk::Boolean continueCollinearCheck </pre>
<pre style="margin:0em;">                                             = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">do</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 							continueCollinearCheck </pre>
<pre style="margin:0em;">                                         = Adesk::kFalse;</pre>
<pre style="margin:0em;"> 							<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  index = 0; </pre>
<pre style="margin:0em;">                             index &lt; faceVertices.length(); </pre>
<pre style="margin:0em;">                             index++)</pre>
<pre style="margin:0em;"> 							<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 								<span style="color:#0000ff">int</span><span style="color:#000000">  v1 = index;</pre>
<pre style="margin:0em;"> 								</pre>
<pre style="margin:0em;">                                 <span style="color:#0000ff">int</span><span style="color:#000000">  v2 = </pre>
<pre style="margin:0em;">                                 (index + 1) &gt;= faceVertices.length() ? </pre>
<pre style="margin:0em;">                                 (index + 1) - faceVertices.length() : </pre>
<pre style="margin:0em;">                                 index + 1;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 								<span style="color:#0000ff">int</span><span style="color:#000000">  v3 = </pre>
<pre style="margin:0em;">                                 (index + 2) &gt;= faceVertices.length() ? </pre>
<pre style="margin:0em;">                                 (index + 2) - faceVertices.length() : </pre>
<pre style="margin:0em;">                                 index + 2;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 								<span style="color:#008000">// Check collinear</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 								AcGePoint3d p1 </pre>
<pre style="margin:0em;">                                 = vertices[faceVertices[v1]];</pre>
<pre style="margin:0em;"> 								</pre>
<pre style="margin:0em;">                                 AcGePoint3d p2 </pre>
<pre style="margin:0em;">                                 = vertices[faceVertices[v2]];</pre>
<pre style="margin:0em;"> 								</pre>
<pre style="margin:0em;">                                 AcGePoint3d p3 </pre>
<pre style="margin:0em;">                                 = vertices[faceVertices[v3]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 								AcGeVector3d vec1 = p1 - p2;</pre>
<pre style="margin:0em;"> 								AcGeVector3d vec2 = p2 - p3;</pre>
<pre style="margin:0em;"> 								<span style="color:#0000ff">if</span><span style="color:#000000">  (vec1.isCodirectionalTo(vec2))</pre>
<pre style="margin:0em;"> 								<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 									faceVertices.removeAt(v2);</pre>
<pre style="margin:0em;"> 									continueCollinearCheck </pre>
<pre style="margin:0em;">                                     = Adesk::kTrue;</pre>
<pre style="margin:0em;"> 									<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 								<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 							<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 						<span style="color:#000000">}</span> <span style="color:#0000ff">while</span><span style="color:#000000">  (continueCollinearCheck);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">if</span><span style="color:#000000">  (faceVertices.length() == 3)</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 							AcGePoint3d p1 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[0]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             AcGePoint3d p2 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[1]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             AcGePoint3d p3 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 							AppendVertex(</pre>
<pre style="margin:0em;">                             modelVertices, </pre>
<pre style="margin:0em;">                             modelFaceInfo, </pre>
<pre style="margin:0em;">                             p1, p2, p3);</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">else</span><span style="color:#000000">  <span style="color:#0000ff">if</span><span style="color:#000000">  (faceVertices.length() == 4)</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 							AcGePoint3d p1 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[0]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             AcGePoint3d p2 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[1]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             AcGePoint3d p3 </pre>
<pre style="margin:0em;">                             = vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 							AppendVertex(</pre>
<pre style="margin:0em;">                             modelVertices, </pre>
<pre style="margin:0em;">                             modelFaceInfo, </pre>
<pre style="margin:0em;">                             p1, p2, p3);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 							p1 = </pre>
<pre style="margin:0em;">                             vertices[faceVertices[2]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             p2 = </pre>
<pre style="margin:0em;">                             vertices[faceVertices[3]];</pre>
<pre style="margin:0em;"> 							</pre>
<pre style="margin:0em;">                             p3 = </pre>
<pre style="margin:0em;">                             vertices[faceVertices[0]];</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 							AppendVertex(</pre>
<pre style="margin:0em;">                             modelVertices, </pre>
<pre style="margin:0em;">                             modelFaceInfo, </pre>
<pre style="margin:0em;">                             p1, p2, p3);</pre>
<pre style="margin:0em;"> 						<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 							acutPrintf(</pre>
<pre style="margin:0em;">                             ACRX_T(<span style="color:#a31515">&quot;Face with more than 4 </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                             vertices will need triangulation </pre>
<pre style="margin:0em;">                             to import <span style="color:#0000ff">in</span><span style="color:#000000">  Direct3D <span style="color:#a31515">&quot;));</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 						<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 					<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">catch</span><span style="color:#000000"> (...)</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					acutPrintf(ACRX_T(<span style="color:#a31515">&quot;Error !!&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcDbHandle handle </pre>
<pre style="margin:0em;">                 = pSolid-&gt;objectId().handle();</pre>
<pre style="margin:0em;"> 				</pre>
<pre style="margin:0em;">                 ACHAR sHandle[50];</pre>
<pre style="margin:0em;"> 				handle.getIntoAsciiBuffer(sHandle);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				AcGePoint3d minPt = sldExtents.minPoint();</pre>
<pre style="margin:0em;"> 				AcGePoint3d maxPt = sldExtents.maxPoint();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				COLORREF colorRef = RGB(255, 255, 255);</pre>
<pre style="margin:0em;"> 				AcCmColor color = pSolid-&gt;color();</pre>
<pre style="margin:0em;"> 				Adesk::UInt8 blue, green, red;</pre>
<pre style="margin:0em;"> 				Adesk::UInt16 ACIindex;</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">long</span><span style="color:#000000">  acirgb, r,g,b; </pre>
<pre style="margin:0em;"> 				<span style="color:#008000">// get the RGB value as an Adesk::Int32</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				Adesk::Int32 nValue = color.color();</pre>
<pre style="margin:0em;"> 				<span style="color:#008000">// now extract the values</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				AcCmEntityColor::ColorMethod cMethod;</pre>
<pre style="margin:0em;"> 				cMethod = color.colorMethod();</pre>
<pre style="margin:0em;"> 				Model::Color modelColor;</pre>
<pre style="margin:0em;"> 				<span style="color:#0000ff">switch</span><span style="color:#000000"> (cMethod)</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">case</span><span style="color:#000000">  AcCmEntityColor::kByColor :</pre>
<pre style="margin:0em;"> 						blue = nValue;</pre>
<pre style="margin:0em;"> 						nValue = nValue&gt;&gt;8;</pre>
<pre style="margin:0em;"> 						green = nValue;</pre>
<pre style="margin:0em;"> 						nValue = nValue&gt;&gt;8;</pre>
<pre style="margin:0em;"> 						red = nValue;</pre>
<pre style="margin:0em;"> 						modelColor.R = red;</pre>
<pre style="margin:0em;"> 						modelColor.G = green;</pre>
<pre style="margin:0em;"> 						modelColor.B = blue;</pre>
<pre style="margin:0em;"> 						colorRef = RGB(red, green, blue);</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">case</span><span style="color:#000000">  AcCmEntityColor::kByACI :</pre>
<pre style="margin:0em;"> 						ACIindex = color.colorIndex(); </pre>
<pre style="margin:0em;"> 						acirgb = acedGetRGB ( ACIindex );</pre>
<pre style="margin:0em;"> 						r = ( acirgb &amp; 0xffL ); </pre>
<pre style="margin:0em;"> 						g = ( acirgb &amp; 0xff00L ) &gt;&gt; 8;</pre>
<pre style="margin:0em;"> 						b = acirgb &gt;&gt; 16;</pre>
<pre style="margin:0em;"> 						modelColor.R = r;</pre>
<pre style="margin:0em;"> 						modelColor.G = g;</pre>
<pre style="margin:0em;"> 						modelColor.B = b;</pre>
<pre style="margin:0em;"> 						colorRef = RGB(r, g, b);</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">case</span><span style="color:#000000">  AcCmEntityColor::kByLayer :</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 					<span style="color:#0000ff">default</span><span style="color:#000000">  :</pre>
<pre style="margin:0em;"> 						<span style="color:#0000ff">break</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 				<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				acadScene.AppendCustomModel(</pre>
<pre style="margin:0em;">                 modelVertices, </pre>
<pre style="margin:0em;">                 modelFaceInfo, </pre>
<pre style="margin:0em;">                 modelColor);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 				modelVertices.removeAll();</pre>
<pre style="margin:0em;"> 				modelFaceInfo.removeAll();</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             pEnt-&gt;close();</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     delete pBTRIterator;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> pMS-&gt;close(); </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">while</span><span style="color:#000000">  (!(gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ] &amp;&amp; </pre>
<pre style="margin:0em;">         gpDX11-&gt;Key[VK_CONTROL]) &amp;&amp; </pre>
<pre style="margin:0em;">         ! gpDX11-&gt;Key[VK_ESCAPE])</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 	gpDX11-&gt;HandleMessages();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	ovrVector3f useHmdToEyeViewOffset[2] = </pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span>EyeRenderDesc[0].HmdToEyeViewOffset,</pre>
<pre style="margin:0em;"> 	EyeRenderDesc[1].HmdToEyeViewOffset<span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;"> 			  </pre>
<pre style="margin:0em;"> 	ovrHmd_BeginFrame(HMD, 0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Keyboard inputs to adjust player orientation</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[VK_LEFT])  Yaw += 0.02f;</pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[VK_RIGHT]) Yaw -= 0.02f;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Keyboard inputs to adjust player position</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ]||gpDX11-&gt;Key[VK_UP])   </pre>
<pre style="margin:0em;">     Pos+=Matrix4f::RotationY(Yaw).</pre>
<pre style="margin:0em;">          Transform(Vector3f(0,0,-0.05f));</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ]||gpDX11-&gt;Key[VK_DOWN]) </pre>
<pre style="margin:0em;">     Pos+=Matrix4f::RotationY(Yaw).</pre>
<pre style="margin:0em;">         Transform(Vector3f(0,0,+0.05f));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ])</pre>
<pre style="margin:0em;">         Pos+=Matrix4f::RotationY(Yaw).</pre>
<pre style="margin:0em;">         Transform(Vector3f(0,+0.05f,0));</pre>
<pre style="margin:0em;"> 	</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ])</pre>
<pre style="margin:0em;">         Pos+=Matrix4f::RotationY(Yaw).</pre>
<pre style="margin:0em;">         Transform(Vector3f(0,-0.05f,0));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ]) </pre>
<pre style="margin:0em;"> 		Pos.y+=0.05f;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (gpDX11-&gt;Key[<span&#39;</span><span style="color:#000000"> ])</pre>
<pre style="margin:0em;"> 		Pos.y-=0.05f;</pre>
<pre style="margin:0em;">   </pre>
<pre style="margin:0em;"> 	ovrPosef temp_EyeRenderPose[2];</pre>
<pre style="margin:0em;"> 	ovrHmd_GetEyePoses(</pre>
<pre style="margin:0em;">             HMD, 0, </pre>
<pre style="margin:0em;">             useHmdToEyeViewOffset, </pre>
<pre style="margin:0em;">             temp_EyeRenderPose, NULL);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Render the two undistorted eye views </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  eye = 0; eye &lt; 2; eye++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		ImageBuffer * useBuffer</pre>
<pre style="margin:0em;">         = pEyeRenderTexture[eye];  </pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         ovrPosef    * useEyePose</pre>
<pre style="margin:0em;">         = &amp;EyeRenderPose[eye];</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">float</span><span style="color:#000000">  * useYaw</pre>
<pre style="margin:0em;">         = &amp;YawAtRender[eye];</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">bool</span><span style="color:#000000">  clearEyeImage  = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">bool</span><span style="color:#000000">  updateEyeImage = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (clearEyeImage)</pre>
<pre style="margin:0em;"> 			gpDX11-&gt;ClearAndSetRenderTarget(</pre>
<pre style="margin:0em;">             useBuffer-&gt;TexRtv,</pre>
<pre style="margin:0em;"> 			pEyeDepthBuffer[eye], </pre>
<pre style="margin:0em;">             Recti(EyeRenderViewport[eye]));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 		<span style="color:#0000ff">if</span><span style="color:#000000">  (updateEyeImage)</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 			*useEyePose = temp_EyeRenderPose[eye];</pre>
<pre style="margin:0em;"> 			*useYaw     = Yaw;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			<span style="color:#008000">// Get view and projection matrices</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 			Matrix4f rollPitchYaw       </pre>
<pre style="margin:0em;">             = Matrix4f::RotationY(Yaw);</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;">             Matrix4f finalRollPitchYaw</pre>
<pre style="margin:0em;">             = rollPitchYaw * Matrix4f(useEyePose-&gt;Orientation);</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;">             Vector3f finalUp</pre>
<pre style="margin:0em;">             = finalRollPitchYaw.Transform(Vector3f(0, 1, 0));</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;">             Vector3f finalForward</pre>
<pre style="margin:0em;">             = finalRollPitchYaw.Transform(Vector3f(0, 0, -1));</pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;">             Vector3f shiftedEyePos</pre>
<pre style="margin:0em;">             = Pos + rollPitchYaw.Transform(useEyePose-&gt;Position);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			Matrix4f view = Matrix4f::LookAtRH(</pre>
<pre style="margin:0em;">             shiftedEyePos, shiftedEyePos + finalForward, </pre>
<pre style="margin:0em;">             finalUp);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			Matrix4f proj = ovrMatrix4f_Projection(</pre>
<pre style="margin:0em;">             EyeRenderDesc[eye].Fov, 0.2f, 1000.0f, <span style="color:#0000ff">true</span><span style="color:#000000"> ); </pre>
<pre style="margin:0em;"> 			</pre>
<pre style="margin:0em;">             Vector4f viewDir = Vector4f(finalForward, 1.0);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 			acadScene.Render(</pre>
<pre style="margin:0em;">             viewDir, </pre>
<pre style="margin:0em;">             view, proj.Transposed());</pre>
<pre style="margin:0em;"> 		<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">// Do distortion rendering, Present and flush/sync</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	ovrD3D11Texture eyeTexture[2]; </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">for</span><span style="color:#000000">  (<span style="color:#0000ff">int</span><span style="color:#000000">  eye = 0; eye&lt;2; eye++)</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">{</span></pre>
<pre style="margin:0em;"> 		eyeTexture[eye].D3D11.Header.API</pre>
<pre style="margin:0em;">         = ovrRenderAPI_D3D11;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         eyeTexture[eye].D3D11.Header.TextureSize</pre>
<pre style="margin:0em;">         = pEyeRenderTexture[eye]-&gt;Size;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         eyeTexture[eye].D3D11.Header.RenderViewport </pre>
<pre style="margin:0em;">         = EyeRenderViewport[eye];</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         eyeTexture[eye].D3D11.pTexture</pre>
<pre style="margin:0em;">         = pEyeRenderTexture[eye]-&gt;Tex;</pre>
<pre style="margin:0em;"> 		</pre>
<pre style="margin:0em;">         eyeTexture[eye].D3D11.pSRView</pre>
<pre style="margin:0em;">         = pEyeRenderTexture[eye]-&gt;TexSv;</pre>
<pre style="margin:0em;"> 	<span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> 	ovrHmd_EndFrame(</pre>
<pre style="margin:0em;">         HMD, </pre>
<pre style="margin:0em;">         EyeRenderPose, </pre>
<pre style="margin:0em;">         &amp;eyeTexture[0].Texture);</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> EnableWindow(hWndParent, TRUE);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Release and close down</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> ovrHmd_Destroy(HMD);</pre>
<pre style="margin:0em;"> ovr_Shutdown();</pre>
<pre style="margin:0em;"> gpDX11-&gt;ReleaseWindow(hInstance);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> delete gpDX11;</pre>
<pre style="margin:0em;"> gpDX11 = NULL;</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>If you are already familiar with the "OculusRoomTiny" sample from the Oculus SDK, the following list of changes will make it easier for you to track the changes :</p>
<p>1. Includes code to add custom solid to the scene based on the meshed solids from AutoCAD</p>
<p>2. Included surface Normal input to the vertex and pixel shader to highlight edges. This works well if the solid color has all components such as R, G and B values.</p>
<p>3. Increased the number of solids in the scene to 20 and generalized it to accept variable number of vertices and indices.</p>
