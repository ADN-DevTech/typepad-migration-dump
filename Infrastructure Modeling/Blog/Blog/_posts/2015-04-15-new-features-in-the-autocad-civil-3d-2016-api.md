---
layout: "post"
title: "New Features in the AutoCAD Civil 3D 2016 API"
date: "2015-04-15 06:13:56"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D"
original_url: "https://adndevblog.typepad.com/infrastructure/2015/04/new-features-in-the-autocad-civil-3d-2016-api.html "
typepad_basename: "new-features-in-the-autocad-civil-3d-2016-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>We’re glad to announce <a href="http://www.autodesk.com/products/autocad-civil-3d/overview">AutoCAD Civil 3D 2016</a> it’s available for download at our main portal. Here I would like to point what’s new on the API:</p>  <p><strong>.NET Changes</strong></p>  <p>This section covers changes to the AutoCAD Civil 3D .NET API for the 2016 release.</p>  <ul>   <li>Alignments </li>    <ul>     <li>A parameter named attainmentRegionType has been added to SuperelevationCriticalStationCollection::Add(…). This parameter is used to specify the attainment region type: AutoAttainmentRegionType, BeginingAttainmentRegion, or EndingAttainmentRegion. Specifying AutoAttainmentRegionType as the attainment region type will replicate the behavior that was in previous releases.</li>   </ul>    <li>Pipe Networks</li>    <ul>     <li>A new property named CrossPipeProfile is exposed in the SettingsCmdCreateNetwork::SettingsCmdLabelNewParts class to enable you to get whether a new crossing pipe is labeled by default when placed in profile view.</li>   </ul> </ul>  <p><strong>COM Changes</strong></p>  <p>If you are using the COM API, you need to update the object version to 10.5 (from 10.4 used in AutoCAD Civil 3D 2015). The objects and interfaces exposed have remained the same, but you should reference the new libraries, which are installed by default to: C:\Program Files\Common Files\Autodesk Shared\Civil Engineering 105.</p>
