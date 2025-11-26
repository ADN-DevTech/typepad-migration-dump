---
layout: "post"
title: "How AcDb3dSolids are drawn when using a custom derived AcGiWorldDraw"
date: "2013-07-18 11:21:29"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/how-acdb3dsolids-are-drawn-when-using-a-custom-derived-acgiworlddraw.html "
typepad_basename: "how-acdb3dsolids-are-drawn-when-using-a-custom-derived-acgiworlddraw"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>It is possible to create your own custom WorldDraw and ViewportDraw classes (in ObjectARX they are called AcGiWorldDraw and AcgiViewprtDraw). By doing this, you can essentially override the low level draw routines that AutoCAD uses and replace them with your own functions which can be used to work out the primitive geometrical object data that is being drawn.    <br />    <br />A good example of how this works can be found in the ObjectARX sample called <strong>acgisamp</strong>. </p>  <p>If you are wondering how AcDb3dSolids (Solid3d in .NET) draw themselves, and which primitive draw functions inside of your custom AcGiWorldDraw are called and when, here’s the answer:</p>  <ol>   <li>AcDb3dSolid::WorldDraw() is called, the solid geometry history worldDraw is called (internal AcDbShHistory::worldDraw()).</li>    <li>From the internal AcDbShHistory::worldDraw() each node in the solid 3d history is obtained and the primative objects are called one by one via their worldDraw() (internal AcDbShPrimitive::WorldDraw())</li>    <li>From the internal AcDbShPrimitive::WorldDraw() the body of the primitive is drawn by drawing each graphics representation in turn... The order is:     <br />      <br />For a regen type of <strong>kAcGiHideOrShadeCommand</strong> or <strong>kAcGiShadedDisplay</strong>      <br />      <br />1) Draws the face edges depending on the edge type, Line or Tessellated yields a geometry.polyline(), Circle or Arc yields a geometry.circle() or geometry.circularArc()      <br />2) Draws the wire edges depending on the edge type as in (1)      <br />3) Draws the body points using geometry.polypoint()      <br />      <br />For a regen type of <strong>kAcGiStandardDisplay</strong>, <strong>kAcGiSaveWorldDrawForR12</strong> or <strong>kAcGiSaveWorldDrawForProxy</strong>      <br />      <br />1) Draws the face edges depending on the edge type, Line or Tessellated yields a geometry.polyline(), Circle or Arc yields a geometry.circle() or geometry.circularArc()      <br />2) Draws the wire edges depending on the edge type as in (1)      <br />3) If SPLFRAME=1 then the spline and spline-fit polylines are drawn using geometry.polyline()      <br />4) Draw the iso line edges, if necessary , depending on the edge type as in (1)      <br />5) Draws the body points using geometry.polypoint()</li> </ol>
