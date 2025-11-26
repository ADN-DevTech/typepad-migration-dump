---
layout: "post"
title: "Restore view panel position to top when opening in Mobile Viewer"
date: "2012-04-27 01:26:20"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/restore-view-panel-position-to-top-when-opening-in-mobile-viewer.html "
typepad_basename: "restore-view-panel-position-to-top-when-opening-in-mobile-viewer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>In Mobile Viewer, if you open a view panel, let’s say the properties panel, scroll down to bottom then close it. Next time you open the same view panel you will found it still show the bottom part of the panel, it does not go back to the page top. It may be not the expected behavior for some users, they prefer to go the page top every time when opening the view panel. How do we do?</p>
<p>This topic is raised by Nivashini from V3 Teletech Pte Ltd and he finds out a solution too. He is very kind to share his solution to help others who are trying to do the same thing.</p>
<p>The solution is simple, open C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\MobileViewer\lib\mobileviewer_viewpanel.js, around line 110, change it as below:</p>
<div style="font-family: courier new; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; showPanel: </span><span style="line-height: 140%; color: blue;">function</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.parentDiv)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.parentDiv.style.visibility = </span><span style="line-height: 140%; color: #a31515;">&#39;visible&#39;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="background-color: #ffff00;"><span style="line-height: 140%; color: blue;">this</span><span style="line-height: 140%;">.scrollPanelDiv.style.top = 0;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; },</span></p>
</div>
<p>You need to change to script reference to “lib/mobileviewer.js&quot; to make it work, please check out <a href="http://adndevblog.typepad.com/infrastructure/2012/04/debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug.html" target="_blank">this post</a> to get more information</p>
<p>Thank you Nivashini for sharing this!</p>
