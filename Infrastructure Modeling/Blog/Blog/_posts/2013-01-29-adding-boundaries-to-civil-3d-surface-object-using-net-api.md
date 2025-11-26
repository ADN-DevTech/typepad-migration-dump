---
layout: "post"
title: "Adding boundaries to Civil 3D Surface object using .NET API"
date: "2013-01-29 21:56:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/01/adding-boundaries-to-civil-3d-surface-object-using-net-api.html "
typepad_basename: "adding-boundaries-to-civil-3d-surface-object-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>We can add
boundaries to Civil 3D Surface object using the <strong>SurfaceDefinitionBoundaries
</strong>class; this class encapsulates the boundary operation list for a surface. Note
that, operations are stored in the order they are performed on the surface.
<strong>SurfaceDefinitionBoundaries</strong> class has overloaded function <strong>AddBoundaries()</strong>. In
the following example we will see how to add boundaries to a surface from a
collection of entity ObjectIds using SurfaceOperationAddBoundary
<strong>AddBoundaries<em>(ObjectIdCollection </em></strong><em>boundaryEntities</em><em>&#0160;</em><strong><em>, double </em></strong><em>midOrdinateDistance</em><strong><em>,  SurfaceBoundaryType </em></strong><em>boundaryType</em><strong><em>, bool
</em></strong><em>useNonDestructiveBreakline</em><strong><em> )</em></strong>&#0160;</p>
<p>There are few
important notes on this API mentioned in Civil 3D .NET API Reference document
which I am highlighting below -&#0160;</p>
<ol>
<li>The parameter
useNonDestructiveBreakline is ignored for a GridVolumeSurface or TinSurface
with a DataClip boundary type.</li>
<li>When creating
the DataClip/Outer boundary, the first ObjectId in boundaryEntities is used,
and any other ObjectIds in the collection are ignored.</li>
<li>The first
boundary in the boundaryEntities should be closed when creating a DataClip
boundary.</li>
</ol>
<p>&#0160;</p>
<p>And here is a
C# .NET code snippet on usage of this API -&#0160;</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Add the selected polyline&#39;s ObjectId to a collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> boundaryEntities = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; boundaryEntities.Add(plineId);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Access the BoundariesDefinition object from the surface object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="background-color: #ffff00;"><span style="color: #2b91af; line-height: 140%;">SurfaceDefinitionBoundaries</span><span style="line-height: 140%;"> surfaceBoundaries = surface.BoundariesDefinition;</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// now add the boundary to the surface</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff40;"><span style="line-height: 140%;">&#0160; surfaceBoundaries.AddBoundaries(boundaryEntities, 1.0, </span><span style="color: #2b91af; line-height: 140%;">SurfaceBoundaryType</span><span style="line-height: 140%;">.Outer, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
