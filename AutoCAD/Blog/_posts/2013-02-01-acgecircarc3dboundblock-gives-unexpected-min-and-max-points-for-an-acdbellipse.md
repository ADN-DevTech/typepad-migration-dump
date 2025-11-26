---
layout: "post"
title: "AcGeCircArc3d::boundBlock() gives unexpected Min and Max points for an AcDbEllipse"
date: "2013-02-01 11:07:07"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/acgecircarc3dboundblock-gives-unexpected-min-and-max-points-for-an-acdbellipse.html "
typepad_basename: "acgecircarc3dboundblock-gives-unexpected-min-and-max-points-for-an-acdbellipse"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>Using AcGeCircArc3d (or CircularArc3d in .NET) to generate a curve, I find that the boundBlock() returns strange results.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>boundBlock() does not necessarily return a bounding box whose sides are parallel to the entity coordinate system (ECS). In fact for ellipses, it returns a bounding box whose edges are parallel to the ellipse major and minor axes... all other bounding boxes are generated parallel to the ECS... orthoBoundBlock(), on the other hand, returns points of a bounding box whose edges are parallel to the ECS - so use orthoBoundBlock() instead.</p>
