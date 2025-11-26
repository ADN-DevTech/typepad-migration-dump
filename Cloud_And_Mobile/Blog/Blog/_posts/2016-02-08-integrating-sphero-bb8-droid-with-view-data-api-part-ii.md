---
layout: "post"
title: "Integrating Sphero BB8 Droid with View & Data API - Part II"
date: "2016-02-08 07:43:05"
author: "Philippe Leefsma"
categories:
  - "Cloud"
  - "HTML5"
  - "Philippe Leefsma"
  - "Server"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/02/integrating-sphero-bb8-droid-with-view-data-api-part-ii.html "
typepad_basename: "integrating-sphero-bb8-droid-with-view-data-api-part-ii"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek">@F3lipek</a></p>
<p>In the <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/01/integrating-sphero-bb8-droid-with-view-data-api-part-i.html">first par</a>t of this on-going series I was taking a look at controlling the Sphero BB8 droid from a local webpage just to get my feet in the plate. I spent the last couple weeks working on a better&nbsp;IoT oriented sample&nbsp;using a more realistic architecture.&nbsp;</p>
<p>Here is the idea, there are three main components to the project:</p>
<ul>
<li>A node.js server sitting&nbsp;in the cloud and listening for incoming connections</li>
</ul>
<ul>
<li>Local controllers running an instance of a node.js script which connects to the cloud server. Those controllers can be any device able to run node.js code and with a bluetooth BLE-capable adapter. It&nbsp;can scan for surrounding BB8 devices and send back that information to the server</li>
</ul>
<ul>
<li>A webpage served by the same cloud application&nbsp;that allows a user to visualise each controller and each device, then connect to any device and send commands to it through the appropriate controller</li>
</ul>
<p>The diagram below illustrates further the concept. By the way I was using&nbsp;<a href="https://www.draw.io">https://www.draw.io</a>&nbsp;to quickly draft it and it looks really handy:</p>
<p><a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19cd9e6970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d19cd9e6970c image-full img-responsive" style="margin: 0px 5px 5px 0px;" title="IoT" src="/assets/image_431a4b.jpg" alt="IoT" border="0" /></a></p>
<p>&nbsp;</p>

<p>The connection between server and controllers is achieved using socket.io, so pretty standard node.js event oriented implementation. The most remarkable feature - if I may say so - is that I wrote the code using Es7 async feature, so it needs to be transpiled, which can be done on the fly when running the scripts. It is also highly promisified in order to get rid of the callbacks and leverage the async syntax.</p>
<p>The implementation is mostly contained in some "service" classes on the server and the controller, which are then used inside thin wrappers. Below are the implementations of the <strong><em>IoTSvc</em></strong> (server side) and the <strong><em>SpheroSvc</em></strong> (controller side):</p>


<script src="https://gist.github.com/leefsmp/f81fa3d077eb85d5c3cd.js"></script>

<script src="https://gist.github.com/leefsmp/2e2d75cf729f3a3e3363.js"></script>

The UI and interaction with the viewer is the part I have a bit left behind at the moment: the model is actually not being updated if several devices are connected and no feedback is sent from the device to update the location in the 3d space.
<br>
<br>
It only exposes a panel which allows to visualise connected controllers and available devices, as well as a control panel for the BB8 device. The last enhancement was to play with automated path, so the droid is moving along a predefined path, in that case a square trajectory.

<br>
<br>


<a class="asset-img-link"  style="" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c812bc37970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c812bc37970b image-full img-responsive" alt="Screen Shot 2016-02-08 at 16.41.36" title="Screen Shot 2016-02-08 at 16.41.36" src="/assets/image_c253cb.jpg" border="0" style="margin: 0px 5px 5px 0px;" /></a>

<br>

The next goal would be to play with data acquisition feature of the device (position, velocity, ...) and see if it can send some useful information in order to adapt trajectories for example. Also implement basic intelligence, thinking about contouring obstacles, sending video stream using HTML5 APIs is as well on my plate ...

<br>
<br>

You can grab the whole code from <a href="https://github.com/leefsmp/bb8">https://github.com/leefsmp/bb8</a>

<br>
<br>

That little recording was shot in the office this morning, with the help of <a href="http://through-the-interface.typepad.com/through_the_interface/about-the-author.html">Kean</a>'s droid. Both devices are being controlled from <a href="https://iotea.herokuapp.com">https://iotea.herokuapp.com</a> and affected to a square trajectory

<br>
<br>

<iframe width="560" height="315" src="https://www.youtube.com/embed/AOCUGtLOGuw" frameborder="0" allowfullscreen></iframe>
