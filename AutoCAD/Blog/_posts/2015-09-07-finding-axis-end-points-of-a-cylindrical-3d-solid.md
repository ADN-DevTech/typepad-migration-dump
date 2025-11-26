---
layout: "post"
title: "Finding Axis end points of a cylindrical 3d solid"
date: "2015-09-07 05:37:29"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/09/finding-axis-end-points-of-a-cylindrical-3d-solid.html "
typepad_basename: "finding-axis-end-points-of-a-cylindrical-3d-solid"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In a recent query from a developer, the axis end points of a cylindrical solid were needed to be retrieved. For this, it is required to get the circular edges that make the cylinder and find its center points. I remembered Gilles Chanteau's elegant code snippet in a discussion forum post that made use of Linq query with BRep API and went in search of it and found <a href="http://forums.autodesk.com/t5/net/identify-solid3d-object-as-box-or-cylinder/td-p/4816641">this</a>. It turned out that the code could be made to retrieve the axis end points of a cylindrical solid with minimal change.</p>
<p>So here is the modified code snippet and thanks to Gilles.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;FindCylinderAxis&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  FindCylinderAxis()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;">     = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     PromptEntityOptions peo </pre>
<pre style="margin:0em;">     = <span style="color:#0000ff">new</span><span style="color:#000000">  PromptEntityOptions(<span style="color:#a31515">&quot;Pick a cylinder : &quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     peo.SetRejectMessage</pre>
<pre style="margin:0em;">     (<span style="color:#a31515">&quot;\\nA 3d solid of cylindrical shape must be selected.&quot;</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     peo.AddAllowedClass(</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">typeof</span><span style="color:#000000"> (Autodesk.AutoCAD.DatabaseServices.Solid3d), <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     PromptEntityResult per = ed.GetEntity(peo);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">if</span><span style="color:#000000">  (per.Status != PromptStatus.OK)</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">return</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">     = doc.Database.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Solid3d sld = tr.GetObject(</pre>
<pre style="margin:0em;">         per.ObjectId, OpenMode.ForRead, <span style="color:#0000ff">false</span><span style="color:#000000"> ) <span style="color:#0000ff">as</span><span style="color:#000000">  Solid3d;</pre>
<pre style="margin:0em;">         Point3d axisPt1 = Point3d.Origin;</pre>
<pre style="margin:0em;">         Point3d axisPt2 = Point3d.Origin;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (GetCylinderAxis(sld, <span style="color:#0000ff">ref</span><span style="color:#000000">  axisPt1, <span style="color:#0000ff">ref</span><span style="color:#000000">  axisPt2))</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             ed.WriteMessage(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>Axis points : <span style="color:#000000">{</span>1<span style="color:#000000">}</span> <span style="color:#000000">{</span>2<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">             Environment.NewLine, </pre>
<pre style="margin:0em;">             axisPt1.ToString(), axisPt2.ToString()));</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             ed.WriteMessage(String.Format(<span style="color:#a31515">&quot;<span style="color:#000000">{</span>0<span style="color:#000000">}</span>Not a cylinder.&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">             Environment.NewLine));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">bool</span><span style="color:#000000">  GetCylinderAxis(</pre>
<pre style="margin:0em;">     Solid3d solid, <span style="color:#0000ff">ref</span><span style="color:#000000">  Point3d axisPt1, <span style="color:#0000ff">ref</span><span style="color:#000000">  Point3d axisPt2)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">bool</span><span style="color:#000000">  isCylinder = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">     axisPt1 = Point3d.Origin;</pre>
<pre style="margin:0em;">     axisPt2 = Point3d.Origin;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Brep brep = <span style="color:#0000ff">new</span><span style="color:#000000">  Brep(solid))</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (brep.Complexes.Count() != 1)</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (brep.Faces.Count() != 3)</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         BrepEdgeCollection edges = brep.Edges;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (edges.Count() != 2)</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">return</span><span style="color:#000000">  <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         CircularArc3d[] circles = brep.Edges</pre>
<pre style="margin:0em;">             .Select(edge =&gt; </pre>
<pre style="margin:0em;">             ((ExternalCurve3d)edge.Curve).NativeCurve </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">as</span><span style="color:#000000">  CircularArc3d)</pre>
<pre style="margin:0em;">             .Where(circle =&gt; circle != <span style="color:#0000ff">null</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">             .ToArray();</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (circles.Length != 2)</pre>
<pre style="margin:0em;">             isCylinder = <span style="color:#0000ff">false</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             isCylinder = </pre>
<pre style="margin:0em;">             (circles[0].Radius == circles[1].Radius &amp;&amp;</pre>
<pre style="margin:0em;">             circles[0].Normal.IsParallelTo(circles[1].Normal));</pre>
<pre style="margin:0em;">             axisPt1 = circles[0].Center;</pre>
<pre style="margin:0em;">             axisPt2 = circles[1].Center;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  isCylinder;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
