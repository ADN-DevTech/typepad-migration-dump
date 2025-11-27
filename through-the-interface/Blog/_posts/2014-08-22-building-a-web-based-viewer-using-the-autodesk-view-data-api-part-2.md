---
layout: "post"
title: "Building a web-based viewer using the Autodesk View &amp; Data API &ndash; Part 2"
date: "2014-08-22 11:21:53"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "HTML"
  - "JavaScript"
  - "PaaS"
  - "REST"
  - "SaaS"
  - "Web/Tech"
original_url: "https://www.keanw.com/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-2.html "
typepad_basename: "building-a-web-based-viewer-using-the-autodesk-view-data-api-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-1.html" target="_blank">the last post</a> we saw the process for getting content uploaded to Autodesk storage and translated into the format required by the Autodesk 360 viewer. In this post we’re going to show the steps to take that data and embed it in a “simple” HTML page. (Any complex capability in this page it’s due to the UI code that <a href="http://twitter.com/danwellman" target="_blank">Dan Wellman</a> kindly allowed me to borrow for the sample: otherwise what it does is very simple indeed.)</p>
<p>There are, of course, more complex samples that the ADN team has developed to demonstrate the richness of the new View &amp; Data API. You can, for example, isolate geometry and search for particular metadata.</p>
<p>As <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">I’ve mentioned before</a>, the model I’ve used for this sample is <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/steampunking-a-morgan-3-wheeler-using-fusion-360.html" target="_blank">big and flat</a>, so isolating sets of components was out of the question: I’ve opted to use simple view changes to highlight different sections of the model. And the <a href="http://forums.autodesk.com/t5/View-and-Data-API/New-Viewing-Service-Client-API/td-p/5201835" target="_blank">latest functionality that was pushed live in late July</a> provides some nice navigation capabilities, smoothly transitioning the view where possible.</p>
<p>Firstly, though, here’s the code for the very basic HTML page:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">!DOCTYPE</span> <span style="color: red;">HTML</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span> <span style="color: red;">charset</span><span style="color: blue;">=&quot;utf-8&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span>Steampunk Morgan Viewer<span style="color: blue;">&lt;/</span><span style="color: maroon;">title</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">link</span> <span style="color: red;">rel</span><span style="color: blue;">=&quot;stylesheet&quot;</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;css/css-transforms-viewer.css&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">name</span><span style="color: blue;">=&quot;viewport&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">content</span><span style="color: blue;">=&quot;width=device-width, minimum-scale=1.0, maximum-scale=1.0&quot;</span> <span style="color: blue;">/&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">meta</span> <span style="color: red;">charset</span><span style="color: blue;">=&quot;utf-8&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">link</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">rel</span><span style="color: blue;">=&quot;stylesheet&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">href</span><span style="color: blue;">=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">type</span><span style="color: blue;">=&quot;text/css&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">src</span><span style="color: blue;">=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/jquery.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/jquery.easing.1.3.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/jquery.csstransform.pack.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">script</span> <span style="color: red;">src</span><span style="color: blue;">=&quot;js/steampunk.js&quot;&gt;&lt;/</span><span style="color: maroon;">script</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">head</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">body</span> <span style="color: red;">onload</span><span style="color: blue;">=&quot;</span>initialize();<span style="color: blue;">&quot;</span> <span style="color: red;">oncontextmenu</span><span style="color: blue;">=&quot;return</span> <span style="color: blue;">false</span>;<span style="color: blue;">&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;viewer&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;cog&quot;&gt;</span><span style="color: #006400;">&lt;!-- --&gt;</span><span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;window&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;viewer3d&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">style</span><span style="color: blue;">=&quot;</span><span style="color: red;">width</span>:<span style="color: blue;">468px</span>; <span style="color: red;">height</span>:<span style="color: blue;">468px</span>; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;<span style="color: blue;">&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;ui&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">ul</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">li</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;label1&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">a</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;#entirety&quot;</span> <span style="color: red;">title</span><span style="color: blue;">=&quot;Entirety&quot;&gt;</span>Entirety<span style="color: blue;">&lt;/</span><span style="color: maroon;">a</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">li</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">li</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;label2&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">a</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;#engine&quot;</span> <span style="color: red;">title</span><span style="color: blue;">=&quot;Engine&quot;&gt;</span>Engine<span style="color: blue;">&lt;/</span><span style="color: maroon;">a</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">li</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">li</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;label3&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">a</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;#body&quot;</span> <span style="color: red;">title</span><span style="color: blue;">=&quot;Body&quot;&gt;</span>Body<span style="color: blue;">&lt;/</span><span style="color: maroon;">a</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">li</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">li</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;label4&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">a</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;#interior&quot;</span> <span style="color: red;">title</span><span style="color: blue;">=&quot;Interior&quot;&gt;</span>Interior<span style="color: blue;">&lt;/</span><span style="color: maroon;">a</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">li</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">li</span> <span style="color: red;">id</span><span style="color: blue;">=&quot;label5&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">a</span> <span style="color: red;">href</span><span style="color: blue;">=&quot;#wheels&quot;</span> <span style="color: red;">title</span><span style="color: blue;">=&quot;Wheels&quot;&gt;</span>Wheels<span style="color: blue;">&lt;/</span><span style="color: maroon;">a</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">li</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">ul</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span><span style="color: #006400;">&lt;!-- --&gt;</span><span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">div</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">&lt;/</span><span style="color: maroon;">body</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: maroon;">html</span><span style="color: blue;">&gt;</span></p>
</div>
<p>Here’s the main custom JavaScript it references (<em>steampunk.js</em>):</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">var</span> viewer;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Many thanks to Dan Wellman (@danwellman). Not only did he write</span></p>
<p style="margin: 0px;"><span style="color: green;">// the excellent post that formed the basis for this application&#39;s</span></p>
<p style="margin: 0px;"><span style="color: green;">// Steampunk UI, he provided the artwork to help build a custom</span></p>
<p style="margin: 0px;"><span style="color: green;">// version...</span></p>
<p style="margin: 0px;"><span style="color: green;">// http://www.dmxzone.com/go/18220/an-image-viewer-with-the-dmxzone-universal-css-transforms-library/</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> initialize() {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get our access token from the internal web-service API</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $.get(<span style="color: #a31515;">&quot;http://&quot;</span> + window.location.host + <span style="color: #a31515;">&#39;/api/token&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (accessToken) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> options = {};</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.env = <span style="color: #a31515;">&quot;AutodeskProduction&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.accessToken = accessToken;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.document =&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Create and initialize our 3D viewer</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> elem = document.getElementById(<span style="color: #a31515;">&#39;viewer3d&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; viewer = <span style="color: blue;">new</span> Autodesk.Viewing.Viewer3D(elem, {});</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.Viewing.Initializer(options, <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.initialize();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Go with the &quot;Riverbank&quot; lighting and background effect</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.impl.setLightPreset(8);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// We have a heavy model, so let&#39;s save some work during</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// navigation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.setOptimizeNavigation(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Let&#39;s zoom in and out of the pivot - the screen</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// real estate is fairly limited - and reverse the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// zoom direction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.navigation.setZoomTowardsPivot(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewer.navigation.setReverseZoomDirection(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; loadDocument(viewer, options.document);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Set up some UI elements for the Steampunk UI</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $(<span style="color: #a31515;">&quot;#ui&quot;</span>).find(<span style="color: #a31515;">&quot;div&quot;</span>).attr(<span style="color: #a31515;">&quot;id&quot;</span>, <span style="color: #a31515;">&quot;over&quot;</span>);</p>
<p style="margin: 0px;">&#0160; $(<span style="color: #a31515;">&quot;#window&quot;</span>).wrapInner(<span style="color: #a31515;">&quot;&lt;div id=\&quot;wrapper\&quot;&gt;&quot;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Store positions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> overPositions =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; entirety: 0, engine: 75, body: 152,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; interior: 230, wheels: 310</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; cogPositions =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; entirety: 5, engine: 80, body: 154,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; interior: 235, wheels: 310</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; previousCogPosition = 0;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Animation function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">function</span> animator(pointer, callback) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Move cog</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&quot;#cog&quot;</span>).animate({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;translateY&quot;</span>: parseInt(cogPositions[pointer]),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;rotate&quot;</span>:</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (parseInt(cogPositions[pointer]) &lt; previousCogPosition) ?</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;-=365&quot;</span> : <span style="color: #a31515;">&quot;+=365&quot;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }, <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; previousCogPosition = cogPositions[pointer];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Move over</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; $(<span style="color: #a31515;">&quot;#over&quot;</span>).animate({</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;translateY&quot;</span>: parseInt(overPositions[pointer])</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Add a delay so the camera changes after the cog has stopped</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// whirring</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (callback) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; setTimeout(<span style="color: blue;">function</span> () { callback(); }, 400);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Is there a hash?</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (window.location.hash) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Store the hash</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> hash = window.location.hash.split(<span style="color: #a31515;">&quot;#&quot;</span>)[1];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Position over&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; animator(hash);</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Add transitions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $(<span style="color: #a31515;">&quot;#ui a&quot;</span>).click(<span style="color: blue;">function</span> (e) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; e.preventDefault();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Store new pointer</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pointer = $(<span style="color: blue;">this</span>).attr(<span style="color: #a31515;">&quot;href&quot;</span>).split(<span style="color: #a31515;">&quot;#&quot;</span>)[1];</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Call animation function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; animator(pointer, <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pointer === <span style="color: #a31515;">&quot;entirety&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoomEntirety();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (pointer === <span style="color: #a31515;">&quot;engine&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoomEngine();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (pointer === <span style="color: #a31515;">&quot;body&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoomBody();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (pointer === <span style="color: #a31515;">&quot;interior&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoomInterior();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; } <span style="color: blue;">else</span> <span style="color: blue;">if</span> (pointer === <span style="color: #a31515;">&quot;wheels&quot;</span>) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; zoomWheels();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; });</p>
<p style="margin: 0px;">&#0160; });</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Helper functions to zoom into a specific part of the model</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomEntirety() {</p>
<p style="margin: 0px;">&#0160; zoom(-48722.5, -54872, 44704.8, 10467.3, 1751.8, 1462.8);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomEngine() {</p>
<p style="margin: 0px;">&#0160; zoom(-17484, -364, 4568, 12927, 173, 1952);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomBody() {</p>
<p style="margin: 0px;">&#0160; zoom(53143, -7200, 5824, 12870, -327.5, 1674);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomInterior() {</p>
<p style="margin: 0px;">&#0160; zoom(20459, -19227, 19172.5, 13845, 1228.6, 2906);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoomWheels() {</p>
<p style="margin: 0px;">&#0160; zoom(260.3, 26327, 954, 371.5, 134, 2242.7);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Set the camera based on a position and target location</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> zoom(px, py, pz, tx, ty, tz) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Make sure our up vector is correct for this model</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> camera = viewer.autocamCamera;</p>
<p style="margin: 0px;">&#0160; camera.up = <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1);</p>
<p style="margin: 0px;">&#0160; viewer.navigation.setWorldUpVector(camera.up);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// This performs a smooth view transition (we might also use</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// setView() to get there more directly)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; viewer.impl.controls.transitionView(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(px, py, pz), <span style="color: blue;">new</span> THREE.Vector3(tx, ty, tz),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; camera.fov, camera.up, <span style="color: blue;">true</span></p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//viewer.navigation.setRequestTransition( </span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//&#0160; true,</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//&#0160; new THREE.Vector3(px, py, pz), new THREE.Vector3(tx, ty, tz),</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//&#0160; viewer.getFOV()</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">//);</span></p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">// Progress listener to set the view once the data has started</span></p>
<p style="margin: 0px;"><span style="color: green;">// loading properly (we get a 5% notification early on that we</span></p>
<p style="margin: 0px;"><span style="color: green;">// need to ignore - it comes too soon)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> progressListener(param) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (param.percent &gt; 0.1 &amp;&amp; param.percent &lt; 5) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Remove the listener once called - one-time operation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; viewer.removeEventListener(<span style="color: #a31515;">&quot;progress&quot;</span>, progressListener);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Iterate the materials to change any red ones to grey</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> p <span style="color: blue;">in</span> viewer.impl.matman().materials) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> m = viewer.impl.matman().materials[p];</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m.color.r &gt;= 0.5 &amp;&amp; m.color.g == 0 &amp;&amp; m.color.b == 0) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.color.r = m.color.g = m.color.b = 0.5;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; m.needsUpdate = <span style="color: blue;">true</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Zoom to the overal view initially</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; zoomEntirety();</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> loadDocument(viewer, docId) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (docId.substring(0, 4) !== <span style="color: #a31515;">&#39;urn:&#39;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; docId = <span style="color: #a31515;">&#39;urn:&#39;</span> + docId;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; Autodesk.Viewing.Document.load(docId,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (document) {</p>
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
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; viewer.addEventListener(<span style="color: #a31515;">&quot;progress&quot;</span>, progressListener);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (errorMsg, httpErrorCode) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> container = document.getElementById(<span style="color: #a31515;">&#39;viewer3d&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (container) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alert(<span style="color: #a31515;">&quot;Load error &quot;</span> + errorMsg);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">}</p>
</div>
<p>A couple of things to note: the document ID is the Base64-encoded URN we saw last time. Which is hosted on my storage, so you’ll need to change this to the equivalent URN on your own, in due course.</p>
<p>You’ll also note that the code calls a REST API on the server hosting the page (<span style="font-family: &#39;Courier New&#39;;">/api/token</span>) in order to get an access token. This is the internal API that very simply calls into the Autodesk web-service that deals with authentication.</p>
<p>Here’s the JavaScript code – using the Node.js framework – that implements this API:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">var</span> CONSUMER_KEY = <span style="color: #a31515;">&#39;K8whhq86fnoYqw4GXAW0ID1hH&#39;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> CONSUMER_SECRET = <span style="color: #a31515;">&#39;DC2cBoXIy8&#39;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> BASE_URL = <span style="color: #a31515;">&#39;https://developer.api.autodesk.com&#39;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> request = require(<span style="color: #a31515;">&#39;request&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">exports.getToken = <span style="color: blue;">function</span> (req, res) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> params = {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; client_id: CONSUMER_KEY,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; client_secret: CONSUMER_SECRET,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; grant_type: <span style="color: #a31515;">&#39;client_credentials&#39;</span></p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; request.post(BASE_URL + <span style="color: #a31515;">&#39;/authentication/v1/authenticate&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; { form: params },</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (error, response, body) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!error &amp;&amp; response.statusCode == 200) {&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> authResponse = JSON.parse(body);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; res.send(authResponse.access_token);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
</div>
<p>The key and secret are the same (edited) ones we saw in the last post. The caller will receive an authorization token they can then use to call into the View &amp; Data API directly, without requiring further API calls from this particular web-service. (Unless the viewer needs another token, in which case this should get called again.)</p>
<p>So how do you get this sample working? The simplest way would be to clone <a href="https://github.com/Developer-Autodesk/client-steampunked-morgan" target="_blank">the sample’s public GitHub repository</a> to your local system (this can be done either using a GitHub client app or the command-line) and then create a new, private repository on GitHub containing this sample with a couple of modified files: you should (of course) change the key and secret to be your own in <em>api.js</em>, but you will also need to update the document ID (the value of <span style="font-family: &#39;Courier New&#39;;">options.document</span> in <em>steampunk.js</em>) to point to your own content that was uploaded and translated using the process we saw last time.</p>
<p>Once these changes are committed to GitHub, you can create a free app on <a href="https://www.heroku.com/" target="_blank">Heroku</a>, which connects directly to GitHub and pulls down the code from there in order to build it. Very, very easy to deploy an app in this way (I won’t go through the specific steps in this post, but if there’s demand I can certainly do so in a follow-up – just post a comment if you’d like to see that).</p>
<p>Et voilà, <a href="http://autode.sk/m3w" target="_blank">the working application</a> (if you see white mudguards and interior leather, give it some time: some materials are still loading). Hopefully you can see that it’s possible to create a fairly impressive web-based viewer for your content without a great deal of code.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201a73e066f25970d-pi" target="_blank"><img alt="The working application" border="0" height="406" src="/assets/image_509135.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="The working application" width="470" /></a></p>
<p>Of course there’s much more you can do with the View &amp; Data API than I’ve shown in this sample, so I do recommend perusing <a href="https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples" target="_blank">the other samples on GitHub</a>, as well as <a href="http://forums.autodesk.com/t5/view-and-data-api/viewer-samples-go-live/td-p/5160177" target="_blank">playing with the running samples themselves</a>. And you’ll find more information on this interesting new API on <a href="http://adndevblog.typepad.com/cloud_and_mobile/" target="_blank">the ADN team’s Cloud &amp; Mobile DevBlog</a>.</p>
<p><strong><em>Update:</em></strong></p>
<p>I’ve had to make s few changes to the client-side JavaScript file for it to work with a recent release of the viewer. I’ve gone ahead and updated the code in the post (you can always see what changes have been made in GitHub, of course).</p>
