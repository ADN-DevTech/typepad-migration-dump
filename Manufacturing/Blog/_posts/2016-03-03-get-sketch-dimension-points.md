---
layout: "post"
title: "Get Sketch Dimension points"
date: "2016-03-03 11:39:14"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/get-sketch-dimension-points.html "
typepad_basename: "get-sketch-dimension-points"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Each type of dimension might&#0160;provide information in a different way. So the easiest way to get familiar with what&#39;s available is if you select the entity in the <strong>UI</strong> and investigate it in <strong>VBA</strong>: <a href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html">Discover object model</a>&#0160;</p>
<p>Here is a <strong>VBA</strong> sample that will highlight all the points available in case of a <strong>TwoLineAngleDimConstraint</strong> by creating temporary <strong>SketchPoints</strong>. &#0160;</p>
<pre>Sub DeletePoints(c As ObjectCollection)
  Dim o As Object
  For Each o In c
    o.Delete
  Next
  
  Call c.Clear
End Sub

Sub GetDimensionGeometry()
  &#39; The Sketch needs to be active and a 
  &#39; two line angle dimension constraint needs 
  &#39; to be selected in the UI 
  Dim d As TwoLineAngleDimConstraint
  Set d = ThisApplication.ActiveDocument.SelectSet(1)
  
  Dim sps As SketchPoints
  Set sps = d.Parent.SketchPoints
  
  Dim sp As SketchPoint
  Dim c As ObjectCollection
  Set c = ThisApplication.TransientObjects.CreateObjectCollection()
  
  &#39; Anchor points (<strong><span style="color: #ff7f00;">A1..A5</span></strong>)
  Dim pt As Point2d
  For Each pt In d.AnchorPoints
    Call c.Add(sps.Add(pt))
  Next
  
  MsgBox (&quot;AnchorPoints&quot;)
  
  Call DeletePoints(c)
  
  &#39; LineOne (<strong><span style="color: #ff7f00;">L1S, L1E</span></strong>)
  Call c.Add(sps.Add(d.LineOne.StartSketchPoint.Geometry))
  Call c.Add(sps.Add(d.LineOne.EndSketchPoint.Geometry))
  
  MsgBox (&quot;LineOne&quot;)
  
  Call DeletePoints(c)
  
  &#39; LineTwo (<strong><span style="color: #ff7f00;">L2S, L2E</span></strong>)
  Call c.Add(sps.Add(d.LineTwo.StartSketchPoint.Geometry))
  Call c.Add(sps.Add(d.LineTwo.EndSketchPoint.Geometry))
  
  MsgBox (&quot;LineTwo&quot;)
  
  Call DeletePoints(c)
  
  &#39; DimensionCenterPoint (<strong><span style="color: #ff7f00;">DC</span></strong>)
  Call c.Add(sps.Add(d.DimensionCenterPoint))
  
  MsgBox (&quot;DimensionCenterPoint&quot;)
  
  Call DeletePoints(c)
  
  &#39; TextPoint (<span style="color: #ff7f00;"><strong>TP</strong></span>)
  Call c.Add(sps.Add(d.TextPoint))
  
  MsgBox (&quot;TextPoint&quot;)
  
  Call DeletePoints(c)
End Sub</pre>
<p>These are all the points showing:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c23eef970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="SketchDimension" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c23eef970d img-responsive" src="/assets/image_666c1b.jpg" title="SketchDimension" /></a></p>
<p>Once you got these points you could also measure the distance between them or do other calculations using the <strong>Inventor</strong> <strong>API</strong>.</p>
