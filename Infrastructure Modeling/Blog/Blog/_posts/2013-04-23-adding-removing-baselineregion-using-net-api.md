---
layout: "post"
title: "Adding / Removing BaselineRegion using .NET API"
date: "2013-04-23 09:31:59"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/adding-removing-baselineregion-using-net-api.html "
typepad_basename: "adding-removing-baselineregion-using-net-api"
typepad_status: "Publish"
---

<p><strong>BaselineRegion
</strong>class in Civil 3D represents a segment of a baseline, specified by alignment
stations for the start and end point. In a corridor, each baseline region uses
a single assembly type. </p>
<p>In AutoCAD
Civil 3D using UI tools we can add / remove BaselineRegion –</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea815057970d-pi" style="display: inline;"><img alt="BaselineRegion01" class="asset  asset-image at-xid-6a0167607c2431970b017eea815057970d" src="/assets/image_c729cc.jpg" title="BaselineRegion01" /></a></p>
<p>&#0160;</p>
<p>New API
functions are exposed in this release to add, remove or access a <strong>BaselineRegion
</strong>in the class <strong>BaselineRegionCollection</strong>.</p>
<p>Here is a C#
.NET code snippet which shows how to add and remove BaselineRegion –</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// BaselineRegionCollection.Add(string regionName, string assemblyName, double startStation, double endStation ) </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Adds a region at the specified start and end station with the given name and assembly. </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the BaselineRegionCollection</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BaselineRegionCollection</span><span style="line-height: 140%;"> baselineRegionColl = corridor.Baselines[0].BaselineRegions;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Add a new BaselineRegion</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">BaselineRegion</span><span style="line-height: 140%;"> baselineregion = baselineRegionColl.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;My Added Region&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Assembly - (1)&quot;</span><span style="line-height: 140%;">, 506.00, 1110.00);&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Remove a BaselineRegion with a given Name&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">baselineRegionColl.Remove(</span><span style="color: #a31515; line-height: 140%;">&quot;Corridor Region (2)&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//rebuild the corridor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">corridor.Rebuild();</span></p>
</div>
<p>Once added
the newly created BaselineRegion will be shown under the Baseline -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea8151a5970d-pi" style="display: inline;"><img alt="BaselineRegion03" class="asset  asset-image at-xid-6a0167607c2431970b017eea8151a5970d" src="/assets/image_15513f.jpg" title="BaselineRegion03" /></a><br /><br /></p>
