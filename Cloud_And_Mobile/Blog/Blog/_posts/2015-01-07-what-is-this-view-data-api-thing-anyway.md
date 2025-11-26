---
layout: "post"
title: "What is this View & Data API thing anyway?"
date: "2015-01-07 15:02:21"
author: "Madhukar Moogala"
categories:
  - "Javascript"
  - "Stephen Preston"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/01/what-is-this-view-data-api-thing-anyway.html "
typepad_basename: "what-is-this-view-data-api-thing-anyway"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/announcing-apphack-20.html">Stephen Preston</a></p>
<p>We&#39;ve already received a lot of responses to the announcement of our first Autodesk Cloud Accelerator program (which takes place March 9th-20th in the Autodesk San Francisco office -&#0160; <a href="http://autodeskcloudaccelerator.com" target="_blank">autodeskcloudaccelerator.com)</a>. Unfortunately, a not insignificant number of these were questions along the lines of &quot;what exactly does the View &amp; Data API do?&quot;. That is understandable because the current information on our developer portal (<a href="http://developer.autodesk.com" target="_blank">developer.autodesk.com</a>) assumes you know exactly what the API is and just want to use it. This blog post is a stopgap while we&#39;re working to fix that.</p>
<p><strong>The executive summary ...</strong></p>
<p>The View &amp; Data API enables web developers to very easily display 3D (and 2D) models on a WebGL-enabled browser. If you&#39;ve got a model you&#39;ve created using some kind of design software, then you can probably display it on your website using this API.</p>
<p style="text-align: left;">Executives like pretty pictures, so here is a small example of the API capability at its most basic. This is just a simple iframe embed of a file uploaded to the <a href="http://autodesk360.com" target="_blank">Autodesk A360 service</a> (which uses the View &amp; Data API as a component):</p>
<p style="text-align: center;"><iframe allowfullscreen="true" frameborder="0" height="352" mozallowfullscreen="true" src="https://myhub.autodesk360.com/ue29c3186/shares/public/SHabee1QT1a327cf2b7a6919c41ac0c7473f?mode=embed" webkitallowfullscreen="true" width="500"></iframe></p>
<p>Go ahead and play with it before you continue reading. This blog page is too narrow to display the default toolbar, so click the fullscreen button to start. Try the explode button and clicking around in the model to select various elements.</p>
<p><em>(If you can&#39;t see the embedded model above its probably because you&#39;re using an unsupported browser, or you&#39;ve turned WebGL off. Try using Chrome instead).</em></p>
<p><strong>Drilling a little deeper ...</strong></p>
<p>The API comes in two parts -</p>
<ul>
<li><strong>A (server-side) REST API</strong> that allows you to translate any of almost 100 design file formats into a common (JSON) file format. The data extracted from the original file is the geometry of every element in the model and the metadata that was attached to each of those model elements.</li>
<li><strong>A (client-side) JavaScript API</strong> that simplifies displaying and interacting with a model that has been processed by ther translator. The UI for the viewer defined by the API can be heavily customized, viewer actions can be automated (zoom, pan rotate, explode, etc), and callbacks are available for user-driven events (e.g. a mouse click on a specific model element, a change in camera position, etc).</li>
</ul>
<p>This allows you to very easily add rich 3D, interactive content to your web page. All you need to know is a little bit of server-side programming (to send a file for translation and request an OAuth access token), and some basic JavaScript (to initialize and control the viewer). The viewer is built using the <a href="http://threejs.org" target="_blank">THREE.js</a> library, so as well as the high level control provided by the Autodesk View &amp; Data API, you also have access and control over the underlying THREE.js objects that define the model scene (meshes, textures, camera, etc).</p>
<p>Autodesk creates software used in the design industry, so most of the models I have are of machines and buildings, but you can pretty much display any model you have. For example, OBJ is one of the file formats supported (and over 100 other file types). The real power of the View &amp; Data API is how easy it makes it for you to take a model you&#39;ve created in almost any format and display it on your web page with no requirement for downloading plug-ins or applications. And if your model contains additional data (part numbers, materials, etc) then they are made available in your web-client too.</p>
<p><strong>More advanced samples ...<br /></strong></p>
<p>Because the client-side API also allows access to the lower-level THREE.js (and WebGL) objects, there&#39;s very little you can&#39;t do with the viewer. We&#39;ve posted a few samples on GitHub (and are hosting live versions so you can play with them before trying to set them up for your own environment). Here are links to a few live samples for you to play with. Watch the videos for a quick walkthrough of each sample.</p>
<p><em>Steampunk</em><br /> <br />Demonstrates customizing the viewer UI to create a &#39;steampunk&#39; theme. Cllick on the buttons on the left to activate set camera viewpoints, or click/drag/scroll with your mouse inside the viewer window to move the camera around.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b9fdab970c-pi" style="display: inline;"><img alt="Steampunk" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0b9fdab970c img-responsive" src="/assets/image_20c41b.jpg" title="Steampunk" /></a></p>
<p><a href="https://www.youtube.com/watch?v=YlQnIj5Jnlk" target="_blank">Video</a> | <a href="http://safe-reef-1847.herokuapp.com/" target="_blank">Demo</a> | <a href="https://github.com/Developer-Autodesk/client-steampunked-morgan" target="_blank">Code</a></p>
<p>&#0160;</p>
<p><em>SAP<br /></em></p>
<p>Demonstrates linking individual elements in a model to an external database (SAP in this case, but it could be any database you like). Pricing data from the SAP data is inserted into the property panel for each element when you select it. Watch the video to understand the featrures we&#39;ve implemented.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07d477b2970d-pi" style="display: inline;"><img alt="SAP" class="asset  asset-image at-xid-6a0167607c2431970b01bb07d477b2970d img-responsive" src="/assets/image_c04443.jpg" title="SAP" /></a></p>
<p><a href="https://www.youtube.com/watch?v=2flj6LuUV-w" target="_blank">Video</a> | <a href="http://54.68.140.45/sapdemo" target="_blank">Demo</a> | <a href="https://github.com/Developer-Autodesk/integration-sap-view.and.data.api" target="_blank">Code</a></p>
<p>&#0160;</p>
<p><em>Gallery<br /></em></p>
<p>This is a general purpose sample that allows you to upload your own models and is also home for many of our client-side API samples, which can be activated and deactivated using our &#39;extensions&#39; API. This is another one where you really need to watch the video to fully understand how cool it is. Sample extensions include adding new meshes to the model, moving parts, replacing textures, associative annotation, creating guided tours (view automation), tailoring the UI, data driven graphing, physics behavior, and more.</p>
<p>(Check out the q-bik model with the Physics extension. Although the View &amp; Data API is a viewing engine rather than a gaming engine, its easy to hook up your model to the physics engine of your choice - <a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/01/integrating-ammojs-physics-with-autodesk-view-data-api.html" target="_blank">Ammo.js in this case</a>. Here&#39;s a <a href="https://www.youtube.com/watch?v=tK2ndbvchIM" target="_blank">short video</a>).</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b9fdec970c-pi" style="display: inline;"><img alt="Gallery" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0b9fdec970c img-responsive" src="/assets/image_3e629e.jpg" title="Gallery" /></a></p>
<p><a href="http://youtu.be/SQJSuqRqiCg" target="_blank">Video</a> | <a href="http://viewer.autodesk.io/node/gallery/#/gallery" target="_blank">Demo</a> | <a href="https://github.com/Developer-Autodesk/workflow-gallery-view.and.data.api" target="_blank">Code</a></p>
<p>&#0160;</p>
<p><strong>Getting started ...<br /></strong></p>
<p>Want to experiment with the API yourself? Everything you need is available at <a href="http://developer.autodesk.com" target="_blank">developer.autodesk.com</a>. Create an Autodesk Id, click the &#39;Request an Access Key&#39; link and request an app key for the &#39;View &amp; Data API&#39;. (Don&#39;t also request a key for AutoCAD I/O, unless you want to wait for your request to be manually approved - they View &amp; Data API keys are generated instantly, the AutoCAD I/O ones have to be approved). Once you have a key, I recommend you start learning the API by working through this <a href="https://fast-shelf-9177.herokuapp.com/" target="_blank">interactive quickstart guide</a>. Then follow the links from the developer portal to our samples poisted on GitHub, the API documentation, and the discussion forum where you can ask questions.</p>
<p>&#0160;</p>
