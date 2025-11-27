---
layout: "post"
title: "Over my first HoloLens hurdle"
date: "2016-07-05 10:11:20"
author: "Kean Walmsley"
categories:
  - "Augmented Reality"
  - "HoloLens"
original_url: "https://www.keanw.com/2016/07/over-my-first-hololens-hurdle.html "
typepad_basename: "over-my-first-hololens-hurdle"
typepad_status: "Publish"
---

<p>It took me longer than expected to get HoloLens working, on Friday night. Here are some thoughts that may or not be of use to others when they go through the same process.</p>
<ul>
<li>The initial tutorial was quite fun. A few parts of it were downright entertaining (I loved the coloured triangles, for instance).</li>
<li>Device callibration was straightforward: you go through a few steps where you tell the device more about its position by holding up your finger a few times per eye.</li>
</ul>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb091b1d59970d-pi" target="_blank"><img alt="Finger alignment" border="0" height="285" src="/assets/image_442071.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border: 0px;" title="Finger alignment" width="504" /></a></p>
<ul>
<li>Entering any serious amount of text using HoloLens is painful: you need to gaze at each letter and select it. Which is really tiring, if you choose to “air-tap” each one – especially when you have a longish name and/or password, and need a few attempts to get it right. To avoid airtap-fatigue you should be quick to set up and use the external selection clicker.</li>
<li>When I initially configured the device’s account, I made the huge mistake of choosing “the device belongs to my company” rather than “it belongs to me”. This headed me down the path of entering my Autodesk domain information. Which I eventually did successfully. The problem was that “for security reasons” I had to enter my password one last time, at the end. For some reason this last step didn’t succeed – I kept getting an “invalid account error”, and it even provided a Windows error code (0x800704CF): there’s nothing like an error code to remind you you’re still in Windows ;-). Anyway, nothing I did let me move beyond this point. Hard resetting/rebooting always brought me back to the same prompt. The only way I could eventually go beyond was by <a href="https://developer.microsoft.com/en-us/windows/holographic/reset_or_recover_your_hololens#Perform_a_full_device_recovery" target="_blank">reflashing the device using the Windows Device Recovery Tool</a>: thankfully someone else had done exactly as I did and <a href="http://forums.hololens.com/discussion/920/setup-account-login-problem" target="_blank">posted their question on the forum</a>. Not to be fooled again, next time around I very carefully selected “it’s mine, I tell you!” when asked about device ownership.</li>
</ul>
<p>Now that HoloLens is up and working, it’s pretty cool. I can see that things have moved on significantly since the <a href="http://through-the-interface.typepad.com/through_the_interface/2015/12/hands-on-with-hololens.html" target="_blank">demo I had at AU 2015</a>: spatial mapping is now working nicely, just for instance.</p>
<p>My next step is to go through the HoloLens 101 tutorial to create my first application. I have a specific idea for the stage beyond that, but I’ll talk more about that in a future post.</p>
