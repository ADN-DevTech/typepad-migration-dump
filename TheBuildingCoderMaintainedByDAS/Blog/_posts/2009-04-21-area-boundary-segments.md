---
layout: "post"
title: "Area Boundary Segments"
date: "2009-04-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Debugging"
  - "Element Relationships"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/04/area-boundary-segments.html "
typepad_basename: "area-boundary-segments"
typepad_status: "Publish"
---

<p>Here are some notes on the properties and ordering of area boundary segments from a case handled by my colleagues Katsuaki Takamizawa, Harry Mattison and Tamas Badics:</p>

<p><strong>Question:</strong>
I am analysing a Revit area element, marked below by the red circle, with another adjacent area beside it:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115702ec224970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115702ec224970b" alt="Area boundary" title="Area boundary" src="/assets/image_f04a43.jpg" border="0"  /></a>

<p>I would have assumed this area to have 4 segments in its boundary. 
When I use the RvtMgdDbg snoop functionality to explore it, though, it displays 5 segments:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115702ec55e970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115702ec55e970b image-full" alt="Snooping area boundary" title="Snooping area boundary" src="/assets/image_481c03.jpg" border="0"  /></a>

<p>Is this the expected behaviour? 
If so, I cannot simply use the vertex and edge count to analyse an area and determine whether it is triangular or rectangular.
Instead, I have to check whether adjacent edges are parallel, and if so, treat them as one edge instead of two.</p>

<p>Concerning the ordering of edges, I have drawn the lines using the edge coordinates, starting from segment 0 in red and ending with segment 4 in dark blue:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330115702ec866970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e1689788330115702ec866970b" alt="Boundary segment ordering" title="Boundary segment ordering" src="/assets/image_59e251.jpg" border="0"  /></a>

<p>The boundary segments seem to be stored in counter clockwise order. 
Is this always true?</p>

<p><strong>Answer:</strong>
The order of storage is counter clockwise for outer boundaries and clockwise for inner boundaries. 
For instance, imagine a room with an island at the centre. 
It will have two boundaries, one outer, and one inner.</p>

<p>As for the number of segments, what you see is expected in this situation.</p>

<h4>Questions to You</h4>

<p>By the way, before we close, I have two questions to you:</p>

<ol>
<li>Is anyone out there using RealDWG inside a Revit add-in, to read DWG files in native format?</li>
<li>Does anyone know of a profiler that works with Revit 2010 add-ins?</li>
</ol>

<p>Thank you!</p>
