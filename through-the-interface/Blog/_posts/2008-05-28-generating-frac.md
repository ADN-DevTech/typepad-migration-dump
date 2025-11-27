---
layout: "post"
title: "Generating fractals inside AutoCAD using F#"
date: "2008-05-28 16:55:10"
author: "Kean Walmsley"
categories: []
original_url: "https://www.keanw.com/2008/05/generating-frac.html "
typepad_basename: "generating-frac"
typepad_status: "Publish"
---

<p>Some of you may remember my interest in fractals from <a href="http://through-the-interface.typepad.com/through_the_interface/2007/07/generating-koch.html">these two</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2007/08/generating-koch.html">previous posts</a>. Well, while researching a problem in F# (related to the conversion of the last post's code to F#), I stumbled across <a href="http://blogs.msdn.com/lukeh/archive/2007/11/14/f.aspx">this post from Luke Hoban</a>, which contains some neat, recursive F# code to generate <a href="http://en.wikipedia.org/wiki/Mandelbrot_set">the Mandelbrot set</a>, sending the result to the console as ASCII text. I couldn't resist modifying the code to generate Solids (filled shapes with 3 or 4 sides, as opposed to Solid3d objects) inside AutoCAD.</p>

<p>Here's the F# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> Mandelbrot</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="color: #a31515;">@&quot;C:\Program Files\Autodesk\AutoCAD 2009&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="color: #a31515;">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#nowarn <span style="color: #a31515;">&quot;57&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Geometry</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Microsoft.FSharp.Math</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> maxIteration = 50</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> granularity = 400</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> modSquared (c : Complex) = c.r * c.r + c.i * c.i</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> MandelbrotResult = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | DidNotEscape</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | Escaped <span style="COLOR: blue">of</span> int</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> mandelbrot c = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> <span style="COLOR: blue">rec</span> mandelbrotInner z iterations = </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">if</span>(modSquared z &gt;= 4.0)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">then</span> Escaped iterations</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">elif</span> iterations = maxIteration</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">then</span> DidNotEscape</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">else</span> mandelbrotInner ((z * z) + c) (iterations + 1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; mandelbrotInner c 0</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="color: #a31515;">&quot;MB&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> drawMandelbrot () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> tm = doc.TransactionManager</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tm.StartTransaction()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTR</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;OpenMode.ForWrite)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Now let's create our geometry</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> xgran = 1.0 / Int32.to_float granularity</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ygran = 1.0 / Int32.to_float granularity</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">for</span> y <span style="COLOR: blue">in</span> [-1.0..xgran..1.0] <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">for</span> x <span style="COLOR: blue">in</span> [-2.0..ygran..1.0] <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">match</span> mandelbrot (Complex.Create (x, y)) <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| DidNotEscape <span style="COLOR: blue">-&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">let</span> pt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Solid</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: blue">new</span> Point3d(x,y,0.0),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d(x+xgran,y,0.0),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d(x,y+ygran,0.0),</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">new</span> Point3d(x+xgran,y+ygran,0.0))</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ms.AppendEntity(pt) |&gt; ignore</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.AddNewlyCreatedDBObject(pt, <span style="COLOR: blue">true</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;| Escaped _ <span style="COLOR: blue">-&gt;</span> ()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tm.QueueForGraphicsFlush()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tm.FlushGraphics()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.UpdateScreen()</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tr.Commit()</p></div><p>And here's what happens when we run the MB command:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Mandelbrot.png"><img height="205" alt="Mandelbrot" src="/assets/Mandelbrot_thumb.png" width="244" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>Here's a zoomed in area, so you can see the pixelation effect of using square Solids:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Mandelbrot%20-%20zoomed.png"><img height="140" alt="Mandelbrot - zoomed" src="/assets/Mandelbrot%20-%20zoomed_thumb.png" width="240" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>You can, of course, vary the maxIteration and granularity parameters in the code, if you want to play around with the results (or even modify the code to ask for the values at the command-line, to save rebuilding the app all the time).</p>

<p>A quick note: at first the app was running slowly and causing memory issues, before I enabled <a href="http://usa.autodesk.com/adsk/servlet/ps/item?siteID=123112&amp;id=9583842&amp;linkID=9240617">the 3Gb switch on my Vista machine</a>. Now things are much better, but then that could also be because I've just rebooted. :-)</p>
