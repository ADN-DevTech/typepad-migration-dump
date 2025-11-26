---
layout: "post"
title: "Creating MPolygon object using .NET API"
date: "2012-05-15 03:20:32"
author: "Partha Sarkar"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/creating-mpolygon-object-using-net-api.html "
typepad_basename: "creating-mpolygon-object-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If ever, you were searching for a code snippet to see how you can create MPolygon object using .NET API, your search ends here :)</p>
<p><br />Here is the C# .NET code snippet which demonstrates Mpolygon creation. Make sure to reference <strong>AcMPolygonMGD.dll</strong> assembly in your project.</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Transaction</span> trans = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">BlockTable</span> blockTable = trans.GetObject(db.BlockTableId, Autodesk.AutoCAD.DatabaseServices.<span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockTable</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">BlockTableRecord</span> blockTableRecord = trans.GetObject(blockTable[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace], Autodesk.AutoCAD.DatabaseServices.<span style="color: #2b91af;">OpenMode</span>.ForWrite) <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockTableRecord</span>;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MPolygonLoop</span> mPolygonLoop = <span style="color: blue;">new</span> <span style="color: #2b91af;">MPolygonLoop</span>();</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygonLoop.Add(<span style="color: blue;">new</span> <span style="color: #2b91af;">BulgeVertex</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point2d</span>(2, 2), 0));</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygonLoop.Add(<span style="color: blue;">new</span> <span style="color: #2b91af;">BulgeVertex</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point2d</span>(2, 1), 0));</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygonLoop.Add(<span style="color: blue;">new</span> <span style="color: #2b91af;">BulgeVertex</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point2d</span>(1, 1), 0));</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygonLoop.Add(<span style="color: blue;">new</span> <span style="color: #2b91af;">BulgeVertex</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point2d</span>(1, 2), 0));</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygonLoop.Add(<span style="color: blue;">new</span> <span style="color: #2b91af;">BulgeVertex</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point2d</span>(2, 2), 0));</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MPolygon</span> mPolygon = <span style="color: blue;">new</span> <span style="color: #2b91af;">MPolygon</span>();</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygon.AppendMPolygonLoop(mPolygonLoop, <span style="color: blue;">false</span>, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// set the pattern scale and space</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygon.PatternScale = 0.05;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygon.PatternSpace = 0.05;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">//set the predefined pattern.</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; mPolygon.SetPattern(<span style="color: #2b91af;">HatchPatternType</span>.PreDefined, <span style="color: #a31515;">&quot;ANSI37&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; blockTableRecord.AppendEntity(mPolygon);</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; trans.AddNewlyCreatedDBObject(mPolygon, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; trans.Commit();</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nException: &quot;</span> + ex.Message);</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
