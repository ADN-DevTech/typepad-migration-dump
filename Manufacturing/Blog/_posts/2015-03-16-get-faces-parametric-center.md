---
layout: "post"
title: "Get Face's parametric center "
date: "2015-03-16 10:15:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/03/get-faces-parametric-center.html "
typepad_basename: "get-faces-parametric-center"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Each face has an evaluator that can be used to check the min and max parametric values on it. If you calculate the middle of that, then you can get to the center of the <strong>Face</strong>.&nbsp;</p>
<p>Here is a <strong>VBA</strong> sample that places a <strong>WorkPoint</strong> at the calculated center of the currently selected <strong>Face</strong> of the <strong>PartDocument</strong>.</p>
<pre>Sub GetFaceCenter()
  Dim d As PartDocument
  Set d = ThisApplication.ActiveDocument

  Dim f As Face
  Set f = d.SelectSet(1)
  
  Dim s As SurfaceEvaluator
  Set s = f.Evaluator
  
  Dim b As Box2d
  Set b = s.ParamRangeRect
  
  ' Get center parameter
  Dim p(1) As Double
  p(0) = b.MinPoint.x + (b.MaxPoint.x - b.MinPoint.x) / 2
  p(1) = b.MinPoint.y + (b.MaxPoint.y - b.MinPoint.y) / 2
  
  ' Convert param to point
  Dim pt(2) As Double
  Call s.GetPointAtParam(p, pt)
  
  ' Add a workpoint
  Dim t As TransientGeometry
  Set t = ThisApplication.TransientGeometry
  
  Dim wpt As Point
  Set wpt = t.CreatePoint(pt(0), pt(1), pt(2))
  
  Call d.ComponentDefinition.WorkPoints.AddFixed(wpt)
End Sub</pre>
<p>This is what I got for the various faces in one of the sample part documents:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0808185f970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0808185f970d img-responsive" title="Facecenter" src="/assets/image_a47a8e.jpg" alt="Facecenter" border="0" /></a></p>
<p>&nbsp;</p>
