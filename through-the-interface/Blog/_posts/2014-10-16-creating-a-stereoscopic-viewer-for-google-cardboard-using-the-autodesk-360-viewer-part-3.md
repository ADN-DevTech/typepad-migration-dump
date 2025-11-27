---
layout: "post"
title: "Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer &ndash; Part 3"
date: "2014-10-16 05:52:25"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "PaaS"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-3.html "
typepad_basename: "creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-3"
typepad_status: "Publish"
---

<p>After <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/gearing-up-for-the-vr-hackathon.html" target="_blank">introducing the topic</a>, <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-1.html" target="_blank">showing a basic stereoscopic viewer using the Autodesk 360 viewer</a> and then <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-2.html" target="_blank">adding full-screen and device-tilt navigation</a>, today we’re going to extend our UI to allow viewing of multiple models.</p>
<p>Firstly it’s worth pointing out that for models to be accessible by the viewer that makes use of my client credentials, I also need to upload that content with the same credentials. You can follow the procedure in <a href="http://through-the-interface.typepad.com/through_the_interface/2014/08/building-a-web-based-viewer-using-the-autodesk-view-data-api-part-1.html" target="_blank">this previous post</a> to see how you do that, although I believe the ADN team has created <a href="https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples" target="_blank">some samples</a> that help simplify the process, too.</p>
<p>Once you have the Base64 document IDs for your various models, it’s pretty simple to abstract the code to work on an arbitrary model. The main caveat is that there may be custom behaviours you want for particular models. For instance there are models for which the up direction is the Z-axis rather than the Y-axis (mainly because the translation process isn’t perfect or at least wasn’t when the model was processed)&#0160; or for which you may want to save a custom view.</p>
<p>We take care of this in the below code by providing a couple of optional arguments to our launchViewer() function that can be used to specify an up direction and an initial zoom for particular models.</p>
<p>And that’s pretty much all this version of the code does beyond yesterday’s. Here’s the main modified section – you can, of course, just take a look at <a href="http://safe-reef-1847.herokuapp.com/js/stereo-multimodel.js" target="_blank">the complete file</a>.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">var</span> viewerLeft, viewerRight;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> updatingLeft = <span style="color: blue;">false</span>, updatingRight = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> leftLoaded = <span style="color: blue;">false</span>, rightLoaded = <span style="color: blue;">false</span>, cleanedModel = <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> leftPos, baseDir, upVector;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> initZoom;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> Commands() { }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.morgan = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1NwTTNXNy5mM2Q=&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1),</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; zoom(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viewerLeft,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -48722.5, -54872, 44704.8,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 10467.3, 1751.8, 1462.8</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.robot_arm = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1JvYm90QXJtLmR3Zng=&#39;</span>&#0160;&#0160;&#0160;</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.chassis = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL0NoYXNzaXMuZjNk&#39;</span></p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.front_loader = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL0Zyb250JTIwTG9hZGVyLmR3Zng=&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1)</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.suspension = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1N1c3BlbnNpb24uaXB0&#39;</span></p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Commands.V8_engine = <span style="color: blue;">function</span> () {</p>
<p style="margin: 0px;">&#0160; launchViewer(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #a31515;">&#39;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c3RlYW1idWNrL1Y4RW5naW5lLnN0cA==&#39;</span></p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">};</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> initialize() {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Populate our initial UI with a set of buttons, one for each</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// function in the Commands object</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> panel = document.getElementById(<span style="color: #a31515;">&#39;control&#39;</span>);</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">for</span> (<span style="color: blue;">var</span> fn <span style="color: blue;">in</span> Commands) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">var</span> button = document.createElement(<span style="color: #a31515;">&#39;div&#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; button.classList.add(<span style="color: #a31515;">&#39;cmd-btn&#39;</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Replace any underscores with spaces before setting the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// visible name</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; button.innerHTML = fn.replace(<span style="color: #a31515;">&#39;_&#39;</span>, <span style="color: #a31515;">&#39; &#39;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; button.onclick = (<span style="color: blue;">function</span> (fn) {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">function</span> () { fn(); };</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; })(Commands[fn]);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Add the button with a space under it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; panel.appendChild(button);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; panel.appendChild(document.createTextNode(<span style="color: #a31515;">&#39;\u00a0&#39;</span>));</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> launchViewer(docId, upVec, zoomFunc) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Assume the default &quot;world up vector&quot; of the Y-axis</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// (only atypical models such as Morgan and Front Loader require</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// the Z-axis to be set as up)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; upVec =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">typeof</span> upVec !== <span style="color: #a31515;">&#39;undefined&#39;</span> ?</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; upVec :</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> THREE.Vector3(0, 1, 0);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Ask for the page to be fullscreen</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// (can only happen in a function called from a</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// button-click handler or some other UI event)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; requestFullscreen();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Hide the controls that brought us here</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> controls = document.getElementById(<span style="color: #a31515;">&#39;control&#39;</span>);</p>
<p style="margin: 0px;">&#0160; controls.style.visibility = <span style="color: #a31515;">&#39;hidden&#39;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Bring the layer with the viewers to the front</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// (important so they also receive any UI events)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> layer1 = document.getElementById(<span style="color: #a31515;">&#39;layer1&#39;</span>);</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> layer2 = document.getElementById(<span style="color: #a31515;">&#39;layer2&#39;</span>);</p>
<p style="margin: 0px;">&#0160; layer1.style.zIndex = 1;</p>
<p style="margin: 0px;">&#0160; layer2.style.zIndex = 2;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Store the up vector in a global for later use</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; upVector = <span style="color: blue;">new</span> THREE.Vector3().copy(upVec);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// The same for the optional Initial Zoom function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (zoomFunc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; initZoom = zoomFunc;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get our access token from the internal web-service API</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; $.get(<span style="color: #a31515;">&#39;http://&#39;</span> + window.location.host + <span style="color: #a31515;">&#39;/api/token&#39;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">function</span> (accessToken) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Specify our options, including the provided document ID</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> options = {};</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.env = <span style="color: #a31515;">&#39;AutodeskProduction&#39;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.accessToken = accessToken;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; options.document = docId;</p>
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
</div>
<p>When you launch <a href="http://safe-reef-1847.herokuapp.com/stereo.html" target="_blank">the HTML page</a> it looks a bit different from last time, but only in the fact there’s now a choice of models to select from.</p>
<p>Here’s a slightly faked view of the UI on a mobile device (I’ve combined two screenshots to get the full UI on one screen):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d07e617c970c-pi" target="_blank"><img alt="The choice of models" border="0" height="460" src="/assets/image_416572.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border: 0px;" title="The choice of models" width="204" /></a></p>
<p>We’ve seen plenty of the Morgan model, but here’s a quick taste of the others. There isn’t currently a back button in the UI, so you’ll have to reload the page to switch between models.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d07e6150970c-pi" target="_blank"><img alt="Robot Arm" border="0" height="238" src="/assets/image_788927.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Robot Arm" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d07e6155970c-pi" target="_blank"><img alt="Front Loader" border="0" height="238" src="/assets/image_350053.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Front Loader" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c6f46043970b-pi" target="_blank"><img alt="Suspension" border="0" height="238" src="/assets/image_959749.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Suspension" width="394" /></a></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d07e6165970c-pi" target="_blank"><img alt="V8 Engine" border="0" height="238" src="/assets/image_447766.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="V8 Engine" width="394" /></a></p>
<p>I haven’t included the “Chassis” model, here: for some reason this looks great on my PC but is all black on my Android device. I’m not sure why, but I’ve nonetheless left it in the model list, for now.</p>
<p>I’ve now arrived in San Francisco and have been finally able to test with DODOcase’s Google Cardboard viewer. And it looks really good! I was expecting to have to tweak the camera offset, but that seems to be fine. I was also concerned I’d need to put a spherical warp on each viewer to compensate for lens distortion, but honestly that seems unnecessary, too. Probably because we’re dealing with a central object view rather than walking through a scene.</p>
<p>I have to admit to finding the experience quite compelling. If you’re coming to AU or to the upcoming DevDays tour then you’ll be able to see for yourself there. Assuming you don’t want to buy or build your own and try it in the meantime, of course. :-)</p>
