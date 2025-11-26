---
layout: "post"
title: "Want to Add a SuperelevationCriticalStation using Civil 3D .NET API ?"
date: "2012-06-14 02:32:44"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/want-to-add-a-superelevationcriticalstation-using-civil-3d-net-api-.html "
typepad_basename: "want-to-add-a-superelevationcriticalstation-using-civil-3d-net-api-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Civil 3D, <strong>SuperelevationCriticalStation</strong> class represents a super-elevation critical station on an <strong>Alignment</strong> object. And <strong>SuperelevationCriticalStationCollection</strong> class represents a collection of SuperelevationCriticalStation objects. <em><strong>SuperelevationCriticalStationCollection.Add()</strong></em> method adds a SuperelevationCriticalStation of the specified transition type at the specified station. For the list of SuperelevationCriticalStationType members, take a look into the SuperelevationCriticalStationType Enumeration in Civil 3D .NET API Reference document.&#0160;</p>
<p>Here is a C# &#0160;code snippet which demonstrates how to add a new critical station at a specified station value (double) with a <strong>SuperelevationCriticalStationType</strong> of <em><strong>BeginNormalCrown</strong></em> :</p>
<p><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> align = trans.GetObject(alignmentId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// get the critical station collection on the aligment.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">SuperelevationCriticalStationCollection</span><span style="line-height: 140%;"> criteriaStationColl = align.SuperelevationCriticalStations;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// add a new critical station at the station 1000.00 with a type BeginNormalCrown.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// values used below can be tested using Civil 3D Tutorial sample DWG file - &quot;Align-Superelevation-1.dwg &quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; criteriaStationColl.Add(1000.00, </span><span style="color: #2b91af; line-height: 140%;">SuperelevationCriticalStationType</span><span style="line-height: 140%;">.BeginNormalCrown);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// get the critical station at the station 1000.00.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">SuperelevationCriticalStation</span><span style="line-height: 140%;"> scs1000 = criteriaStationColl.GetCriticalStationAt(1000.00, 0.01);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; scs1000.SetSlope(</span><span style="color: #2b91af; line-height: 140%;">SuperelevationCrossSegmentType</span><span style="line-height: 140%;">.LeftOutLaneCrossSlope, 0.05);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//....</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And you are expected to see a result similar to this :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163068ec2d8970d-pi" style="display: inline;"><img alt="SuperElevn" class="asset  asset-image at-xid-6a0167607c2431970b0163068ec2d8970d" src="/assets/image_58237a.jpg" title="SuperElevn" /></a></p>
<p>Hope this helps !</p>
