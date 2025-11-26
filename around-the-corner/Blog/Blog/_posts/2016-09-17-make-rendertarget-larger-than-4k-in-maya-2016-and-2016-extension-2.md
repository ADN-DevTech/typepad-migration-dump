---
layout: "post"
title: "Make RenderTarget larger than 4k in Maya 2016 and 2016 Extension 2"
date: "2016-09-17 23:31:30"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2016/09/make-rendertarget-larger-than-4k-in-maya-2016-and-2016-extension-2.html "
typepad_basename: "make-rendertarget-larger-than-4k-in-maya-2016-and-2016-extension-2"
typepad_status: "Publish"
---

<p>There is a restriction for Maya 2016 that doesn&#39;t allow Maya to use more than 4k textures. As a result, the size of Viewport 2 MRenderTarget is limited to 4096x4096.</p>
<p>We&#39;ve opened a back door to remove this restriction and allow users to use the maximum texture size supported by their graphics card. This is now possible due to a new environment value introduced in Maya 2016 SP6 and 2016 Extension 2 + SP1. This variable is named: MAYA_VP2_USE_GPU_MAX_TARGET_SIZE, you can set its value to 1. After setting it in your environment, Maya will use the max texture size of your graphic card instead of 4096x4096.<br />You can find the max texture size in Maya output window:</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01b7c8944fc2970b-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d01b7c8944fc2970b img-responsive" src="/assets/image_fe7b65.jpg" title="Image" /></a></p>
<p><br />In Maya 2017, we&#39;ve removed this restriction and the environment variable is no longer required to unleash the full potential of your graphic card. Enjoy:)</p>
