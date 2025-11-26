---
layout: "post"
title: "How to remove the white outline in the RenderView"
date: "2016-05-24 23:24:26"
author: "Cheng Xi Li"
categories:
  - "C++"
  - "Cheng Xi Li"
  - "Maya"
  - "Rendering"
original_url: "https://around-the-corner.typepad.com/adn/2016/05/how-to-remove-the-white-outline-in-the-renderview.html "
typepad_basename: "how-to-remove-the-white-outline-in-the-renderview"
typepad_status: "Publish"
---

<p>The MRenderView::updatePixels API has an annoying side-effect of showing a white outline. For example, calling it in the default way on a rectangle will show it like this:</p>  <p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb09063831970d-pi"><img title="withoutline" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="withoutline" src="/assets/image_6897fd.jpg" width="244" height="154" /></a></p>  <p>&#160;</p>  <p>There is an undocumented API available that can alleviate this problem. You can call the <strong>MRenderView::setDrawTileBoundary(false)</strong> method after startRender and it will disable Maya from drawing the outline. </p>  <p><a href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d01bb09063835970d-pi"><img title="withoutOutline" style="border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px" border="0" alt="withoutOutline" src="/assets/image_75c984.jpg" width="244" height="178" /></a></p>  <p>&#160;</p>  <p>Unfortunately, this API is currently marked for deprecation. But we have discussed this with our engineering team, and they agree it is an acceptable workaround for the time being.</p>
