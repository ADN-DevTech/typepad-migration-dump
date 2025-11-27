---
layout: "post"
title: "Adding spatial sound to our Unity model in HoloLens &ndash; Part 3"
date: "2016-08-03 16:18:32"
author: "Kean Walmsley"
categories:
  - "Augmented Reality"
  - "HoloLens"
  - "Robotics"
  - "Unity3D"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2016/08/adding-spatial-sound-to-our-unity-model-in-hololens-part-3.html "
typepad_basename: "adding-spatial-sound-to-our-unity-model-in-hololens-part-3"
typepad_status: "Publish"
---

<p>After the first <a href="http://through-the-interface.typepad.com/through_the_interface/2016/07/adding-spatial-sound-to-our-unity-model-in-hololens-part-1.html" target="_blank">two</a> <a href="http://through-the-interface.typepad.com/through_the_interface/2016/07/adding-spatial-sound-to-our-unity-model-in-hololens-part-2.html" target="_blank">parts</a> of this series, where we looked at items 1 &amp; 2 on the below list, it’s time to tackle item 3:</p>
<ol>
<li>A single sound is assigned to our robot
<ul>
<li>When the robot stops completely, so does the sound</li>
</ul>
</li>
<li>The same sound is assigned to each of the robot’s parts
<ul>
<li>When each part stops moving, so does the sound for that part</li>
</ul>
</li>
<li><strong>A different sound is assigned to each of the robot’s parts</strong>
<ul>
<li>When each part stops moving, so does the sound for that part</li>
</ul>
</li>
</ol>
<p>As I mentioned last time, I was fairly happy with the results from the second option. This time I went and added new sounds for two of the larger parts (keeping the same sound for the base of the robot and the smaller parts at the end of the arm). Everything worked well enough from a technical perspective, but I did find the resulting cacophony a little overwhelming. I’m sure that with a better of choice of sound – for this particular scenario – it would work well, but that’s getting into professional sound design territory: I’m personally happy rolling back to where we were at the end of the last post.</p>
<p>Here’s a video of this approach in action, to give you a sense of what I mean.</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube-nocookie.com/embed/de4x2tSdFIw?rel=0&amp;showinfo=0" width="500"></iframe></p>
<p style="text-align: center">&#0160;</p>
<p>I feel that spatial sound has been explored adequately, for now, but I do expect to come back to it: it’s definitely a useful tool for indicating not only the operation of the robot but potentially also collisions with its environment.</p>
<p>In terms of where I want to go next… I want to add some simple scaling capability – to make the model larger or smaller via voice commands – as well as putting some more effort into spatial mapping and placement: I want the user not to be able to put the robot somewhere it does fit, and – ideally – have the robot not hit walls during its gyrations, if it was placed too close to one. And then there’s the dancing piece: I want to hook up a beat detection component and have it send messages to the robot to have it movements affected by the music that’s playing (this is going to be shown at an evening event with a live DJ, so that’ll be really fun).</p>
