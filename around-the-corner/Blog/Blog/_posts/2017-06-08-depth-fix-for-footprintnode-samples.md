---
layout: "post"
title: "Depth fix for footPrintNode samples"
date: "2017-06-08 19:46:42"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2017/06/depth-fix-for-footprintnode-samples.html "
typepad_basename: "depth-fix-for-footprintnode-samples"
typepad_status: "Publish"
---

<p>We received a report that footprint samples aren&#39;t working properly. It turns out that the depthPriority wasn&#39;t implemented correctly. <br /> <br /> The original samples are using fixed depth(5) and when the node is duplicated, the old one is still on top of it. We can determine a proper depth with <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-SDK/cpp_ref/class_m_h_w_render_1_1_m_geometry_utilities.html#af2f27b52377c28d25d76853a7a75785c">MGeometryUtilities::displayStatus.</a></p>
<pre id="IUXACAnyNK2">    unsigned int depthPriority;<br />    switch(MHWRender::MGeometryUtilities::displayStatus(path))<br />    {<br />        case MHWRender::kLead:<br />        case MHWRender::kActive:<br />        case MHWRender::kHilite:<br />        case MHWRender::kActiveComponent:<br />            depthPriority = MHWRender::MRenderItem::sActiveWireDepthPriority;<br />            break;<br />        default:<br />            depthPriority = MHWRender::MRenderItem::sDormantFilledDepthPriority;<br />            break;<br />    }</pre>
<p>You also have to replace the depth settings. For footPrintNode sample, we are going to set depth with <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-SDK/cpp_ref/class_m_h_w_render_1_1_m_u_i_draw_manager.html#ad19b05026d05d93c1ffca4914fe81fa6">MDrawManager::setDepthPriority</a>. For footPrintNode_GeometryOverride, it is using render items; so it is <a href="http://help.autodesk.com/cloudhelp/2017/ENU/Maya-SDK/cpp_ref/class_m_h_w_render_1_1_m_render_item.html#a6e865e2cf5c9d818b1cbb99af2360c47">MRenderItem::DepthPriority</a>.<br /> </p>
