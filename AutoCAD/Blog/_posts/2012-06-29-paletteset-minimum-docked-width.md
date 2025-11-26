---
layout: "post"
title: "PaletteSet minimum docked width"
date: "2012-06-29 07:28:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/paletteset-minimum-docked-width.html "
typepad_basename: "paletteset-minimum-docked-width"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have a PaletteSet that contains a toolbar and its width is set to 40 pixels. This works fine in AutoCAD 2008, but in AutoCAD 2011 I cannot make the width smaller than 150 pixels when the PaletteSet is docked.</p>
<p><strong>Solution</strong></p>
<p>In AutoCAD 2009 the minimum docked width of dockable windows including PaletteSets has been changed from 40 to 150 pixels and it is so in AutoCAD 2011 as well.</p>
<p>In AutoCAD 2012 however a new ARX function is being introduced which can override this value:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> AdUiSetDockBarMinWidth(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> width)</span></p>
</div>
<p>You can also use it from .NET via P/Invoke:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; PaletteSet ps;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// The function signature passed in for EntryPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">// is different in case of AutoCAD x64</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [DllImport(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;adui18.dll&quot;</span><span style="line-height: 140%;">, CallingConvention = CallingConvention.Cdecl, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;?AdUiSetDockBarMinWidth@@YA_NH@Z&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> AdUiSetDockBarMinWidth(</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> width);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; [CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;MyPalette&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> MyPalette()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; AdUiSetDockBarMinWidth(40);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ps == </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ps = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> PaletteSet(</span><span style="color: #a31515; line-height: 140%;">&quot;My Palette 1&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ps.MinimumSize = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> System.Drawing.Size(30, 300);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ps.Dock = DockSides.Top;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MyControl1 myCtrl = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> MyControl1();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; myCtrl.Dock = System.Windows.Forms.DockStyle.Fill;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; ps.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;test&quot;</span><span style="line-height: 140%;">, myCtrl);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ps.Visible = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
