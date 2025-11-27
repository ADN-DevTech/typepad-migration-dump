---
layout: "post"
title: "Create dimension relative to other dimension"
date: "2014-07-22 03:58:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/07/create-dimension-relative-to-other-dimension.html "
typepad_basename: "create-dimension-relative-to-other-dimension"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When you want to use the value of a dimension from another dimension, then you actually need to use the <strong>Parameter</strong> of that dimension. If you click on a dimension which is referencing another dimension&#39;s value, then you can see that both dimensions have an associated parameter, and the equation is based on those. Both dimensions are just simple dimensions based on a <strong>Parameter</strong>: in this case one is based on <strong>d5</strong> and the other on <strong>d6</strong>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd36de20970b-pi" style="display: inline;"><img alt="Dimensions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd36de20970b image-full img-responsive" src="/assets/image_64ddeb.jpg" title="Dimensions" /></a></p>
<p>If I wanted to recreate the above highlighted dimension then I could do it like this in <strong>VBA</strong>. Before running the code you need to go to the <strong>Sketch</strong> environment in the <strong>Part</strong> document, then <strong>select the dimension</strong> you want to reference from the new dimension and then <strong>shift-select the sketch line</strong> you want to dimension:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e68dd5970c-pi" style="display: inline;"><img alt="Dimensions2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511e68dd5970c image-full img-responsive" src="/assets/image_9720c0.jpg" title="Dimensions2" /></a></p>
<pre>Sub AddDimension()
  Dim pd As PartDocument
  Set pd = ThisApplication.ActiveDocument
  
  Dim dc As DimensionConstraint
  Set dc = pd.SelectSet(1)
  
  Dim sl As SketchLine
  Set sl = pd.SelectSet(2)
  
  Dim sk As PlanarSketch
  Set sk = sl.Parent
  
  Dim tg As TransientGeometry
  Set tg = ThisApplication.TransientGeometry
  
  Dim pt As Point2d
  Set pt = tg.CreatePoint2d( _
    (sl.StartSketchPoint.Geometry.x + _
      sl.EndSketchPoint.Geometry.x) / 2, _
    sl.StartSketchPoint.Geometry.y + 0.5)
    
  Dim dc2 As TwoPointDistanceDimConstraint
  Set dc2 = sk.DimensionConstraints.AddTwoPointDistance( _
    sl.StartSketchPoint, _
    sl.EndSketchPoint, _
    kAlignedDim, _
    pt)

  dc2.Parameter.Expression = dc.Parameter.name + &quot; * 2 ul&quot;

  &#39; If the sketch is consumed by a feature
  &#39; this will get the lines updated
  Call sk.Solve
End Sub</pre>
<p>&#0160;</p>
