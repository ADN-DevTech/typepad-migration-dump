---
layout: "post"
title: "Assign coordinate systems to an AutoCAD Civil 3D DWG file using API"
date: "2013-07-18 00:05:13"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/07/assign-coordinate-systems-to-an-autocad-civil-3d-dwg-file-using-api.html "
typepad_basename: "assign-coordinate-systems-to-an-autocad-civil-3d-dwg-file-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD
Civil 3D user interface (UI), in the &#39;Drawing Settings&#39; dialog, at &#39;Units and
Zone&#39; tab, we can select a geographic coordinate system from the available list
and apply the same to the current DWG file.</p>
<p>
Here is a C# .NET
code snippet which demonstrates how to do the same using .NET API -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">DocumentLock</span><span style="line-height: 140%;"> locker = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.LockDocument())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// perform any document / database modifications here&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">SettingsDrawing</span><span style="line-height: 140%;"> settingsDWG = civilDoc.Settings.DrawingSettings;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//settingsDWG.UnitZoneSettings.CoordinateSystemCode = &quot;LL84&quot;;</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; settingsDWG.UnitZoneSettings.CoordinateSystemCode = </span><span style="color: #a31515; line-height: 140%;">&quot;FL83-EF&quot;</span><span style="line-height: 140%;">;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCoordinateSystemCode = &quot;</span><span style="line-height: 140%;"> + settingsDWG.UnitZoneSettings.CoordinateSystemCode.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e51ae2b970b-pi" style="display: inline;"><img alt="Civil3D_CoordinateSystem" class="asset  asset-image at-xid-6a0167607c2431970b01901e51ae2b970b" src="/assets/image_4d5e19.jpg" title="Civil3D_CoordinateSystem" /></a><br /><br /></span></p>
</div>
