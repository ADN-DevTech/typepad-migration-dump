---
layout: "post"
title: "Interoperability between Civil 3D and Inventor"
date: "2012-07-03 10:46:44"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD Civil 3D 2012"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/interoperability-between-civil-3d-and-inventor.html "
typepad_basename: "interoperability-between-civil-3d-and-inventor"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/augusto-goncalves.html" target="_blank">Augusto Goncalves</a></p>
<p>This article is based on a case study presented at <a href="http://adndevblog.typepad.com/infrastructure/2012/04/aec-devcamp-2012.html" target="_blank">ADN DevCamp 2012</a> on how improve interoperability between Civil 3D and Inventor. The image below is a summary of what was done and presented: read Corridor data at Civil 3D to create a spline at a Inventor sketch. It is required some Inventor API knowledge, so if you now familiar with it, please visit the <a href="http://www.autodesk.com/developinventor" target="_blank">Inventor Developer Center</a>.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167682133ba970b-pi"><img alt="c3d_inventor" border="0" height="190" src="/assets/image_02776f.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border: 0px;" title="c3d_inventor" width="422" /></a></p>
<p>The idea was to use Inventor out-of-process COM API from Civil 3D in-process .NET API based plug-in as the main architecture. This approach allow us direct access to the objects without use of any intermediate file, which is also a common practice, but can result in a lot of extra work. One disadvantage is that both application must be installed and running (although we can launch automatically).</p>
<p>Below is a very simplified version of the plug-in to demonstrate the idea, but you can  <span class="asset  asset-generic at-xid-6a0167607c2431970b017742fc43d0970d"><a href="http://adndevblog.typepad.com/files/c3dalignment2invsketch.zip">download the full version here</a></span>. For a given Civil 3D Corridor Feature Line, get all AutoCAD Geometry Points, then create a Inventor 3D Sketch, where the 3D Spline will be drawn. Like AutoCAD Civil 3D, Inventor has a type of geometry entities called Transient Geometry, so the code creates Inventor Transient Points based on the AutoCAD Geometry Points XYZ coordinates, which are then used to create Inventor Sketch Points that compose the Inventor Sketch Spline.</p>
<div style="background: white;">
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Get the feature line collection of Acad geometry points</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="color: #2b91af;"><span style="font-size: 8pt;">Point3dCollection</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> points = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">Point3dCollection</span></span><span style="color: #000000;">();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Iterate through the collection of feature points</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">foreach</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> (</span><span><span style="color: #2b91af;">FeatureLinePoint</span></span><span style="color: #000000;"> flPoint </span><span><span style="color: #0000ff;">in</span></span><span style="color: #000000;"> featureLine.FeatureLinePoints)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span><span style="font-size: 8pt; color: #008000;">// If is between the margin we seek</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">if</span></span><span style="color: #000000;"> (flPoint.Station &gt; startStation &amp;&amp; flPoint.Station &lt; endStation)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; {</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160;&#0160;&#0160; </span></span><span><span style="font-size: 8pt; color: #008000;">// Add to the collection</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; points.Add(flPoint.XYZ);</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; }</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">}</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Get running Inventor and its open document (Part Doc)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Application</span></span><span style="color: #000000;"> app = GetApplication();</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">PartDocument</span></span><span style="color: #000000;"> partDoc = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; app.ActiveDocument </span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">as</span></span><span style="color: #000000;"> Inventor.</span><span><span style="color: #2b91af;">PartDocument</span></span><span style="color: #000000;">;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Create a new sketch</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Sketch3D</span></span><span style="color: #000000;"> sketch = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; partDoc.ComponentDefinition.Sketches3D.Add();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Access Inventor Transient Geometry object, </span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// used to create non-visual (in memory geometry) objects</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">TransientGeometry</span></span><span style="color: #000000;"> tg = app.TransientGeometry;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Create a collection of points</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">ObjectCollection</span></span><span style="color: #000000;"> pointForInventor = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; app.TransientObjects.CreateObjectCollection();</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Convert each Acad Geometry point into Inventor Geometry points</span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// and store into the collection</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">foreach</span></span></span><span style="font-size: 8pt;"><span style="color: #000000;"> (</span><span><span style="color: #2b91af;">Point3d</span></span><span style="color: #000000;"> acadPt </span><span><span style="color: #0000ff;">in</span></span><span style="color: #000000;"> points)</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; pointForInventor.Add(</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; sketch.SketchPoints3D.Add(</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;&#0160;&#0160; tg.CreatePoint(acadPt.X, acadPt.Y, acadPt.Z)));</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">}</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// Finally use the collection of points to create the </span></span></span></p>
<p style="margin: 0px;"><span><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #008000;">// new spline at the new sketch</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;"><span style="font-size: 8pt;">Inventor.</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">SketchSpline3D</span></span><span style="color: #000000;"> spline = sketch.SketchSplines3D.Add(</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="font-size: 8pt; color: #000000;">&#0160; pointForInventor);</span></span></p>
</div>
<p>It is possible to note here that the raw data, basically coordinates as double values, is immediately transferred without any lost or intermediate conversion. For a approach like that it is required to go deep enough (i.e. from Civil 3D Corridor, to Feature Line, to Geometry Points) and then back (i.e. from Inventor Transient Point, to Sketch Point, to Sketch Spline) so it’s possible to base the execution only on raw data.</p>
