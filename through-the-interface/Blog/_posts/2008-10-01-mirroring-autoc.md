---
layout: "post"
title: "Mirroring AutoCAD entities using .NET"
date: "2008-10-01 10:53:31"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
original_url: "https://www.keanw.com/2008/10/mirroring-autoc.html "
typepad_basename: "mirroring-autoc"
typepad_status: "Publish"
---

<p><em>The code used as the basis for this post was from a recent response sent out by Philippe Leefsma, from our European DevTech team. Thanks, Philippe!</em></p>

<p>It's very common to want to manipulate an entity programmatically in AutoCAD, and often the best way to do so is to &quot;transform&quot; it. The technique is very straightforward: you create a transformation matrix using the static members of the Matrix3d class (Displacement(), Rotation(), Scaling(), Mirroring(), or the possibly less commonly needed AlignCoordinateSystem(), Projection(), PlaneToWorld() and WorldToPlane()), you make sure your entity is open for write, and then simply pass the matrix into the entity's TransformBy() method.</p>

<p>And that's really all there is to it. The below code demonstrates a very simplistic usage of TransformBy(): we want to make a mirrored copy of an entity. We use the Clone() method to generate a &quot;shallow&quot; copy of the original entity - which means that you may have less success with more complex entities that require &quot;deep&quot; cloning - and we then apply a mirror transformation to the clone (having asked the user to specify the mirror line). Please don't take this as a definitive approach to mirroring - there are probably lots of cases that it doesn't handle, in fact I've just seen a follow-up response from Philippe covering the case of mirroring text, which I'll no doubt steal for a future post :-) - but this should give an overall indication of how to use matrices to transform entities.</p>

<p>Here's the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> EntityTransformation</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;MENT&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> MirrorEntity()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Prompt for the entity to mirror</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span></span><span style="LINE-HEIGHT: 140%"> peo =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect an entity:&quot;</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityResult</span></span><span style="LINE-HEIGHT: 140%"> per =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetEntity(peo);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (per.Status != </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the points defining the mirror line</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptPointOptions</span></span><span style="LINE-HEIGHT: 140%"> ppo =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptPointOptions</span></span><span style="LINE-HEIGHT: 140%">(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect first point of mirror line:&quot;</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptPointResult</span></span><span style="LINE-HEIGHT: 140%"> ppr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.GetPoint(ppo);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (ppr.Status != </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span></span><span style="LINE-HEIGHT: 140%"> pt1 = ppr.Value;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;ppo.BasePoint = pt1;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;ppo.UseBasePoint = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;ppo.Message =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect second point of mirror line:&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;ppr = ed.GetPoint(ppo);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (ppr.Status != </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span></span><span style="LINE-HEIGHT: 140%"> pt2 = ppr.Value;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Create the mirror line and the transformation matrix</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line3d</span></span><span style="LINE-HEIGHT: 140%"> ml =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line3d</span></span><span style="LINE-HEIGHT: 140%">(pt1, pt2);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;MirrorEntity(doc.Database, per.ObjectId, ml, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Helper function to mirror an entity</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> MirrorEntity(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span></span><span style="LINE-HEIGHT: 140%"> db,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">ObjectId</span></span><span style="LINE-HEIGHT: 140%"> id,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Line3d</span></span><span style="LINE-HEIGHT: 140%"> line,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">bool</span><span style="LINE-HEIGHT: 140%"> erase</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; )</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Matrix3d</span></span><span style="LINE-HEIGHT: 140%"> mm =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Matrix3d</span></span><span style="LINE-HEIGHT: 140%">.Mirroring(line);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span></span><span style="LINE-HEIGHT: 140%"> tr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the entity to mirror</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span></span><span style="LINE-HEIGHT: 140%"> ent =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;id,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ) </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Clone it</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span></span><span style="LINE-HEIGHT: 140%"> me = ent.Clone() </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Entity</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Apply the mirror transformation</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; me.TransformBy(mm);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Add mirrored entity to the database</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%"> bt =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;db.BlockTableId,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%"> btr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;bt[</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span></span><span style="LINE-HEIGHT: 140%">.ModelSpace],</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForWrite</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; );</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; btr.AppendEntity(me);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.AddNewlyCreatedDBObject(me, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (erase)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Finally erase the original entity (if needed)</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.UpgradeOpen();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; ent.Erase();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
