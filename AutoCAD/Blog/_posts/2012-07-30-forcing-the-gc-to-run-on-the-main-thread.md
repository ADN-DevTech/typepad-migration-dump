---
layout: "post"
title: "Forcing the GC to run on the Main Thread"
date: "2012-07-30 16:29:46"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/forcing-the-gc-to-run-on-the-main-thread.html "
typepad_basename: "forcing-the-gc-to-run-on-the-main-thread"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Recently I posted this <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-3.html">blog entry</a> on Performance, as you can see at the bottom it drove a lot of comments.</p>
<p>One question that came up was when to call Dispose(). The thing is that all of our .NET code wraps ObjectARX, in turn, all of our ObjectARX code is single threaded and the .NET Garbage Collector runs on a background thread... What this means is, if you don&#39;t call Dispose() on our AutoCAD .NET objects (the ones you create in your code) you run the high risk of the GC garbage collecting our objects on a background thread, which in turn may cause AutoCAD to crash.</p>
<p>That said, a crash happening really does depend on what the AutoCAD implementation of the Dispose code does under the hood. If the Dispose() simply does a memory free for which any destructors do nothing, then you’re probably going to be fine. However, if the dispose does other things like fire events, update UI, etc etc, then you are most likely looking at a crash. Therefore, I recommend you Dispose() all AutoCAD .NET objects (the ones you create in your code) using the .NET &#39;using&#39; statement – that way you are not relying on the GC to clean up after yourself.</p>
<p>Now, say you are at a customer site where they are experiencing random crashes with your app, or you are running behind schedule and just can’t seem to find the place where the random crash is happening then here’s a quick fix for you – try forcing the GC to run on the Main Thread…</p>
<blockquote>
<p>There is a setting that can be applied to the acad.exe.config file called <strong>gcConCurrent</strong>. See <a href="http://msdn.microsoft.com/en-us/library/yhwwzef8.aspx">http://msdn.microsoft.com/en-us/library/yhwwzef8.aspx</a></p>
<p>An example of this being implemented in the acad.exe.config file is shown thus:</p>
<div style="font-family: consolas; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">&lt;</span><span style="color: #a31515;">configuration</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;</span><span style="color: #a31515;">startup</span><span style="color: blue;"> </span><span style="color: red;">useLegacyV2RuntimeActivationPolicy</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">true</span>&quot;<span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">supportedRuntime</span><span style="color: blue;"> </span><span style="color: red;">version</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">v4.0</span>&quot;<span style="color: blue;">/&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160; &lt;/</span><span style="color: #a31515;">startup</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">&lt;!--</span><span style="color: green;">All assemblies in AutoCAD are fully trusted so there&#39;s no point generating publisher evidence</span><span style="color: blue;">--&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160; &lt;</span><span style="color: #a31515;">runtime</span><span style="color: blue;">&gt;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">generatePublisherEvidence</span><span style="color: blue;"> </span><span style="color: red;">enabled</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">false</span>&quot;<span style="color: blue;">/&gt;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &lt;</span><span style="color: #a31515;">gcConcurrent</span><span style="color: blue;"> </span><span style="color: red;">enabled</span><span style="color: blue;">=</span>&quot;<span style="color: blue;">false</span>&quot;<span style="color: blue;"> /&gt; </span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160; &lt;/</span><span style="color: #a31515;">runtime</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&lt;/</span><span style="color: #a31515;">configuration</span><span style="color: blue;">&gt;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
</blockquote>
