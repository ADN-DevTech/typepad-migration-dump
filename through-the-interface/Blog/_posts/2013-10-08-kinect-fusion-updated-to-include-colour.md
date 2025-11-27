---
layout: "post"
title: "Kinect Fusion updated to include colour"
date: "2013-10-08 11:40:33"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Kinect"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2013/10/kinect-fusion-updated-to-include-colour.html "
typepad_basename: "kinect-fusion-updated-to-include-colour"
typepad_status: "Publish"
---

<p>A few weeks ago <a href="http://blogs.msdn.com/b/kinectforwindows/archive/2013/09/16/updated-sdk-with-html5-kinect-fusion-improvements-and-more.aspx" target="_blank">the Kinect SDK was updated to version 1.8</a>. I’d been eagerly awaiting this update for one reason, in particular: aside from receiving some updates to provide more robust tracking – something that was very much needed – Kinect Fusion has now been updated to include realistic colours in the output.</p>
<p>There are some additional SDK enhancements, such as a background removal APIs (good for greenscreening) and HTML support (handy for interactive kiosks), but the ones that interest me most relate to Kinect Fusion.</p>
<p>There are a few new Kinect Fusion samples that I need to take a look at – one combines Face Tracking with Kinect Fusion for head capture, another enables multi-sensor support with Kinect Fusion – but I haven’t had the chance to look at these, as yet.</p>
<p>The first order of business was to integrate colour into the existing AutoCAD-Kinect samples. This didn’t prove to be too tricky, overall: I chose the path of least resistance, to fork <a href="http://through-the-interface.typepad.com/through_the_interface/2013/03/kinect-fusion-inside-autocad.html" target="_blank">the existing Kinect Fusion command implementation</a>, so there’s currently quite a bit of duplicate code. At some point I’ll factor out shared functions or add to the class hierarchy, but the first step was to get the commands working with as few differences as made sense. That’s what I’ve included, for now.</p>
<p>It’s worth bearing in mind a few things: while there have been improvements to the robustness of Kinect Fusion’s tracking algorithm, adding colour support has reduced the size of the volume you can realistically track, mainly because you need memory capacity on your GPU to track the additional per-vertex information. Which is completely understandable.</p>
<p>One reason I’m happy to have colour information, at last, is that it has also allowed me to identify some core issues with the previous version of the code: mainly because having colour makes it so much easier to troubleshoot the point cloud output. This means I’m actually going to be able to make more progress with the sample’s development than I had in the past, so I fully expect another update to come between now and AU.</p>
<p>Here’s <a href="http://through-the-interface.typepad.com/files/KinectSamples-v1.8.zip" target="_blank">a link to the new samples</a>, and here’s a quick screenshot of the results of the new KINFUSCOL command, which I used to capture <a href="http://www.dyson.com/fans/fansandheaters.aspx" target="_blank">a Dyson Hot+Cool</a> that I bought a few months ago:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2019affd9b0f1970c-pi" target="_blank"><img alt="Kinect Fusion capture in colour" border="0" height="323" src="/assets/image_71996.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Kinect Fusion capture in colour" width="454" /></a></p>
<p>I’m pretty confident that I could have captured more of the object if I’d been more patient – the tracking is still a little flakey, given the fact we introduce latency by retrieving the points in 3D rather than consuming a rendered bitmap – but the results still look fairly good as they stand.</p>
