---
layout: "post"
title: "Rotate assembly component around axis"
date: "2015-09-25 08:15:49"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/09/rotate-assembly-component-around-axis.html "
typepad_basename: "rotate-assembly-component-around-axis"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>There is already a blog post on <a href="http://adndevblog.typepad.com/manufacturing/2014/11/rotate-camera-around-any-axis-any-angle.html" target="_self">rotating inside a drawing</a>, now we&#39;ll do the same with an assembly component / component occurrence.</p>
<p>Once you have the matrix of the rotation and the current transformation of the component, you just have to combine them and assign that to the component.</p>
<p>Just select the axis you want to use and the component you want to rotate by 90 degrees, then run the <strong>VBA</strong> code:</p>
<pre>Sub RotateAroundAxis()
  Const PI = 3.14159265358979
  
  Dim d As AssemblyDocument
  Set d = ThisApplication.ActiveDocument
  
  Dim a As WorkAxis
  Set a = d.SelectSet(1)
  
  Dim o As ComponentOccurrence
  Set o = d.SelectSet(2)
  
  Dim t As TransientGeometry
  Set t = ThisApplication.TransientGeometry
    
  Dim l As Line
  Set l = a.Line
  
  Dim mRot As Matrix
  Set mRot = t.CreateMatrix()
  &#39; PI / 2 =&gt; 90 deg
  Call mRot.SetToRotation(PI / 2, l.Direction.AsVector(), l.RootPoint)
  
  Dim m As Matrix
  Set m = o.Transformation
  Call m.PreMultiplyBy(mRot)
  
  o.Transformation = m
End Sub</pre>
<p>Result:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d3b9e2970b-pi" style="display: inline;"><img alt="Rotate_component" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d3b9e2970b image-full img-responsive" src="/assets/image_803bf6.jpg" title="Rotate_component" /></a></p>
<p>&#0160;</p>
