---
layout: "post"
title: "Get image from M3dView::readColorBuffer in Viewport 2"
date: "2016-05-05 22:36:06"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2016/05/get-image-from-m3dviewreadcolorbuffer-in-viewport-2.html "
typepad_basename: "get-image-from-m3dviewreadcolorbuffer-in-viewport-2"
typepad_status: "Publish"
---

<p>Although the API is marked as obsolete, it is the easiest way to get a screenshot from Viewport in Maya and many plug-ins are still using it.</p>
<p>But if you are trying to get a MImage from Viewport 2, you may have some problem with your previous Python code like this.</p>
<pre class="brush: python; toolbar: false">import maya.OpenMaya as openmaya
import maya.OpenMayaUI as openmayaUI

view = openmayaUI.M3dView.active3dView()
img = openmaya.MImage()
view.readColorBuffer(img)
</pre>
<p>This code will not work because in Viewport 2, the image is stored as float and the default format of MImage is BGRA byte.</p>
<p>The solution is simple, make MImage a float image. Replace the last line above with following code snippet.</p>
<pre class="brush: python; toolbar: false">if view.getRendererName() == view.kViewport2Renderer:       
    img .create(view.portWidth(), view.portHeight(), 4, openmaya.MImage.kFloat)
    view.readColorBuffer(img)
    img.convertPixelFormat(openmaya.MImage.kByte)
else:
    view.readColorBuffer(img)
</pre>
<p>You will get a BGRA byte image in the end like the old time:)</p>
