---
layout: "post"
title: "How to set a 'View Scale' in Map 3D using API?"
date: "2012-07-16 01:13:39"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/07/how-set-a-view-scale-in-map-3d-using-api.html "
typepad_basename: "how-set-a-view-scale-in-map-3d-using-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>In AutoCAD Map 3D UI, we can easily change / set the <strong>View Scale</strong> by specifying a new / custom scale value using the dropdown button next to the &quot;View Scale&quot; as shown in the screenshot below -</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0176167e18ee970c-pi" style="display: inline;"><img alt="ViewScale_01" class="asset  asset-image at-xid-6a0167607c2431970b0176167e18ee970c" src="/assets/image_dc57f9.jpg" title="ViewScale_01" /></a></p>
<p>&#0160;</p>
<p>You can achieve the same using Map 3D Platform API <strong>AcMapMap.SetViewScale(double scale)</strong>. Here is a .NET code snippet which demonstrates the same :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the Map Object</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;"> currentMap = </span><span style="color: #2b91af; line-height: 140%;">AcMapMap</span><span style="line-height: 140%;">.GetCurrentMap();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Double</span><span style="line-height: 140%;"> viewScale = currentMap.GetViewScale();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;ViewScale Before Change : &quot;</span><span style="line-height: 140%;"> + viewScale.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// set the ViewScale</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">viewScale = (viewScale / 100);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">currentMap.SetViewScale(viewScale);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;ViewScale After Change : &quot;</span><span style="line-height: 140%;"> + viewScale.ToString());</span></p>
</div>
<p>&#0160;</p>
<p>And the result you would see after running in the above code snippet :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743643c0c970d-pi" style="display: inline;"><img alt="ViewScale_02" class="asset  asset-image at-xid-6a0167607c2431970b017743643c0c970d" src="/assets/image_f8f1fb.jpg" title="ViewScale_02" /></a></p>
<p>Hope this is useful to you!</p>
