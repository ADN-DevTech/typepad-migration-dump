---
layout: "post"
title: "Get / Set SuperelevationOptions using AutoCAD Civil 3D .NET API"
date: "2013-10-03 04:28:16"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/10/get-set-superelevationoptions-using-autocad-civil-3d-net-api.html "
typepad_basename: "get-set-superelevationoptions-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If we refer
to AutoCAD Civil 3D API reference document for <a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/html/7b4b7995-09ab-cddf-8e54-cfe008d15713.htm">SuperelevationOptions</a>,
we see most of the properties are Get as shown in the screenshot below -</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affbff313970c-pi" style="display: inline;"><img alt="Civil3D_SuperelevationOptions_01" class="asset  asset-image at-xid-6a0167607c2431970b019affbff313970c" src="/assets/image_57bdf4.jpg" title="Civil3D_SuperelevationOptions_01" /></a><br /><br /></p>
<p>If you are
wondering why these are only Get and how do we set these properties using API,
answer is, their <strong>values</strong> are <em><strong>Get</strong></em> and <em><strong>Set</strong></em> and we can use the appropriate property
and it’s value to set as per our project requirement.</p>
<p>Here is an
example using C# .NET API – </p>
<p>Default
settings before we make a change using API – </p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affbfd8d3970b-pi" style="display: inline;"><img alt="Civil3D_SuperelevationOptions_02" class="asset  asset-image at-xid-6a0167607c2431970b019affbfd8d3970b" src="/assets/image_9127d2.jpg" title="Civil3D_SuperelevationOptions_02" /></a></p>
<p>Here is the
code snippet –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SettingsAlignment</span><span style="line-height: 140%;"> alignmentSettings = civilDoc.Settings.GetSettings&lt;</span><span style="color: #2b91af; line-height: 140%;">SettingsAlignment</span><span style="line-height: 140%;">&gt;(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSuperelevationOptions.CorridorType Before Set :&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignmentSettings.SuperelevationOptions.CorridorType.Value.ToString() +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nSuperelevationOptions.NominalWidth Before Set&#0160; :&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; alignmentSettings.SuperelevationOptions.NominalWidth.Value.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Values are Get / Set</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><span style="line-height: 140%;">&#0160; alignmentSettings.SuperelevationOptions.CorridorType.Value = </span><span style="color: #2b91af; line-height: 140%;">SuperelevationCorridorType</span><span style="line-height: 140%;">.Divided;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">&#0160; alignmentSettings.SuperelevationOptions.NominalWidth.Value = 8.2;&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And changed
values after we run our custom code – </p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019affc08336970d-pi" style="display: inline;"><img alt="Civil3D_SuperelevationOptions_afterChange" class="asset  asset-image at-xid-6a0167607c2431970b019affc08336970d" src="/assets/image_265d03.jpg" title="Civil3D_SuperelevationOptions_afterChange" /></a></p>
<p>Hope this helps
!</p>
