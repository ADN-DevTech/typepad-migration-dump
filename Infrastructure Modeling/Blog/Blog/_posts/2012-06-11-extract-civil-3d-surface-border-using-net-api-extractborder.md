---
layout: "post"
title: "Extract Civil 3D Surface border using .NET API ExtractBorder()  - Part I"
date: "2012-06-11 05:14:22"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/extract-civil-3d-surface-border-using-net-api-extractborder.html "
typepad_basename: "extract-civil-3d-surface-border-using-net-api-extractborder"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D 2013 .NET API, <strong>ExtractBorder(</strong>SurfaceExtractionSettingsType&#0160;&#0160; <em>settingsType</em><strong>)</strong> is a new addition which extracts the surface border from the terrain surface.</p>
<p>You need to specify <em><strong>SurfaceExtractionSettingsType.Plan</strong></em> to extract the border information using the plan visual style settings, or <em><strong>SurfaceExtractionSettingsType.Model</strong></em> to use the model settings.</p>
<p>The extracted entities can be Polyline, Polyline3d, or Face. If the surface has no border information, this method returns an empty ObjectIdCollection.</p>
<p>Here is C# .NET code snippet which demonstrates usage of ExtractBorder() :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Extract Border from the TinSurface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// The extracted entities can be Polyline, Polyline3d, or Face.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> entityIds;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; entityIds = surface.ExtractBorder(Autodesk.Civil.</span><span style="color: #2b91af; line-height: 140%;">SurfaceExtractionSettingsType</span><span style="line-height: 140%;">.Plan);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; entityIds.Count; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> entityId = entityIds[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (entityId.ObjectClass == </span><span style="color: #2b91af; line-height: 140%;">RXClass</span><span style="line-height: 140%;">.GetClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Polyline3d</span><span style="line-height: 140%;">)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Polyline3d</span><span style="line-height: 140%;"> border = entityId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline3d</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Do what you want with the extrated 3d-polyline</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
</div>
