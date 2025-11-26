---
layout: "post"
title: "Advantage and limitation of using MUiDrawManager in VP 2.0"
date: "2015-05-10 13:12:00"
author: "Zhong Wu"
categories:
  - "Autodesk"
  - "Maya"
  - "OpenGL"
  - "Rendering"
  - "Zhong Wu"
original_url: "https://around-the-corner.typepad.com/adn/2015/05/advantage-and-limitation-of-using-muidrawmanager-in-vp-20.html "
typepad_basename: "advantage-and-limitation-of-using-muidrawmanager-in-vp-20"
typepad_status: "Publish"
---

<p>VP 2.0 provides a very convenient way for plugin developers to draw some simple basic UI elements such as lines, circles, arcs, text, …. In Maya 2016, it now provides the ability to support drawing icons. By using MUiDrawManager, plugin developers do not need to consider the different GPU device, it will work for all.</p>
<p>MUiDrawManager is very powerful, some developers mention that they are trying to use that as much as possible. Actually, we provide several interfaces for partners to use MUiDrawManager in different context which includes:</p>
<ul>
<li>MPxDrawOverride</li>
<li>MPxGeometryOverride</li>
<li>MPxManipContainer</li>
<li>MPxManipulatorNode</li>
<li>MPxContext</li>
<li>MUserRenderOperation/MHUDRender/MSceneRender</li>
</ul>
<p>For more details about using MUiDrawManager with the above interfaces, please check out the Maya online SDK help <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_A9DE3226_0708_4FAA_9B97_C5FF6574688A_htm">here</a>. But before really using this class, there is something important you need to know first.</p>
<p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb082cee27970d-pi"><img alt="clip_image001[25]" border="0" height="186" src="/assets/image_ae78ae.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border-width: 0px;" title="clip_image001[25]" width="463" /></a></p>
<p>&#0160;</p>
<p>Unlike other Draw API which are used as immediate mode drawing, MUiDrawManager is different, it does not draw anything in the scene directly. Instead, this API will queue the draw operations in a special ‘retain’ mode. In another word, MUiDrawManager does not work with the GPU directly, you cannot access any GPU status while using MUiDrawManager. Also notice that MUiDrawManager only work with MFrameContext instead of MDrawContext. If anyone asks if they can draw some basic elements by using MUiDrawManager, and the draw operation needs to get information from the GPU, the answer is “<strong>No</strong>”.</p>
