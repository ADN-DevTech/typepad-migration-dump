---
layout: "post"
title: "Using SLAM-based Augmented Reality to visualize 3D geometry"
date: "2013-02-11 17:14:56"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "Augmented Reality"
  - "Geometry"
  - "HTML"
  - "JavaScript"
  - "JSON"
  - "SaaS"
original_url: "https://www.keanw.com/2013/02/using-slam-based-augmented-reality-to-visualize-3d-geometry.html "
typepad_basename: "using-slam-based-augmented-reality-to-visualize-3d-geometry"
typepad_status: "Publish"
---

<p>I first became aware of the work being done by <a href="http://13thlab.com" target="_blank">13th Lab</a> a couple of years ago, but just last week someone pinged me about it again and re-triggered my interest (thanks, Jim :-).</p>
<p>13th Lab is a small Swedish company that has created some really interesting <a href="http://en.wikipedia.org/wiki/Augmented_reality" target="_blank">Augmented Reality</a> technology. Many AR systems make use of <a href="http://en.wikipedia.org/wiki/Fiduciary_marker" target="_blank">fiduciary markers</a> (which often look like sections of QR codes) to make it easier to determine where the 3D content should be positioned and visualized in the 2D image of the scene being fed from your device’s camera.</p>
<p>Ideally, though, you want a markerless AR system. 13th Lab have created just that, making use of a technique borrowed from the world of robotics called <a href="http://en.wikipedia.org/wiki/Simultaneous_localization_and_mapping" target="_blank">simultaneous localization and mapping (SLAM)</a>.</p>
<br /><iframe allowfullscreen="allowfullscreen" frameborder="0" height="264" src="http://www.youtube.com/embed/K5OKaK3Ay8U?rel=0" width="470"></iframe>  <br />  <br />
<p>I’m far from being an expert in AR, but I thought I’d have a play around with <a href="http://pointcloud.io" target="_blank">some technology that 13th Lab have recently released into Beta</a> (both of which are “<em>FREE (even for commercial use)</em>”). They’ve created two products: an SDK (currently for iOS and Unity 3D – apparently Android support is on its way) – which I decided not to spend time on – and a browser you run on an iOS device that loads custom web-pages.</p>
<p>The PointCloud Browser can be <a href="https://itunes.apple.com/us/app/pointcloud-browser/id492142085?mt=8" target="_blank">installed from the App Store</a> and pointed at one of the samples that show you how to use the JavaScript APIs to do something simple such as add basic 3D primitives or go much further and implement an AR-based game. Using one of these samples as a base, I created <a href="http://autode.sk/appar" target="_blank">my own web-page</a> that pulls down 3D fractal data from the web-service I implemented as part of last year’s <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/cloud-mobile-series-summary.html" target="_blank">the Cloud &amp; Mobile series</a> (if you try to load this page in a standard browser you’ll get uninteresting results, by the way).</p>
<p>Once loaded, you can point your camera at a flat surface with some helpful detail (SLAM systems are presumably greatly helped by the existence of unique visual detail). I chose the whiteboard in our kitchen:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee868f649970d-pi" target="_blank"><img alt="Choosing a flat surface" border="0" height="296" src="/assets/image_885485.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Choosing a flat surface" width="394" /></a></p>
<p>When you tap the screen, the browser then uses computer vision techniques to capture information about the surface:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d40f47c5a970c-pi" target="_blank"><img alt="Capturing the surface" border="0" height="296" src="/assets/image_777190.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="Capturing the surface" width="394" /></a></p>
<p>At which point you should first see your 3D geometry:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c36c5baec970b-pi" target="_blank"><img alt="View of our data from one angle" border="0" height="296" src="/assets/image_810221.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="View of our data from one angle" width="394" /></a></p>
<p>As you move the device around, you see difference views on the geometry, of course. Here’s a close up:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee868f863970d-pi" target="_blank"><img alt="A closer view" border="0" height="296" src="/assets/image_520903.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A closer view" width="394" /></a></p>
<p>And here’s a view from the other side of the whiteboard:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c36c5bc26970b-pi" target="_blank"><img alt="And from another angle" border="0" height="296" src="/assets/image_70323.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="And from another angle" width="394" /></a></p>
<p>The results aren’t yet quite as I want them, even if they prove the concept: the spheres currently all have the same radius, for instance, as that gets set at the mesh level (and mesh objects are ideally shared by multiple nodes). Some work will be needed either to find a way to set this per instance or to establish an efficient way of generating mesh objects for unique size/colour combinations.</p>
<p>Here’s the HTML and JavaScript code that integrates this data from the web-service:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;!</span><span style="line-height: 140%; color: maroon;">DOCTYPE</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">title</span><span style="line-height: 140%; color: blue;">&gt;</span><span style="line-height: 140%;">Apollonian</span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">title</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">meta</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">name</span><span style="line-height: 140%; color: blue;">=&quot;viewport&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">content</span><span style="line-height: 140%; color: blue;">=&quot;user-scalable=no, width=device-width, initial-scale=1.0, maximum-scale=1.0&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">meta</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">name</span><span style="line-height: 140%; color: blue;">=&quot;viper-init-options&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">content</span><span style="line-height: 140%; color: blue;">=&quot;manual&quot;/&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">link</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">rel</span><span style="line-height: 140%; color: blue;">=&quot;viper-app-icon&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;images/png&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">href</span><span style="line-height: 140%; color: blue;">=&quot;resources/images/appicon.jpg&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">link</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">rel</span><span style="line-height: 140%; color: blue;">=&quot;stylesheet&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">href</span><span style="line-height: 140%; color: blue;">=&quot;./css/common.css&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/css&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">charset</span><span style="line-height: 140%; color: blue;">=&quot;utf-8&quot;/&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;http://code.jquery.com/jquery-1.7.1.min.js&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">src</span><span style="line-height: 140%; color: blue;">=&quot;./js/common.js&quot;&gt;&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/xml&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">id</span><span style="line-height: 140%; color: blue;">=&quot;library&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;library&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh1&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0,0,0,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh2&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.5,0,0,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh3&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.5,0.5,0,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh4&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0,0.5,0,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh5&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0,0.5,0.5,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh6&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0,0,0.5,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh7&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.5,0,0.5,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh8&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.9,0.9,0.9,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh9&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.6,0.6,0.6,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh10&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;0.3,0.3,0.3,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh11&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;1,1,1,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;mesh</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh12&quot; radius=&quot;0.03&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; color=&quot;1,1,1,1&quot; details=&quot;3&quot; /&gt; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere1&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh1&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere2&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh2&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere3&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh3&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere4&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh4&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere5&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh5&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere6&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh6&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere7&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh7&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere8&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh8&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere9&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh9&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere10&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh10&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere11&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh11&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;masterSphere12&quot; static=&quot;true&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh12&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/node&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/library&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/xml&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">id</span><span style="line-height: 140%; color: blue;">=&quot;scene&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;scene base=&quot;relative-baseplane&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;light id=&quot;main_light&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; intensity=&quot;1.0&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; fade=&quot;constant&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ambient=&quot;0.2, 0.2, 0.2, 0.2&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; diffuse=&quot;1.0, 1.0, 1.0, 1.0&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; specular=&quot;1.0, 1.2, 1.2, 1.0&quot; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; position=&quot;3, 0.5, 2, 0&quot;/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; &lt;/scene&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">type</span><span style="line-height: 140%; color: blue;">=&quot;text/javascript&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> startup() {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.requireRealityMap();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">/*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160; * This function is called when the web app is fully loaded</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160; * (e.g. sounds, textures, image descriptors)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160; * and we&#39;re completely ready to go</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160; */</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> onAppLoaded() {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; startup();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> nodeListener = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> viper.NodeListener();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.getCamera().attachListener(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; nodeListener,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;">(node, data) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> pos = data.position.getTranslation();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.log(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;Received camera pos update: &quot;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pos.getX() + </span><span style="line-height: 140%; color: maroon;">&quot;,&quot;</span><span style="line-height: 140%;"> + pos.getY() + </span><span style="line-height: 140%; color: maroon;">&quot;,&quot;</span><span style="line-height: 140%;"> + pos.getZ()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">/*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; * This function is called when the Viper JavaScript API is</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; * ready for use </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; */</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> onViperReady() {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.setLoggingEnabled(</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> scene = viper.getScene();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; populateWithLevel(scene, 5);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Create an observer. This observer contains the callback</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// functions that may be called from the engine layer. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// We only need to add the functions that we are interested</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// in.</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> observer = {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">/*</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; * Called when the user clicked cancel in the map creation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; * view</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #006400;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; */</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; onMapCreationCancelled: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> () {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; startup();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Attach the observer to viper </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.setObserver(observer);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> populateWithLevel(scene, level) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.log(</span><span style="line-height: 140%; color: maroon;">&quot;Populate with level: &quot;</span><span style="line-height: 140%;"> + level);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Make sure CORS is enabled</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; jQuery.support.cors = </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Call our web-service with the appropriate level</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $.ajax(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; url:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&#39;http://apollonian.cloudapp.net/api/spheres/1/&#39;</span><span style="line-height: 140%;"> +</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; level,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; crossDomain: </span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; data: {},</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; dataType: </span><span style="line-height: 140%; color: maroon;">&quot;json&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; error: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (err) {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alert(err.statusText);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; },</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; success: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (data) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.log(</span><span style="line-height: 140%; color: maroon;">&quot;Successfully called web service.&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Process each sphere, adding it to the scene</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; $.each(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; data,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;"> (i, item) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Get shortcuts to our JSON data</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; viper.log(</span><span style="line-height: 140%; color: maroon;">&quot;Processing item &quot;</span><span style="line-height: 140%;"> + i);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> x = item.X, y = item.Y, z = item.Z,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; rad = item.R, level = item.L;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> length = Math.sqrt(x * x + y * y + z * z);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Only add spheres near the edge of the outer one</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (length + rad &gt; 0.99) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Create a spherical node</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> nodeID = </span><span style="line-height: 140%; color: maroon;">&quot;sphere_&quot;</span><span style="line-height: 140%;"> + i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> position =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> viper.math.Vector(x/3, y/3, z/3);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> sphere = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> viper.Node(nodeID, position);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sphere.setPrototype(</span><span style="line-height: 140%; color: maroon;">&quot;masterSphere&quot;</span><span style="line-height: 140%;"> + level);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; scene.addChild(sphere);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">script</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">head</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: maroon;">body</span><span style="line-height: 140%; color: blue;">/&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: maroon;">html</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
<p>If you want to give this a try yourself, install the PointCloud Browser and enter “<a href="http://autode.sk/appar" target="_blank">http://autode.sk/appar</a>” in the address bar: this will re-direct to the longer URL on my blog.</p>
<p>Once I’ve found a way to adjust the radii per instance, I think I’m going to investigate exporting simple 3D geometry from AutoCAD, to see what’s possible (the browser does support .OBJ for more complex objects – I’ll be looking at fairly simple stuff).</p>
