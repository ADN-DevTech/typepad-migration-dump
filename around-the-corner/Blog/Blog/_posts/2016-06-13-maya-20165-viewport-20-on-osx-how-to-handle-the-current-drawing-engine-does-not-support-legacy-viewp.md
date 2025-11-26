---
layout: "post"
title: "Maya 2016.5 Viewport 2.0 on OSX : How to handle the \"Current drawing engine does not support legacy viewport\" warning"
date: "2016-06-13 01:51:21"
author: "Vijaya Prakash"
categories:
  - "Mac"
  - "OpenGL"
  - "Rendering"
  - "Vijay Prakash"
original_url: "https://around-the-corner.typepad.com/adn/2016/06/maya-20165-viewport-20-on-osx-how-to-handle-the-current-drawing-engine-does-not-support-legacy-viewp.html "
typepad_basename: "maya-20165-viewport-20-on-osx-how-to-handle-the-current-drawing-engine-does-not-support-legacy-viewp"
typepad_status: "Publish"
---

<p>In Maya 2016 Extension 2 (2016.5), you may have noticed that on Mac OS X, Maya would give the following warning message: &quot;Current drawing engine does not support legacy viewport&quot;. Maya 2016.5 uses OpenGL API version 4.1( please see the image below). The “OpenGL Core Profile (strict)” is the default rendering engine on Mac OS X which supports OpenGL version 3.2 and above. So, it won’t support OpenGL 2.1 and below features. If you still want to use OpenGL 2.1 and below features you have to change the Viewport settings to use “OpenGL Legacy” mode. To change this, go to Maya -&gt;Windows -&gt; Settings/Preferences -&gt; Preferences -&gt; Display -&gt;Viewport 2.0.<br /> <br /> If you have developed your own viewport for Maya, make sure that you are using latest OpenGL profile, or you can change the settings in Maya GUI to use the Legacy one.<br /> <br /> You can also use an environment variable to enable the Legacy viewport. i.e, MAYA_ENABLE_LEGACY_VIEWPORT=1 and make sure that you have enabled OPENGL_LEGACY environment variable as well.<br /> <a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb09106272970d-pi" style="display: inline;"><img alt="Vp2_legacy" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01bb09106272970d image-full img-responsive" src="/assets/image_0f4723.jpg" title="Vp2_legacy" /></a><br /><br /></p>
