---
layout: "post"
title: "What kind of objects are returned from AssemblyConstraint GeometryOne()/GeometryTwo() methods?"
date: "2012-05-23 08:29:54"
author: "Philippe Leefsma"
categories:
  - "Inventor"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/what-kind-of-objects-are-returned-from-assemblyconstraint-geometryonegeometrytwo-methods.html "
typepad_basename: "what-kind-of-objects-are-returned-from-assemblyconstraint-geometryonegeometrytwo-methods"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>Here is an issue that can be faced by C++ programmers when dealing with the Inventor API:</p>  <p><em>When I call GetGeometryOne(), I get an IDispatch interface back. but i am unable to identify the object type since DispatchUtils::GetObjectType() fails for these obects. What object types can be returned and how can I get at them?</em></p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>Currently, the complete list of possible results from the Geometry properties on any assembly constraints is limited to the following object types.</p>  <p>Point ( IRxPoint )</p>  <p>Line ( IRxLine )</p>  <p>Circle ( IRxCircle )</p>  <p>EllipseFull ( IRxEllipseFull )</p>  <p>Vector ( IRxVector )</p>  <p>Plane ( IRxPlane )</p>  <p>Cylinder ( IRxCylinder )</p>  <p>Cone ( IRxCone )</p>  <p>EllipticalCone ( IRxEllipticalCone )    <br />Sphere ( IRxSphere )     <br />Torus ( IRxTorus )     <br />BSplineSurface ( IRxBSplineSurface )</p>  <p>For each constraint type, the list is of course, likely to be much smaller.</p>  <p>Since these objects are not strongly typed objects, the only way to deal with this is to just QI for the types you expect in a cascading QI if/else if</p>
