---
layout: "post"
title: "Wall and Opening Tessellation"
date: "2017-05-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Export"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/05/wall-and-opening-tessellation.html "
typepad_basename: "wall-and-opening-tessellation"
typepad_status: "Publish"
---

<p>Let's turn to a geometric question on tessellation, expanding on the discussion
on <a href="http://thebuildingcoder.typepad.com/blog/2014/05/tessellatesolidorshell-holes-versus-wholes.html">TessellateSolidOrShell &ndash; holes versus wholes</a>.</p>

<h4><a name="2"></a>Question</h4>

<p>I am exporting Revit wall geometry to an external application for comparison of net and gross wall areas.</p>

<p>I have a problem with the fact that <code>Face.Triangulate(double levelOfDetail)</code> creates a different tessellation segmentation of neighbouring faces for the wall and its opening, although the underlying intersection curve between them is obviously the same.</p>

<p>Why does the tessellation segmentation differ in this manner? The <code>levelOfDetail</code> in unchanged.</p>

<p>What can I do to get the same segmentation for the wall and its opening?</p>

<p>Here is an example image:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2855bc1970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2855bc1970c img-responsive" alt="Curved wall opening tessellation" title="Curved wall opening tessellation" src="/assets/image_65c609.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Note that the segmentations of the exported wall and opening solids differ:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2855bce970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2855bce970c image-full img-responsive" alt="Curved wall opening tessellation" title="Curved wall opening tessellation" src="/assets/image_de7314.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>I also tried to use the method <code>TessellateSolidOrShell(Solid, SolidOrShellTessellationControls)</code>, but the result is similar and the segmentation of the exported wall and opening solids differs there too, cf. the discussion
on <a href="http://thebuildingcoder.typepad.com/blog/2014/05/tessellatesolidorshell-holes-versus-wholes.html">TessellateSolidOrShell &ndash; holes versus wholes</a>.
 </p>

<h4><a name="3"></a>Answer</h4>

<p>Actually, the problem in this case is different.</p>

<p>I don’t think the user’s request is reasonable, for reasons I state below.
 
The user wants to calculate wall areas (and other quantities) of a wall with and without openings: 'net' and 'gross' areas.</p>

<p>The Building Coder published a whole series of blog posts on that topic:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/12/retrieving-wall-openings-and-sorting-points.html">Retrieving wall openings and sorting points</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/12/wall-opening-profiles-and-happy-holidays.html">Wall opening profiles</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/determining-wall-opening-areas-per-room.html">Determining wall opening areas per room</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/more-on-wall-opening-areas-per-room.html">More on wall opening areas per room</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/01/family-category-and-two-energy-model-types.html">Two energy model types</a></li>
</ul>

<p>To calculate these areas, they subtract the wall with openings from the wall without openings to get solids representing the shapes of the openings. Their application requires faceted shapes, so they triangulate these various 3D shapes. The wall with an opening, triangulated looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8fb1cb2970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8fb1cb2970b img-responsive" style="width: 219px; " alt="Curved wall opening tessellation" title="Curved wall opening tessellation" src="/assets/image_b0b0ce.jpg" /></a><br /></p>

<p></center></p>

<p>Here is the shape of the opening, triangulated:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb099e34f9970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb099e34f9970d img-responsive" style="width: 172px; " alt="Curved wall opening tessellation" title="Curved wall opening tessellation" src="/assets/image_dc742d.jpg" /></a><br /></p>

<p></center></p>

<p>The user says that they then merge the triangulated opening shape into the triangulated wall-with-opening shape to get the 'gross' wall without openings, in triangulated form, so that they can calculate the face areas for the 'gross' wall. Their merge operation requires the triangulations of the two objects to match, so it fails in this case because the triangulations of the opening shape’s faces differs from the triangulations of the corresponding faces of the wall-with-openings.</p>

<!--
1. I don’t understand this workflow. If they were already able to get the wall-without-openings, they can calculate the areas of its faces directly, so why do they bother trying to merge triangulated objects?
2. There’s no reason why Revit would make the triangulations of the faces of the opening match the triangulations of the corresponding faces of the opening shape.
-->

<p>Unfortunately, the Revit triangulation process is not aimed at merging the resulting triangulated objects.</p>

<p>Therefore, there is no reason why Revit would make the triangulations of the faces of the opening match the triangulations of the corresponding faces of the opening shape.</p>

<p>For example, the triangulation of the bottom face of the opening must match the triangulation of the vertical face below it (on the side of the wall facing us). But the vertical face is the entire side face of the wall (facing us), and its triangulation depends on the overall shape of that face. By contrast, the vertical face that’s facing us on the opening shape has a different overall shape than the corresponding face on the wall, so it’s to be expected that it may get a different triangulation.
The conditions on the bottom faces of the wall and the opening shape imposed by adjacent faces are therefore different in general, so one shouldn’t expect that the triangulations of those two faces will 'match', or that the triangulations of the side face of the opening shape that’s facing us will match the triangulation of the portion of the wall’s side face that lies below the opening.</p>

<p>In general, it seems that in order to avoid certain limitations in the user’s tools and algorithms, they want to impose conditions on Revit’s solid triangulation API that are not really reasonable.</p>
