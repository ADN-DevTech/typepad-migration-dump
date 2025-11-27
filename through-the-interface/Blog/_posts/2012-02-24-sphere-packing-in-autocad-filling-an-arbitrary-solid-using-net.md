---
layout: "post"
title: "Sphere packing in AutoCAD: filling an arbitrary solid using .NET"
date: "2012-02-24 05:53:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
  - "Solid modeling"
original_url: "https://www.keanw.com/2012/02/sphere-packing-in-autocad-filling-an-arbitrary-solid-using-net.html "
typepad_basename: "sphere-packing-in-autocad-filling-an-arbitrary-solid-using-net"
typepad_status: "Publish"
---

<p>As we’re nearing the end of this series, it seems a good time to do a quick recap of where we’ve been with the posts leading up to this point. Here goes…</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/01/an-interesting-challenge-generating-variable-density-fill-patterns-for-3d-printing.html">An interesting challenge: generating variable density fill patterns for 3D printing</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/01/generating-hyperbolic-geometry-on-a-poincar-disk-in-autocad-using-net.html">Generating hyperbolic geometry on a Poincaré disk in AutoCAD using .NET</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/01/generating-hyperbolic-tessellations-inside-autocad-using-net.html">Generating hyperbolic tessellations inside AutoCAD using .NET</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/01/scripting-the-generation-of-hyperbolic-tessellations-inside-autocad.html">Scripting the generation of hyperbolic tessellations inside AutoCAD</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-net.html">Circle packing in AutoCAD: creating an Apollonian gasket using .NET</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-1.html">Circle packing in AutoCAD: creating an Apollonian gasket using F# – Part 1</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/circle-packing-in-autocad-creating-an-apollonian-gasket-using-f-part-2.html">Circle packing in AutoCAD: creating an Apollonian gasket using F# – Part 2</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-1.html">Sphere packing in AutoCAD: creating an Apollonian packing using F# – Part 1</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-2.html">Sphere packing in AutoCAD: creating an Apollonian packing using F# – Part 2</a> <br /><a target="_blank" href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-3.html">Sphere packing in AutoCAD: creating an Apollonian packing using F# – Part 3</a></p>
<p>Until now, the focus on the series has largely been around relatively pure mathematical approaches to fill simple (in our case circular or spherical) areas/volumes: we’ve so far avoided having to adapt the filling/packing algorithm depending on the form selected.</p>
<p>Today’s post changes that: it uses a very different approach to fill a selected Solid3d with spheres. A bit like the “hyperbolic tessellation” patterns we saw, early on, we want to create smaller spheres at the surface of the solid, with the radius gradually increasing, layer upon layer, as we get towards the centre.</p>
<p>Something like this sketch, created by my colleague Francesco Tonioni (as mentioned in the previous post) as we brainstormed optimal filling strategies (from a strength – rather than material – perspective):</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20168e7cc894c970c-pi"><img title="Initial sketch of fill algorithm" width="390" alt="Initial sketch of fill algorithm" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_558227.jpg" border="0" height="263" /></a></p>
<p>To do this for an arbitrary solid, we’re using this basic approach:</p>
<ol>
<li>Section a solid along an appropriate plane (one that’s defined by two user-selected points and the view direction) and create a ring of (initially very small) spheres along the resultant curve. </li>
<li>Move the points along in one direction (normal to the plane) by the diameter of the sphere. Repeat step 1 (and then 2) until you run out of solid to section. </li>
<li>Starting from the initially-created section, move the points in the other direction repeating the process until you hit the other end of the solid. </li>
<li>Now that you’ve got a complete layer of spheres at the surface of the solid, offset the solid by the diameter of the spheres and start again with a slightly larger sphere diameter. </li>
</ol>
<p>There are a host of devils in the detail, of course: to reduce the chances of any of the outer layer of spheres intersecting the surface of the solid, we need to know the curvature of the surface at a particular point (and while we get that right for relatively uniform surfaces, I’ll bet it’ll fail for highly irregular ones), which allows us to adjust the depth of the sphere at that point to make sure it fits.</p>
<p>In fact, here are the back-of-the-envelope sketches I drew to work out some of the trigonometry needed to find out how adjacent rings of spheres should be positioned:</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016301d59fa7970d-pi"><img title="Trigonometric calculations" width="470" alt="Trigonometric calculations" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_523939.jpg" border="0" height="164" /></a>These calculations are far from foolproof, so we iterate adjusting the values to see if we can get a better fit. Once again, this probably works well with solids with a uniform shape along the direction of our slices (the normal to the section vector) – so rotations, etc., can work very well, but I’d expect less uniform shapes to cause problems.</p>
<p>Here’s the C# code, also contained in <a target="_blank" href="http://through-the-interface.typepad.com/files/CircleAndSpherePacking.zip">the project with the various code files for this series</a>. I ended up using C# for this post, as it had a lot to do with accessing and managing AutoCAD geometry: the times I’ve used F# of late have been when I’ve had more “pure” calculations to perform. There’s no strict need for this division, but I do like having a split between core algorithms and AutoCAD-specific code. And while it might seem a bit artificial to use a language barrier to enforce it, it does feel like it plays to the strengths of the respective languages.</p>
<p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> SpherePacking</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PackingCommands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;PACKSPHERES&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> PackSolidWithSpheres()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We need a solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> peo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect solid to fill&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; peo.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nMust be a Solid3d.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; peo.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(peo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> solId = per.ObjectId;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// And an initial section plane</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (the view direction is used to define the third point)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;"> ppo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect first point for initial section plane&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> first = ppr.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; ppo.Message =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect second point for initial section plane&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; ppo.BasePoint = first;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; ppo.UseBasePoint = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; ppo.UseDashedLine = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> second = ppr.Value;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We either generate spheres or subtract them from the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// selected solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptKeywordOptions</span><span style="line-height: 140%;"> pko =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptKeywordOptions</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;\nSubtract spheres from original?&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pko.AllowNone = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pko.Keywords.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;Yes&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pko.Keywords.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;No&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pko.Keywords.Default = </span><span style="color: #a31515; line-height: 140%;">&quot;No&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">PromptResult</span><span style="line-height: 140%;"> pkr = ed.GetKeywords(pko);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pkr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> subtract = (pkr.StringResult == </span><span style="color: #a31515; line-height: 140%;">&quot;Yes&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We need a transaction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; db.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// And we also need the modelspace open for write</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; db.BlockTableId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bt[</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the view direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">ViewportTableRecord</span><span style="line-height: 140%;"> vtr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">ViewportTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ed.ActiveViewportId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> viewDir = vtr.ViewDirection;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Open the selected solid for write</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;"> outer =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tr.GetObject(solId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> cancelled = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (outer != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create our sphere packer and use it to fill the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// selected solid with spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">SpherePacker</span><span style="line-height: 140%;"> sp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SpherePacker</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; db, tr, btr, outer, first, second, viewDir, subtract</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; sp.FillWithSpheres();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cancelled = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!cancelled)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SpherePacker</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> _db = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> _tr = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> _btr = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;"> _parent = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">, _sol = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> _first, _second;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> _viewDir;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> _sub = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">ProgressMeter</span><span style="line-height: 140%;"> _pm = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Constructor</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> SpherePacker(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db, </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr, </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> btr,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;"> parent, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> first, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> second,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> viewDir, </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> subtract</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Set the member data from the arguments</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _db = db;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _tr = tr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _btr = btr;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _parent = parent;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _sub = subtract;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _first = first;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _second = second;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _viewDir = viewDir;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Make a copy of our outer solid, which we will</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// repeatedly shrink for each layer of spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _sol = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _sol.CopyFrom(_parent);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Add it to the drawing</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; AddToDrawing(_sol);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Make sure we have target layers in place for each layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// of spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; CreateLayers();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the name for a layer based on its integer level</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (a very simple function, but nice to have it centralised</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// in case we want to do things differently)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> GetLayer(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> layer.ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create a set of layers for our spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">internal</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateLayers()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Open the layer table for write</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">LayerTable</span><span style="line-height: 140%;"> lt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; (</span><span style="color: #2b91af; line-height: 140%;">LayerTable</span><span style="line-height: 140%;">)_tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _db.LayerTableId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Loop through, creating 10 layers</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">short</span><span style="line-height: 140%;"> i = 1; i &lt;= 10; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> name = GetLayer(i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!lt.Has(name))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">LayerTableRecord</span><span style="line-height: 140%;"> ltr = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">LayerTableRecord</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ltr.Color =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">Color</span><span style="line-height: 140%;">.FromColorIndex(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Autodesk.AutoCAD.Colors.</span><span style="color: #2b91af; line-height: 140%;">ColorMethod</span><span style="line-height: 140%;">.ByAci, i</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ltr.Name = name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lt.Add(ltr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _tr.AddNewlyCreatedDBObject(ltr, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Our main method</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">internal</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> FillWithSpheres()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create and start a progress meter (the limit is slightly</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// arbitrary - it works well for spheres, but is likely to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// work less well for other forms)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _pm = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ProgressMeter</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _pm.SetLimit(180);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _pm.Start(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _sub ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;Subtracting spheres from selected solid&quot;</span><span style="line-height: 140%;"> :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;Filling solid with spheres&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Start with layer 1 and a radius of 0.2 (which should</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// get overwritten immediately of the solid has valid</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// extents)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer = 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius = 0.2;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_sol.Bounds.HasValue)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Set the radius relative to the solid's extents</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Extents3d</span><span style="line-height: 140%;"> ext = _sol.Bounds.Value;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> vec = ext.MaxPoint - ext.MinPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> mag = vec.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; radius = mag / 150;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Shrink the body by half the radius of the first layer,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// just to give us a little gap between the spheres</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// and the outer solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _sol.OffsetBody(-0.5 * radius);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create layers of spheres, starting with the initial</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// radius value and multiplying by a factor for each</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// new layer</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> r = radius; r &lt; radius * 20; r *= 1.3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; GenerateSectionsAcrossSolid(r, layer++);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Shrink the body by the diameter of the layer's spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _sol.OffsetBody(-2 * r);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We won't attempt to fill volumes that aren't large</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// enough (we check the radius against the volume)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_sol.MassProperties.Volume &lt; 350 * r)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Set our shrinking solid to be the next level down</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (via its layer)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _sol.Layer = GetLayer(layer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If subtracting, do so for this remaining solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_sub)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _parent.BooleanOperation(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BooleanOperationType</span><span style="line-height: 140%;">.BoolSubtract, _sol</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Rethrow any exception, letting is get caught in our</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// command and dealt with there</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">throw</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Make sure we stop our progress meter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; _pm.Stop();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Generate a layer of spheres across the complete solid</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">internal</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> GenerateSectionsAcrossSolid(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Use a coordinate system object to work out the direction</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// vector for us to move the section plane</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (should be perpendicular to the section plane, basically)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">CoordinateSystem3d</span><span style="line-height: 140%;"> cs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">CoordinateSystem3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, _viewDir, _second - _first</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> unitVec = cs.Zaxis / cs.Zaxis.Length;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Local variables that get adjusted by each call to our</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// GenerateSpheresAlongSection method</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt1 = _first,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt2 = _second,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; prevCen = </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> prevCur = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> prevDep = 0.0;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create a ring of spheres around our initial section curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; GenerateSpheresAlongSection(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt1, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt2, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCen, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCur, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevDep,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; unitVec, radius, 0, layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevCur != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; prevCur.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; prevCur = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> done = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> initCen = prevCen;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> initDep = prevDep;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Now go all the way across in one direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; done =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; !GenerateSpheresAlongSection(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt1, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt2, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCen, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCur, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevDep,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; unitVec, radius, 1, layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (!done);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevCur != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; prevCur.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; prevCur = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// And then go all the way in the other direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pt1 = _first;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; pt2 = _second;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; prevCen = initCen;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; prevDep = initDep;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; done =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; !GenerateSpheresAlongSection(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt1, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> pt2, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCen, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevCur, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> prevDep,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; unitVec, radius, -1, layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (!done);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevCur != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; prevCur.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Generate a ring of spheres along a single section</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> GenerateSpheresAlongSection(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt1, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt2, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> prevCen,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> prevCur, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> prevDep,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> unitVec, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> direction, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Tick our progress meter and check for user break</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _pm.MeterProgress();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; System.Windows.Forms.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DoEvents();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">HostApplicationServices</span><span style="line-height: 140%;">.Current.UserBreak())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevCur != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; prevCur.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">throw</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;User break&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Take local copies of the points defining the section</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> first = pt1, second = pt2;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Approximate the distance from the outer curve for the first</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// pass. Set some limits for our target distance from the last</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> bestDist = radius,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; bestDepth = radius,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; upperDistLimit = 2.1 * radius,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; lowerDistLimit = 2 * radius;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We'll count the number of iterations</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> tries = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> res = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> cur = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If we're creating ring in a direction along the surface,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// we need to transform the points defining the section</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (direction != 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> disp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Displacement(direction * unitVec * bestDist);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt1 = pt1.TransformBy(disp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt2 = pt2.TransformBy(disp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the curve from this section</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; cur = CurveFromSection(pt1, pt2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cur == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; FillRemainingCurve(prevCur, radius, prevDep, layer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; res = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the distance between the proposed position of the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// first sphere and the first sphere of the previous ring</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> cen = GetFirstCenter(cur, bestDepth);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> center2center = (cen - prevCen).Length;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If this is either the first time run (i.e. no previous</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// sphere) or the spheres are at an acceptable distance,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// skip the trigonometric calculations</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; prevCen.DistanceTo(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin) &gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint &amp;&amp;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (center2center &lt; lowerDistLimit ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; center2center &gt; upperDistLimit)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Only do the calculations if this is the first run,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// otherwise we will just adjust the values</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tries == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Calculate the distance to the next section</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get slope at the current point on the surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// (ideal would be to get the tangent at the mid-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">//&nbsp; point of the spheres, but we have to approximate)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> slope =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; GetSlopeAtPoint(pt1, pt2, unitVec, radius);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (slope.Length &lt; </span><span style="color: #2b91af; line-height: 140%;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; res = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the &quot;down&quot; vector based on the curve direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> down =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cur.GetPointAtParameter(cur.EndParam / 2) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cur.GetPointAtParameter(cur.StartParam);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the viewing plane</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> p = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, _viewDir))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Project the various vectors onto the plane</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Vector2d</span><span style="line-height: 140%;"> unit2d = unitVec.Convert2d(p),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; slope2d = slope.Convert2d(p),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; down2d = down.Convert2d(p);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the angle of the slope to the offset direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> theta = unit2d.GetAngleTo(slope2d);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the angle of the slope to the down direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> theta2 = down2d.GetAngleTo(slope2d);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Calculate the distance and the depth</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bestDist = </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Abs(2 * radius * </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Cos(theta));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bestDepth = </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Abs(radius / </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Sin(theta2));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Incrementally adjust the distance upwards or</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// downwards, depending on which side of ideal we are</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (center2center &lt; lowerDistLimit)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bestDist *= 1.05;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (center2center &gt; upperDistLimit)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; bestDist *= 0.95;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Reset the points, so that we start from scratch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// but with (hopefully) a better distance value</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt1 = first;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; pt2 = second;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; tries++;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Max out at 20 iterations (gives much better coverage</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// than 10)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tries &gt; 20)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> curLen =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cur.GetDistanceAtParameter(cur.EndParam);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> prevLen =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; prevCur.GetDistanceAtParameter(prevCur.EndParam);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Only fill previous curves when the current one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// is much shorter than the previous one</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (curLen &lt; prevLen / 4)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; FillRemainingCurve(prevCur, radius, prevDep, layer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; res = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We have spheres at an acceptable distance from</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// each other (or this is the first one)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; prevCen = cen;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; GenerateSpheresAlongCurve(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cur, radius, bestDepth, layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; res = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (prevCur != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; prevCur.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; prevCur = cur;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> res;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> FillRemainingCurve(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> cur, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> initDepth, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> inner = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; inner =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (initDepth == 0.0 ? cur : GetInnerCurve(cur, initDepth));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inner == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Fill the remaining space as best we can</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> inc = 2 * radius,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; depth = inc;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> done = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (!done)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; done =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; !GenerateSpheresAlongCurve(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inner, radius, depth, layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; depth += inc;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> { }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inner != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> &amp;&amp; initDepth &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inner.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return the slope of the solid's surface at a particular</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// section location</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> GetSlopeAtPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt1, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt2, </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> unitVec, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// To get the slope of the surface at the point we care</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// about, we'll take a section either side of the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// point - using 5% of the radius as the distance in </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// each direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">const</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> factor = 0.05;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Transform the points defining our section line in one</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> disp =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Displacement(radius * factor * unitVec);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt11 = pt1.TransformBy(disp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt12 = pt2.TransformBy(disp);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// And then the other direction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; disp = </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Displacement(-radius * factor * unitVec);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt21 = pt1.TransformBy(disp);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt22 = pt2.TransformBy(disp);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the defining curves of these two sections</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> c1 = CurveFromSection(pt11, pt12);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> c2 = CurveFromSection(pt21, pt22);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Assume their start points are at the same relative point</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (c1 != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> &amp;&amp; c2 != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return the angle between the two</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> c1.StartPoint - c2.StartPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">(0, 0, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> CurveFromSection(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt1, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Make a collection from our defining points</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3dCollection</span><span style="line-height: 140%;"> pts =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3dCollection</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">[2] { pt1, pt2 });</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Declare the arrays for our results</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Array</span><span style="line-height: 140%;"> fillEnts, bgEnts, fgEnts, fTangEnts, cTangEnts;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create the section and generate the geometry</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Section</span><span style="line-height: 140%;"> sec = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Section</span><span style="line-height: 140%;">(pts, _viewDir))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; sec.GenerateSectionGeometry(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _sol, </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> fillEnts, </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> bgEnts,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> fgEnts, </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> fTangEnts, </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> cTangEnts</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We only care about the fillEnts - dispose of the rest</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> bgEnts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> fTangEnts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> e </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> cTangEnts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; e.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (fillEnts.Length == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return the first curve, dispose of any remaining</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> c = fillEnts.GetValue(0) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 1; i &lt; fillEnts.Length; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> o = fillEnts.GetValue(i) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (o != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; o.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> c;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get an inner curve offset at a certain depth</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> GetInnerCurve(</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> c, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> depth)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We only return a single curve, if there is one.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If there are more, we return null</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> inners = c.GetOffsetCurves(-depth);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inners.Count == 1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> inners[0] </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> o </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> inners)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; o.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> { }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return the center of the 1st sphere to be placed on a curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> GetFirstCenter(</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> c, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Only handle closed curves</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (c.Closed)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the inner curve at the depth of the radius</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> inner = GetInnerCurve(c, radius))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return the start point of the curve (which we assume</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// will be at a consistent point across the various</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// section profiles)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inner != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> inner.StartPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Generate adjacent spheres along a curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> GenerateSpheresAlongCurve(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> curve, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> depth, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Only deal with closed curves</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!curve.Closed)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the inner curve at a certain depth</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> inner = GetInnerCurve(curve, depth))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Return null if we didn't get a curve or if it's too</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// short to realistically fill with spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inner == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inner.GetDistanceAtParameter(inner.EndParam) &lt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 8.85 * radius</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inner != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// For shorter curves we'll create one last</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// sphere at the center of the curve, assuming</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// it's a circle or an ellipse (spheres</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// generally generate ellipse section curves,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// it seems)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (inner </span><span style="color: blue; line-height: 140%;">is</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Ellipse</span><span style="line-height: 140%;"> || inner </span><span style="color: blue; line-height: 140%;">is</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Circle</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> lastCen =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; (inner </span><span style="color: blue; line-height: 140%;">is</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Ellipse</span><span style="line-height: 140%;"> ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ((</span><span style="color: #2b91af; line-height: 140%;">Ellipse</span><span style="line-height: 140%;">)inner).Center :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; ((</span><span style="color: #2b91af; line-height: 140%;">Circle</span><span style="line-height: 140%;">)inner).Center);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; CreateSphereAtPoint(lastCen, radius, layer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the curve's plane</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> p = inner.GetPlane();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the center of the first sphere (the curve's start</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// point) and make a local copy of it</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> cen = inner.StartPoint,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; initial = cen;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We'll step along the curve parametrically</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> curParam = inner.StartParam;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// A flag to indication completion</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> done = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We'll loop until we can't create more spheres</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (!done)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create a circle at the first location</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; CreateSphereAtPoint(cen, radius, layer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the center of the next sphere: cen and curParam</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// will get updated, and lastRadius will only get set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// in the case where there's only space to create a</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// smaller sphere</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> lastRadius;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; done =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; !GetNextSphereCenter(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; inner, p, radius, initial, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> cen, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> curParam,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> lastRadius</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If we're at the end of the ring, create the last</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// sphere</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (done &amp;&amp; lastRadius &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; CreateSphereAtPoint(cen, lastRadius, layer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Get the next sphere's center along the ring</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> GetNextSphereCenter(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> curve, </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> p, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius, </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> initial,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> cen, </span><span style="color: blue; line-height: 140%;">ref</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> curParam, </span><span style="color: blue; line-height: 140%;">out</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> lastRadius</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We may not return a lastRadius, so default to 0</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; lastRadius = 0.0;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Make a local copy of the previous sphere's center</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> prevCen = cen;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// We're going to use a circle on the plane of the curve</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// to find the center of our next sphere</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Circle</span><span style="line-height: 140%;"> c = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Circle</span><span style="line-height: 140%;">(cen, p.Normal, 2 * radius))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// The first - at the previous sphere's center - gets</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// intersected with the curve</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3dCollection</span><span style="line-height: 140%;"> pts = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3dCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; curve.IntersectWith(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; c, </span><span style="color: #2b91af; line-height: 140%;">Intersect</span><span style="line-height: 140%;">.OnBothOperands, pts,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">IntPtr</span><span style="line-height: 140%;">.Zero, </span><span style="color: #2b91af; line-height: 140%;">IntPtr</span><span style="line-height: 140%;">.Zero</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// For each of the intersection points, we get the parameter</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> pt </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> pts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> param = curve.GetParameterAtPoint(pt);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If the point's parameter is greater than the current</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// one, we'll use it</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (param &gt; curParam)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> distFromInitial = (pt - initial).Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (distFromInitial &gt;= (2 * radius * 0.98))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cen = pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curParam = param;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Fill the gap at the end of a ring by reducing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// the radius and adjusting the center to be the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// mid-point on the curve between the first and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// the previous spheres' centers</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curParam =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; curParam + ((curve.EndParam - curParam) / 2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; cen = curve.GetPointAtParameter(curParam);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> distFromInitial2 = (cen - initial).Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; lastRadius = distFromInitial2 - radius;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create a sphere at the given point</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateSphereAtPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> cen, </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> radius, </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> layer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Just make sure the center isn't at the origin (which</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// we've naughtily been using to denote an error) and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// that the radius is greater than zero</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; cen.DistanceTo(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin) &gt; </span><span style="color: #2b91af; line-height: 140%;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &amp;&amp; radius &gt; </span><span style="color: #2b91af; line-height: 140%;">Tolerance</span><span style="line-height: 140%;">.Global.EqualPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Create and position our sphere</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;"> sol = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Solid3d</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If we're subtracting, make the sphere 2% smaller, so</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// they don't touch each other</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; sol.CreateSphere(_sub ? radius * 0.98 : radius);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Move the sphere to the desired location</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> disp = </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Displacement(cen - </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; sol.TransformBy(disp);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Set its layer</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; sol.Layer = GetLayer(layer);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Add it to the modelspace and the transaction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; AddToDrawing(sol);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// If we're subtracting from the outer solid, do so</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (_sub)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; _parent.BooleanOperation(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style="color: #2b91af; line-height: 140%;">BooleanOperationType</span><span style="line-height: 140%;">.BoolSubtract, sol</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// Add an entity to the open block table record and the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: green; line-height: 140%;">// current transaction</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> AddToDrawing(</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _btr.AppendEntity(ent);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; &nbsp; _tr.AddNewlyCreatedDBObject(ent, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
</p>
<p>Let’s see what we get when we run the PACKSPHERES command selecting a sphere and a section line right through the middle.</p>
<p>At the top-left of the below image is the wireframe view of the sphere prior to packing, top-middle is the packed sphere, also in wireframe (and prior to graphics regeneration, which would lead to the inner layers not showing as well), and the subsequent shots are of the various layers of the solid in realistic view (one more layer of the onion is peeled off in each shot).</p>
<p>I didn’t bother hiding the last layer, as we can – in any case – see the inner solid and I prefer the symmetry of the below image as it stands. The inner solid is the one we have been using to generate each layer, and has been created by iterative offsets from the original sphere: by design we choose to stop the process - leaving the inner body – when the radius of the spheres we would be creating would be relatively too large to create a useful layer.</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20168e7cc8a58970c-pi"><img title="Packed sphere - different layers" width="475" alt="Packed sphere - different layers" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_629509.jpg" border="0" height="475" /></a></p>
<p>If you look at the model from a different angle, you’ll see some quirks: the poles have some gaps, for instance, and as spheres of a particular radius are not guaranteed to fill a ring completely, we fill the remainder with smaller spheres, where needed.</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016301d5a109970d-pi"><img title="Close up of the poles of our sphere packing" width="475" alt="Close up of the poles of our sphere packing" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_40475.jpg" border="0" height="368" /></a></p>
<p>There are alternative approaches which would avoid this “fault line” of small spheres: we could adjust the radius of the spheres very slightly to make sure they are even over the length of the curve, or we could rotate the whole curve by a random amount. Each approach ultimately has advantages and disadvantages, so I’ve left it as is for this initial implementation.</p>
<p>One benefit of an approach that takes sections across the length of a solid is that it can pack different forms full of spheres, too. For instance, here’s a bottle:</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016762ca78df970b-pi"><img title="Bottle packed with spheres" width="304" alt="Bottle packed with spheres" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_51782.jpg" border="0" height="523" /></a></p>
<p>And here’s a tree-like form (the algorithm needs some work to deal with sudden changes in size of the section curve, such as when we go from the lower part of the “leafy part” to the trunk):</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016762ca7906970b-pi"><img title="Tree packed with spheres" width="479" alt="Tree packed with spheres" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_685265.jpg" border="0" height="231" /></a></p>
<p>The PACKSPHERES command does have an option we haven’t looked at in this post, which is to automatically subtract the inner spheres from the outer solid. This can take a long time to work, but generates results that are quite like those in the last post (where we performed more-or-less the same process manually).</p>
<p>Here’s a section view of the resultant hollowed bottle when that option is selected (the size of the individual holes clearly depends on where the spheres happen to be relative to the section plane):</p>
<p><a target="_blank" href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016301d5a1b1970d-pi"><img title="Sectioned hollowed bottle" width="470" alt="Sectioned hollowed bottle" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" src="/assets/image_336779.jpg" border="0" height="288" /></a></p>
<p>AutoCAD’s STLOUT command generated a 257 MB STL file for the above solid. Thanks to Alex Fielder for suggesting <a target="_blank" href="http://www.netfabb.com/">netfabb</a> as a tool for viewing STL files (with the caveat that he hadn’t tested it on files quite that large): while netfabb Studio Basic managed to load the file, it unfortunately had difficulty slicing it.</p>
<p>But anyway – until someone invents a way to 3D print enclosed, hollow spheres without needing support material (perhaps <a target="_blank" href="http://en.wikipedia.org/wiki/Space_manufacturing">microgravity fabs in lunar orbit</a> would help – which is actually <a target="_blank" href="http://www.businessweek.com/technology/3d-printing-coming-to-the-manufacturing-spaceand-outer-space-01092012.html">less sci-fi than you might think</a> :-), this remains an intellectual – albeit entertaining (for me, at least) – exercise.</p>
<p>Next week we’re going to have a change of pace: as the <a target="_blank" href="http://autodesk.blogs.com/between_the_lines/2012/02/autocad-2013-news.html">news about</a> <a target="_blank" href="http://lynn.blogs.com/lynn_allens_blog/2012/02/autocad-2013-is-approaching-the-finish-line.html">AutoCAD 2013</a> is starting to hit the web, I’ll spend the week talking about the coming developer-oriented features in the 2013 release of AutoCAD.</p>
