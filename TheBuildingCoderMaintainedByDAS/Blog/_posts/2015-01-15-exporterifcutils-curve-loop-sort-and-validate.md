---
layout: "post"
title: "ExporterIfcUtils Curve Loop Sort and Validate"
date: "2015-01-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Geometry"
  - "IFC"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2015/01/exporterifcutils-curve-loop-sort-and-validate.html "
typepad_basename: "exporterifcutils-curve-loop-sort-and-validate"
typepad_status: "Publish"
---

<p>Joel Spahn raised a pertinent

<a href="http://thebuildingcoder.typepad.com/blog/2015/01/autodesk-internship-in-california-and-sorting-edges.html?cid=6a00e553e16897883301b8d0be1356970c">
question</a> on

<a href="http://thebuildingcoder.typepad.com/blog/2015/01/autodesk-internship-in-california-and-sorting-edges.html#3">
sorting face loop edges</a> that

was kindly picked up and answered by Scott Conover and Angel Velez from the Revit development team:</p>

<p><strong>Question:</strong> It would be nice to know exactly what the following methods do:</p>

<ul>
<li><b>SortCurveLoops</b>: sorts a set of curve loops such that outer and inner loops are separated.</li>
<ul>
<li>Remarks: Outer loops are separated and inner loops are grouped according to their outer loop. Loops are assumed to be non-intersecting, and there will be no nesting of inner loops, i.e., an inner loop of an inner loop is another outer loop.</li>
<li>Returns: The sorted collection of loops.</li>
<li>Input:</li>
<ul>
<li>loops, the curve loops.</li>
</ul>
</ul>
<li><b>ValidateCurveLoops</b>: performs validity checks on a list of curve loops to ensure that they are all co-planar, closed, and properly oriented.</li>
<ul>
<li>Returns the curve loops properly oriented, if possible.  If not, the return contains no loops.</li>
<li>Input:</li>
<ul>
<li>curveLoops, the loops to check</li>
<li>extrDirVec, the normal vector.</li>
</ul>
</ul>
</ul>

<p>In ValidateCurveLoops, the 'extrDirVec' normal vector defines what the 'proper orientation' means, by ensuring that the loops are counter-clockwise relative to this direction vector.</p>

<p>Both methods are perfectly usable outside of any IFC context.</p>

<p>In the long term, there is no reason why they should not move into the regular Revit API.</p>

<p>Thank you, Joel, for raising this very valid question, and to Scott and Angel for their answers.</p>

<p>As I mentioned in the recent discussion

<a href="http://thebuildingcoder.typepad.com/blog/2015/01/autodesk-internship-in-california-and-sorting-edges.html#3">
sorting face loop edges</a>,

the RoomEditorApp provides a sample code snippet exercising SortCurveLoops.</p>
