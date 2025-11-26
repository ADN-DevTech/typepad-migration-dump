---
layout: "post"
title: "RevitAPI: How to get view position in sheet"
date: "2015-05-04 18:01:00"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2015/05/revitapi-how-to-get-view-position-in-sheet.html "
typepad_basename: "revitapi-how-to-get-view-position-in-sheet"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/45192273">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>View inserted into Sheet will become Viewport, when we select the view port, snoop it with RevitLookup, we can see:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1070807970c-pi" style="display: inline;"><img alt="Viewport" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1070807970c image-full img-responsive" src="/assets/image_622325.jpg" title="Viewport" /></a></p>
<p>So how to get its location? After looking at RevitAPI.chm, we can find there are 3 relevant methods:</p>
<pre class="csharp">//Returns the center of the outline of the viewport on the sheet, <br />// excluding the viewport label.
    XYZ GetBoxCenter();

//Returns the outline of the viewport on the sheet, <br />// excluding the viewport label.
     Outline GetBoxOutline();

//Gets the outline viewport&#39;s label on the sheet.
    Outline GetLabelOutline();
</pre>
<p><br /> Let&#39;s see the meaning of those methods directly with below image:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1070811970c-pi" style="display: inline;"><img alt="GetViewportLocation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1070811970c image-full img-responsive" src="/assets/image_192863.jpg" title="GetViewportLocation" /></a></p>
