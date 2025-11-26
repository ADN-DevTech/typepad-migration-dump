---
layout: "post"
title: "CUI and images with transparency"
date: "2015-07-17 05:37:52"
author: "Virupaksha Aithal"
categories:
  - "2016"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/cui-and-images-with-transparency.html "
typepad_basename: "cui-and-images-with-transparency"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/virupaksha-aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Till AutoCAD 2014, AutoCAD used to take only BMP format images in CUI. As BMP format doesn&#39;t work with transparency, AutoCAD used to interpret RGB color 192,192,192 as transparent. AutoCAD users had used this workaround in CUI.&#0160;</p>
<p>But the limitation of this approach is that the background color is fixed as 192,192,192. Any other background color will make the background visible in CUI.</p>
<p>From AutoCAD 2015 (and later versions), you can provide PNG images in CUI. As PNG format supports transparency, you can set the transparency for the images and hence the workaround to set the background color as 192,192,192 is not required.</p>
<p>If you have a BMP with specific background color (like 192,192,192) then “Bitmap.MakeTransparent” API call can convert passed color as Transparent. Refer below code which converts the BMP with 192,192,192 background color to Transparent PNG</p>
<div style="font-family: Consolas; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">using</span> (<span style="color: #2b91af;">Bitmap</span> myBitmap = <span style="color: blue;">new</span> <span style="color: #2b91af;">Bitmap</span>(<span style="color: #a31515;">@&quot;C:\temp\transparent.bmp&quot;</span>))</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: green;">//assuming that first pixel will have back ground color</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">Color</span> backColor = myBitmap.GetPixel(0, 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myBitmap.MakeTransparent(backColor);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; myBitmap.Save(<span style="color: #a31515;">@&quot;C:\temp\transparent.png&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; System.Drawing.Imaging.<span style="color: #2b91af;">ImageFormat</span>.Png);</p>
<p style="margin: 0px;">}</p>
</div>
