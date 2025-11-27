---
layout: "post"
title: "GetIsoCurve"
date: "2015-06-17 14:27:53"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/getisocurve.html "
typepad_basename: "getisocurve"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The&#0160;<strong>SurfaceEvaluator</strong> object has a <strong>GetIsoCurve</strong> function that can return a curve on the surface running in the&#0160;<strong>U</strong> or <strong>V</strong> parametric direction.</p>
<p>The function&#39;s&#0160;<strong><em>UDirection</em>&#0160;</strong>parameter could be confusing. It tells the function if the curve should be along the <strong>U</strong> or<strong> V</strong> parametric direction, and <strong>not</strong> what the passed in parameter should be: i.e. in case of <strong>UDirection</strong> = <strong>True</strong> you get back a curve that runs in the <strong>U</strong> parametric direction, but the parameter you pass to the function should be a <strong>V</strong> parametric value.</p>
<p>Here is a function that goes along the <strong>U</strong>&#0160;direction of a <strong>Face</strong> and creates <strong>WorkPoints</strong> there plus creates a circle based on what <strong>GetIsoCurve</strong>(UDirection = False) returns:</p>
<pre>Sub GetIsoCurveTest()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim cd As PartComponentDefinition
  Set cd = doc.ComponentDefinition
  
  Dim f As Face
  For Each f In cd.SurfaceBodies(1).Faces
    &#39; We only deal with the cylindrical side face now
    If TypeOf f.Geometry Is Cylinder Then
      Dim s As SurfaceEvaluator
      Set s = f.Evaluator
    
      Dim paramRange As Box2d
      Set paramRange = s.ParamRangeRect

      &#39; Create points along U direction
      Dim uDif As Double
      uDif = (paramRange.MaxPoint.x - paramRange.MinPoint.x) / 5
    
      Dim tr As TransientGeometry
      Set tr = ThisApplication.TransientGeometry
    
      Dim wps As WorkPoints
      Set wps = cd.WorkPoints
    
      Dim i As Integer
      For i = 0 To 5
        Dim pt() As Double
        Dim params(1) As Double
        params(0) = paramRange.MinPoint.x + (uDif * i)
        params(1) = paramRange.MinPoint.y
        Call s.GetPointAtParam(params, pt)
        Call wps.AddFixed(tr.CreatePoint(pt(0), pt(1), pt(2)))
      Next
        
      &#39; Get IsoCurve in the V direction (<strong>UDirection</strong> = <strong>False</strong>) 
      &#39; at the mid U parametric position
      &#39; In case of our model this should provide a Circle
      Dim midU As Double
      midU = (paramRange.MinPoint.x + paramRange.MaxPoint.x) / 2#
      
      Dim oc As ObjectCollection
      Set oc = s.GetIsoCurve(midU, False)

      Dim circ As Inventor.Circle
      Set circ = oc(1)
    
      Dim s3d As Sketch3D
      Set s3d = cd.Sketches3D.Add()
    
      &#39; Show it in the UI by adding it to a 3d sketch
      Call s3d.SketchCircles3D.AddByCenterRadius( _
        circ.Center, circ.Normal, circ.Radius)
    End If
  Next
End Sub</pre>
<p>The result is:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d129b6ee970c-pi" style="display: inline;"><img alt="GetIsoCurves" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d129b6ee970c image-full img-responsive" src="/assets/image_2e6766.jpg" title="GetIsoCurves" /></a></p>
<p>In case of the above cylinder&#39;s side <strong>Face</strong> if you call the <strong>GetIsoCurve</strong> with <strong>UDirection</strong> = <strong>True</strong> then you&#39;ll get back a straight <strong>LineSegment</strong>&#0160;(and the passed in parameter should be along the <strong>V</strong> direction, <strong>Point2d.Y</strong>), but if pass in <strong>UDirection</strong> = <strong>False</strong> then the returned curve will be a <strong>Circle</strong> (and the passed in parameter should be along the <strong>U&#0160;</strong>direction, <strong>Point2d.X</strong>) - this is highlighted in blue in the above picture. &#0160; &#0160;</p>
