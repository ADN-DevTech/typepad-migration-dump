---
layout: "post"
title: "Applying Spatial Filter to Query FDO data using Geospatial Platform API in AutoCAD Map 3D"
date: "2013-10-10 00:10:33"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2013"
  - "AutoCAD Map 3D 2014"
  - "FDO"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/10/applying-spatial-filter-to-query-fdo-data-using-geospatial-platform-api-in-autocad-map-3d.html "
typepad_basename: "applying-spatial-filter-to-query-fdo-data-using-geospatial-platform-api-in-autocad-map-3d"
typepad_status: "Publish"
---

<p>Applying
Spatial Filter to Query FDO data using Geospatial Platform API in AutoCAD Map
3D</p>
<p>In AutoCAD
Map 3D, using the UI tools, we can apply spatial filter to query FDO data. To
give an example, in the screenshot below, if we want to see the Road feature
(FDO Feature) inside of the highlighted parcel (FDO feature), we can apply the
Map 3D Query Filter Data UI tools and see the road features inside of the
parcel. “Inside” is just an example here, we can apply many other spatial
conditions like intersects , Overlaps etc.</p>
<p>&#0160;</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affe7e0df970d-pi" style="display: inline;"><img alt="Map3D_Query_Filter_API_01" class="asset  asset-image at-xid-6a0167607c2431970b019affe7e0df970d" src="/assets/image_f0c9f4.jpg" title="Map3D_Query_Filter_API_01" /></a></p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affe76ace970b-pi" style="display: inline;"><img alt="Map3D_Query_Filter_API_02" class="asset  asset-image at-xid-6a0167607c2431970b019affe76ace970b" src="/assets/image_5593bd.jpg" title="Map3D_Query_Filter_API_02" /></a><br /><br /></p>
<p>If we want to achieve the same using Map 3D API, we can
use Map 3D Geospatial Platform API <strong>SetSpatialFilter &#0160;</strong>and <em><strong>MgFeatureSpatialOperations.Intersects</strong></em> spatial operation.</p>
<p>&#0160;</p>
<p>Here is a C# .NET code snippet demonstrating this -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MapSpatialQueryFilterDemo()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Selct using API</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgLayerCollection</span><span style="line-height: 140%;"> layers = currentMap.GetLayers();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> layer = layers.GetItem(</span><span style="color: #a31515; line-height: 140%;">&quot;Roads&quot;</span><span style="line-height: 140%;">); </span><span style="color: green; line-height: 140%;">// specific to Roads SDF data</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureService</span><span style="line-height: 140%;"> fs = </span><span style="color: #2b91af; line-height: 140%;">AcMapServiceFactory</span><span style="line-height: 140%;">.GetService(</span><span style="color: #2b91af; line-height: 140%;">MgServiceType</span><span style="line-height: 140%;">.FeatureService) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureService</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> fsId = layer.GetFeatureSourceId();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> className = layer.GetFeatureClassName();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Selecting the Boundary feature </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// In this case the Parcel boundary&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptSelectionResult</span><span style="line-height: 140%;"> selResult = ed.GetSelection();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">SelectionSet</span><span style="line-height: 140%;"> selSet = selResult.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgSelectionBase</span><span style="line-height: 140%;"> selection = </span><span style="color: #2b91af; line-height: 140%;">AcMapFeatureEntityService</span><span style="line-height: 140%;">.GetSelection(selSet);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> layerFrame = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MgLayerBase</span><span style="line-height: 140%;"> testlayer </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> selection.GetLayers())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (testlayer.Name == </span><span style="color: #a31515; line-height: 140%;">&quot;Parcels&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; layerFrame = testlayer;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//get the properties </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> reader = selection.GetSelectedFeatures(layerFrame, layerFrame.FeatureClassName, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgAgfReaderWriter</span><span style="line-height: 140%;"> agfReadWrite = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgAgfReaderWriter</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgGeometry</span><span style="line-height: 140%;"> boundaryGeomtry = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (reader.ReadNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; boundaryGeomtry = agfReadWrite.Read(reader.GetGeometry(layerFrame.GetFeatureGeometryName()));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; reader.Close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//spatial relationship inside&#0160; filter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;"> query = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgFeatureQueryOptions</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; query.SetSpatialFilter(</span><span style="color: #a31515; line-height: 140%;">&quot;Geometry&quot;</span><span style="line-height: 140%;">, boundaryGeomtry, </span><span style="color: #2b91af; line-height: 140%;">MgFeatureSpatialOperations</span><span style="line-height: 140%;">.Inside);</span></span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//Get the features </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;"> resId = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MgResourceIdentifier</span><span style="line-height: 140%;">(fsId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">MgFeatureReader</span><span style="line-height: 140%;"> ftrRdr = fs.SelectFeatures(resId, className, query);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//Display the ID and Other Property of the selected features </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (ftrRdr.ReadNext())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> id = ftrRdr.GetInt32(</span><span style="color: #a31515; line-height: 140%;">&quot;Autogenerated_SDF_ID&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> roadName = ftrRdr.GetString(</span><span style="color: #a31515; line-height: 140%;">&quot;ST_NAME&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nID: &quot;</span><span style="line-height: 140%;"> + id + </span><span style="color: #a31515; line-height: 140%;">&quot; &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;Road Name : &quot;</span><span style="line-height: 140%;"> + roadName.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ftrRdr.Close();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">MgException</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(ex.Message.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
</div>
<p>&#0160;</p>
<p>And the result of the same :</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affe76709970c-pi" style="display: inline;"><img alt="Map3D_Query_Filter_API_03" class="asset  asset-image at-xid-6a0167607c2431970b019affe76709970c" src="/assets/image_4526a8.jpg" title="Map3D_Query_Filter_API_03" /></a></p>
<p>&#0160;</p>
<p>Hope this is useful to you.</p>
