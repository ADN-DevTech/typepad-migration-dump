---
layout: "post"
title: "Rearranging ribbon panels"
date: "2012-06-09 17:46:46"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/rearranging-ribbon-panels.html "
typepad_basename: "rearranging-ribbon-panels"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p><p>Here is a small code snippet that uses the "Move" method to change the ribbon panel position within the same ribbon tab :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> tabName = </span><span style="color: #a31515; line-height: 140%;">&quot;MyTab&quot;</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> panelName = </span><span style="color: #a31515; line-height: 140%;">&quot;MyPanel2&quot;</span><span style="line-height: 140%;">; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Autodesk.Windows.RibbonControl ribCntrl =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Autodesk.AutoCAD.Ribbon.RibbonServices.RibbonPaletteSet.RibbonControl;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (Autodesk.Windows.RibbonTab tab </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> ribCntrl.Tabs)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (tab.Name != tabName) </span><span style="color: blue; line-height: 140%;">continue</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> oldIndex = -1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (Autodesk.Windows.RibbonPanel panel </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> tab.Panels)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; oldIndex++;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (panel.Source.Title != panelName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">continue</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">//To ensure that our panel gets displayed as the </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// first panel in the ribbon tab</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; tab.Panels.Move(oldIndex, 0);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
