---
layout: "post"
title: "Show up Mesh Triangles from Data of HitTest "
date: "2017-01-04 00:38:18"
author: "Xiaodong Liang"
categories:
  - "THREE.js"
  - "Viewer"
  - "WebGL"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2017/01/show-up-mesh-triangles-from-data-of-hittest-.html "
typepad_basename: "show-up-mesh-triangles-from-data-of-hittest-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><img alt="" src="/assets/Forge%20Viewer-2.11-green.svg" /></p>
<p>We have some extensions that can get the triangles of viewer model, such as <a href="https://github.com/Autodesk-Forge/library-javascript-viewer-extensions/blob/3bc8881519513945dde7352881e23ddbf1facd70/src/Autodesk.ADN.Viewing.Extension.MeshImporter/Autodesk.ADN.Viewing.Extension.MeshImporter.js">Autodesk.ADN.Viewing.Extension.MeshData.js</a>. It is useful for geometry analysis, simulation etc. I tried to write more codes to calculate the hit point on one object, check which triangle the point locates in and build the THREE.js face above the corresponding viewer triangle.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c3a821970b-pi" style="display: inline;"><img alt="Select-triangles" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c3a821970b img-responsive" src="/assets/image_850ce1.jpg" title="Select-triangles" /></a></p>
<p>It can work, but finally I got to know I missed one existing method <strong>viewer.impl.hitTest.</strong> By passing a screen point, it can return the corresponding interaction&#0160;point on the face, and the face information! This is quite useful. It tells the index of the vertices, the fragment Id and face normal. By this method, the codes will be more clean and simple.</p>
<p>GIF (Click it to open a new page, in which you can see the animation)</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8c3a7de970b-pi" style="display: inline;"><img alt="Select-face" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8c3a7de970b img-responsive" src="/assets/image_685630.jpg" title="Select-face" /></a></p>
<p>I am sharing the codes as below. After the extension is loaded, the triangle will be displayed accordingly when mouse moving like the gif&#0160;shows.</p>
<script src="https://gist.github.com/xiaodongliang/7446cc7bab389b7ec0a8b1508bec45fb.js"></script>
<p>Some extra information: in recent Accelerators in China, a requirement is to identify the topology face of the model in the format of Forge Viewer. Unfortunately, from our engineer team, such information is not stored with current viewer API. The information would probably available with a separate file (prefix is &#39;topology&#39;) in svf package, but it is not always available when the source model is complex. In addition, there is not yet API to get it out. Only the primitives (triangles) of the mesh is provided by Forge Viewer.</p>
