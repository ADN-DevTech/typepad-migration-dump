---
layout: "post"
title: "RevitAPI: how to get Graphic Display Options"
date: "2016-02-18 00:02:55"
author: "Aaron Lu"
categories:
  - "Aaron Lu"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2016/02/revitapi-how-to-get-graphic-display-options.html "
typepad_basename: "revitapi-how-to-get-graphic-display-options"
typepad_status: "Publish"
---

<p><a href="http://blog.csdn.net/lushibi/article/details/50686704">中文链接</a></p>
<p><small>By <a href="http://adndevblog.typepad.com/aec/aaron-lu.html" target="_self">Aaron Lu</a></small></p>
<p>Several customers is asking how to get Graphic Display Options from Revit API, after my investigation, I found API did not expose all available information in UI, what are available is shown in below picture, makred with 1,2,3:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bbe34a970d-pi" style="display: inline;"><img alt="GraphicDisplayOptions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bbe34a970d image-full img-responsive" src="/assets/image_753551.jpg" title="GraphicDisplayOptions" /></a></p>
<p>Related methods are:</p>
<pre class="csharp prettyprint">ViewDisplayModel displayModel = view.GetViewDisplayModel(); //1
ViewDisplaySketchyLines sketchyLines = view.GetSketchyLines(); //2
SunAndShadowSettings sunAndShadowSettings = view.SunAndShadowSettings; //3</pre>
<p>&#0160;</p>
