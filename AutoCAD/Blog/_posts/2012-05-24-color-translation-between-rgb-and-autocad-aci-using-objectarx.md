---
layout: "post"
title: "Color translation between RGB and AutoCAD ACI using ObjectARX"
date: "2012-05-24 21:07:18"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/color-translation-between-rgb-and-autocad-aci-using-objectarx.html "
typepad_basename: "color-translation-between-rgb-and-autocad-aci-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Translating an RGB color to the nearest equivalent AutoCAD Color Index (ACI) can be done by using the "loopUpACI" and "lookUpRGB" methods implemented in 'AcCmEntityColor '.</p>
<p>The following examples uses the lookUpRGB() to convert the ACI value 47 to get the corresponding RGB values and using the same RGB values to get the ACI using lookUpACI() function</p>
<p>Here is the sample code : </p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcCmEntityColor cEntityColor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::UInt8 iIndex = 47;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the RGB equivalent of the ACI</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::UInt32 nValue = cEntityColor.lookUpRGB(iIndex);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::UInt8 blue, green, red;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now convert back to the original values, first 8 bits blue&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">blue = nValue;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now move nValue right 8 bits, green</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">nValue = nValue &gt;&gt; 8;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now get the green</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">green = nValue;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// move right to the next 8 bits </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">nValue = nValue &gt;&gt; 8;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// now get the red</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">red = nValue;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcCmColor cColor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">cColor.setRGB(red, green, blue);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the ACI equivalent of the RGB</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Adesk::UInt8 aci = AcCmEntityColor::lookUpACI(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cColor.red(),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cColor.green(),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cColor.blue()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
</div>
