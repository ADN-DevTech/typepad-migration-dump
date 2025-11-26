---
layout: "post"
title: "Creating Civil 3D TIN Surface from Contours (Polylines) using .NET API"
date: "2014-01-28 05:12:24"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/01/creating-civil-3d-tin-surface-from-contours-polylines-using-net-api.html "
typepad_basename: "creating-civil-3d-tin-surface-from-contours-polylines-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>You have a <a class="zem_slink" href="http://en.wikipedia.org/wiki/.dwg" rel="wikipedia" target="_blank" title=".dwg">DWG</a> file with <a class="zem_slink" href="http://en.wikipedia.org/wiki/Contour_line" rel="wikipedia" target="_blank" title="Contour line">contour lines</a> which are actually polylines with elevation values and you want to create a Civil 3D TIN Surface using those Polylines.</p>
<p>Here is a screenshot which shows few Polylines in a DWG file :</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d682b8e970d-pi" style="display: inline;"><img alt="Civil3D_Contour_Lines" class="asset  asset-image at-xid-6a0167607c2431970b01a73d682b8e970d img-responsive" src="/assets/image_62498f.jpg" title="Civil3D_Contour_Lines" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>If want to create a Civil 3D TIN Surface using these Polylines, first you need to create an empty TinSurface by using <strong>TinSurface.Create()</strong> <a class="zem_slink" href="http://en.wikipedia.org/wiki/Application_programming_interface" rel="wikipedia" target="_blank" title="Application programming interface">API</a> and then using <strong>SurfaceOperationAddContour.AddContours()</strong> API you need to add the Polylines to the TinSurface.&#0160;</p>
<p>Here is a C#<a class="zem_slink" href="http://www.microsoft.com/net" rel="homepage" target="_blank" title=".NET Framework">.NET</a> code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the AutoCAD Editor</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">CivilDocument</span><span style="line-height: 140%;"> civilDoc = </span><span style="color: #2b91af; line-height: 140%;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Select a Surface style to use </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> styleId = civilDoc.Styles.SurfaceStyles[</span><span style="color: #a31515; line-height: 140%;">&quot;Slope Banding (2D)&quot;</span><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">//Contour Entities collection</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;"> contourEntitiesIdColl = </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">ObjectIdCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// In the following line I use a custom function GetContourEntities() </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// which creates a collection of all the contour polyline Ids</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; contourEntitiesIdColl = GetContourEntities(); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (styleId != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;"> &amp;&amp; contourEntitiesIdColl != </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Create an empty TIN Surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="background-color: #ffff00;"><strong><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> surfaceId = </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">.Create(</span><span style="color: #a31515; line-height: 140%;">&quot;TIN_Surface_From_Contours&quot;</span><span style="line-height: 140%;">, styleId);</span></strong></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> surface = trans.GetObject(surfaceId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">//public SurfaceOperationAddContour AddContours( ObjectIdCollection contourEntities, double midOrdinateDistance,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// double maximumDistance,&#0160; double weedingDistance,&#0160; double weedingAngle )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; <span style="background-color: #ffff00;"><strong>surface.ContoursDefinition.AddContours(contourEntitiesIdColl, 1.0, 100.00, 15.0, 4.0);&#0160;</strong></span> &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">catch</span><span style="line-height: 140%;"> (System.</span><span style="color: #2b91af; line-height: 140%;">Exception</span><span style="line-height: 140%;"> ex)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// handle the exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Surface Creation from Contours Failed !&quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &quot;</span><span style="line-height: 140%;"> +&#0160; ex.Message);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>&#0160;</p>
<p>And a TinSurface created using Polylines :</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5115c97f0970c-pi" style="display: inline;"><img alt="Civil3D_Contour_Lines_used2create_TinSurface" class="asset  asset-image at-xid-6a0167607c2431970b01a5115c97f0970c img-responsive" src="/assets/image_d12216.jpg" title="Civil3D_Contour_Lines_used2create_TinSurface" /></a></p>
