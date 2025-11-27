---
layout: "post"
title: "Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer &ndash; Part 1"
date: "2014-10-14 08:34:07"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "PaaS"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-1.html "
typepad_basename: "creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-1"
typepad_status: "Publish"
---

<p>After <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/gearing-up-for-the-vr-hackathon.html" target="_blank">yesterday’s introduction</a> to this series of posts, today we’re going to dive into some specifics, implementing a basic, web-based, stereoscopic viewer.</p>
<p>While this series of posts is really about using <a href="https://cardboard.withgoogle.com" target="_blank">Google Cardboard</a> to view Autodesk 360 models in 3D (an interesting topic, I hope you’ll agree ;-), it’s also about how easily you can use the Autodesk 360 viewer to power Google Cardboard: we’ll see it’s a straightforward way to get 3D content into a visualization system that’s really all about 3D.</p>
<p>Let’s start with some basics. We clearly need two views in our web-page, one for each eye. For now we’re not going to worry about making the page full-screen – which basically means hiding the address bar – as we’ll address that when we integrate device-tilt navigation tomorrow. But the web-page will fill the screen estate that we have, of course.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb0797a97a970d-pi" target="_blank"><img alt="Our basic stereoscopic 3D viewer" border="0" height="286" src="/assets/image_907780.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Our basic stereoscopic 3D viewer" width="474" /></a></p>
<p>The Autodesk 360 viewer doesn’t currently support multiple viewports on a single scene – even if this is a capability that <a href="http://threejs.org" target="_blank">Three.js</a> provides – so for now we’re going to embed two separate instances of the Autodesk 360 viewer. At some point the viewer will hopefully provide viewporting capability – and allow us to reduce the app’s network usage and memory footprint – but we’ll see over the coming posts that even with two separate viewer instances the app performs well.</p>
<p>In this post and the next we’re going to make use of the Morgan model that we saw <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/steampunking-a-morgan-3-wheeler-using-fusion-360.html" target="_blank">“steampunked” using Fusion 360</a> and then integrated into <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">my first Autodesk 360 application</a>. Basically because it’s the model that’s content that can already be accessed by this particular site. On Thursday we’ll extend that to be able to choose from a selection of models.</p>
<p>The lighting used for this model is different from in the previous sample: “simple grey” works better on mobile devices that “riverbank”, it seems (which has much more going on in terms of lights and environment backgrounds, etc.).</p>
<p>I’m looking at this viewer as an “object viewer”, which allows us to spin the camera around a fixed point of interest and view it from different angles, rather than a “walk-/fly-through viewer”. This is a choice, of course: you could easily take the foundation shown in this series and make a viewer that’s better-suited for viewing an architectural model from the inside, for instance.</p>
<p>OK, before we go much further, I should probably add this caveat: <span style="text-decoration: underline;">I don’t actually yet have a Google Cardboard device in my possession</span>. I have a Nexus 4 phone – which has Android 4.4.4 and can run <a href="https://play.google.com/store/apps/details?id=com.google.samples.apps.cardboarddemo&amp;hl=en" target="_blank">the native Google Cardboard app</a> as well as host <a href="https://www.khronos.org/webgl" target="_blank">WebGL</a> for a web-based viewer implementation – but I don’t actually have the lenses, etc. I have a <a href="http://www.dodocase.com/products/google-cardboard-vr-goggle-toolkit" target="_blank">DODOcase VR Cardboard Toolkit</a> waiting for me in San Francisco, but until now I haven’t tested to see whether the stereoscopic effect works or not. I’ve squinted at the screen from close up, of course, but haven’t yet seen anything jump out in 3D. That said, Jim Quanci assures me it looks great with the proper case, so I’m fairly sure I’m not wasting everyone’s time with these posts.</p>
<p>The main “known unknown” until I test firsthand has been the distance to be used between the two camera positions. Three.js allows us to translate a camera in the X direction (relative to its viewing direction along Z, which basically means pan left or right) very easily, but I’ve had to guess a little with the distance. For now I’ve taken 4% of the distance between the camera and the target – as this gives a very slight difference between the views for various models I tried – but this value may need some tweaking.</p>
<p>Beyond working out the camera positions of the two views, the main work is about keeping them in sync: if the lefthand view changes then the righthand view should adjust to keep the stereo effect and vice-versa. In my first implementation I used a number of HTML5 events to do this: <em>click</em>, <em>mouseup</em>, <em>mousemove</em>, <em>touchstart</em>, <em>touchend</em>, <em>touchcancel</em>, <em>touchleave</em> &amp; <em>touchmove</em>. And then I realised that there was no simple way to hook into zoom, which drove me crazy for a while. Argh. But then I realised I could hook into the viewer’s <em>cameraChanged</em> event, instead, which was much better (although this gets called for any change in the viewer, and you also need to make sure you don’t get into some circular modifications, leading to your model disappearing into the distance… :-).</p>
<p>Here’s an animated GIF of the views being synchronised successfully between the two embedded viewers inside a desktop browser:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb0797a999970d-pi"><img alt="Stereo Morgan" height="213" src="/assets/image_332930.jpg" style="float: none; margin: 20px auto; display: block;" title="Stereo Morgan" width="300" /></a></p>
<p>Now for some code… here’s the HTML page (which I’ve named <a href="http://safe-reef-1847.herokuapp.com/stereo-basic.html" target="_blank">stereo-basic.html</a>) for the simple, stereoscopic viewer. I’ve embedded the styles but have kept the JavaScript in a separate file for easier debugging.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">!DOCTYPE</span> <span style="color: red;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span> <span style="color: red;">charset</span><span style="color: blue;">=&quot;utf-8&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span>Basic Stereoscopic Viewer<span style="color: blue;">&lt;/</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">link</span> <span style="color: red;">rel</span><span style="color: blue;">=&quot;shortcut icon&quot;</span> <span style="color: red;">type</span><span style="color: blue;">=&quot;image/x-icon&quot;</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;/favicon.ico?v=2&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">name</span><span style="color: blue;">=&quot;viewport&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">content</span><span style="color: blue;">=</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&quot;width=device-width, minimum-scale=1.0, maximum-scale=1.0&quot;</span> <span style="color: blue;">/&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span> <span style="color: red;">charset</span><span style="color: blue;">=&quot;utf-8&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">link</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">rel</span><span style="color: blue;">=&quot;stylesheet&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">href</span><span style="color: blue;">=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">type</span><span style="color: blue;">=&quot;text/css&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">src</span><span style="color: blue;">=</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/jquery.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/stereo-basic.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">margin</span>: <span style="color: blue;">0px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">style</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span> <span style="color: red;">onload</span><span style="color: blue;">=&quot;</span>initialize();<span style="color: blue;">&quot;</span> <span style="color: red;">oncontextmenu</span><span style="color: blue;">=&quot;return</span> <span style="color: blue;">false</span>;<span style="color: blue;">&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">table</span> <span style="color: red;">width</span><span style="color: blue;">=&quot;100%&quot;</span> <span style="color: red;">height</span><span style="color: blue;">=&quot;100%&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">width</span><span style="color: blue;">=&quot;50%&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;viewLeft&quot;</span> <span style="color: red;">style</span><span style="color: blue;">=&quot;</span><span style="color: red;">width</span>:<span style="color: blue;">50%</span>; <span style="color: red;">height</span>:<span style="color: blue;">100%</span>;<span style="color: blue;">&quot;&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">td</span> <span style="color: red;">width</span><span style="color: blue;">=&quot;50%&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;viewRight&quot;</span> <span style="color: red;">style</span><span style="color: blue;">=&quot;</span><span style="color: red;">width</span>:<span style="color: blue;">50%</span>; <span style="color: red;">height</span>:<span style="color: blue;">100%</span>;<span style="color: blue;">&quot;&gt;&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">td</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">tr</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">table</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
</div>
<p>And here’s <a href="http://safe-reef-1847.herokuapp.com/js/stereo-basic.js" target="_blank">the referenced JavaScript file</a>:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">var</span> viewerLeft, viewerRight;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> updatingLeft = <span style="color: blue;">false</span>, updatingRight = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> leftLoaded = <span style="color: blue;">false</span>, rightLoaded = <span style="color: blue;">false</span>, cleanedModel = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> initialize() {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get our access token from the internal web-service API</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $.get(<span style="color: #a31515;">&#39;http://&#39;</span> + window.location.host + <span style="color: #a31515;">&#39;/api/token&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (accessToken) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Specify our options, including the document ID</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> options = {};</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.env = <span style="color: #a31515;">&#39;AutodeskProduction&#39;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.accessToken = accessToken;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.document =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=&#39;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create and initialize our two 3D viewers</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> elem = document.getElementById(<span style="color: #a31515;">&#39;viewLeft&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; viewerLeft = <span style="color: blue;">new</span> Autodesk.Viewing.Viewer3D(elem, {});</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Initializer(options, <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewerLeft.initialize();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loadDocument(viewerLeft, options.document);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; elem = document.getElementById(<span style="color: #a31515;">&#39;viewRight&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; viewerRight = <span style="color: blue;">new</span> Autodesk.Viewing.Viewer3D(elem, {});</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Initializer(options, <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewerRight.initialize();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loadDocument(viewerRight, options.document);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> loadDocument(viewer, docId) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// The viewer defaults to the full width of the container,</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// so we need to set that to 50% to get side-by-side</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; viewer.container.style.width = <span style="color: #a31515;">&#39;50%&#39;</span>;</p>
<p style="margin: 0px;">&#0160; viewer.resize();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Let&#39;s zoom in and out of the pivot - the screen</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// real estate is fairly limited - and reverse the</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// zoom direction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setZoomTowardsPivot(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setReverseZoomDirection(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (docId.substring(0, 4) !== <span style="color: #a31515;">&#39;urn:&#39;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; docId = <span style="color: #a31515;">&#39;urn:&#39;</span> + docId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; Autodesk.Viewing.Document.load(docId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (document) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Boilerplate code to load the contents</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> geometryItems = [];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (geometryItems.length == 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; geometryItems =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Document.getSubItemsWithProperties(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; document.getRootItem(),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; { <span style="color: #a31515;">&#39;type&#39;</span>: <span style="color: #a31515;">&#39;geometry&#39;</span>, <span style="color: #a31515;">&#39;role&#39;</span>: <span style="color: #a31515;">&#39;3d&#39;</span> },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">true</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (geometryItems.length &gt; 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.load(document.getViewablePath(geometryItems[0]));</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Add our custom progress listener and set the loaded</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// flags to false</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; viewer.addEventListener(<span style="color: #a31515;">&#39;progress&#39;</span>, progressListener);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; leftLoaded = rightLoaded = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (errorMsg, httpErrorCode) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> container = document.getElementById(<span style="color: #a31515;">&#39;viewerLeft&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (container) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alert(<span style="color: #a31515;">&#39;Load error &#39;</span> + errorMsg);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Progress listener to set the view once the data has started</span></p>
<p style="margin: 0px;"><span style="color: green;">// loading properly (we get a 5% notification early on that we</span></p>
<p style="margin: 0px;"><span style="color: green;">// need to ignore - it comes too soon)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> progressListener(e) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// If we haven&#39;t cleaned this model&#39;s materials and set the view</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// and both viewers are sufficiently ready, then go ahead</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (!cleanedModel &amp;&amp;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; ((e.percent &gt; 0.1 &amp;&amp; e.percent &lt; 5) || e.percent &gt; 5)) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (e.target.clientContainer.id === <span style="color: #a31515;">&#39;viewLeft&#39;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; leftLoaded = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (e.target.clientContainer.id === <span style="color: #a31515;">&#39;viewRight&#39;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; rightLoaded = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (leftLoaded &amp;&amp; rightLoaded &amp;&amp; !cleanedModel) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Iterate the materials to change any red ones to grey</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> p <span style="color: blue;">in</span> viewerLeft.impl.matman().materials) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> m = viewerLeft.impl.matman().materials[p];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m.color.r &gt;= 0.5 &amp;&amp; m.color.g == 0 &amp;&amp; m.color.b == 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.color.r = m.color.g = m.color.b = 0.5;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.needsUpdate = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> p <span style="color: blue;">in</span> viewerRight.impl.matman().materials) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> m = viewerRight.impl.matman().materials[p];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m.color.r &gt;= 0.5 &amp;&amp; m.color.g == 0 &amp;&amp; m.color.b == 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.color.r = m.color.g = m.color.b = 0.5;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.needsUpdate = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Zoom to the overall view initially</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; zoomEntirety(viewerLeft);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span> () { transferCameras(<span style="color: blue;">true</span>); }, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; cleanedModel = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">else</span> <span style="color: blue;">if</span> (cleanedModel &amp;&amp; e.percent &gt; 10) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// If we have already cleaned and are even further loaded,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// remove the progress listeners from the two viewers and</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// watch the cameras for updates</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; unwatchProgress();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; watchCameras();</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Add and remove the pre-viewer event handlers</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> watchCameras() {</p>
<p style="margin: 0px;">&#0160; viewerLeft.addEventListener(<span style="color: #a31515;">&#39;cameraChanged&#39;</span>, left2right);</p>
<p style="margin: 0px;">&#0160; viewerRight.addEventListener(<span style="color: #a31515;">&#39;cameraChanged&#39;</span>, right2left);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> unwatchCameras() {</p>
<p style="margin: 0px;">&#0160; viewerLeft.removeEventListener(<span style="color: #a31515;">&#39;cameraChanged&#39;</span>, left2right);</p>
<p style="margin: 0px;">&#0160; viewerRight.removeEventListener(<span style="color: #a31515;">&#39;cameraChanged&#39;</span>, right2left);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> unwatchProgress() {</p>
<p style="margin: 0px;">&#0160; viewerLeft.removeEventListener(<span style="color: #a31515;">&#39;progress&#39;</span>, progressListener);</p>
<p style="margin: 0px;">&#0160; viewerRight.removeEventListener(<span style="color: #a31515;">&#39;progress&#39;</span>, progressListener);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Event handlers for the cameraChanged events</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> left2right() {</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (!updatingRight) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; updatingLeft = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; transferCameras(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span> () { updatingLeft = <span style="color: blue;">false</span>; }, 500);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> right2left() {</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (!updatingLeft) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; updatingRight = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; transferCameras(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span> () { updatingRight = <span style="color: blue;">false</span>; }, 500);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> transferCameras(leftToRight) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// The direction argument dictates the source and target</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> source = leftToRight ? viewerLeft : viewerRight;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> target = leftToRight ? viewerRight : viewerLeft;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> pos = source.navigation.getPosition();</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> trg = source.navigation.getTarget();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Set the up vector manually for both cameras</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> upVector = <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1);</p>
<p style="margin: 0px;">&#0160; source.navigation.setWorldUpVector(upVector);</p>
<p style="margin: 0px;">&#0160; target.navigation.setWorldUpVector(upVector);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get the new position for the target camera</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> up = source.navigation.getCameraUpVector();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get the position of the target camera</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> newPos = offsetCameraPos(source, pos, trg, leftToRight);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Save the left-hand camera position: device tilt orbits</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// will be relative to this point</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; leftPos = leftToRight ? pos : newPos;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Zoom to the new camera position in the target</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; zoom(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; target, newPos.x, newPos.y, newPos.z, trg.x, trg.y, trg.z,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; up.x, up.y, up.z</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> offsetCameraPos(source, pos, trg, leftToRight) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get the distance from the camera to the target</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> xd = pos.x - trg.x;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> yd = pos.y - trg.y;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> zd = pos.z - trg.z;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> dist = Math.sqrt(xd * xd + yd * yd + zd * zd);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Use a small fraction of this distance for the camera offset</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> disp = dist * 0.04;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Clone the camera and return its X translated position</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> clone = source.autocamCamera.clone();</p>
<p style="margin: 0px;">&#0160; clone.translateX(leftToRight ? disp : -disp);</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">return</span> clone.position;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Model-specific helper to zoom into a specific part of the model</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomEntirety(viewer) {</p>
<p style="margin: 0px;">&#0160; zoom(viewer, -48722.5, -54872, 44704.8, 10467.3, 1751.8, 1462.8);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Set the camera based on a position, target and optional up vector</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoom(viewer, px, py, pz, tx, ty, tz, ux, uy, uz) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Make sure our up vector is correct for this model</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> upVector = <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1);</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setWorldUpVector(upVector, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> up =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (ux &amp;&amp; uy &amp;&amp; uz) ? <span style="color: blue;">new</span> THREE.Vector3(ux, uy, uz) : upVector;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setView(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(px, py, pz),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(tx, ty, tz)</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setCameraUpVector(up);</p>
<p style="margin: 0px;">}</p>
</div>
<p>To host something similar yourself, I recommend starting with <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">the post I linked to earlier</a> and building it up from there (you basically need to provide the ‘/api/token’ server API – using your own client credentials – for this to work).</p>
<p>But you don’t need to build it yourself – or even have an Android device – to give this a try. <a href="http://safe-reef-1847.herokuapp.com/stereo-basic.html" target="_blank">Simply load the HTML page in your preferred WebGL-capable browser</a> (Chrome is probably safest, considering that’s what I’ve been using when developing this) and have a play.</p>
<p>On a PC it will respond to mouse or touch navigation, of course, but in tomorrow’s post we’ll implement a much more interesting – at least with respect to Google Cardboard, where you can’t get your fingers near the screen to navigate – tilt-based navigation mechanism. We’ll also take a look at how we can use <a href="http://www.google.com/intl/en/chrome/browser/canary.html" target="_blank">Google Chrome Canary</a> to emulate device-tilt on a PC, reducing the need to jump through the various hoops needed to debug remotely. Interesting stuff. :-)</p>
