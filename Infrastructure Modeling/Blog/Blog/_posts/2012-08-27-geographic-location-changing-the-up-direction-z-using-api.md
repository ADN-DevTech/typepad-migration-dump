---
layout: "post"
title: "'Geographic Location' & changing the Up direction (Z) using API"
date: "2012-08-27 02:03:22"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/08/geographic-location-changing-the-up-direction-z-using-api.html "
typepad_basename: "geographic-location-changing-the-up-direction-z-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Using
&quot;<strong>GEOGRAPHICLOCATION</strong>&quot; command you could bring up the &quot;Geographic
Location&quot; window and there you will find the &quot;Up direction&quot;
drop-down to set the &quot;Z&quot; value. If you want to set / change the &quot;Up direction&quot; in GEOGRAPHICLOCATION settings through API, you can use
<strong>GeoLocationData.UpDirection</strong></p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01761775a14a970c-pi" style="display: inline;"><img alt="GeoLocation" class="asset  asset-image at-xid-6a0167607c2431970b01761775a14a970c" src="/assets/image_902c7d.jpg" title="GeoLocation" /></a></p>
<p>&#0160;</p>
<p><strong>GeoLocationData
</strong>Class is available in AutoCAD .NET API
-&#0160;<strong>Autodesk.AutoCAD.DatabaseServices.GeoLocationData</strong>&#0160;</p>
<p>&#0160;</p>
<p>Here is a C#
code snippet which demonstrates how to change &quot;Up direction&quot; using :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Access GeoLocationData</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> geoDataId = db.GeoDataObject;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">GeoLocationData</span><span style="line-height: 140%;"> geoLocdata = trans.GetObject(geoDataId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">GeoLocationData</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// Set the UpDirection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> vec3d = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(0.0, 0.0, 1.00);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; geoLocdata.UpDirection = vec3d;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
