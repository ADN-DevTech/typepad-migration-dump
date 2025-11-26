---
layout: "post"
title: "Export DWG to SDF using Autodesk.Gis.Map.ImportExport.Exporter API"
date: "2012-04-30 01:29:31"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/export-dwg-to-sdf-using-autodeskgismapimportexportexporter-api.html "
typepad_basename: "export-dwg-to-sdf-using-autodeskgismapimportexportexporter-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In <a href="http://adndevblog.typepad.com/infrastructure/2012/03/exporting-dwg-to-sdf-using-epf-file-using-map-3d-api.html" target="_self">Exporting DWG to SDF using *.epf file using Map 3D API</a> I showed how to use *.epf file and Map 3D API to export DWG file to SDF file. In this example, I tried to explore how we can export DWG data to <a href="http://en.wikipedia.org/wiki/Spatial_data_file" target="_self">SDF</a> (Spatial Data File) using AutoCAD Map 3D API without using a *.epf file. &#0160;This sample code uses Map 3D <strong>Autodesk.Gis.Map.ImportExport.Exporter</strong> class and exports the entities from a specific layer in the DWG file to <em><strong>GeometryType.Line</strong></em>. To find out what all GeometryType you can export, you need to explore the <strong>Autodesk:: Gis:: Map:: ImportExport:: GeometryType</strong> <em>Enumeration</em>. Details of these are available in the Map 3D SDK <a href="http://adndevblog.typepad.com/infrastructure/2012/04/renaming-the-industry-data-model-aka-topobase-api-assemblies-and-namespace-in-autocad-map-3d-2013.html" target="_self">API reference documents</a>.&#0160;Here is the code snippet :</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Editor</span> ed = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">Exporter</span> myExporter = <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">MapApplication</span> mapApp = <span style="color: #2b91af;">HostMapApplicationServices</span>.Application;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter = mapApp.Exporter;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.Init(<span style="color: #a31515;">&quot;FDO_SDF&quot;</span>, <span style="color: #a31515;">@&quot;C:\Temp\Test.sdf&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.ClassMappingType = <span style="color: #2b91af;">ExportClassMappingType</span>.ByLayer;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.ClosedPolylinesAsPolygons = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">string</span> sSourceName = <span style="color: #a31515;">&quot;Line&quot;</span>; <span style="color: green;">// Source DWG file should have a Layer Named &quot;Line&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">string</span> sSchemaName = <span style="color: #a31515;">&quot;ADNSchema&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">string</span> sFeatureClassName = <span style="color: #a31515;">&quot;LineFC&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">ExportClassMapping</span> mapping = <span style="color: #2b91af;">ExportClassMapping</span>.Create(sSourceName, sSchemaName, sFeatureClassName);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; mapping.AddPropertyMapping(<span style="color: #a31515;">&quot;.COLOR&quot;</span>, <span style="color: #a31515;">&quot;Color&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; mapping.AddPropertyMapping(<span style="color: #a31515;">&quot;.LAYER&quot;</span>, <span style="color: #a31515;">&quot;Layer&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; mapping.AddPropertyMapping(<span style="color: #a31515;">&quot;.LENGTH&quot;</span>, <span style="color: #a31515;">&quot;Length&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.AddClassMapping(mapping);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.SetGeometryTypeForClass(sSchemaName, sFeatureClassName, <span style="color: #2b91af;">GeometryType</span>.Line);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.TargetCoordinateSystem = <span style="color: #a31515;">&quot;LL84&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.ExportAll = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; myExporter.Export(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nExport Completed !&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (Autodesk.Gis.Map.<span style="color: #2b91af;">MapImportExportException</span> e)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;Exception thrown. Error Code = &quot;</span> + e.ErrorCode);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
</div>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">And here is the resulted output SDF file in Map 3D :</p>
</div>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0168eaec88bf970c-pi" style="display: inline;"><img alt="DWG_2_SDF" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0168eaec88bf970c image-full" src="/assets/image_bdd5a0.jpg" title="DWG_2_SDF" /></a><br /><br /></p>
