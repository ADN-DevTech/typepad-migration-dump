---
layout: "post"
title: "More knots in Tinkercad using JavaScript"
date: "2013-07-17 06:54:00"
author: "Kean Walmsley"
categories:
  - "Geometry"
  - "JavaScript"
  - "Tinkercad"
original_url: "https://www.keanw.com/2013/07/more-knots-in-tinkercad-using-javascript.html "
typepad_basename: "more-knots-in-tinkercad-using-javascript"
typepad_status: "Publish"
---

<p>After working out how generally <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/drawing-a-figure-of-eight-knot-inside-tinkercad-using-javascript.html" target="_blank">to use Brandon Cole’s Shape Script code to display mathematical shapes in Tinkercad</a>, I carried on looking for some interesting functions to “plot”. I ended up finding definitions for two <a href="https://en.wikipedia.org/wiki/Cinquefoil_knot" target="_blank">Cinquefoil knots</a>, specifically the (2,5) and the (5,2) variations (I also tried the (5,1), but that ended up looking just like the (5,2)). <a href="http://web.archive.org/web/20040604232208/http://wwwhome.cs.utwente.nl/~jagersaa/Knopen/IndexP.html" target="_blank">This “page that once was”</a> helped a great deal with the underlying formulae, as of course did <a href="http://www.derivative-calculator.net" target="_blank">the online derivative calculator</a> (Brandon tells me he personally used <a href="http://www.wolframalpha.com/widgets/view.jsp?id=c44e503833b64e9f27197a484f4257c0" target="_blank">Wolfram Alpha</a> to calculate his derivatives, a site <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/wolframalpha-a-computational-knowledge-engine-in-the-cloud.html" target="_blank">I’ve blogged about in the past</a>).</p>
<p>Here are the relevant lines of JavaScript for each of the knots…</p>
<p>Cinquefoil 2,5:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation...&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.cos(2 * t) * (5 + 2 * Math.cos(5 * t)),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.sin(2 * t) * (5 + 2 * Math.cos(5 * t)),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(5 * t));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation Derivative...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt_d =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -10 * Math.cos(2 * t) * Math.sin(5 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(2 * t) * (2 * Math.cos(5 * t) + 2),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.cos(2 * t) * (2 * Math.cos(5 * t) + 5) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 10 * Math.sin(2 * t) * Math.sin(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 10 * Math.cos(5 * t));</span></p>
</div>
<p>Cinquefoil 5,1:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation...&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Math.cos(t) + 2) * Math.cos(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Math.cos(t) + 2) * Math.sin(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -Math.sin(t));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation Derivative...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt_d =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -5 * (Math.cos(t) + 2) * Math.sin(5 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.sin(t) * Math.cos(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 5 * (Math.cos(t) + 2) * Math.cos(5 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Math.sin(t) * Math.sin(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -Math.cos(t));</span></p>
</div>
<p>Cinquefoil 5,2:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation...&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Math.cos(2 * t) + 2) * Math.cos(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (Math.cos(2 * t) + 2) * Math.sin(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -Math.sin(2 * t));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Equation Derivative...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt_d =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> XYZ(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -5 * (Math.cos(2 * t) + 2) * Math.sin(5 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(2 * t) * Math.cos(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 5 * (Math.cos(2 * t) + 2) * Math.cos(5 * t) -</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; 2 * Math.sin(2 * t) * Math.sin(5 * t),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; -2 * Math.cos(2 * t));</span></p>
</div>
<p>&#0160;</p>
<p>Here are the resulting objects inside <a href="https://tinkercad.com/things/6ibNi5l9ZbY-more-knots" target="_blank">the public model with the shared Shape Scripts</a>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20192ac05d858970d-pi" target="_blank"><img alt="Knots, knots, knots" border="0" height="364" src="/assets/image_998263.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Knots, knots, knots" width="470" /></a></p>
