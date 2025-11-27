---
layout: "post"
title: "VR for design visualization &ndash; Part 2"
date: "2016-03-04 08:23:59"
author: "Kean Walmsley"
categories:
  - "Virtual Reality"
original_url: "https://www.keanw.com/2016/03/vr-for-design-visualization-part-2.html "
typepad_basename: "vr-for-design-visualization-part-2"
typepad_status: "Publish"
---

<p>Thanks for the positive responses to <a href="http://through-the-interface.typepad.com/through_the_interface/2016/03/vr-for-design-visualization-part-1.html" target="_blank">the last post</a>. I can see it’s an interesting topic for people, so I’m going to stick with it for a bit.</p>
<p>I promised some kind of categorisation of where I see existing VR solutions in the design visualization space. These are fairly broad categories that focus on the technology, rather than the user scenario, so hopefully I’ve managed to cover the main ones.</p>
<ul>
<li>Tethered (HTC Vive, Oculus Rift)
<ul>
<li>Realtime graphics
<ul>
<li>Game engine runtime *
<ul>
<li>Lots of people are using this approach, today. One of the main issues continues to be around the content pipeline: getting CAD models into VR – with accurate materials, lighting, etc. – is just hard. There are external companies working on this problem and Autodesk’s Project Expo is our attempt to solve it for the Revit to Stingray (which includes VR) workflow.</li>
</ul>
</li>
<li>Web-based **
<ul>
<li>This is an emerging space… WebVR hasn’t yet hit the mainstream, but it’s of genuine interest. Some game engines are also targeting HTML5/WebGL for deployment, too.</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
<li>Untethered (Google Cardboard, Gear VR)
<ul>
<li>Realtime graphics
<ul>
<li>Game engine runtime *
<ul>
<li>I don’t know enough about the ability of mobile game engine runtimes to support serious CAD data, today. I have to believe this is an area that will be of increasing interest over time, though.</li>
</ul>
</li>
<li>Web-based **
<ul>
<li>This will come, in due course. An early attempt, based on the <a href="https://developer.autodesk.com/api/view-and-data-api/" target="_blank">View &amp; Data API</a> – and not strictly WebVR – can be found here: <a href="http://vrok.it" target="_blank">Vrok-It</a>.</li>
</ul>
</li>
</ul>
</li>
<li>Pre-rendered graphics
<ul>
<li>You can use <a href="https://gallery.autodesk.com/a360rendering" target="_blank">cloud rendering in A360</a> to generate stereo panoramas today, which is the main subject of this post. (And yes, you could use this for tethered headsets, too, but I’ve only listed it here as it’s a really good way to harness mobile VR devices.)</li>
</ul>
</li>
</ul>
</li>
</ul>
<p>&#0160;&#0160;&#0160; * Unity, Unreal, CryEngine, Stingray</p>
<p>&#0160;&#0160;&#0160; ** WebGL &amp; WebVR</p>
<p>I could have flattened this matrix in different ways, but this made as much sense as any to me, at least. I have no doubt that there’s activity in the various sub-categories that I’m unaware of, but I did want to touch on the last one: pre-rendered CAD scenes viewed on mobile VR solutions.</p>
<p>A number of modern rendering tools allow you to generate 3D stereo panoramas. While it apparently isn’t that hard, it isn’t immediately obvious how it works (to me, anyway). The trick is that you’re essentially generating two panoramic images – typically cube or spherical maps – and using one for each eye. But as your head rotates around, the two “cameras” need to rotate, too. Anyway, thankfully it’s <a href="http://paulbourke.net/stereographics/stereopanoramic/" target="_blank">a solved problem</a>, at this stage, even if the solution isn’t obvious to rendering luddites such as myself.</p>
<p>The A360 rendering team has created <a href="http://cardboard.autodesk.com" target="_blank">a mechanism that allows you </a><a href="http://autodesk360rendering.typepad.com/blog/2016/01/further-updates-to-stereo-panorama.html" target="_blank">to generate stereo renders</a> directly from within 3ds Max or Revit as well as via uploaded AutoCAD or Fusion 360 models. It’s really easy to do, and you can find a bunch of stereo renders inside <a href="https://gallery.autodesk.com/a360rendering/projects" target="_blank">the online gallery</a>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c81dac9c970b-pi" target="_blank"><img alt="Stereo renders in the online gallery" border="0" height="335" src="/assets/image_37533.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border: 0px;" title="Stereo renders in the online gallery" width="500" /></a></p>
<p>To view one of these panoramas on a mobile device, open up a project that shows the little VR goggles icon, and then look for a rendering in the project that shows it, too. When you select it, you should see a dialog such as this:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb08c2729f970d-pi" target="_blank"><img alt="A preview of a stereo render" border="0" height="334" src="/assets/image_52945.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="A preview of a stereo render" width="500" /></a></p>
<p>You can copy and paste the link or use the QR code to view the stereo panorama using Google Cardboard on your phone. Very cool, very easy. Oh, and a <a href="https://developer.autodesk.com/api/rendering/" target="_blank">cloud rendering API</a> is coming soon as part of the Forge platform, too. I’ll be reporting more on that in due course.</p>
<p>Pre-rendering has a number of advantages, today, in that you can quickly generate decent VR-like visualizations without having to prepare a scene completely for navigation or worrying about the specific capabilities of the target device (such as whether it can run a game engine runtime). It has its limitations, of course: you can’t move around the scene, for instance, but it can nonetheless be a powerful visualization tool.</p>
<p>Something the current implementation doesn’t really support is the ability to aggregate a bunch of scenes into a single “presentation”, allowing you to navigate between renderings without removing the headset. This type of aggregate scene can go a long way mitigate the issue of individual scenes being rendered from a single location.</p>
<p>For the internal project I mentioned last time, I went and built a simple tool that downloads the contents of a number of stereo renderings and builds a set of HTML files that use the <a href="http://krpano.com" target="_blank">krpano</a> viewer for display and navigation. We’ll take a look at more details next week.</p>
