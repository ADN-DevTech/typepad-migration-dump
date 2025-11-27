---
layout: "post"
title: "Rotate camera around any axis any angle"
date: "2014-11-20 11:10:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/rotate-camera-around-any-axis-any-angle.html "
typepad_basename: "rotate-camera-around-any-axis-any-angle"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>This is a generalization of this post:&nbsp;<br /><a title="" href="http://adndevblog.typepad.com/manufacturing/2014/11/rotate-drawing-view-around-x-axis-of-sheet.html" target="_self">http://adndevblog.typepad.com/manufacturing/2014/11/rotate-drawing-view-around-x-axis-of-sheet.html</a></p>
<p>Here we'll use the <strong>Matrix</strong> class to help us create a matrix that can transform any point or vector around any vector/axis and an angle: <strong>Matrix.SetToRotation</strong>(Angle As Double, Axis As Vector, Center As Point)&nbsp;</p>
<p>To make sure we have a precise radian value for <strong>90 degrees</strong> I'm using <strong>2 *&nbsp;Atn(1)</strong> inside <strong>VBA</strong>. In languages like <strong>.NET</strong> there is a constant for <strong>PI</strong>, so you could use that instead.</p>
<p>We just need to get the vector we want to rotate around inside the <strong>Model</strong> space. If the vector we want to use is in the <strong>Sheet</strong> space then we need to transform it using <strong>SheetToModelTransform</strong>&nbsp;matrix.&nbsp;</p>
<p>Once we have both the rotation centre (<strong>Camera.Target</strong> in our case) and the rotation axis (Sheet X inside the model space in our case) then we can create the transformation matrix and use it to get the new <strong>Eye</strong> position and <strong>UpVector</strong> direction.</p>
<pre>Sub RotateBaseView()
  Dim baseView As DrawingView
  Set baseView = ThisApplication.ActiveDocument.SelectSet(1)
  
  Dim tr As TransientGeometry
  Set tr = ThisApplication.TransientGeometry
  
  Dim c As Camera
  Set c = baseView.Camera
  
  Dim oldEye As Point
  Set oldEye = c.eye.Copy
  
  Dim oldTarget As Point
  Set oldTarget = c.Target.Copy
  
  Dim oldUpVector As UnitVector
  Set oldUpVector = c.UpVector.Copy
  
  ' Get the Sheet's X axis into the model space
  Dim sheetXInModel As Vector
  Set sheetXInModel = tr.CreateVector(1, 0, 0)
  Call sheetXInModel.TransformBy(baseView.SheetToModelTransform)
  
  ' Calculate rotation in the model
  ' around the Target and the Sheet X
  ' in model space
  Dim m As Matrix
  Set m = tr.CreateMatrix()
  
  ' Set the rotation angle to 90 degrees
  ' Atn(1) = 45 degrees in radian
  Dim Rad90 As Double
  Rad90 = 2 * Math.Atn(1)
  Call m.SetToRotation(Rad90, sheetXInModel, oldTarget)

  ' Now we just have to transform the points and
  ' vectors
  Call oldEye.TransformBy(m)
  c.eye = oldEye
  Call oldUpVector.TransformBy(m)
  c.UpVector = oldUpVector
  
  Call c.ApplyWithoutTransition
End Sub</pre>
