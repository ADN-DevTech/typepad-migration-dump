---
layout: "post"
title: "Forge Viewer Custom Shaders - Part 1"
date: "2017-01-26 05:24:58"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "Javascript"
  - "Philippe Leefsma"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/forge-viewer-custom-shaders-part-1.html "
typepad_basename: "forge-viewer-custom-shaders-part-1"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" rel="noopener noreferrer" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" rel="noopener noreferrer" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; Shaders - They are probably the most powerful tool to customize how your models get rendered by the viewer, unfortunately this is also one of&nbsp;the most difficult to master... Anything that gets rendered on the&nbsp;screen is most likely customizable through shaders, so their applications are quite broad and their capabilities virtually unlimited.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; This post is not an introduction to shaders so if that term doesn't ring a bell for you, you probably need to read a bit about the topic, here for example:&nbsp;<a href="https://aerotwist.com/tutorials/an-introduction-to-shaders-part-1/" target="_blank" rel="noopener noreferrer">an introduction to shaders, by Aerotwist</a>.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; <a href="https://twitter.com/hahakumquat" target="_blank" rel="noopener noreferrer">Michael Ge</a>, our regretted intern, wrote couple of articles a while ago about integrating custom shaders with the viewer:</span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif;"><a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/07/using-shaders-to-generate-dynamic-textures.html" target="_blank" rel="noopener noreferrer">Using Shaders to Generate Dynamic Textures in the Viewer API</a></span></li>
<li><span style="font-family: arial, helvetica, sans-serif;"><a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/08/ace-editor-for-threejs-shadermaterials-in-the-forge-viewer.html" target="_blank" rel="noopener noreferrer">Ace Editor for Three.js ShaderMaterials in the Forge Viewer</a></span></li>
</ul>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; This year I'm planning to start playing more seriously with shaders and I wanted to get things started with a very naive implementation: today's shader will provide&nbsp;a specific color to the affected components, using a <strong><em>uniform</em></strong>, the code will randomly change that color every 2 seconds, simply to illustrate how we can use a shader to dynamically change rendered properties on our components.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&nbsp; &nbsp; Below is an example of how my model looks like after I select two of its components. You can see the custom color but my shader is so basic that it doesn't take care about rendering the lights, hence&nbsp;details on the components are not visible... Next time I will take a closer look at how lights can be rendered by the&nbsp;shader so it looks nicer. Stay tuned!</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;"> <a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ce5b68970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ce5b68970b img-responsive" title="Screen Shot 2017-01-26 at 10.04.48" src="/assets/image_6b8d1c.jpg" alt="Screen Shot 2017-01-26 at 10.04.48" /></a><br /></span></p>
The code is so basic that I assume it is self-explanatory at this point ...
<br>
<br>
<script src="https://gist.github.com/leefsmp/f89be435f475b805780fe3ea4fd1fc54.js"></script>
