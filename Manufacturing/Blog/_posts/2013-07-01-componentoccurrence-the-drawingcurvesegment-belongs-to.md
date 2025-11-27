---
layout: "post"
title: "ComponentOccurrence the DrawingCurveSegment belongs to"
date: "2013-07-01 06:33:48"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/componentoccurrence-the-drawingcurvesegment-belongs-to.html "
typepad_basename: "componentoccurrence-the-drawingcurvesegment-belongs-to"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you want to find out which ComponentOccurrence of the assembly a given DrawingCurve/DrawingCurveSegment belongs to then you can find it like so:</p>
<pre>Sub GetOccurrenceFromDrawingCurveSegment()
  &#39; First select a drawing curve segment in
  &#39; one of the views that is showing an assembly
  &#39; If the curve is related to geometry
  &#39; inside a subcomponent then that suboccurrence
  &#39; in the assembly will be retrieved
  Dim dcs As DrawingCurveSegment
  Set dcs = ThisApplication.ActiveDocument.SelectSet(1)
    
  Dim ep As EdgeProxy
  Set ep = dcs.Parent.ModelGeometry
    
  Dim co As ComponentOccurrence
  Set co = ep.ContainingOccurrence
    
  MsgBox (co.Name)
End Sub</pre>
