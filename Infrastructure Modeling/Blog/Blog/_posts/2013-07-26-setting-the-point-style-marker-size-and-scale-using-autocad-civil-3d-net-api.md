---
layout: "post"
title: "Setting the Point Style Marker Size and Scale using AutoCAD Civil 3D .NET API"
date: "2013-07-26 00:14:09"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/07/setting-the-point-style-marker-size-and-scale-using-autocad-civil-3d-net-api.html "
typepad_basename: "setting-the-point-style-marker-size-and-scale-using-autocad-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you want to create a new <strong>PointStyle</strong> object in AutoCAD
Civil 3D and set the <em><strong>Marker Size</strong></em> and <em><strong>Scale</strong></em> using .NET API, you first need to
create the new Point style or access the point style object you want to modify
and then set the SizeType property to use the <strong>MarkerSizeType</strong> enum values that
defines how the marker is sized such as ‘Use drawing scale’ or ‘Use fixed scale’
etc. Next set the MarkerSize or Scale values accordingly.</p>
<p>Here
is a C# .NET code snippet demonstrating the same :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Create a point style that uses a custom marker</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// and set the Size and scale</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointStyleId = civilDoc.Styles.PointStyles.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;My DEMO Point Style&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">PointStyle</span><span style="line-height: 140%;"> pointStyle = pointStyleId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PointStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointStyle.MarkerType = </span><span style="color: #2b91af; line-height: 140%;">PointMarkerDisplayType</span><span style="line-height: 140%;">.UseCustomMarker;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointStyle.CustomMarkerStyle = </span><span style="color: #2b91af; line-height: 140%;">CustomMarkerType</span><span style="line-height: 140%;">.CustomMarkerX;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; pointStyle.CustomMarkerSuperimposeStyle = </span><span style="color: #2b91af; line-height: 140%;">CustomMarkerSuperimposeType</span><span style="line-height: 140%;">.Circle;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// set the size to use &#39;Drawing Scale&#39;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">&#0160; pointStyle.SizeType = </span><span style="color: #2b91af; line-height: 140%;">MarkerSizeType</span><span style="line-height: 140%;">.DrawingScale;</span></strong></p>
<p style="margin: 0px;"><strong><span style="line-height: 140%;">&#0160; pointStyle.MarkerSize = 0.003;</span></strong></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// If you want to set the size to use &#39;Fixed Scale&#39;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//pointStyle.SizeType = MarkerSizeType.FixedScale;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//Point3d markerfixedscalePoint3d = new Point3d(3, 3, 3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">//pointStyle.MarkerFixedScale = markerfixedscalePoint3d;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Here is a screenshot of my new PointStyle with custom Marker :</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104694fdc970c-pi" style="display: inline;"><img alt="Civil3D_PointStyle_Marker_Setting" class="asset  asset-image at-xid-6a0167607c2431970b019104694fdc970c" src="/assets/image_d43cac.jpg" title="Civil3D_PointStyle_Marker_Setting" /></a><br /><br /></p>
<p>Hope this is useful to you!</p>
