---
layout: "post"
title: "Testing whether an AutoCAD drawing is 2D or 3D using .NET"
date: "2008-07-09 15:58:28"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Solid modeling"
original_url: "https://www.keanw.com/2008/07/testing-whether.html "
typepad_basename: "testing-whether"
typepad_status: "Publish"
---

<p>This post demonstrates a simple check for whether a drawing is two or three dimensional. The code is almost embarrassingly simple, but then the question is significant and in the absence of a &quot;Is3D&quot; property on the Database object this is likely to prove useful for people.</p>

<p>So how do we check whether a drawing is 3D? The quick answer is that in most circumstances the EXTMAX system variable will have a non-zero Z value for a 3D drawing. There are potential situations where this might not be true (and EXTMAX doesn't reflect the 3D nature of certain geometry), but given the likelihood that any real-world 3D model includes a variety of geometry, it's pretty safe to rely upon. The alternative is to iterate through and test geometry, but checking EXTMAX is quicker, by far, and the alternative should only by needed if you find a particular scenario that EXTMAX doesn't address.</p>

<p>Here's some C# code that tells us whether a drawing is 2D or 3D:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> NumberOfDimensions</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="color: #2b91af;">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;IS3D&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> CheckWhether3D()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Database</span> db = doc.Database;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="color: #2b91af;">Editor</span> ed = doc.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="color: #a31515;">&quot;\nDrawing is {0}.&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (IsDrawing3D(db) ? <span style="color: #a31515;">&quot;3D&quot;</span> : <span style="color: #a31515;">&quot;2D&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">private</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">bool</span> IsDrawing3D(<span style="color: #2b91af;">Database</span> db)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">return</span> (db.Extmax.Z &gt; <span style="color: #2b91af;">Tolerance</span>.Global.EqualPoint);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Here's what happens when we call the IS3D command on a fresh drawing, after we've drawn a 2D line and then after we've drawn a tiny sphere:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">IS3D</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Drawing is 2D.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">LINE</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify first point: <span style="color: #ff0000;">0,0,0</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Undo]: <span style="color: #ff0000;">@10,10</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify next point or [Undo]:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">IS3D</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Drawing is 2D.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">SPHERE</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify center point or [3P/2P/Ttr]: <span style="color: #ff0000;">0,0,0</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Specify radius or [Diameter]: <span style="color: #ff0000;">0.0001</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">IS3D</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Drawing is 3D.</p></div>
