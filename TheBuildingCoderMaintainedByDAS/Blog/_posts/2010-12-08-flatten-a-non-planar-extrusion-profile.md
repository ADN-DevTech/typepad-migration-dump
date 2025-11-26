---
layout: "post"
title: "Flatten a Non-Planar Extrusion Profile"
date: "2010-12-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/flatten-a-non-planar-extrusion-profile.html "
typepad_basename: "flatten-a-non-planar-extrusion-profile"
typepad_status: "Publish"
---

<p>We arrived safely in Moscow yesterday for the next developer conference, taking place today.
It is not as cold as expected, just 5 degrees Centigrade below zero, quite tolerable after Tel Aviv.
And no beach.

<p>Anyway, here is another contribution from Ritchie Jackson of the

<a href="http://www.aac.bartlett.ucl.ac.uk">
Adaptive Architecture and Computation</a> programme

at UCL, the

<a href="http://en.wikipedia.org/wiki/University_College_London">
University College London</a>, who 

already presented information on

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/blends-hermite-splines-and-derivatives.html">
blends, Hermite splines and derivatives</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/complexity-versus-constructability.html">
complexity versus constructability</a>, 

and using the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/11/project-vasari-api.html">
project Vasari API</a>.

He now discusses an interesting aspect of creating an extrusion of a non-planar profile:

<p>I've found this useful on occasion when needing to create extrusions from profiles consisting of a non-planar element array &ndash; it works as long as the end-points of element pairs are coincident.</p>

<p>The 'Extrusion' function will automatically use the z-Coord of the second end-point of the first element in the CurveArray to establish the extrusion's base offset from the working plane.</p>

<p>In the image below the sketch profile consists of an arc and two lines in 3D. 
After the extrusion was created using the API the original curves were exposed by selecting 'Edit Extrusion' in the current view showing them to be still in 3D. 
However, if the API is used to access the Edges of the bottom Face, all evaluated parameters return a consistent z-Value &ndash; enabling a 2D 'Flatten' function. 
The curve in plan will no longer be a true arc as the original end-point z-Values were different:-</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c680f8fe970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c680f8fe970c" alt="Flatten non-planar extrusion profile" title="Flatten non-planar extrusion profile" src="/assets/image_ecbb2f.jpg" border="0" /></a> <br />

</center>
