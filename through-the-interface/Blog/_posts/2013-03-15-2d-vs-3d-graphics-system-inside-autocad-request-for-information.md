---
layout: "post"
title: "2D vs. 3D graphics system inside AutoCAD (+ request for information)"
date: "2013-03-15 07:34:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Graphics system"
original_url: "https://www.keanw.com/2013/03/2d-vs-3d-graphics-system-inside-autocad-request-for-information.html "
typepad_basename: "2d-vs-3d-graphics-system-inside-autocad-request-for-information"
typepad_status: "Publish"
---

<p>Everyone who uses AutoCAD – even if they use it exclusively in one or the other mode – knows that it’s capable of being used to generate both 2D drawings and 3D models. Not everyone realises there are actually two distinct graphics systems in the product, however. (At least at the time of writing, talking about AutoCAD 2013… I’m not making predictions, for people reading this from the future. :-)</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017ee94bd5b3970d-pi" target="_blank"><img alt="A 2D and 3D character, almost certainly not inside AutoCAD" border="0" height="217" src="/assets/image_215169.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border-width: 0px;" title="A 2D and 3D character, almost certainly not inside AutoCAD" width="454" /></a></p>
<p>The 2D graphics system is known as WHIP and has been around – albeit with regular enhancements – since the days of R13. You know if you’re using WHIP if the current Visual Style is set to “2D Wireframe”. What’s sometimes a little confusing is that WHIP is also 3D-capable: it’s not as though everything appears flattened onto the XY plane when WHIP is enabled, for instance. But it is focused on – and highly optimised for – working with 2D data.</p>
<p>The 3D graphics system has been in the product – again, with various incremental improvements, release-on-release – since AutoCAD 2007. For the sake of this post we’re going to refer to the 3D GS as AGS (AutoCAD Graphics System). You know if you’re using AGS if the current Visual Style is set to <em>anything other than</em> “2D Wireframe”.</p>
<p>Programmatically it’s possible to check whether you’re using WHIP or AGS via one simple API: if the result of calling Document.GraphicsManager.GetGsView() (making sure you pass “false” as the second argument, as we don’t want to create a new view) is null, then you know you’re using WHIP, otherwise you’re using AGS.</p>
<p>[Many developers who care whether they’re in WHIP or AGS are working with ObjectARX from C++, a requirement for implementing custom entities: these developers will check whether acgsGetGsView() returns NULL. This is, of course, the underlying unmanaged API that is wrapped by the .NET API mentioned above.]</p>
<p>Here’s some simple C# code that reports whether you’re in 2D or 3D (from a GS perspective, that is):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.GraphicsSystem;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">namespace</span><span style="line-height: 140%;"> GSTest</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; [</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;GGS&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> GetGS()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> doc = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">var</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">short</span><span style="line-height: 140%;"> vpn = (</span><span style="line-height: 140%; color: blue;">short</span><span style="line-height: 140%;">)</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="line-height: 140%; color: #a31515;">&quot;CVPORT&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">View</span><span style="line-height: 140%;"> v = doc.GraphicsManager.GetGsView(vpn, </span><span style="line-height: 140%; color: blue;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;\nGraphics system is {0}D.&quot;</span><span style="line-height: 140%;">, v == </span><span style="line-height: 140%; color: blue;">null</span><span style="line-height: 140%;"> ? </span><span style="line-height: 140%; color: #a31515;">&quot;2&quot;</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #a31515;">&quot;3&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Running the GGS command should report “Graphics system is 2D” when using WHIP (i.e. with Visual Style is “2D Wireframe”) and “Graphics system is 3D” when using AGS (i.e. with any other Visual Style).</p>
<p>As these graphics systems have different capabilities – such as the way graphics are displayed – it’s sometimes important for applications to be aware of whether the current graphics system is WHIP or AGS. This is something that’s pretty common for applications with complex graphics – the AutoCAD-based verticals are examples of these – but it’s something we’d like to understand better and reduce the need for. Our ideal scenario would be to provide a unified graphics API that doesn’t require any knowledge of the specific, underlying GS.</p>
<p>Now we’ve worked through the preamble, here’s what I’m interested in knowing… Does your application at any point check whether it is in the 2D or 3D graphics system? If so, why does it need to know (i.e. what does the application use the information for)? The gist of the question I’m really asking is this: what capabilities would you therefore like to see generalised across both graphics system implementations? (Please <a href="mailto:kean.walmsley@autodesk.com" target="_blank">send me an email</a>, assuming you don’t want to post this information in a blog comment for the world to see. :-)</p>
<p>To be clear, I don’t actually expect many people will respond to this, as this is quite a niche area for developers who are making heavy use of custom graphics (whether via custom entities or overrules). But then I may well be surprised, and any information in this area would certainly help the AutoCAD team a great deal.</p>
<p><span style="color: #666666;">photo credit: </span><a href="http://www.flickr.com/photos/audiovisualjunkie/7861683004/"><span style="color: #666666;">audiovisualjunkie</span></a><span style="color: #666666;"> via </span><a href="http://photopin.com"><span style="color: #666666;">photopin</span></a><span style="color: #666666;"> </span><a href="http://creativecommons.org/licenses/by-nc-nd/2.0/"><span style="color: #666666;">cc</span></a></p>
