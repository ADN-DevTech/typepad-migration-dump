---
layout: "post"
title: "Removing Surface Breaklines using AutoCAD Civil 3D API"
date: "2013-09-25 02:11:29"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/09/removing-surface-breaklines-using-autocad-civil-3d-com-api.html "
typepad_basename: "removing-surface-breaklines-using-autocad-civil-3d-com-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>AutoCAD Civil
3D COM API has <em><strong>IAeccSurfaceBreaklines:: Remove</strong></em> method to remove Surface
Breaklines. In the ‘ActiveX API Reference’ document we see this &#0160;– </p>
<p><strong>IAeccSurfaceBreaklines::
Remove</strong> Method - &gt; Removes an item from the collection, specified by <em><strong>index</strong></em> or
<em>object reference</em>. </p>
<p>HRESULT
Remove( [in] VARIANT varIndex);</p>
<p>One of our
Civil 3D application developers friend recently asked me ‘how do we use <em>object
reference</em> in Remove()’ ? That prompted me to dig deeper in this and I find we
can only use <strong>index</strong> in the <strong>Remove()</strong> and it <span style="text-decoration: underline;">doesn’t</span> work with <em>object reference</em>. By
the time we update the Civil 3D ActiveX API reference document on this, I
thought it would be useful to you all if I share this information here in IM
DevBlog. </p>
<p>Here is the
VB.NET code snippet showing usage of COM API Remove(index) –</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;">(oTinSurface.Breaklines.Count &gt; 0)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: green; line-height: 140%;">&#39; Remove() Breakline using COM API</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">&#0160; oTinSurface.Breaklines.Remove(0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; oTinSurface.Rebuild()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(vbCrLf + </span><span style="color: #a31515; line-height: 140%;">&quot;Breakline Removed ! &quot;</span><span style="line-height: 140%;"> )</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> </span></p>
</div>
<p>&#0160;</p>
<p>In Civil 3D .NET API we have the equivalent <strong>RemoveAt(int)</strong>
and here is C# .NET code snippet –</p>
<p><span style="color: green; line-height: 140%; font-family: &#39;Courier New&#39;; font-size: 8pt;">// Get the TIN Surface &#0160; &#0160; &#0160;&#0160;</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;"> tinSurface = surfaceId.GetObject(</span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TinSurface</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Lets try to Remove Breakline using RemoveAt(int)&#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; background-color: #ffff00;">&#0160; tinSurface.BreaklinesDefinition.RemoveAt(0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// rebuild the surface</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; tinSurface.Rebuild();</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you!</p>
