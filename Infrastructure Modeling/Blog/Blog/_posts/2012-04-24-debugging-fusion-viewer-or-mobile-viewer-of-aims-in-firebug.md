---
layout: "post"
title: "Debugging Fusion Viewer or Mobile Viewer of AIMS in Firebug"
date: "2012-04-24 00:24:30"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug.html "
typepad_basename: "debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>Have you ever started programming on Fusion viewer --as known as flexible web layout in Infrastructure Studio -- or Mobile viewer of Autodesk Infrastructure Map Server? If yes, this post may be helpful for you.</p>
<p>Fusion viewer and Mobile viewer are implemented with heavy JavaScript at client side and some PHP code at server side.&#0160; At most time, you will be fighting with JavaScript when extending the functionalities of Fusion Viewer or Mobile Viewer.&#0160; To debug JavaScript, a good toolset is Firefox + Firebug.</p>
<p>To improve performance, Fusion Viewer or Mobile Viewer are referencing a compressed version of JavaScript lib, which is created by ant + YUICompressor, but for debugging purpose, you will need the uncompressed version.</p>
<p>&#0160;</p>
<p>For Fusion Viewer, let’s say you are using the “slate” template, please open C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2013\www\fusion\templates\mapguide\slate\index.html with your favorite text editor, I am using notepad++, change following code:</p>
<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">script</span> <span class="attr">type</span><span class="kwrd">=&quot;text/javascript&quot;</span> <br /><span class="attr">src</span><span class="kwrd">=&quot;../../../lib/fusionSF-compressed.js&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">script</span><span class="kwrd">&gt;</span></pre>
<p>to</p>
<pre class="csharpcode"><span class="kwrd">&lt;</span><span class="html">script</span> <span class="attr">type</span><span class="kwrd">=&quot;text/javascript&quot;</span> <br /><span class="attr">src</span><span class="kwrd">=&quot;../../../lib/fusion.js&quot;</span><span class="kwrd">&gt;&lt;/</span><span class="html">script</span><span class="kwrd">&gt;</span></pre>
<p>&#0160;</p>
<p>For Mobile Viewer, please open C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\MobileViewer\index.html and change JavaScript and CSS file references as indicated in the comment:</p>
<pre class="csharpcode"><span class="rem">&lt;!-- For debugging, use these links--&gt;</span>
<span class="kwrd">&lt;</span><span class="html">link</span> <span class="attr">href</span><span class="kwrd">=&quot;css/mobileviewer.css&quot;</span> 
  <span class="attr">rel</span><span class="kwrd">=&quot;stylesheet&quot;</span> <span class="attr">media</span><span class="kwrd">=&quot;screen&quot;</span> <span class="attr">type</span><span class="kwrd">=&quot;text/css&quot;</span> <span class="kwrd">/&gt;</span>
<span class="kwrd">&lt;</span><span class="html">script</span> <span class="attr">type</span><span class="kwrd">=&quot;text/javascript&quot;</span> <span class="attr">src</span><span class="kwrd">=&quot;lib/mobileviewer.js&quot;</span><span class="kwrd">&gt;</span>
<span class="kwrd">&lt;/</span><span class="html">script</span><span class="kwrd">&gt;</span></pre>
<pre class="csharpcode"><span class="kwrd">&#0160;</span></pre>
<p><span class="kwrd">&#0160;</span></p>
<p>&#0160;</p>
<p>Now you are ready to open your webpage in Firefox and enable Firebug to debug. You can setup break points in JavaScript code and check the runtime value of objects in monitoring window. It also to step into or step over by pressing F11 or F10 to run through your JavaScript code. There are many other interesting functionalities provided by Firebug, so please give it a try and find more.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0167659dda27970b-pi"><img alt="image" border="0" height="397" src="/assets/image_26a21a.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="486" /></a></p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016304aa50a0970d-pi"><img alt="image" border="0" height="399" src="/assets/image_9b925c.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="484" /></a></p>
<p>&#0160;</p>
<p>Mobile Viewer is targeting the safari browser on iPad/iPhone/iPod touch, but it runs on Firefox on PC as well, so you can debug your extension on PC and check it on mobile devices. On the Apple<sup>®</sup>iPhone<sup>®</sup> itself, the settings for the Apple<sup>®</sup> Safari<sup>®</sup> app allow the debug console to be enabled (look under the &#39;Developer&#39; menu in the settings) which is useful in determining errors that cannot be reproduced on the desktop.</p>
<p>&#0160;</p>
<p>Of cause, the recent versions of other browsers also have developer tools, like IE9 or Google Chrome, which one do you prefer?</p>
