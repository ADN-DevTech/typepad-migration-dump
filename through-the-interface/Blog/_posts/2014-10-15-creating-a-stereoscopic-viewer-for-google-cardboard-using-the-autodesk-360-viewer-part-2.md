---
layout: "post"
title: "Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer &ndash; Part 2"
date: "2014-10-15 08:36:00"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "PaaS"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-2.html "
typepad_basename: "creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-2"
typepad_status: "Publish"
---

<p>I’m heading out the door in a few minutes to take the train to Zurich and a (thankfully direct) flight from there to San Francisco. I’ll have time on the flight to write up the next part in the series, so all will be in place for <a href="http://vrhackathon.com" target="_blank">this weekend’s VR Hackathon</a>.</p>
<p>In today’s post we’re going to extend the implementation <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-1.html" target="_blank">we saw yesterday</a> (and <a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/gearing-up-for-the-vr-hackathon.html" target="_blank">introduced on Monday</a>) by adding full-screen viewing and device-tilt navigation.</p>
<p>Full-screen mode is easy: I borrowed some code from <a href="http://stackoverflow.com/questions/7836204/chrome-fullscreen-api" target="_blank">here</a> that works well, the only thing to keep in mind is that the API can only be called in a UI event handler (such as when someone has pressed a button). This is clearly intended to stop naughty pages from forcing you into full-screen mode on load. So we’re adding a single, huge “Start” button to launch the viewer. Nothing particularly interesting, although we do hide – and change the Z-order on – some divs to make an apparently multi-page UI happen via a single HTML file. We’ll extend this approach in tomorrow’s post to show more buttons, one for each hosted model.</p>
<p>Device-tilt support is only a little more involved: the window has a ‘deviceorientation’ event we can listen to that gives us alpha/beta/gamma values representing data coming from the host device’s sensors (presumably the accelerometer and magnetometer). These appear to be given irrespective of the actual orientation (meaning whether it’s in portrait or landscape mode). We’re only interested in landscape mode, so we need to look at the alpha value for the horizontal (left-right) rotation and gamma for the vertical (front-back) rotation. The vertical rotation can be absolute, but we want to fix the left-right rotation based on an initial direction – horizontal rotations after that should be relative to that initial direction.</p>
<p><a href="http://safe-reef-1847.herokuapp.com/stereo-tilt.html" target="_blank">The HTML page</a> hasn’t changed substantially – it has some additional styles, but that’s about it.</p>
<p>Here are the relevant additions to the referenced JavaScript file (I’ve omitted the UI changes and the event handler subscription – you can get <a href="http://safe-reef-1847.herokuapp.com/js/stereo-tilt.js" target="_blank">the full source here</a>).</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">function</span> orb(e) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">if</span> (e.alpha &amp;&amp; e.gamma) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Remove our handlers watching for camera updates,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// as we&#39;ll make any changes manually</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// (we won&#39;t actually bother adding them back, afterwards,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// as this means we&#39;re in mobile mode and probably inside</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// a Google Cardboard holder)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; unwatchCameras();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// Our base direction allows us to make relative horizontal</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">// rotations when we rotate left &amp; right</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (!baseDir)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; baseDir = e.alpha;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (viewerLeft.running &amp;&amp; viewerRight.running) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> deg2rad = Math.PI / 180;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// gamma is the front-to-back in degrees (with</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// this screen orientation) with +90/-90 being</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// vertical and negative numbers being &#39;downwards&#39;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// with positive being &#39;upwards&#39;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> vert = (e.gamma + (e.gamma &lt;= 0 ? 90 : -90)) * deg2rad;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// alpha is the compass direction the device is</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// facing in degrees. This equates to the</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// left - right rotation in landscape</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// orientation (with 0-360 degrees)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> horiz = (e.alpha - baseDir) * deg2rad;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; orbitViews(vert, horiz);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">function</span> orbitViews(vert, horiz) {</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// We&#39;ll rotate our position based on the initial position</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// and the target will stay the same</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> pos = <span style="color: blue;">new</span> THREE.Vector3().copy(leftPos);</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> trg = viewerLeft.navigation.getTarget();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Start by applying the left/right orbit</span></p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// (we need to check the up/down value, though)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> zAxis = <span style="color: blue;">new</span> THREE.Vector3(0, 0, 1);</p>
<p style="margin: 0px;">&#0160; pos.applyAxisAngle(zAxis, (vert &lt; 0 ? horiz + Math.PI : horiz));</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Now add the up/down rotation</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> axis = <span style="color: blue;">new</span> THREE.Vector3().subVectors(pos, trg).normalize();</p>
<p style="margin: 0px;">&#0160; axis.cross(zAxis);</p>
<p style="margin: 0px;">&#0160; pos.applyAxisAngle(axis, vert);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Zoom in with the lefthand view</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; zoom(viewerLeft, pos.x, pos.y, pos.z, trg.x, trg.y, trg.z);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// Get a camera slightly to the right</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> pos2 = offsetCameraPos(viewerLeft, pos, trg, <span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: green;">// And zoom in with that on the righthand view, too</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">var</span> up = viewerLeft.navigation.getCameraUpVector();</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; zoom(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; viewerRight,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; pos2.x, pos2.y, pos2.z,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; trg.x, trg.y, trg.z,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; up.x, up.y, up.z</p>
<p style="margin: 0px;">&#0160; );</p>
<p style="margin: 0px;">}</p>
</div>
<p>So how can we test this? Obviously with a physical device – and I recommend using Chrome on an Android device for best results – or you can choose to use <a href="https://www.google.com/chrome/browser/canary.html" target="_blank">Google Chrome Canary</a> on your PC (whether Mac or Windows). Canary is the codename for the next version of Chrome that’s currently in Beta: I don’t actually know whether the next release is always called Canary, or whether this changes. As you can probably tell, this is the first time I’ve installed it. :-)</p>
<p>Canary currently includes some very helpful developer tools that go beyond what’s in the current stable release of Chrome (which at the time of writing is version 38.0.2125.101 for me, at least). The version of Chrome Canary I have installed is version 40.0.2185.0.</p>
<p>Here’s the main page loaded in Chrome Canary with the enhanced developer tools showing:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c6f33579970b-pi" target="_blank"><img alt="Our page in Chrome Canary" border="0" height="348" src="/assets/image_575341.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Our page in Chrome Canary" width="470" /></a></p>
<p>The important part is the bottom-right pane which includes sensor emulation information. For more information on enabling this (which you do via the blue “mobile device” icon at the top, next to the search icon) check <a href="https://developer.chrome.com/devtools/docs/device-mode" target="_blank">the online Chrome developer docs</a>.</p>
<p>You can either enter absolute values – which is in itself very handy – or grab onto the device and wiggle it around (which helps emulate more realistic device usage, I expect).<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07985938970d-pi"><img alt="Canary device-tilt" height="286" src="/assets/image_762156.jpg" style="margin: 20px 0px; display: inline;" title="Canary device-tilt" width="470" /></a></p>
<p>Again, here’s <a href="http://safe-reef-1847.herokuapp.com/stereo-tilt.html" target="_blank">the page for you to try yourself</a>.</p>
<p>In tomorrow’s post we’ll extend this implementation to look at other models, refactoring some of the UI and viewer control code in the process.</p>
