---
layout: "post"
title: "Using AcMapMapManagementService::DuplicateMap() to create a duplicate Map"
date: "2012-07-19 01:22:17"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/using-acmapmapmanagementserviceduplicatemap-to-create-a-duplicate-map.html "
typepad_basename: "using-acmapmapmanagementserviceduplicatemap-to-create-a-duplicate-map"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Map 3D 2013 Platform API, we have added <strong>AcMapMapManagementService::DuplicateMap()</strong> to create a duplicate map.</p>
<p>Here is a C# code snippet which demonstrates how to create a duplicate map using this new API :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get an Instance of AcMapMapManagementService</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMapManagementService</span><span style="line-height: 140%;"> mapMgServ = </span><span style="color: #2b91af; line-height: 140%;">AcMapMapManagementService</span><span style="line-height: 140%;">.GetInstance(</span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create a Duplicate Map</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> duplicateMap = mapMgServ.DuplicateMap(mapMgServ.CurrentMap);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Set the Map Name</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">duplicateMap.SetName(</span><span style="color: #a31515; line-height: 140%;">&quot;MyDuplicate_Map&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Set the Duplicate Map as current</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">mapMgServ.SetCurrentMap(duplicateMap);&#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
