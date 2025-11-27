---
layout: "post"
title: "Rotate sketch entities"
date: "2016-05-18 07:10:59"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/05/rotate-sketch-entities.html "
typepad_basename: "rotate-sketch-entities"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>We have a function on the <strong>PlanarSketch</strong> object called <strong>RotateSketchObjects</strong> which takes a collection of <strong>SketchEntity</strong>&#39;s and rotates them around a given point. It seems that if I also pass in all the entities including the <strong>sketch points</strong> that the sketch curves&#0160;depend on then it does not work, but if I omit the points and only pass in the <strong>sketch curves</strong>, that works fine:&#0160;</p>
<pre>Sub RotateSketchEntities()
  Const PI = 3.14159265358979

  &#39; Make sure that the Sketch is active in the UI
  &#39; before running the code
  Dim sk As PlanarSketch
  Set sk = ThisApplication.ActiveEditObject
  
  Dim tro As TransientObjects
  Set tro = ThisApplication.TransientObjects
  
  Dim oc As ObjectCollection
  Set oc = tro.CreateObjectCollection
  
  Dim pt As SketchEntity
  For Each pt In sk.SketchEntities
    &#39; Omit points
    If Not TypeOf pt Is SketchPoint Then
      Call oc.Add(pt)
    End If
  Next
  
  Dim trg As TransientGeometry
  Set trg = ThisApplication.TransientGeometry
  
  &#39; Rotate them 90 degrees around origin point
  Call sk.RotateSketchObjects( _
    oc, trg.CreatePoint2d(), PI / 2#)
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09022a65970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Rotatesketch" class="asset  asset-image at-xid-6a0167607c2431970b01bb09022a65970d img-responsive" src="/assets/image_f17267.jpg" title="Rotatesketch" /></a></p>
<p>&#0160;</p>
