---
layout: "post"
title: "Extract Surface Contours using Civil 3D .NET API"
date: "2012-08-03 03:31:30"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/extract-surface-contours-using-civil-3d-net-api.html "
typepad_basename: "extract-surface-contours-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>ExtractContours</strong>(double<em> interval</em>) is now available in Civil 3D .NET API and this function extracts the surface contour information from the terrain surface at a specified elevation interval.</p>
<p>Overloaded method <strong>ExtractContours</strong>(<em>double interval, ContourSmoothingType smoothType, int smoothFactor</em>) takes additional inputs - <em>ContourSmoothingType&#0160;</em> that Defines how surface contours are smoothed and smoothFactor;&#0160;<em>smoothFactor</em> should be in the range [0, 10]. A value of 10 generates the smoothest contours.&#0160;</p>
<p>Here is a C# code snippet which demonstrates how to extract contours from a TinSurface in Civil 3D :</p>
<p><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Extract contours from the TinSurface</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> contourIds;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> contourInterval = 10.0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">contourIds = surface.ExtractContours(contourInterval);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; contourIds.Count; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> contourId = contourIds[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Contours are lightweight Polyline objects:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> contour = contourId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Let&#39;s set a color and highlight the extracted polylines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; contour.ColorIndex = i + 1;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; contour.Highlight();&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160; &#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743e0a122970d-pi" style="display: inline;"><img alt="Contour" class="asset  asset-image at-xid-6a0167607c2431970b017743e0a122970d" src="/assets/image_ad0787.jpg" title="Contour" /></a><br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
</div>
