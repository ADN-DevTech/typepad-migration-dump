---
layout: "post"
title: "Integrating Paper.js with Leap Motion using JavaScript"
date: "2013-07-22 07:22:00"
author: "Kean Walmsley"
categories:
  - "Graphics system"
  - "HTML"
  - "JavaScript"
  - "Leap Motion"
original_url: "https://www.keanw.com/2013/07/integrating-paperjs-with-leap-motion-using-javascript.html "
typepad_basename: "integrating-paperjs-with-leap-motion-using-javascript"
typepad_status: "Publish"
---

<p>Today marks the public release of the Leap Motion controller: people who pre-ordered devices will start to receive them today, so it seemed a good time to post something related to it.</p>
<p>Last week I came across a really interesting JavaScript library called <a href="http://paperjs.org" target="_blank">Paper.js</a>. According to its website, it’s “<em>an open source vector graphics scripting framework that runs on top of the HTML5 Canvas. It offers a clean Scene Graph / Document Object Model and a lot of powerful functionality to create and work with vector graphics and bezier curves, all neatly wrapped up in a well designed, consistent and clean programming interface.</em>”</p>
<p>If you want to validate this assertion, just take a look at <a href="http://paperjs.org/examples" target="_blank">these examples</a>, clicking down through the list and checking out the source code using the link in the top-right of each page. Very cool stuff.</p>
<p>For instance, here’s <a href="http://paperjs.org/examples/path-intersections" target="_blank">a really neat example</a> – especially relevant to people who care about CAD – of intersections between “paths” calculated dynamically:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201901e52e51e970b-pi" target="_blank"><img alt="Intersecting paths courtesy of Paper.js" border="0" height="262" src="/assets/image_291872.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Intersecting paths courtesy of Paper.js" width="470" /></a></p>
<p>Given the fact there’s a JavaScript library available for the Leap Motion controller called <a href="http://js.leapmotion.com" target="_blank">LeapJS</a>, it seemed like a fun idea to hook the two together. I’m fairly familiar with the LeapJS implementation – having created a few (as yet unpublished) prototypes to make use of AutoCAD 2014’s JavaScript API – so I just needed to take a look at what needed to be done with Paper.js.</p>
<p>Paper.js provides a scripting environment known as <a href="http://paperjs.org/tutorials/getting-started/working-with-paper-js" target="_blank">PaperScript</a>. It’s based on JavaScript but adds additional capabilities to the language and abstracts away boilerplate code. Most of the posted Paper.js examples are in PaperScript, so there’s a bit more work to do if we’re going to adapt any to work from plain old JavaScript.</p>
<p>The example I chose to hook up to Leap Motion is called “Lines” and is part of the Paper.js download (I’m using <a href="http://paperjs.org/download/paperjs-v0.9.8.zip" target="_blank">v0.9.8</a>). Any of the samples in the “examples\Animated” folder would probably be good candidates for integrating with Leap Motion, though.</p>
<p>The first step was to get the PaperScript code working in JavaScript, as per the instructions in <a href="http://paperjs.org/tutorials/getting-started/using-javascript-directly" target="_blank">this tutorial</a>. This mostly involved adding a few manual calls to the Paper.js framework and changing the PaperScript code to replace its (very convenient) arithmetic operations between points (etc.) with more manual approaches such as adding respective X and Y values together and using trigonometry to calculate the 2D vector from a distance at a certain angle. Not that hard to do, but it needed doing.</p>
<p>Here’s the HTML page with embedded JavaScript code that integrates LeapJS with Paper.js:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;!</span><span style="line-height: 140%; color: maroon;">DOCTYPE</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">&lt;!-- Reference the Paper.js and Leap.js libraries (minified</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; versions of each also exist --&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;js/paper.js&quot;&gt;&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;js/leap.js&quot;&gt;&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Global variables</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> amount = 45; </span><span style="line-height: 140%; color: #006400;">// The number of lines that make up the ball</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ballSize = 200; </span><span style="line-height: 140%; color: #006400;">// The initial size of the ball</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> position; </span><span style="line-height: 140%; color: #006400;">// The last position of the ball</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> cursor;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// The position of the cursor (also set by Leap)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> children; </span><span style="line-height: 140%; color: #006400;">// The lines that make up the ball</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// As we&#39;re in JavaScript (rather than PaperScript), we need to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// jump through a few extra hoops to get Paper.js working</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; paper.install(window);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Our iterate function that does the bulk of the work</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> iterate(count) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// We need to calculate deltas between points manually</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// (PaperScript provides arithmetic operators on - for</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// instance - points, which eases this pain)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Find out how the cursor has moved since the last</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// iteration (and move it a tenth of the distance along</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// that path, presumably for smoother animation)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> deltaX = cursor.x - position.x;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> deltaY = cursor.y - position.y;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; position.x += deltaX / 10;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; position.y += deltaY / 10;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Loop through the lines in the ball and adjust them</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> i = 1; i &lt; amount; i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> path = children[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> length = Math.abs(Math.sin(i + count / 40) * ballSize);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> am2pi = 2 * Math.PI / amount; </span><span style="line-height: 140%; color: #006400;">// Common value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Trigonometric operation needs to be done manually</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// (PaperScript allows you to add vectors defined by</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// angle and length)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; path.segments = [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Point(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position.x + deltaX / 1.5, position.y + deltaY / 1.5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Point(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position.x + length * Math.sin(i * am2pi),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position.y + length * Math.cos(i * am2pi)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Point(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position.x + length * Math.sin((i + 1) * am2pi),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position.y + length * Math.cos((i + 1) * am2pi)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// The colour varies based on the size of the line</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// (but shouldn&#39;t depend on the size of the ball)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; path.fillColor.hue = count - (length * 200 / ballSize);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Our main startup function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; window.onload = </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Define the Canvas element as our target</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; paper.setup(</span><span style="line-height: 140%; color: maroon;">&#39;myCanvas&#39;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Assign (initial) values to our globals</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; position = view.center;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; cursor = view.center;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; children = project.activeLayer.children;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Create the lines in our ball</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> i = 0; i &lt; amount; i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> path = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Path({</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fillColor: {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hue: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; saturation: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; brightness: Math.random()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; closed: </span><span style="line-height: 140%; color: blue;">true</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; });</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// We need a tool to intercept our mouse movements</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> tool = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Tool();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; tool.onMouseMove = </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (event) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; iterate();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cursor = event.point;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// We also iterate when the mouse isn&#39;t moving</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; view.onFrame = </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (event) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; iterate(event.count);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// If the Leap Motion library is available, set up our</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Leap loop. Will only be called if a controller is</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// available, though</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">typeof</span><span style="line-height: 140%;"> Leap !== </span><span style="line-height: 140%; color: maroon;">&quot;undefined&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Setup Leap loop with frame callback function</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; Leap.loop(</span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (frame) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// We just want to get the first hand</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (frame.hands.length &gt; 0) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> hand = frame.hands[0];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Define the size of the ball based on the height of</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// the hand</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ballSize = hand.sphereCenter[1] * 3;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// And we set the location of the ball - relative to</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// the center of the view - based on the position of the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// hand in Leap space</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cursor =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Point(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hand.sphereCenter[0] * 4 + view.size.width / 2,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; hand.sphereCenter[2] * 4 + view.size.height / 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; })</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">body</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">style</span><span style="line-height: 140%; color: blue;">=&#39;</span><span style="line-height: 140%; color: red;">background-color</span><span style="line-height: 140%; color: blue;">:black&#39;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">canvas</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">id</span><span style="line-height: 140%; color: blue;">=&quot;myCanvas&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">resize</span><span style="line-height: 140%; color: blue;">&gt;&lt;/</span><span style="line-height: 140%; color: maroon;">canvas</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">body</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
<p>You should be able to see it in action below or by opening <a href="http://through-the-interface.typepad.com/files/LeapPaper.htm" target="_blank">this link</a> (assuming you have <a href="http://paperjs.org/about/#browser-support" target="_blank">a compatible browser</a>, which means it won&#39;t work with IE8 and below… if using IE9 or above and don’t see anything, make sure you don’t have “compatibility mode” enabled for this site). The page doesn’t actually require a Leap Motion controller to work – if you move your mouse cursor over the window, the ball of lines should react. Using Leap Motion you can not only move the ball around but also change its size by adjusting the elevation of your hand.</p>
<br /><iframe frameborder="0" height="400" marginheight="0" marginwidth="0" scrolling="no" src="https://through-the-interface.typepad.com/files/LeapPaper.htm" width="470"></iframe>  <br />  <br />
<p>It was a fun mini-project to get these two JavaScript libraries working in the same HTML page. I’m definitely planning to do more with Paper.js as the opportunity arises (and will inevitably work more with Leap Motion, too: they seem to have made solid progress with their runtime in recent weeks/months, and it’ll certainly be interesting to see how people respond to the product’s release).</p>
<p><em>[A reminder that I&#39;m on holiday for the whole of this week. I have a couple of posts queued up for the rest of the week about an Arduino project I&#39;ve been fooling around with.]</em></p>
<p><em><strong>Update:</strong></em></p>
<p>See <a href="http://through-the-interface.typepad.com/through_the_interface/2013/08/more-fun-with-leapjs-and-paperjs.html" target="_blank">this post</a>. It turns out you can make use of JavaScript libraries directly from PaperScript, which means a lot of the work performed for this post is redundant. Ho hum.</p>
