---
layout: "post"
title: "Intersecting AutoCAD curves with a plane using .NET"
date: "2015-09-14 15:56:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
original_url: "https://www.keanw.com/2015/09/intersecting-autocad-curves-with-a-plane-using-net.html "
typepad_basename: "intersecting-autocad-curves-with-a-plane-using-net"
typepad_status: "Publish"
---

<p>This question came in via <a href="http://through-the-interface.typepad.com/through_the_interface/2013/03/finding-a-point-on-an-autocad-surface-based-on-xy-coordinates-using-net-part-1.html#comment-2247594658" target="_blank">a comment</a>, last week:</p>
<blockquote>
<p>Is it possible to get the point of intersection between a spline/3dpolyline and a plane surface?</p>
</blockquote>
<p>It turned out to be quite an interesting problem to solve. I started with brute force methods for the various curve types…</p>
<p>For Polylines I checked linear segments against the plane (this is an intersection we can test), and for arc segments I broke these into linear segments and checked those, too. It all worked well with a high enough “resolution” for arc segments.</p>
<p>Polyline3ds only have linear segments, so I could safely use the line-plane intersection approach for these, too.</p>
<p>Splines were more tricky. I started by converting them to Polylines with lots of linear (no arc) segments, before processing them as Polylines. This worked well enough, but was *really* slow to get accurate results. So I went back to the drawing board… I realised that if I projected the Spline onto the plane of interest, I could then check the new, projected curve against the original: the intersection points between the two curves should be the intersections between the Spline and the plane.</p>
<p>This worked perfectly! And it turns out that I could do this on all curves – not just Splines – so I ended up ripping out pretty much all the code I’d written up to that point. Coding is a process, after all, and the code we’re left with is nice and clean: no need for any curve faceting, etc.</p>
<p>Here’s the code in action on a set of curves intersecting a planar surface. The top curve is a Polyline, the next a Spline, the third a Polyline3d and – for good measure – the last is a Polyline2d.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1562e62970c-pi"><img alt="Intersecting curves and a plane" height="474" src="/assets/image_590563.jpg" style="float: none; margin: 50px auto; display: block;" title="Intersecting curves and a plane" width="474" /></a></p>
<p>Here’s the C# code defining our CPI (for CurvePlaneIntersections) command and the various extension methods upon which it relies:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> EntityIntersections</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Returns an array of Point3d objects from a Point3dCollection.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">An array of Point3d objects.</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Point3d</span>[] ToArray(<span style="color: blue;">this</span> <span style="color: #2b91af;">Point3dCollection</span> pts)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> res = <span style="color: blue;">new</span> <span style="color: #2b91af;">Point3d</span>[pts.Count];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pts.CopyTo(res, 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> res;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Get the intersection points between this planar entity and a curve.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;param name=&quot;cur&quot;&gt;</span><span style="color: green;">The curve to check intersections against.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">An array of Point3d intersections.</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Point3d</span>[] IntersectWith(<span style="color: blue;">this</span> <span style="color: #2b91af;">Plane</span> p, <span style="color: #2b91af;">Curve</span> cur)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pts = <span style="color: blue;">new</span> <span style="color: #2b91af;">Point3dCollection</span>();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the underlying GeLib curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> gcur = cur.GetGeCurve();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Project this curve onto our plane</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> proj = gcur.GetProjectedEntity(p, p.Normal) <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve3d</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (proj != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create a DB curve from the projected Ge curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> gcur2 = <span style="color: #2b91af;">Curve</span>.CreateFromGeCurve(proj))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Check where it intersects with the original curve:</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// these should be our intersection points on the plane</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cur.IntersectWith(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; gcur2, <span style="color: #2b91af;">Intersect</span>.OnBothOperands, pts, <span style="color: #2b91af;">IntPtr</span>.Zero, <span style="color: #2b91af;">IntPtr</span>.Zero</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> pts.ToArray();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Test whether a point is on this curve.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;param name=&quot;pt&quot;&gt;</span><span style="color: green;">The point to check against this curve.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Boolean indicating whether the point is on the curve.</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> IsOn(<span style="color: blue;">this</span> <span style="color: #2b91af;">Curve</span> cv, <span style="color: #2b91af;">Point3d</span> pt)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Return true if operation succeeds</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> p = cv.GetClosestPointTo(pt, <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> (p - pt).Length &lt;= <span style="color: #2b91af;">Tolerance</span>.Global.EqualPoint;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> { }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Otherwise we return false</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;CPI&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> CurvePlaneIntersection()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">null</span> == doc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Ask the user to select a curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect a curve&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(<span style="color: #a31515;">&quot;Must be a curve.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Curve</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per = ed.GetEntity(peo);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> curId = per.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// And a PlaneSurface to check intersections against</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo2 = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect plane surface&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(<span style="color: #a31515;">&quot;Must be a planar surface.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">PlaneSurface</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per2 = ed.GetEntity(peo2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per2.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> planeId = per2.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = doc.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Start by opening the plane</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> plane = tr.GetObject(planeId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">PlaneSurface</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (plane != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the PlaneSurface&#39;s defining GeLib plane</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> p = plane.GetPlane();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Open our curve...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cur = tr.GetObject(curId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (cur != <span style="color: blue;">null</span>) <span style="color: green;">// Should never fail</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pts = p.IntersectWith(cur);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// If we have results we&#39;ll place them in modelspace</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ms =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">SymbolUtilityServices</span>.GetBlockModelSpaceId(db),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Point3d</span> pt <span style="color: blue;">in</span> pts)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create a point in the drawing for each intersection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> dbp = <span style="color: blue;">new</span> <span style="color: #2b91af;">DBPoint</span>(pt);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dbp.ColorIndex = 2; <span style="color: green;">// Make them yellow</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ms.AppendEntity(dbp);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(dbp, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Quick test to make sure each point lies on our source curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;\nPoint is {0} curve.&quot;</span>, cur.IsOn(pt) ? <span style="color: #a31515;">&quot;on&quot;</span> : <span style="color: #a31515;">&quot;off&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
