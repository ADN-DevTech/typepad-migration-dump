---
layout: "post"
title: "Get image clip boundary coordinates"
date: "2013-02-16 23:13:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/get-image-clip-boundary-coordinates.html "
typepad_basename: "get-image-clip-boundary-coordinates"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I can set image’s clip by AcadRasterImage.ClipBoundary, but how do I get the AcadRasterImage&#39;s clip boundary coordinates? I know if it is rectangular, I can get its bounding box, width and height. But, if the clip boundary is of a irregular shape, I can&#39;t find any exposed methods to get such data.</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The ObjectARX AcDbRasterImage::clipBoundary() equivalent functionality is not exposed in the current ActiveX Automation Model.</p>
<p>We can work around this limitation by exposing an interface that does exactly that. The whole project is attached:&#0160;
<span class="asset  asset-generic at-xid-6a0167607c2431970b017d3f570be0970c"><a href="http://adndevblog.typepad.com/files/imgclipboundary.zip">Download ImgClipBoundary</a></span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">STDMETHODIMP CClipBoundaryObj::GetImageClipBoundary(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; VARIANT* pVarDblArray, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; VARIANT acadImage)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// TODO: Add your implementation code here</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComQIPtr&lt;IAcadRasterImage&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pAcadImage(acadImage.punkVal);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(!pAcadImage)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> E_POINTER;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CComQIPtr&lt;IAcadBaseObject&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pBaseObj(pAcadImage);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(!pBaseObj)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> E_POINTER;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcDbObjectId id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pBaseObj-&gt;GetObjectId(&amp;id);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcDbObjectPointer&lt;AcDbRasterImage&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pImage(id, AcDb::kForRead);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(pImage.openStatus() != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> E_UNEXPECTED;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGePoint2dArray </span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;"> =        <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pImage-&gt;clipBoundary();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> len = </span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">.length();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(len == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">throw</span><span style="line-height: 140%;"> E_FAIL;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGeMatrix3d mat;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pImage-&gt;getPixelToModelTransform(mat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; mat.inverse();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcAxPoint2dArray pts;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i=0; i&lt;len; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; AcGePoint3d pt(</span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">[i].x, </span><span style="line-height: 140%; color: blue;">array</span><span style="line-height: 140%;">[i].y, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pt.transformBy(mat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.append(AcGePoint2d(pt.x, pt.y));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.setVariant(pVarDblArray);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> HRESULT h)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// for now</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; acutPrintf(_T(</span><span style="line-height: 140%; color: #a31515;">&quot;\nSomething is wrong.&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> h; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> S_OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>To use the ability above,    <br />1. build the attached project, load it in AutoCAD    <br />2.&#0160; open VBA IDE    <br />3. Tools &gt;&gt; Reference: select ARX. now you can see one library is available in the list. </p>
<p>4. Run the VBA code below. It creates an image and set its clip, and gets its clip boundary by the method provided in asdkImgClipBoundary.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3528438b970b-pi"><img alt="image" border="0" height="289" src="/assets/image_803010.jpg" style="display: inline; border: 0px;" title="image" width="390" /></a> </p>
<p><span style="font-size: xx-small;">Sub Example_ClipBoundary()     <br />&#0160;&#0160;&#0160; &#39; This example adds a raster image in model space.      <br />&#0160;&#0160;&#0160; &#39; It then clips the image based on a clip boundary.      <br />&#0160;&#0160;&#0160; &#39; This example uses the &quot;downtown.jpg&quot; found in the sample      <br />&#0160;&#0160;&#0160; &#39; directory. If you do not have the image, or it is located      <br />&#0160;&#0160;&#0160; &#39; in a different directory, insert a valid path and name for the      <br />&#0160;&#0160;&#0160; &#39; imageName variable below.      <br />&#0160;&#0160;&#0160; Dim insertionPoint(0 To 2) As Double </span></p>
<p><span style="font-size: xx-small;">&#0160;&#0160;&#0160; Dim scalefactor As Double     <br />&#0160;&#0160;&#0160; Dim rotationAngle As Double      <br />&#0160;&#0160;&#0160; Dim imageName As String      <br />&#0160;&#0160;&#0160; Dim rasterObj As AcadRasterImage      <br />&#0160;&#0160;&#0160; imageName = &quot;C:\01_SDK\ObjectARX 2012\samples\graphics\AsdkTransientGraphicsSampFolder\Airport-Image.jpg&quot;      <br />&#0160;&#0160;&#0160; insertionPoint(0) = 5#: insertionPoint(1) = 5#: insertionPoint(2) = 0#      <br />&#0160;&#0160;&#0160; scalefactor = 2#      <br />&#0160;&#0160;&#0160; rotationAngle = 0      <br />&#0160;&#0160;&#0160; On Error Resume Next </span></p>
<p><span style="font-size: xx-small;">&#0160;&#0160;&#0160; &#39; Creates a raster image in model space     <br />&#0160;&#0160;&#0160; Set rasterObj = ThisDrawing.ModelSpace.AddRaster(imageName, insertionPoint, scalefactor, rotationAngle)      <br />&#0160;&#0160;&#0160; If Err.Description = &quot;Filer error&quot; Then      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; MsgBox imageName &amp; &quot; could not be found.&quot;      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Exit Sub      <br />&#0160;&#0160;&#0160; End If      <br />&#0160;&#0160;&#0160; ZoomAll      <br />&#0160;&#0160;&#0160; MsgBox &quot;Clip the image?&quot;, , &quot;ClipBoundary Example&quot;      <br />&#0160;&#0160;&#0160; &#39; Establish the clip boundary with an array of points </span></p>
<p><span style="font-size: xx-small;">&#0160;&#0160;&#0160; Dim clipPoints(0 To 9) As Double     <br />&#0160;&#0160;&#0160; clipPoints(0) = 6: clipPoints(1) = 6.75      <br />&#0160;&#0160;&#0160; clipPoints(2) = 7: clipPoints(3) = 6      <br />&#0160;&#0160;&#0160; clipPoints(4) = 6: clipPoints(5) = 5      <br />&#0160;&#0160;&#0160; clipPoints(6) = 5: clipPoints(7) = 6      <br />&#0160;&#0160;&#0160; clipPoints(8) = 6: clipPoints(9) = 6.75      <br />&#0160;&#0160;&#0160; &#39; Clip the image      <br />&#0160;&#0160;&#0160; rasterObj.ClipBoundary clipPoints      <br />&#0160;&#0160;&#0160; &#39; Enable the display of the clip </span></p>
<p><span style="font-size: xx-small;">&#0160;&#0160;&#0160; rasterObj.ClippingEnabled = True     <br />&#0160;&#0160;&#0160; ThisDrawing.Regen acActiveViewport      <br />&#0160;&#0160;&#0160; &#39;MsgBox &quot;The image has been clipped.&quot;, , &quot;ClipBoundary Example&quot;      <br />&#0160;&#0160;&#0160; On Error GoTo 0      <br />&#0160;&#0160;&#0160; Dim xObj As asdkImgClipBoundaryLib.ClipBoundaryObj      <br />&#0160;&#0160;&#0160; Set xObj = ThisDrawing.Application.GetInterfaceObject(&quot;ImgClipBoundary.ClipBoundaryObj.1&quot;)      <br />&#0160;&#0160;&#0160; Dim boundary As Variant      <br />&#0160;&#0160;&#0160; xObj.GetImageClipBoundary boundary, rasterObj      <br />End Sub</span></p>
