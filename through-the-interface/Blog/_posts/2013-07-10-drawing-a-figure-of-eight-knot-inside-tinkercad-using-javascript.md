---
layout: "post"
title: "Drawing a figure of eight knot inside Tinkercad using JavaScript"
date: "2013-07-10 06:59:00"
author: "Kean Walmsley"
categories:
  - "JavaScript"
  - "Solid modeling"
  - "Tinkercad"
original_url: "https://www.keanw.com/2013/07/drawing-a-figure-of-eight-knot-inside-tinkercad-using-javascript.html "
typepad_basename: "drawing-a-figure-of-eight-knot-inside-tinkercad-using-javascript"
typepad_status: "Publish"
---

<p>Following on from <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/creating-a-grid-of-columns-inside-tinkercad-using-javascript.html" target="_blank">my initial playing around with Tinkercad and its Shape Script implementation</a>, I started to look more closely into what’s possible with the Gen6 kernel.</p>
<p>I came across <a href="http://blog.tinkercad.com/2013/06/14/sharing_shape_scripts" target="_blank">this recent blog post</a>, which highlights the ability to make Shape Script implementation details public. The Shape Scripts shown in the post looked nice and complex, so I tracked down the model by selecting “Discover” inside <a href="https://tinkercad.com" target="_blank">Tinkercad</a> and searching for “Brandon Cole”, the originator of these very interesting Shape Scripts. He published <a href="https://tinkercad.com/things/eE1W4HRkHTn-shape-scripts" target="_blank">this design</a> about 3 weeks ago, which I went ahead and copied (yes, shamelessly, but at least I’m admitting to it :-) via the “Copy &amp; Tinker” button.</p>
<p>Inside the model, Brandon’s Shape Scripts were visible in the right-sash. Selecting and editing them – as he’d kindly made them public – brought up their underlying code. Brandon used a similar approach for each of them (with some minor implementation differences), the primary difference being the mathematical equations that represent the various shapes he chose to display.</p>
<p>I went looking for additional, interesting mathematical shapes to bring into Brandon’s framework. And so it was that I discovered <a href="http://en.wikipedia.org/wiki/Knot_theory" target="_blank">knot theory</a>, which is <a href="http://en.wikipedia.org/wiki/Knot_(mathematics)" target="_blank">the mathematical study of knots</a>. These are ripe for representation in Tinkercad, as mathematical knots seem generally to be based on the idea of a closed loop being knotted in some way, and – as we found, last time – the exposure of the Gen6 kernel to Shape Scripts currently requires a single mesh or solid to be created (although it is possible to extrude a set of disjoint paths, as we also saw last time).</p>
<p>Brandon has cleverly worked around the limitation of not being able to extrude along a 3D path by meshing the extrusion manually. Very cool stuff.</p>
<p>Anyway, back to knots. It turns out that – beyond the <a href="http://en.wikipedia.org/wiki/Trefoil_knot" target="_blank">Trefoil</a> and <a href="http://en.wikipedia.org/wiki/Torus_knot" target="_blank">Torus</a> that Brandon has already modeled – there are <a href="http://en.wikipedia.org/wiki/List_of_mathematical_knots_and_links" target="_blank">all kinds of mathematical knots</a> with names such as <a href="http://en.wikipedia.org/wiki/Cinquefoil_knot" target="_blank">Cinquefoil</a>, <a href="http://en.wikipedia.org/wiki/7%E2%82%81_knot" target="_blank">Septafoil</a> and <a href="http://en.wikipedia.org/wiki/Stevedore_knot_(mathematics)" target="_blank">Stevedore</a>. I was looking for knots with a published parametric representation – as I want both the equation for the knot’s X, Y, Z coordinates and the equation’s derivative – so I ended up choosing <a href="http://en.wikipedia.org/wiki/Figure-eight_knot_(mathematics)" target="_blank">the Figure 8 knot</a>.</p>
<p>Plugging the knot’s formulae – for each of the X, Y and Z values – into the code was straightforward, but I needed a little help finding their respective derivatives. I ended up using <a href="http://www.derivative-calculator.net" target="_blank">this online calculator</a> (what I wouldn’t have given for one of these, 20+ years ago!), which saved me from the mental contortions I’d have needed to determine them, otherwise.</p>
<p>Here’s the resultant Shape Script code, courtesy of Brandon Cole, with my own edits on the lines marked “Equation” and “Equation Derivative” (in addition to the simple transformation at the end).</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">params = [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;size&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Size&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;float&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 50,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 40</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;id&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;radius&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;displayName&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;Radius&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;type&quot;</span><span style="line-height: 140%;">: </span><span style="line-height: 140%; color: maroon;">&quot;float&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMin&quot;</span><span style="line-height: 140%;">: 1,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;rangeMax&quot;</span><span style="line-height: 140%;">: 10,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;default&quot;</span><span style="line-height: 140%;">: 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">/**</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">* @class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">*/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">XYZ = </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (x, y, z) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x = x;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y = y;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z = z;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">XYZ.prototype = {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; add: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (other) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x += other.x;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y += other.y;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z += other.z;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; sub: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (other) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x -= other.x;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y -= other.y;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z -= other.z;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; scale: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (scale) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x *= scale;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y *= scale;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z *= scale;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; length: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Math.sqrt((</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; normalize: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.scale(1 / </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.length());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; cross: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (other) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ((</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * other.z) - (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * other.y),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * other.x) - (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * other.z),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * other.y) - (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * other.x));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; dot: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (other) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * other.x) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * other.y) +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * other.z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; clone: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; transformByQuat: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (quat) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> vq = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Quaternion(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z, 0.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> v1 = quat.congugate().transformByQuat(vq);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> v2 = quat.transformByQuat(v1);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> v2 = v1.transformByQuat(quat);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(v2.x, v2.y, v2.z);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; toArray: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> [</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">/**</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">* @class</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">*/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Quaternion = </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (x, y, z, w) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x = x;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y = y;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z = z;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w = w;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Quaternion.prototype = {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; congugate: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Quaternion(-</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x, -</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y, -</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z, </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; fromAxisAngle: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (axis, angle) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> halfAngle = angle * 0.5;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> sinAngle = Math.sin(halfAngle);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x = (axis.x * sinAngle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y = (axis.y * sinAngle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z = (axis.z * sinAngle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w = Math.cos(halfAngle);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; transformByQuat: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (quat) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Quaternion(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w * quat.x + </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * quat.w +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * quat.z - </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * quat.y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w * quat.y + </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * quat.w +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * quat.x - </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * quat.z,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w * quat.z + </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * quat.w +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * quat.y - </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * quat.x,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.w * quat.w - </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.x * quat.x -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.y * quat.y - </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.z * quat.z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; },</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; normalize: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> nearest(xyz, list) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> result = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> distance = xyz.clone().sub(list[0]).length();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> i = 1; i &lt; list.length; i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> len = xyz.clone().sub(list[i]).length();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (len &lt; distance) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; distance = len;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; Debug.log(distance)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; result = i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> result;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">/**</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">* @function</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">*/</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> tesselate(mesh, points, derivs, normals, radius) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ndivs = Tess.circleDivisions(radius);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> t = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> circles = [];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> len = points.length;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (t = 0; t &lt; len; t++) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Build the geometry...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> circle = [];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> j = 0; j &lt; ndivs; j++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> alpha = (Math.PI * 2 * (j / ndivs));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> q = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Quaternion().fromAxisAngle(derivs[t], alpha);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> n = normals[t].clone().transformByQuat(q);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> p = points[t].clone().add(n.scale(radius));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; circle.push(p);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; circle.push(circle[0]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; circles.push(circle);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Tesselate the geometry...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (t = 0; t &lt; circles.length - 1; t++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> start = circles[t];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> end = circles[t + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ndx = nearest(start[0], end);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Debug.log(ndx);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> u = 0; u &lt; start.length - 1; u++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt1 = start[u];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt2 = start[u + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt3 = end[(ndx + u) % (end.length - 1)];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt4 = end[(ndx + u + 1) % (end.length - 1)];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; mesh.triangle(pt1.toArray(), pt2.toArray(), pt3.toArray());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; mesh.triangle(pt2.toArray(), pt4.toArray(), pt3.toArray());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> process(params) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> size = params.size;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> radius = params.radius;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ndivs = Tess.circleDivisions(20) * 3;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> mesh = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> Mesh3D();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> t;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt_d;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt_n;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> points = []; </span><span style="line-height: 140%; color: #006400;">// Points positions</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> derivs = []; </span><span style="line-height: 140%; color: #006400;">// Point derivatives</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> normals = []; </span><span style="line-height: 140%; color: #006400;">// Point normals</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Calculate the points and derivatives...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (i = 0; i &lt; ndivs; i++) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Calculate (t)...&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; t = (i / ndivs) * (Math.PI * 2);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation...&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ((2 + Math.cos(2 * t)) * Math.cos(3 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (2 + Math.cos(2 * t)) * Math.sin(3 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.sin(4 * t));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation Derivative...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt_d = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(-3 * (Math.cos(2 * t) + 2) * Math.sin(3 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(2 * t) * Math.cos(3 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 3 * (Math.cos(2 * t) + 2) * Math.cos(3 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(2 * t) * Math.sin(3 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 4 * Math.cos(4 * t));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Scale Equation...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt.scale(size / 4);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Normalize Derivative...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt_d.normalize();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Store...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; points.push(pt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; derivs.push(pt_d);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Calculate the normals...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (i = 0; i &lt; ndivs; i++) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt1 = points[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt2 = pt1.clone().add(derivs[i]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pt3 = ((ndivs - 1) === i) ? points[0] : points[i + 1];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ux = pt2.clone().sub(pt1).normalize();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> uv = pt3.clone().sub(pt1).normalize();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> norm = ux.cross(uv).normalize();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; normals.push(norm);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; points.push(points[0]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; derivs.push(derivs[0]);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; normals.push(normals[0]);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Tesselate...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tesselate(mesh, points, derivs, normals, radius);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Make the solid...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> solid = Solid.make(mesh);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: #006400;">// Rotate 90 degrees around the Z-axis for visibility&#39;s sake</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; solid.transform(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, -1, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 1, 0, 0, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, 0, 1, 0,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; 0, 0, 0, 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> solid;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">};</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>And here’s <a href="https://tinkercad.com/things/88EhQ3TyJB5-knots" target="_blank">the “tinkered” model</a> with an instance of my Shape Script in it (with the other objects moved around to make some space in the workplane):</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20192abd8209a970d-pi" target="_blank"><img alt="To be or knot to be" border="0" height="316" src="/assets/image_320407.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="To be or knot to be" width="470" /></a></p>
