---
layout: "post"
title: "What controls the radius of the curves when we create Alignment?"
date: "2012-07-02 00:59:39"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/what-controls-the-radius-of-the-curves-when-we-create-alignment.html "
typepad_basename: "what-controls-the-radius-of-the-curves-when-we-create-alignment"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In <a href="http://adndevblog.typepad.com/infrastructure/2012/06/do-you-want-to-programmatically-convert-polyline-to-civil-3d-alignment-object.html">&#39;Do you want to programmatically convert polyline to Civil 3D Alignment object?</a>&#39; I showed how to convert a Polyline to a Civil 3D Alignment object. Christopher of <a href="http://blog.civil3dreminders.com/">CIVIL 3D REMINDERS</a> asked about what controls the radius of the curves? I thought I will take a quick look into this and share my findings with you all.</p>
<p>Radius of the curve is determined by the value set for &quot;<strong>Default radius</strong>&quot; under &#39;<strong>Create from Entities</strong>&#39; in &quot;<strong>Edit Command Settings - CreateAlignmentEntities</strong>&quot; dialog box as shown in the screenshot below.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01676812deda970b-pi" style="display: inline;"><img alt="Alignment_Radius" class="asset  asset-image at-xid-6a0167607c2431970b01676812deda970b" src="/assets/image_2699fd.jpg" title="Alignment_Radius" /></a><br /><br /></p>
<p>And this value can be accessed using following code snippet -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> settingsCmdCrAlignmentEnt </span><span style="color: blue; line-height: 140%;">As</span><span style="background-color: #ffff00;"><span style="line-height: 140%;"> Autodesk.Civil.Settings.</span><span style="color: #2b91af; line-height: 140%;">SettingsCmdCreateAlignmentEntities</span></span><span style="line-height: 140%;"> = civilDoc.Settings.GetSettings(</span><span style="color: blue; line-height: 140%;">Of</span><span style="line-height: 140%;"> Autodesk.Civil.Settings.</span><span style="color: #2b91af; line-height: 140%;">SettingsCmdCreateAlignmentEntities</span><span style="line-height: 140%;">)()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;SettingsCmdCreateAlignmentEntities.SettingsCmdCreateFromEntities.Radius =&#0160; :&#0160; &quot;</span><span style="line-height: 140%;"> + settingsCmdCrAlignmentEnt.CreateFromEntities.Radius.Value.ToString())&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Hope this helps !</p>
