---
layout: "post"
title: "Speed up the scrolling of Mobile Viewer panel"
date: "2012-05-10 09:15:00"
author: "Daniel Du"
categories:
  - "AIMS 2012"
  - "AIMS 2013"
  - "Daniel Du"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/05/speed-up-the-scrolling-of-mobile-viewer-panel.html "
typepad_basename: "speed-up-the-scrolling-of-mobile-viewer-panel"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/daniel-du.html">Daniel Du</a></p>
<p>You may find the the scrolling of legend and property panel is too slow in iPad when you have many items in the panel.</p>
<p>Here is a workaround for this.</p>
<p>1. Open MobileViewer\lib\mobileviewerSF.js</p>
<p>2. Go to function onScroll: function(e)</p>
<p>3. Add diffY *= 4; after if(diffY != 0)</p>
<pre class="csharpcode"><span class="kwrd">if</span>(diffY != 0)
{
  <strong> diffY *= 4;</strong>
   <span class="kwrd">var</span> newTop = <span class="kwrd">this</span>.startDragOffsetY - diffY;
    <span class="kwrd">var</span> visibleHeight = <span class="kwrd">this</span>.parentDiv.clientHeight <br />                     - <span class="kwrd">this</span>.titleBarDiv.clientHeight;
    <span class="kwrd">if</span>(newTop &lt; visibleHeight <br />                 - <span class="kwrd">this</span>.scrollPanelDiv.scrollHeight)
    {
        newTop = visibleHeight <br />                  - <span class="kwrd">this</span>.scrollPanelDiv.scrollHeight;
        <span class="kwrd">this</span>.captureDragStart(e);
    }
...</pre>
<p>To make it effective, please open C:\Program Files\Autodesk\Autodesk Infrastructure Web Server Extension 2012\www\MobileViewer\index.html and change JavaScript references to this modified file:</p>
<pre>&lt;!-- For debugging, use these links--&gt;
&lt;script type=&quot;text/javascript&quot; src=&quot;lib/mobileviewerSF.js&quot;&gt;
&lt;/script&gt;</pre>
<p><span style="font-family: Arial;">You may want to check <a href="http://adndevblog.typepad.com/infrastructure/2012/04/debugging-fusion-viewer-or-mobile-viewer-of-aims-in-firebug.html" target="_blank">this blog</a> for more detail information.</span></p>
<p>Hope this helps!</p>
