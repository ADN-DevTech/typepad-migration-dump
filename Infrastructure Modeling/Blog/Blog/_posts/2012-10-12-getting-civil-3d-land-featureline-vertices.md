---
layout: "post"
title: "Getting Civil 3D Land FeatureLine Vertices"
date: "2012-10-12 03:01:32"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/10/getting-civil-3d-land-featureline-vertices.html "
typepad_basename: "getting-civil-3d-land-featureline-vertices"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Civil 3D .NET
API has <strong>FeatureLine</strong> class, but not many functionalities are exposed in the
current release (Civil 3D 2013). On the other hand Civil 3D COM API has
<strong>AeccLandFeatureLine</strong> Object and it has some useful functions exposed. I am
aware, some of our Civil 3D application developers want to access the Land
FeatureLine Vertices and in the absence of equivalent .NET API, I would suggest
to use COM API <strong>IAeccLandFeatureLine:: GetPoints()</strong></p>
<p>&#0160;</p>
<p>Here is a
VB.NET code snippet which demonstrates how to get the Civil 3D Land FeatureLine
Vertices –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> idEnt </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> = promptEntRs.ObjectId&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ent </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(trans.GetObject(idEnt, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite), Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oFtrLn </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AeccLandFeatureLine</span><span style="line-height: 140%;"> =&#0160; </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(ent.AcadObject, Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccLandFeatureLine</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> output </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> points() </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Double</span><span style="line-height: 140%;"> = oFtrLn.GetPoints()</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> varArraySurfacePoints </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span><span style="line-height: 140%;"> = oFtrLn.GetPoints()&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> iPvertices </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Long</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> iPvertices = 0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> (oFtrLn.PointsCount * 3 - 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; output = output &amp; </span><span style="color: #a31515; line-height: 140%;">&quot;Coordinate Values&#0160; :&#0160; &quot;</span><span style="line-height: 140%;"> &amp; points(iPvertices) &amp; vbCrLf &amp; vbLf</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Vertices of Selected Land Feature Line : &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(vbCrLf + output)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee41e167f970d-pi" style="display: inline;"><img alt="LandFeatureLine" class="asset  asset-image at-xid-6a0167607c2431970b017ee41e167f970d" src="/assets/image_9eff72.jpg" title="LandFeatureLine" /></a><br /><br /></span></p>
</div>
