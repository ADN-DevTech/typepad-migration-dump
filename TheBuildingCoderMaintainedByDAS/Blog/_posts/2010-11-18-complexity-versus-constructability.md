---
layout: "post"
title: "Complexity versus Constructability"
date: "2010-11-18 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Algorithm"
  - "Element Creation"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/11/complexity-versus-constructability.html "
typepad_basename: "complexity-versus-constructability"
typepad_status: "Publish"
---

<p>Here is another contribution from Ritchie Jackson of the

<a href="http://www.aac.bartlett.ucl.ac.uk">
Adaptive Architecture and Computation</a> programme

at UCL, the

<a href="http://en.wikipedia.org/wiki/University_College_London">
University College London</a>, who 

recently showed us his code and results working with 

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/blends-hermite-splines-and-derivatives.html">
blends, Hermite splines and derivatives</a>.

This time Ritchie addresses the theme of how to ensure and demonstrate the constructability of complex doubly curved shapes:

<p>With contemporary international focus on the design of complex, double-curved forms I thought I'd use the API to try and rationalise the process as an aid to fabrication and assembly.</p>

<p>Using a Roller-Coaster Reception Facility as a test rig, a 3D-spline was sketched in a conceptual mass family and the points exported to create setouts for C# driven components in a generic model family. The latter was purposely chosen to force simplification of geometry in order to address the buildability question, with all elements being reduced to planar forms (arcs and lines)</p>

<p>The images below outline the process. Although the 3D spline cannot be visualised in the generic model family, it can still be used as an underlying generator for geometry. It was evaluated at regular intervals along its length to provide setout points for the arc sub-components. The division number was progressively increased until the component junctions visually appeared to be smooth (tangents almost co-linear). Curve derivatives were extracted at these points and the tangent vectors used to create plane-normals for the cross-bracing and rafters:-</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134892b0b11970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134892b0b11970c" alt="Roller-Coaster Setout" title="Roller-Coaster Setout" src="/assets/image_7da89e.jpg" border="0" /></a> <br />

</center>

<p>The bracing points were tapered via linear interpolation and the rafter spans adjusted using a sine function:-</p>

<center>


<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f60c7c38970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f60c7c38970b" alt="Roller-Coaster Model" title="Roller-Coaster Model" src="/assets/image_ded743.jpg" border="0" /></a> <br />

</center>

<p>Finally, a reception desk was added using a similar process (spline as generator) to give a sense of scale to the project:-</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330134892b0ca4970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330134892b0ca4970c" alt="Roller-Coaster Render" title="Roller-Coaster Render" src="/assets/image_edb7ad.jpg" border="0" /></a> <br />

</center>

<p>Many thanks to Ritchie for these inspiring ideas and images!
