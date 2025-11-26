---
layout: "post"
title: "Creating Surface Spot Elevation Label using Civil 3D .NET API"
date: "2013-02-04 22:24:19"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/creating-surface-spot-elevation-label-using-civil-3d-net-api.html "
typepad_basename: "creating-surface-spot-elevation-label-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In <a class="zem_slink" href="http://www.autodesk.com/autocad" rel="homepage" target="_blank" title="AutoCAD">AutoCAD
Civil 3D</a>, we can create different types of Labels for a Civil 3D Surface object.
In the screenshot below, we can see from Annotation Tab, using the Add Labels,
we can add a spot Elevation Label, Contour Label etc. for a Surface object.</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40c6ebc0970c-pi" style="display: inline;"><img alt="Surface_Add_Label" class="asset  asset-image at-xid-6a0167607c2431970b017d40c6ebc0970c" src="/assets/image_b5e270.jpg" title="Surface_Add_Label" /></a><br /><br /></p>
<p>Civil 3D <a class="zem_slink" href="http://msdn.microsoft.com/netframework" rel="homepage" target="_blank" title=".NET Framework">.NET</a>
<a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a> has equivalent functions for creating Labels programmatically for many of
these options as shown in the above screenshot.</p>
<p>If you want
to create Surface Spot Elevation Label or Contour Label, you can use the
following API -</p>
<p><strong>SurfaceElevationLabel.Create()</strong>&#0160;</p>
<p><strong>SurfaceContourLabelGroup.Create()</strong>&#0160;</p>
<p>&#0160;</p>
<p>In this C#
.NET code snippet below, we can see how to create TIN surface spot elevation -</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//select a surface</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> selSurface = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a Tin Surface: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selSurface.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nOnly Tin Surface is allowed&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">selSurface.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> resSurface = ed.GetEntity(selSurface);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (resSurface.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK) </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceId = resSurface.ObjectId;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//select a Point to Add Elevation Label</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;"> ppo = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect a Point to Add Spot Elevation Label: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK) </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;&#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Prepare the Elevation</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> x = ppr.Value.X;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> y = ppr.Value.Y;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;"> pnt2d = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Point2d</span><span style="line-height: 140%;">(x, y);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// create the spot elevation using SurfaceElevationLabel.Create</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="background-color: #ffff00;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceElevnLblId = </span><span style="color: #2b91af; line-height: 140%;">SurfaceElevationLabel</span><span style="line-height: 140%;">.Create(surfaceId, pnt2d);</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">SurfaceElevationLabel</span><span style="line-height: 140%;"> label = surfaceElevnLblId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">SurfaceElevationLabel</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSurface Spot Elevation Label created and Style Name is :&quot;</span><span style="line-height: 140%;"> + label.StyleName.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (Autodesk.AutoCAD.Runtime.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n Exception message :&quot;</span><span style="line-height: 140%;"> + ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>And here is
the result -</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee83baedb970d-pi" style="display: inline;"><img alt="SurfaceElevationLabel" class="asset  asset-image at-xid-6a0167607c2431970b017ee83baedb970d" src="/assets/image_11c0a3.jpg" title="SurfaceElevationLabel" /></a><br /><br /></p>
<p>&#0160;</p>
<p>Hope this is
useful to you!</p>
