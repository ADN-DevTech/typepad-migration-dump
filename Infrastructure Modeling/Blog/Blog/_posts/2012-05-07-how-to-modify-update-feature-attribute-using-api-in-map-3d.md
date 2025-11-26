---
layout: "post"
title: "How to modify / update Feature attribute using API in Map 3D?"
date: "2012-05-07 07:41:10"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/how-to-modify-update-feature-attribute-using-api-in-map-3d.html "
typepad_basename: "how-to-modify-update-feature-attribute-using-api-in-map-3d"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>You would like to programmatically modify / update the Feature Object&#39;s fields i.e. it&#39;s attribute data using Geospatial Platform API in Map 3D.</p>
<p><br />In the screenshot below, we can see a Feature Object with ID = 10 and its Length property = 10.1234; using API we will update it’s value to 12.0.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163054dc6c7970d-pi" style="display: inline;"><img alt="IMG_1" class="asset  asset-image at-xid-6a0167607c2431970b0163054dc6c7970d" src="/assets/image_81bd1a.jpg" title="IMG_1" /></a><br /><br /></p>
<p><br />Here is a VB.NET code snippet which demonstrates how to programmatically modify / update the Feature Object&#39;s fields i.e. it&#39;s attribute data using Geospatial Platform API in Map 3D :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Try</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> map <span style="color: blue;">As</span> <span style="color: #2b91af;">AcMapMap</span> = <span style="color: #2b91af;">AcMapMap</span>.GetCurrentMap()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> resId <span style="color: blue;">As</span> <span style="color: #2b91af;">MgResourceIdentifier</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layerName <span style="color: blue;">As</span> <span style="color: blue;">String</span> = <span style="color: #a31515;">&quot;API_Poly_Objects&quot;</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> className <span style="color: blue;">As</span> <span style="color: blue;">String</span> = <span style="color: #a31515;">&quot;&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Get the resource Id and class name of the layer on which we want to update</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layers <span style="color: blue;">As</span> <span style="color: #2b91af;">MgLayerCollection</span> = map.GetLayers()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> layer <span style="color: blue;">As</span> <span style="color: #2b91af;">MgLayerBase</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> iCount <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = <span style="color: blue;">Nothing</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">For</span> iCount = 0 <span style="color: blue;">To</span> (layers.Count)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> (layers.GetItem(iCount).Name.ToLower() = layerName.ToLower()) <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer = layers.GetItem(iCount)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; resId = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgResourceIdentifier</span>(layer.GetFeatureSourceId().ToString())</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit For</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Next</span> iCount</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> layer <span style="color: blue;">Is</span> <span style="color: blue;">Nothing</span> <span style="color: blue;">Then</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbCrLf + <span style="color: #a31515;">&quot;Layer Not Found ! &quot;</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit Sub</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Create a new Property colleaction Object</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> propColl <span style="color: blue;">As</span> <span style="color: #2b91af;">MgPropertyCollection</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgPropertyCollection</span>()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Create STNAME prop to Update it</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> lengthProp <span style="color: blue;">As</span> <span style="color: #2b91af;">MgDoubleProperty</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgDoubleProperty</span>(<span style="color: #a31515;">&quot;Length&quot;</span>, 12.0)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;Add the length property to property collection</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propColl.Add(lengthProp)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Define a filter to get the object whose property we want to update</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;&#39; Feature ID = 10 is specific to a sample SDF </span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> filterText <span style="color: blue;">As</span> <span style="color: blue;">String</span> = <span style="color: #a31515;">&quot;ID=10&quot;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39;Create the UpdateFeatures object for the update</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> updateFeatures <span style="color: blue;">As</span> <span style="color: #2b91af;">MgUpdateFeatures</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgUpdateFeatures</span>(layer.FeatureClassName, propColl, filterText)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Create a command collection for the update</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> commands <span style="color: blue;">As</span> <span style="color: #2b91af;">MgFeatureCommandCollection</span> = <span style="color: blue;">New</span> <span style="color: #2b91af;">MgFeatureCommandCollection</span>()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; commands.Add(updateFeatures)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Commit the Update</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fs.UpdateFeatures(resId, commands, <span style="color: blue;">True</span>)</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layer.ForceRefresh()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; trans.Commit()</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Catch</span> ex <span style="color: blue;">As</span> <span style="color: #2b91af;">MgException</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.Message.ToString())</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Finally</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; trans.Dispose()</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Try</span></p>
</div>
