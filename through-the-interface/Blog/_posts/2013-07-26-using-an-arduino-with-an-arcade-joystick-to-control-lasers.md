---
layout: "post"
title: "Using an Arduino with an arcade joystick to control lasers"
date: "2013-07-26 07:40:00"
author: "Kean Walmsley"
categories:
  - "Arduino"
original_url: "https://www.keanw.com/2013/07/using-an-arduino-with-an-arcade-joystick-to-control-lasers.html "
typepad_basename: "using-an-arduino-with-an-arcade-joystick-to-control-lasers"
typepad_status: "Publish"
---

<p>As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2013/07/using-an-arduino-with-an-arcade-joystick-to-control-leds.html" target="_blank">the last post</a>, I had some fun ripping apart laser pointer key-rings and repurposing their laser diodes to (at last!) use my Arduino to blink something a bit more interesting than LEDs. This project is really just about blinking lasers using the switches inside a joystick – nothing very complicated. I’m not using servo or stepper motors to create a controllable laser turret or even <a href="http://makezine.com/projects/diy-3d-laser-scanner-using-arduino" target="_blank">my own 3D scanner</a> (both of which sound like interesting future projects ;-).</p>
<p>A quick note on the source of these lasers. I picked up a bunch of <a href="http://www.bmelabandscience.com/store/index.php?main_page=product_info&amp;products_id=68" target="_blank">laser pointer key-ring</a> that a local retailer was about to throw out (the batteries had leaked in many of them). I found a way to rip out the laser diodes, which are rated at 1 mW (which shouldn’t be damaging to the eye, <a href="http://physics.stackexchange.com/questions/3933/why-is-a-1mw-laser-dangerous" target="_blank">as long as your blink reflex kicks in</a>) and take 3.5-4.5V. So I’m using a 50 ohm resistor for each to take the 5V input from the Arduino down to the right range. All fairly simple stuff.</p>
<p>The trickiest part of the project ended up being the connection of the circuit to the ripped-out laser diode components. The approach I settled upon was to use <a href="http://en.wikipedia.org/wiki/Twist_tie" target="_blank">twist ties</a> (the ones that seem to come in all children’s toys, these days) as the primary wires, as they’re both small enough – once stripped back, of course – to fit the breadboard and strong enough to support the weight of the laser. I used some alligator clips to hold and run current to each laser and then taped a simple, separate ground wire to the outside of the laser. Or perhaps I have that backwards – in any case, it worked. :-)</p>
<p>Here’s a photo of the configuration from the front – I could have pointed the lasers in different directions, but left them pointing downwards for the video, in particular.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201901e4ec3ed970b-pi" target="_blank"><img alt="Joystick-controlled lasers" border="0" height="304" src="/assets/image_746250.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Joystick-controlled lasers" width="454" /></a></p>
<p>Here’s a top view, so you can see the breadboard:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201901e4ec477970b-pi" target="_blank"><img alt="A view of the circuit" border="0" height="304" src="/assets/image_445484.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A view of the circuit" width="454" /></a></p>
<p>And here it is in action:</p>
<br /><iframe allowfullscreen="allowfullscreen" frameborder="0" height="264" src="//www.youtube.com/embed/PDd8f2ucePw?rel=0" width="470"></iframe>
<p><em>[I should be in Italy as this post is going live: as mentioned&#0160;last week, I queued up a few posts prior to crossing the Alps for a few days. I&#39;ll be back in the saddle from Monday.]</em></p>
