---
layout: "post"
title: "My first HoloLens application"
date: "2016-07-06 11:13:58"
author: "Kean Walmsley"
categories:
  - "Augmented Reality"
  - "HoloLens"
  - "Unity3D"
original_url: "https://www.keanw.com/2016/07/my-first-hololens-application.html "
typepad_basename: "my-first-hololens-application"
typepad_status: "Publish"
---

<p>After managing to <a href="http://through-the-interface.typepad.com/through_the_interface/2016/07/over-my-first-hololens-hurdle.html" target="_blank">get HoloLens working properly</a>, yesterday I dove right into the Holograms 101 tutorial. There are two versions of this tutorial: the classic <a href="https://developer.microsoft.com/en-us/windows/holographic/holograms_101" target="_blank">101</a> is for use with a physical device, while <a href="https://developer.microsoft.com/en-us/windows/holographic/holograms_101e" target="_blank">101E</a> is an alternative for the HoloLens Emulator.</p>
<p>In my case I had to use the standard 101 version as I’m currently running a mix of Windows 7, 8 and 8.1 on my various PCs and the HoloLens Emulator only works with Windows 10. I could upgrade to Windows 10 – or use it in a Hyper-V VM, I suppose – but as it hasn’t yet officially been rolled out within Autodesk – and I now have access to a physical HoloLens device – I’ve decided to forgoe that, for now.</p>
<p>The tutorial is well-structured and straightforward to follow. So far I’ve only got as far as creating the first, basic app – with some Unity assets provided with the tutorial – but this “quick win” felt really good (it’s this same feeling we aimed for with the <a href="http://autodesk.com/myfirstplugin" target="_blank">My First Plug-In</a> series, back in the day). Later on I’ll be adding gaze, gesture, voice &amp; spatial mapping support, but that’s for another day.</p>
<p>Rather than stick with the standard Unity assets from the tutorial, I decided to integrate something more relevant to an event I’m helping organise…</p>
<p>Later this year Autodesk will be celebrating 25 years in Switzerland. We’re going to be holding a <a href="http://www.autodesk.com/gallery/design-nights" target="_blank">Design Night</a> in the Neuchatel area, and the current thinking is to have a robotics theme – given the history of the region, and the fact the <a href="https://en.wikipedia.org/wiki/Jaquet-Droz_automata" target="_blank">first “robots”</a> were created here in the late 18th century. The plan is to get a number of robots in for the event – we’re busy speaking to various vendors and collaborators on that front – but I’d also like to have a virtual, robotic exhibit that’s only experienced by people wearing HoloLens devices.</p>
<p>So, with that in mind, I decided to see how it would work with a very basic robot arm. Luckily there’s <a href="https://www.assetstore.unity3d.com/en/#!/content/11016" target="_blank">one available on the Unity asset store</a>. This is really just a first step – I’m in contact with a Tom Eriksson, a Fusion 360 aficionado in Sweden, about a <a href="https://gallery.autodesk.com/fusion360/projects/24398/abb-robot-project" target="_blank">project he’s created</a> that works <a href="http://www.t77.se/IRB6620_V3_Tom_Eriksson/IRB6620_V3_Tom_Eriksson.html" target="_blank">really nicely in Unity</a>. With Tom’s help I expect to be able to create something much more fun.</p>
<p>Anyway, here’s a quick screenshot (maybe that should be holoshot?) of the basic application in action:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b7c87822c5970b-pi"><img alt="Robot arm in HoloLens app" border="0" height="285" src="/assets/image_401567.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 30px auto; display: block; padding-right: 0px; border-width: 0px;" title="Robot arm in HoloLens app" width="504" /></a></p>
<p>And here’s a video, so you can see and hear the animation (although the sound isn’t yet spatial).</p>
<p>&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube-nocookie.com/embed/sN6Dj7Somk8?rel=0&amp;showinfo=0" width="500"></iframe></p>
<p>&#0160;</p>
<p>The robot arm isn’t perfectly aligned with the floor, but it’s a good enough start.</p>
