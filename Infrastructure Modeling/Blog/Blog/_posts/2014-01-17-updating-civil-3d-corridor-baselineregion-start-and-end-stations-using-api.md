---
layout: "post"
title: "Updating Civil 3D Corridor BaseLineRegion Start and End Stations using API"
date: "2014-01-17 00:30:08"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/01/updating-civil-3d-corridor-baselineregion-start-and-end-stations-using-api.html "
typepad_basename: "updating-civil-3d-corridor-baselineregion-start-and-end-stations-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In <a href="http://adndevblog.typepad.com/infrastructure/2013/04/adding-removing-baselineregion-using-net-api.html" target="_blank">this</a> earlier blog <a href="http://adndevblog.typepad.com/infrastructure/2013/04/adding-removing-baselineregion-using-net-api.html" target="_blank">post</a>, I demonstrated how to Add / Remove BaseLineRegion in <a class="zem_slink" href="http://construction-project-management-software.findthebest.com/l/70/AutoCAD" rel="fdbsoftware" target="_blank" title="AutoCAD">AutoCAD Civil 3D</a> using <a class="zem_slink" href="http://www.microsoft.com/net" rel="homepage" target="_blank" title=".NET Framework">.NET</a> <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a>. In one of the <a href="http://forums.autodesk.com/t5/AutoCAD-Civil-3D-Customization/Corridor-BaseLine-Region/td-p/4755079" target="_blank">questions </a>in AutoCAD Civil 3D Customization Forum, I see a developer having an issue in editing the Corridor Baseline Region start and end station. In the <a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/" target="_blank">Civil 3D API reference</a> document I see the <strong>BaselineRegion.StartStation</strong> and <strong>BaselineRegion.EndStation</strong> properties are <strong>Get</strong> and <strong>Set</strong> i.e. we can Get the station values as well as we Set them. I thought to give it a try to confirm they are working as per the API reference doc and I do find we can Get as well as Set the station values.</p>
<p>&#0160;</p>
<p>Here is the working C# .NET code snippet :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Corridor</span><span style="line-height: 140%;"> corridor = trans.GetObject(corridorId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">Corridor</span><span style="line-height: 140%;">;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the BaselineRegionCollection</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BaselineRegionCollection</span><span style="line-height: 140%;"> baselineRegionColl = corridor.Baselines[0].BaselineRegions;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the BaselineRegion to update the Start and End Station</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BaselineRegion</span><span style="line-height: 140%;"> baselineregion = baselineRegionColl[</span><span style="color: #a31515; line-height: 140%;">&quot;Corridor Region (1)&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (baselineregion != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n BaseLineRegion Start Station Before Update : &quot;</span><span style="line-height: 140%;"> + baselineregion.StartStation.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n BaseLineRegion End&#0160;&#0160; Station Before Update : &quot;</span><span style="line-height: 140%;"> + baselineregion.EndStation.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// update the stations Value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;<span style="background-color: #ffff00;"> baselineregion.StartStation = 50.00;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">&#0160; baselineregion.EndStation = 568.00;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//rebuild the corridor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; corridor.Rebuild();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n BaseLineRegion Start Station After Update : &quot;</span><span style="line-height: 140%;"> + baselineregion.StartStation.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n BaseLineRegion End&#0160;&#0160; Station After Update : &quot;</span><span style="line-height: 140%;"> + baselineregion.EndStation.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
</div>
<p>&#0160;</p>
<p>And the result in AutoCAD Civil 3D 2014 :</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51103e9e4970c-pi" style="display: inline;"><img alt="Civil_3D_Corridor_Baseline_Station_Update_API" class="asset  asset-image at-xid-6a0167607c2431970b01a51103e9e4970c img-responsive" src="/assets/image_82ca15.jpg" title="Civil_3D_Corridor_Baseline_Station_Update_API" /></a></p>
