---
layout: "post"
title: "Map.Save() throws MgNotImplementedException exception when adding new layer to map"
date: "2012-04-12 01:29:22"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
  - "MapGuide Enterprise 2011"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/mapsave-throws-mgnotimplementedexception-exception-when-adding-new-layer-to-map.html "
typepad_basename: "mapsave-throws-mgnotimplementedexception-exception-when-adding-new-layer-to-map"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html" target="_self">Daniel Du</a></p>
<p>Are you getting <strong>MgNotImplementedException </strong>when trying to add new layers to map? If yes, keep reading.</p>
<p>&nbsp;</p>
<p>Here is my code snippet to create a MapGuide layer and insert it to current map, but it throws <strong>MgNotImplementedException </strong>at <strong>map.Save()</strong>:</p>
<div style="font-family: courier new; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Open the site connection and connect to </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//mapguide site using current session</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (siteConnection == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;"> userInfo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;">(sessionId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; siteConnection = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; siteConnection.Open(userInfo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Create resource service from site connection</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;"> resSvc = siteConnection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; .CreateService(</span><span style="line-height: 140%; color: #2b91af;">MgServiceType</span><span style="line-height: 140%;">.ResourceService)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Declare an resource id for layer</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgResourceIdentifier</span><span style="line-height: 140%;"> filterLayerId =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgResourceIdentifier</span><span style="line-height: 140%;">(sessionLayerResId);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Open current map</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;"> map = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.Open(resSvc, mapName);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Create a MgLayerBase object</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgLayerBase</span><span style="line-height: 140%;"> filteredLayer =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgLayerBase</span><span style="line-height: 140%;">(filterLayerId, resSvc);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//set up the new layer </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.LegendLabel = </span><span style="line-height: 140%; color: #a31515;">"filtered"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Selectable = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.DisplayInLegend = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Name = </span><span style="line-height: 140%; color: #a31515;">"_filtered"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Group = map.GetLayerGroups()[0];</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//insert the layer into map on top</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.GetLayers().Insert(0, filteredLayer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//MgNotImplementedException here ****</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.Save(resSvc);&nbsp; </span></p>
</div>
<p>When running this code snippet, I always get MgNotImplementedException, detailed error message goes as below:</p>
<p>==============================</p>
<p><em>Not implemented.</em></p>
<p><strong>Description: </strong>An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code.     <br /><strong>Exception Details: </strong>OSGeo.MapGuide.MgNotImplementedException: Not implemented.     <br /><strong>Source Error:</strong></p>
<p>&nbsp;</p>
<pre>Line 412:        map.GetLayers().Insert(0, filteredLayer);
Line 413:
Line 414:        map.Save(resSvc);
Line 415:
Line 416:    }</pre>
<p>==============================</p>
<p>Is there anything wrong with my code above?</p>
<pre class="csharpcode">    </pre>
<h4>Solution:</h4>
<p>Use <strong>MgLayer</strong> instead of MgLayerBase when inserting layers into map,&nbsp; it is simple but it is a little tricky and not easy to debug. so the working code snippet will be:</p>
<div style="font-family: courier new; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Open the site connection and connect to </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//mapguide site using current session</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (siteConnection == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;"> userInfo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgUserInformation</span><span style="line-height: 140%;">(sessionId);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; siteConnection = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgSiteConnection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; siteConnection.Open(userInfo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Create resource service from site connection</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;"> resSvc = siteConnection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; .CreateService(</span><span style="line-height: 140%; color: #2b91af;">MgServiceType</span><span style="line-height: 140%;">.ResourceService)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgResourceService</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Declare an resource id for layer</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgResourceIdentifier</span><span style="line-height: 140%;"> filterLayerId =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgResourceIdentifier</span><span style="line-height: 140%;">(sessionLayerResId);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Open current map</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;"> map = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgMap</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.Open(resSvc, mapName);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//Create a MgLayerBase object</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">MgLayer</span><span style="line-height: 140%;"> filteredLayer =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MgLayer</span><span style="line-height: 140%;">(filterLayerId, resSvc);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//set up the new layer </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.LegendLabel = </span><span style="line-height: 140%; color: #a31515;">"filtered"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Selectable = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.DisplayInLegend = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Name = </span><span style="line-height: 140%; color: #a31515;">"_filtered"</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; filteredLayer.Group = map.GetLayerGroups()[0];</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//insert the layer into map on top</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.GetLayers().Insert(0, filteredLayer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">//works fine now</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; map.Save(resSvc);&nbsp; </span></p>
</div>
<p>&nbsp;</p>
<p>Hope this helps you.</p>
<p>&nbsp;</p>
