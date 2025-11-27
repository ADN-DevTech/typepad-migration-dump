---
layout: "post"
title: "Sphere packing in AutoCAD: creating an Apollonian packing using F# &ndash; Part 2"
date: "2012-02-20 06:09:00"
author: "Kean Walmsley"
categories:
  - "3D printing"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Concurrent programming"
  - "F#"
  - "Fractals"
  - "Geometry"
  - "Solid modeling"
original_url: "https://www.keanw.com/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-2.html "
typepad_basename: "sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-2"
typepad_status: "Publish"
---

<p>This post continues the series on fill algorithms for 3D printing by looking specifically at an Apollonian sphere packing. In <a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/sphere-packing-in-autocad-creating-an-apollonian-packing-using-f-part-1.html" target="_blank">the last post</a> we got most of the way there, but today we’re going to introduce a more elegant algorithm for solving the problem (with pretty impressive results :-).</p>
<p>Many thanks to <a href="http://www.scivis.ethz.ch/people/rpeikert/" target="_blank">Professor Ronald Peikert</a> from <a href="http://www.ethz.ch/index_EN" target="_blank">ETH Zurich</a> for kindly providing the C++ code used to generate the F# code in today’s post.</p>
<p>The original algorithm was outlined in <a href="http://graphics.ethz.ch/~peikert/papers/apollonian.pdf" target="_blank">this paper co-authored by Professor Peikert</a>, under the section The “Inversion Algorithm” and – as you might divine from the name – uses <a href="http://en.wikipedia.org/wiki/Inversive_geometry" target="_blank">inversion</a> to simplify the challenge of identifying mutually tangent spheres.</p>
<p>Professor Peikert has also provided <a href="http://through-the-interface.typepad.com/files/Apollonian%20sphere%20packing%20-%20derivation%20of%20special%20coordinate%20system%20and%20inversion%20formula.pdf" target="_blank">notes</a> describing the steps to derive the special coordinates he has used to simplify working with large sets of spheres, taking us through the transformation between Cartesian, inversive and then &quot;special” coordinates.</p>
<p>Here is the F# code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">module</span><span style="line-height: 140%;"> SpherePackingInversionFs</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">mutable</span><span style="line-height: 140%;"> minRad = 0.01</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">mutable</span><span style="line-height: 140%;"> maxGen = 6</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">mutable</span><span style="line-height: 140%;"> clans = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// assign color to clans, not generations</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> draw generation clan A B C D E =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// returns false if the radius is less than minRad</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Convert special coords to inversive coords</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> rt2o2 = sqrt(2.)/2.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> a = (-B + C + D - E) * rt2o2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> b = ( B - C + D - E) * rt2o2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> c = ( B + C - D - E) * rt2o2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> d = A - B - C - D - E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> e = ( B + C + D + E) * sqrt(6.)/2.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Convert inversive coords to Cartesian coords</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Positive radius required for drawing</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> r = </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> e-d &gt; 0. </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> 1./(e-d) </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> 1./(d-e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> x = a*r</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> y = b*r</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> z = c*r</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> r &lt; minRad </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> inversionCircle = generation &lt; 0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> outerCircle = (generation = 0 &amp;&amp; clan = 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> transparent = inversionCircle || outerCircle</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> colorIndex = </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> clans </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> clan </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> (generation + 1)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> transparent </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Check for z &lt; 0 to draw lower half of the packing only</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; [((x, y, z, r), colorIndex)]</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> generateItem item itemVal i A B C D E =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">match</span><span style="line-height: 140%;"> i </span><span style="line-height: 140%; color: blue;">with</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 0 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> item = i </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> -A </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> A + itemVal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 1 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> item = i </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> -B </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> B + itemVal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 2 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> item = i </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> -C </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> C + itemVal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | 3 </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> item = i </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> -D </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> D + itemVal</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; | _ </span><span style="line-height: 140%; color: blue;">-&gt;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> item = i </span><span style="line-height: 140%; color: blue;">then</span><span style="line-height: 140%;"> -E </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> E + itemVal</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">rec</span><span style="line-height: 140%;"> generate generation clan i A B C D E =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> A1 = generateItem 0 A i A B C D E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> B1 = generateItem 1 B i A B C D E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> C1 = generateItem 2 C i A B C D E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> D1 = generateItem 3 D i A B C D E</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> E1 = generateItem 4 E i A B C D E</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: green;">// Avoid duplicates</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (i=0 &amp;&amp; (&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; B1&lt; A1||C1&lt; A1||D1&lt; A1||E1&lt; A1) ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; i=1 &amp;&amp; (A1&lt;=B1||&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; C1&lt; B1||D1&lt; B1||E1&lt; B1) ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; i=2 &amp;&amp; (A1&lt;=C1||B1&lt;=C1||&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; D1&lt; C1||E1&lt; C1) ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; i=3 &amp;&amp; (A1&lt;=D1||B1&lt;=D1||C1&lt;=D1||&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; E1&lt; D1) ||</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; i=4 &amp;&amp; (A1&lt;=E1||B1&lt;=E1||C1&lt;=E1||D1&lt;=E1&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )) </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> res = draw generation clan A1 B1 C1 D1 E1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> res = [] </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; res @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> generation &lt; maxGen </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; List.concat</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (List.map</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: blue;">fun</span><span style="line-height: 140%;"> j </span><span style="line-height: 140%; color: blue;">-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> j &lt;&gt; i </span><span style="line-height: 140%; color: blue;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; generate (generation+1) clan j A1 B1 C1 D1 E1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [0..4])</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; []</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">type</span><span style="line-height: 140%;"> Packer() =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">member</span><span style="line-height: 140%;"> ApollonianGasket steps minR cl =</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; maxGen &lt;- steps</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; minRad &lt;- minR</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; clans &lt;- cl</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Draw initial circles and generate all circles of their clans</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> init =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw 0 0 1. 0. 0. 0. 0. @ generate 1 0 0 1. 0. 0. 0. 0. @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw 0 1 0. 1. 0. 0. 0. @ generate 1 1 1 0. 1. 0. 0. 0. @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw 0 2 0. 0. 1. 0. 0. @ generate 1 2 2 0. 0. 1. 0. 0. @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw 0 3 0. 0. 0. 1. 0. @ generate 1 3 3 0. 0. 0. 1. 0. @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw 0 4 0. 0. 0. 0. 1. @ generate 1 4 4 0. 0. 0. 0. 1.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Draw inversion spheres</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">let</span><span style="line-height: 140%;"> Q = sqrt(3.)/6.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; init @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; draw -1 -1 (-2.*Q) Q Q Q Q @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; draw -1 -1 Q (-2.*Q) Q Q Q @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; draw -1 -1 Q Q (-2.*Q) Q Q @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; draw -1 -1 Q Q Q (-2.*Q) Q @</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; draw -1 -1 Q Q Q Q (-2.*Q)</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>The C# loader code shown last time has been updated to add a new command (AGFS) calling this new F# implementation.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> SpherePackingLoader</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFS&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ApollonianGasket(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFSE&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ExteriorApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ApollonianGasket(</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;AGFSI&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> InteriorApollonianGasket()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ApollonianGasket(</span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CreateLayers(</span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db, </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">LayerTable</span><span style="line-height: 140%;"> lt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">LayerTable</span><span style="line-height: 140%;">)tr.GetObject(db.LayerTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">short</span><span style="line-height: 140%;"> i = 1; i &lt;= 20; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> name = i.ToString();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!lt.Has(name))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">LayerTableRecord</span><span style="line-height: 140%;"> ltr = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">LayerTableRecord</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.Color =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.Colors.</span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromColorIndex(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.Colors.</span><span style="line-height: 140%; color: #2b91af;">ColorMethod</span><span style="line-height: 140%;">.ByAci, i</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.Name = name;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lt.Add(ltr);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(ltr, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ApollonianGasket(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> inverse, </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> exterior = </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> tr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.TransactionManager.StartTransaction();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (tr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CreateLayers(db, tr);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;"> bt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTable</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId, </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForRead</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;"> btr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">)tr.GetObject(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[</span><span style="line-height: 140%; color: #2b91af;">BlockTableRecord</span><span style="line-height: 140%;">.ModelSpace], </span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> res =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; inverse ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SpherePackingInversionFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.ApollonianGasket(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 7, 0.001, </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; exterior ?</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SpherePackingFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.ExteriorApollonianGasket(5)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; :</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">SpherePackingFs</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">Packer</span><span style="line-height: 140%;">.InteriorApollonianGasket(5);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (res.Length &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We get an F# list of tuples containing our circle</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// definitions (no need for a special class)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> tup </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> res)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Our circles are defined in terms of position (x,y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// and curvature (the 3rd item in the nested tuple)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> radius = System.</span><span style="line-height: 140%; color: #2b91af;">Math</span><span style="line-height: 140%;">.Abs(tup.Item1.Item4);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (radius &gt; 0.0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// x, y &amp; z are items 1, 2 &amp; 3 (in the nested tuple)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// respectively</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;"> s = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Solid3d</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.CreateSphere(radius);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;"> center =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tup.Item1.Item1, tup.Item1.Item2, tup.Item1.Item3</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Vector3d</span><span style="line-height: 140%;"> disp = center - </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">.Origin;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.TransformBy(</span><span style="line-height: 140%; color: #2b91af;">Matrix3d</span><span style="line-height: 140%;">.Displacement(disp));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The Layer (and therefore the colour) will be based</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// on the &quot;level&quot; of each sphere</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (item 2 in the top-level tuple)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; s.Layer = tup.Item2.ToString();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.AppendEntity(s);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.AddNewlyCreatedDBObject(s, </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>When we run the AGFS command, we see a beautiful Apollonian packing gets generated in 3D:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20168e7a25580970c-pi" target="_blank"><img alt="Our full 3D Apollonian packing" border="0" height="465" src="/assets/image_629024.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Our full 3D Apollonian packing" width="475" /></a></p>
<p>And if we turn off layer “1”, we effectively hide the four primary internal spheres, allowing a deeper view into the guts of our packing:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2016762a06b64970b-pi" target="_blank"><img alt="And with the layer with the four primary internal spheres turned off" border="0" height="460" src="/assets/image_807350.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="And with the layer with the four primary internal spheres turned off" width="475" /></a></p>
<p>If you’re interested in looking at the results without running the code, <a href="http://through-the-interface.typepad.com/files/Apollonian%20sphere%20packing.dwg" target="_blank">here they are as an AutoCAD 2010 drawing without the enclosing sphere (6.16 MB)</a>.</p>
<p>In the next post, we’ll take the results of this packing algorithm and subtract them from an enclosing sphere, to see what kind of results we get.</p>
<p>There’s one last implementation that I’d like to present before publishing the complete source project (this will hopefully come at the end of the week). The project will also contain a version of today’s post that runs in parallel (as it’s a <span style="text-decoration: underline;">highly</span> parallelizable algorithm :-). While it’s probably a good topic for a future post, I left the code in there for anyone who’s interested, in the meantime.</p>
