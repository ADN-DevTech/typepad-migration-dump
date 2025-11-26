---
layout: "post"
title: "Creating AutoCAD SelectionSet from FDO Features"
date: "2012-07-13 02:47:33"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/creating-autocad-selectionset-from-fdo-features.html "
typepad_basename: "creating-autocad-selectionset-from-fdo-features"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Many a times I have come across a question : How do I create an AutoCAD selection set from the Geospatial Platform selection set (MgSelectionBase) containing FDO features ?</p>
<p>&#0160;</p>
<p>Here is a C# code snippet demonstrating the steps -&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgLayerCollection</span><span style="line-height: 140%;"> layers = currentMap.GetLayers();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> layer = layers.GetItem(</span><span style="color: #a31515; line-height: 140%;">&quot;Parcels&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgFeatureService</span><span style="line-height: 140%;"> fs = </span><span style="color: #2b91af; line-height: 140%;">AcMapServiceFactory</span><span style="line-height: 140%;">.GetService(</span><span style="color: #2b91af; line-height: 140%;">MgServiceType</span><span style="line-height: 140%;">.FeatureService) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureService</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> fsId = layer.GetFeatureSourceId();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> className = layer.GetFeatureClassName();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;"> query = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Parcels.shp file is used</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">query.SetFilter(</span><span style="color: #a31515; line-height: 140%;">&quot;FeatId = 166&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;"> resId = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;">(fsId);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> featureReader = fs.SelectFeatures(resId, className, query);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgSelectionBase</span><span style="line-height: 140%;"> selectionSet = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgSelectionBase</span><span style="line-height: 140%;">(currentMap);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//Add all features in the feature reader to the newly constructed selection set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selectionSet.AddFeatures(layer, featureReader, 0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapFeatureEntityService</span><span style="line-height: 140%;">.HighlightFeatures(selectionSet);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// create an AutoCAD selection</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SelectionSet</span><span style="line-height: 140%;"> acadSelSet = </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureEntityService</span><span style="line-height: 140%;">.AddFeaturesToSelectionSet(</span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, selectionSet);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
<p>Hope this is useful to you!</p>
