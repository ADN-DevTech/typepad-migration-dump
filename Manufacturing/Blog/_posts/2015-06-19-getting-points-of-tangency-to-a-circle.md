---
layout: "post"
title: "Getting points of tangency to a circle"
date: "2015-06-19 20:42:22"
author: "Balaji"
categories:
  - "Balaji Ramamoorthy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/getting-points-of-tangency-to-a-circle.html "
typepad_basename: "getting-points-of-tangency-to-a-circle"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In a recent query, a developer wanted to retrieve the points of tangency of sketch lines with circles. The way to do get that information is to set up the sketch constraints right just as you would with the Inventor UI. The geometry can then be retrieved from the tangent lines using SketchLine.Geometry. Here is a sample code snippet and the screenshot of sketch that it generates.</p>
<p></p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  partDoc <span style="color:#0000ff">As</span><span style="color:#000000">  PartDocument = _</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">TryCast</span><span style="color:#000000"> (_invApp.ActiveDocument, PartDocument)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCompDef <span style="color:#0000ff">As</span><span style="color:#000000">  PartComponentDefinition _</pre>
<pre style="margin:0em;">         = partDoc.ComponentDefinition</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; New sketch</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oSketch <span style="color:#0000ff">As</span><span style="color:#000000">  PlanarSketch = _</pre>
<pre style="margin:0em;">         oCompDef.Sketches.Add(oCompDef.WorkPlanes(3))</pre>
<pre style="margin:0em;">     oSketch.Name = <span style="color:#a31515">&quot;MySketch&quot;</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTG <span style="color:#0000ff">As</span><span style="color:#000000">  TransientGeometry = _</pre>
<pre style="margin:0em;">         _invApp.TransientGeometry</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  geomConstraints <span style="color:#0000ff">As</span><span style="color:#000000">  GeometricConstraints _</pre>
<pre style="margin:0em;">         = oSketch.GeometricConstraints</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  points <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoints = oSketch.SketchPoints</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp0 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = points.Add( _</pre>
<pre style="margin:0em;">         oTG.CreatePoint2d(0, 0), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp1 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = points.Add( _</pre>
<pre style="margin:0em;">         oTG.CreatePoint2d(10, 0), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Construction line joining center points</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span&#39; of the circles</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCenLine <span style="color:#0000ff">As</span><span style="color:#000000">  SketchLine = _</pre>
<pre style="margin:0em;">         oSketch.SketchLines.AddByTwoPoints(skp0, skp1)</pre>
<pre style="margin:0em;">     oCenLine.Construction = <span style="color:#0000ff">True</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     geomConstraints.AddHorizontal(oCenLine)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Circle with radius 4</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCircle1 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchCircle = _</pre>
<pre style="margin:0em;">         oSketch.SketchCircles.AddByCenterRadius( _</pre>
<pre style="margin:0em;">             oCenLine.StartSketchPoint, 4)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Circle with radius 2</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oCircle2 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchCircle = _</pre>
<pre style="margin:0em;">         oSketch.SketchCircles.AddByCenterRadius( _</pre>
<pre style="margin:0em;">             oCenLine.EndSketchPoint, 2)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Fix the center points</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     geomConstraints.AddGround(oCircle1.CenterSketchPoint)</pre>
<pre style="margin:0em;">     geomConstraints.AddGround(oCircle2.CenterSketchPoint)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Line 1 that we will make tangential to the circle</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp2 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = _</pre>
<pre style="margin:0em;">         points.Add(oTG.CreatePoint2d(0, 4), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp3 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = _</pre>
<pre style="margin:0em;">         points.Add(oTG.CreatePoint2d(10, 2), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTanLine1 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchLine = _</pre>
<pre style="margin:0em;">         oSketch.SketchLines.AddByTwoPoints(skp2, skp3)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Line 2 that we will make tangential to the circle </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp4 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = points.Add( _</pre>
<pre style="margin:0em;">         oTG.CreatePoint2d(0, -4), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  skp5 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchPoint = points.Add( _</pre>
<pre style="margin:0em;">         oTG.CreatePoint2d(10, -2), <span style="color:#0000ff">False</span><span style="color:#000000"> )</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTanLine2 <span style="color:#0000ff">As</span><span style="color:#000000">  SketchLine = _</pre>
<pre style="margin:0em;">         oSketch.SketchLines.AddByTwoPoints(skp4, skp5)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Make both the lines tangential to the circles</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     geomConstraints.AddTangent(oCircle1, oTanLine1)</pre>
<pre style="margin:0em;">     geomConstraints.AddTangent(oCircle2, oTanLine1)</pre>
<pre style="margin:0em;">     geomConstraints.AddTangent(oCircle1, oTanLine2)</pre>
<pre style="margin:0em;">     geomConstraints.AddTangent(oCircle2, oTanLine2)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Ensure that the tangent end points are on circles</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     geomConstraints.AddCoincident( _</pre>
<pre style="margin:0em;">         oTanLine1.StartSketchPoint, oCircle1)</pre>
<pre style="margin:0em;">     geomConstraints.AddCoincident( _</pre>
<pre style="margin:0em;">         oTanLine1.EndSketchPoint, oCircle2)</pre>
<pre style="margin:0em;">     geomConstraints.AddCoincident( _</pre>
<pre style="margin:0em;">         oTanLine2.StartSketchPoint, oCircle1)</pre>
<pre style="margin:0em;">     geomConstraints.AddCoincident( _</pre>
<pre style="margin:0em;">         oTanLine2.EndSketchPoint, oCircle2)</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span&#39; Get the goemetry from tangents</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTanLine1Geom <span style="color:#0000ff">As</span><span style="color:#000000">  LineSegment2d _</pre>
<pre style="margin:0em;">         = oTanLine1.Geometry</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  oTanLine2Geom <span style="color:#0000ff">As</span><span style="color:#000000">  LineSegment2d _</pre>
<pre style="margin:0em;">         = oTanLine2.Geometry</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  sp1 <span style="color:#0000ff">As</span><span style="color:#000000">  Point2d = oTanLine1Geom.StartPoint</pre>
<pre style="margin:0em;">     <span style="color:#0000ff">Dim</span><span style="color:#000000">  ep1 <span style="color:#0000ff">As</span><span style="color:#000000">  Point2d = oTanLine1Geom.EndPoint</pre>
<pre style="margin:0em;">     MessageBox.Show(<span style="color:#0000ff">String</span><span style="color:#000000"> .Format( _</pre>
<pre style="margin:0em;">         <span style="color:#a31515">&quot;Tangent 1 SP : <span style="color:#000000">{</span>0<span style="color:#000000">}</span> <span style="color:#000000">{</span>1<span style="color:#000000">}</span> EP : <span style="color:#000000">{</span>2<span style="color:#000000">}</span> <span style="color:#000000">{</span>3<span style="color:#000000">}</span>&quot;</span><span style="color:#000000"> , _</pre>
<pre style="margin:0em;">         sp1.X, sp1.Y, ep1.X, ep1.Y))</pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p></p>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0845ad2f970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0845ad2f970d image-full img-responsive" alt="Sketch" title="Sketch" src="/assets/image_4c0a5f.jpg" border="0" /></a><br />
