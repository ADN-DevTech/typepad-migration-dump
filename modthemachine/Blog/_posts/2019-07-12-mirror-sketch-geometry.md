---
layout: "post"
title: "Mirror Sketch Geometry"
date: "2019-07-12 10:48:19"
author: "Adam Nagy"
categories:
  - "Adam"
  - "Inventor"
  - "Visual Basic for Applications (VBA)"
original_url: "https://modthemachine.typepad.com/my_weblog/2019/07/mirror-sketch-geometry.html "
typepad_basename: "mirror-sketch-geometry"
typepad_status: "Publish"
---

<p>In the <strong>Sketch</strong> environment there is a <strong>Mirror</strong> command in the <strong>Pattern</strong> palette:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a4bbff76200b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Sketch_Mirror4" class="asset  asset-image at-xid-6a00e553fcbfc688340240a4bbff76200b img-responsive" src="/assets/image_164438.jpg" title="Sketch_Mirror4" /></a></p>
<p>Unfortunately, its functionality is not directly exposed in the <strong>API</strong>.&#0160; <br />However, we can check what it does and try to replicate it.</p>
<p>When I mirror sketch geometry, I can see that the <strong>Mirror</strong> command basically <strong>copies</strong> all the <strong>sketch points and lines</strong>, and then sets up a <strong>symmetry constraint</strong> between the original and copied <strong>sketch points</strong>.</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a46e2523200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Sketch_Mirror3" class="asset  asset-image at-xid-6a00e553fcbfc688340240a46e2523200c img-responsive" src="/assets/image_2382.jpg" title="Sketch_Mirror3" /></a></p>
<p>Here is a <strong>VBA</strong> sample which achieves this for <strong>sketch lines </strong>- for other geometry you would have to implement the functionality in a similar fashion:</p>
<pre>Function CopyAndConstrainPoint(pt As SketchPoint, ln As SketchLine, pts As Dictionary) As SketchPoint
  &#39; The sketch we are working in
  Dim sk As PlanarSketch
  Set sk = pt.Parent
     
  &#39; Check if we already copied this
  &#39; sketch point
  If Not pts.Exists(pt) Then
    &#39; Create new point
    Set CopyAndConstrainPoint = sk.SketchPoints.Add(pt.Geometry, False)
    Call pts.Add(pt, CopyAndConstrainPoint)
    &#39; Constrain it
    Call sk.GeometricConstraints.AddSymmetry(pt, CopyAndConstrainPoint, ln)
  Else
    Set CopyAndConstrainPoint = pts(pt)
  End If
End Function

Sub MirrorSketchLines()
  Dim selSet As SelectSet
  Set selSet = ThisApplication.ActiveDocument.SelectSet

  &#39; The first selected item needs to be the
  &#39; mirror line
  Dim mirrorLine As SketchLine
  Set mirrorLine = selSet(1)
  
  &#39; The sketch we are working in
  Dim sk As PlanarSketch
  Set sk = mirrorLine.Parent
  
  &#39; Let&#39;s get the lines we need to mirror
  &#39; We need to copy the lines, the sketch points
  &#39; and set up a symmetry constraint between the
  &#39; points and the mirror line
  Dim mirroredSketchPoints As New Dictionary
  Dim sketchLines As ObjectCollection
  Set sketchLines = ThisApplication.TransientObjects.CreateObjectCollection()
  
  &#39; First store the selection set
  &#39; because it might get reset when adding new
  &#39; sketch entities and setting up constraints
  Dim i As Integer
  For i = 2 To selSet.Count
    If TypeOf selSet(i) Is SketchLine Then
      Call sketchLines.Add(selSet(i))
    End If
  Next
      
  Dim line As SketchLine
  For Each line In sketchLines
    &#39; Get the copied points
    Dim sp As SketchPoint
    Set sp = CopyAndConstrainPoint(line.StartSketchPoint, mirrorLine, mirroredSketchPoints)
    
    Dim ep As SketchPoint
    Set ep = CopyAndConstrainPoint(line.EndSketchPoint, mirrorLine, mirroredSketchPoints)
    
    &#39; Create the new line using the copied and constrained
    &#39; sketch points
    Call sk.sketchLines.AddByTwoPoints(sp, ep)
  Next
End Sub</pre>
<p>When running it, make sure that you <strong>first</strong> select the <strong>mirror line</strong>, and <strong>then</strong> the <strong>rest of the geometry</strong>:</p>
<p><a class="asset-img-link" href="https://modthemachine.typepad.com/.a/6a00e553fcbfc688340240a46e2539200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Sketch_Mirror2" class="asset  asset-image at-xid-6a00e553fcbfc688340240a46e2539200c img-responsive" src="/assets/image_61163.jpg" title="Sketch_Mirror2" /></a></p>
<p>-Adam</p>
