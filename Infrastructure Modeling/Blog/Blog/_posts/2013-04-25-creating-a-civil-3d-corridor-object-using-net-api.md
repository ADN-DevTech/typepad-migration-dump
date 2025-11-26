---
layout: "post"
title: "Creating a Civil 3D Corridor object using .NET API"
date: "2013-04-25 03:21:10"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/04/creating-a-civil-3d-corridor-object-using-net-api.html "
typepad_basename: "creating-a-civil-3d-corridor-object-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Civil 3D 2014 <a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/index.html">.NET
API</a> has an overloaded <strong>Add()</strong> method in <strong>CorridorCollection</strong> class. In this
post let’s see how to build a Corridor using the <strong>CorridorCollection.Add</strong>(string
corridorName,</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; string
baselineName,</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjectId
alignmentId,</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjectId
profileId,</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; string
baselineRegionName,</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ObjectId
assemblyId &#0160;)</p>
<p>&#0160;</p>
<p>If you want to target the Existing ground surface in the
Target Mapping as shown in the screenshot below, then you need to do a trick :) . Thanks to my colleague (<strong>Paul Chen</strong>)
from engineering team for an important hint to achieve this. You need to find out <strong><em>TargetType</em></strong> for each of the <strong>SubassemblyTargetInfo
</strong>object and then set the&#0160;<strong>TargetIds</strong> to SurfaceIds as shown in the code
snippet below -</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901b910f24970b-pi" style="display: inline;"><img alt="Corridor_Target_Mapping" class="asset  asset-image at-xid-6a0167607c2431970b01901b910f24970b" src="/assets/image_1031aa.jpg" title="Corridor_Target_Mapping" /></a></p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the first alignment of this drawing</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> alignmentId = civilDoc.GetAlignmentIds()[0];</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> alignment = trans.GetObject(alignmentId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the first profile of this alignment</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> profileId = alignment.GetProfileIds()[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the first Assembly in the DWG</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> assemblyId = civilDoc.AssemblyCollection[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Create a new Corridor</span></p>
<p style="margin: 0px;"><strong><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> newCorridorId = civilDoc.CorridorCollection.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;Proposed Corridor&quot;</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Proposed Baseline&quot;</span><span style="line-height: 140%;">, alignmentId, profileId, </span><span style="color: #a31515; line-height: 140%;">&quot;Proposed BaselineRegion&quot;</span><span style="line-height: 140%;">, assemblyId);</span></strong></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Corridor</span><span style="line-height: 140%;"> corridor = trans.GetObject(newCorridorId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Corridor</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Set the Target to existing surface</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// GetTargets() Gets the targets information. </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// The returned object can be modified and passed to SetTargets() </span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// to update a corridor&#39;s subassembly targets information.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">SubassemblyTargetInfoCollection</span><span style="line-height: 140%;"> subtargetinfocoll = corridor.GetTargets();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; subtargetinfocoll.Count; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SubassemblyTargetInfo</span><span style="line-height: 140%;"> subassemblytargetinfo = subtargetinfocoll[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (subassemblytargetinfo.TargetType == </span><span style="color: #2b91af; line-height: 140%;">SubassemblyLogicalNameType</span><span style="line-height: 140%;">.Surface)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; subassemblytargetinfo.TargetIds = civilDoc.GetSurfaceIds();&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }&#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// update the target information</span></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">corridor.SetTargets(subtargetinfocoll);</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now rebuild the corridor</span></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">corridor.Rebuild();</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">trans.Commit();</span></p>
</div>
<p>&#0160;</p>
<p>I used one of the Civil 3D Tutorials DWG file which has prerequisite
objects built in it to create the corridor using above code snippet. Hope this is useful to you !</p>
