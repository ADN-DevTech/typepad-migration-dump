---
layout: "post"
title: "Creating Fibonacci spirals in AutoCAD using F#"
date: "2009-06-04 10:38:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Fractals"
original_url: "https://www.keanw.com/2009/06/creating-fibonacci-spirals-in-autocad-using-f.html "
typepad_basename: "creating-fibonacci-spirals-in-autocad-using-f"
typepad_status: "Publish"
---

<p>I recently stumbled across <a href="http://www.russiantequila.com/wordpress/?p=70">this post</a> which inspired me to do something similar in AutoCAD (the fact that both posts cover <a href="http://en.wikipedia.org/wiki/Fibonacci_spiral">Fibonacci spirals</a> and use F# is about where the similarity ends - they do things quite differently).</p>
<p>Fibonacci spirals are an approximation of the <a href="http://en.wikipedia.org/wiki/Golden_spiral">golden spiral</a>, which for old timers out there will be reminiscent of the AutoCAD R12 (it was R12, wasn’t it?) design collateral - the same as <a href="http://www.flickr.com/photos/btl/1369064867/in/set-72157600140924927/">this one from AME 2.1</a> - which I still find cool after all these years. :-)</p>
<p>The first thing was to create a function that returns a portion of the Fibonacci sequence:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> fibs n =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Seq.unfold (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> (n0, n1) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> Some(n0, (n1, n0 + n1))) (0I,1I)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; |&gt; Seq.take n</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; |&gt; Seq.to_list</span></p></div>
<p>A few comments about this implementation:</p>
<ul>
<li>I searched online for <a href="http://en.wikipedia.org/wiki/Tail_recursion">tail-recursive</a> Fibonacci implementations (not that we’re likely to create a stack overflow with the number of recursions we’re going to do, but I like to do things right when I can :-) 
<ul>
<li>Tail-recursive solutions are easy if returning a specific number, but as we need to return a portion of the Fibonacci sequence things get a little more complicated </li>
<li>Here’s a quick reminder of <a href="http://through-the-interface.typepad.com/through_the_interface/2008/02/using-reflector.html">how we can check that tail call optimization has happened</a>, when we do choose to use tail recursion </li>
</ul>
</li>
<li>I ended up going for a <a href="http://en.wikipedia.org/wiki/Lazy_evaluation">lazily-evalulated</a> solution, <a href="http://books.google.ch/books?id=n1DEBdl_oloC&amp;pg=PA180&amp;lpg=PA180&amp;dq=foundations+of+F%23+Fibonacci&amp;source=bl&amp;ots=7883oHPQbw&amp;sig=jAsvawjLNlxlPwVBC5sVOWRFZpg&amp;hl=en&amp;ei=vnMnSsfHMYqD_AbjtbHJBQ&amp;sa=X&amp;oi=book_result&amp;ct=result&amp;resnum=1">copied and modified</a> from the <a href="http://www.amazon.com/Foundations-F-Experts-Voice-Net/dp/1590597575">Foundations of F#</a> book by <a href="http://www.strangelights.com/">Robert Pickering</a> 
<ul>
<li><a href="http://en.wikipedia.org/wiki/Anamorphism">Unfold</a> (OK – I admit this Wikipedia entry is beyond obscure for most of us mere mortals – <a href="http://blogs.msdn.com/jaredpar/archive/2008/10/07/unfold.aspx">this post</a> may be of more help) applies a function to a seed value to create what may be an infinite sequence of numbers (which is precisely what the Fibonacci sequence is, of course) </li>
<li>We use the Seq data-type (essentially an IEnumerable in .NET) which is lazy 
<ul>
<li>This means that it only actually evaluates the various items in the list as we ask for them </li>
</ul>
</li>
<li>We then “take” the first n items from the list (n will be specified by the user), so only that number of items get evaluated </li>
<li>We convert the results to a list to return to the caller </li>
</ul>
</li>
<li>I like the elegance of this solution and it’s certainly efficient enough for our purposes </li>
</ul>
<p>If you load this code into F# interactive and execute it against the first 20 numbers in the sequence, you get:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&gt; fibs 20;;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">val it : bigint list</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">= [0I; 1I; 1I; 2I; 3I; 5I; 8I; 13I; 21I; 34I; 55I; 89I; 144I; 233I; 377I; 610I;</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; 987I; 1597I; 2584I; 4181I]</span></p></div>
<p>Otherwise the below implementation should be reasonably straightforward. We define a local addSegment function which we then call on each member of the subset of the Fibonacci sequence (in reverse, as we’re drawing the curves from large to small). We use the iteri function to do this, as it provides us with a useful index into the list which then allows us to decide which of the four directions we’re facing (the orientation of the arc rotates 90 degrees each time).</p>
<p>Here’s the complete F# code:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">#light</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Declare our namespace and module name</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">module</span><span style="LINE-HEIGHT: 140%"> Fibonacci.Spiral</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// Import managed assemblies</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">open</span><span style="LINE-HEIGHT: 140%"> System</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: green">// A lazy Fibonacci sequence generator</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> fibs n =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Seq.unfold (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">fun</span><span style="LINE-HEIGHT: 140%"> (n0, n1) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> Some(n0, (n1, n0 + n1))) (0I,1I)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; |&gt; Seq.take n</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; |&gt; Seq.to_list</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">[&lt;CommandMethod(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;fib&quot;</span><span style="LINE-HEIGHT: 140%">)&gt;]</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> fibonacciSpiral() =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s get the usual helpful AutoCAD objects</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; Application.DocumentManager.MdiActiveDocument</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> db = doc.Database</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Ask the user how deep to go</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pio =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> PromptIntegerOptions(</span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;\nEnter number of levels: &quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.AllowNone &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.AllowZero &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.AllowNegative &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">false</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.DefaultValue &lt;- 10</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.LowerLimit &lt;- 1</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.UpperLimit &lt;- 50</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; pio.UseDefaultValue &lt;- </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pir = ed.GetInteger(pio)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> pir.Status = PromptStatus.OK </span><span style="LINE-HEIGHT: 140%; COLOR: blue">then</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;ll actually add one to the value provided</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// as this gives more logical results</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> levels = pir.Value + 1</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">use</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction()</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (db.BlockTableId,OpenMode.ForRead)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; :?&gt; BlockTable</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> ms =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (bt.[BlockTableRecord.ModelSpace],</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; OpenMode.ForWrite)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; :?&gt; BlockTableRecord</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create our polyline, set its defaults,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// add it to the modelspace and the transaction</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pl = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> Polyline()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; pl.SetDatabaseDefaults()</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; ms.AppendEntity(pl) |&gt; ignore</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(pl, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; pl.AddVertexAt</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; (pl.NumberOfVertices, Point2d.Origin, 0.0, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We need a mutable start point variable for</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// each of our arcs to connect</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> start = ref Point3d.Origin</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add an arc segment to our polyline</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> addSegment i size =</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// i is the index in the list provided by iteri</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Decide the directions of the &quot;axes&quot; and the arc&#39;s start</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// angle based on one of four possibilities </span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> (xdir, ydir, startAngle) =</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">match</span><span style="LINE-HEIGHT: 140%"> (i % 4) </span><span style="LINE-HEIGHT: 140%; COLOR: blue">with</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | 0 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> (Vector3d.XAxis, Vector3d.YAxis, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | 1 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> (-Vector3d.YAxis, Vector3d.XAxis, Math.PI * 1.5)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | 2 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> (-Vector3d.XAxis, -Vector3d.YAxis, Math.PI)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | 3 </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> (Vector3d.YAxis, -Vector3d.XAxis, Math.PI / 2.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; | _ </span><span style="LINE-HEIGHT: 140%; COLOR: blue">-&gt;</span><span style="LINE-HEIGHT: 140%"> failwith </span><span style="LINE-HEIGHT: 140%; COLOR: maroon">&quot;Invalid modulus remainder!&quot;</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The end angle is 90 degrees from the start</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> endAngle = startAngle + Math.PI / 2.0</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// The center of the arc is bottom right-hand corner</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// of the box (direction goes along the bottom from</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// left to right, so we go &quot;size&quot; units along the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// direction from the start point)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> center = !start + xdir * float size</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Bulge is defined as the tan of one quarter of the</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// included angle (and negative, as we&#39;re going</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// clockwise)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> bulge = Math.Tan((endAngle - startAngle) / -4.0)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We need to convert our 3D start point to a 2D point</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// on the plane of the polyline</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">let</span><span style="LINE-HEIGHT: 140%"> pos = (!start).Convert2d(pl.GetPlane())</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Now we just add the vertex at the end and mutate our</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// start variable to contain the end of the arc</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pl.AddVertexAt(pl.NumberOfVertices, pos, bulge, 0.0, 0.0)</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; start.contents &lt;- center + ydir * float size</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Here&#39;s where we plug it all together...</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the first n fibonacci numbers, reverse the list and</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// call our function on each one (passing its index along,</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// too)</span></p>
<p style="MARGIN: 0px">&#0160;</p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; fibs levels |&gt; List.rev |&gt; List.iteri addSegment</span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; tr.Commit()</span></p></div>
<p>Here’s what happens when we run the FIB command and select levels 1 to 8 (we need to call FIB eight times to do this), creating eight different Fibonacci spirals:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201156fc8e25c970c-pi"><img alt="Levels 1 to 8 of our Fibonacci spiral" border="0" height="305" src="/assets/image_624928.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Levels 1 to 8 of our Fibonacci spiral" width="469" /></a> </p>
<p>And here’s the result for level 50, as a comparison (although unless you zoom right in it might as well be level 20):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2011570be1be8970b-pi"><img alt="Level 50 Fibonacci spiral" border="0" height="305" src="/assets/image_659110.jpg" style="BORDER-RIGHT-WIDTH: 0px; DISPLAY: inline; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Level 50 Fibonacci spiral" width="469" /></a></p>
