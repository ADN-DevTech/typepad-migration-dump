---
layout: "post"
title: "Drawing coordinate systems and camera"
date: "2014-11-24 13:07:30"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/drawing-coordinate-systems-and-camera.html "
typepad_basename: "drawing-coordinate-systems-and-camera"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There are 3 coordinate systems being used inside a drawing: <strong>Sheet</strong>, <strong>DrawingView</strong> and <strong>Model</strong>.</p>
<p>The <strong>Sheet</strong> and <strong>DrawingView</strong> coordinate systems are almost identical apart from their origin: they have the same axes and same scaling. The <strong>Model</strong> coordinate system is the coordinate system of the document that is being shown inside the drawing view.</p>
<p>The API makes it really easy to jump between the coordinate systems. There are functions like&#0160;<strong>ModelToSheetSpace()</strong>, to transform a point from one coordinate system to the other. If you want to transform a direction then it&#39;s easier to use the transformation matrix, which is also provided, e.g.&#0160;<strong>ModelToSheetTransform</strong> matrix, and use that to transform a vector using&#0160;<strong>Vector.TransformBy(Matrix)</strong>.</p>
<p>We can write a simple function which transforms the origin of the various coordinate systems and the model workpoint into sheet coordinate system, and draws a circle at each position. This might make it easier to see the relationship between them.</p>
<pre>Sub DrawCircle(pt As Point2d, s As Sheet)
  Dim sk As DrawingSketch
  
  &#39; Sheet sketches are in Sheet coordinate system
  &#39; and DrawingView sketches are in DrawingView
  &#39; coordinate system
  If s.Sketches.count &gt; 0 Then
    Set sk = s.Sketches(1)
  Else
    Set sk = s.Sketches.Add()
  End If
  
  If Not s.Parent.ActivatedObject Is sk Then
    sk.Edit
  End If
  
  Call sk.SketchCircles.AddByCenterRadius(pt, 1)
End Sub

Sub DrawCircles()
  Dim doc As DrawingDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim s As Sheet
  Set s = doc.ActiveSheet
  
  Dim tg As TransientGeometry
  Set tg = ThisApplication.TransientGeometry
  
  Dim v As DrawingView
  Set v = s.DrawingViews(1)
  
  &#39; (1) Show the origin of the sheet
  Call DrawCircle(tg.CreatePoint2d(), s)
  
  &#39; (2) Show origin of the DrawingView
  Call DrawCircle(v.DrawingViewToSheetSpace(tg.CreatePoint2d()), s)
  &#39; Same as
  &#39;Call DrawCircle(v.Position, s)
  
  &#39; (3) Show origin of Model coordinate system
  Call DrawCircle(v.ModelToSheetSpace(tg.CreatePoint()), s)
  
  &#39; (4) Show WorkPoint of model
  &#39; transformed to Sheet coordinate system
  Dim prt As PartDocument
  Set prt = v.ReferencedDocumentDescriptor.ReferencedDocument
  
  Dim wp As WorkPoint
  Set wp = prt.ComponentDefinition.WorkPoints(&quot;MyPoint&quot;)
  
  Call DrawCircle(v.ModelToSheetSpace(wp.Point), s)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b3b6a0970d-pi" style="display: inline;"><img alt="View1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07b3b6a0970d img-responsive" src="/assets/image_27ce00.jpg" title="View1" /></a></p>
<p>There is also the <strong>Camera</strong> of the drawing view which is described with <strong>Model</strong> coordinates. In our case the <strong>Target</strong> is the middle of the box and <strong>Eye</strong>&#0160;(the position of our eye looking at the model, described with <strong>Model</strong> coordinates) is in front of it along the (1, 1, 1) vector. Though the <strong>UpVector</strong> looks as if it&#39;s the same as the&#0160;<strong>Model</strong>&#39;s&#0160;<strong>Y</strong>&#0160;axis, it points behind&#0160;that axis and equals to (-1, 2, -1). The <strong>view direction</strong>, which points from the <strong>Eye</strong> to the <strong>Target</strong>,&#0160;and the <strong>UpVector</strong> are always <strong>perpendicular</strong>.&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b3b9d1970d-pi" style="display: inline;"><img alt="View2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07b3b9d1970d img-responsive" src="/assets/image_ad563d.jpg" title="View2" /></a></p>
<p>Here is a nice post on Cameras:&#0160;<a href="http://modthemachine.typepad.com/my_weblog/2013/09/working-with-cameras-part-1.html" target="_self" title="">http://modthemachine.typepad.com/my_weblog/2013/09/working-with-cameras-part-1.html</a></p>
