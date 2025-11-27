---
layout: "post"
title: "Autodesk Research and IoT"
date: "2016-06-09 10:29:35"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Autodesk Research"
  - "IoT"
  - "PaaS"
original_url: "https://www.keanw.com/2016/06/autodesk-research-and-iot.html "
typepad_basename: "autodesk-research-and-iot"
typepad_status: "Publish"
---

<p>One of the projects I’ve been working on – alongside Simon Breslav, a colleague from Autodesk Research Toronto – is a prototype, web-based client for our IoT back-end. I’ll be demoing it next week at the <a href="http://forge.autodesk.com/conference" target="_blank">Forge DevCon</a>. It uses Forge’s viewer component to provide access to sensor readings from within a 3D model.</p> <p>Here are a few sequences of images to give you an idea of what it does. It’s still early days, but we’re steadily working towards feature parity with the desktop-based <a href="http://autodeskresearch.com/projects/dasher" target="_blank">Project Dasher</a>.</p> <p>The first sequence shows how you can view the sensors in a building model: in our case it’s the 210 King Street office of Autodesk Toronto. You can hover over the sensor markers to see their IDs and types, and then select the ones you’re interested in, showing the historical data in separate or combined graphs.</p> <p align="center"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c86afde3970b-pi" target="_blank"><img title="Dasher 360 basic" style="margin: 30px 0px; display: inline" alt="Dasher 360 basic" src="/assets/image_903211.jpg" width="500" height="412"></a></p> <p>The second sequence shows us disabling textures – we’re definitely going to need this once we implement surface shading to display sensor data graphically – and then using a building browser to navigate through the floors, down to individual rooms. </p> <p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb090e7f3a970d-pi" target="_blank"><img title="Dasher 360 navigation" style="float: none; margin: 30px auto; display: block" alt="Dasher 360 navigation" src="/assets/image_779380.jpg" width="500" height="412"></a></p> <p>We have a long way to go, but I’m pleased with the progress we’ve made, so far. We’re making heavy use of TypeScript, which is something I’ve wanted to do for some time. Simon has done a great job of building out the infrastructure, and it’s also looking pretty solid from an architectural perspective.</p> <p>It’s going to be a while before the API powering this client application is available through Forge – we’re still early in that process, so there are no guarantees of when (or even whether) it will be exposed – but hopefully this gives you some idea of where we’re going, aspirationally-speaking.</p> <p>If you’re interested in how any particular feature was implemented, please post a comment. I’ll look at writing a blog post to dive into the topic.</p> <p>And be sure to ask me about the project next week in San Francisco, if you’re attending the DevCon!</p>
