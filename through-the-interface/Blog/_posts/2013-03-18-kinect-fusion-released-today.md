---
layout: "post"
title: "Kinect Fusion released today!"
date: "2013-03-18 10:21:13"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Kinect"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2013/03/kinect-fusion-released-today.html "
typepad_basename: "kinect-fusion-released-today"
typepad_status: "Publish"
---

<p>This is very exciting: v1.7 of the Kinect for Windows SDK is being released today and it includes the uber-cool <a href="http://research.microsoft.com/apps/pubs/default.aspx?id=155416" target="_blank">Kinect Fusion</a> component.</p>  <p>For those of you who have not yet heard of Kinect Fusion, it allows you to use your Kinect for Windows sensor as an effective reality capture device: it aggregates input from depth frames provided by the Kinect sensor, mapping out a 3D volume. Or, for the layperson, it allows you to paint a 3D model of an existing real-world object or scene into your computer’s memory.</p>  <p>Here’s a video from <a href="http://www.engadget.com/2013/03/16/live-bob-heddle/" target="_blank">Engadget’s Expand event</a>, held over the weekend in San Francisco, where this version of the SDK was announced and the two main features – Kinect Interactions and Kinect Fusion – were demonstrated:</p>  <br />  <p><iframe id="viddler-8889f6e6" height="301" src="//www.viddler.com/embed/8889f6e6/?f=1&amp;offset=0&amp;autoplay=0&amp;secret=76358704&amp;disablebranding=0&amp;view_secret=76358704" frameborder="0" width="470" webkitallowfullscreen="true" mozallowfullscreen="true"></iframe></p>  <br />  <p>I’ve been working with a pre-release version of the SDK for the last few months, and it’s been a fun experience. I’m happy to say that the KfW team were really responsive providing APIs that make sense for applications such as AutoCAD (where you actually want to deal with raw 3D data rather than a synthesized 2D image of the reconstruction volume from a particular viewpoint).</p>  <p>Kinect Fusion makes heavy use of the GPU, so although I was – from v1.6 – finally able to work with the Kinect from inside Parallels Desktop on my MacBook Pro, I’ve now had to move back to developing for Kinect on my native Windows box (at least until the day Parallels provides GPU virtualization, I suppose).</p>  <p>I have some code to share that integrates point cloud data from Kinect Fusion inside AutoCAD, but I’m going to wait until the final release of the SDK is available later today (the SDK being made available “in the morning” probably means at the end of my day, as I’m based in Europe) before posting the code later in the week.</p>
