---
layout: "post"
title: "Boundary and bhatch on custom entities"
date: "2013-01-11 17:44:50"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/boundary-and-bhatch-on-custom-entities.html "
typepad_basename: "boundary-and-bhatch-on-custom-entities"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Lets say, you have created a custom entity based on a polyline, but derived from a AcDbEntity, and wish to change the way BHATCH and BOUNDARY find the edges of my object.</p>  <p>You can do this by overriding the AcDbEntity::explode() virtual method to return the boundaries you want used by the BOUNDARY or BHATCH commands. You would check the CMDNAMES system variable, to see whether one of the two commands was active, and then return the appropriate entities. Note that if you derive from AcDbPolyline, it will not call explode on your entity, but will use   <br />the Polyline data directly.</p>
