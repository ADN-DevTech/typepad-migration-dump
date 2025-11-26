---
layout: "post"
title: "The property View.Viewport is not available any more in AutoCAD .NET 2013 API"
date: "2012-04-02 15:32:57"
author: "Gopinath Taget"
categories:
  - ".NET"
  - "AutoCAD"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/the-property-viewviewport-is-not-available-any-more-in-autocad-net-2013-api.html "
typepad_basename: "the-property-viewviewport-is-not-available-any-more-in-autocad-net-2013-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html" target="_self">Gopinath Taget</a></p>
<p>The property Autodesk.AutoCAD.GraphicsSystem.<strong>View.Viewport</strong> has been removed from the AutoCAD .NET 2013 API. You can use the Autodesk.AutoCAD.GraphicsSystem<strong>.View.<span style="color: #0000ff;">ViewportExtents</span></strong> property instead.</p>
<p>There is&#0160; a little difference between these two properties though: <br />&#0160;<em>Viewport property returned the extents of the viewport in device coordinates (pixels), but ViewportExtents property returns the extents of the viewport in <strong>normalized</strong> device coordinates.</em></p>
<p>What does normalized device coordinates mean? In this context, it means that the viewport extents are normalized against the size of the device object containing the viewport. So the approach to get the proper viewport size is as follows:</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #2b91af;"><span>Device</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;"> dev = view.Device;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>System.Drawing.Size s = dev.Size;&#0160; </span></span></span><span style="line-height: 11pt;"><span style="color: #008000;">//container or window size</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #2b91af;"><span>Extents2d</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;"> extents = view.ViewportExtents;</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #2b91af;"><span>Point2d</span></span></span><span><span style="line-height: 11pt;"><span style="color: #000000;"> min_pt = </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">Point2d</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">(extents.MinPoint.X * s.Width, </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; extents.MinPoint.Y * s.Height);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #2b91af;"><span>Point2d</span></span></span><span><span style="line-height: 11pt;"><span style="color: #000000;"> max_pt = </span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> </span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">Point2d</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">(extents.MaxPoint.X * s.Width, </span></span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;&#0160;&#0160; extents.MaxPoint.Y * s.Height);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="line-height: 11pt;"><span style="font-family: Courier New;"><span style="color: #000000;">System.Drawing.Rectangle view_rect = </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span style="line-height: 11pt;"><span style="color: #0000ff;">new</span></span><span style="line-height: 11pt;"><span style="color: #000000;"> System.Drawing.Rectangle(</span></span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="line-height: 11pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">int</span></span><span style="line-height: 11pt;"><span style="color: #000000;">)min_pt.X, </span></span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span><span style="line-height: 11pt;"><span style="color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span></span><span style="line-height: 11pt;"><span style="color: #0000ff;">int</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">)min_pt.Y,</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span></span></span><span><span style="line-height: 11pt;"><span style="color: #0000ff;">int</span></span><span style="line-height: 11pt;"><span style="color: #000000;">)System.</span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">Math</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">.Abs(max_pt.X - min_pt.X), </span></span></span></p>
<p style="margin: 0px;"><span style="font-family: Courier New;"><span style="line-height: 11pt;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span></span></span><span><span style="line-height: 11pt;"><span style="color: #0000ff;">int</span></span><span style="line-height: 11pt;"><span style="color: #000000;">)System.</span></span><span style="line-height: 11pt;"><span style="color: #2b91af;">Math</span></span></span><span style="line-height: 11pt;"><span style="color: #000000;">.Abs(max_pt.Y - min_pt.Y));</span></span></span></p>
</div>
