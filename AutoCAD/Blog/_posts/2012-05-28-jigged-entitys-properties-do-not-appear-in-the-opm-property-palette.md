---
layout: "post"
title: "Jigged entity's properties do not appear in the OPM (Property Palette)"
date: "2012-05-28 08:23:31"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/jigged-entitys-properties-do-not-appear-in-the-opm-property-palette.html "
typepad_basename: "jigged-entitys-properties-do-not-appear-in-the-opm-property-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>  <p>My code follows very closely the &quot;ObjectARX 2009 SDK\samples\editor\Palettes\BoltSolution&quot; sample but still the properties of the jigged entity do not appear in the OPM. </p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>After looking through your code it seems that you forgot to add the ACRX_CMD_INTERRUPTIBLE flag to the command you start the jig from:</p>  <div style="font-family: Courier New; font-size: 8pt; color: black; background: white;"> <p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Supports OPM display of command properties</span></p> <p style="margin: 0px;"><span style="color: blue; line-height: 140%;">#define</span><span style="line-height: 140%;"> ACRX_CMD_INTERRUPTIBLE&nbsp; &nbsp; &nbsp; 0x00400000</span></p> </div>
