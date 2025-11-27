---
layout: "post"
title: "Using an Arduino with an arcade joystick to control LEDs"
date: "2013-07-24 07:02:00"
author: "Kean Walmsley"
categories:
  - "Arduino"
original_url: "https://www.keanw.com/2013/07/using-an-arduino-with-an-arcade-joystick-to-control-leds.html "
typepad_basename: "using-an-arduino-with-an-arcade-joystick-to-control-leds"
typepad_status: "Publish"
---

<p>Over the long July 4th weekend (always a great time for me, being based in Europe, to catch up on projects free from interruption) I dusted off my Arduino and started playing around with it again. My goal was to hook up my <a href="http://www.play-zone.ch/en/zippyy-arcade-joystick.html" target="_blank">Zippyy arcade joystick</a> to find out how it might be used to control electronics circuits, in this post to control LEDs but later to drive some 1mW lasers I salvaged from some broken toy key-rings.</p>
<p>The joystick was actually simpler than I expected to hook up, in that it’s essentially a collection of 4 switches: one for each of the primary directions (up, down, left, right). You can adjust the joystick’s control plate to limit its movement to 2, 4 or 8 directions, depending on your need. I left it at the default of 8, which basically means that the diagonal corners result in 2 of the switches at a time being in the “on” position.</p>
<p>I used 560 ohm resistors to limit the voltage to each of the LEDs in the circuit – just as I’d done in previous projects <a href="http://through-the-interface.typepad.com/through_the_interface/2013/01/blinking-lights-and-other-revelations.html" target="_blank">to</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2013/02/blinking-lights-with-a-digispark.html" target="_blank">blink</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2013/02/blinking-lights-with-a-netduino-plus-2.html" target="_blank">LEDs</a> – while for the lasers I’ll apparently need to drop the resistance to 50 ohms to get the 5V input voltage down to the expected 3.5-4.5V.</p>
<p>But that’s for a future post: here’s the joystick controlling a set of 4 LEDs positioned to reflect the various directions it’s moved in.</p>
<br /><iframe allowfullscreen="allowfullscreen" frameborder="0" height="264" src="//www.youtube.com/embed/TXM1ujf0Uqo?rel=0" width="470"></iframe>  <br />  <br />
<p>Once again it’s nothing particularly earth-shattering, but I do find I’m getting more comfortable implementing controls in my circuits and it’ll certainly be fun to fire off low-powered lasers based on the movements of a joystick (as it stands my kids enjoy playing around with it even when controlling mere LEDs).</p>
