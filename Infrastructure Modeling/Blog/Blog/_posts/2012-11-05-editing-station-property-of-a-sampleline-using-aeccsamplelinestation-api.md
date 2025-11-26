---
layout: "post"
title: "Editing Station property of a Sampleline using AeccSampleLine.Station API"
date: "2012-11-05 22:48:02"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/11/editing-station-property-of-a-sampleline-using-aeccsamplelinestation-api.html "
typepad_basename: "editing-station-property-of-a-sampleline-using-aeccsamplelinestation-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This VB.NET
code snippet demonstrates how to add a Civil 3D SampleLine using COM API
<strong>IAeccSampleLines:: AddByPolyline()</strong> method and then check whether the sample
line is locked to the station using &#0160;<strong>IAeccSampleLine:: LockToStation</strong>,&#0160;before updating it&#39;s Station value using COM API
<strong>IAeccSampleLine:: Station</strong> Property.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;idEnt is Polyline ObjectId</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> pLineEntity </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(trans.GetObject(idEnt, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite), Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;idEnt1 is Alignment ObjectId</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> alignmentEntity </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(trans.GetObject(idEnt1, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite), Autodesk.AutoCAD.DatabaseServices.</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> pline </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Interop.Common.</span><span style="color: #2b91af; line-height: 140%;">AcadLWPolyline</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(pLineEntity.AcadObject, Autodesk.AutoCAD.Interop.Common.</span><span style="color: #2b91af; line-height: 140%;">AcadLWPolyline</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oAlignment </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccAlignment</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">DirectCast</span><span style="line-height: 140%;">(alignmentEntity.AcadObject, Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccAlignment</span><span style="line-height: 140%;">)&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oSampleLine </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Autodesk.AECC.Interop.Land.</span><span style="color: #2b91af; line-height: 140%;">AeccSampleLine</span><span style="line-height: 140%;"> = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><span style="background-color: #ffff00;">&#0160; oAlignment.SampleLineGroups.Item(0).SampleLines.AddByPolyline</span>(</span><span style="color: #a31515; line-height: 140%;">&quot;My_SampleLine&quot;</span><span style="line-height: 140%;">, pline, </span><span style="color: blue; line-height: 140%;">False</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; IAeccSampleLine:: Station Property</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Gets or sets the alignment station for this sample line.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Station Value :&quot;</span><span style="line-height: 140%;"> + oSampleLine.Station.ToString())</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39; Check whether the sample line is locked to the station</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;">(oSampleLine.LockToStation)</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; oSampleLine.LockToStation = </span><span style="color: blue; line-height: 140%;">False</span></span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Now Change the Station Value</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Following hardcoded value is applicable to Civil 3D Sample DWG - &quot;Sections-Views-Create.dwg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">oSampleLine.Station = 550.50</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Station Value After Change:&quot;</span><span style="line-height: 140%;"> + oSampleLine.Station.ToString())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">trans.Commit()</span></p>
</div>
<p>&#0160;</p>
<p>Here is the Result :</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee4cad13a970d-pi" style="display: inline;"><img alt="SampleLine_Update" class="asset  asset-image at-xid-6a0167607c2431970b017ee4cad13a970d" src="/assets/image_7c00cf.jpg" title="SampleLine_Update" /></a><br /><br /></p>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
