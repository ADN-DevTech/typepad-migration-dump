---
layout: "post"
title: "Previewing and capturing Kinect sensor data directly inside AutoCAD"
date: "2011-04-14 17:46:01"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Jigs"
  - "Kinect"
  - "Point clouds"
  - "Reality capture"
original_url: "https://www.keanw.com/2011/04/previewing-and-capturing-kinect-sensor-data-directly-inside-autocad.html "
typepad_basename: "previewing-and-capturing-kinect-sensor-data-directly-inside-autocad"
typepad_status: "Publish"
---

<p>Now things are getting interesting. :-)</p>
<p>It took me some effort to get this working, but looking at the results I think it was worth it. As mentioned in <a href="http://through-the-interface.typepad.com/through_the_interface/2011/04/initial-fooling-around-with-kinect-and-autocad.html" target="_blank">the last post</a>, I’ve been trying to get a live feed from the Kinect sensor to display dynamically inside AutoCAD. This post shows how to do that.</p>
<p>A huge thanks to Boris Scheiman for helping me get to this point: he not only extended his <a href="http://nkinect.codeplex.com/" target="_blank">nKinect</a> implementation to generate the point cloud data I asked for, he sent a number of emails explaining (and even providing) the various modules needed to get nKinect up and running.</p>
<p>Before we get into the implementation, I should add some information about general Kinect development. In addition to the four options for developing Kinect apps&#0160; mentioned in the last post, there’s a very important one I omitted: <a href="http://www.openni.org/" target="_blank">OpenNI</a> (NI standing for Natural Interaction). This was founded – at least in part – by <a href="http://www.primesense.com/" target="_blank">PrimeSense</a>, the company who provides the 3D-sensing technology to Microsoft for their Kinect sensors. PrimeSense co-founded OpenNI back in November, when they released their own drivers that could be used with the Kinect. Boris chose to move the nKinect implementation across to OpenNI (although the Codeplex site says otherwise, hence the confusion in my last post). As such, OpenNI is the closest thing to an “official” implementation (until <a href="http://research.microsoft.com/kinectsdk" target="_blank">Microsoft’s Kinect SDK</a> is available, that is).</p>
<p>It took me much, much longer than I’d hoped to get the OpenNI implementation up and running: there are three main components to install for nKinect to work, and it’s essential to get matching versions of each. The versions I ended up using were suggested and provided by Boris: I couldn’t have managed without him as they no longer appear to be available on the web.</p>
<p>Let’s talk a bit about these various components and what they’re for.</p>
<ol>
<li><a href="http://openni.org/downloadfiles" target="_blank">OpenNI SDK</a> 
<ul>
<li>This is the main SDK, providing a base infrastructure for developing NI apps </li>
<li>Both <a href="http://openni.org/downloadfiles/openni-binaries/21-stable" target="_blank">stable</a> and <a href="http://openni.org/downloadfiles/openni-binaries/20-latest-unstable" target="_blank">unstable</a> builds of the SDK are available  
<ul>
<li>At the time of writing 1.0.0.38 (stable) and 1.0.0.39 (unstable) are available </li>
</ul>
</li>
<li>I’m using 1.0.0.23 (unstable – I think), as that’s the one needed by SensorKinect<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2014e60ecc9be970c-pi" target="_blank"><img alt="Installing OpenNI" border="0" height="265" src="/assets/image_163807.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Installing OpenNI" width="341" /></a> </li>
</ul>
</li>
<li><a href="https://github.com/avin2/SensorKinect" target="_blank">SensorKinect</a> 
<ul>
<li>PrimeSensor modules for OpenNI  
<ul>
<li>As far as I can tell this adds an additional Kinect-specific OpenNI-compliant driver, as well as some additional files to make it easier to get OpenNI and NITE working with the Kinect </li>
</ul>
</li>
<li>I installed the <span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">SensorKinect-Win32-5.0.0.exe – this has not been updated since early January, and so needs old, unstable builds of OpenNI and NITE<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2014e87cbaabb970d-pi" target="_blank"><img alt="Installing KinectSensor" border="0" height="265" src="/assets/image_182008.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Installing KinectSensor" width="341" /></a><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2014e87cbaad7970d-pi" target="_blank"><img alt="Installing KinectSensor 2" border="0" height="265" src="/assets/image_853165.jpg" style="background-image: none; margin: 0px auto 20px; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Installing KinectSensor 2" width="341" /></a></span> </li>
</ul>
</li>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><a href="http://www.primesense.com/?p=515" target="_blank">NITE Middleware</a></span> 
<ul>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">Provides higher-level capabilities such as user identification, feature and gesture detection</span> </li>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">Both <a href="http://openni.org/downloadfiles/openni-compliant-middleware-binaries/34-stable" target="_blank">stable</a> and <a href="http://openni.org/downloadfiles/openni-compliant-middleware-binaries/33-latest-unstable" target="_blank">unstable</a> builds of the SDK are available</span> 
<ul>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">At the time of writing 1.3.1.3 (stable) and 1.3.1.4 (unstable) are available</span> </li>
</ul>
</li>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">I’m using 1.3.0.17 (also unstable – I think), as that’s the one needed by SensorKinect</span> </li>
<li><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">You need to use this license key when installing the SDK: “<span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><strong>0KOIk2JeIBYClPWVnMoRKn5cdY4=</strong></span>”</span><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2014e60ecca03970c-pi" target="_blank"><img alt="Installing NITE - license key" border="0" height="266" src="/assets/image_601631.jpg" style="background-image: none; margin: 20px auto 0px; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Installing NITE - license key" width="341" /></a><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2014e60ecca14970c-pi" target="_blank"><img alt="Installing NITE" border="0" height="265" src="/assets/image_989970.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Installing NITE" width="341" /></a></span> </li>
</ul>
</li>
</ol>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">Once all these components have been installed, you need to copy some XML files installed with SensorKinect into the folders for OpenNI and NITE. The license key needs to be entered in one of these.</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">Then there’s nKinect itself, which can be downloaded and placed anywhere on your system. It has a dependency on <a href="http://opencv.willowgarage.com/wiki/" target="_blank">OpenCV</a>, so I also needed to get the right version of that (2.2), copying across a subset of its DLLs (as reported by <a href="http://www.dependencywalker.com/" target="_blank">Dependency Walker</a> after loading the main nKinect.dll). There’s also an OpenNI.xml configuration file that needs to be in a Data folder beneath your .DLL module’s location.</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">nKinect abstracts away a lot of the hard work needed to deal with Kinect input: you get a couple of callbacks for when the RGB and depth data is updated, so you can update your own UI. It looks, in fact, very similar to functionality that was presented yesterday at <a href="http://channel9.msdn.com/events/mix/mix11/key02" target="_blank">the MIX 11 Day 2 keynote</a> (starting about 1 hour 40 mins in). I do expect I’ll be porting across to that – as it clearly gets rid of a large amount of configuration trouble, as you can see from the above steps – but nKinect has certainly allowed me to get up and running in advance of that being available.</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">So what does the nKinect-based code look like?</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">Here’s my C# implementation, which has a DrawJig taking the point data provided by nKinect, displaying it dynamically to the user until a point is selected. Much of this code is actually used to bring in the point cloud from a TXT file via LAS to PCG (using <a href="http://www.cs.unc.edu/~isenburg/lastools" target="_blank">txt2las</a> and the implementation show in the <a href="http://through-the-interface.typepad.com/through_the_interface/2010/10/octobers-plugin-of-the-month-browsephotosynth-for-autocad.html" target="_blank">Browse Photosynth</a> <a href="http://labs.autodesk.com/utilities/ADN_plugins" target="_blank">Plugin of the Month</a>).</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"> </span></p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsInterface;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Collections.Generic;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Windows.Media;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Diagnostics;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.Reflection;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System.IO;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> NKinect;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> KinectIntegration</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">KinectJig</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #2b91af;">DrawJig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We need our nKinect sensor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Sensor</span><span style="line-height: 140%;"> _kinect = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A list of points to be displayed</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3dCollection</span><span style="line-height: 140%;"> _points;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A list of points captured by the sensor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (for eventual export)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">List</span><span style="line-height: 140%;">&lt;</span><span style="line-height: 140%; color: #2b91af;">ColorVector3</span><span style="line-height: 140%;">&gt; _vecs;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The constructor takes a Sensor argument</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> KinectJig(</span><span style="line-height: 140%; color: #2b91af;">Sensor</span><span style="line-height: 140%;"> kinect)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; _kinect = kinect;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; _points = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3dCollection</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;"> Sampler(</span><span style="line-height: 140%; color: #2b91af;">JigPrompts</span><span style="line-height: 140%;"> prompts)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Se don&#39;t really need a point, but we do need some</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// user input event to allow us to loop, processing</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// for the Kinect input</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptPointResult</span><span style="line-height: 140%;"> ppr =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prompts.AcquirePoint(</span><span style="line-height: 140%; color: #a31515;">&quot;\nClick to capture: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Generate a point cloud via nKinect</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _vecs = _kinect.GeneratePointCloud();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Extract the points for display in the jig</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (note we only take 1 in 5)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _points.Clear();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = 0; i &lt; _vecs.Count; i += 5)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ColorVector3</span><span style="line-height: 140%;"> vec = _vecs[i];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; _points.Add(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3d</span><span style="line-height: 140%;">(vec.X, vec.Y, vec.Z)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.OK;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">SamplerStatus</span><span style="line-height: 140%;">.Cancel;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">protected</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> WorldDraw(</span><span style="line-height: 140%; color: #2b91af;">WorldDraw</span><span style="line-height: 140%;"> draw)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// This simply draws our points</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; draw.Geometry.Polypoint(_points, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ExportPointCloud(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> filename)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (_vecs.Count &gt; 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">StreamWriter</span><span style="line-height: 140%;"> sw = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">StreamWriter</span><span style="line-height: 140%;">(filename))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// For each pixel, write a line to the text file:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// X, Y, Z, R, G, B</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">foreach</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">ColorVector3</span><span style="line-height: 140%;"> pt </span><span style="line-height: 140%; color: blue;">in</span><span style="line-height: 140%;"> _vecs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sw.WriteLine(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;{0}, {1}, {2}, {3}, {4}, {5}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pt.X, pt.Y, pt.Z, pt.R, pt.G, pt.B</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;ADNPLUGINS&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;KINECT&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #2b91af;">CommandFlags</span><span style="line-height: 140%;">.Modal)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ImportFromKinect()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Autodesk.AutoCAD.ApplicationServices.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Sensor</span><span style="line-height: 140%;"> kinect = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">KinectJig</span><span style="line-height: 140%;"> kj = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> pr = </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kinect = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Sensor</span><span style="line-height: 140%;">(s =&gt; { }, r =&gt; { }, d =&gt; { });</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kinect.Start();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kj = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">KinectJig</span><span style="line-height: 140%;">(kinect);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pr = ed.Drag(kj);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> { }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">finally</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (kinect != </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kinect.Stop();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kinect.Dispose();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status == </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We&#39;ll store most local files in the temp folder.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We get a temp filename, delete the file and</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// use the name for our folder</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> localPath = </span><span style="line-height: 140%; color: #2b91af;">Path</span><span style="line-height: 140%;">.GetTempFileName();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Delete(localPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.CreateDirectory(localPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; localPath += </span><span style="line-height: 140%; color: #a31515;">&quot;\\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Paths for our temporary files</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> txtPath = localPath + </span><span style="line-height: 140%; color: #a31515;">&quot;points.txt&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> lasPath = localPath + </span><span style="line-height: 140%; color: #a31515;">&quot;points.las&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Our PCG file will be stored under My Documents</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> outputPath =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Environment</span><span style="line-height: 140%;">.GetFolderPath(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Environment</span><span style="line-height: 140%;">.</span><span style="line-height: 140%; color: #2b91af;">SpecialFolder</span><span style="line-height: 140%;">.MyDocuments</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) + </span><span style="line-height: 140%; color: #a31515;">&quot;\\Kinect Point Clouds\\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.Exists(outputPath))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.CreateDirectory(outputPath);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// We&#39;ll use the title as a base filename for the PCG,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// but will use an incremented integer to get an unused</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// filename</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cnt = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> pcgPath;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">do</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pcgPath =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; outputPath + </span><span style="line-height: 140%; color: #a31515;">&quot;Kinect&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (cnt == 0 ? </span><span style="line-height: 140%; color: #a31515;">&quot;&quot;</span><span style="line-height: 140%;"> : cnt.ToString()) + </span><span style="line-height: 140%; color: #a31515;">&quot;.pcg&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; cnt++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Exists(pcgPath));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// The path to the txt2las tool will be the same as the</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// executing assembly (our DLL)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> exePath =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Path</span><span style="line-height: 140%;">.GetDirectoryName(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Assembly</span><span style="line-height: 140%;">.GetExecutingAssembly().Location</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) + </span><span style="line-height: 140%; color: #a31515;">&quot;\\&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Exists(exePath + </span><span style="line-height: 140%; color: #a31515;">&quot;txt2las.exe&quot;</span><span style="line-height: 140%;">))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCould not find the txt2las tool: please make sure &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;it is in the same folder as the application DLL.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Export our point cloud from the jig</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nSaving TXT file of the captured points.\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; kj.ExportPointCloud(txtPath);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Use the txt2las utility to create a .LAS</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// file from our text file</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nCreating a LAS from the TXt file.\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">ProcessStartInfo</span><span style="line-height: 140%;"> psi =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">ProcessStartInfo</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; exePath + </span><span style="line-height: 140%; color: #a31515;">&quot;txt2las&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;-i \&quot;&quot;</span><span style="line-height: 140%;"> + txtPath +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; -o \&quot;&quot;</span><span style="line-height: 140%;"> + lasPath +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; -parse xyzRGB&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; psi.CreateNoWindow = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; psi.WindowStyle = </span><span style="line-height: 140%; color: #2b91af;">ProcessWindowStyle</span><span style="line-height: 140%;">.Hidden;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Wait up to 20 seconds for the process to exit</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">Process</span><span style="line-height: 140%;"> p = </span><span style="line-height: 140%; color: #2b91af;">Process</span><span style="line-height: 140%;">.Start(psi))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; p.WaitForExit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; { }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If there&#39;s a problem, we return</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!</span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Exists(lasPath))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nError creating LAS file.&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Delete(txtPath);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;Indexing the LAS and attaching the PCG.\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Index the .LAS file, creating a .PCG</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> lasLisp = lasPath.Replace(</span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pcgLisp = pcgPath.Replace(</span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;(command \&quot;_.POINTCLOUDINDEX\&quot; \&quot;&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lasLisp + </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; \&quot;&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pcgLisp + </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot;)(princ) &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Attach the .PCG file</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;_.WAITFORFILE \&quot;&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pcgLisp + </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; \&quot;&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; lasLisp + </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;(command \&quot;_.-POINTCLOUDATTACH\&quot; \&quot;&quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pcgLisp +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\&quot; \&quot;0,0\&quot; \&quot;1\&quot; \&quot;0\&quot;)(princ) &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; doc.SendStringToExecute(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;_.-VISUALSTYLES _C _Conceptual _.ZOOM _E &quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">false</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//Cleanup();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Return whether a file is accessible</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> IsFileAccessible(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> filename)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// If the file can be opened for exclusive access it means</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the file is accesible</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">FileStream</span><span style="line-height: 140%;"> fs =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Open(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; filename, </span><span style="line-height: 140%; color: #2b91af;">FileMode</span><span style="line-height: 140%;">.Open,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">FileAccess</span><span style="line-height: 140%;">.Read, </span><span style="line-height: 140%; color: #2b91af;">FileShare</span><span style="line-height: 140%;">.None</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (fs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">IOException</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// A command which waits for a particular PCG file to exist</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;ADNPLUGINS&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;WAITFORFILE&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #2b91af;">CommandFlags</span><span style="line-height: 140%;">.NoHistory</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; )]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> WaitForFileToExist()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Document</span><span style="line-height: 140%;"> doc =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;"> ha =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.Current;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">PromptResult</span><span style="line-height: 140%;"> pr = ed.GetString(</span><span style="line-height: 140%; color: #a31515;">&quot;Enter path to PCG: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> pcgPath = pr.StringResult.Replace(</span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; pr = ed.GetString(</span><span style="line-height: 140%; color: #a31515;">&quot;Enter path to LAS: &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (pr.Status != </span><span style="line-height: 140%; color: #2b91af;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> lasPath = pr.StringResult.Replace(</span><span style="line-height: 140%; color: #a31515;">&#39;/&#39;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&#39;\\&#39;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nWaiting for PCG creation to complete...\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Check the write time for the PCG file...</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// if it hasn&#39;t been written to for at least half a second,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// then we try to use a file lock to see whether the file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// is accessible or not</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> ticks = 50;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">TimeSpan</span><span style="line-height: 140%;"> diff;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">bool</span><span style="line-height: 140%;"> cancelled = </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// First loop is to see when writing has stopped</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (better than always throwing exceptions)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Exists(pcgPath))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">DateTime</span><span style="line-height: 140%;"> dt = </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.GetLastWriteTime(pcgPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; diff = </span><span style="line-height: 140%; color: #2b91af;">DateTime</span><span style="line-height: 140%;">.Now - dt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (diff.Ticks &gt; ticks)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DoEvents();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// Second loop will wait until file is finally accessible</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// (by calling a function that requests an exclusive lock)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (!cancelled)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> inacc = 0;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">while</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (IsFileAccessible(pcgPath))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">break</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; inacc++;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.Windows.Forms.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DoEvents();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;\nFile inaccessible {0} times.&quot;</span><span style="line-height: 140%;">, inacc);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CleanupTmpFiles(lasPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">catch</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; { }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">internal</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> CleanupTmpFiles(</span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> txtPath)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Exists(txtPath))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">File</span><span style="line-height: 140%;">.Delete(txtPath);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Directory</span><span style="line-height: 140%;">.Delete(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Path</span><span style="line-height: 140%;">.GetDirectoryName(txtPath)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;">When running the code the user should see the jigged point-cloud. It’s possible to zoom, pan and orbit to get the point cloud feed in the view (this can be a little tricky, admittedly).</span></p>
<p><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11pt; mso-ascii-theme-font: minor-latin; mso-fareast-font-family: calibri; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin; mso-bidi-font-family: &#39;Times New Roman&#39;; mso-bidi-theme-font: minor-bidi; mso-ansi-language: en-us; mso-fareast-language: en-us; mso-bidi-language: ar-sa;"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201538dd83d86970b-pi" target="_blank"><img alt="Dynamic preview of Kinect-generated points" border="0" height="352" src="/assets/image_791416.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Dynamic preview of Kinect-generated points" width="392" /></a></span></p>
<p>Selecting a point (an arbitrary one – that’s just allowing us to update the data being displayed, we don’t actually care about the point itself) will cause the full import process to occur. The data imported is that displayed most recently inside AutoCAD.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201538dd83dac970b-pi" target="_blank"><img alt="The final point cloud" border="0" height="323" src="/assets/image_594796.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="The final point cloud" width="407" /></a></p>
<p>And just to show it’s a full 3D point cloud – albeit captured from a single position – here’s another angle:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201538dd83def970b-pi" target="_blank"><img alt="From another angle" border="0" height="315" src="/assets/image_973864.jpg" style="background-image: none; margin: 0px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="From another angle" width="427" /></a></p>
<p>Now I need to start looking at interpreting gestures, to see what can be done to supplement AutoCAD’s existing user interface features. Fun fun fun! :-)</p>
<p><strong><em>Update:</em></strong></p>
<p>Thanks to prompting from <a href="http://gbellmann.wordpress.com/" target="_blank">Guillermo Bellmann</a>, I managed to download and install the following module versions before they were superseded (all this stuff is really in the Wild West of coding ;-).</p>
<ul>
<li>
<div class="MsoNormal" style="margin: 0in 0in 0pt;">OpenNI 1.1.0.39 (unstable)</div>
</li>
<li>
<div class="MsoNormal" style="margin: 0in 0in 0pt;">SensorKinect 5.0.1.32</div>
</li>
<li>
<div class="MsoNormal" style="margin: 0in 0in 0pt;">NITE 1.3.1.3 (unstable)</div>
</li>
</ul>
<p>I had to make some minor modifications to nKinect to get it working, mainly to incorporate the updated headers and libraries from OpenNI and NITE. I can now confirm that it’s possible to make the above code work with these versions.</p>
<p>On a side note, in trying to troubleshoot a few stumbling blocks, I stumbled across <a href="http://asimmittal.net/blog/?p=121" target="_blank">these</a> <a href="http://asimmittal.net/blog/?p=303" target="_blank">useful</a>&#0160;<a href="http://asimmittal.net/blog/?m=201104" target="_blank">posts</a>, which help explain the history and functionality of these components.</p>
