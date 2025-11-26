---
layout: "post"
title: "Moving visually your components in the viewer using the TransformTool"
date: "2015-08-14 08:08:29"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/08/moving-visually-your-components-in-the-viewer-using-the-transformtool.html "
typepad_basename: "moving-visually-your-components-in-the-viewer-using-the-transformtool"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Here is sample code I meant to produce for a while: providing a slick and easy way to visually drag components around in 3d inside our View &amp; Data API viewer... well here is it at last!</p>
<p>Based on the Three.js <a href="http://threejs.org/examples/misc_controls_transform.html" target="_self">transform control</a>, the development team implemented the section command using this nice 3d manipulator allowing to select plane, axis and rotation. This is provided out of the box with the GuiViewer3D:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d148ea94970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d148ea94970c img-responsive" title="Screen Shot 2015-08-14 at 4.59.16 PM" src="/assets/image_aa67a6.jpg" alt="Screen Shot 2015-08-14 at 4.59.16 PM" border="0" /></a></p>
<p>I reused some of their implementation to provide the ability to stick the control on the selected mesh and drag it around. It's more challenging that it may seem as the viewer has a complex way to work with Three.js meshes. This is due to enhancements that we created on top of that library to be able to support models with a huge number of meshes.</p>
<p>Here is a picture of the control in action, you can also click <a href="http://viewer.autodesk.io/node/gallery/embed?id=54dd0bb7725ef3180fc4ab9b&amp;extIds=Autodesk.ADN.Viewing.Extension.TransformTool" target="_blank">here</a> to try a live sample...</p>
<p>&nbsp; <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7bf10a7970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7bf10a7970b image-full img-responsive" title="Screen Shot 2015-08-14 at 5.00.57 PM" src="/assets/image_9845de.jpg" alt="Screen Shot 2015-08-14 at 5.00.57 PM" border="0" /></a></p>
<p>
<script type="text/javascript" src="https://gist.github.com/leefsmp/a56413d9cc49575d3454.js"></script>
</p>
