---
layout: "post"
title: "Gearing up for the VR Hackathon"
date: "2014-10-13 10:24:00"
author: "Kean Walmsley"
categories:
  - "Augmented Reality"
  - "Autodesk"
  - "HTML"
  - "JavaScript"
  - "Leap Motion"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2014/10/gearing-up-for-the-vr-hackathon.html "
typepad_basename: "gearing-up-for-the-vr-hackathon"
typepad_status: "Publish"
---

<p>I’m heading back across to the Bay Area on Wednesday for 10 days. There seems to be a pattern forming to my trips across: I’ll spend the first few days in San Francisco – in this case attending internal strategy meetings in our 1 Market office – and then head up after the weekend to San Rafael to work with the members of the AutoCAD engineering team based up there. I’ll still probably head back into SF for the odd day, the following week, but that’s fine: I really like commuting by ferry from Larkspur to the Embarcadero.</p>
<p>The weekend I’m spending in the Bay Area is looking to have a slightly different shape this time, though. Rather than just catching up with old friends (which I still hope to do), I’ve signed up for the <a href="http://vrhackathon.com" target="_blank">VR Hackathon</a>, an event that looks really interesting. I was happy to find out about this one and that it fell exactly during my stay. I’ve even roped a few colleagues into coming along, too.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb07971397970d-pi" target="_blank"><img alt="VR Hackathon" border="0" height="143" src="/assets/image_207288.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="VR Hackathon" width="474" /></a></p>
<p>Looking at the “<a href="http://vrhackathon.com/hack-categories.html" target="_blank">challenges</a>” posted for the hackathon, it seemed worth taking a look at web and mobile <a href="http://en.wikipedia.org/wiki/Virtual_reality" target="_blank">VR</a>, as these look like the two that I’m most likely to be able to contribute towards. Which led me to reaching out to Jim Quanci and Cyrille Fauvel, over in the <a href="http://autodesk.com/joinadn" target="_blank">ADN</a> team, to see what’s been happening with respect to VR platforms such as Oculus Rift and Google Cardboard.</p>
<p>It turns out the ADN team has invested in a few Oculus Rift Developer Kits, but was looking for someone to spend some time fooling around with integrating <a href="http://through-the-interface.typepad.com/through_the_interface/2014/07/my-first-autodesk-360-viewer-sample.html" target="_blank">the new WebGL-based Autodesk 360 viewer</a> with Google Cardboard. And as “fooling around” is my middle name, I signed up enthusiastically. :-)</p>
<p>For those of you who haven’t been following <a href="http://through-the-interface.typepad.com/through_the_interface/2013/12/local-presentations-and-musings-on-arvr.html" target="_blank">the VR space</a>, lately, I think it’s fair to say that Facebook put the cat amongst the pigeons when they acquired Oculus. Google’s competitive response was very interesting: <a href="https://www.youtube.com/watch?v=DFog2gMnm44" target="_blank">at this year’s Google I/O</a> they announced <a href="https://developers.google.com/cardboard/" target="_blank">Google Cardboard</a>, a simple <a href="http://en.wikipedia.org/wiki/View-Master" target="_blank">View-Master</a>-like mount for a smartphone that can be used for <a href="http://en.wikipedia.org/wiki/Augmented_reality" target="_blank">AR</a> or VR.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d07b0845970c-pi"><img alt="Cardboard" height="262" src="/assets/image_55942.jpg" style="display: inline;" title="Cardboard" width="470" /></a></p>
<p>A few notes about the design: there are two lenses that focus the smartphone’s display – which is split in half in landscape mode, with one half for each eye – and there’s a simple magnet-based button on the left as well as an embedded NFC tag to tell the phone when to launch the Cardboard software. The rear camera has also been left clear in case you need its input for a “reality feed” in the case of AR or perhaps some additional information to help with VR.</p>
<p>Aside from the smartphone, the whole package can be made for a few dollars (assuming a certain economy of scale, of course) with <a href="https://cardboard.withgoogle.com/#hardware" target="_blank">the provided instructions</a>. Right now you can pick them up pre-assembled for anywhere between $15 and $30 – still cheap for the capabilities provided. Which has led to the somewhat inevitable nickname of “Oculus Thrift”. :-)</p>
<p>The point Google is making, of course, is that you don’t need expensive, complex kit to do VR: today’s smartphones have a lot of the capabilities needed, in terms of processing power, sensors and responsive, high-resolution displays.</p>
<p>When looking into the possibilities for supporting Cardboard from a software perspective, there seem to be two main options: the first is to create a native Android app <a href="https://developers.google.com/cardboard/overview" target="_blank">using their SDK</a>, the second is to create a web-app such as those available on the <a href="http://vr.chromeexperiments.com/" target="_blank">Chrome Experiments</a> site.</p>
<p>Given the web-based nature of the Autodesk 360 viewer, it seemed to make sense to follow the latter path. Jim and Cyrille kindly pointed me at <a href="http://jaanga.github.io/cookbook/cardboard/readme-reader.html" target="_blank">an existing integration of Cardboard with Three.js/WebGL</a>, which turned out to be really useful. But we’ll look at some specifics more closely in the next post.</p>
<p>During the rest of the week – and I expect to post each day until Thursday, at least, so check back often – I’ll cover the following topics:</p>
<ol>
<li>Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer</li>
<li>Adding tilt support for model navigation and enabling fullscreen mode</li>
<li>Supporting multiple models</li>
</ol>
<p>If I manage to get my hands on the pre-release Leap Motion SDK for Android then I’ll try to integrate that, too, at some point. <a href="https://www.leapmotion.com/product/vr" target="_blank">Mounting a Leap Motion controller</a> to the back of the goggles allows you to use hand gestures for <a href="http://blog.leapmotion.com/leap-motion-sets-a-course-for-vr/" target="_blank">additional (valuable) input in a VR environment</a>… I’m thinking this may end up being the “killer app” for Leap Motion (not mine specifically, but VR in general).</p>
<p>Until tomorrow!</p>
