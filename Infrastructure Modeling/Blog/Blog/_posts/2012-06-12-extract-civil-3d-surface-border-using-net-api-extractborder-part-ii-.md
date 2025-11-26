---
layout: "post"
title: "Extract Civil 3D Surface border using .NET API ExtractBorder() - Part II "
date: "2012-06-12 02:47:32"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/06/extract-civil-3d-surface-border-using-net-api-extractborder-part-ii-.html "
typepad_basename: "extract-civil-3d-surface-border-using-net-api-extractborder-part-ii-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In <a href="http://adndevblog.typepad.com/infrastructure/2012/06/extract-civil-3d-surface-border-using-net-api-extractborder.html" target="_self">previous post</a> I shared a C# .NET code snippet on usage of ExtractBorder(). Interestingly, the extracted entities from the <strong>ExtractBorder()</strong> function can be Polyline, Polyline3d, or Face. In this post we will explore various situations which will return Polyline, Polyline3d, or Face.</p>
<p>Here is a situation when <strong>ExtractBorder()</strong> will return <em><strong>Polyline3d</strong></em> :&#0160;If Surface “Border Display Mode” is set to “Use Surface Elevation” Or “Exaggerate Elevation” then the Extracted entity using the ExtractBorder() .NET API is of type Polyline3d. &#0160;</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167676547eb970b-pi" style="display: inline;"><img alt="Surface_Border_01" class="asset  asset-image at-xid-6a0167607c2431970b0167676547eb970b" src="/assets/image_3e3c83.jpg" title="Surface_Border_01" /></a></p>
<p>&#0160;</p>
<p>Here is a situation when <strong>ExtractBorder()</strong> will return <em><strong>Polyline</strong></em> : &#0160;If Surface “Border Display Mode” is set to “Flatten Elevations” then the Extracted entity using the ExtractBorder() .NET API is of type Polyline.</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176155ac538970c-pi" style="display: inline;"><img alt="Surface_Border_02" class="asset  asset-image at-xid-6a0167607c2431970b0176155ac538970c" src="/assets/image_28a52e.jpg" title="Surface_Border_02" /></a></p>
<p>&#0160;</p>
<p>Here is a situation when <strong>ExtractBorder()</strong> will return <em><strong>Face</strong></em> : If Surface “Border Display Mode” is set to “Use Surface Elevation” and ‘Datum’ options are set to “Use Datum’ = True and ‘Project Grid to Datum’ = True,&#0160; then the Extracted entities will include 3D Face.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016306719223970d-pi" style="display: inline;"><img alt="Surface_Border_03" class="asset  asset-image at-xid-6a0167607c2431970b016306719223970d" src="/assets/image_3c864d.jpg" title="Surface_Border_03" /></a></p>
