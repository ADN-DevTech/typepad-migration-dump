---
layout: "post"
title: "Read FDO Features using Map 3D Geospatial Platform API"
date: "2012-05-17 02:02:48"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/read-fdo-features-using-map-3d-geospatial-platform-api.html "
typepad_basename: "read-fdo-features-using-map-3d-geospatial-platform-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are looking for a way to access the FDO features and read the feature attributes in Map 3D using Geospatial Platform API, then the following C#.NET code snippet might be useful to you. This code snippet shows how to use the Geospatial Platform API in AutoCAD Map 3D and access the FDO Features.</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">// Get the Map Object</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcMapMap</span> currentMap = <span style="color: #2b91af;">AcMapMap</span>.GetCurrentMap();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Prompt user to Select Feature in Map</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptSelectionOptions</span> psop = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptSelectionOptions</span>();</p>
<p style="margin: 0px;">psop.MessageForAdding = <span style="color: #a31515;">&quot;Select the FDO Feature in Map 3D to read Data : &quot;</span>;</p>
<p style="margin: 0px;">psop.SingleOnly = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptSelectionResult</span> psResult = ed.GetSelection(psop);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (psResult.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">SelectionSet</span> selSet = psResult.Value;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">// Get Map Selectionset from AutoCAD SelectionSet</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">MgSelectionBase</span> mapSelBase = <span style="color: #2b91af;">AcMapFeatureEntityService</span>.GetSelection(selSet);</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">AcMapLayer</span> mapLayer = <span style="color: #2b91af;">AcMapFeatureEntityService</span>.GetLayer(psResult.Value[0].ObjectId);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: green;">//Get the ID of the selected Parcel</span></p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">MgFeatureReader</span> ftrRdr = mapSelBase.GetSelectedFeatures(mapLayer, mapLayer.FeatureClassName, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">while</span> (ftrRdr.ReadNext())</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">MgClassDefinition</span> cd = ftrRdr.GetClassDefinition();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">//this is just an example; change this according to your FDO data field</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">String</span> ownerName = ftrRdr.GetString(<span style="color: #a31515;">&quot;STNAME&quot;</span>);</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\n Parcel Owner Name :&quot;</span> + ownerName.ToString());</p>
<p style="margin: 0px;">&#0160; &#0160; }&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">trans.Commit();</p>
</div>
