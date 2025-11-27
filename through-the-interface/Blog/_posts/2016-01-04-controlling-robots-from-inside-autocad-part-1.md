---
layout: "post"
title: "Controlling robots from inside AutoCAD &ndash; Part 1"
date: "2016-01-04 17:23:51"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "HTML"
  - "IoT"
  - "JavaScript"
  - "Raspberry Pi"
  - "Robotics"
original_url: "https://www.keanw.com/2016/01/controlling-robots-from-inside-autocad-part-1.html "
typepad_basename: "controlling-robots-from-inside-autocad-part-1"
typepad_status: "Publish"
---

<p>Happy New Year! I hope that those of you who celebrate at this time of year, were able to take a nice, relaxing break. I certainly did. :-)</p>
<p>Anyway, it’s now time for me to ease back into work. But rather than it being an abrupt transition, I’ve decided to take a look at a pet project that I thought would be pretty fun: controlling robots from inside AutoCAD. The thinking is to outline some possibilities for moving virtual robots inside a floorplan drawing and having their real-world, physical counterparts move as instructed. Perhaps along a specific path, perhaps by finding a path based on the current 2D floorplan… we’ll see what makes most sense as we go along.</p>
<p>But let’s start with the basics, such as which robots to use. In the Walmsley household we had a number of them under the tree, this Christmas: I bought a <a href="http://www.sphero.com/starwars">Sphero BB-8</a> and my wife bought a <a href="http://www.lego.com/en-us/mindstorms/products/31313-mindstorms-ev3">LEGO Mindstorms EV3 construction system</a>, both ostensibly “for the kids”. Thankfully it seems I’m also covered by that particular description, so I get to mess around with them now that the actual kids are back in their respective classrooms.</p>
<p>I haven’t cracked open the EV3 set, as yet, but we already had a lot of fun with BB-8, over the break. For those of you who missed the fuss, this is the “real” BB-8 from <em>Star Wars: The Force Awakens</em>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c80249f5970b-pi" target="_blank"><img alt="BB-8" height="236" src="/assets/image_126648.jpg" style="float: none; margin: 50px auto; display: block;" title="BB-8" width="560" /></a></p>
<p>The Sphero version of BB-8 is pretty darn cool. Here’s the marketing video:</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="315" src="https://www.youtube.com/embed/-1Y2WfcCb4M?rel=0&amp;showinfo=0" width="560"></iframe></p>
<p>&#0160;</p>
<p>This little bot is typically controlled by mobile apps that make use of Bluetooth Low Energy, much as its siblings the classic Sphero and the Ollie are. To take control of it from my PC, I decided to use <a href="http://cylonjs.com">Cylon.js</a>, a JavaScript robotics framework that integrates with a number of different robot and Internet of Things platforms. I definitely see more IoT fun in my near future, so it seemed to make sense to use this rather than <a href="https://www.npmjs.com/package/sphero">a Sphero-specific SDK</a> of some kind.</p>
<p>It seems a little quirky to be using a JavaScript framework on a physical PC (or Mac) to send instructions to a nearby device, but there are some likely advantages, down the line: it would certainly make it easier to have the controller physically independent of the robotic environment, having it driven remotely via a web-page or -service on (for instance) a Raspberry Pi with a Node server and a Bluetooth dongle. And in the meantime AutoCAD can host the page or call the service on the same system via localhost.</p>
<p>When getting started, <a href="https://medium.com/@phirate/sphero-bb8-robot-toy-the-missing-manual-5f275f1fae98#.d9cygnj9u">this post</a> helped quite a bit, as well as <a href="https://gist.github.com/jakswa/6d607ceb130ace7f3d0c">this one it points to</a>. To simplify the act of getting BB-8 to work, I also integrated keyboard support into my Cylon code using <a href="https://gist.github.com/deadprogram/c6305a8bee694d75294b">this snippet</a>.</p>
<p>Oh, and just to do something that goes beyond what the existing mobile apps can do, I added support for multiple devices. Here’s some JavaScript code that controls both the new BB-8 and last year’s Christmas present, the Ollie:</p>
<p>&#0160;</p>
<p>
<script src="https://gist.github.com/KeanW/a25b8668894ebab9556d.js"></script>
</p>
<p>&#0160;</p>
<p>And here’s the code in action. I cycle through a sequence of four commands – front, left, right, backwards – and as the robots are facing in opposite directions they end up in the same place.</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="315" src="https://www.youtube.com/embed/g1XunTXWLxY?rel=0&amp;showinfo=0" width="560"></iframe></p>
<p>&#0160;</p>
<p>To make this work I applied different durations for the “roll” operations for each type of robot: given its design, the Ollie is somewhat grippier and more responsive, so I used a roll duration of 600ms vs. 1.5 seconds for BB-8.</p>
<p>Now that we can see how easy it is to control these robots from JavaScript, now I have to work out how to plug this into AutoCAD. The step after that will probably involve decomposing AutoCAD “path” geometry into movement operations. I believe these bots can also provide collision detection feedback, too… maybe we can use that to generate a crude map of a floorspace, too!</p>
