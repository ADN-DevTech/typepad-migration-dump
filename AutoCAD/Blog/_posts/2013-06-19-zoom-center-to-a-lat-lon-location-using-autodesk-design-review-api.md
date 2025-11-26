---
layout: "post"
title: "Zoom & Center to a Lat / Lon location using Autodesk Design Review API"
date: "2013-06-19 05:31:07"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "ADR API"
  - "DWF"
  - "JavaScript"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/zoom-center-to-a-lat-lon-location-using-autodesk-design-review-api.html "
typepad_basename: "zoom-center-to-a-lat-lon-location-using-autodesk-design-review-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&#0160;</p>
<p>In <a href="http://usa.autodesk.com/design-review/">Autodesk Design Review</a> using
the “<strong>Center to Coordinates</strong>” UI tool we can set the center for a Map to a
specific coordinate value entered through the User Interface as seen in the
screenshot below –</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019103851771970c-pi" style="float: left;"><img alt="ADR_CenterToCoordinates" class="asset  asset-image at-xid-6a0167607c2431970b019103851771970c" src="/assets/image_24103.jpg" style="margin: 0px 5px 5px 0px;" title="ADR_CenterToCoordinates" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>Using&#0160;<em><strong>ECompositeViewer.centerToCoordinates(</strong>coordType,
x, y<strong>)</strong></em> we can replicate the above mentioned UI tool command. You need to pass on the correct Lat &amp; Lon values
in context to the page you are trying to view, e.g.</p>
<p><span style="color: #4040ff;">dwfView.centerToCoordinates(2,
-12.562387, 131.073166)</span> <span style="color: #60bf00;">// Lat &amp; Lon values should be correct to view and
zoom to your dataset</span></p>
<p>Note that, to use this API, sections in the
DWF must contain Georeferenced properties. </p>
This API can be used in a URL to open a DWF with
a Map section and Center the view to the coordinates provided. However, when
used as part of a URL the coordinates must be within the section that is being
viewed.
