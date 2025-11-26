---
layout: "post"
title: "Create four split modelspace viewports and set different orthographic views"
date: "2015-08-13 01:54:13"
author: "Balaji"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/08/create-four-split-modelspace-viewports-and-set-different-orthographic-views.html "
typepad_basename: "create-four-split-modelspace-viewports-and-set-different-orthographic-views"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently, a developer wanted to modify the sample code from <a href="http://exchange.autodesk.com/autocadmechanical/enu/online-help/AMECH_PP/2012/ENU/pages/WS1a9193826455f5ff2566ffd511ff6f8c7ca-42f6.htm">here</a> to create split model space viewports and set different orthographic view in each of them. Here is a sample code to do that. The below code creates four split modelspace viewports. When creating the new ViewportTableRecords that represent the newly created split viewports, its view parameters are set from the active ViewportTableRecord. This ensures that the ViewportTableRecord is correctly setup for us to set the orthographic view. Finally, each modelspace viewport is zoomed to extents. Here is a screenshot of the resulting viewport arrangement.</p>
<p></p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d148502f970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d148502f970c img-responsive" alt="SplitVP" title="SplitVP" src="/assets/image_41843.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#008000">// Create 4 split modelspace viewports,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// set different orthographic view direction in each </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// and zoom to extents.</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;SplitMVP&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SplitAndSetViewModelViewports()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;">         = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Database db = doc.Database;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     db.UpdateExt(<span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;">     Extents3d dbExtent </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  Extents3d(db.Extmin, db.Extmax);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ViewportTable vt = tr.GetObject(</pre>
<pre style="margin:0em;">             db.ViewportTableId, OpenMode.ForWrite) </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTable;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTableRecord vtr1 = tr.GetObject(</pre>
<pre style="margin:0em;">             doc.Editor.ActiveViewportId, </pre>
<pre style="margin:0em;">             OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTableRecord;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         Point2d ll = vtr1.LowerLeftCorner;</pre>
<pre style="margin:0em;">         Point2d ur = vtr1.UpperRightCorner;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         vtr1.LowerLeftCorner = ll;</pre>
<pre style="margin:0em;">         vtr1.UpperRightCorner = <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(</pre>
<pre style="margin:0em;">             ll.X + (ur.X - ll.X) * 0.5, </pre>
<pre style="margin:0em;">             ll.Y + (ur.Y - ll.Y) * 0.5);</pre>
<pre style="margin:0em;">         vtr1.SetViewDirection(OrthographicView.LeftView);</pre>
<pre style="margin:0em;">         ZoomExtents(vtr1, dbExtent);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTableRecord vtr2 = </pre>
<pre style="margin:0em;">             CreateVTR(vt, vtr1, </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(ll.X + (ur.X - ll.X) * 0.5, ll.Y), </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(ur.X, ll.Y + (ur.Y - ll.Y) * 0.5), </pre>
<pre style="margin:0em;">             dbExtent, OrthographicView.RightView);</pre>
<pre style="margin:0em;">         vt.Add(vtr2);</pre>
<pre style="margin:0em;">         tr.AddNewlyCreatedDBObject(vtr2, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTableRecord vtr3 = </pre>
<pre style="margin:0em;">             CreateVTR(vt, vtr1, </pre>
<pre style="margin:0em;">             vtr1.UpperRightCorner, ur, </pre>
<pre style="margin:0em;">             dbExtent, OrthographicView.BottomView);</pre>
<pre style="margin:0em;">         vt.Add(vtr3);</pre>
<pre style="margin:0em;">         tr.AddNewlyCreatedDBObject(vtr3, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         ViewportTableRecord vtr4 = </pre>
<pre style="margin:0em;">             CreateVTR(vt, vtr1, </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(ll.X, ll.Y + (ur.Y - ll.Y) * 0.5), </pre>
<pre style="margin:0em;">             <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(ll.X + (ur.X - ll.X) * 0.5, ur.Y), </pre>
<pre style="margin:0em;">             dbExtent, OrthographicView.TopView);</pre>
<pre style="margin:0em;">         vt.Add(vtr4);</pre>
<pre style="margin:0em;">         tr.AddNewlyCreatedDBObject(vtr4, <span style="color:#0000ff">true</span><span style="color:#000000"> );</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Update the display with new tiled viewports</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         doc.Editor.UpdateTiledViewportsFromDatabase();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">         <span style="color:#008000">// Commit the changes </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// Create a model space viewport and uses the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// view parameters from a reference viewport</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#008000">// before setting the orthographic view and zooming to extents</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  ViewportTableRecord CreateVTR(</pre>
<pre style="margin:0em;">     ViewportTable vt, ViewportTableRecord refVTR, </pre>
<pre style="margin:0em;">     Point2d ll, Point2d ur, Extents3d dbExtent, </pre>
<pre style="margin:0em;">     OrthographicView ov)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     ViewportTableRecord newVTR = <span style="color:#0000ff">new</span><span style="color:#000000">  ViewportTableRecord();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     newVTR.LowerLeftCorner = ll;</pre>
<pre style="margin:0em;">     newVTR.UpperRightCorner = ur;</pre>
<pre style="margin:0em;">     newVTR.Name = <span style="color:#a31515">&quot;*Active&quot;</span><span style="color:#000000"> ;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     newVTR.ViewDirection = refVTR.ViewDirection;</pre>
<pre style="margin:0em;">     newVTR.ViewTwist = refVTR.ViewTwist;</pre>
<pre style="margin:0em;">     newVTR.Target = refVTR.Target;</pre>
<pre style="margin:0em;">     newVTR.BackClipEnabled = refVTR.BackClipEnabled;</pre>
<pre style="margin:0em;">     newVTR.BackClipDistance = refVTR.BackClipDistance;</pre>
<pre style="margin:0em;">     newVTR.FrontClipEnabled = refVTR.FrontClipEnabled;</pre>
<pre style="margin:0em;">     newVTR.FrontClipDistance = refVTR.FrontClipDistance;</pre>
<pre style="margin:0em;">     newVTR.Elevation = refVTR.Elevation;</pre>
<pre style="margin:0em;">     newVTR.SetViewDirection(ov);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ZoomExtents(newVTR, dbExtent);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">return</span><span style="color:#000000">  newVTR;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  ZoomExtents</pre>
<pre style="margin:0em;">     (ViewportTableRecord vtr, Extents3d dbExtent)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     <span style="color:#008000">//get the screen aspect ratio to </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// calculate the height and width</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">double</span><span style="color:#000000">  scrRatio = (vtr.Width / vtr.Height);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//prepare Matrix for DCS to WCS transformation</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     Matrix3d matWCS2DCS </pre>
<pre style="margin:0em;">         = Matrix3d.PlaneToWorld(vtr.ViewDirection);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//for DCS target point is the origin</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     matWCS2DCS = Matrix3d.Displacement</pre>
<pre style="margin:0em;">         (vtr.Target - Point3d.Origin) * matWCS2DCS;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//WCS Xaxis is twisted by twist angle</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     matWCS2DCS = Matrix3d.Rotation(-vtr.ViewTwist,</pre>
<pre style="margin:0em;">                                     vtr.ViewDirection,</pre>
<pre style="margin:0em;">                                     vtr.Target</pre>
<pre style="margin:0em;">                                 ) * matWCS2DCS;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	matWCS2DCS = matWCS2DCS.Inverse();</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//tranform the extents to the DCS </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// defined by the viewdir</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	dbExtent.TransformBy(matWCS2DCS);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//width of the extents in current view</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  width </pre>
<pre style="margin:0em;">         = (dbExtent.MaxPoint.X - dbExtent.MinPoint.X);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//height of the extents in current view</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">double</span><span style="color:#000000">  height </pre>
<pre style="margin:0em;">         = (dbExtent.MaxPoint.Y - dbExtent.MinPoint.Y);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//get the view center point</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	Point2d center = <span style="color:#0000ff">new</span><span style="color:#000000">  Point2d(</pre>
<pre style="margin:0em;">         (dbExtent.MaxPoint.X + dbExtent.MinPoint.X) * 0.5,</pre>
<pre style="margin:0em;"> 		(dbExtent.MaxPoint.Y + dbExtent.MinPoint.Y) * 0.5);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//check if the width&#39; in current window</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//if not then get the new height as per the </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// viewports aspect ratio</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#0000ff">if</span><span style="color:#000000">  (width &gt; (height * scrRatio))</pre>
<pre style="margin:0em;"> 		height = width / scrRatio;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     vtr.Height = height;</pre>
<pre style="margin:0em;">     vtr.Width = height * scrRatio;</pre>
<pre style="margin:0em;">     vtr.CenterPoint = center;</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
