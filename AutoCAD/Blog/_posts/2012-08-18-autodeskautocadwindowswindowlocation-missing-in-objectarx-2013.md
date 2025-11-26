---
layout: "post"
title: "Autodesk.AutoCAD.Windows.Window.Location missing in ObjectARX 2013"
date: "2012-08-18 05:22:31"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/08/autodeskautocadwindowswindowlocation-missing-in-objectarx-2013.html "
typepad_basename: "autodeskautocadwindowswindowlocation-missing-in-objectarx-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you are using the code in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/09/taking-screenshots-of-autocads-main-and-drawing-windows-using-net.html">this</a> blog post from <a href="http://through-the-interface.typepad.com/">Kean's blog</a>, you may find that "Autodesk.AutoCAD.Windows.Window" class no longer has the "Location" property in the 2013 SDK. It has been replaced with "DeviceIndependentLocation" property. Similarly, the "Size" property has been replaced by the "DeviceIndependentSize" property.</p>
<p>Here is a sample code snippet that uses the "DeviceIndependentLocation" and "DeviceIndependentSize" to determine the location and size of the window :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">Point pt = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)wd.DeviceIndependentLocation.X,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)wd.DeviceIndependentLocation.Y</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">Size sz = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Size&nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)wd.DeviceIndependentSize.Width, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)wd.DeviceIndependentSize.Height</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">System.Windows.Vector s = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; Autodesk.AutoCAD.Windows.Window.GetDeviceIndependentScale(IntPtr.Zero);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pt = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Point&nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)(wd.DeviceIndependentLocation.X * s.X),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)(wd.DeviceIndependentLocation.Y * s.Y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">sz = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> Size(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)(wd.DeviceIndependentSize.Width * s.X),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)(wd.DeviceIndependentSize.Height * s.Y)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; );</span></p>
</div>
