---
layout: "post"
title: "Aligning entity using 3 points"
date: "2013-07-18 23:53:06"
author: "Balaji"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "2014"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/aligning-entity-using-3-points.html "
typepad_basename: "aligning-entity-using-3-points"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to align a selected entity based on three source and their corresponding destination points. The order in which the point are selected is shown in the screenshot and a sample drawing to try the the code snippet.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Document activeDoc = Application.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Database db = activeDoc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Editor ed = activeDoc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Prompt for entity selection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptEntityResult per </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">= ed.GetEntity(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptEntityOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select an entity : &quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ObjectId oid = per.ObjectId;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Matrix3d ucs2wcs = ed.CurrentUserCoordinateSystem;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// 3 source points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d srcpt1 = Point3d.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d srcpt2 = Point3d.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d srcpt3 = Point3d.Origin;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// 3 destination points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d destpt1 = Point3d.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d destpt2 = Point3d.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Point3d destpt3 = Point3d.Origin;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Source point 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptPointResult ppr1 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select src point 1&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr1.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">srcpt1 = ppr1.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Destination point 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ppr1 = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select dest point 1&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr1.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">destpt1 = ppr1.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Display transient line to show the selection of points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">IntegerCollection coll = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> IntegerCollection();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Line tmpline1 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Line(srcpt1, destpt1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.AddTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp; (tmpline1, TransientDrawingMode.DirectShortTerm, 128, coll);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Source point 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptPointResult ppr2 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select src point 2&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr2.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">srcpt2 = ppr2.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Destination point 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ppr2 = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select dest point 2&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr2.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">destpt2 = ppr2.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Display transient line to show the selection of points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Line tmpline2 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Line(srcpt2, destpt2);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.AddTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (tmpline2, TransientDrawingMode.DirectShortTerm, 128, coll);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Source point 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">PromptPointResult ppr3 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select src point 3&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr3.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">srcpt3 = ppr3.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Destination point 3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ppr3 = ed.GetPoint(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PromptPointOptions(</span><span style="color: #a31515; line-height: 140%;">&quot;Select dest point 3&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr3.Status != PromptStatus.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">destpt3 = ppr3.Value.TransformBy(ucs2wcs);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Display transient line to show the selection of points</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Line tmpline3 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Line(srcpt3, destpt3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.AddTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (tmpline3, TransientDrawingMode.DirectShortTerm, 128, coll);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Point3d fromOrigin = srcpt1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d fromXaxis = srcpt2 - srcpt1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d fromYaxis = srcpt3 - srcpt1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d fromZaxis = fromXaxis.CrossProduct(fromYaxis);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Point3d toOrigin = destpt1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d toXaxis = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (destpt2 - destpt1).GetNormal() * fromXaxis.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d toYaxis </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; = (destpt3 - destpt1).GetNormal() * fromYaxis.Length;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Vector3d toZaxis = toXaxis.CrossProduct(toYaxis);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Get the transformation matrix for aligning coordinate systems</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Matrix3d mat = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Matrix3d();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; mat = Matrix3d.AlignCoordinateSystem(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; fromOrigin, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; fromXaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; fromYaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; fromZaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; toOrigin, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; toXaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; toYaxis, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; toZaxis</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Transform the selected entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (Transaction tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Entity ent = tr.GetObject(oid, OpenMode.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> Entity;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ent.TransformBy(mat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.Exception ex )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.WriteMessage(ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Remove the transient lines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.EraseTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; (tmpline1, coll);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tmpline1.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.EraseTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; (tmpline2, coll);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tmpline2.Dispose();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">TransientManager.CurrentTransientManager.EraseTransient</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; (tmpline3, coll);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">tmpline3.Dispose();</span></p>
</div>
<p>The screenshots show the order of selected points and the aligned entity</p>
<a class="asset-img-link"  style="float: center;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e5607e8970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e5607e8970b" alt="Input" title="Input" src="/assets/image_884730.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<a class="asset-img-link"  style="float: center;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e56081e970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e56081e970b" alt="Aligned" title="Aligned" src="/assets/image_474613.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<p>Here is a sample drawing to try the sample code :</p>
<p></p>
<span class="asset  asset-generic at-xid-6a0167607c2431970b0191044c1015970c"><a href="http://adndevblog.typepad.com/files/test.dwg">Download Test</a></span>
