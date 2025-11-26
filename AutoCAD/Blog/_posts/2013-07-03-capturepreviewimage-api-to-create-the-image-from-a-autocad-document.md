---
layout: "post"
title: "CapturePreviewImage : API to create the image from a AutoCAD document"
date: "2013-07-03 00:29:18"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/capturepreviewimage-api-to-create-the-image-from-a-autocad-document.html "
typepad_basename: "capturepreviewimage-api-to-create-the-image-from-a-autocad-document"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
The easy way to create an image from the drawing file is to use the “CapturePreviewImage” API of document. As this is an API of document, you need to open the drawing file in the editor to use this API.
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;CreateImage&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateImage()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//zoom extents... need to use .NET 4.0 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dynamic acad = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.AcadApplication;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acad.ZoomExtents();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Bitmap</span><span style="line-height: 140%;"> image = doc.CapturePreviewImage(376, 292))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//save the image...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; image.Save(</span><span style="color: #a31515; line-height: 140%;">&quot;c:\\temp\\test.png&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; System.Drawing.Imaging.</span><span style="color: #2b91af; line-height: 140%;">ImageFormat</span><span style="line-height: 140%;">.Png);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
