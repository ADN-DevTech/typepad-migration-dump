---
layout: "post"
title: "Rotate drawing view around X axis of sheet"
date: "2014-11-18 16:21:13"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/rotate-drawing-view-around-x-axis-of-sheet.html "
typepad_basename: "rotate-drawing-view-around-x-axis-of-sheet"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>In order to rotate a drawing view, you need to modify the paraneters of its&#0160;<strong>Camera</strong>: <strong>UpVector</strong> (shown in below picture), the <strong>Eye</strong> (from where you are looking at the model) and the <strong>Target</strong> (the point in the model you are looking at)</p>
<p>If you want to rotate the view around the X axis of the sheet, so that if the drawing view currently shows <strong>Front&#0160;</strong>then it will switch to <strong>Top</strong>, then you can just swap the <strong>UpVector</strong> for the current view direction (i.e. <strong>Eye</strong> to <strong>Target</strong> direction) and calculate the new <strong>Eye</strong> position. The <strong>Target</strong> position remains the same.</p>
<pre>Sub RotateBaseView()
  Dim baseView As DrawingView
  Set baseView = ThisApplication.ActiveDocument.SelectSet(1)
   
  &#39; Rotate by setting the UpVector
  &#39; as the current view direction
  &#39; (i.e. Eye to Target) and then
  &#39; calculate the new eye
  &#39; which should be in the same
  &#39; direction as the old UpVector
  
  Dim c As Camera
  Set c = baseView.Camera
  
  Dim oldEye As Point
  Set oldEye = c.eye.Copy
  
  Dim oldTarget As Point
  Set oldTarget = c.Target.Copy
  
  Dim oldViewDir As Vector
  Set oldViewDir = oldEye.VectorTo(oldTarget)
  
  Dim viewDist As Double
  viewDist = oldViewDir.Length
  
  Dim newTargetToEye As Vector
  Set newTargetToEye = c.UpVector.AsVector().Copy
  Call newTargetToEye.ScaleBy(viewDist)
  
  Call oldTarget.TranslateBy(newTargetToEye)

  c.eye = oldTarget
  c.UpVector = oldViewDir.AsUnitVector()
  
  Call c.ApplyWithoutTransition
End Sub</pre>
<p>And this is what you get when running the above VBA code:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d093239c970c-pi" style="display: inline;"><img alt="Rotateview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d093239c970c image-full img-responsive" src="/assets/image_3cdc6e.jpg" title="Rotateview" /></a></p>
<p>&#0160;</p>
