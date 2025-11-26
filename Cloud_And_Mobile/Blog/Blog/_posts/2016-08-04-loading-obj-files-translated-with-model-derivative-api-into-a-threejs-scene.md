---
layout: "post"
title: "Loading .obj files translated with Model Derivative API into a three.js scene"
date: "2016-08-04 20:35:50"
author: "Shiya Luo"
categories:
  - "Model Derivative"
  - "Shiya Luo"
  - "THREE.js"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/loading-obj-files-translated-with-model-derivative-api-into-a-threejs-scene.html "
typepad_basename: "loading-obj-files-translated-with-model-derivative-api-into-a-threejs-scene"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/ShiyaLuo">@ShiyaLuo</a></p>

<p>With the model derivative API peeling itself away from the View and Data API, you can now translate stuff into OBJ at API level. Before you had to load up a viewer instance, which only takes in svf translated with this API. Now you can just load the OBJs that you've translated into your three.js scene!</p>

<p>I translated a f3d model that I have on hand into an obj, and loaded it into a scene. It's this car:</p>

<p><img src="/assets/image_9b8d5c.jpg" alt="" /></p>

<p>The API lets you translate parts of the model by specifying the part of the hierarchy you want to translate. So I translated just the suspension.</p>

<p>I just modified the three.js example code that lets you load your obj. Just replace the url of where the obj is.</p>

<p>I used <code>THREE.MeshPhongMaterial</code> instead of mapping a texture myself.</p>

<p>Here's the relevant part of the code:</p>

<p><img src="/assets/image_d6e99d.jpg" alt="" /></p>

<p>Now you have a weird-ish looking blue suspension in your three.js scene!</p>

<p><img src="/assets/image_eca610.jpg" alt="" /></p>

<p>Get the code on <a href="https://github.com/shiya/three.js-samples/blob/master/obj_loader/webgl_loader_obj.html">GitHub</a></p>

<p>Happy coding!</p>
