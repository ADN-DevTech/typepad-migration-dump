---
layout: "post"
title: ".NET DllImport a method defined in C++"
date: "2012-06-26 13:15:05"
author: "Augusto Goncalves"
categories:
  - ".NET"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/net-dllimport-a-method-defined-in-c.html "
typepad_basename: "net-dllimport-a-method-defined-in-c"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html" target="_blank">Augusto Goncalves</a></p>
<p>Suppose there is a <em>void MyFunc()</em> C++ function you need to call from .NET. The DllImport call will only recognize it if the function is declared with <em><a href="http://msdn.microsoft.com/en-us/library/a90k134d.aspx" target="_blank">dllexport</a> </em>modifier.</p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">extern&#0160;</span></span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;C&quot;&#0160;</span></span><span><span style="color: #0000ff;">__declspec</span></span><span style="color: #000000;">( </span><span><span style="color: #0000ff;">dllexport</span></span><span style="color: #000000;"> ) </span><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;"> MyFunc()</span></span></span></p>
</div>
<p>It is also possible pass a .NET AutoCAD Entity to C++ unmanaged, just declare the function with a pointer and use <strong>UnmanagedObject</strong> property from .NET.</p>
<p><strong>C++</strong></p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">extern&#0160;</span></span></span><span style="font-size: 8pt;"><span><span style="color: #a31515;">&quot;C&quot;&#0160;</span></span><span><span style="color: #0000ff;">__declspec</span></span><span style="color: #000000;">( </span><span><span style="color: #0000ff;">dllexport</span></span><span style="color: #000000;"> ) </span><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;">&#0160;MyFunc(AcDbLine* line)</span></span></span></p>
</div>
<p><strong>.NET</strong></p>
<div style="background: white;">
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="font-size: 8pt;">[</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">DllImport</span></span><span style="color: #000000;">(</span><span><span style="color: #a31515;">&quot;MyArxModule.arx&quot;</span></span><span style="color: #000000;">,</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; CallingConvention = </span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">CallingConvention</span></span><span style="color: #000000;">.Cdecl,</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; CharSet = </span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">CharSet</span></span><span style="color: #000000;">.Unicode)]</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">private&#0160;</span></span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">static&#0160;</span></span><span><span style="color: #0000ff;">extern&#0160;</span></span><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;"> MyFunc(System.</span><span><span style="color: #2b91af;">IntPtr</span></span><span style="color: #000000;"> line);</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160;</span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="font-size: 8pt;">[</span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">CommandMethod</span></span><span style="color: #000000;">(</span><span><span style="color: #a31515;">&quot;myCommand&quot;</span></span><span style="color: #000000;">)]</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span><span style="color: #0000ff;"><span style="font-size: 8pt;">static&#0160;</span></span></span><span style="font-size: 8pt;"><span><span style="color: #0000ff;">public&#0160;</span></span><span><span style="color: #0000ff;">void</span></span><span style="color: #000000;"> CmdCallCommand()</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">{</span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="color: #000000;"><span style="font-size: 8pt;">&#0160; </span></span><span style="font-size: 8pt;"><span><span style="color: #2b91af;">Line</span></span><span style="color: #000000;"> l = </span></span><span><span style="font-size: 8pt; color: #008000;">// do something here...</span></span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">&#0160; MyFunc(l.UnmanagedObject);</span></span></p>
<p style="margin: 0px;"><span style="font-family: &#39;Courier New&#39;;"><span style="font-size: 8pt; color: #000000;">}</span></span></p>
</div>
