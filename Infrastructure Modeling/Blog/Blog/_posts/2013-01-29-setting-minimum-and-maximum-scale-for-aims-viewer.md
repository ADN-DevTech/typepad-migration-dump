---
layout: "post"
title: "Setting minimum and maximum scale for AIMS/MapGuide viewer"
date: "2013-01-29 21:43:17"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/01/setting-minimum-and-maximum-scale-for-aims-viewer.html "
typepad_basename: "setting-minimum-and-maximum-scale-for-aims-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>  <p>In the Ajax Viewer, you can force the minimum and maximum scale zoom available for the user by modifying the following values in the file ..\WebServerExtensions\www\viewerfiles\ajaxmappane.templ :</p>  <p><i>var minScale = 1000, maxScale = 100000000;</i></p>  <p>The minimum scale zoom in this example is 1:1000 and the maximum scale zoom 1:100000000</p>  <p>In Fusion Viewer you can modify the minscale value (about line 338) in the LoadMap.php in the fusion\layers\MapGuide\php folder and this appears to do the trick. The line number may be different in different releases. Please note that there are 2 LoadMap.php in AIMS, you need to to edit the one in </p>  <p>C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2013\www\fusion\layers\<strong>MapGuide</strong>\php\LoadMap.php: </p>  <p>$minScale = &quot;0&quot;;&#160; //Edit this value</p>  <p>$maxScale = 'infinity'; // as MDF's VectorScaleRange::MAX_MAP_SCALE</p>  <p>&#160;</p>  <p>Please note that this is a global setting, it applies to all layers even all web applications hosted in this MapServer. If you just want to change one specific layer, you can edit the layer definition: </p>  <p>&lt;VectorScaleRange&gt;   <br /> &lt;&lt;MinScale&gt;500&lt;/MinScale&gt;     <br /> &lt;&lt;MaxScale&gt;10000&lt;/MaxScale&gt;&#160; <br />...</p>  <p> &lt;/VectorScaleRange&gt;</p>  <p>It can be set in MapGuide Studio layer editor.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40a00950970c-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_49b259.jpg" width="451" height="119" /></a></p>  <p>It can be done by API as well, using resource service. And further more, you can even make it user-specific by applying it to a temporary layer. Please refer the sample solution for a demo of resource resource(sokution3) and demo of temp layer(solution 5/6),they can be download from <a href="http://adndevblog.typepad.com/infrastructure/2012/05/devtv-and-code-sample-create-temporary-layer-feature-editing-in-aims2012.html">here</a>.</p>  <p>Hope this helps. </p>
