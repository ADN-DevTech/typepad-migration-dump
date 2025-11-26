---
layout: "post"
title: "Accessing Surface Boundary vertices using Civil 3D .NET API"
date: "2012-10-26 05:38:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/10/accessing-surface-boundary-vertices-using-civil-3d-net-api.html "
typepad_basename: "accessing-surface-boundary-vertices-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are looking for a way to read the Vertices of Civil 3D Surface boundary object, here is the .NET API which facilitates the same.&#0160;</p>
<p><strong>SurfaceBoundary.Vertices</strong>
Property - &gt; Gets a collection of vertices that make up the boundary. </p>
<p>&#0160;</p>
<p>And here is a
VB.NET code snippet which demonstrates the usage of the same -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> tSurface </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.Civil.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">TinSurface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tSurface = trans.GetObject(idEnt, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> tSurface.BoundariesDefinition.Count &gt; 0 </span><span style="color: blue; line-height: 140%;">Then</span><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> i </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 0 </span><span style="color: blue; line-height: 140%;">to</span><span style="line-height: 140%;"> tSurface.BoundariesDefinition.Count -1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> boundaryName </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = tSurface.BoundariesDefinition.Item(i).Name.ToString() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> surfaceBoundary </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurfaceBoundary</span><span style="line-height: 140%;"> = tSurface.BoundariesDefinition.Item(i).Item(0)&#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> boundaryVerticesPointColl </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3dCollection</span><span style="line-height: 140%;"> = surfaceBoundary.Vertices</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Boundary Name :&quot;</span><span style="line-height: 140%;"> + boundaryName )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> j </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> = 0 </span><span style="color: blue; line-height: 140%;">to</span><span style="line-height: 140%;"> boundaryVerticesPointColl.Count -1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Boundary Vertices :&quot;</span><span style="line-height: 140%;"> + boundaryVerticesPointColl.Item(j).ToString() )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
</div>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee4765cb0970d-pi" style="display: inline;"><img alt="Boundary_Vertices" class="asset  asset-image at-xid-6a0167607c2431970b017ee4765cb0970d" src="/assets/image_065da9.jpg" title="Boundary_Vertices" /></a></p>
<p>Hope this is
useful to you!</p>
