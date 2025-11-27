---
layout: "post"
title: "Updated AutoCAD integration samples for Kinect SDK v1.6"
date: "2012-11-02 07:23:00"
author: "Kean Walmsley"
categories:
  - "AU"
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Kinect"
original_url: "https://www.keanw.com/2012/11/updated-autocad-integration-samples-for-kinect-sdk-v16.html "
typepad_basename: "updated-autocad-integration-samples-for-kinect-sdk-v16"
typepad_status: "Publish"
---

<p>As part of my ongoing procrastination around my AU material development – despite which I’m managing to make some progress… my WinRT stuff is mostly done, now – I went ahead and updated my Kinect samples to use v1.6 of the SDK. The version which finally works from a Windows session inside a Parallels VM on my Mac. Yay!</p>
<p>Here is <a href="http://through-the-interface.typepad.com/files/KinectSamples-v1.6.zip" target="_blank">the updated sample project</a>, which includes the face-tracking capabilities shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/face-tracking-inside-autocad-using-kinect.html" target="_blank">this previous post</a> and therefore also requires <a href="http://www.microsoft.com/en-us/download/details.aspx?id=34807" target="_blank">the Kinect Developer Toolkit</a>.</p>
<p>It wasn’t really much effort to port: a couple of methods that map depth and colour data into “skeleton space” have been deprecated, so while they still work it seemed sensible to avoid the compiler warnings and get them migrated to the new way of doing things. Here’s the previous code:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> x = i % depWidth;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> y = i / depWidth;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">SkeletonPoint</span><span style="line-height: 140%;"> p =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; kinect.MapDepthToSkeletonPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DepthImageFormat</span><span style="line-height: 140%;">.Resolution640x480Fps30,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; x, y, depth[i]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
</div>
<p>And here’s the new way of doing things:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">DepthImagePoint</span><span style="line-height: 140%;"> pt = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DepthImagePoint</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pt.X = i % depWidth;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pt.Y = i / depWidth;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pt.Depth = depth[i];</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">CoordinateMapper</span><span style="line-height: 140%;"> cm = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">CoordinateMapper</span><span style="line-height: 140%;">(kinect);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">SkeletonPoint</span><span style="line-height: 140%;"> p =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; cm.MapDepthPointToSkeletonPoint(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DepthImageFormat</span><span style="line-height: 140%;">.Resolution640x480Fps30, pt</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; );</span></p>
</div>
<p>What seemed peculiar, when I first ran the code, was the relative scale of the points returned by the new approach. I decided to put the two calls side-by-side and compare the results, and – sure enough – the new technique returns points that are 8x the scale of the ones returned the old way. I seem to remember that the points were previously pretty accurate, so I adjusted the code to simply divide the various ordinates by 8, bringing the results back in line.</p>
<p>Here’s a point cloud capture using the KINECT command with me in <a href="http://through-the-interface.typepad.com/through_the_interface/2012/10/happy-halloween.html" target="_blank">my Halloween costume</a>:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee49beff0970d-pi"><img alt="Kinect point cloud in 2013" height="430" src="/assets/image_85008.jpg" style="display: inline;" title="Kinect point cloud in 2013" width="470" /></a></p>
