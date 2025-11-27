---
layout: "post"
title: "Cannot extrude sketch geometry"
date: "2015-02-09 10:34:13"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/cannot-extrude-sketch-geometry.html "
typepad_basename: "cannot-extrude-sketch-geometry"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html">Adam Nagy</a></p>
<p>You may have created sketch curves that overlap, but their end points do not meet, so you cannot extrude the area they enclose. Actually, the end points of the curves do not need to coincide in order to be recognised as a profile; it&#39;s enough if the curves are tied together with points - i.e. there is a point that is constrained to be coincident with both curves.</p>
<p>So if you start with something like this:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d30892970c-pi" style="display: inline;"><img alt="Sketch1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d30892970c img-responsive" src="/assets/image_e4736e.jpg" title="Sketch1" /></a></p>
<p>Then you can use the following <strong>VBA</strong> code to solve the problem:</p>
<pre>Public Sub AddSketchPoints()
  &#39; You need to be in Sketch environment
  Dim sk As PlanarSketch
  Set sk = ThisApplication.ActiveEditObject
  
  &#39; This expects to have the sketch
  &#39; entities shown in the picture:
  &#39; straight lines on each side connected
  &#39; by arcs
  Dim lines As SketchLines
  Set lines = sk.SketchLines
  
  Dim arcs As SketchArcs
  Set arcs = sk.SketchArcs
  
  &#39; Create 4 points and constrain each to
  &#39; a line and an arc
  Dim pts As SketchPoints
  Set pts = sk.SketchPoints
  
  Dim gc As GeometricConstraints
  Set gc = sk.GeometricConstraints
  
  Dim line As SketchLine
  Dim arc As SketchArc
  For Each line In lines
    For Each arc In arcs
      &#39; Temporarily fix the line and arc so
      &#39; they do not move around
      Dim gnd(1) As GroundConstraint
      Set gnd(0) = gc.AddGround(line)
      Set gnd(1) = gc.AddGround(arc)
    
      &#39; Does not matter where we place
      &#39; it initially
      Dim pt As SketchPoint
      Set pt = pts.Add(line.StartSketchPoint.Geometry)
      
      &#39; Now constrain it to a line
      &#39; and an arc
      Call gc.AddCoincident(arc, pt)
      Call gc.AddCoincident(line, pt)
      
      &#39; Delete the temporary constraints
      Call gnd(0).Delete
      Call gnd(1).Delete
    Next
  Next
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d308aa970c-pi" style="display: inline;"><img alt="Sketch2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d308aa970c img-responsive" src="/assets/image_135573.jpg" title="Sketch2" /></a></p>
<p>Now this can be extruded:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07ed4fa6970d-pi" style="display: inline;"><img alt="Sketch3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07ed4fa6970d image-full img-responsive" src="/assets/image_0d7e30.jpg" title="Sketch3" /></a></p>
<p>&#0160;</p>
