---
layout: "post"
title: "Sketch extents"
date: "2014-06-23 06:46:17"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/06/sketch-extents.html "
typepad_basename: "sketch-extents"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Though the sketch object itself has no <strong>RangeBox</strong> that could be used to tell its extents, you can iterate through its entities and combine their extents into a single <strong>Box2d</strong> object that will provide that information:</p>
<pre>Sub GetSketchExtents()
  &#39; Select the sketch in the UI before running the code
  Dim oSketch As PlanarSketch
  Set oSketch = ThisApplication.ActiveDocument.SelectSet(1)
  
  Dim oRB As Box2d
  Set oRB = ThisApplication.TransientGeometry.CreateBox2d
  
  Dim oEnt As SketchEntity
  For Each oEnt In oSketch.SketchEntities
      Call oRB.Extend(oEnt.RangeBox.MinPoint)
      Call oRB.Extend(oEnt.RangeBox.MaxPoint)
  Next
  
  Call MsgBox( _
    &quot;(Value in internal length unit (cm))&quot; + vbCrLf + _
    &quot;Min = &quot; + _
    str(oRB.MinPoint.x) + _
    str(oRB.MinPoint.y) + vbCrLf + _
    &quot;Max = &quot; + _
    str(oRB.MaxPoint.x) + _
    str(oRB.MaxPoint.y))
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd23ba2b970b-pi" style="display: inline;"><img alt="Sketchextents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd23ba2b970b image-full img-responsive" src="/assets/image_cfcaa0.jpg" title="Sketchextents" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
