---
layout: "post"
title: "Gizmo location for custom entity"
date: "2013-07-19 00:23:09"
author: "Balaji"
categories:
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/gizmo-location-for-custom-entity.html "
typepad_basename: "gizmo-location-for-custom-entity"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p><a href="http://www.youtube.com/watch?v=deOXrR7FjOw">Gizmo</a> earlier called "grip tools" get displayed at the geometric center of the selection set if the "GTLOCATION" system variable is set to 1. AutoCAD gathers the extents of all the selected entities and determines the geometric center of the combined extents. To have the gizmo correctly located for a custom entity, it is needed to override the "subGetGeomExtents" method and provide the extents.</p>
<p></p>
