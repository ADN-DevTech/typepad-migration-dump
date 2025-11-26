---
layout: "post"
title: "Creating Viewport with UCS Follow"
date: "2015-05-26 02:52:11"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/creating-viewport-with-ucs-follow.html "
typepad_basename: "creating-viewport-with-ucs-follow"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to create a viewport with UCS Follow turned on. This should ensure that the viewport displays the plan based on the UCS whenever it changes.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [CommandMethod(&quot;CreateVP&quot;,</pre>
<pre style="margin:0em;">     CommandFlags.NoTileMode)]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  void CreateVPMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;">     = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Database db = doc.Database;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ObjectId layoutId = LayoutManager.Current.GetLayoutId</pre>
<pre style="margin:0em;">                 (LayoutManager.Current.CurrentLayout);</pre>
<pre style="margin:0em;">     </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction Tx </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Layout LayoutDest </pre>
<pre style="margin:0em;">         = Tx.GetObject(layoutId, OpenMode.ForRead) </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">as</span><span style="color:#000000">  Layout;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         BlockTableRecord btrDest </pre>
<pre style="margin:0em;">         = Tx.GetObject(LayoutDest.BlockTableRecordId, </pre>
<pre style="margin:0em;">         OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  BlockTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTable vt </pre>
<pre style="margin:0em;">         = Tx.GetObject(db.ViewportTableId, OpenMode.ForRead)</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTableRecord vtr </pre>
<pre style="margin:0em;">         = Tx.GetObject(vt[&quot;*Active&quot;], OpenMode.ForRead) </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         UcsTable ucsTbl </pre>
<pre style="margin:0em;">         = Tx.GetObject(db.UcsTableId, OpenMode.ForRead) </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">as</span><span style="color:#000000">  UcsTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#0000ff">if</span><span style="color:#000000">  (vtr != null)</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             Autodesk.AutoCAD.DatabaseServices.Viewport vpNew </pre>
<pre style="margin:0em;">             = <span style="color:#0000ff">new</span><span style="color:#000000">  Autodesk.AutoCAD.DatabaseServices.Viewport();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             vpNew.SetDatabaseDefaults();</pre>
<pre style="margin:0em;">             vpNew.Width = 6.0;</pre>
<pre style="margin:0em;">             vpNew.Height = 5.0;</pre>
<pre style="margin:0em;">             vpNew.CenterPoint = <span style="color:#0000ff">new</span><span style="color:#000000">  Point3d(3.25, 3, 0);</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (ucsTbl.Has(&quot;myucs&quot;))</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 ObjectId ucsId = ucsTbl[&quot;myucs&quot;];</pre>
<pre style="margin:0em;">                 vpNew.SetUcs(ucsId);</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             vpNew.StandardScale </pre>
<pre style="margin:0em;">             = StandardScaleType.Scale1To1;</pre>
<pre style="margin:0em;">             vpNew.ViewCenter = vtr.CenterPoint;</pre>
<pre style="margin:0em;">             vpNew.ViewHeight = vtr.Height;</pre>
<pre style="margin:0em;">             vpNew.ViewDirection = vtr.ViewDirection;</pre>
<pre style="margin:0em;">             vpNew.ViewTarget = vtr.Target;</pre>
<pre style="margin:0em;">             vpNew.TwistAngle = vtr.ViewTwist;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             vpNew.UcsPerViewport = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             vpNew.UcsFollowModeOn = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             vpNew.GridOn = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             vpNew.GridFollow = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">             btrDest.AppendEntity(vpNew);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             Tx.AddNewlyCreatedDBObject(vpNew, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">             vpNew.On = <span style="color:#0000ff">true</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Tx.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
