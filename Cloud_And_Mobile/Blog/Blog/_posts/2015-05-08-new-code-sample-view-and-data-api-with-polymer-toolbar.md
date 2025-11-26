---
layout: "post"
title: "New Code Sample: View and Data API with Polymer Toolbar"
date: "2015-05-08 16:52:36"
author: "Shiya Luo"
categories:
  - "Client"
  - "HTML"
  - "HTML5"
  - "Shiya Luo"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/05/new-code-sample-view-and-data-api-with-polymer-toolbar.html "
typepad_basename: "new-code-sample-view-and-data-api-with-polymer-toolbar"
typepad_status: "Publish"
---

<p style="text-align: left;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/shiya-luo.html">Shiya Luo</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d110de6a970c-pi" style="display: inline;"><img alt="E9b456e6-ef67-11e4-9c1d-9328b44df298" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d110de6a970c image-full img-responsive" src="/assets/image_9babcc.jpg" title="E9b456e6-ef67-11e4-9c1d-9328b44df298" /></a></p>
<p>As an experiment, Nop Jiarathanakul, graphics engineer at Autodesk, has built customizable toolbars for the View and Data API. Polymer is a library from Google built on top of web components, which encapsulate code and let you call them with custom HTML elements.&#0160;</p>
<p>Previously, developers had to struggle with either calling the <code>Viewer3D</code> function in JavaScript and have to write their own UI components and functions. The other choice was to call the “private” function <code>GuiViewer3D</code>, which is essentially the Autodesk 360 toolbar. The built in toolbar is not customizable, and can sometimes be broken by CSS on the page. Although&#0160; the <code>GuiViewer3D</code> function is convenient, developers will have to take whatever the Autodesk 360 team decides to do with the toolbar.</p>
<p>This is an effort to offer up a library for developers who wants to customize their toolbars without having to do all the heavy lifting. Instead of putting a <code>&lt;div&gt;</code> element on the page and let JavaScript populate the area, you can simply include the viewer using this single line of code:</p>
<pre><code>&lt;lmv-viewer url=“https://lmv.rocks/data/engineraw/0.svf”&gt;&lt;/lmv-viewer&gt;</code></pre>
<p>The goal of this toolbar is to let developers be able to implement the View and Data API and easily customize the toolbar without writing a single line of javascript.</p>
<p>A demo of this toolbar can be found at <a href="http://lmv.rocks/" target="_blank" title="lmv.rocks">http://lmv.rocks/</a></p>
<p>The source code is on <a href="https://github.com/nopjia/lmv-polymer" target="_self">GitHub</a>.</p>
