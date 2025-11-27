---
layout: "post"
title: "Getting the type of an AutoCAD solid using .NET"
date: "2007-05-29 19:38:43"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Object properties"
original_url: "https://www.keanw.com/2007/05/getting_the_typ.html "
typepad_basename: "getting_the_typ"
typepad_status: "Publish"
---

<p>Now this may seem like a very trivial post, but this was actually a significant problem for my team when preparing the material for the <a href="http://through-the-interface.typepad.com/through_the_interface/2007/03/recorded_presen.html">Autodesk Component Technologies presentation</a> we delivered at last year's DevDays tour.</p>

<p>Basically the &quot;type&quot; (or really &quot;sub-type&quot;) of a Solid3d (an AcDb3DSolid in ObjectARX) is not exposed through ObjectARX and therefore neither is it exposed through the managed interface. It is possible to get at the information from C++ using the Brep API, but this is currently not available from .NET (and yes, we are aware that many of you would like to see this point addressed, although I can't commit to when it will happen).</p>

<p>But there is some good news: the property is exposed through COM, so we can simply get a COM interface for our solid and query the data through that. Thanks again for Fenton Webb, a member of DevTech Americas, for the tip of using COM rather than worrying about the Brep API.</p>

<p>Here's some C# code demonstrating the technique:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Interop.Common;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> SolidTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Cmds</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;GST&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> GetSolidType()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Document</span> doc =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = doc.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Database</span> db = doc.Database;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptEntityResult</span> per =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetEntity(<span style="COLOR: maroon">&quot;\nSelect a 3D solid: &quot;</span>);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (per.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Transaction</span> tr =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; db.TransactionManager.StartTransaction();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">using</span> (tr)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">DBObject</span> obj =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;tr.GetObject(per.ObjectId, <span style="COLOR: teal">OpenMode</span>.ForRead);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: teal">Solid3d</span> sol = obj <span style="COLOR: blue">as</span> <span style="COLOR: teal">Solid3d</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (sol != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Acad3DSolid</span> oSol =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">Acad3DSolid</span>)sol.AcadObject;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: maroon"><span style="color: #000000;">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span>&quot;\nSolid is of type \&quot;{0}\&quot;&quot;</span>,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; oSol.SolidType</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.Commit();</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>And here's what we see when we run the GST command, selecting a number of Solid3d objects, one by one:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Box&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:&nbsp; <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Sphere&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:&nbsp; <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Wedge&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:&nbsp; <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Cylinder&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:&nbsp; <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Cone&quot;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:&nbsp; <span style="COLOR: red">GST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Select a 3D solid:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Solid is of type &quot;Torus&quot;</p></div>
