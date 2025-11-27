---
layout: "post"
title: "Using AutoCAD 2009's new transient graphics API to show point clouds from F#"
date: "2008-03-10 14:05:27"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "ObjectARX"
  - "Point clouds"
original_url: "https://www.keanw.com/2008/03/using-autocad-2.html "
typepad_basename: "using-autocad-2"
typepad_status: "Publish"
---

<p>To start off my series of more in-depth looks at the new APIs provided in AutoCAD 2009, I decided to extend some recently posted F# code to generate and draw transient point clouds to be slightly less transient: we&#39;ll see how to use the new transient graphics API in AutoCAD to display a cache of transient graphics, even after the view has been changed.</p>
<p>Some of you may be wondering about the amount of code I&#39;m posting in F#. I find the technology extremely interesting and am also increasingly productive with it, so I&#39;ve found myself gravitating towards using it more for my blog samples. I understand it&#39;s not for everyone, and I will definitely continue to post C# on a regular basis, but as I&#39;m currently spending quite a lot of time on F# I&#39;m somewhat selfishly posting what I&#39;m doing, rather than duplicating effort.</p>
<p>A few people have asked me by email &quot;so should I be learning F# now, rather than C#?&quot;. I generally recommend to people to carry on learning how to program in C# (or VB.NET, for that matter, although I personally prefer the syntax in C#), as this skill is currently more relevant in the industry than F# programming. I really like F#, but for me it&#39;s another (for now, secondary) tool for solving certain classes of problem. I will say, however, that learning functional programming makes you a better programmer overall, and FP techniques are making their way into more mainstream languages, such as VB.NET and C#. For instance, today we&#39;re going to be using a lambda expression to register an event-handler: anonymous functions or lambda expressions are now part of C#.</p>
<p>Why does FP make you a better programmer? Because it leads you away from relying on shared state and side-effects. I won&#39;t get into the details of that now, but reducing your reliance on shared state is a good thing for your code: at some point in the future it will more easily harness parallel processing capabilities such as multicore chips. So even if you don&#39;t use F# on a daily basis, the way that you look at problems after you&#39;ve understood its fundamental approach could - one day - have significant implications on your code&#39;s performance.</p>
<p>I digress, although this has reminded me that I&#39;ve been meaning to post a comparative piece on programming technologies, sometime soon.</p>
<p>Here&#39;s the F# code from <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/recursive-f-cod.html" target="_blank">this previous post</a>, modified to make use of the transient graphics API in AutoCAD 2009 to draw its points (rather than using Editor.DrawVector() with a zero-length vector, which is how we did it last time).</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Use lightweight F# syntax</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#light</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Declare a specific namespace and module name</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">module</span> MyNamespaceRecursive.MyApplication</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Import managed assemblies</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2009&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.GraphicsInterface</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Get a random vector on a plane</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> randomVectorOnPlane pl =</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Create our random number generator</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> ran = <span style="COLOR: blue">new</span> System.Random()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// First we get the absolute value</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// of our x, y and z coordinates</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> absx = ran.NextDouble()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> absy = ran.NextDouble()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Then we negate them, half of the time</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> x = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absx <span style="COLOR: blue">else</span> absx</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> y = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absy <span style="COLOR: blue">else</span> absy</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> v2 = <span style="COLOR: blue">new</span> Vector2d(x,y)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">new</span> Vector3d(pl,v2)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Get a random vector in 3D space</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> randomVector3d() =</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Create our random number generator</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> ran = <span style="COLOR: blue">new</span> System.Random()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// First we get the absolute value</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// of our x, y and z coordinates</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> absx = ran.NextDouble()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> absy = ran.NextDouble()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> absz = ran.NextDouble()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Then we negate them, half of the time</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> x = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absx <span style="COLOR: blue">else</span> absx</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> y = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absy <span style="COLOR: blue">else</span> absy</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> z = <span style="COLOR: blue">if</span> (ran.NextDouble() &lt; 0.5) <span style="COLOR: blue">then</span> -absz <span style="COLOR: blue">else</span> absz</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">new</span> Vector3d(x, y, z)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Create some state to store information about</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// the current view. We use this to determine</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// when we need to update our transient</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// graphics.</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> <span style="COLOR: blue">mutable</span> vd = <span style="COLOR: blue">new</span> Vector3d(0.0,0.0,0.0)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> <span style="COLOR: blue">mutable</span> vt = 0.0</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> <span style="COLOR: blue">mutable</span> vh = 0.0</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Check the view against our stored info:</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// if anything has changed, update the</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// cache and return true.</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> viewChanged (vtr : ViewTableRecord) =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">if</span> (vd &lt;&gt; vtr.ViewDirection ||</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;vt &lt;&gt; vtr.ViewTwist ||</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;vh &lt;&gt; vtr.Height) <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; vd &lt;- vtr.ViewDirection</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; vt &lt;- vtr.ViewTwist</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; vh &lt;- vtr.Height</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">false</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Here&#39;s where we&#39;ll store our list of DBPoint objects</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// to be redrawn</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> <span style="COLOR: blue">mutable</span> savedpts = []</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: green">// Now we declare our command</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;pts&quot;</span>)&gt;]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">let</span> createPoints () =</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Let&#39;s get the usual helpful AutoCAD objects</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> doc =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; Application.DocumentManager.MdiActiveDocument</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> ed = doc.Editor</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> db = doc.Database</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">use</span> tr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; db.TransactionManager.StartTransaction();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> bt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(db.BlockTableId,OpenMode.ForRead)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; :?&gt; BlockTable</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> ms =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; tr.GetObject</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(bt.[BlockTableRecord.ModelSpace],</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;OpenMode.ForRead)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; :?&gt; BlockTableRecord</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// a list of random points on its surface</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> getNPoints n (sol:Solid3d) ptlist =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">if</span> n &lt;= 0 <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ptlist</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">else</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> mp = sol.MassProperties</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> pl = <span style="COLOR: blue">new</span> Plane()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pl.Set(mp.Centroid,randomVector3d())</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> reg = sol.GetSection(pl)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> ray = <span style="COLOR: blue">new</span> Ray()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ray.BasePoint &lt;- mp.Centroid</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ray.UnitDir &lt;- randomVectorOnPlane pl</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> pts = <span style="COLOR: blue">new</span> Point3dCollection()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;reg.IntersectWith</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (ray,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; Intersect.OnBothOperands,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; pts,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; 0, 0)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;pl.Dispose()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;reg.Dispose()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;ray.Dispose()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;getNPoints</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (n - pts.Count) sol</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; (ptlist @ Seq.untyped_to_list pts)</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> generatePoints numPoints (x : ObjectId) =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; | :? Solid3d <span style="COLOR: blue">-&gt;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">let</span> sol = (obj :?&gt; Solid3d)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;getNPoints numPoints sol []</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; | _ <span style="COLOR: blue">-&gt;</span> []</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Create a DBPoint from a Point3d</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> to_db_point pt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> dbp = <span style="COLOR: blue">new</span> DBPoint(pt)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; dbp.ColorIndex &lt;- 1</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; dbp</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Add a single point (or any &quot;drawable&quot; object, for that</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// matter) to the transient graphics manager.</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> drawTransient x =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> tm = TransientManager.CurrentTransientManager</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">let</span> ic = <span style="COLOR: blue">new</span> IntegerCollection()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; tm.AddTransient</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;(x, TransientDrawingMode.DirectShortTerm, 0, ic)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; |&gt; ignore</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// We&#39;ll generate 100K points per solid</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// (the below line simply defined a new function</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// by currying (fixing one argument for) another</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// function)</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">let</span> points = generatePoints 100000</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Save the points we generate in our mutable state</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; savedpts &lt;-</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; Seq.untyped_to_list ms |&gt;&#0160; <span style="COLOR: green">// ObjectIds from modelspace</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;List.map points |&gt;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Get points for each object</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; List.concat |&gt;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// No need for the outer list</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; List.map to_db_point <span style="COLOR: green">// Get DBPoints</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// And then add each point to the transient graphics system</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; List.iter drawTransient savedpts</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; tr.Commit()</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// Add an event handler to respond to the doc-lock changed</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// event. This happens after every doc-centric command</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// (for instance), so we check whether the view has changed</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: green">// before starting a potentially time-consuming operation.</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; Application.DocumentManager.DocumentLockModeChanged.Add</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; (<span style="COLOR: blue">fun</span> _ <span style="COLOR: blue">-&gt;</span> </p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> viewChanged (ed.GetCurrentView()) <span style="COLOR: blue">then</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">for</span> pt <span style="COLOR: blue">in</span> savedpts <span style="COLOR: blue">do</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">let</span> tm = TransientManager.CurrentTransientManager</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">let</span> ic = <span style="COLOR: blue">new</span> IntegerCollection()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tm.UpdateTransient(pt, ic) |&gt; ignore)</p></div>
<p>Some interesting points about this code:</p>
<ul>
<li>We now store a list (potentially a very big list) of points in memory, in the savedpts variable 
<ul>
<li>These are DBPoints, as they need to be &quot;drawable&quot; to be managed by the transient graphics subsystem</li>
</ul>
</li>
<li>The use of the new transient graphics API is in the drawTransient function, which does the equivalent of this C# call: 
<ul>
<li>Autodesk.AutoCAD.GraphicsInterface. TransientManager.CurrentTransientManager.AddTransient(pt, TransientDrawingMode.DirectShortTerm, 0, new IntegerCollection()); </li>
<li>This API can be used to draw any &quot;drawable&quot; object - it doesn&#39;t have to be one that would typically be stored in the DWG file. It can be used to display custom glyphs and tooltips, for instance 
<ul>
<li>Check out the ObjectARX (C++) sample under <em>ObjectARX 2009\samples\graphics\AsdkTransientGraphicsSampFolder</em> for more details</li>
</ul>
</li>
</ul>
</li>
<li>We register an event handler to update the display of these points when the view has changed 
<ul>
<li>We do not currently have a viewChanged event exposed through the managed API, so we check for DocumentLockChanged and then see whether the view has changed there 
<ul>
<li>We store some state about the previous view, so we know when it has changed</li>
</ul>
</li>
<li>This event handler calls the equivalent of this C# code: 
<ul>
<li>Autodesk.AutoCAD.GraphicsInterface. TransientManager.CurrentTransientManager.UpdateTransient(pt, new IntegerCollection());</li>
</ul>
</li>
<li>See how easy it is to register an event handler in F#: the use of lambda expressions makes this really trivial (no need to define a function that we specify as a delegate). We also use the underscore (&quot;_&quot;) to state we don&#39;t care about the arguments passed to the event handler, in this particular situation. Very neat.</li>
</ul>
</li>
</ul>
<ul>
</ul>
<p>Here&#39;s what happens when we run the PTS command on a set of 6 solids:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Big%20cloud%20in%202009.png"><img alt="Big cloud in 2009" border="0" height="188" src="/assets/Big%20cloud%20in%202009_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="244" /></a> </p>
<p>We can see the points disappear when we perform a 3DORBIT:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Orbiting%20big%20cloud%20in%202009.png"><img alt="Orbiting big cloud in 2009" border="0" height="188" src="/assets/Orbiting%20big%20cloud%20in%202009_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="244" /></a> </p>
<p>When we exit the orbit, our old point graphics are displayed...</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Graphics%20catching%20up%20in%202009.png"><img alt="Graphics catching up in 2009" border="0" height="188" src="/assets/Graphics%20catching%20up%20in%202009_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="244" /></a></p>
<p>Until the event handler kicks in and updates the display of our points:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Graphics%20updated%20in%202009.png"><img alt="Graphics updated in 2009" border="0" height="188" src="/assets/Graphics%20updated%20in%202009_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="244" /></a></p>
