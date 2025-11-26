---
layout: "post"
title: "How to create FDO Feature using Geospatial Platform API in Map 3D?"
date: "2012-05-07 04:25:29"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/how-to-create-fdo-feature-using-geospatial-platform-api-in-map-3d.html "
typepad_basename: "how-to-create-fdo-feature-using-geospatial-platform-api-in-map-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want to know, how to create FDO Feature Object in AutoCAD Map 3D using Geospatial Platform API, here is a VB.NET code snippet which demonstrates creation of FDO Feature in Map 3D (in&#0160;this sample it shows creation of Point Feature like the UI command MAPPointCreate) :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> ed <span style="color: blue;">As</span> <span style="color: #2b91af;">Editor</span> = Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Editor</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;start the transaction</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> trans <span style="color: blue;">As</span> <span style="color: #2b91af;">Transaction</span> = Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; layerName used in the folloiwn gline is specific to a FDO </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Data source which has the same layer. </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layerName <span style="color: blue;">As</span> <span style="color: blue;">String</span> = <span style="color: #a31515;">&quot;park_points&quot;</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> map <span style="color: blue;">As</span> <span style="color: #2b91af;">AcMapMap</span> = <span style="color: #2b91af;">AcMapMap</span>.GetCurrentMap()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layers <span style="color: blue;">As</span> <span style="color: #2b91af;">MgLayerCollection</span> = map.GetLayers()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layer <span style="color: blue;">As</span> <span style="color: #2b91af;">AcMapLayer</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> iCount <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">For</span> iCount = 0 <span style="color: blue;">To</span> (layers.Count)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> (layers.GetItem(iCount).Name.ToLower() = layerName.ToLower()) <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer = layers.GetItem(iCount)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Next</span> iCount</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> layer <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + <span style="color: #a31515;">&quot;Layer Not Found ! &quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> pt <span style="color: blue;">As</span> Autodesk.AutoCAD.Geometry.<span style="color: #2b91af;">Point3d</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> promptPtOp <span style="color: blue;">As</span> <span style="color: #2b91af;">PromptPointOptions</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">PromptPointOptions</span>(vbCrLf + <span style="color: #a31515;">&quot;Select the Location to create a FDO Point : &quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; promptPtOp.AllowNone = <span style="color: blue;">False</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> promptPtRes <span style="color: blue;">As</span> <span style="color: #2b91af;">PromptPointResult</span> = ed.GetPoint(promptPtOp)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> promptPtRes.Status &lt;&gt; <span style="color: #2b91af;">PromptStatus</span>.OK <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;Exiting! Try Again !&quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; pt = promptPtRes.Value</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> geoFact <span style="color: blue;">As</span> <span style="color: #2b91af;">MgGeometryFactory</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgGeometryFactory</span>()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> coord <span style="color: blue;">As</span> <span style="color: #2b91af;">MgCoordinate</span> = geoFact.CreateCoordinateXY(<span style="color: #2b91af;">Convert</span>.ToDouble(pt.X), <span style="color: #2b91af;">Convert</span>.ToDouble(pt.Y))</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> point <span style="color: blue;">As</span> <span style="color: #2b91af;">MgPoint</span> = geoFact.CreatePoint(coord)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> agf <span style="color: blue;">As</span> <span style="color: #2b91af;">MgAgfReaderWriter</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgAgfReaderWriter</span>()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> reader <span style="color: blue;">As</span> <span style="color: #2b91af;">MgByteReader</span> = agf.Write(point)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> propCol <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">MgPropertyCollection</span>()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propCol.Add(<span style="color: blue;">New</span> <span style="color: #2b91af;">MgGeometryProperty</span>(layer.GetFeatureGeometryName(), reader))</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propCol.Add(<span style="color: blue;">New</span> <span style="color: #2b91af;">MgStringProperty</span>(<span style="color: #a31515;">&quot;STNAME&quot;</span>, <span style="color: #a31515;">&quot;Autodesk&quot;</span>))</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> commands <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">MgFeatureCommandCollection</span>()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; commands.Add(<span style="color: blue;">New</span> <span style="color: #2b91af;">MgInsertFeatures</span>(layer.GetFeatureClassName(), propCol))</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer.UpdateFeatures(commands)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Call to SaveFeatureChanges to update the FDO Data Source</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer.SaveFeatureChanges(<span style="color: blue;">New</span> <span style="color: #2b91af;">MgFeatureQueryOptions</span>())</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer.ForceRefresh()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; trans.Commit()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">MgException</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.Message.ToString())</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Finally</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; trans.Dispose()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
</div>
