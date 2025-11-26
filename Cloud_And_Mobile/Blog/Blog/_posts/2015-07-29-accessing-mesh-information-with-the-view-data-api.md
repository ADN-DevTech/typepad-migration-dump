---
layout: "post"
title: "Accessing mesh information with the View & Data API"
date: "2015-07-29 14:15:57"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/accessing-mesh-information-with-the-view-data-api.html "
typepad_basename: "accessing-mesh-information-with-the-view-data-api"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>Pretty much everything is said in the title! So today's post exposes a viewer extension that shows you how to access vertices and facets information of the meshes in a loaded model. Because we are using a custom renderer and some work have been done in order for the viewer to support a very large amount of triangles, the approach is quite different than what you would do from a plain Three.js scene.</p>
<p>Once loaded, the "Autodesk.ADN.Viewing.Extension.MeshData" extension will draw lines and spheres to represent vertices and facets of the selected mesh in your model. Below is a picture of the result and the full source of the sample:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b65c86970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b65c86970b image-full img-responsive" title="Screen Shot 2015-07-29 at 10.55.29 PM" src="/assets/image_e9b349.jpg" alt="Screen Shot 2015-07-29 at 10.55.29 PM" border="0" /></a></p>
</br>
<script src="https://gist.github.com/leefsmp/e6ac700e652baf3c2feb.js"></script>
