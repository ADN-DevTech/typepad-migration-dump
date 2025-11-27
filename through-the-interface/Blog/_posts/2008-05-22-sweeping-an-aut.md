---
layout: "post"
title: "Sweeping an AutoCAD surface using .NET"
date: "2008-05-22 07:30:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Selection"
  - "Solid modeling"
original_url: "https://www.keanw.com/2008/05/sweeping-an-aut.html "
typepad_basename: "sweeping-an-aut"
typepad_status: "Publish"
---

<p>AutoCAD 2007 introduced more advanced solid &amp; surface modeling tools. This post takes a look at how to generate one particular type of surface: a SweptSurface, which is created by sweeping a profile (which could be a region, a planar surface or a curve) through a particular path (which must be a curve).</p>
<p>The below C# code shows how to sweep an object along a curved path to create a surface. Our SAP (for SweepAlongPath) command doesn&#39;t provide all the options of the standard SWEEP command, as the point is to show how to do this programmatically, not to duplicate standard AutoCAD functionality.</p>
<p>We&#39;re creating a SweptSurface in our code: it&#39;s also possible to sweep a similar entity along a path to create a Solid3d, but at the time of writing this is only exposed through ObjectARX (AcDb3dSolid::createSweptSolid()). If you have a strong need to create swept solids in AutoCAD using .NET, please send me an email.</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Geometry;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="COLOR: blue">namespace</span> SolidCreation</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: #2b91af">Commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; [<span style="COLOR: #2b91af">CommandMethod</span>(<span style="COLOR: #a31515">&quot;SAP&quot;</span>)]</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> SweepAlongPath()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Document</span> doc =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Database</span> db = doc.Database;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Editor</span> ed = doc.Editor;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Ask the user to select a region to extrude</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptEntityOptions</span> peo1 =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptEntityOptions</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #a31515">&quot;\nSelect profile or curve to sweep: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo1.SetRejectMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #a31515">&quot;\nEntity must be a region, curve or planar surface.&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo1.AddAllowedClass(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Region</span>), <span style="COLOR: blue">false</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo1.AddAllowedClass(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Curve</span>), <span style="COLOR: blue">false</span>);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo1.AddAllowedClass(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">PlaneSurface</span>), <span style="COLOR: blue">false</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptEntityResult</span> per =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; ed.GetEntity(peo1);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (per.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">ObjectId</span> regId = per.ObjectId;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Ask the user to select an extrusion path</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">PromptEntityOptions</span> peo2 =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">PromptEntityOptions</span>(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #a31515">&quot;\nSelect path along which to sweep: &quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo2.SetRejectMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #a31515">&quot;\nEntity must be a curve.&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;peo2.AddAllowedClass(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">typeof</span>(<span style="COLOR: #2b91af">Curve</span>), <span style="COLOR: blue">false</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;per = ed.GetEntity(peo2);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">if</span> (per.Status != <span style="COLOR: #2b91af">PromptStatus</span>.OK)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">return</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">ObjectId</span> splId = per.ObjectId;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: green">// Now let&#39;s create our swept surface</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">Transaction</span> tr =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; db.TransactionManager.StartTransaction();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">using</span> (tr)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;{</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Entity</span> sweepEnt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.GetObject(regId, <span style="COLOR: #2b91af">OpenMode</span>.ForRead) <span style="COLOR: blue">as</span> <span style="COLOR: #2b91af">Entity</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">Curve</span> pathEnt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;tr.GetObject(splId, <span style="COLOR: #2b91af">OpenMode</span>.ForRead) <span style="COLOR: blue">as</span> <span style="COLOR: #2b91af">Curve</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: blue">if</span> (sweepEnt == <span style="COLOR: blue">null</span> || pathEnt == <span style="COLOR: blue">null</span>)</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; {</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;ed.WriteMessage(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #a31515">&quot;\nProblem opening the selected entities.&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">return</span>;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; }</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// We use a builder object to create</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// our SweepOptions</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SweepOptionsBuilder</span> sob =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">SweepOptionsBuilder</span>();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Align the entity to sweep to the path</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sob.Align =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: #2b91af">SweepOptionsAlignOption</span>.AlignSweepEntityToPath;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// The base point is the start of the path</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sob.BasePoint = pathEnt.StartPoint;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// The profile will rotate to follow the path</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; sob.Bank = <span style="COLOR: blue">true</span>;</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// Now generate the surface...</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">SweptSurface</span> ss =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;<span style="COLOR: blue">new</span> <span style="COLOR: #2b91af">SweptSurface</span>();</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ss.CreateSweptSurface(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;sweepEnt,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;pathEnt,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;sob.ToSweepOptions()</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; );</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: green">// ... and add it to the modelspace</span></p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">BlockTable</span> bt =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">BlockTable</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; db.BlockTableId,</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForRead</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; <span style="COLOR: #2b91af">BlockTableRecord</span> ms =</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;(<span style="COLOR: #2b91af">BlockTableRecord</span>)tr.GetObject(</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; bt[<span style="COLOR: #2b91af">BlockTableRecord</span>.ModelSpace],</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: #2b91af">OpenMode</span>.ForWrite</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;&#0160; &#0160;);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; ms.AppendEntity(ss);</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.AddNewlyCreatedDBObject(ss, <span style="COLOR: blue">true</span>);</p><br />
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; &#0160; tr.Commit();</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; <span style="COLOR: blue">catch</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;&#0160; { }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160;&#0160; &#0160;}</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; &#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160; }</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">}</p></div>
<p>Here&#39;s an image of a very simple drawing I used to demonstrate the function. I started by creating a helix, and then copied it, essentially creating my two paths. I then drew a tiny circle at the end point of the first helix (not aligned to the path in any way - just flat in the World UCS - as the SweepOptions we choose will ask that the profile to be aligned automatically to the path), and drew a simple &quot;S&quot;-shaped spline at the end point of the second helix (I drew the spline elsewhere and moved it using the mid-point as a base point, selecting the end of the helix as the destination).</p>
<p>So we end up with two paths (both helixes) and their respective non-aligned profiles (one a circle, the other a spline):</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Helix%20paths%20and%20profiles.png"><img alt="Helix paths and profiles" border="0" height="118" src="/assets/Helix%20paths%20and%20profiles_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>I then ran the SAP command twice, selecting one of the profiles and its respective path each time. Here&#39;s the 2D wireframe view of what was created:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Swept%20surfaces%20-%20plan.png"><img alt="Swept surfaces - plan" border="0" height="118" src="/assets/Swept%20surfaces%20-%20plan_thumb.png" style="BORDER-RIGHT-WIDTH: 0px; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" width="240" /></a> </p>
<p>To see the geometry better, I then changed to use the conceptual visual style and orbitted around to get a better 3D view:</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Swept%20surfaces%20-%203D%20conceptual.png"><img alt="Swept surfaces - 3D conceptual" border="0" height="114" src="/assets/Swept%20surfaces%20-%203D%20conceptual_thumb.png" width="240" /></a></p>
<p><span style="font-weight: bold"><span style="FONT-STYLE: italic">Update</span></span></p>
<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/02/creating-an-editable-autocad-solid-using-net.html">this post</a>, Solid3d.CreateSweptSolid has now been implemented in AutoCAD 2010.</p>
<p><strong><em>Update 2</em></strong></p>
<p>And you can see the how to use Solid3d.CreateSweptSolid in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/01/sweeping-an-autocad-solid-using-net.html">this post</a>.</p>
