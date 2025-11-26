---
layout: "post"
title: "TinSurfaceTriangle and its vertices direction in AutoCAD Civil 3D"
date: "2013-03-19 01:54:49"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/03/tinsurfacetriangle-and-its-vertices-direction-in-autocad-civil-3d.html "
typepad_basename: "tinsurfacetriangle-and-its-vertices-direction-in-autocad-civil-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Recently I
was working on a project and I had to use <strong>TinSurfaceTriangle</strong> object
to access the Surface triangle vertices. You might be knowing already that <strong>TinSurfaceTriangle
</strong>class encapsulates a triangle in a TinSurface in AutoCAD Civil 3D.
TinSurfaceTriangle type exposes the following members - </p>
<p>&#0160;<strong>Vertex1</strong> - &gt; Gets the first vertex in the
triangle.&#0160; </p>
<p>&#0160;<strong>Vertex2</strong> - &gt; Gets the second vertex in the
triangle.&#0160; </p>
<p>&#0160;<strong>Vertex3</strong> - &gt; Gets the third vertex in the
triangle.</p>
<p>&#0160;</p>
<p>One question
came up during our discussion - in which direction (clock-wise or
anti-clockwise) these vertex points are counted. So, to figure out that I did a
quick test to iterate through the vertices and see which direction they are
returned by Civil 3D API. Here is the result which shows us the direction :</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee989560c970d-pi" style="display: inline;"><img alt="SurfaceTriangle_Direction" class="asset  asset-image at-xid-6a0167607c2431970b017ee989560c970d" src="/assets/image_940d72.jpg" title="SurfaceTriangle_Direction" /></a><br /><br /></p>
<p>&#0160;</p>
<p>And the C# .NET code snippet I used :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurfaceTriangleCollection</span><span style="line-height: 140%;"> tinSurfTrianglColl = surface.GetTriangles(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tinSurfTrianglColl.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">TinSurfaceTriangle</span><span style="line-height: 140%;"> tinsurfTriangle </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> tinSurfTrianglColl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Vertex1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pnt3d = tinsurfTriangle.Vertex1.Location;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex1 ::&#0160; X1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.X.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Y1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Y.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Z1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Z.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Vertex2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; pnt3d = tinsurfTriangle.Vertex2.Location;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex2 ::&#0160; X1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.X.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Y1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Y.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Z1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Z.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Vertex3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; pnt3d = tinsurfTriangle.Vertex3.Location;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Vertex3 ::&#0160; X1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.X.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Y1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Y.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; Z1 : &quot;</span><span style="line-height: 140%;"> + pnt3d.Z.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
