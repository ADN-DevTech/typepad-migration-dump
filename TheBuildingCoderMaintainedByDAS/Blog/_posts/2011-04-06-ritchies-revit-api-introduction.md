---
layout: "post"
title: "Ritchie's Revit API Introduction"
date: "2011-04-06 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Geometry"
  - "Getting Started"
  - "News"
  - "Training"
  - "VSTA"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/04/ritchies-revit-api-introduction.html "
typepad_basename: "ritchies-revit-api-introduction"
typepad_status: "Publish"
---

<p>We have already seen a couple of interesting contributions by Ritchie Jackson of the 

<a href="http://www.aac.bartlett.ucl.ac.uk">
Adaptive Architecture and Computation</a>

programme at UCL, the 

<a href="http://en.wikipedia.org/wiki/University_College_London">
University College London</a>:

<!--
472_hermite_splines.htm:<!-- Ritchie Jackson [r
480_constructability.htm:<!-- Ritchie Jackson [
483_project_vasari_api.htm:<p>Ritchie Jackson a
495_flatten_profile.htm:<p>Anyway, here is anot
-->

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/11/blends-hermite-splines-and-derivatives.html">Blends, Hermite splines and derivatives</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/11/complexity-versus-constructability.html">Complexity versus constructability</a>
<li>The Autodesk <a href="http://thebuildingcoder.typepad.com/blog/2010/11/project-vasari-api.html">Project Vasari API</a>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/12/flatten-a-non-planar-extrusion-profile.html">Flattening a non planar extrusion profile</a>
</ul>

<p>Here is another topic, an introduction to the Revit API for programming novices for the 

<a href="http://www.lrug.org.uk">
London Revit User Group</a> LRUG.

Knowing Ritchie, it includes a couple of novel aspects:

<ul>
<li>Focus on

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/vsta-to-stay-and-updater-to-go.html#1">
VSTA</a> macros.

<li>Comparison of manual versus API approach.

<li>Focus on geometry generation.

</ul>

<p>The source code snippets cover the generation of some extremely simple shapes whilst the sample projects Ritchie presents feature the API workflow in more advanced examples:

<ul>
<li>A single line.
<li>A simple box extrusion.
<li>A box with a cut-out.
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e874250bb970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e874250bb970d" alt="A box with a cut-out" title="A box with a cut-out" src="/assets/image_205cbb.jpg" border="0" /></a> <br />

</center>

<ul>
<li>Iteration to generate a series of boxes with cut-outs to represent a boardwalk.
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e3c200ca970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e3c200ca970b image-full" alt="Boardwalk" title="Boardwalk" src="/assets/image_ce8545.jpg" border="0" /></a> <br />

</center>

<ul>
<li>An amphitheatre set-out:</li>
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e87425254970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e87425254970d" alt="Amphitheatre" title="Amphitheatre" src="/assets/image_c3a0da.jpg" border="0" /></a> <br />

</center>

<ul>
<li>A roller-coaster reception with all its elements and materials:</li>
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e6066deae970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e6066deae970c" alt="Roller-coaster reception" title="Roller-coaster reception" src="/assets/image_902797.jpg" border="0" /></a> <br />

</center>

<ul>
<li>A curved truss with all its elements:</li>
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e3c2030a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e3c2030a970b" alt="Curved truss" title="Curved truss" src="/assets/image_ba18a5.jpg" border="0" /></a> <br />

</center>

<ul>
<li>A conceptual high-rise model including 
<ul>
<li>Fa&ccedil;ade set-out
<li>Fa&ccedil;ade panels
<li>Fa&ccedil;ade materials
<li>Floor plates
<li>Beams
<li>Beam materials
</ul>
</li>
</ul>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e874253cf970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e874253cf970d" alt="Highrise" title="Highrise" src="/assets/image_42ae75.jpg" border="0" /></a> <br />

</center>

<p>The presentation includes more details on all of these items, obviously, descriptions of the workflows, and a detailed quick start guide for creating VSTA macros.
Some of these samples were also mentioned in Ritchie's previous contributions.

<p>Here is Ritchie's complete 

<span class="asset  asset-generic at-xid-6a00e553e168978833014e874255e7970d"><a href="http://thebuildingcoder.typepad.com/files/lrug-01-api-ritchiejackson.pdf">Revit API Introduction Presentation</a></span>

with the accompanying 


<span class="asset  asset-generic at-xid-6a00e553e168978833014e874257c3970d"><a href="http://thebuildingcoder.typepad.com/files/lrug-01-api-ritchiejackson-notes.pdf">Notes</a></span>

and


<span class="asset  asset-generic at-xid-6a00e553e168978833014e874258ee970d"><a href="http://thebuildingcoder.typepad.com/files/lrug-01-api-code-ritchiejackson.zip">Source Code samples</a></span>.

Very many thanks to Ritchie for sharing this with us!
