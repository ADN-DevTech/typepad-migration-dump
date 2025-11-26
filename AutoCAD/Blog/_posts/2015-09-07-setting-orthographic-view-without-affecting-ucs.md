---
layout: "post"
title: "Setting orthographic view without affecting UCS"
date: "2015-09-07 02:53:23"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/09/setting-orthographic-view-without-affecting-ucs.html "
typepad_basename: "setting-orthographic-view-without-affecting-ucs"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a <a href="http://adndevblog.typepad.com/autocad/2012/07/setting-current-view-to-orthographic-view.html">previous blog post</a> on a similar topic for setting an orthographic view. The limitation with the code snippet posted in that blog post is that it modifies the active ViewportTableRecord and after the view change, the name of current UCS does not get displayed under the View Cube. When changing the view using AutoCAD's View Cube and switching to Top View, it only changes the view without affecting the UCS. Also, the X and Y axes are aligned horizontally and vertically. To get a similar behavior using the API, here is a code snippet that sets the viewing direction along +Z axis and aligns the X and Y axes just as the View Cube does.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> [DllImport(<span style="color:#a31515">&quot;accore.dll&quot;</span><span style="color:#000000"> , </pre>
<pre style="margin:0em;">     CallingConvention = CallingConvention.Cdecl, </pre>
<pre style="margin:0em;">     EntryPoint = <span style="color:#a31515">&quot;acedTrans&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">extern</span><span style="color:#000000">  <span style="color:#0000ff">int</span><span style="color:#000000">  acedTrans(</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">double</span><span style="color:#000000"> [] point, </pre>
<pre style="margin:0em;">     IntPtr fromRb, </pre>
<pre style="margin:0em;">     IntPtr toRb, </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">int</span><span style="color:#000000">  disp, </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">double</span><span style="color:#000000"> [] result);</pre>
<pre style="margin:0em;">  </pre>
<pre style="margin:0em;"> [CommandMethod(<span style="color:#a31515">&quot;SetViewDir&quot;</span><span style="color:#000000"> )]</pre>
<pre style="margin:0em;"> <span style="color:#0000ff">public</span><span style="color:#000000">  <span style="color:#0000ff">static</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  SetViewDirMethod()</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     Document doc </pre>
<pre style="margin:0em;">         = Application.DocumentManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Database db = doc.Database;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Matrix3d ucs = ed.CurrentUserCoordinateSystem;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (Transaction tr </pre>
<pre style="margin:0em;">         = db.TransactionManager.StartTransaction())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         ViewportTable vt = tr.GetObject(</pre>
<pre style="margin:0em;">             db.ViewportTableId, </pre>
<pre style="margin:0em;">             OpenMode.ForWrite) <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTable;</pre>
<pre style="margin:0em;">         ViewportTableRecord activeVTR = </pre>
<pre style="margin:0em;">             tr.GetObject(ed.ActiveViewportId, </pre>
<pre style="margin:0em;">             OpenMode.ForRead) <span style="color:#0000ff">as</span><span style="color:#000000">  ViewportTableRecord;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">using</span><span style="color:#000000">  (ViewTableRecord vtr = <span style="color:#0000ff">new</span><span style="color:#000000">  ViewTableRecord())</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             vtr.Target = activeVTR.Target;</pre>
<pre style="margin:0em;">             vtr.ViewDirection = activeVTR.ViewDirection;</pre>
<pre style="margin:0em;">             vtr.Height = activeVTR.Height;</pre>
<pre style="margin:0em;">             vtr.CenterPoint = activeVTR.CenterPoint;</pre>
<pre style="margin:0em;">                     </pre>
<pre style="margin:0em;">             vtr.ViewDirection = ucs.CoordinateSystem3d.Zaxis;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">             ed.SetCurrentView(vtr);</pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         tr.Commit();</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     ucs = ed.CurrentUserCoordinateSystem;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">double</span><span style="color:#000000"> [] resVec = <span style="color:#0000ff">new</span><span style="color:#000000">  <span style="color:#0000ff">double</span><span style="color:#000000"> [] <span style="color:#000000">{</span> 0, 0, 0 <span style="color:#000000">}</span>;</pre>
<pre style="margin:0em;">     ResultBuffer rbFrom</pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5003, 0));</pre>
<pre style="margin:0em;">     ResultBuffer rbTo </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue(5003, 2));</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     Vector3d yAxisUCS = ucs.CoordinateSystem3d.Yaxis;</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">int</span><span style="color:#000000">  res = acedTrans(</pre>
<pre style="margin:0em;">         yAxisUCS.ToArray(),</pre>
<pre style="margin:0em;">         rbFrom.UnmanagedObject,</pre>
<pre style="margin:0em;">         rbTo.UnmanagedObject,</pre>
<pre style="margin:0em;">         1,</pre>
<pre style="margin:0em;">         resVec</pre>
<pre style="margin:0em;">     );</pre>
<pre style="margin:0em;">     Vector3d yAxisDCS </pre>
<pre style="margin:0em;">         = <span style="color:#0000ff">new</span><span style="color:#000000">  Vector3d(resVec[0], resVec[1], resVec[2]);</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">double</span><span style="color:#000000">  twistAngle </pre>
<pre style="margin:0em;">         = (Math.PI * 0.5) </pre>
<pre style="margin:0em;">         - Math.Atan2(yAxisDCS.Y, yAxisDCS.X);</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (ViewTableRecord vtr </pre>
<pre style="margin:0em;">         = doc.Editor.GetCurrentView())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         vtr.ViewTwist = twistAngle;</pre>
<pre style="margin:0em;">         ed.SetCurrentView(vtr);</pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>Here is a screenshot of the View cube before and after the View change while retaining the UCS : </p>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d153f7b2970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d153f7b2970c img-responsive" alt="Before" title="Before" src="/assets/image_757485.jpg" style="margin: 0px 5px 5px 0px;" /></a>
<a class="asset-img-link"  style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ca0b46970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ca0b46970b img-responsive" alt="After" title="After" src="/assets/image_742807.jpg" style="margin: 0px 5px 5px 0px;" /></a>
