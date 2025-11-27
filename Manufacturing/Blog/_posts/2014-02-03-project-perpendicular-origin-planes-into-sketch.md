---
layout: "post"
title: "Project perpendicular origin planes into sketch"
date: "2014-02-03 14:57:56"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/02/project-perpendicular-origin-planes-into-sketch.html "
typepad_basename: "project-perpendicular-origin-planes-into-sketch"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Projecting entities into a sketch is really easy through the API. <br />E.g. projecting the origin planes which are perpendicular to the sketch can be done like this in VBA:</p>
<pre>Public Sub ProjectPerpendicularOriginPlanes()
  Dim doc As Document
  Set doc = ThisApplication.ActiveDocument
  
  If Not TypeOf doc Is PartDocument Then
    Call MsgBox(&quot;You need to be inside a part document&quot;)
    Exit Sub
  End If
    
  Dim ao As PlanarSketch
  Set ao = ThisApplication.ActiveEditObject
  
  If Not TypeOf ao Is PlanarSketch Then
    Call MsgBox(&quot;You need to be inside a sketch&quot;)
    Exit Sub
  End If
  
  Dim pd As PartDocument
  Set pd = doc
  
  Dim cd As PartComponentDefinition
  Set cd = pd.ComponentDefinition
  
  Dim sk As PlanarSketch
  Set sk = ao
 
  &#39; The origin planes are the first 3
  &#39; in the WorkPlanes collection
  Dim i As Integer
  For i = 1 To 3
    Dim wp As WorkPlane
    Set wp = cd.WorkPlanes(i)
      
    &#39; If the WorkPlane was already added
    &#39; then AddByProjectingEntity would throw
    &#39; an error.
    &#39; To avoid that we can do error handling:
    On Error Resume Next
    
    If wp.Plane.IsPerpendicularTo(sk.PlanarEntityGeometry) Then
      &#39; Checking if the workplane is perpendicular might
      &#39; be an overkill because if not, then the below
      &#39; function would throw an error.
      &#39; But I think it&#39;s nicer if we check :)
      Call sk.AddByProjectingEntity(wp)
    End If
    
    On Error GoTo 0
  Next i
End Sub</pre>
<p>&#0160;</p>
