---
layout: "post"
title: "Using MAPTRIM in .NET Application"
date: "2014-03-11 02:25:08"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "AutoCAD Map 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/03/using-maptrim-in-net-application.html "
typepad_basename: "using-maptrim-in-net-application"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p><strong>MAPTRIM</strong> is AutoCAD Map 3D&#39;s UI command. API equivalent for MAPTRIM is <strong>map_dwgtrimobj(). </strong>&#0160;It is part of the C/C++ and LISP APIs.&#0160;API equivalent of MAPTRIM is not yet available in <a class="zem_slink" href="http://www.microsoft.com/net" rel="homepage" target="_blank" title=".NET Framework">.NET</a>.</p>
<p>&#0160;</p>
<p>If you want to use MAPTRIM with necessary input parameters, you can build the command string and use <strong>SendCommand()</strong> or <strong>SendStringToExecute()</strong> to pump the command in AutoCAD Map 3D&#39;s command line.</p>
<p>&#0160;</p>
<p>Here is code snippet on how to use it for a selected layer and for all layers :&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Using Maptrim command through SendCommand</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; in the following example &#39;miscellaneous0&#39; is the layer name for selection </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> commandStr </span><span style="color: blue; line-height: 140%;">As</span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;.maptrim &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;s &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;l &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;miscellaneous0&quot;</span><span style="line-height: 140%;"> + vbCr +</span><span style="color: #a31515; line-height: 140%;">&quot;n &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;o &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;i &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; acApp.ActiveDocument.SendCommand(commandStr)</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; Using Maptrim command through SendCommand</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">&#39;&#39; this example is for all Layers </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> commandStr </span><span style="color: blue; line-height: 140%;">As</span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;"> = </span><span style="color: #a31515; line-height: 140%;">&quot;.maptrim &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;s &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;l &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;n &quot;</span><span style="line-height: 140%;"> +</span><span style="color: #a31515; line-height: 140%;">&quot;n &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;o &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;i &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;y &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acApp.ActiveDocument.SendCommand(commandStr)</span></p>
</div>
