---
layout: "post"
title: "Integrating Sphero BB8 Droid with View & Data API - Part I"
date: "2016-01-27 02:25:37"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/01/integrating-sphero-bb8-droid-with-view-data-api-part-i.html "
typepad_basename: "integrating-sphero-bb8-droid-with-view-data-api-part-i"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>2016 will likely be the year of <a href="https://en.wikipedia.org/wiki/Internet_of_Things">IoT</a>&nbsp;and after Christmas the new cool kid in town is definitely the <a href="http://store.sphero.com/products/bb-8-by-sphero?gclid=CMv-pNDXycoCFQb3wgodvgcAfA">Sphero BB8</a> bluetooth controlled Droid! So walking in Kean's footsteps who <a href="Controlling robots from inside AutoCAD">controls that robot from AutoCAD</a>, I decided to have a play with it and integrate it with our <a href="https://developer.autodesk.com/api/view-and-data-api/">View &amp; Data API</a>.</p>
<p>Being not much of a designer, I picked a free BB8 model on our Fusion gallery, credits and to thanks <a href="https://gallery.autodesk.com/fusion360/projects/24826/farhans-bb-8-droid">Farhan Suhaime</a>. As Fusion model, this translate pretty good material-wise in View &amp; Data API, see <a href="http://viewer.autodesk.io/node/gallery/embed?id=56a249b0a393afc4057263c3" target="_blank">translated model</a>.</p>
<p>For a first experiment, I kept things pretty simple, the idea is to control the droid through bluetooth connected to a local computer which runs a node local server. Two SDKs are available to do so from node: the <a href="https://www.npmjs.com/package/sphero">official Orbotix JavaScript SDK module</a> and the <a href="https://www.npmjs.com/package/cylon">cylon.js</a> package which exposes supports for many more devices. In the case of Sphero driver, Cylon is actually a very thin wrapper around the Sphero SDK, which has more samples by the way, so I would recommend installing both to take a look at useful examples.</p>
<p>What took me the most time to implement in that first version is the UI for the panel in the web page in order to control the droid. The virtual joystick allows to send 2d movements commands at specified speed and heading. The LED light can be set to any color and programmatically blinked:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1975ed0970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d1975ed0970c img-responsive" title="Screen Shot 2016-01-27 at 11.01.32" src="/assets/image_c6ed34.jpg" alt="Screen Shot 2016-01-27 at 11.01.32" /></a></p>
<p>This allows a user to interact with a REST API that controls the actual droid. The implementation is very basic at the moment:</p>

<script src="https://gist.github.com/leefsmp/4fb90a10081a4d30be2d.js"></script>

The full sample is available from that <a href="https://github.com/leefsmp/bb8">github repository</a>. Below is a quick video that demo the sample in action, it illustrates controlling the LED light and movement of the droid.

<br>
<br>

The interest as far as View & Data is concerned is limited at the moment: the 3D model is translated but doesn't get any feedback from the real droid. It however sets the ground for more advanced usage. Having our droids represented in the viewer could allow the user to easily select one and let it perform actions in case we are dealing with more units and more complex environments. The droid can also stream to the controller information about velocity, so it could potentially update the UI by providing feedback.

<br>
<br>

In the next chapter I will implement a more useful approach where the droid can be controlled remotely from a cloud hosted application. A local node client will connect to that server and send commands to the droid. I'm also thinking taking advantage of the collision detection feature and implement some basic AI to handle obstacles and move around. Stay tuned...

<br>
<br>

<iframe width="600" height="450" src="https://www.youtube.com/embed/CVeZEn4gVQk?feature=oembed" frameborder="0" allowfullscreen></iframe>&nbsp;
