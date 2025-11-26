---
layout: "post"
title: "How do I get the geometry of the Watershed area created in AutoCAD Civil 3D?"
date: "2012-05-21 02:10:00"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/how-do-i-get-the-geometry-of-the-watershed-area-created-in-autocad-civil-3d.html "
typepad_basename: "how-do-i-get-the-geometry-of-the-watershed-area-created-in-autocad-civil-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D 2013 .NET API, there is a new function included which extracts the surface watershed information from the terrain surface. <strong>TinSurface.ExtractWatershed()</strong> takes <em>SurfaceExtractionSettingsType</em> as input parameter and it returns <strong>ObjectIdCollection </strong>for entities that can be Polyline3d or Hatch. Here is a C#.NET code snippet which demonstrates usage of TinSurface.ExtractWatershed() :</p>
<p>&#0160;</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">//select a surface</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptEntityOptions</span> selSurface = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect a Tin Surface: &quot;</span>);</p>
<p style="margin: 0px;">selSurface.SetRejectMessage(<span style="color: #a31515;">&quot;\nOnly Tin Surface is allowed&quot;</span>);</p>
<p style="margin: 0px;">selSurface.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">TinSurface</span>), <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptEntityResult</span> resSurface = ed.GetEntity(selSurface);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (resSurface.Status != <span style="color: #2b91af;">PromptStatus</span>.OK) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectId</span> surfaceId = resSurface.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> db = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Database;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> trans = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">TinSurface</span> surface = trans.GetObject(surfaceId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">TinSurface</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">// Extract ExtractWatershed() from the TinSurface</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">// The extracted entities can be Polyline3d or Hatch.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">ObjectIdCollection</span> wsentityIds;</p>
<p style="margin: 0px;">&#0160; &#0160; wsentityIds = surface.ExtractWatershed(Autodesk.Civil.<span style="color: #2b91af;">SurfaceExtractionSettingsType</span>.Plan);</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; wsentityIds.Count; i++)</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">ObjectId</span> entityId = wsentityIds[i];&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">Polyline3d</span> watershedLine = entityId.GetObject(<span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Polyline3d</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (watershedLine != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Access watershedLine Properties&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">Hatch</span> watersheHatch = entityId.GetObject(<span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Hatch</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">if</span> (watersheHatch != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Access watersheHatch Properties</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }&#0160; &#0160; &#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; trans.Commit();</p>
<p style="margin: 0px;">}</p>
</div>
