---
layout: "post"
title: "Align DrawingView to Face and Edge "
date: "2015-02-05 16:26:32"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/02/align-drawingview-to-face-and-edge.html "
typepad_basename: "align-drawingview-to-face-and-edge"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Wrote a few articles on drawing views already, but they do not seem to be enough :)</p>
<p><a href="http://adndevblog.typepad.com/manufacturing/2014/11/rotate-drawing-view-around-x-axis-of-sheet.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/11/rotate-drawing-view-around-x-axis-of-sheet.html</a>&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2014/11/rotate-camera-around-any-axis-any-angle.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/11/rotate-camera-around-any-axis-any-angle.html</a>&#0160;<br /><a href="http://adndevblog.typepad.com/manufacturing/2014/11/drawing-coordinate-systems-and-camera.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2014/11/drawing-coordinate-systems-and-camera.html</a>&#0160;</p>
<p>So, now here is one on aligning the view to show a given face. To make this work directly in a drawing, where only edges can be selected, we&#39;ll let the user select two drawing curve segments. From those we&#39;ll find the corresponding model edges, and then we&#39;ll find the common face of those edges. Using the <strong>Face</strong>&#39;s normal we&#39;ll set the camera&#39;s view direction, and using the second selected <strong>Edge</strong>&#39;s direction we&#39;ll set the <strong>UpVector</strong> of the view. <br />Note that the below code only works with planar faces and linear edges, and the direction of the edge used to set the&#0160;<strong>UpVector</strong> may be opposite to what you&#39;d like in which case the view will be upside-down.</p>
<pre>Function GetFace(edges() As Edge) As Face
  Dim i As Integer
  Dim j As Integer
  For i = 1 To 2
    For j = 1 To 2
      If edges(0).Faces(i) Is edges(1).Faces(j) Then
        Set GetFace = edges(0).Faces(i)
        Exit Function
      End If
    Next
  Next
End Function

Sub SetViewDirection()
  Dim doc As DrawingDocument
  Set doc = ThisApplication.ActiveDocument

  Dim edges(1) As Edge
  Set edges(0) = doc.SelectSet(1).Parent.ModelGeometry
  Set edges(1) = doc.SelectSet(2).Parent.ModelGeometry
  
  Dim f As Face
  Set f = GetFace(edges)
  
  Dim p As Plane
  Set p = f.Geometry
  
  Dim dv As DrawingView
  Set dv = doc.SelectSet(1).Parent.Parent
  
  Dim c As Camera
  Set c = dv.Camera
    
  &#39; Set Eye or Target
  Dim pt As Point
  If f.IsParamReversed Then
    Set pt = c.Eye.Copy
    Call pt.TranslateBy(p.Normal.AsVector())
    c.Target = pt
  Else
    Set pt = c.Target.Copy
    Call pt.TranslateBy(p.Normal.AsVector())
    c.Eye = pt
  End If
  
  &#39; Set UpVector
  Dim ls As LineSegment
  Set ls = edges(1).Geometry
  c.UpVector = ls.Direction
  
  Call c.ApplyWithoutTransition
End Sub</pre>
<p>Here is the result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d0e282970c-pi" style="display: inline;"><img alt="Drawingview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d0e282970c image-full img-responsive" src="/assets/image_f93494.jpg" title="Drawingview" /></a><br />&#0160;</p>
<p>&#0160;</p>
