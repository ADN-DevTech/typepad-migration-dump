---
layout: "post"
title: "Enable component selection for MPxSubsceneOverride of apiMeshShape sample"
date: "2016-07-27 19:31:37"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2016/07/enable-component-selection-for-mpxsubsceneoverride-of-apimeshshape-sample.html "
typepad_basename: "enable-component-selection-for-mpxsubsceneoverride-of-apimeshshape-sample"
typepad_status: "Publish"
---

<p>In Maya 2016 Extention 2, Autodesk has completed the native Viewport 2 implementation, and it is now the main viewport. There are many new APIs introduced and I've already posted a blog about <a href="http://around-the-corner.typepad.com/adn/2016/05/make-mpxsurfaceshape-selectable-in-viewport-2-non-highlighted-maya-2016-extension-2-api-changes.html" target="_blank">Make MPxSurfaceShape selectable in Viewport 2</a> that was a different change in Maya 2016 Extension 2. Today, I am going to talk about another change for the MPxSubsceneOverride.</p>  <p><a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/cpp_ref/class_m_h_w_render_1_1_m_px_sub_scene_override.html#a3d9b68da81d724902522fe2bf5f809c5" target="_blank">MPxSubsceneOverride::getInstancedSelectionPath</a> and <a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/cpp_ref/class_m_h_w_render_1_1_m_px_sub_scene_override.html#af3b88498daedce6cd2e55103c58160a4" target="_blank">MPxSubsceneOverride::getSelectionPath</a> are very important for the native Viewport 2 selection, especially for the component selection. When selecting with the Marquee tool in Maya, it will check the **hilite **object and try to intersect components with the renderItems. Once Maya has the component list from <a href="http://help.autodesk.com/view/MAYAUL/2016/ENU/?guid=__cpp_ref_class_m_h_w_render_1_1_m_px_component_converter_html" target="_blank">MPxComponentConverter</a>, it will merge the component list from MPxComponentConverter to the object's active component selection list.</p>  <p>For MPxSubsceneOverride, because the sample could be instancing a shape with multiple transforms, Maya will try to call getInstancedSelectionPath and getSelectionPath to find a proper dagPath of the component renderItem. When it cannot be done, Maya will keep using the hilite object path (e.g. transform in this case). Thus, MPxSurfaceShape::hasActiveComponents will not return true and there is no activeComponents info for the shape if it returns false by default.</p>  <p>So, if you are trying to select vertex with MPxSubsceneOverride of apiMeshShape, the component info is actually stored on the transform1 instead apiMeshShape1. To correct this, you should implement MPxSubsceneOverride::getInstancedSelectionPath like this:</p>  <pre><code>bool apiMeshSubSceneOverride::getInstancedSelectionPath(const MHWRender::MRenderItem&amp; renderItem, 
                                                        const MHWRender::MIntersection&amp; intersection, 
                                                        MDagPath&amp; dagPath) const
{
    // Return shape path for the component selection
    if(intersection.selectionLevel() == MHWRender::MSelectionContext::kComponent)
    {
        MFnDagNode dagNode(fObject);    
        dagNode.getPath(dagPath);
        return true;
    }

    // Use transform path by default.
    return false;
}
</code></pre>

<p>This will store the component's selection in the shape and let <a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/cpp_ref/class_m_px_surface_shape.html#a168e1b383c958be627e8935a4ffdd7cb" target="_blank">MPxSurfaceShape::hasActiveComponents</a> and <a href="http://help.autodesk.com/cloudhelp/2016/ENU/Maya-SDK/cpp_ref/class_m_px_surface_shape.html#a5d0a5e76d641c2bd825f8d5d8e0d386e" target="_blank">MPxSurfaceShape::activeComponents </a>of apiMeshShape return the value that the sample is expecting.</p>

<p>The updated apiMeshShape is available <a href="https://github.com/iamsleepy/apiMeshShape.ComponentSelection/tree/master" target="_blank">here</a>, so please check it out. For more usage reference, please see the gpuSubsceneOverride in our devkit.</p>
