---
layout: "post"
title: "Scripting the generation of hyperbolic tessellations inside AutoCAD"
date: "2012-01-27 05:23:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Fractals"
  - "Geometry"
original_url: "https://www.keanw.com/2012/01/scripting-the-generation-of-hyperbolic-tessellations-inside-autocad.html "
typepad_basename: "scripting-the-generation-of-hyperbolic-tessellations-inside-autocad"
typepad_status: "Publish"
---

<p>To follow on from <a href="http://through-the-interface.typepad.com/through_the_interface/2012/01/an-interesting-challenge-generating-variable-density-fill-patterns-for-3d-printing.html" target="_blank">this</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2012/01/generating-hyperbolic-geometry-on-a-poincar-disk-in-autocad-using-net.html" target="_blank">recent</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2012/01/generating-hyperbolic-tessellations-inside-autocad-using-net.html" target="_blank">topic</a>, today’s post looks at a simple script to generate various hyperbolic tessellations, laying them out in an order that makes some sense of the progressive nature of the patterns that can be generated using the HT command.</p>
<p>Here’s an AutoCAD script (which can be saved as an .scr and executed using the SCRIPT command) to generate all the valid {<em>n k</em>} patterns where <em>n</em> &lt;= 11 and <em>k</em> &lt;= 7 (<em>n</em> being the number of sides in each polygon, <em>k</em> is the number of polygons that meet at each vertex). The application module implementing the HT command (using the code in the previous post, must be NETLOADed before this script will function properly, of course).</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">_zoom _w 0,0 50,25</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; k=3 series</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 25,5 0.2 0 {7 3}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 25,5 2 _ht l 7 3 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 30,5 0.2 0 {8 3}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 30,5 2 _ht l 8 3 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 35,5 0.2 0 {9 3}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 35,5 2 _ht l 9 3 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 40,5 0.2 0 {10 3}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 40,5 2 _ht l 10 3 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 45,5 0.2 0 {11 3}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 45,5 2 _ht l 11 3 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; k=4 series</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 15,10 0.2 0 {5 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 15,10 2 _ht l 5 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 20,10 0.2 0 {6 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 20,10 2 _ht l 6 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 25,10 0.2 0 {7 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 25,10 2 _ht l 7 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 30,10 0.2 0 {8 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 30,10 2 _ht l 8 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 35,10 0.2 0 {9 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 35,10 2 _ht l 9 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 40,10 0.2 0 {10 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 40,10 2 _ht l 10 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 45,10 0.2 0 {11 4}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 45,10 2 _ht l 11 4 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; k=5 series</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 10,15 0.2 0 {4 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 10,15 2 _ht l 4 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 15,15 0.2 0 {5 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 15,15 2 _ht l 5 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 20,15 0.2 0 {6 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 20,15 2 _ht l 6 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 25,15 0.2 0 {7 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 25,15 2 _ht l 7 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 30,15 0.2 0 {8 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 30,15 2 _ht l 8 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 35,15 0.2 0 {9 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 35,15 2 _ht l 9 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 40,15 0.2 0 {10 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 40,15 2 _ht l 10 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 45,15 0.2 0 {11 5}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 45,15 2 _ht l 11 5 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; k=6 series</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 10,20 0.2 0 {4 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 10,20 2 _ht l 4 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 15,20 0.2 0 {5 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 15,20 2 _ht l 5 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 20,20 0.2 0 {6 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 20,20 2 _ht l 6 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 25,20 0.2 0 {7 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 25,20 2 _ht l 7 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 30,20 0.2 0 {8 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 30,20 2 _ht l 8 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 35,20 0.2 0 {9 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 35,20 2 _ht l 9 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 40,20 0.2 0 {10 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 40,20 2 _ht l 10 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 45,20 0.2 0 {11 6}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 45,20 2 _ht l 11 6 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">; k=7 series</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 5,25 0.2 0 {3 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 5,25 2 _ht l 3 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 10,25 0.2 0 {4 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 10,25 2 _ht l 4 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 15,25 0.2 0 {5 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 15,25 2 _ht l 5 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 20,25 0.2 0 {6 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 20,25 2 _ht l 6 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 25,25 0.2 0 {7 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 25,25 2 _ht l 7 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 30,25 0.2 0 {8 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 30,25 2 _ht l 8 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 35,25 0.2 0 {9 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 35,25 2 _ht l 9 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 40,25 0.2 0 {10 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 40,25 2 _ht l 10 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_text _j _m 45,25 0.2 0 {11 7}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_circle 45,25 2 _ht l 11 7 _s</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">_zoom _e</span></p>
</div>
<p>Just looking at the output results (extracted manually from the command-line output), we can see the level used and the number of polygons created for each tessellation:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">{7 3} (level 5) =&gt; 617 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{8 3} (level 4) =&gt; 609 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{9 3} (level 3) =&gt; 271 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{10 3} (level 3) =&gt; 421 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{11 3} (level 3) =&gt; 617 polygons.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">{5 4} (level 4) =&gt; 761 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{6 4} (level 3) =&gt; 505 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{7 4} (level 2) =&gt; 127 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{8 4} (level 2) =&gt; 177 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{9 4} (level 2) =&gt; 235 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{10 4} (level 2) =&gt; 301 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{11 4} (level 2) =&gt; 375 polygons.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">{4 5} (level 4) =&gt; 913 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{5 5} (level 3) =&gt; 841 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{6 5} (level 2) =&gt; 199 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{7 5} (level 2) =&gt; 295 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{8 5} (level 2) =&gt; 409 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{9 5} (level 2) =&gt; 541 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{10 5} (level 2) =&gt; 691 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{11 5} (level 2) =&gt; 859 polygons.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">{4 6} (level 3) =&gt; 673 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{5 6} (level 2) =&gt; 221 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{6 6} (level 2) =&gt; 361 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{7 6} (level 2) =&gt; 533 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{8 6} (level 2) =&gt; 737 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{9 6} (level 2) =&gt; 973 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{10 6} (level 1) =&gt; 41 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{11 6} (level 1) =&gt; 45 polygons.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">{3 7} (level 4) =&gt; 496 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{4 7} (level 2) =&gt; 181 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{5 7} (level 2) =&gt; 351 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{6 7} (level 2) =&gt; 571 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{7 7} (level 2) =&gt; 841 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{8 7} (level 1) =&gt; 41 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{9 7} (level 1) =&gt; 46 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{10 7} (level 1) =&gt; 51 polygons.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{11 7} (level 1) =&gt; 56 polygons.</span></p>
</div>
<p>And here are the patterns, themselves:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016760cb39ed970b-pi" target="_blank"><img alt="A selection of hyperbolic tesselations" border="0" height="268" src="/assets/image_74842.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A selection of hyperbolic tesselations" width="474" /></a></p>
<p>Just to see the difference, here are the same tessellations with curved geometry (as per the implementation in the second post in the series). Created using a simple search &amp; replace in the above script, from “_s” to “_c”.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20162ffd788e9970d-pi" target="_blank"><img alt="A selection of hyperbolic tesselations with curves" border="0" height="264" src="/assets/image_155774.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="A selection of hyperbolic tesselations with curves" width="474" /></a></p>
<p>Thinking about future directions for this investigation… (some of which have been suggested by Alex Fielder, after previewing drafts of these posts…)</p>
<ul>
<li>Clearly it’d be great to get to 3D and get the results integrated with Inventor and Autodesk’s Simulation (FEA) tools. </li>
<li>Remaining in 2D, it’d be interesting to see about supporting non-circular boundaries. 
<ul>
<li>One option for this would be to use the <a href="http://through-the-interface.typepad.com/through_the_interface/2011/02/creating-the-smallest-possible-circle-around-2d-autocad-geometry-using-net.html" target="_blank">minimal enclosing circle</a> around the geometry as the boundary, and then trim back the results or filter them based on whether they’re inside or outside/intersecting the enclosing geometry. </li>
<li>Ideally the fill pattern would be generated in a way that followed the shape being filled, rather than relying on a circle at all. That seems more of a stretch, though, given the nature of the geometry.</li>
</ul>
</li>
</ul>
<p>If you have your own ideas on uses and directions for this implementation, please post a comment!</p>
