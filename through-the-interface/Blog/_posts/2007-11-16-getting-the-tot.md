---
layout: "post"
title: "Getting the total volume of 3D solids in an AutoCAD model using F#"
date: "2007-11-16 17:09:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
original_url: "https://www.keanw.com/2007/11/getting-the-tot.html "
typepad_basename: "getting-the-tot"
typepad_status: "Publish"
---

<p>In one of my sessions at this year's <a href="http://www.autodesk.com/au">AU</a>, <em>&quot;There's More to .DWG Than AutoCAD®&quot;</em>, I'll be showing some VB.NET code that goes through and collects information about solids, presenting it in a dialog along with the sum of the various volumes. You can get the code and the results from <a href="http://through-the-interface.typepad.com/through_the_interface/2007/10/au-handouts-the.html">Part 1 of the session's handout</a>.</p>

<p>Just for fun, I thought I'd write some F# code to add the volumes of the 3D solid objects in the modelspace of the current drawing. I adopt a similar approach to the VB code - not caring about intersecting volumes, for instance - but obviously the code looks quite different.</p>

<p>I won't step through the code line-by-line, as <a href="http://through-the-interface.typepad.com/through_the_interface/2007/11/more-fun-with-f.html">the last post</a> introduced the fundamental concepts that also apply here.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Use lightweight F# syntax</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">(* Declare a specific namespace</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&nbsp; and module name</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">*)</span> </p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyNamespace.MyApplication</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Import managed assemblies</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#I <span style="COLOR: maroon">@&quot;C:\Program Files\Autodesk\AutoCAD 2008&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acdbmgd.dll&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">#r <span style="COLOR: maroon">&quot;acmgd.dll&quot;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> System.Collections.Generic</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.DatabaseServices</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">// Now we declare our command</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">[&lt;CommandMethod(<span style="COLOR: maroon">&quot;volume&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">let</span> listWords () =</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Let's get the usual helpful AutoCAD objects</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> db = doc.Database;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;use&quot; has the same effect as &quot;using&quot; in C#</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">use</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Get appropriately-typed BlockTable and BTRs</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> bt =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(db.BlockTableId,OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTable</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ms =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; tr.GetObject</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(bt.[BlockTableRecord.ModelSpace],</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;OpenMode.ForRead)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; :?&gt; BlockTableRecord</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// A function that accepts an ObjectId and returns</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// the volume of a 3D solid, if it happens to be one</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Note the valid use of tr, as it is in scope</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> getVolume (x : ObjectId) =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> obj = tr.GetObject(x,OpenMode.ForRead);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">match</span> obj <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | :? Solid3d <span style="COLOR: blue">-&gt;</span> (obj :?&gt; Solid3d).MassProperties.Volume</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; | _ <span style="COLOR: blue">-&gt;</span> 0.0</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// Use fold_left in a partial application to find </span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// the sum of the contents of a list</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> sum =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; List.fold_left (<span style="COLOR: blue">fun</span> x y <span style="COLOR: blue">-&gt;</span> x+y) 0.0</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// And here's where we plug everything together...</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> vol =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Seq.untyped_to_list ms |&gt; List.map getVolume |&gt; sum</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nTotal volume: &quot;</span> + vol.ToString());</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// As usual, committing is cheaper than aborting</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; tr.Commit()</p></div>

<p>The only tricky thing is the use of fold_left to apply an anonymous (or lambda, for the LISPers out there) addition function across the contents of the list containing the individual volumes of the objects in the modelspace.</p>

<p>Here's what we see when we run the &quot;volume&quot; command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">volume</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Total volume: 15275.8711619534</p></div>

<p>This is the same result as displayed by the previous example (although presented with a few more decimal places).</p>
