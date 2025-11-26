---
layout: "post"
title: "PInvokeStackImbalance error"
date: "2012-07-27 05:23:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/pinvokestackimbalance-error.html "
typepad_basename: "pinvokestackimbalance-error"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m trying to P/Invoke acedPostCommand() so that I could use it to cancel the current command, but I get a PInvokeStackImbalance error when calling acedPostCommand inside my .NET code:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743717568970d-pi" style="display: inline;"><img alt="Pinvokestack" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017743717568970d" src="/assets/image_233808.jpg" title="Pinvokestack" /></a></p>
<p>How could I solve this?</p>
<p>Here is the P/Invoke I&#39;m using:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CharSet = </span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;?acedPostCommand@@YAHPB_W@Z&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> acedPostCommand(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> cmd);</span></p>
</div>
<p><strong>Solution</strong></p>
<p>You also need to specify the calling convention. Once that&#39;s done acedPostCommand() works fine:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CharSet = </span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; CallingConvention = </span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;?acedPostCommand@@YAHPB_W@Z&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> acedPostCommand(</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> cmd);</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
