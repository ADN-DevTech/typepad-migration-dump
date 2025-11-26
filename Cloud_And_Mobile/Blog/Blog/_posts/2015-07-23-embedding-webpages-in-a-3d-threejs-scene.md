---
layout: "post"
title: "Embedding webpages in a 3D Three.js scene"
date: "2015-07-23 02:34:48"
author: "Philippe Leefsma"
categories:
  - "HTML"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/embedding-webpages-in-a-3d-threejs-scene.html "
typepad_basename: "embedding-webpages-in-a-3d-threejs-scene"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Today sample is inspired by the following <a href="http://learningthreejs.com/blog/2013/04/30/closing-the-gap-between-html-and-webgl/" target="_self">article</a> which exposes how to mix a 2d webpage into a 3d WebGL scene. The approach enables some interesting perspectives concerning what we can achieve with WebGL today: for example think about embedding videos or website to a TV screen inside your 3d scene or take advantage of some cool CSS effects and feature mapped onto a 3d surface...</p>
<p>In order to achieve proper blending of 3d meshes and 2d pages in my sample, I had to rely on the approach exposed by this <a href="http://codereply.com/answer/83pofc/threejs-properly-blending-css3d-webgl.html" target="_self">solution</a>. The author suggest adding the WebGL renderer as a child of the CSS3D renderer element, which works but disable the possibility for the user to interact with the page... When adding both renderers directly to the document, interaction with the page is possible, however the meshes do not get render properly in front of the css elements. So far I haven't found a way to combine both results with a single approach.</p>
<p>Here is my current code and a picture of the result: 3 webpages inserted in a Three.js scene. One amazing thing is the possibility to render a WebGL page inside WebGL, hence creating WebGL recursion! I don't know however how resource hungry this might be :)</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0856e40a970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb0856e40a970d image-full img-responsive" title="Screen Shot 2015-07-21 at 15.37.21" src="/assets/image_1556f3.jpg" alt="Screen Shot 2015-07-21 at 15.37.21" border="0" /></a></p>
<p>&nbsp;</p>
<script src="https://gist.github.com/leefsmp/38926bf2c379f604f9b5.js"></script>
