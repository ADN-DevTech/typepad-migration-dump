---
layout: "post"
title: "Selective explode in the viewer"
date: "2016-12-20 22:16:09"
author: "Philippe Leefsma"
categories:
  - "Client"
  - "Cloud"
  - "Forge"
  - "Frontend"
  - "Javascript"
  - "Philippe Leefsma"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/selective-explode-in-the-viewer.html "
typepad_basename: "selective-explode-in-the-viewer"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" rel="noopener noreferrer" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" rel="noopener noreferrer" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; Here is a question that was asked by a developer during our last accelerator: <em>How to exclude some components from being exploded in a model?</em></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; Well, there is no API method directly exposed to achieve that, so I dig in the viewer source code and extracted the piece of code that handles the explosion transforms, then customized it a bit by adding an if condition that will prevent the passed fragment ids from being transformed.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">&#0160; &#0160; Here is the method, I named that <em>selectiveExplode</em>:&#0160;</span></p>
<script src="https://gist.github.com/leefsmp/bc1ee01f715c289db42725d7b6722bd9.js"></script>
<p>&#0160; &#0160; You can cut and paste that code <a href="http://viewer.autodesk.io/node/gallery/#/extension-editor?id=560c6c57611ca14810e1b2bf" rel="noopener noreferrer" target="_blank">here</a> to quickly test it, simply replace the load method by this one with predefined fragment Ids:</p>
<script src="https://gist.github.com/leefsmp/7ffa8541397b803bd5ad239691498097.js"></script>
<p>&#0160; &#0160; This will prevent the carburator (&quot;Carb:1&quot;)&#0160;from being exploded:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8bf4610970b-pi" style="display: inline;"><img alt="Screen Shot 2016-12-21 at 15.06.45" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8bf4610970b image-full img-responsive" src="/assets/image_b2c182.jpg" title="Screen Shot 2016-12-21 at 15.06.45" /></a></p>
