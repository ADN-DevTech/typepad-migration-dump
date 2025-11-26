---
layout: "post"
title: "Maya MMeshIntersector and memory"
date: "2012-07-04 01:19:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Geometry"
  - "Maya"
  - "Modeling"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/07/maya-mmeshintersector-and-memory.html "
typepad_basename: "maya-mmeshintersector-and-memory"
typepad_status: "Publish"
---

<p>Before anything &#39;<strong>Happy 4th of July</strong>&#39; to our american readers!</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017615dfa961970c-pi" style="display: inline;"><img alt="Usa flag" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017615dfa961970c" src="/assets/image_599ba1.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Usa flag" /></a><br />In the documentation of MMeshIntersector::create(MObject &amp;meshObject, const MMatrix &amp;matrix), it says &quot;This method creates the data required by the intersector. It is a heavy operation that should be called only when necessary.&quot;</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017742b10cea970d-pi" style="display: inline;"><img alt="Tg_tut11_3" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017742b10cea970d" src="/assets/image_37b83b.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Tg_tut11_3" width="200" /></a><br />Internally, that class will tessellate the mesh, if it hasn&#39;t already been, and will build an octree for its triangles, which is going to be at least as heavy as the mesh itself and probably heavier.</p>
<p>However there are three ways for doing mesh intersection&#0160;in Maya:</p>
<ul>
<li>First, MFnMesh::intersect() which is not threaded and very slow. But it has no memory overhead.</li>
<li>Second, MFnMesh:closestIntersection() which uses grid-based intersection and is super-fast with very little memory overhead. Not threaded either, but fast.</li>
<li>Third, MMeshIntersector which is fast but uses an octree which consumes memory. Its main advantage is that its fully threaded.</li>
</ul>
<p>From one of my colleague experience, the grid-based intersection works well, but it really depends on how often you plan to do an intersection test on the same object, how many polygons the object has, and whether you can take advantage of multi-threading.</p>
<p>If you want to limit memory overhead, I think the grid-based approach will be very svelte in terms of usage. I know that the octree can get big on larger geometries.</p>
