---
layout: "post"
title: "Getting the centroid of an AutoCAD region using .NET"
date: "2015-08-17 08:27:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Object properties"
original_url: "https://www.keanw.com/2015/08/getting-the-centroid-of-an-autocad-region-using-net.html "
typepad_basename: "getting-the-centroid-of-an-autocad-region-using-net"
typepad_status: "Publish"
---

<p>This week we’re going to look at an interesting problem: how to create text that fits into a particular space. The scenario was originally presented (to me, anyway) by Alex Fielder, early last year (thanks, Alex!), but it’s taken a while for me to get to it. Alex wanted to check for the extents of block attributes overflowing their containers. I may well go ahead and implement that, in due course, but first I wanted to let the user select a space and create some text to fill it.</p>
<p>Let’s take a look at how to make this happen. Here’s the flow of operations:</p>
<ol>
<li>The user selects a point</li>
<li>Call Editor.TraceBoundary() to determine the containing space</li>
<li>Call Region.CreateFromCurves() with the resulting geometry</li>
<li>Determine the centroid of the Region</li>
<li>Check whether the centroid is actually inside the Region</li>
<li>If it is, then generate some text to place (we could also have asked the user for this, of course)…</li>
<li>… and then calculate the size of the text such that it fits entirely into the space</li>
</ol>
<p>Step 7 is probably the most interesting, in that we iteratively adjust the size of the text and test whether its extents fall within the Region. But more on that, later in the week.</p>
<p>For now we have a “simple” problem… step 4. Regions do have this information available – you can access it via the MASSPROP command, for instance – but it’s the first time I’ve used the .NET API to get it. It turned out to be worthy of its own blog post, so here it is.</p>
<p>Here’s the C# code:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> RegionalActivities</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Region extensions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Get the centroid of a Region.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;param name=&quot;cur&quot;&gt;</span><span style="color: green;">An optional curve used to define the region.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">A nullable Point3d containing the centroid of the Region.</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Point3d</span>? GetCentroid(<span style="color: blue;">this</span> <span style="color: #2b91af;">Region</span> reg, <span style="color: #2b91af;">Curve</span> cur = <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (cur == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> idc = <span style="color: blue;">new</span> <span style="color: #2b91af;">DBObjectCollection</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; reg.Explode(idc);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (idc.Count == 0)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cur = idc[0] <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (cur == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">null</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cs = cur.GetPlane().GetCoordinateSystem();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> o = cs.Origin;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> x = cs.Xaxis;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> y = cs.Yaxis;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> a = reg.AreaProperties(<span style="color: blue;">ref</span> o, <span style="color: blue;">ref</span> x, <span style="color: blue;">ref</span> y);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pl = <span style="color: blue;">new</span> <span style="color: #2b91af;">Plane</span>(o, x, y);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> pl.EvaluatePoint(a.Centroid);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;COR&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> CentroidOfRegion()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect a region&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(<span style="color: #a31515;">&quot;\nMust be a region.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Region</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per = ed.GetEntity(peo);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = doc.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> reg = tr.GetObject(per.ObjectId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Region</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (reg != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pt = reg.GetCentroid();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nCentroid is {0}&quot;</span>, pt);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Some comments about this:</p>
<ul>
<li>To get the “area properties” of a Region – which include the Region’s centroid, perimeter, radii of gyration, moments of inertia, etc. – we first need to specify a plane. Region doesn’t have an implementation for Entity.GetPlane(), so we have to determine this using some of its boundary geometry. In the upcoming post, we pass in one of the Curves we’d used to generate the Region, but in this post we’re going to have to explode the selected Region to get at its boundary.</li>
<li>Region.AreaProperties() will reject a plane that isn’t well-defined, as per the AutoCAD .NET reference:
<ul>
<li><em>This function calculates the area properties of the Region. All of the values in the returned RegionAreaProperties struct are in the coordinate system specified by origin, xAxis, and yAxis (which must be in WCS coordinates). The function validates the origin, xAxis, and yAxis parameters to ensure that the axes are of unit length and are perpendicular to each other, and that the axes and origin lie in the same plane as the Region.</em></li>
</ul>
</li>
<li>The specific curve used to define the plane shouldn’t matter, too much: as we’re using the plane to evaluate and return a Point3d, which curve was chosen is ultimately unimportant (providing it works, of course :-).</li>
</ul>
<p>To make sure it does work, here’s an example of running both the COR and MASSPROP commands, selecting a Region we’ve created manually on an arbitrary plane in 3D space.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d148ce22970c-pi"><img alt="Region" height="318" src="/assets/image_622489.jpg" style="float: none; margin: 50px auto; display: block;" title="Region" width="374" /></a></p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">Command: <span style="color: #ff0000;">COR</span></p>
<p style="margin: 0px;">Select a region:</p>
<p style="margin: 0px;">Centroid is (24.8686431313716,-11.7227175502621,7.71521951836058)</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Command: <span style="color: #ff0000;">MASSPROP</span></p>
<p style="margin: 0px;">Select objects: 1 found</p>
<p style="margin: 0px;">Select objects:</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">----------------&#0160;&#0160; REGIONS&#0160;&#0160; ----------------</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Area:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 112.3602</p>
<p style="margin: 0px;">Perimeter:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 43.1524</p>
<p style="margin: 0px;">Bounding box:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; X: 18.2540&#0160; --&#0160; 31.6548</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Y: -15.4236&#0160; --&#0160; -8.3921</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Z: 1.8974&#0160; --&#0160; 14.8993</p>
<p style="margin: 0px;">Centroid:&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; X: 24.8686</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Y: -11.7227</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Z: 7.7152</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Write analysis to a file? [Yes/No] &lt;N&gt;: <span style="color: #ff0000;">N</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Looks good! In the next post we’ll see how this integrates into our “space labelling” application.</p>
