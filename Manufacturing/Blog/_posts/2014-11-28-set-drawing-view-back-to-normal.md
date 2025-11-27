---
layout: "post"
title: "Set drawing's view back to normal"
date: "2014-11-28 13:05:03"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/11/set-drawing-view-back-to-normal.html "
typepad_basename: "set-drawing-view-back-to-normal"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Just like the <strong>Camera</strong> of the <strong>View</strong> of a part or an assembly, a drawing view's camera can be modified as well. However, this is not possible through the <strong>UI</strong>.&nbsp;Using an ISO view can give an insight into the iternals of <strong>Inventor</strong>: what seems to be just a bunch of 2d lines are actually 3d:</p>
<pre>Sub DrawingIsoView()
  Dim v As View
  Set v = ThisApplication.ActiveView
  
  Dim c As Camera
  Set c = v.Camera
  
  c.ViewOrientationType = kIsoTopRightViewOrientation
  c.ApplyWithoutTransition
End Sub</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b788cd970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07b788cd970d img-responsive" title="Drawingview" src="/assets/image_652719.jpg" alt="Drawingview" border="0" /></a></p>
<p>If you ended up with such a view by accident - it happened to someone somehow - then you can easily set things back to normal:</p>
<pre>Sub DrawingNormalView()
  Dim v As View
  Set v = ThisApplication.ActiveView
  
  Dim c As Camera
  Set c = v.Camera
  
  c.ViewOrientationType = kFrontViewOrientation
  c.ApplyWithoutTransition
End Sub</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b78918970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07b78918970d image-full img-responsive" title="Drawingview2" src="/assets/image_32cf3a.jpg" alt="Drawingview2" border="0" /></a></p>
<p>More info on <strong>Camera</strong> manipulation: <a title="" href="http://modthemachine.typepad.com/my_weblog/2013/09/working-with-cameras-part-1.html" target="_self">http://modthemachine.typepad.com/my_weblog/2013/09/working-with-cameras-part-1.html</a></p>
