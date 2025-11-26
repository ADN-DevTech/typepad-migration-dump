---
layout: "post"
title: "Markup3D Sample for View & Data API"
date: "2016-04-25 00:55:35"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/04/markup3d-sample-for-view-data-api.html "
typepad_basename: "markup3d-sample-for-view-data-api"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif;">By&#0160;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html" target="_blank">Philippe Leefsma</a> <a href="https://twitter.com/F3lipek" target="_blank">(@F3lipek)</a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">Here is a reworked version of one of my early sample for View &amp; Data API: This illustrates how you can create markups pointing to a specific point on your model. The main feature is that the markups display a 2D symbol on the canvas and is hooked up to camera events, so when rotating the model, the 3D&#0160;world point changes position and so does the 2D symbol, giving the impression that the markup sticks to that point.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">There are few remarks about the implementation that I wanted to highlight in this post:</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">I extensively used&#0160;SVG&#0160;to handle any 2D symbol on top of the WebGL canvas: this is clearly what gives the most flexibility and nicest visuals&#0160;as far as 2D graphics are concerned. To manipulate SVG, I find <a href="http://snapsvg.io/">Snap.svg</a> pretty handy, a thin wrapper I already <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/02/svg-graphics-with-snapsvg.html">blogged about</a> previously.&#0160;</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">I am splitting the source into several files as it makes sense and also facilitate code reuse, then I rely on a build step using <a href="https://webpack.github.io/">webpack</a> to bundle all those files, including also the .css, into a single .js that can easily be loaded by my main application. Also webpack makes it very easy to use <a href="https://babeljs.io/">Babel</a> to transpile code to enjoy all the <a href="https://kadira.io/blog/other/top-es2015-features-in-15-minutes">benefits of ES2015&#0160;modern syntax</a>.</span></p>
<p><span style="font-family: arial, helvetica, sans-serif;">Here are how&#0160;the extension files look like:&#0160;</span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c847dd09970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Screen Shot 2016-04-22 at 17.32.15" class="asset  asset-image at-xid-6a0167607c2431970b01b7c847dd09970b img-responsive" height="257" src="/assets/image_506f28.jpg" title="Screen Shot 2016-04-22 at 17.32.15" width="522" /></a></p>
<p>The entry point is <em><strong>Viewing.Extension.Markup3D.js</strong></em> but most the logic is implemented in the tool and LeaderNote classes. One thing that is interesting to&#0160;point out is that&#0160;the markups can be stored as part of the state. This isn&#39;t a documented feature, but if you look into the source code for <strong><em>viewer.getState</em></strong> and <strong><em>viewer.restoreState</em></strong> you will see that&#0160;those methods are iterating through the loaded extensions in order to let them inject or restore custom state data.&#0160;</p>
<p>In order to use that feature, all you need to do is implementing those same methods in your extension, which looks has below in my sample:</p>
<pre style="line-height: 100%; font-family: monospace; background-color: #ffffff; padding: 4px; font-size: 10pt; border: 0.01mm solid #000000;"><span style="color: #800000; background-color: #f0f0f0;"> 1 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;"> 2 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 3 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//  From viewer.getState: 
</span><span style="color: #800000; background-color: #f0f0f0;"> 4 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//  Allow extensions to inject their state data
</span><span style="color: #800000; background-color: #f0f0f0;"> 5 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;"> 6 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//  for (var extensionName in viewer.loadedExtensions) {
</span><span style="color: #800000; background-color: #f0f0f0;"> 7 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//    viewer.loadedExtensions[extensionName].getState(
</span><span style="color: #800000; background-color: #f0f0f0;"> 8 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//      viewerState);
</span><span style="color: #800000; background-color: #f0f0f0;"> 9 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//  }
</span><span style="color: #800000; background-color: #f0f0f0;">10 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;">11 </span><span style="background-color: #ffffff;">getState(viewerState) {
</span><span style="color: #800000; background-color: #f0f0f0;">12 
13 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.markup3DTool.getState(
</span><span style="color: #800000; background-color: #f0f0f0;">14 </span><span style="background-color: #ffffff;">    viewerState);
</span><span style="color: #800000; background-color: #f0f0f0;">15 </span><span style="background-color: #ffffff;">}
</span><span style="color: #800000; background-color: #f0f0f0;">16 
17 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;">18 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;">19 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//    From viewer.restoreState: 
</span><span style="color: #800000; background-color: #f0f0f0;">20 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//    Allow extensions to restore their data
</span><span style="color: #800000; background-color: #f0f0f0;">21 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//
</span><span style="color: #800000; background-color: #f0f0f0;">22 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//    for (var extensionName in viewer.loadedExtensions) {
</span><span style="color: #800000; background-color: #f0f0f0;">23 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//      viewer.loadedExtensions[extensionName].restoreState(
</span><span style="color: #800000; background-color: #f0f0f0;">24 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//        viewerState, immediate);
</span><span style="color: #800000; background-color: #f0f0f0;">25 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">//    }
</span><span style="color: #800000; background-color: #f0f0f0;">26 </span><span style="color: #808080; background-color: #ffffff; font-style: italic;">/////////////////////////////////////////////////////////////////
</span><span style="color: #800000; background-color: #f0f0f0;">27 </span><span style="background-color: #ffffff;">restoreState(viewerState, immediate) {
</span><span style="color: #800000; background-color: #f0f0f0;">28 
29 </span>  <span style="color: #000080; background-color: #ffffff; font-weight: bold;">this</span><span style="background-color: #ffffff;">.markup3DTool.restoreState(
</span><span style="color: #800000; background-color: #f0f0f0;">30 </span><span style="background-color: #ffffff;">    viewerState, immediate);
</span><span style="color: #800000; background-color: #f0f0f0;">31 </span><span style="background-color: #ffffff;">}</span></pre>
<p>The complete implementation is available from there: <a href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/tree/master/src/Viewing.Extension.Markup3D">Viewing.Extension.Markup3D</a></p>
<p>And <a href="https://lmv-react.herokuapp.com/embed?id=560c6c57611ca14810e1b2bf&amp;extIds=Viewing.Extension.StateManager;Viewing.Extension.Markup3D&amp;options=%27{%22showPanel%22:%22true%22,%22stateId%22:%22a9f227f6154286f00c7%22}%27" target="_blank">here is a sample you can play with</a>. It also includes the StateManager so you can save and restore markups. Click any point on the model to create new markups, double click a label to edit the markup properties, drag an existing label to modify its position.</p>
<p>&#0160;<iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/ABgHQVj-h3c?feature=oembed" width="459"></iframe>&#0160;</p>
<script src="https://gist.github.com/leefsmp/a91ac8a14ee33a5a3ddf64e85824c794.js"></script>
