---
layout: "post"
title: "Set Drawing View Width"
date: "2014-11-27 14:42:51"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/set-drawing-view-width.html "
typepad_basename: "set-drawing-view-width"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>You cannot set the <strong>Width</strong> or <strong>Height</strong> of a <strong>DrawingView</strong> directly. However, you can calculate the <strong>Scale</strong> necessary to achieve the width or height you need. Here is a <strong>VBA</strong> code demonstrating it. First you need to select the drawing view in question through the UI then run the code:</p>
<pre>Sub SetDrawingViewWidth()
  Dim dv As DrawingView
  Set dv = ThisApplication.ActiveDocument.SelectSet(1)
  
  &#39; Current width
  Dim cw As Double
  cw = dv.Width
  
  &#39; New width we want
  &#39; Set in &#39;cm&#39; (internal length unit)
  Dim nw As Double
  nw = 10
  
  &#39; New scale
  Dim ns As Double
  ns = nw / cw * dv.Scale
  
  dv.[Scale] = ns
  
  Call MsgBox(&quot;Width = &quot; + Str(dv.Width))
End Sub</pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d09bbc40970c-pi" style="display: inline;"><img alt="Viewwidth" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d09bbc40970c image-full img-responsive" src="/assets/image_411770.jpg" title="Viewwidth" /></a></p>
<p>&#0160;</p>
