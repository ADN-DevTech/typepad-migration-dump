---
layout: "post"
title: "Change equation curve values"
date: "2014-07-01 02:13:28"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/change-equation-curve-values.html "
typepad_basename: "change-equation-curve-values"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>The equation curve values are fully accessible from the API and can be changed as well. One thing that can be difficult to spot, unless you read the API Help Reference carefully, is that the parameters for <strong>GetEquation</strong> and <strong>SetEquation</strong> are not the same. So if you try to use the same parameters then <strong>SetEquation</strong> will fail.</p>
<p>The difference is in the <strong>MinValue</strong> and <strong>MaxValue</strong>. In <strong>GetEquation</strong> they are <strong>Parameter</strong>, but in <strong>SetEquation</strong> they are a <strong>Variant</strong> which should either be a <strong>numeric value or a string</strong>.</p>
<p>This is how I can change the X value from &quot;136*t&quot; to &quot;136+13.325*t^2&quot;&#0160;</p>
<pre>Sub ModifyEquationCurve()
  Dim doc As PartDocument
  Set doc = ThisApplication.ActiveDocument
  
  Dim s3d As Sketch3D
  Set s3d = doc.ComponentDefinition.Sketches3D(1)
  
  Dim sec As SketchEquationCurve3D
  Set sec = s3d.SketchEquationCurves3D(1)
  
  Call s3d.Edit
  
  Dim x As String, y As String, z As String
  Dim cst As CoordinateSystemTypeEnum
  Dim min As Parameter, max As Parameter
  Dim mins As String, maxs As String
  Call sec.GetEquation(cst, x, y, z, min, max)
  
  mins = min.Expression
  maxs = max.Expression
  x = &quot;136+13.625*t^2&quot;
  Call sec.SetEquation(cst, x, y, z, mins, maxs)
  
  Call s3d.ExitEdit
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73de3ef3b970d-pi" style="display: inline;"><img alt="Equationcurve" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73de3ef3b970d image-full img-responsive" src="/assets/image_5c72ab.jpg" title="Equationcurve" /></a><br /><br /></p>
<p>&#0160;</p>
