---
layout: "post"
title: "Recursive F# code to generate random point clouds inside AutoCAD"
date: "2008-02-29 16:29:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
original_url: "https://www.keanw.com/2008/02/recursive-f-cod.html "
typepad_basename: "recursive-f-cod"
typepad_status: "Publish"
---

<p>At the beginning of the week, we looked at <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/more-random-mus.html">some iterative F# code</a> to generate random point clouds inside AutoCAD. We then took the time to <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/using-reflector.html">use Reflector to dig under the hood</a> and understand why the previous recursive implementation was causing stack problems.</p>

<p>For completeness (and - I admit it - being driven slightly by laziness, as this is a quick post to crank out :-) here's the recursive version of the random point cloud generation code in F#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Use lightweight F# syntax</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Declare a specific namespace and module name</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespaceRecursive.MyApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Import managed assemblies</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Get a random vector on a plane</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> randomVectorOnPlane pl =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Create our random number generator</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ran = <span style="COLOR: blue">new</span> System.Random()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First we get the absolute value</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// of our x, y and z coordinates</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> absx = ran.NextDouble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> absy = ran.NextDouble()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Then we negate them, half of the time</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> x = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absx <span style="COLOR: blue">else</span> absx</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> y = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absy <span style="COLOR: blue">else</span> absy</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> v2 = <span style="COLOR: blue">new</span> Vector2d(x,y)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> Vector3d(pl,v2)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Get a random vector in 3D space</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Note: _ is only used to make sure this function gets</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// executed when it is called... if we have no argument</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// it's a value that doesn't require repeated execution</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> randomVector3d _ =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Create our random number generator</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ran = <span style="COLOR: blue">new</span> System.Random()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// First we get the absolute value</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// of our x, y and z coordinates</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> absx = ran.NextDouble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> absy = ran.NextDouble()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> absz = ran.NextDouble()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Then we negate them, half of the time</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> x = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absx <span style="COLOR: blue">else</span> absx</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> y = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absy <span style="COLOR: blue">else</span> absy</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> z = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absz <span style="COLOR: blue">else</span> absz</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">new</span> Vector3d(x, y, z)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Now we declare our command</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;ptsr&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> createPoints () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// a list of random points on its surface</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> getNPoints n (sol:Solid3d) ptlist =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span> n &lt;= 0 <span style="COLOR: blue">then</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ptlist</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> mp = sol.MassProperties</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pl = <span style="COLOR: blue">new</span> Plane()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pl.Set(mp.Centroid,randomVector3d n)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> reg = sol.GetSection(pl)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> ray = <span style="COLOR: blue">new</span> Ray()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.BasePoint &lt;- mp.Centroid</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.UnitDir &lt;- randomVectorOnPlane pl</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> pts = <span style="COLOR: blue">new</span> Point3dCollection()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;reg.IntersectWith</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (ray,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Intersect.OnBothOperands,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; pts,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 0, 0)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;pl.Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;reg.Dispose()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ray.Dispose()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;getNPoints</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (n - pts.Count) sol</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (ptlist @ Seq.untyped_to_list pts)</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> generatePoints numPoints (x : ObjectId) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | :? Solid3d <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">let</span> sol = (obj :?&gt; Solid3d)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;getNPoints numPoints sol []</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> []</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// A recursive function to show the contents of a list</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> drawPointList (x:Point3d list) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> x <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | [] <span style="COLOR: blue">-&gt;</span> ()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | h :: t <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.DrawVector(h,h,1,<span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;drawPointList t</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's generate 100K points per solid</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> points = generatePoints 100000</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Here's where we plug everything together...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Seq.untyped_to_list ms |&gt; <span style="COLOR: green">// ObjectIds from modelspace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; List.map points |&gt;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: green">// Get points for each object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;List.concat |&gt;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: green">// No need for the outer list</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; drawPointList&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: green">// Draw the resultant points</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tr.Commit()</p></div>

<p>This code is much more elegant from a functional perspective, and F#'s tail call optimization means the resultant code runs just as efficiently as if we'd used iterative code with mutable state. To make the code optimizable, I had to adjust the arguments to the genNPoints function to accept an accumulator object (being the list of points generated thus far). Appending to this list, rather than the one being returned by the next recursion call, allows F# to optimize the recursion into a loop.</p>

<p>Thanks to Namin for his comments on the last post - they were very helpful to my understanding of the problem.</p>

<p>Next week I'm going to try to spend some time diving into the new APIs coming with AutoCAD 2009.</p>
