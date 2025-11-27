---
layout: "post"
title: "Creating an AutoCAD polyline from an exploded region using .NET"
date: "2008-08-25 14:25:02"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2008/08/getting-a-polyl.html "
typepad_basename: "getting-a-polyl"
typepad_status: "Publish"
---

<p><em>Thanks to Philippe Leefsma, from our DevTech team in Europe, for providing the code used as the basis for this post. I took Philippe&#39;s code and enhanced it to support arcs and to check for disconnected segments (which in theory should never happen, but it&#39;s better to be safe than to loop infinitely :-).</em></p>
<p>When you explode a region in AutoCAD, the resultant geometry is in the form of lines and arcs. The following technique shows how to take the lines and arcs returned by the Explode() function (which doesn&#39;t perform the equivalent of the EXPLODE command in AutoCAD, remember: it just returns the exploded geometry corresponding to the objects upon which it was called, they do not get added to the database and neither is the source entity erased) and use them to construct an equivalent Polyline object.</p>
<p>It&#39;s interesting code for a number of reasons:</p>
<ul>
<li>It loops through and connects segments that may not be listed in sequence </li>
<li>It determines the bulge factor needed to make a Polyline segment geometrically equivalent to an Arc object 
<ul>
<li>This is calculated as the tangent of a quarter of the included angle</li>
</ul>
</li>
</ul>
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
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Explode Region -&gt; collection of Curves</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">DBObjectCollection</span> cvs =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">DBObjectCollection</span>();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; reg.Explode(cvs);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Create a plane to convert 3D coords</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// into Region coord system</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Plane</span> pl =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">new</span> <span style="color: #2b91af;">Plane</span>(<span style="color: blue;">new</span> <span style="color: #2b91af;">Point3d</span>(0, 0, 0), reg.Normal);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// The resulting Polyline</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Polyline</span> p = <span style="color: blue;">new</span> <span style="color: #2b91af;">Polyline</span>();</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Set common entity properties from the Region</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.SetPropertiesFrom(reg);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// For initial Curve take the first in the list</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Curve</span> cv1 = cvs[0] <span style="color: blue;">as</span> <span style="color: #2b91af;">Curve</span>;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;cv1.StartPoint.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;BulgeFromCurve(cv1, <span style="color: blue;">false</span>), 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;cv1.EndPoint.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;0, 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cvs.Remove(cv1);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// The next point to look for</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: #2b91af;">Point3d</span> nextPt = cv1.EndPoint;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Find the line that is connected to</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// the next point</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// If for some reason the lines returned were not</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// connected, we could loop endlessly.</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// So we store the previous curve count and assume</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// that if this count has not been decreased by</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// looping completely through the segments once,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// then we should not continue to loop.</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Hopefully this will never happen, as the curves</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// should form a closed loop, but anyway...</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Set the previous count as artificially high,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// so that we loop once, at least.</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">int</span> prevCnt = cvs.Count + 1;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">while</span> (cvs.Count &gt; 0 &amp;&amp; cvs.Count &lt; prevCnt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;prevCnt = cvs.Count;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: blue;">foreach</span> (<span style="color: #2b91af;">Curve</span> cv <span style="color: blue;">in</span> cvs)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;{</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// If one end of the curve connects with the</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: green;">// point we&#39;re looking for...</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: blue;">if</span> (cv.StartPoint == nextPt ||</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;cv.EndPoint == nextPt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Calculate the bulge for the curve and</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// set it on the previous vertex</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">double</span> bulge =</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;BulgeFromCurve(cv, cv.EndPoint == nextPt);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.SetBulgeAt(p.NumberOfVertices - 1, bulge);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Reverse the points, if needed</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (cv.StartPoint == nextPt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;nextPt = cv.EndPoint;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// cv.EndPoint == nextPt</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;nextPt = cv.StartPoint;</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Add out new vertex (bulge will be set next</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// time through, as needed)</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; p.AddVertexAt(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.NumberOfVertices,</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;nextPt.Convert2d(pl),</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;0, 0, 0</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// Remove our curve from the list, which</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: green;">// decrements the count, of course</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; cvs.Remove(cv);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">break</span>;</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;}</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">if</span> (cvs.Count &gt;= prevCnt)</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.Dispose();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="color: #a31515;">&quot;\nError connecting segments.&quot;</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="color: blue;">else</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Once we have added all the Polyline&#39;s vertices,</span></p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// transform it to the original region&#39;s plane</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;p.TransformBy(<span style="color: #2b91af;">Matrix3d</span>.PlaneToWorld(pl));</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Append our new Polyline to the database</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;btr.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;btr.AppendEntity(p);</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.AddNewlyCreatedDBObject(p, <span style="color: blue;">true</span>);</p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="color: green;">// Finally we erase the original region</span></p>
<br />
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;reg.UpgradeOpen();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;reg.Erase();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;&#0160; tr.Commit();</p>
<p style="font-size: 8pt; margin: 0px;">&#0160; &#0160;&#0160; &#0160;}</p>
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
<p>To really put the code through its paces, try creating a Region in arbitrary 3D space, defined by a closed Polyline containing both arc and lines segments. The RTP command should replace the selected Region with a Polyine of the same shape.</p>
<p>I&#39;ve done my best to anticipate as much as I can in the above code - my hope being that it will work on any Region - but if I&#39;ve missed a case, be sure to let me know.</p>
<p><em><strong>Update:</strong></em></p>
<p>The above code doesn&#39;t take care of more complex regions and neither does it dispose of the temporary curves properly (thanks for ali for pointing out this second issue). Rather than fix this post, I&#39;ve made the changes in an update to <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/creating-a-seri.html" target="_blank">the next post</a>, which is an evolution of this one.</p>
<p>&#0160;</p>
