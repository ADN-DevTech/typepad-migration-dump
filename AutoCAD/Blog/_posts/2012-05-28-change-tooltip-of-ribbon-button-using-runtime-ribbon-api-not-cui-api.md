---
layout: "post"
title: "Change tooltip of Ribbon button using Runtime Ribbon API (not CUI API)"
date: "2012-05-28 08:45:58"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/change-tooltip-of-ribbon-button-using-runtime-ribbon-api-not-cui-api.html "
typepad_basename: "change-tooltip-of-ribbon-button-using-runtime-ribbon-api-not-cui-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>I want to change the tooltip of my ribbon button. I was hoping to do it like this:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">thisCmdbutton.ToolTip.Title = tipTitle;</span></p>    <p style="margin: 0px"><span style="line-height: 140%">thisCmdbutton.ToolTip.Content = tipContent;</span></p> </div>  <p>Unfortunately RibbonButton.ToolTip does not seem to have any string properties.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>RibbonButton.ToolTip simply returns an object, but you can cast it to RibbonToolTip:</p>  <div style="font-family: Courier New; font-size: 8pt; color: black; background: white;"> <p style="margin: 0px;"><span style="line-height: 140%;">RibbonToolTip toolTip = myRibbonButton.ToolTip </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> RibbonToolTip;</span></p> <p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (toolTip == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p> <p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p> <p style="margin: 0px;">&nbsp;</p> <p style="margin: 0px;"><span style="line-height: 140%;">toolTip.Title = </span><span style="color: #a31515; line-height: 140%;">&quot;New Title&quot;</span><span style="line-height: 140%;">;</span></p> <p style="margin: 0px;"><span style="line-height: 140%;">toolTip.Content = </span><span style="color: #a31515; line-height: 140%;">&quot;New Content&quot;</span><span style="line-height: 140%;">;</span></p> </div>
