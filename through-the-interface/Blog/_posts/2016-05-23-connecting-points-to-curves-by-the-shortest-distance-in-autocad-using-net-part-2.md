---
layout: "post"
title: "Connecting points to curves by the shortest distance in AutoCAD using .NET &ndash; Part 2"
date: "2016-05-23 11:29:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
original_url: "https://www.keanw.com/2016/05/connecting-points-to-curves-by-the-shortest-distance-in-autocad-using-net-part-2.html "
typepad_basename: "connecting-points-to-curves-by-the-shortest-distance-in-autocad-using-net-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2016/05/connecting-points-to-curves-by-the-shortest-distance-in-autocad-using-net-part-1.html" target="_blank">the last post</a> we saw a simple command that connects a block with a curve via a line that starts at the insertion point and meets the curve at its closest point. In this post we’re going to see how we can search the modelspace for the nearest curve and connect each block to that.</p>
<p>There are a few interesting techniques used in this post’s code:</p>
<ul>
<li>We use the dynamic keyword to count the block references for a particular block without starting a transaction (as we’re still in the user input phase, at that point).</li>
<li>We’re going to use Linq to query all the curves from the modelspace block table record.</li>
<li>For each block we’re going to use Linq again to sort the list of curves based on their distance from the insertion point.</li>
</ul>
<p>Here’s the C# code that connects all blocks of a certain type to the nearest curve. The code could easily be changed to only search for certain types or curve – or only curves tagged with a certain bit of XData – but I’ve left that as an exercise for the reader.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Linq;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> ConnectBlocksToCurves</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">double</span> DistanceFrom(<span style="color: blue;">this</span> <span style="color: #2b91af;">Curve</span> c, <span style="color: #2b91af;">Point3d</span> pt)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> pt.DistanceTo(c.GetClosestPointTo(pt, <span style="color: blue;">false</span>));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ConnectPointToCurve(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">this</span> <span style="color: #2b91af;">Transaction</span> tr, <span style="color: #2b91af;">BlockTableRecord</span> btr,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point3d</span> pt, <span style="color: #2b91af;">Curve</span> c</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; tr.DrawLine(btr, pt, c.GetClosestPointTo(pt, <span style="color: blue;">false</span>));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ConnectPointsToCurves(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">this</span> <span style="color: #2b91af;">Transaction</span> tr, <span style="color: #2b91af;">BlockTableRecord</span> btr,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Point3dCollection</span> pts, <span style="color: #2b91af;">ObjectIdCollection</span> ids</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Open all the curves and store them in an array</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> objs = <span style="color: blue;">new</span> <span style="color: #2b91af;">Curve</span>[ids.Count];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; ids.Count; i++)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objs[i] = tr.GetObject(ids[i], <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// For each point...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Point3d</span> pt <span style="color: blue;">in</span> pts)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// ... we&#39;re going to sort our curves based on their distance from</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// said point</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sorted =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; objs.OrderBy(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; c =&gt; c == <span style="color: blue;">null</span> ? <span style="color: blue;">double</span>.PositiveInfinity : c.DistanceFrom(pt)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Then we&#39;re going to connect the point to the first curve in the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// sorted list</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ConnectPointToCurve(tr, btr, pt, sorted.First&lt;<span style="color: #2b91af;">Curve</span>&gt;());</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> DrawLine(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">this</span> <span style="color: #2b91af;">Transaction</span> tr, <span style="color: #2b91af;">BlockTableRecord</span> btr, <span style="color: #2b91af;">Point3d</span> pt1, <span style="color: #2b91af;">Point3d</span> pt2</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ln = <span style="color: blue;">new</span> <span style="color: #2b91af;">Line</span>(pt1, pt2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(ln);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(ln, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;CBC&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> ConnectBlocksToCurves()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the block to connect</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect the block&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(<span style="color: #a31515;">&quot;\nMust be a block.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">BlockReference</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per = ed.GetEntity(peo);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Use a dynamic object to count the blocks, rather than creating</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// a transaction etc.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">dynamic</span> brId = per.ObjectId;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ObjectIdCollection</span> refIds =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; brId.BlockTableRecord.GetBlockReferenceIds(<span style="color: blue;">true</span>, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nFound {0} references. &quot;</span>, refIds.Count);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pko = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(<span style="color: #a31515;">&quot;Connect them all?&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pko.AllowNone = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(<span style="color: #a31515;">&quot;Yes&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Add(<span style="color: #a31515;">&quot;No&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Default = <span style="color: #a31515;">&quot;Yes&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pkr = ed.GetKeywords(pko);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pkr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Use the same code path for a single object, just reset the collection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pkr.StringResult != <span style="color: #a31515;">&quot;Yes&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; refIds.Clear();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; refIds.Add(brId);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> btr =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">SymbolUtilityServices</span>.GetBlockModelSpaceId(db),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Collect all the Curves from the modelspace</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> curveIds =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">from</span> <span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> btr</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">where</span> id.ObjectClass.IsDerivedFrom(<span style="color: #2b91af;">RXClass</span>.GetClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Curve</span>)))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">select</span> id;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Convert our enumerable to a ObjectIdCollection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ids = <span style="color: blue;">new</span> <span style="color: #2b91af;">ObjectIdCollection</span>(curveIds.ToArray&lt;<span style="color: #2b91af;">ObjectId</span>&gt;());</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Could also do this dynamically using...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// var ptEnum = from dynamic id in refIds select (Point3d)id.Position;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// pts = new Point3dCollection(ptEnum.ToArray&lt;Point3d&gt;());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pts = <span style="color: blue;">new</span> <span style="color: #2b91af;">Point3dCollection</span>();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">ObjectId</span> id <span style="color: blue;">in</span> refIds)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> br = tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pts.Add(br.Position);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Now we have our points and curves, connect them</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.UpgradeOpen();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.ConnectPointsToCurves(btr, pts, ids);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;B2C&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> Block2Curve()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the block to connect</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect the block&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(<span style="color: #a31515;">&quot;\nMust be a block.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">BlockReference</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per = ed.GetEntity(peo);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> brId = per.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the curve to connect it to</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> peo2 = <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect the curve&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo2.SetRejectMessage(<span style="color: #a31515;">&quot;\nMust be a curve.&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; peo2.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Curve</span>), <span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> per2 = ed.GetEntity(peo2);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (per2.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> cId = per2.ObjectId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Use a transaction for the geometry access and connection creation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = doc.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> br = tr.GetObject(brId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> c = tr.GetObject(cId, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (br != <span style="color: blue;">null</span> &amp;&amp; c != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We&#39;ll be writing to the modelspace</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> btr =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">SymbolUtilityServices</span>.GetBlockModelSpaceId(db), <span style="color: #2b91af;">OpenMode</span>.ForWrite</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Connect the point to the closest point on the curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.ConnectPointToCurve(btr, br.Position, c);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here’s the updated code in action:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09033d06970d-pi"><img alt="Connecting points to curves" height="488" src="/assets/image_408758.jpg" style="float: none; margin: 30px auto; display: block;" title="Connecting points to curves" width="499" /></a></p>
<p>I’m now on my way to Montreal for the Autodesk Technical Summit 2016. I’ll be presenting a session about web-based VR, which will be interesting in light of the announcements made at Google I/O last week. Another topic I expect to post about in the coming days…</p>
