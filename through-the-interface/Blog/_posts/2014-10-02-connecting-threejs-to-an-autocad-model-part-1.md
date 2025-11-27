---
layout: "post"
title: "Connecting Three.js to an AutoCAD model &ndash; Part 1"
date: "2014-10-02 16:01:37"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "Notification / Events"
  - "Solid modeling"
original_url: "https://www.keanw.com/2014/10/connecting-threejs-to-an-autocad-model-part-1.html "
typepad_basename: "connecting-threejs-to-an-autocad-model-part-1"
typepad_status: "Publish"
---

<p>As part of my preparations for AU, I’ve been extending <a href="http://through-the-interface.typepad.com/through_the_interface/2014/05/javascript-in-autocad-viewing-3d-solids-using-threejs.html" target="_blank">this Three.js integration sample</a> to make it more responsive to model changes: I went ahead and implemented event handlers in .NET – much as we saw in <a href="http://through-the-interface.typepad.com/through_the_interface/2014/09/displaying-the-area-of-the-last-autocad-entity-in-an-html-palette-using-javascript-and-net.html" target="_blank">the last post</a> – to send interaction information through to JavaScript so that it can update the HTML palette view.</p>  <p>The code is in pretty good shape, but I still need to decide whether to post it separately or with the other JavaScript samples I’m working on (I’ll also be showing <a href="http://paperjs.org" target="_blank">Paper.js</a> and <a href="https://github.com/jdan/isomer" target="_blank">Isomer</a> integrations during my AU talk, as well as a special demo bringing <a href="http://shapeshifter.io" target="_blank">ShapeShifter</a> models into AutoCAD).</p>  <p>In the meantime, here’s a screencast of the <a href="http://threejs.org/" target="_blank">Three.js</a> updated integration in action.</p>  <br /><iframe height="294" src="https://screencast.autodesk.com/Embed/f89f6e94-40c1-4270-9589-67d30e1d5a35" frameborder="0" width="470" webkitallowfullscreen="webkitallowfullscreen" allowfullscreen="allowfullscreen"></iframe>  <br />  <br />  <p>My apologies for the sound quality: I’ve managed to lose my external mic and my new MacBook’s internal one pics up a lot of noise from the fan, once it gets going.</p>  <p>Also, if the command-list provided by Screencast is getting in the way, if you go to full-screen mode it should be easier to see what’s going on.</p>
