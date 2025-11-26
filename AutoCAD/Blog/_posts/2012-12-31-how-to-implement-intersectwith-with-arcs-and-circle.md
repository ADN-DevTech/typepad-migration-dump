---
layout: "post"
title: "How to implement intersectWith with arcs and circle?"
date: "2012-12-31 19:27:06"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/how-to-implement-intersectwith-with-arcs-and-circle.html "
typepad_basename: "how-to-implement-intersectwith-with-arcs-and-circle"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>When you implement my custom entity, the function intersectWith is called when the user enters the TRIM command. Although you implement custom entity such that it can handle arcs and circles, you will find that the intersectWith function of the custom entity is passed an entity for which isKindOf( AcDbArc::desc() ) and isKindOf(AcDbCircle::desc()) are both False when the arcs and circles are selected. So how can you provide special handling for circles and arcs?</p>  <p>For arcs and circles, the entity passed is actually an AcDbEllipse, which allows functions that handle all elliptical objects generically. If you can handle circles and arcs in a specific way ( not as general ellipses ) then extract their defining data ( such as center, normal, start and end points ) and implement the intersections.</p>
