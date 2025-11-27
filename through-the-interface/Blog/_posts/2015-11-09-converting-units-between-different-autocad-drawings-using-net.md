---
layout: "post"
title: "Converting units between different AutoCAD drawings using .NET"
date: "2015-11-09 08:49:31"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Drawing structure"
original_url: "https://www.keanw.com/2015/11/converting-units-between-different-autocad-drawings-using-net.html "
typepad_basename: "converting-units-between-different-autocad-drawings-using-net"
typepad_status: "Publish"
---

<p>This is actually a redo of <a href="http://through-the-interface.typepad.com/through_the_interface/2015/11/attaching-autocad-xrefs-and-inserting-them-at-the-origin-using-net.html" target="_blank">last week’s post</a>, just with a different title: while the approach shown worked well when creating external references to drawings using the same units, when bringing in (for instance) metric xrefs into an imperial master drawing the scale was all messed up. Thanks to Hans Lammerts for reporting the issue.</p>
<p>The scaling ended up being straightforward to implement: the hard work was done by UnitsConverter.GetConversionFactor(), which established the scale factor to use, converting between the Units property of the new block table record and the Insunits property of the target database. Then it was a simple matter of scaling the block reference by this amount.</p>
<p>So yes, last week’s simple solution proved to be overly so, in the end. Here’s a version of the code with a small amount of complexity added, but for a very good reason.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> XrefAttachAtZero</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Attaches the specified Xref to the current space in the current drawing.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;param name=&quot;path&quot;&gt;</span><span style="color: green;">Path to the drawing file to attach as an Xref.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;param name=&quot;pos&quot;&gt;</span><span style="color: green;">Position of Xref in WCS coordinates.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;param name=&quot;name&quot;&gt;</span><span style="color: green;">Optional name for the Xref.</span><span style="color: gray;">&lt;/param&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span> <span style="color: gray;">&lt;returns&gt;</span><span style="color: green;">Whether the attach operation succeeded.</span><span style="color: gray;">&lt;/returns&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">bool</span> XrefAttachAndInsert(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">this</span> <span style="color: #2b91af;">Database</span> db, <span style="color: blue;">string</span> path, <span style="color: #2b91af;">Point3d</span> pos, <span style="color: blue;">string</span> name = <span style="color: blue;">null</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ret = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!<span style="color: #2b91af;">File</span>.Exists(path))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> ret;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #2b91af;">String</span>.IsNullOrEmpty(name))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; name = <span style="color: #2b91af;">Path</span>.GetFileNameWithoutExtension(path);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">try</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = db.TransactionManager.StartOpenCloseTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Attach the Xref - add it to the database&#39;s block table</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> xId = db.AttachXref(path, name);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (xId.IsValid)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Open the newly created block, so we can get its units</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> xbtr = (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(xId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Determine the unit conversion between the xref and the target</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sf = <span style="color: #2b91af;">UnitsConverter</span>.GetConversionFactor(xbtr.Units, db.Insunits);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create the block reference and scale it accordingly</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> br = <span style="color: blue;">new</span> <span style="color: #2b91af;">BlockReference</span>(pos, xId);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br.ScaleFactors = <span style="color: blue;">new</span> <span style="color: #2b91af;">Scale3d</span>(sf);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add the block reference to the current space and the transaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> btr =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.CurrentSpaceId, <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(br);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(br, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ret = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">catch</span> (Autodesk.AutoCAD.Runtime.<span style="color: #2b91af;">Exception</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; { }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> ret;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;XAO&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> XrefAttachAtOrigin()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Ask the user to specify a file to attach</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> opts = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptOpenFileOptions</span>(<span style="color: #a31515;">&quot;Select Reference File&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; opts.Filter = <span style="color: #a31515;">&quot;Drawing (*.dwg)|*.dwg&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pr = ed.GetFileNameForOpen(opts);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pr.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Attach the specified file and insert it at the origin</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> res = db.XrefAttachAndInsert(pr.StringResult, <span style="color: #2b91af;">Point3d</span>.Origin);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;External reference {0}attached at the origin.&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res ? <span style="color: #a31515;">&quot;&quot;</span> : <span style="color: #a31515;">&quot;not &quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here’s the XAO command in action again.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb088c2d02970d-pi"><img alt="Unit Conversion" height="516" src="/assets/image_804915.jpg" style="float: none; margin: 50px auto; display: block;" title="Unit Conversion" width="508" /></a></p>
<p>This time I created two drawings: the one on the left uses the imperial <em>acad.dwt</em> template, the one on the right uses the metric <em>acadiso.dwt</em>. I created a CIRCLE with a radius of 5 at 10,10,0 in each drawing. I then saved the right-hand (metric) drawing to disk and the video shows using the XAO command to attach it into the left-hand drawing. You can see that the drawing is attached at the appropriate scale, making the content 25.4 times smaller.</p>
