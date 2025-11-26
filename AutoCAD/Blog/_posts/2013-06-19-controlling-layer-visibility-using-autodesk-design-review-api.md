---
layout: "post"
title: "Controlling Layer Visibility using Autodesk Design Review API"
date: "2013-06-19 04:45:58"
author: "Partha Sarkar"
categories:
  - "ADR API"
  - "DWF"
  - "JavaScript"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/controlling-layer-visibility-using-autodesk-design-review-api.html "
typepad_basename: "controlling-layer-visibility-using-autodesk-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want
to control the layer visibility of a DWF file embedded in a Web Page using <a href="http://usa.autodesk.com/design-review/">Autodesk Design Review</a> JavaScript
API, you can access the Layers property from the DWF Viewer control object and
for a specific layer in the layer list you can set the Visible property to true
or false to show that layer or to switch it off.</p>
<p>
Here is a relevant
JavaScript code snippet –&#0160;</p>
<pre>function LayerON(){ <br /><br /><span style="color: #60bf00;">// Switching a Layer ON</span><br /><span style="color: #60bf00;">// DWFViewer is DwfViewer Object that represents the DWF Viewer control</span></pre>
<pre>DWFViewer.Viewer.Layers.Item(2).<strong>Visible</strong> = <strong>true</strong>;&#0160;</pre>
<pre><br />//<br />}<br /><br />function LayerOFF(){&#0160;</pre>
<pre><br /><span style="color: #60bf00;">// Switching a Layer OFF</span><br /><span style="color: #60bf00;">// DWFViewer is DwfViewer Object that represents the DWF Viewer control</span><br /></pre>
<pre>DWFViewer.Viewer.Layers.Item(2).<strong>Visible</strong> = <strong>false</strong>; &#0160;</pre>
<pre><br />}</pre>
<p>&#0160;</p>
