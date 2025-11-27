---
layout: "post"
title: "Creating a grid of columns inside Tinkercad using JavaScript"
date: "2013-07-03 07:56:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "JavaScript"
  - "Solid modeling"
  - "Tinkercad"
original_url: "https://www.keanw.com/2013/07/creating-a-grid-of-columns-inside-tinkercad-using-javascript.html "
typepad_basename: "creating-a-grid-of-columns-inside-tinkercad-using-javascript"
typepad_status: "Publish"
---

<p>A little over a month ago, <a href="http://blog.tinkercad.com/2013/05/18/autodesk_tinkercad" target="_blank">Autodesk acquired Tinkercad</a>. <a href="https://tinkercad.com" target="_blank">Tinkercad</a> is a 3D CAD tool that uses <a href="http://www.khronos.org/webgl" target="_blank">WebGL</a> to display graphics directly in your browser. While this tool is primarily targeted at consumers – it’s proving very popular among the 3D printing community – I thought I’d check it out to understand its customization capabilities.</p>
<p>If you want to get to know the capabilities of the Tinkercad system, I suggest taking a look at <a href="https://tinkercad.com/quests" target="_blank">these step-by-step lessons</a>. I personally just dove right in – the system is very straightforward to learn – but I’m sure there are basics that I’ve missed by doing so. :-)</p>
<p>After creating a new design – which gets assigned a fun random name, in my case “Fantabulous Kup” – you get presented with your blank workplane. On the right-sash, you’ll see the “Shape Scripts” section, from which you can create a Shape Script based on an existing sample or start from scratch. You should definitely check out the samples – there’s some great stuff there – but we’ll just copy and paste from code into an “Empty” Shape Script.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201901e0014dd970b-pi" target="_blank"><img alt="Creating an empty Shape Script" border="0" height="324" src="/assets/image_33807.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Creating an empty Shape Script" width="470" /></a></p>
<p>Once the empty script has been created, we’ll see the JavaScript code that forms the basis of the Shape Script in a window at the bottom left of the screen.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019103f60a12970c-pi" target="_blank"><img alt="Our empty Shape Script" border="0" height="324" src="/assets/image_155239.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our empty Shape Script" width="470" /></a></p>
<p>This JavaScript code will get executed on the server against the Gen6 geometry kernel. For more detailed information on how this all works, check out the <a href="https://tinkercad.com/developer" target="_blank">developer documentation</a> and specifically <a href="https://tinkercad.com/developer/api-procedural.html" target="_blank">the API reference</a>.</p>
<p>Here’s the JavaScript implementation of our simple Shape Script – that creates a grid of square columns – for you to copy &amp; paste into the implementation window:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">params = [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;cols&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Columns&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;int&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;rows&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Rows&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;int&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;size&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Size&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;int&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;gap&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Gap&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;int&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; { </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;height&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Height&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;float&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 100,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 10</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> process(params) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> c = params.cols;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> r = params.rows;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> h = params.height;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> s = params.size;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> g = params.gap;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Our array of paths to populate</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> paths;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> i = 0; i &lt; c; i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> j = 0; j &lt; r; j++) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Create a square 2D path at the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// appropriate location</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> path = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Path2D();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> isg = i * (s + g);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> jsg = j * (s + g);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; path.moveTo(isg, jsg);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; path.lineTo(isg + s, jsg);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; path.lineTo(isg + s, jsg + s);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; path.lineTo(isg, jsg + s);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; path.lineTo(isg, jsg);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// If the first item in the array, create it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// otherwise append to it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (i == 0 &amp;&amp; j == 0) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; paths = [path];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; paths = paths.concat(path);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Extrude the paths into our grid</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> solid = Solid.extrude(paths, h);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Flip the shape vertically</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> tm =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 1, 0, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, -1, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, 0, 1, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, 0, 0, 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; solid.transform(tm);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> solid;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Once you’ve pasted the code in, if you click the cog you can modify the Shape Script’s name.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019103f60a4c970c-pi" target="_blank"><img alt="Adding a name to our Shape Script" border="0" height="324" src="/assets/image_986538.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Adding a name to our Shape Script" width="470" /></a></p>
<p>At some point, the preview for the Shape Script – based on the default parameters defined in the script – will get displayed in the UI. Even before this happens we can drag the object defined by the script onto our workplane.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20192abbf3e06970d-pi" target="_blank"><img alt="Drag and drop across onto the workplane" border="0" height="324" src="/assets/image_168795.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Drag and drop across onto the workplane" width="470" /></a></p>
<p>You’ll see the parameters for the object get displayed in the “Inspector” window. We can modify these parameters to see the results of our script in action (it gets re-executed on the server whenever a parameter is changed).</p>
<p>Here you’ll see the sliders have been moved across – and the view changed – and the preview has also now been displayed in the right-sash.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019103f60ba9970c-pi" target="_blank"><img alt="Modify the parameters and the view" border="0" height="323" src="/assets/image_720575.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Modify the parameters and the view" width="470" /></a></p>
<p>That’s it for <a href="https://tinkercad.com/things/hOC6YF8xQHP-fantabulous-kup" target="_blank">my first playing around with Tinkercad and Gen6</a>. I’d really like to implement something more complicated, such as <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/turtle-fracta-2.html" target="_blank">a tree object</a> or <a href="http://through-the-interface.typepad.com/through_the_interface/2008/07/turtle-fractals.html" target="_blank">a Hilbert cube</a>, but I can see there are some limitations placed on the modelling operations you can perform (for the first we’d probably need to return or union multiple solids, for the second we’d need to extrude along an arbitrary 3D path, neither of which seem to be possible with the current Shape Script implementation). I really like the way extensibility has been built into the Tinkercad system, though, and am looking forward to doing more with it.</p>
