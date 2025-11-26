---
layout: "post"
title: "Integrate custom entity with the lengthen command"
date: "2012-12-12 04:52:14"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/integrate-custom-entity-with-the-lengthen-command.html "
typepad_basename: "integrate-custom-entity-with-the-lengthen-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>The LENGTHEN command can only work with the following classes, or any custom classes derived from them:</p>  <p>AcDbLine   <br />AcDbArc    <br />AcDbCircle    <br />AcDb2dPolyline    <br />AcDb3dPolyline    <br />AcDbEllipse    <br />AcDbSpline    <br />AcDbPolyline</p>  <p>If the custom entity is derived from any of these, LENGTHEN will automatically work, but if not, unfortunately there is nothing we can do to implement the command. </p>
