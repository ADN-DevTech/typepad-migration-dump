---
layout: "post"
title: "Drawing and Selection tips in VP2.0"
date: "2016-09-27 20:18:04"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Maya"
  - "Qt"
  - "Rendering"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/09/drawing-and-selection-tips-in-vp20.html "
typepad_basename: "drawing-and-selection-tips-in-vp20"
typepad_status: "Publish"
---

<p>We have 2 options to draw the icon/geometry in VP2.0. The first one is using MUIDrawManager object, and another one is MRenderOverride. If you are developing a plugin using Maya node, both MUiDrawManager and MrenderOverride objects are available at any point in the implementation. One issue is that if you are trying to draw the geometry/icon in a callback like postRenderMsgCallback(), VP2.0 would already have finished the drawing at that time, and the MUIDrawManager object won’t be available. In this scenario, you can use the second option i.e, using MRenderOverride.</p>
<p>If you have already developed a plugin, instead of doing replacing the drawing code and rebuilding your plugin, there are a few other options to draw in VP2.0. These are not Maya objects, but it still may be a better solution than rewriting your plugin:</p>
<p><a href="http://www.gamedev.net/topic/559367-dx11-how-do-you-draw-a-triangle/" target="_blank">1. Use Dx11 API. ( Windows Only SDK)</a></p>
<p><a href="http://around-the-corner.typepad.com/adn/2016/09/how-to-build-customized-qt-561-for-maya-2017-on-windows.html" target="_blank">2. Use Qt C++ API ( Platform Independent SDK)</a></p>
<p>In case of selection in VP2.0, Autodesk Maya moved to hardware selection in Maya 2016 Extension 2. Please check <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_31E929F2_56E0_4CB8_9984_123AD3A85670_htm" target="_blank">this</a> link for porting selection from VP1 to VP2. If you still wants to use the VP1 selection, that is still possible. You can use MAYA_<em>VP2_</em>USE_<em>VP1_</em>SELECTION=1 environment variable (make sure that you are in VP1 mode or VP2 non-core profile mode).</p>
