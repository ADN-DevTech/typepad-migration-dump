---
layout: "post"
title: "AutoCAD FIELD Expression to display Lat/Long value as Text"
date: "2013-11-25 00:46:44"
author: "Partha Sarkar"
categories:
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "AutoCAD Map 3D 2014"
  - "Map 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/autocad-field-expression-to-display-latlong-value-as-text.html "
typepad_basename: "autocad-field-expression-to-display-latlong-value-as-text"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>What <strong>FIELD</strong> Expression should I use to show the Lat/Long value of a Point object in a DWG file in AutoCAD Map 3D which has an assigned coordinate system ?&#0160;</p>
<p>Here is an example of a &#39;FIELD Expression&#39; you can use to how the Lat/Long value as Text in AutoCAD Map 3D -</p>
<p><strong>%&lt;\AcObjProp Object(%&lt;\_ObjId 8796087804352&gt;%).Coordinates \f &quot;%lu6&quot;&gt;%</strong></p>
<p>&#0160;</p>
<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b01a0a6bf970d-pi" style="display: inline;"><img alt="Map3D_CS_Field" class="asset  asset-image at-xid-6a0167607c2431970b019b01a0a6bf970d" src="/assets/image_39f442.jpg" title="Map3D_CS_Field" /></a><br /></strong></p>
