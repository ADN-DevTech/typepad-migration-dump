---
layout: "post"
title: "Demystifying CogoPoint.Latitude and CogoPoint.Longitude API"
date: "2013-08-08 01:25:24"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/08/demystifying-cogopointlatitude-and-cogopointlongitude-api.html "
typepad_basename: "demystifying-cogopointlatitude-and-cogopointlongitude-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil
3D <a href="http://docs.autodesk.com/CIV3D/2014/ENU/API_Reference_Guide/index.html">.NET
API 2014 Reference</a> document clearly lists the following on <strong>CogoPoint.Latitude</strong>
and <strong>CogoPoint.Longitude</strong> –</p>
<p><strong>CogoPoint.Longitude</strong> -&gt; Gets or sets
the longitude for a point, relative to the coordinate zone and the
transformation settings specified for the drawing.</p>
<p><span style="font-size: 10pt;"><em>public:</em></span></p>
<p><span style="font-size: 10pt;"><em>property double Longitude {</em></span></p>
<p><span style="font-size: 10pt;"><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; double
get ();</em></span></p>
<p><span style="font-size: 10pt;"><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; void
set (double value);</em></span></p>
<p><span style="font-size: 10pt;"><em>}</em></span></p>
<p>&#0160;</p>
<p><strong>CogoPoint.Latitude</strong> -&gt; Gets or sets
the latitude for a point, relative to the coordinate zone and the
transformation settings specified for the drawing.</p>
<p>&#0160;</p>
<p><span style="font-size: 10pt;"><em>public:</em></span></p>
<p><span style="font-size: 10pt;"><em>property double Latitude {</em></span></p>
<p><span style="font-size: 10pt;"><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; double
get ();</em></span></p>
<p><span style="font-size: 10pt;"><em>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; void
set (double value);</em></span></p>
<p><span style="font-size: 10pt;"><em>}</em></span></p>
<p><span style="font-size: 10pt;"><em><br /></em></span></p>
<p>Let’s explore
this API returned results. First, we need to set an appropriate coordinate system
to our drawing file. For this experiment, I have used CA-II CS code as shown in
the screenshot below -</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104a462a1970c-pi" style="display: inline;"><img alt="AutoCAD_Civil3D_Point_Lat_Long_01" class="asset  asset-image at-xid-6a0167607c2431970b019104a462a1970c" src="/assets/image_c00921.jpg" title="AutoCAD_Civil3D_Point_Lat_Long_01" /></a><br /><br /></p>
<p>Now I will
create a COGO Point in the drawing area and we will investigate it’s Latitude
and Longitude values from API and in UI OPM.</p>
<p>Now let’s run
our custom application using Civil 3D .NET API CogoPoint.Latitude and CogoPoint.Longitude.
Here is the code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> pointId = points.Add(pointCoord, </span><span style="color: #a31515; line-height: 140%;">&quot;My_Test_Point&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;"> cgPoint = trans.GetObject(pointId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CogoPoint</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Point Latitude and Longitude values are in Radians</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPoint Lat : &quot;</span><span style="line-height: 140%;"> + <span style="background-color: #ffff00;">cgPoint.Latitude.ToString()</span> + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; + </span><span style="color: #a31515; line-height: 140%;">&quot;Point Long : &quot;</span><span style="line-height: 140%;"> + <span style="background-color: #ffff00;">cgPoint.Longitude.ToString()</span>);</span></p>
</div>
<p>&#0160;</p>
<p>And here is
the result shown :</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104a46312970c-pi" style="display: inline;"><img alt="AutoCAD_Civil3D_Point_Lat_Long_02" class="asset  asset-image at-xid-6a0167607c2431970b019104a46312970c" src="/assets/image_54dd41.jpg" title="AutoCAD_Civil3D_Point_Lat_Long_02" /></a><br /><br /></p>
<p>In the result
we get decimal values whereas in the Properties window we see the values are in
degree-minute-second. However the values don’t seem to match! </p>
<p>API returned double
values are in <strong>Radians</strong>. Referring the above screenshot, if we convert the
Latitude value 0.654133960948817 radians to degree we get 37.4791089757124
degree and the Longitude value 2.24952166028165 radians converts to 128.8880756575156
degree. </p>
<p>Hope this is
useful to you!</p>
