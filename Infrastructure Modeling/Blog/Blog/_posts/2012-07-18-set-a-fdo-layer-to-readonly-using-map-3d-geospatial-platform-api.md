---
layout: "post"
title: "Set a FDO Layer to ReadOnly using Map 3D Geospatial Platform API"
date: "2012-07-18 22:53:44"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/set-a-fdo-layer-to-readonly-using-map-3d-geospatial-platform-api.html "
typepad_basename: "set-a-fdo-layer-to-readonly-using-map-3d-geospatial-platform-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Using <strong>Autodesk.Gis.Map.Platform.AcMapLayer.IsLayerReadOnly()</strong> API, we can now check if a FDO layer shown in Display Manger is ReadOnly.</p>
<p>We can even set it to ReadOnly using <strong>AcMapLayer.SetLayerReadOnly()</strong> API, which is new in 2013 release.</p>
<p>Here is a C# code snippet on how to use it :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!layer.IsLayerReadOnly())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; layer.SetLayerReadOnly();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
