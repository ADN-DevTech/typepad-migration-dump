---
layout: "post"
title: "Export Civil 3D Corridor Feature Lines to Polyline using.NET API"
date: "2012-07-04 02:23:37"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/export-civil-3d-corridor-feature-lines-to-polyline-usingnet-api.html "
typepad_basename: "export-civil-3d-corridor-feature-lines-to-polyline-usingnet-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are looking for a way to export Civil 3D Corridor Feature Lines to Polyline using .NET API, you are at the right place! :)&#0160;</p>
<p>Following code snippet demonstrates how to export corridor Feature Lines to Polyline using Civil 3D .NET API :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Using</span><span style="line-height: 140%;"> (trans)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; get the Corridor Object. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39;&#39; In this Case I am getting a specific Corridor from Civil 3D Tutorial DWG - &quot;Corridor-5c.dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; corridor = trans.GetObject(civilDoc.CorridorCollection.Item(</span><span style="color: #a31515; line-height: 140%;">&quot;Corridor - (1)&quot;</span><span style="line-height: 140%;">), </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">, </span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> bLine </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> corridor.Baselines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; fTLCollMap = bLine.MainBaselineFeatureLines.FeatureLineCollectionMap</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> fTLColl </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> fTLCollMap</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> corFTL </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> fTLColl</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; fTLPointColl = corFTL.FeatureLinePoints</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ReDim</span><span style="line-height: 140%;"> fTLPArray(0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> fTLPointColl.Count * 3 - 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> i = 0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> fTLPointColl.Count - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; fTLPoint = fTLPointColl.Item(i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; pt3d = fTLPoint.XYZ</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; fTLPArray(i * 3) = pt3d.X</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; fTLPArray(i * 3 + 1) = pt3d.Y</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; fTLPArray(i * 3 + 2) = pt3d.Z&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span><span style="line-height: 140%;"> i</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">&#39; Draw the Polyline line</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline = acadDoc.ModelSpace.AddPolyline(fTLPArray)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline.LinetypeGeneration = </span><span style="color: blue; line-height: 140%;">True</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline.Lineweight = </span><span style="color: #2b91af; line-height: 140%;">ACAD_LWEIGHT</span><span style="line-height: 140%;">.acLnWt050</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> col </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcadAcCmColor</span><span style="line-height: 140%;"> = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">AcadAcCmColor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; col.ColorIndex = </span><span style="color: #2b91af; line-height: 140%;">AcColor</span><span style="line-height: 140%;">.acCyan</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline.TrueColor = col</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline.Update()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; pline.Highlight(</span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit()</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Using</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
