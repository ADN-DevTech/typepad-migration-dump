---
layout: "post"
title: "Loading a *.layer file using API"
date: "2012-05-10 02:42:04"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/loading-a-layer-file-using-api.html "
typepad_basename: "loading-a-layer-file-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Map 3D you can use the &#39;Load Layer...&#39; UI tool to load a <strong>*.layer</strong> file and create data layer with specific FDO data source.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676662d28d970b-pi" style="display: inline;"><img alt="Img1" class="asset  asset-image at-xid-6a0167607c2431970b01676662d28d970b" src="/assets/image_f85e18.jpg" title="Img1" /></a><br /><br /><br />If you have many such *.layer files and you want them to load programmatically, you can use <strong>Autodesk.Gis.Map.Platform.AcMapMap &#0160;<em>LoadLayer(string layerFile)</em></strong> API for the same.&#0160;</p>
<p>Here is a C# code snippet :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">// Get the Map Object</span></p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; <span style="color: #2b91af;">AcMapMap</span> currentMap = <span style="color: #2b91af;">AcMapMap</span>.GetCurrentMap();</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; currentMap.LoadLayer(<span style="color: #a31515;">@&quot;C:\Data_set\Layer_Files\Parcels.layer&quot;</span>);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; &#0160; <span style="color: blue;">catch</span> (<span style="color: #2b91af;">MgException</span> ex)</p>
<p style="margin: 0px;">&#0160; &#0160; {</p>
<p style="margin: 0px;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.Message.ToString());</p>
<p style="margin: 0px;">&#0160; &#0160; }</p>
</div>
