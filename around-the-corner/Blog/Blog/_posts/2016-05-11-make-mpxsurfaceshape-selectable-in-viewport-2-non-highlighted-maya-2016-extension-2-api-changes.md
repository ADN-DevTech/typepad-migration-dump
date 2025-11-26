---
layout: "post"
title: "Make MPxSurfaceShape selectable in Viewport 2 - Non-highlighted Maya 2016 Extension 2 API Changes"
date: "2016-05-11 02:29:18"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2016/05/make-mpxsurfaceshape-selectable-in-viewport-2-non-highlighted-maya-2016-extension-2-api-changes.html "
typepad_basename: "make-mpxsurfaceshape-selectable-in-viewport-2-non-highlighted-maya-2016-extension-2-api-changes"
typepad_status: "Publish"
---

<p>In Maya 2016, several APIs were introduced but marked as <strong>Not implemented</strong> or not mandatory. Some of them are now fully implemented in Maya 2016 Extension 2, and actually now required in your plug-in code. The <strong>Completion of native Viewport 2.0 selection</strong> feature is one of them. If you wonder why Maya 2016 had some ‘Not implemented’ API but signature present, it was to help binary compatibility between Maya 2016 versions of the plug-ins using these API. However, as you probably figure out now, Maya 2016 Extension 2 is not binary compatible with the Maya 2016 family, but for other reasons.</p>
<p><a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/cpp_ref/class_m_px_surface_shape.html#acedd5e97c21aacfc5f976499438c7fa8" target="_blank">MPxSurfaceShape::getShapeSelectionMask</a> is one of the must in the Viewport 2.0 native selection API. To make your shape selectable in Viewport 2, you have to override it now.</p>
<p>For general meshes, the code below would be enough.</p>
<pre><code>MSelectionMask apiMesh::getShapeSelectionMask() const
{
    MSelectionMask::SelectionType selType = MSelectionMask::kSelectMeshes;
    return MSelectionMask( selType );
}
</code></pre>
<p>Please update your custom shapes if you want to make it selectable in Maya 2016 Extension 2. The <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__files_GUID_31E929F2_56E0_4CB8_9984_123AD3A85670_htm" target="_blank">Porting Selection from Viewport 1 to 2</a> guide has been updated for the latest Maya, please check it out.</p>
