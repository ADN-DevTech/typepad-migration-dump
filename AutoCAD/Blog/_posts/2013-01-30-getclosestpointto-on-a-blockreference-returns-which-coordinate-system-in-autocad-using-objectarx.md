---
layout: "post"
title: "GetClosestPointTo() on a BlockReference returns which coordinate system in AutoCAD using ObjectARX"
date: "2013-01-30 13:21:10"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/getclosestpointto-on-a-blockreference-returns-which-coordinate-system-in-autocad-using-objectarx.html "
typepad_basename: "getclosestpointto-on-a-blockreference-returns-which-coordinate-system-in-autocad-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>Why are the coordinates of a closest point (using GetClosestPointTo()) obtained from a block reference relative to neither WCS nor UCS?.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The points that you receive are relative to the coordinate system of the owning   <br />AcDbBlockTableRecord. If you want to convert them to coordinate system of the    <br />AcDbBlockTableRecord in which you have the AcDbBlockReference, you must    <br />transform them by the AcDbBlockReference::blockTransform().</p>  <p>You probably first want to transform the &quot;specified point&quot; by the inverse of   <br />AcDbBlockReference::blockTransform(), do the closest point calculation, then    <br />convert the resulting point back to the original space.</p>
