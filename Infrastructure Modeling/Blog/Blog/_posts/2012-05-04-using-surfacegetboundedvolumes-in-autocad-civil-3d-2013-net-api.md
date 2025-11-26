---
layout: "post"
title: "Using Surface.GetBoundedVolumes() in AutoCAD Civil 3D 2013 .NET API"
date: "2012-05-04 02:01:17"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/using-surfacegetboundedvolumes-in-autocad-civil-3d-2013-net-api.html "
typepad_basename: "using-surfacegetboundedvolumes-in-autocad-civil-3d-2013-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>Surface.GetBoundedVolumes()</strong> is a new inclusion in 2013 release of AutoCAD Civil 3D .NET API, which calculates the volume of a closed area. <em><strong>GetBoundedVolumes(Point3dCollection polygon)</strong></em> takes a <em>Point3dCollection </em>containing points that define the vertices of a closed area and an optional elevation datum (If you do not provide an elevation datum, the method uses 0.0). One point to be noted here, the first and last point in the vertices collection <span style="text-decoration: underline;">must</span> be the same, i.e. the polygon must be closed. GetBoundedVolumes() method returns a <em><strong>SurfaceVolumeInfo </strong></em>object that includes values for net volume, cut volume, and fill volume.&#0160;</p>
<p>Here is a C# code snippet which demonstrates usage of this API function in AutoCAD Civil 3D 2013 :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">// Get the AutoCAD Editor</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">Editor</span> ed = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">//select a polygon</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptEntityOptions</span> selPline = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect a Closed Polyline: &quot;</span>);</p>
<p style="margin: 0px;">selPline.SetRejectMessage(<span style="color: #a31515;">&quot;\nOnly polyline is allowed&quot;</span>);</p>
<p style="margin: 0px;">selPline.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Polyline</span>), <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptEntityResult</span> resPline = ed.GetEntity(selPline);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (resPline.Status != <span style="color: #2b91af;">PromptStatus</span>.OK) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">ObjectId</span> plineId = resPline.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> db = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Database;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> trans = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">CivilDocument</span> civilDoc = <span style="color: #2b91af;">CivilApplication</span>.ActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TinVolumeSurface</span> tinVolSurf = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectIdCollection</span> SurfaceIds = civilDoc.GetSurfaceIds();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> surfaceId <span style="color: blue;">in</span> SurfaceIds)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Autodesk.Civil.DatabaseServices.<span style="color: #2b91af;">Surface</span> oSurface = surfaceId.GetObject(<span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> Autodesk.Civil.DatabaseServices.<span style="color: #2b91af;">Surface</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (oSurface.GetType().ToString()== <span style="color: #a31515;">&quot;Autodesk.Civil.DatabaseServices.TinVolumeSurface&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; tinVolSurf = (<span style="color: #2b91af;">TinVolumeSurface</span>)oSurface;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">break</span>; <span style="color: green;">// In this example we are dealing with the 1st TinVolume Surface</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Polyline</span> pline = trans.GetObject(plineId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Polyline</span>;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point3dCollection</span> pointcoll = <span style="color: blue;">new</span> <span style="color: #2b91af;">Point3dCollection</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; pline.NumberOfVertices; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pointcoll.Add(pline.GetPoint3dAt(i));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// the start point and the end point must be the same. </span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pointcoll.Add(pline.StartPoint);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">SurfaceVolumeInfo</span> svinfo = tinVolSurf.GetBoundedVolumes(pointcoll);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\n*********Bounded Volume Result from API Function call **************&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #2b91af;">String</span>.Format(<span style="color: #a31515;">&quot;\nCut volume: {0}\n Fill volume: {1}\n Net&#0160; volume: {2}\n&quot;</span>, svinfo.Cut, svinfo.Fill, svinfo.Net));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; trans.Commit();</p>
<p style="margin: 0px;">}</p>
</div>
<p><br /><br />And here is a screenshot of Bounded Volume calculation performed using API as well as using the Civil 3D Volumes utility tools:</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676618cc03970b-pi" style="display: inline;"><img alt="C3D2013_Bounded_Vol" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01676618cc03970b image-full" src="/assets/image_0ff041.jpg" title="C3D2013_Bounded_Vol" /></a><br /><br /></p>
