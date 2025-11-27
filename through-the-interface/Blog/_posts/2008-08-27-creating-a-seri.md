---
layout: "post"
title: "Creating a series of AutoCAD polylines by exploding a complex region using .NET"
date: "2008-08-27 07:03:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2008/08/creating-a-seri.html "
typepad_basename: "creating-a-seri"
typepad_status: "Publish"
---

<p>This is a follow-up to <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/getting-a-polyl.html">the last post</a>, where we looked at some C# code to generate a Polyline from a Region. Fernando very kindly pointed out that complex Regions would not be handled properly, so I went away and enhanced the RTP command to create separate Polylines for each of the loops (or nested Regions) contained in a Region we&#39;re trying to explode.</p>
<p>The significant changes were:</p>
<ul>
<li>To return a collection of objects, rather than a single Polyline </li>
<li>To recurse when we find a Region in the results of the Explode() call</li>
</ul>
<p>Otherwise the code is reasonably similar, although I suspect it&#39;s now somewhat cleaner from having been worked with more closely.</p>
<p>Here&#39;s the C# code:</p>
<div style="font-size: 8pt; background: white; color: black; font-family: courier new;">
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Geometry;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">using</span> System;</p>
<br />
<p style="font-size: 8pt; margin: 0px;"><span style="color: blue;">namespace</span> RegionConversion</p>
<p style="font-size: 8pt; margin: 0px;">{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;RTP&quot;</span>)]</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> RegionToPolyline()</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Document</span> doc =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">PromptEntityOptions</span> peo =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptEntityOptions</span>(<span style="color: #a31515;">&quot;\nSelect a region:&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;peo.SetRejectMessage(<span style="color: #a31515;">&quot;\nMust be a region.&quot;</span>);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;peo.AddAllowedClass(<span style="color: blue;">typeof</span>(<span style="color: #2b91af;">Region</span>), <span style="color: blue;">true</span>);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">PromptEntityResult</span> per =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetEntity(peo);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (per.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">return</span>;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Transaction</span> tr =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; doc.TransactionManager.StartTransaction();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span> (tr)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">BlockTable</span> bt =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="color: #2b91af;">BlockTable</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;db.BlockTableId,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">BlockTableRecord</span> btr =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;bt[<span style="color: #2b91af;">BlockTableRecord</span>.ModelSpace],</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Region</span> reg =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.GetObject(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;per.ObjectId,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">Region</span>;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (reg != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">DBObjectCollection</span> objs =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;PolylineFromRegion(reg);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Append our new entities to the database</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; btr.UpgradeOpen();</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Entity</span> ent <span style="color: blue;">in</span> objs)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;btr.AppendEntity(ent);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(ent, <span style="color: blue;">true</span>);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Finally we erase the original region</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; reg.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; reg.Erase();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">DBObjectCollection</span> PolylineFromRegion(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Region</span> reg</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// We will return a collection of entities</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// (should include closed Polylines and other</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// closed curves, such as Circles)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObjectCollection</span> res =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">DBObjectCollection</span>();</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Explode Region -&gt; collection of Curves / Regions</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">DBObjectCollection</span> cvs =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">DBObjectCollection</span>();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;reg.Explode(cvs);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Create a plane to convert 3D coords</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// into Region coord system</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Plane</span> pl =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">Plane</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point3d</span>(0, 0, 0), reg.Normal);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">using</span>(pl)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">bool</span> finished = <span style="color: blue;">false</span>;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">while</span> (!finished &amp;&amp; cvs.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Count the Curves and the non-Curves, and find</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// the index of the first Curve in the collection</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">int</span> cvCnt = 0, nonCvCnt = 0, fstCvIdx = -1;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; cvs.Count; i++)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Curve</span> tmpCv = cvs[i] <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (tmpCv == <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; nonCvCnt++;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Closed curves can go straight into the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// results collection, and aren&#39;t added</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// to the Curve count</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (tmpCv.Closed)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; res.Add(tmpCv);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cvs.Remove(tmpCv);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Decrement, so we don&#39;t miss an item</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; i--;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cvCnt++;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (fstCvIdx == -1)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;fstCvIdx = i;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (fstCvIdx &gt;= 0)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// For the initial segment take the first</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Curve in the collection</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Curve</span> fstCv = (<span style="color: #2b91af;">Curve</span>)cvs[fstCvIdx];</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// The resulting Polyline</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Polyline</span> p = <span style="color: blue;">new</span> <span style="color: #2b91af;">Polyline</span>();</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Set common entity properties from the Region</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.SetPropertiesFrom(reg);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Add the first two vertices, but only set the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// bulge on the first (the second will be set</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// retroactively from the second segment)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// We also assume the first segment is counter-</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// clockwise (the default for arcs), as we&#39;re</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// not swapping the order of the vertices to</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// make them fit the Polyline&#39;s order</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; fstCv.StartPoint.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; BulgeFromCurve(fstCv, <span style="color: blue;">false</span>), 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; fstCv.EndPoint.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; 0, 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;cvs.Remove(fstCv);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// The next point to look for</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Point3d</span> nextPt = fstCv.EndPoint;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Find the line that is connected to</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// the next point</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// If for some reason the lines returned were not</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// connected, we could loop endlessly.</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// So we store the previous curve count and assume</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// that if this count has not been decreased by</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// looping completely through the segments once,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// then we should not continue to loop.</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Hopefully this will never happen, as the curves</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// should form a closed loop, but anyway...</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Set the previous count as artificially high,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// so that we loop once, at least.</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">int</span> prevCnt = cvs.Count + 1;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">while</span> (cvs.Count &gt; nonCvCnt &amp;&amp; cvs.Count &lt; prevCnt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; prevCnt = cvs.Count;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">DBObject</span> obj <span style="color: blue;">in</span> cvs)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Curve</span> cv = obj <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (cv != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// If one end of the curve connects with the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// point we&#39;re looking for...</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (cv.StartPoint == nextPt ||</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cv.EndPoint == nextPt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Calculate the bulge for the curve and</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// set it on the previous vertex</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">double</span> bulge =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; BulgeFromCurve(cv, cv.EndPoint == nextPt);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (bulge != 0.0)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.SetBulgeAt(p.NumberOfVertices-1, bulge);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Reverse the points, if needed</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (cv.StartPoint == nextPt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; nextPt = cv.EndPoint;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// cv.EndPoint == nextPt</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; nextPt = cv.StartPoint;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Add out new vertex (bulge will be set next</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// time through, as needed)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; nextPt.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; 0, 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; );</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Remove our curve from the list, which</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// decrements the count, of course</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; cvs.Remove(cv);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">break</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Once we have added all the Polyline&#39;s vertices,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// transform it to the original region&#39;s plane</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.TransformBy(<span style="color: #2b91af;">Matrix3d</span>.PlaneToWorld(pl));</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;res.Add(p);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (cvs.Count == nonCvCnt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; finished = <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// If there are any Regions in the collection,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// recurse to explode and add their geometry</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (nonCvCnt &gt; 0 &amp;&amp; cvs.Count &gt; 0)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">foreach</span> (<span style="color: #2b91af;">DBObject</span> obj <span style="color: blue;">in</span> cvs)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #2b91af;">Region</span> subReg = obj <span style="color: blue;">as</span> <span style="color: #2b91af;">Region</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (subReg != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">DBObjectCollection</span> subRes =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;PolylineFromRegion(subReg);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">DBObject</span> o <span style="color: blue;">in</span> subRes)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;res.Add(o);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cvs.Remove(subReg);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (cvs.Count == 0)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;finished = <span style="color: blue;">true</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> res;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: green;">// Helper function to calculate the bulge for arcs</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">double</span> BulgeFromCurve(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Curve</span> cv,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">bool</span> clockwise</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; )</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">double</span> bulge = 0.0;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: #2b91af;">Arc</span> a = cv <span style="color: blue;">as</span> <span style="color: #2b91af;">Arc</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">if</span> (a != <span style="color: blue;">null</span>)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">double</span> newStart;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// The start angle is usually greater than the end,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// as arcs are all counter-clockwise.</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// (If it isn&#39;t it&#39;s because the arc crosses the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// 0-degree line, and we can subtract 2PI from the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// start angle.)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (a.StartAngle &gt; a.EndAngle)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; newStart = a.StartAngle - 8 * <span style="color: #2b91af;">Math</span>.Atan(1);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; newStart = a.StartAngle;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// Bulge is defined as the tan of</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// one fourth of the included angle</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; bulge = <span style="color: #2b91af;">Math</span>.Tan((a.EndAngle - newStart) / 4);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// If the curve is clockwise, we negate the bulge</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (clockwise)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; bulge = -bulge;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">return</span> bulge;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">}</p>
</div>
<p>Each of the complex regions I tested - and I created quite a few, starting with the REGION command to create simple Regions from Circles and closed Polylines which I then UNIONed and SUBTRACTed to create complex Regions with holes and islands - was handled by this code. The one case I didn&#39;t explicitly code for was around coincident geometry: I didn&#39;t spend time working out a way to determine which of multiple segments with vertexes at the same location should be selected while constructing a particular loop. I can imagine it would take some logic to back-track (effectively creating a decision tree), but that seemed beyond the scope of this sample.</p>
<p>If anyone comes across further cases that they feel should be handled by the above code (but currently are not), then please do let me know.</p>
<p><em><strong>Update:</strong></em></p>
<p>Here&#39;s an updated version of the code in this post that correctly disposes of temporary curves, avoiding the problem highlighted in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/getting-a-polyl.html#comment-6a00d83452464869e2019103cf10ee970c" target="_blank">a comment</a> on <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/getting-a-polyl.html" target="_blank">the last post</a>. Thanks, ali!</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> RegionConversion</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;RTP&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> RegionToPolyline()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> peo =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a region:&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; peo.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nMust be a region.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; peo.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; ed.GetEntity(peo);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="color: #2b91af; line-height: 140%;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.BlockTableId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; (</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bt[</span><span style="color: #2b91af; line-height: 140%;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace],</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;"> reg =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; per.ObjectId,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (reg != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> objs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; PolylineFromRegion(reg);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Append our new entities to the database</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; btr.UpgradeOpen();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> objs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; btr.AppendEntity(ent);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(ent, </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Finally we erase the original region</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; reg.UpgradeOpen();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; reg.Erase();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> PolylineFromRegion(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;"> reg</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We will return a collection of entities</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// (should include closed Polylines and other</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// closed curves, such as Circles)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> res =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Explode Region -&gt; collection of Curves / Regions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> cvs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; reg.Explode(cvs);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create a plane to convert 3D coords</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// into Region coord system</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> pl =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">(0, 0, 0), reg.Normal);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;">(pl)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> finished = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (!finished &amp;&amp; cvs.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Count the Curves and the non-Curves, and find</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the index of the first Curve in the collection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> cvCnt = 0, nonCvCnt = 0, fstCvIdx = -1;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; cvs.Count; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> tmpCv = cvs[i] </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tmpCv == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; nonCvCnt++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Closed curves can go straight into the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// results collection, and aren&#39;t added</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// to the Curve count</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tmpCv.Closed)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; res.Add(tmpCv);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cvs.Remove(tmpCv);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Decrement, so we don&#39;t miss an item</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; i--;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cvCnt++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (fstCvIdx == -1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fstCvIdx = i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (fstCvIdx &gt;= 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// For the initial segment take the first</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Curve in the collection</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> fstCv = (</span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">)cvs[fstCvIdx];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The resulting Polyline</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;"> p = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Polyline</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Set common entity properties from the Region</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.SetPropertiesFrom(reg);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Add the first two vertices, but only set the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// bulge on the first (the second will be set</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// retroactively from the second segment)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We also assume the first segment is counter-</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// clockwise (the default for arcs), as we&#39;re</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// not swapping the order of the vertices to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// make them fit the Polyline&#39;s order</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.NumberOfVertices,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fstCv.StartPoint.Convert2d(pl),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; BulgeFromCurve(fstCv, </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">), 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.NumberOfVertices,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fstCv.EndPoint.Convert2d(pl),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cvs.Remove(fstCv);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The next point to look for</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> nextPt = fstCv.EndPoint;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// We no longer need the curve</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fstCv.Dispose();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Find the line that is connected to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// the next point</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If for some reason the lines returned were not</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// connected, we could loop endlessly.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// So we store the previous curve count and assume</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// that if this count has not been decreased by</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// looping completely through the segments once,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// then we should not continue to loop.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Hopefully this will never happen, as the curves</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// should form a closed loop, but anyway...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Set the previous count as artificially high,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// so that we loop once, at least.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> prevCnt = cvs.Count + 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">while</span><span style="line-height: 140%;"> (cvs.Count &gt; nonCvCnt &amp;&amp; cvs.Count &lt; prevCnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; prevCnt = cvs.Count;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> obj </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> cvs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> cv = obj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cv != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If one end of the curve connects with the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// point we&#39;re looking for...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cv.StartPoint == nextPt ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cv.EndPoint == nextPt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Calculate the bulge for the curve and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// set it on the previous vertex</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> bulge =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; BulgeFromCurve(cv, cv.EndPoint == nextPt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (bulge != 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.SetBulgeAt(p.NumberOfVertices-1, bulge);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Reverse the points, if needed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cv.StartPoint == nextPt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; nextPt = cv.EndPoint;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// cv.EndPoint == nextPt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; nextPt = cv.StartPoint;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Add out new vertex (bulge will be set next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// time through, as needed)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.AddVertexAt(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.NumberOfVertices,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; nextPt.Convert2d(pl),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Remove our curve from the list, which</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// decrements the count, of course</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cvs.Remove(cv);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cv.Dispose();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Once we have added all the Polyline&#39;s vertices,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// transform it to the original region&#39;s plane</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; p.TransformBy(</span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.PlaneToWorld(pl));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; res.Add(p);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cvs.Count == nonCvCnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; finished = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If there are any Regions in the collection,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// recurse to explode and add their geometry</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (nonCvCnt &gt; 0 &amp;&amp; cvs.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> obj </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> cvs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;"> subReg = obj </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Region</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (subReg != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">DBObjectCollection</span><span style="line-height: 140%;"> subRes =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; PolylineFromRegion(subReg);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">DBObject</span><span style="line-height: 140%;"> o </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> subRes)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; res.Add(o);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cvs.Remove(subReg);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; subReg.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (cvs.Count == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; finished = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> res;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Helper function to calculate the bulge for arcs</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">private</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> BulgeFromCurve(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Curve</span><span style="line-height: 140%;"> cv,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> clockwise</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> bulge = 0.0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Arc</span><span style="line-height: 140%;"> a = cv </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Arc</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (a != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> newStart;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// The start angle is usually greater than the end,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// as arcs are all counter-clockwise.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// (If it isn&#39;t it&#39;s because the arc crosses the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// 0-degree line, and we can subtract 2PI from the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// start angle.)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (a.StartAngle &gt; a.EndAngle)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; newStart = a.StartAngle - 8 * </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Atan(1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; newStart = a.StartAngle;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Bulge is defined as the tan of</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// one fourth of the included angle</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; bulge = </span><span style="color: #2b91af; line-height: 140%;">Math</span><span style="line-height: 140%;">.Tan((a.EndAngle - newStart) / 4);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// If the curve is clockwise, we negate the bulge</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (clockwise)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; &#0160; bulge = -bulge;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> bulge;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
