---
layout: "post"
title: "Integrating View & Data API with Appery.io - Part I"
date: "2015-02-06 04:41:34"
author: "Philippe Leefsma"
categories:
  - "Client"
  - "Cloud"
  - "HTML"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/integrating-view-data-api-with-apperyio-part-i.html "
typepad_basename: "integrating-view-data-api-with-apperyio-part-i"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d11fff970c-pi" style="display: inline;"><img alt="Appery.io_logo_221x46" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d11fff970c img-responsive" src="/assets/image_adb74f.jpg" title="Appery.io_logo_221x46" /></a></p>
<p>I&#39;ve been playing with a cool technology this week named <a href="http://appery.io/" target="_self" title="">Appery.io</a>, the best is to define it with their own words:&#0160;</p>
<p><em>Appery.io, developed by&#0160;<a href="http://www.exadel.com/" target="_blank">Exadel</a>, is the first mobile platform that offers a cloud-based rapid&#0160;<a href="http://appery.io/backendservices">enterprise mobile app development</a>&#0160;environment with integrated backend services and a rich catalog of API plug-ins that dramatically simplify integration with cloud services and enterprise systems. It combines the simplicity of visual development with the power of JavaScript to create cross-platform enterprise apps rapidly. Because the platform is 100% cloud-based, you can focus on creating great applications, while we worry about maintaining the platform.</em></p>
<p>Alright, my goal is to integrate View &amp; Data API inside that stuff to produce a little mobile App, here is the idea: I will use my existing backend REST services from the&#0160;<a href="http://gallery.autodesk.io" target="_self" title="">Gallery</a>&#0160;to create an App that displays the list of models and lets the user select one to load it in the viewer, sounds like a good proof of concepts, let&#39;s get started!</p>
<p>First of all, here are the available REST APIs exposed by my backend that I&#39;m going to use and their purpose:</p>
<p>1.&#0160;<a href="http://viewer.autodesk.io/node/gallery/api/models" target="_self" title="">http://viewer.autodesk.io/node/gallery/api/models</a>:</p>
<p>&#0160;&#0160;&#0160;&#0160;Returns the list of all models available in the Gallery</p>
<p>2.&#0160;<a href="http://viewer.autodesk.io/node/gallery/api/thumbnail/{modelId}" target="_self" title="">http://viewer.autodesk.io/node/gallery/api/thumbnail/{modelId}</a>:</p>
<p>&#0160;&#0160;&#0160;&#0160;Returns thumbnail data for the specified modelId</p>
<p>3.&#0160;<a href="http://viewer.autodesk.io/node/gallery/embed/{modelId}" target="_self" title="">http://viewer.autodesk.io/node/gallery/embed/{modelId}</a>:</p>
<p>&#0160;&#0160;&#0160;&#0160;Allows to embed a specific model in an iframe</p>
<p>&#0160;</p>
<p>Let&#39;s create a new App, I name it &quot;<em>ADN Gallery</em>&quot;:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d120d3970c-pi" style="display: inline;"><img alt="App Creation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d120d3970c image-full img-responsive" src="/assets/image_ac2307.jpg" title="App Creation" /></a></p>
<p>Appery.io propose a pretty slick browser-based visual IDE that allows you to create pages and dialogs for your App which you populate with built-in or custom controls, so in my startScreen page, I will first place a List:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747c401970b-pi" style="display: inline;"><img alt="Pages" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c747c401970b image-full img-responsive" src="/assets/image_be15bc.jpg" title="Pages" /></a></p>
<p>I want to populate that list with&#0160;models from the Gallery, so I then have to create a new REST Service, there are some very convenient built-in tools to work with REST in Appery.&#0160;</p>
<p>I define the properties of my REST service below, notice that I&#39;m using the &quot;<em>Appery.io Proxy</em>&quot; option, this will allow me to bypass CORS security restrictions and test my App directly in the browser!</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747c4ef970b-pi" style="display: inline;"><img alt="Rest service" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c747c4ef970b image-full img-responsive" src="/assets/image_6f5d37.jpg" title="Rest service" /></a></p>
<p>You can test the service using the &quot;Test&quot; tab and once the reply has been received, you can can directly &quot;Import as Response&quot; which automatically defines the response format of the service:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eb6140970d-pi" style="display: inline;"><img alt="Test rest service" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07eb6140970d image-full img-responsive" src="/assets/image_631ba8.jpg" title="Test rest service" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d12266970c-pi" style="display: inline;"><img alt="Service response format" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d12266970c image-full img-responsive" src="/assets/image_2543f4.jpg" title="Service response format" /></a></p>
<p>The next step would be to define two events handlers, which is also very easy to do through the interface: upon loading of the startScreen page, we will invoke the models service and upon success of that service, we will define a mapping so the data for each model in the response is automatically mapped with a new listview item:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747c58a970b-pi" style="display: inline;"><img alt="Events" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c747c58a970b image-full img-responsive" src="/assets/image_35b80d.jpg" title="Events" /></a></p>
<p>The mappings are defined graphically with a specific editor as well: I&#39;m mapping each model in the response to a modelItem in the list and model.name is mapped to the list item name:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eb627b970d-pi" style="display: inline;"><img alt="Bind service result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07eb627b970d image-full img-responsive" src="/assets/image_e74a64.jpg" title="Bind service result" /></a></p>
<p>Now I want to go a step further and display the thumbnail of the models directly in the listview items, so time to write some JavaScript! Clicking the little &quot;JS&quot; logo at the end of the mapping arrow takes you to an editor where you can input some code that gets executed before the mapping happens, there you can transform the output of the mapping or perform some additional actions through JavaScript.</p>
<p>Here is the code of my mapping, this does two things: sets up an event listener, so when a list item gets clicked, the data about this item is stored in the local storage - this info will be accessed by the next page to know which model to load - and it invokes my thumbnail service to set the image data in the list item:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eb6372970d-pi" style="display: inline;"><img alt="Binding code" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07eb6372970d image-full img-responsive" src="/assets/image_533522.jpg" title="Binding code" /></a></p>
<p>At that point, you can hit the Test button and see your App running in the browser with a phone frame around it or not, as well as testing different screen sizes and orientation:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d124ef970c-pi" style="display: inline;"><img alt="Test list" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d124ef970c image-full img-responsive" src="/assets/image_4586b7.jpg" title="Test list" /></a></p>
<p>Alright, I&#39;m defining another event so when a list item gets clicked, another page will be displayed, this will be the viewerPage. I create a mapping to bind the data in the local storage to the title of that page, so my model name appears at the top:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747c947970b-pi" style="display: inline;"><img alt="Viewer binding" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c747c947970b image-full img-responsive" src="/assets/image_8a0389.jpg" title="Viewer binding" /></a></p>
<p>And then I also create a mapping between the selectedModel._id and the header text, so I can place some custom JavaScript in the mapping transform where I can easily grab the id of the model to load. This is where JavaScript and DOM manipulation is so cool: I can dynamically create an &lt;iframe&gt; element with the correct url based on my model id and append that iframe to the &lt;div&gt; of my viewerPage.</p>
<p>Here is the code for that with some little tweaks to adjust the height and the style:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eb64ab970d-pi" style="display: inline;"><img alt="Iframe code" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07eb64ab970d image-full img-responsive" src="/assets/image_f371af.jpg" title="Iframe code" /></a></p>
<p>And that&#39;s pretty much it! Just with couple lines of code I get a fully functional mobile App, below is a recording of the final version tested in the browser:</p>
<p><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/ZG2bex7bP4Q?feature=oembed" width="459"></iframe>&#0160;</p>
<p>You can then export the result as compiled package or as project that you can load in an IDE, the generated project is based on&#0160;<a href="http://cordova.apache.org/" target="_self" title="">Cordova</a>:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747cb5d970b-pi" style="display: inline;"><img alt="Export" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c747cb5d970b image-full img-responsive" src="/assets/image_a03fa4.jpg" title="Export" /></a></p>
<p>You could as well directly see the generated code on Appery.io:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eb65b4970d-pi" style="display: inline;"><img alt="Source" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07eb65b4970d image-full img-responsive" src="/assets/image_da8554.jpg" title="Source" /></a></p>
<p>Finally you can create a backup of your project and pass it to somebody else for example, so he can create his own Appery.io App based on yours, impressive flexibility I must say...</p>
<p>Attached is the backup for my project and the Android/iOS packages if you want to have fun playing with the Apps:</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07eb6630970d img-responsive"><a href="http://adndevblog.typepad.com/files/adn-gallery_backup.zip">Download ADN Gallery_backup</a></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07eb6630970d img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0d12755970c img-responsive"><a href="http://adndevblog.typepad.com/files/adngallery---eclipse.zip">Download ADN+Gallery - Eclipse</a></span></span></p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb07eb6630970d img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d0d12755970c img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c747cc28970b img-responsive"><a href="http://adndevblog.typepad.com/files/adngallery---xcode.zip">Download ADN+Gallery - XCode</a></span></span></span></p>
<p>&#0160;</p>
