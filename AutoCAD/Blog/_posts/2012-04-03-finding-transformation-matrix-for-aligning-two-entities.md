---
layout: "post"
title: "Finding Transformation matrix for aligning two entities"
date: "2012-04-03 03:24:34"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/finding-transformation-matrix-for-aligning-two-entities.html "
typepad_basename: "finding-transformation-matrix-for-aligning-two-entities"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In AutoCAD API, entities are transformed (Moved / Rotated / Scaled) using transformation matrices.</p>
<p>Here is a sample code snippet that demonstrates aligning two entities (say two lines oriented in space) by finding the required transformation matrix.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> activeDoc </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = activeDoc.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = activeDoc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetEntity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an entity : &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> oid = per.ObjectId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr1 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select src point 1&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr1.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> sp1 = ppr1.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select src point 2&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr2.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> ep1 = ppr2.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr3 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select dest point 1&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr3.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> sp2 = ppr3.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr4 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select dest point 2&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr4.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;"> ep2 = ppr4.Value;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> resMat = </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Identity;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans1 = </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Displacement(sp2-sp1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1 = sp1.TransformBy(trans1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ep1 = ep1.TransformBy(trans1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resMat = resMat.PreMultiplyBy(trans1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Rotation about Z axis</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> dir1 = ep1 - sp1;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;"> dir2 = ep2 - sp2;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> xy = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.ZAxis);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Rotation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2.AngleOnPlane(xy)-dir1.AngleOnPlane(xy),</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.ZAxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1 = sp1.TransformBy(trans2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ep1 = ep1.TransformBy(trans2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resMat = resMat.PreMultiplyBy(trans2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Rotation about X axis</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir1 = ep1 - sp1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2 = ep2 - sp2;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> yz = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.XAxis);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans3 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Rotation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2.AngleOnPlane(yz)-dir1.AngleOnPlane(yz), </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.XAxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1 = sp1.TransformBy(trans3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ep1 = ep1.TransformBy(trans3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resMat = resMat.PreMultiplyBy(trans3);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Rotation about Y axis</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir1 = ep1 - sp1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2 = ep2 - sp2;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;"> xz = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Plane</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Point3d</span><span style="line-height: 140%;">.Origin, </span><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.YAxis);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans4 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Rotation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2.AngleOnPlane(yz)-dir1.AngleOnPlane(xz), </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Vector3d</span><span style="line-height: 140%;">.YAxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1 = sp1.TransformBy(trans4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ep1 = ep1.TransformBy(trans4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resMat = resMat.PreMultiplyBy(trans4);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Scaling</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir1 = ep1 - sp1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2 = ep2 - sp2;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;"> trans5 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= </span><span style="color: #2b91af; line-height: 140%;">Matrix3d</span><span style="line-height: 140%;">.Scaling</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">dir2.Length / dir1.Length, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sp1 = sp1.TransformBy(trans5);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ep1 = ep1.TransformBy(trans5);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">resMat = resMat.PreMultiplyBy(trans5);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;"> ent = (</span><span style="color: #2b91af; line-height: 140%;">Entity</span><span style="line-height: 140%;">)tr.GetObject(oid, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ent.TransformBy(resMat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
