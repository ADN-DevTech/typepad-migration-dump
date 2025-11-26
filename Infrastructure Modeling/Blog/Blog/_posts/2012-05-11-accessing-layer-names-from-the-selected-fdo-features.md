---
layout: "post"
title: "Accessing Layer names from the selected FDO Features"
date: "2012-05-11 03:50:47"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/accessing-layer-names-from-the-selected-fdo-features.html "
typepad_basename: "accessing-layer-names-from-the-selected-fdo-features"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>When you select a FDO feature in Map 3D, you want to know the corresponding layer name as shown in Map 3D display Manager. How to do this using Map 3D Geospatial Platform API ?</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eb6502ab970c-pi" style="display: inline;"><img alt="Img1" class="asset  asset-image at-xid-6a0167607c2431970b0168eb6502ab970c" src="/assets/image_d83211.jpg" title="Img1" /></a></p>
<p><br />We can use the <em><strong>MgSelectionBase.GetLayers()</strong></em> API to get the layers name for the selected FDO features in Map 3D. Here is a C#.NET code snippet for the same:</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green;">// Get the Map Object</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">AcMapMap</span> currentMap = <span style="color: #2b91af;">AcMapMap</span>.GetCurrentMap();</p>
<p style="margin: 0px;"><span style="color: #2b91af;">MgLayerCollection</span> layers = currentMap.GetLayers();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">//select feature on Map</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptSelectionResult</span> selResult = ed.GetSelection();</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (selResult.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">SelectionSet</span> selSet = selResult.Value;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: #2b91af;">MgSelectionBase</span> selectionBase = <span style="color: #2b91af;">AcMapFeatureEntityService</span>.GetSelection(selSet);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">MgLayerBase</span> layer <span style="color: blue;">in</span> selectionBase.GetLayers())</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nFeature selected from Layer :&quot;</span> + layer.Name.ToString());</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
