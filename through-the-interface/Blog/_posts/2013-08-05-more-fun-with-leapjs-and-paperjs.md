---
layout: "post"
title: "More fun with LeapJS and Paper.js"
date: "2013-08-05 07:14:00"
author: "Kean Walmsley"
categories:
  - "Graphics system"
  - "JavaScript"
  - "Leap Motion"
original_url: "https://www.keanw.com/2013/08/more-fun-with-leapjs-and-paperjs.html "
typepad_basename: "more-fun-with-leapjs-and-paperjs"
typepad_status: "Publish"
---

<p>After <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/integrating-paperjs-with-leap-motion-using-javascript.html" target="_blank">my initial fooling around</a> with combining the <a href="http://js.leapmotion.com" target="_blank">Leap Motion JavaScript library</a> with <a href="http://paperjs.org" target="_blank">Paper.js</a> – and <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/integrating-paperjs-with-leap-motion-using-javascript.html#comment-6a00d83452464869e201901e697195970b" target="_blank">some gentle prompting from Kerry Brown</a> – I started looking at how it might be possible to integrate Leap Motion support into the <a href="http://paperjs.org/examples/meta-balls/" target="_blank">Meta Balls</a> and <a href="http://paperjs.org/examples/voronoi" target="_blank">Voronoi</a> samples from the Paper.js website.</p>
<p>These are both somewhat more complex samples, with Voronoi requiring an external JavaScript library. As usual with the Paper.js samples the posted versions are coded in PaperScript, so I realised I was wrong about needing to code in pure JavaScript to integrate LeapJS with Paper.js. Happy days! :-)</p>
<p>Anyway, so it turns out much of the work I did last time was redundant, even if it was an interesting exercise to convert a PaperScript sample to JavaScript. I therefore decided to take the posted versions of the Lines, Meta Balls and Voronoi samples – all coded in PaperScript rather than pure JavaScript – and create versions that integrate LeapJS in the lightest possible way (i.e. with the code staying in PaperScript).</p>
<p>I made relatively few modifications to the originals, mainly to make them fit better in the width of the blog. Meta Balls and Voronoi were actually slightly trickier to get working than Lines: Lines had a constant onFrame() callback animating the lines on each browser frame that the other two were missing (as they didn&#39;t need to have their graphics animated continuously). Once I added an onFrame() handler to regenerate the graphics on a repeated basis - using a &quot;dirty&quot; flag to keep performance reasonably snappy - both these samples worked well with input from LeapJS. Overall the code additions are nothing at all special – the code to integrate Leap Motion was trivial.</p>
<p>Here they all are, embedded and linked to their source files.&#0160;Just as last time, none of these samples actually need a Leap Motion device to be viewable – they will also respond to mouse movements across their respective windows.</p>
<p>What&#39;s quite cool is that if you are using a Leap Motion controller each of the three embedded views, below, will respond to the hand movements in sync. Although you&#39;ll see smoother performance if you load each page in turn, of course.</p>
<p><a href="http://through-the-interface.typepad.com/files/LeapLines.html" target="_blank">Lines</a></p>
<br /><iframe frameborder="0" height="400" marginheight="0" marginwidth="0" scrolling="no" src="https://through-the-interface.typepad.com/files/LeapLines.html" width="470"></iframe>  <br />  <br />
<p><a href="http://through-the-interface.typepad.com/files/LeapMetaBalls.html" target="_blank">Meta Balls</a></p>
<br /><iframe frameborder="0" height="400" marginheight="0" marginwidth="0" scrolling="no" src="https://through-the-interface.typepad.com/files/LeapMetaBalls.html" width="470"></iframe>  <br />  <br />
<p><a href="http://through-the-interface.typepad.com/files/LeapVoronoi.html" target="_blank">Voronoi</a></p>
<br /><iframe frameborder="0" height="400" marginheight="0" marginwidth="0" scrolling="no" src="https://through-the-interface.typepad.com/files/LeapVoronoi.html" width="470"></iframe>
<p>Fun stuff! At some point I plan to take a look at using Paper.js inside AutoCAD, to see what interesting UI capabilities it brings when implementing a palette-based UI in HTML and JavaScript.</p>
