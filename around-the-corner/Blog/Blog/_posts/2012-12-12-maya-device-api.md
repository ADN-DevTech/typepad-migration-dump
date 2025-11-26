---
layout: "post"
title: "Maya Device API"
date: "2012-12-12 21:26:20"
author: "Cyrille Fauvel"
categories:
  - "Animation"
  - "C++"
  - "Concurrent programming"
  - "Cyrille Fauvel"
  - "Kinect"
  - "Linux"
  - "Mac"
  - "Maya"
  - "MotionBuilder"
original_url: "https://around-the-corner.typepad.com/adn/2012/12/maya-device-api.html "
typepad_basename: "maya-device-api"
typepad_status: "Publish"
---

<p>Recently there was a lot of enthusiams around Virtual Production and use of Motion Capture. This <a href="http://www.businesswire.com/news/home/20120807005518/en/Autodesk-Lightstorm-Entertainment-Weta-Digital-Drive-Virtual" target="_self">article</a> really summarize what great future and fun we will get on that side.</p>
<p> But this is mainly about Virtual Production in general and mostly applied to MotionBuilder when it comes to Motion Capture. But there are many people asking - ok, but what about Maya and/or 3ds Max. I am not an expert on 3ds Max, but what I am going to say here is probably also applicable (but differently) to 3ds Max.</p>
<p>The main difference between MotionBuilder and Maya is that these &#0160;products were designed with 2 completely different vision in mind. MotionBuilder was for real-time animation. And Motion Capture is just a subset of the power of MotionBuilder. Whereas Maya was designed for complex content creation. 2 different visions, 2 different architectures.</p>
<p>The main issue for Maya is that whatever you do, it will never be real-time as the DG and the fact Maya is mostly single threaded will prevent real-time features of any kind. As a side note true real-time isn&#39;t possible anyway on a pre-emptive OS such as Linux, Windows, or OSX. There are ways to come near real-time, so I fear it depends what someone means by real-time.</p>
<p>Back on Maya - a long time ago there was an API for devices which has been retired in the 2010 release I think. But Maya 2013 has introduced a new device API back. That API comes with 3 samples.</p>
<p>There is a randomizer example which generates info and feeds it into Maya. Then there is a UDP based Linux only example and a game controller windows only example.</p>
<p>Internally, the Maya engineering team use these classes to send character information from MotionBuilder to Maya in our Live Stream feature which by the way also use the Human IK&#0160;character representation library. </p>
<p>
 
The class you want to look at in case you want to write device support in Maya is:
 
                <a href="http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/cpp_ref/class_m_px_threaded_device_node.html" target="_self">http://docs.autodesk.com/MAYAUL/2013/ENU/Maya-API-Documentation/cpp_ref/class_m_px_threaded_device_node.html</a>. If you take a look at it, you will see that it contains some simple primitives for handling the starting and stopping of threads and the ability to pass data from a secondary thread to the primary (where Maya operates). It is important from there to understand what I said in a previous post, that you can really only use Maya API in the main Maya thread unless specified differently. So here, it means the device thread will only do something in Maya, if Maya is free and not in the middle of a DG evaluation for example - so not realtime. That also means, in case Maya is busy, that you will have to cache your device data and wait for Maya.</p>
<p>I&#39;ll probably use some free time to write a Kinect sample during the Chritmas break as I bought one recently. Will share the sample when it is ready.</p>
