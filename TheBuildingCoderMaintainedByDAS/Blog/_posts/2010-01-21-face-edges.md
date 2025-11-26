---
layout: "post"
title: "Face Edges"
date: "2010-01-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - "AU 2009"
  - "Geometry"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/01/face-edges.html "
typepad_basename: "face-edges"
typepad_status: "Publish"
---

<p>This is part 6 of Scott Conover's AU 2009 class on

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/analyse-building-geometry.html">
analysing building geometry</a>.

<h4>Edge and Face Parameterization</h4>

<p>Edges are boundary curves for a given face.</p>

<p>Iterate the edges of a Face using the EdgeLoops property. 
Each loop represents one closed boundary on the face. 
Edges are always parameterized from 0 to 1.</p>

<p>An edge is usually defined by computing intersection of two faces. 
But Revit doesn't recompute this intersection when it draws graphics. 
So the edge stores a list of points &ndash; end points for a straight edge and a tessellated list for a curved edge. 
The points are parametric coordinates on the two faces. 
These points are available through the TessellateOnFace method.</p>

<p>Sections produce 'cut edges'. These are artificial edges &ndash; not representing a part of the model-level geometry, and thus do not provide a Reference.</p>

<h4>Edge Direction</h4>

<p>Direction is normally clockwise on the first face (first representing an arbitrary face which Revit has identified for a particular edge). But because two different faces meet at one particular edge, and the edge has the same parametric direction regardless of which face you are concerned with, sometimes you need to figure out the direction of the edge on a particular face.</p>

<p>The figure below illustrated how this works. 
For Face 0, the edges are all parameterized clockwise. 
For Face 1, the edge shared with Face 0 is not re-parameterized; 
thus with respect to Face 1 the edge has a reversed direction, and some edges intersect where both edges' parameters are 0 (or 1):</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012876f47ad1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012876f47ad1970c image-full" alt="Edge parameterization" title="Edge parameterization" src="/assets/image_186ca2.jpg" border="0"  /></a> <br />

</center>

<h4>The PanelEdgeLengthAngle Revit SDK Sample</h4>

<p>The Revit SDK sample PanelEdgeLengthAngle shows how to recognize edges that are reversed for a given face. 
It uses the tangent vector at the edge endpoints to calculate the angle between adjacent edges, and detect whether or not to flip the tangent vector at each intersection to calculate the proper angle:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833012876f47a66970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833012876f47a66970c image-full" alt="PanelEdgeLengthAngle results" title="PanelEdgeLengthAngle results" src="/assets/image_28d9a6.jpg" border="0"  /></a> <br />

</center>

<p>The next instalment of this series will look at transformations.
