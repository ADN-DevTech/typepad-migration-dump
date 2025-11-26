---
layout: "post"
title: "Light Draw Override in Viewport 2.0"
date: "2016-10-20 01:51:12"
author: "Vijaya Prakash"
categories:
  - "C++"
  - "Custom Nodes"
  - "Geometry"
  - "Maya"
  - "Plug-in"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/10/light-draw-override-in-viewport-20.html "
typepad_basename: "light-draw-override-in-viewport-20"
typepad_status: "Publish"
---

<p>If you use <strong><em>MPxDrawOverride</em></strong> class to override the draw of your custom locator class, you will have to register the draw override as below:</p>
<pre class="brush: cpp;toolbar: false;">static MString drawdb("drawdb/geometry/light/customLight");
static MString sDrawRegistrantId("customLightDraw");
plugin.registerNode("customLight",
customLightNode::m_id,
customLightNode::creator,
customLightNode::initialize,
MPxNode::kLocatorNode,
&amp;drawdb);
MHWRender::MDrawRegistry::registerDrawOverrideCreator(drawdb,
sDrawRegistrantId,
MCustomLightDrawOverride::Creator);
</pre>
<p>However, if you want your locator to be classified as light with the same <strong><em>drawdb</em></strong> as above, Maya won't treat your locator as light, because the above drawdb string is applicable only for geometry override not for light. The node will be treated as light only after changing the classification string to “light/customLight” without “geometry” (e.g. static MString drawdb("light/customLight");) However in this case you can’t receive any draw call back for Viewport 2.0 becasue you didn't include “geometry” in the drawdb string.</p>
<p>So, how to treat your node as light and make it receive the draw callback for Viewport 2.0?.</p>
<p>You should define your drawdb as below.</p>
<pre class="brush: cpp;toolbar: false;">static MString drawdb("light:drawdb/light/directionalLight:drawdb/geometry/light/directionalLightCustom");
</pre>
<p>This classification allows for the usage of the internal light and UI (geometry drawing) VP2 evaluators to be used. Note that there is no explicit VP2 override class used as the internal VP2 geometry evaluator will handle drawing. Attributes which control the light are created to match names on a Maya lights so that they will be picked up when DG evaluation occurs.</p>
<p>Please refer the “<a href="http://help.autodesk.com/view/MAYAUL/2017/ENU/?guid=__cpp_ref_api_directional_light_shape_2api_directional_light_shape_8cpp_example_html" target="_blank">apiDirectionalLightShape</a>” devkit sample for complete information.</p>
