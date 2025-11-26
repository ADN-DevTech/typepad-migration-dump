---
layout: "post"
title: "How do I change the default styles of Points in AutoCAD Civil 3D using .NET API?"
date: "2013-06-10 22:44:56"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/06/how-do-i-change-the-default-styles-of-points-in-autocad-civil-3d-using-net-api.html "
typepad_basename: "how-do-i-change-the-default-styles-of-points-in-autocad-civil-3d-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD
Civil 3D ‘Create Points’ dialog, under Parameter if you expand the ‘Default
Styles’ collection, you will see the default ‘Point Style’ and ‘Point Label
Style’ field and their corresponding values in the ‘Value’ column as shown in
the screenshot below –</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901d3f73aa970b-pi" style="display: inline;"><img alt="Civil3D_Point_Default_Style" class="asset  asset-image at-xid-6a0167607c2431970b01901d3f73aa970b" src="/assets/image_79635c.jpg" title="Civil3D_Point_Default_Style" /></a><br /><br /></p>
<p>If you want to change these default style values using
Civil 3D .NET API, you need to get the <strong>SettingsPoint</strong> object first and then
update the Style and Label styles value as shown in the C# .NET code snippet
below –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// get the SettingsPoint object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">SettingsPoint</span><span style="line-height: 140%;"> pointSettings = civilDoc.Settings.GetSettings&lt;</span><span style="color: #2b91af; line-height: 140%;">SettingsPoint</span><span style="line-height: 140%;">&gt;() </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SettingsPoint</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// now set the value for default Style and Label Style</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// make sure the values exists in DWG file before you try to set them</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointSettings.Styles.Point.Value = </span><span style="color: #a31515; line-height: 140%;">&quot;Benchmark&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointSettings.Styles.PointLabel.Value = </span><span style="color: #a31515; line-height: 140%;">&quot;Elevation Only&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Once you run your custom application the default style
and labels style value for Point object will be changed – </p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901d3f74ff970b-pi" style="display: inline;"><img alt="Civil3D_Point_Default_Style_afterChange" class="asset  asset-image at-xid-6a0167607c2431970b01901d3f74ff970b" src="/assets/image_c3009c.jpg" title="Civil3D_Point_Default_Style_afterChange" /></a><br /><br /></p>
<p>Hope this is useful to you!</p>
