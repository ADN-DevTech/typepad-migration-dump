---
layout: "post"
title: "acreEntityToFaces normals on polygon mesh"
date: "2013-01-18 08:51:52"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/acreentitytofaces-normals-on-polygon-mesh.html "
typepad_basename: "acreentitytofaces-normals-on-polygon-mesh"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The normal represents the normal of the surface, of which the facet is an approximation. When facetting AcDb3dSolids, the facets may lie on curved surfaces. Certain algorithms require the exact normal at the facet vertex, so they can produce more realistic renderings.</p>  <p>The PolygonMesh object does not represent an exact solid. A normal cannot therefore be calculated at the facet vertex. ( There is no surface on which to calculate the normal ).</p>  <p>Assuming that the polygon mesh is used to represent some solid object, then it may be appropriate to use the average normal of all the facets around that vertex. If the normals are used to affect a rendered image, this would show sharp edges as rounded. It would therefore be useful to have a minimum angular turn that a facet edge could have. If the angle between adjacent facets were greater than this tolerance, then each facet would retain its own normal, so the edge looked sharp.</p>
