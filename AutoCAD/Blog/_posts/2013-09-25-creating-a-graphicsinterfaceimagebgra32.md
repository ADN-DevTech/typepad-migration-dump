---
layout: "post"
title: "Creating a GraphicsInterface.ImageBGRA32"
date: "2013-09-25 07:06:47"
author: "Balaji"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/09/creating-a-graphicsinterfaceimagebgra32.html "
typepad_basename: "creating-a-graphicsinterfaceimagebgra32"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Here is a sample code to demonstrate the creation of an instance of "ImageBGRA32" class from a bitmap and to use it in a "WorldDraw" method. The code to copy the raw bytes between two arrays is not optimized and I think there are more efficient way to do it. But I hope this sample code helps you get started.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Circle c = (Circle)drawable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Bitmap bitmap = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Bitmap(@</span><span style="color: #a31515; line-height: 140%;">&quot;c:\Work\Test.bmp&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">BitmapData raw = bitmap.LockBits</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Rectangle(0, 0, (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)bitmap.Width, (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)bitmap.Height), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ImageLockMode.ReadOnly, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; bitmap.PixelFormat</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> size = raw.Height * raw.Stride;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">byte[] rawImageRGB24 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Byte[size];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">System.Runtime.InteropServices.Marshal.Copy(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; raw.Scan0, rawImageRGB24, 0, size);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">byte[] rawImageBGRA32 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Byte[bitmap.Width * bitmap.Height * 4];</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> rgbIndex = 0;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> rgbaIndex = 0;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> row = 0; row &lt; bitmap.Height; row++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> col = 0; col &lt; bitmap.Width; col++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rgbIndex = col * 3 + row * raw.Stride;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rgbaIndex = col * 4 + row * bitmap.Width * 4;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rawImageBGRA32[rgbaIndex] </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = rawImageRGB24[rgbIndex];&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Blue</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rawImageBGRA32[rgbaIndex + 1] </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = rawImageRGB24[rgbIndex + 1];&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Green</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rawImageBGRA32[rgbaIndex + 2] </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = rawImageRGB24[rgbIndex + 2];&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Red</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; rawImageBGRA32[rgbaIndex + 3] </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = 255;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Alpha</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (raw != null)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; bitmap.UnlockBits(raw);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">IntPtr unmanagedPointer </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = Marshal.AllocHGlobal(rawImageBGRA32.Length);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Marshal.Copy(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; rawImageBGRA32, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 0, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; unmanagedPointer, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; rawImageBGRA32.Length); </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ImageBGRA32 imgBGRA32 </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> ImageBGRA32</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (uint)bitmap.Width, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (uint)bitmap.Height, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; unmanagedPointer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> result </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = wd.Geometry.Image</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; imgBGRA32, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; c.Center, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Vector3d.XAxis * (bitmap.Width / bitmap.Height), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; Vector3d.YAxis</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Marshal.FreeHGlobal(unmanagedPointer);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;"> result;</span></p>
</div>
