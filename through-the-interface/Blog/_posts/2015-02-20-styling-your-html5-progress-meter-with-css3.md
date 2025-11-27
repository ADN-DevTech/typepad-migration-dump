---
layout: "post"
title: "Styling your HTML5 progress meter with CSS3"
date: "2015-02-20 08:51:09"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "HTML"
  - "JavaScript"
  - "Runtime"
  - "User interface"
original_url: "https://www.keanw.com/2015/02/styling-your-html5-progress-meter-with-css3.html "
typepad_basename: "styling-your-html5-progress-meter-with-css3"
typepad_status: "Publish"
---

<p>After <a href="http://through-the-interface.typepad.com/through_the_interface/2015/02/creating-your-own-autocad-progress-meter-using-html5-and-javascript.html" target="_blank">yesterday’s fun with creating an HTML5-based progress meter for AutoCAD</a>, today we’re going to have some more fun styling it with CSS.</p>
<p>To recap, here’s the progress meter that comes “out of the box”, with the default styling from Chromium on Windows.</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7509ce4970b-pi" target="_blank"><img alt="Original progress meter" height="55" src="/assets/image_144608.jpg" style="display: inline;" title="Original progress meter" width="470" /></a> <br /> </p>
<p>The first thing we need to do for our various changes is to use CSS to disable the default styling, at which point we can then use CSS to override it.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">-webkit-appearance</span>: <span style="color: blue;">none</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
</div>
<p>Here’s how our progress meter looks when unstyled:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07f44cfc970d-pi" target="_blank"><img alt="Unstyled progress meter" height="55" src="/assets/image_126706.jpg" style="display: inline;" title="Unstyled progress meter" width="470" /></a> <br /> </p>
<p>Now that it’s stripped bare, we can apply some styling to make it look as we want. For inspiration I started with some code from <a href="http://css-tricks.com/html5-progress-element/" target="_blank">this page</a>:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress[value]::-webkit-progress-value</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-image</span>:</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-45deg,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">transparent</span> <span style="color: blue;">33%,</span> <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">33%,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">66%,</span> <span style="color: blue;">transparent</span> <span style="color: blue;">66%</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">top,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">.25),</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.25)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(left,</span> <span style="color: blue;">#09c,</span> <span style="color: blue;">#f44)</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border-radius</span>: <span style="color: blue;">2px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-size</span>: <span style="color: blue;">35px</span> <span style="color: blue;">20px</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
</div>
<p>Which makes it nice and colorful, but for some reason makes me think of <a href="http://licoricelover.blogspot.ch/2010/06/red-white-blue-licorice-for-july-4th.html" target="_blank">licorice</a>.</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7509cf0970b-pi" target="_blank"><img alt="Colourful progress meter" height="55" src="/assets/image_585485.jpg" style="display: inline;" title="Colourful progress meter" width="470" /></a> <br /> </p>
<p>So to see what difference certain tweaks make, I changed the first colour in the 2nd “-webkit-linear-gradient” to #ccc and the second to #fff. This washed the colours away:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7509cf4970b-pi" target="_blank"><img alt="Monochrome progress meter" height="55" src="/assets/image_295727.jpg" style="display: inline;" title="Monochrome progress meter" width="470" /></a> <br /> </p>
<p>For the next iteration I borrowed from <a href="http://cssdeck.com/labs/styling-html5-progress-bars" target="_blank">this page</a>, which helped add some warmth back in:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d0d9f8b0970c-pi" target="_blank"><img alt="Nice progress meter" height="55" src="/assets/image_468394.jpg" style="display: inline;" title="Nice progress meter" width="470" /></a> <br /> </p>
<p>From there I decided to try the blue-within-blue (yes, that’s a <a href="http://en.wikipedia.org/wiki/Dune_%28novel%29" target="_blank">Dune</a> reference ;-) styling used by AutoCAD’s standard progress meter. I didn’t manage to get the exact colours, but they look OK:</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7509cfa970b-pi" target="_blank"><img alt="Blue progress meter" height="55" src="/assets/image_247231.jpg" style="display: inline;" title="Blue progress meter" width="470" /></a> <br /> </p>
<p>Then I decided to round off the corners, which is a simple matter of setting the border-radius to 50px in the main progress meter style but also to add a style for the background doing much the same:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress::-webkit-progress-bar</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background</span>: <span style="color: blue;">gray</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border-radius</span>: <span style="color: blue;">50px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">padding</span>: <span style="color: blue;">0px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">box-shadow</span>: <span style="color: blue;">0</span> <span style="color: blue;">1px</span> <span style="color: blue;">0px</span> <span style="color: blue;">0</span> <span style="color: blue;">rgba(255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">0.2)</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
</div>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d0d9f8b2970c-pi" target="_blank"><img alt="Blue progress meter with rounded corners" height="55" src="/assets/image_367296.jpg" style="display: inline;" title="Blue progress meter with rounded corners" width="470" /></a> <br /> </p>
<p>At this point things were shaping up nicely. The final touch was to replicate the same kind of <a href="http://en.wikipedia.org/wiki/Barber%27s_pole" target="_blank">barber’s pole</a> striping to the background, making it look like a striped reservoir filling with water. Overall it’s an effect I like a lot. And you can very easily tweak this to better suit your own application’s requirements, of course.</p>
<p><br /><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c7509d04970b-pi" target="_blank"><img alt="Final progress meter" height="55" src="/assets/image_331185.jpg" style="display: inline;" title="Final progress meter" width="470" /></a> <br /> </p>
<p>Here’s the completed CSS which can be pasted into the &lt;style&gt; element in yesterday’s post (or placed in its own file and referenced from the HTML page, if you prefer that approach):</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress[value]::-webkit-progress-value</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-image</span>:</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-45deg,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">transparent</span> <span style="color: blue;">33%,</span> <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">33%,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">66%,</span> <span style="color: blue;">transparent</span> <span style="color: blue;">66%</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">top,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">.25),</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.25)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(left,</span> <span style="color: blue;">#335EC4,</span> <span style="color: blue;">#1F71F4)</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border-radius</span>: <span style="color: blue;">50px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-size</span>: <span style="color: blue;">35px</span> <span style="color: blue;">20px</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress::-webkit-progress-bar</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-image</span>:</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-45deg,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">transparent</span> <span style="color: blue;">33%,</span> <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">33%,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.1)</span> <span style="color: blue;">66%,</span> <span style="color: blue;">transparent</span> <span style="color: blue;">66%</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">top,</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">255,</span> <span style="color: blue;">.25),</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">rgba(0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">0,</span> <span style="color: blue;">.25)</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">)</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">-webkit-linear-gradient(left,</span> <span style="color: blue;">#333,</span> <span style="color: blue;">#666)</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">border-radius</span>: <span style="color: blue;">50px</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">background-size</span>: <span style="color: blue;">35px</span> <span style="color: blue;">20px</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>, <span style="color: blue;">100%</span> <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">body</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">overflow</span>: <span style="color: blue;">hidden</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">98%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">height</span>: <span style="color: blue;">98%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">hidden</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">display</span>: <span style="color: blue;">none</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">progress</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">-webkit-appearance</span>: <span style="color: blue;">none</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.td-center</span>&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-align</span>: <span style="color: blue;">center</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.td-right</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">text-align</span>: <span style="color: blue;">right</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">.center-div</span>&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">width</span>: <span style="color: blue;">100%</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">padding</span>: <span style="color: blue;">25%</span> <span style="color: blue;">0</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: maroon;">div</span> {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-family</span>: <span style="color: blue;">&#39;Segoe UI&#39;</span>, <span style="color: blue;">Tahoma</span>, <span style="color: blue;">Geneva</span>, <span style="color: blue;">Verdana</span>, <span style="color: blue;">sans-serif</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-size</span>: <span style="color: blue;">large</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: red;">font-weight</span>: <span style="color: blue;">bold</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
</div>
<p>Next week I’m hitting the slopes with my family as the kids are off school, the reason I’m not able to make it across to <a href="http://through-the-interface.typepad.com/through_the_interface/2015/02/real-2015-in-san-francisco-from-february-25-27.html" target="_blank">REAL 2015</a>. Hopefully I’ll get the chance to post to this blog at least once, but there’s some chance I’ll end up taking the whole week off.</p>
