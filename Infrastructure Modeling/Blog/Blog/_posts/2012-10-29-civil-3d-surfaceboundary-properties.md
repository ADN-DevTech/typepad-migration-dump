---
layout: "post"
title: "Civil 3D SurfaceBoundary Properties"
date: "2012-10-29 02:14:32"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/10/civil-3d-surfaceboundary-properties.html "
typepad_basename: "civil-3d-surfaceboundary-properties"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>This is
further to my previous post ‘<a href="http://adndevblog.typepad.com/infrastructure/2012/10/accessing-surface-boundary-vertices-using-civil-3d-net-api.html">Accessing
Surface Boundary vertices using Civil 3D .NET API</a>’. When we try to add a
boundary object in Civil 3D using UI tools, we get to see the following UI
window :</p>
<p>&#0160;</p>
<p>&#0160;
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee48a8a18970d-pi" style="display: inline;"><img alt="Boundary_Prop" class="asset  asset-image at-xid-6a0167607c2431970b017ee48a8a18970d" src="/assets/image_88560f.jpg" title="Boundary_Prop" /></a></p>
<p>&#0160;</p>
<p>In this post
I will explain what are the equivalent APIs available currently (in release
2013) corresponding to boundary properties we see in the above UI dialog box.</p>
<p>&#39;<em><strong>Name</strong></em>&#39; is accessible through
<strong>BoundariesDefinition.Item(i).Name</strong></p>
<p>&#39;<em><strong>Type</strong></em>&#39; is accessible through
<strong>SurfaceBoundary.BoundaryType</strong> </p>
<p><strong>SurfaceBoundaryType</strong> Enumeration -</p>
<p>&#0160;<em><strong>Show&#0160; </strong></em></p>
<p><em><strong>&#0160;DataClip&#0160;
</strong></em></p>
<p><em><strong>&#0160;Outer&#0160;&#0160; </strong></em></p>
<p><em><strong>&#0160;Hide</strong></em> </p>
<p>&#0160;</p>
<p>&#39;<em><strong>Non-Destructive breakline</strong></em>&#39; is same as
<strong>IsTrimmed</strong> [BoundariesDefinition.Item(i).IsTrimmed ]&#0160; </p>
<p>&#0160;</p>
<p>&#39;<em><strong>Mid-ordinate
distance</strong></em>&#39; is not exposed yet; we have a wish list logged for the same. Let me
know if this is useful to you, I can assign an appropriate priority based on
number of responses I see here.</p>
