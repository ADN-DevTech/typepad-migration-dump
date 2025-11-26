---
layout: "post"
title: "Unmodified Element Geometry"
date: "2010-02-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Relationships"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/02/unmodified-element-geometry.html "
typepad_basename: "unmodified-element-geometry"
typepad_status: "Publish"
---

<p>Still on vacation in Andalusia, and still not finished fixing the roof, since it never stops raining.
The weather is so bad I have nothing better to do than write a brand new post on how to obtain the original unmodified geometry of an object intersected by another building element:

<p><strong>Question:</strong> I have two building elements which intersect, for instance like this:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301310f2fdb20970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301310f2fdb20970c" alt="Intersecting elements" title="Intersecting elements" src="/assets/image_11ea42.jpg" border="0"  /></a> <br />

</center>

<p>The two may be walls, slabs, columns, beams or other elements.
I am trying to obtain the coordinates of the corner points of the top face of element2, i.e. (p1, p2, p3, p4).
Instead, the Tessellate method is returning the vertices (p1, pp1, pp2, pp3, p3, p4) of the modified geometry.
What can I do to obtain the vertices of the original, unmodified geometry?

<p><strong>Answer:</strong> I see that you have two elements which are mutually intersecting. 
The geometry being returned to you is the result of subtracting the other element geometry from the one you are interested in. 
What you are trying to obtain is the original, unmodified geometry. 

<p>Probably the most reliable way to solve this problem is to use the doc.Delete approach for temporary element deletion described by Scott Conover on obtaining the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/02/material-quantity-extraction.html">
gross material quantities</a>

of an element.

<p>Some other aspects of mutually intersecting elements in the specific case of family instances are discussed in the post on

<a href="http://thebuildingcoder.typepad.com/blog/2010/02/retrieving-column-and-stair-geometry.html">
column and stair geometry</a>.

<p>If the element2 that you are analysing is a wall, and the element1 cutting off part of it is a column, you might also find the FindReferencesByDirection technique useful to determine the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/01/findreferencesbydirection.html">
columns intersecting the wall</a>, 

i.e. which columns to temporarily delete.
