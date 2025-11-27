---
layout: "post"
title: "An augmented reality view of an Apollonian packing using PointCloud Browser"
date: "2013-02-15 08:05:00"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "Augmented Reality"
  - "Geometry"
  - "HTML"
  - "JavaScript"
  - "JSON"
  - "SaaS"
original_url: "https://www.keanw.com/2013/02/an-augmented-reality-view-of-an-apollonian-packing-using-pointcloud-browser.html "
typepad_basename: "an-augmented-reality-view-of-an-apollonian-packing-using-pointcloud-browser"
typepad_status: "Publish"
---

<p>After <a href="http://through-the-interface.typepad.com/through_the_interface/2013/02/using-slam-based-augmented-reality-to-visualize-3d-geometry.html" target="_blank">my initial (only partially successful) attempt</a>, earlier in the week, to get 3D geometry from <a href="http://through-the-interface.typepad.com/through_the_interface/2012/06/cloud-mobile-series-summary.html" target="_blank">the Apollonian web-service</a> into a <a href="http://pointcloud.io" target="_blank">PointCloud Browser</a> session, I finally managed to get it working properly.</p>
<p>Given the currently fairly light <a href="http://developer.pointcloud.io/browser/docs" target="_blank">documentation available</a> – especially for the Viper JavaScript namespace which gives access to the 3D rendering capabilities in the browser – I ended up posting <a href="http://forum.pointcloud.io/discussion/105/creating-spheres-of-different-radii-via-the-same-mesh-object" target="_blank">a question to the PointCloud forum</a>. The answer was very instructive – I was able not only to get spheres of different radii displayed using the same mesh…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c36d6acf1970b-pi" target="_blank"><img alt="All in white" border="0" height="296" src="/assets/image_497828.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="All in white" width="394" /></a></p>
<p>… but also to apply different colours to the same mesh via tinting. Here’s an intermediate step I hit (I’m not fully sure why the colours came out as they did, but anyway)…</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d4105b55e970c-pi" target="_blank"><img alt="A bit washed out" border="0" height="296" src="/assets/image_400955.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A bit washed out" width="394" /></a></p>
<p>… before getting much more satisfactory results. With fewer lines of code.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c36d6ae2d970b-pi" target="_blank"><img alt="That&#39;s better" border="0" height="296" src="/assets/image_88517.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="That&#39;s better" width="394" /></a></p>
<p>Here’s the updated HTML page (also available <a href="http://through-the-interface.typepad.com/files/apollonian-ar2.html" target="_blank">here</a> and at <a href="http://autode.sk/appar2" target="_blank">this shortened URL</a>):</p>
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
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_mesh&quot; radius=&quot;1&quot; primitive=&quot;sphere&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; details=&quot;2&quot; /&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;node id=&quot;sphere_node&quot;&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;model</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; texture_src=&quot;resources/images/white-4x4.png&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; id=&quot;sphere_model&quot; mesh=&quot;sphere_mesh&quot;/&gt;</span></p>
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
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&#39;http://apollonian.cloudapp.net/api/spheres/0.3/&#39;</span><span style="line-height: 140%;"> +</span></p>
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
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Hard-code the colour for each level in an array</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> colors =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; [ </span><span style="line-height: 140%; color: maroon;">&quot;0,0,0,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;1,0,0,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;1,1,0,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;0,1,0,1&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;0,1,1,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;0,0,1,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;1,0,1,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;0.9,0.9,0.9,1&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;0.6,0.6,0.6,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;0.3,0.3,0.3,1&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: maroon;">&quot;1,1,1,1&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: maroon;">&quot;1,1,1,1&quot;</span><span style="line-height: 140%;"> ]</span></p>
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
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (length + rad &gt; 0.29) {</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #006400;">// Create a spherical node</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> nodeID = </span><span style="line-height: 140%; color: maroon;">&quot;sphere_&quot;</span><span style="line-height: 140%;"> + i;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> position = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> viper.math.Vector(x, y, z);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> sphere = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> viper.Node(nodeID, position);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sphere.setPrototype(</span><span style="line-height: 140%; color: maroon;">&quot;sphere_node&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sphere.setScale(rad);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; sphere.setTint(colors[parseInt(level)]);</span></p>
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
<p>The page depends on an additional texture (which you can get <a href="http://through-the-interface.typepad.com/files/resources/images/white-4x4.png" target="_blank">here</a>) but that should be of modest inconvenience).</p>
<p>To really get a sense of the responsiveness of the PointCloud Browser when viewing this 3D geometry, here’s a quick video I recorded this morning to show it in action:</p>
<br /><iframe allowfullscreen="allowfullscreen" frameborder="0" height="264" src="http://www.youtube.com/embed/-1IGfYETFEw?rel=0" width="470"></iframe>  <br />  <br />
<p>For fun, here’s a level 7 packing (you can tweak the level in the call to populateWithLevel(), above). This change does slow down the load considerably – which stalls the “reality map acquisition” process for several seconds – even though the runtime performance on my iPad 2 remains pretty good. So I’ve left the posted versions at level 5.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017d4105b7b1970c-pi" target="_blank"><img alt="At level 7" border="0" height="296" src="/assets/image_414988.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="At level 7" width="394" /></a></p>
