---
layout: "post"
title: "How to Update the Status of External Process to AutoCAD CommandLine"
date: "2015-08-28 03:20:37"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "2016"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/08/how-to-update-the-status-of-external-process-to-autocad-commandline.html "
typepad_basename: "how-to-update-the-status-of-external-process-to-autocad-commandline"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Let us assume you have a .Net command that runs an external process that takes a long time to complete. While you are waiting for it to complete you can output a progress indicator to the command line :</p>
<p>Note: Following procedure works only .NET 4.5, as I’m incorporating await and aysnc methods.</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;TestCommand&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> TestCommand()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">Method1();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">async</span> <span style="color: blue;">void</span> Method1()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">try</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;"><span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;"><span style="color: blue;">for</span> (<span style="color: blue;">int</span> i = 0; i &lt; 20; i++)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">ed.WriteMessage(<span style="color: #a31515;">&quot;Processing {0}...&quot;</span>, i);</p>
<p style="margin: 0px;"><span style="color: blue;">string</span> result = <span style="color: blue;">await</span> WaitASynchronously();</p>
<p style="margin: 0px;">ed.WriteMessage(<span style="color: #a31515;">&quot;Done.\n&quot;</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span> (System.<span style="color: #2b91af;">Exception</span> ex)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Application</span>.ShowAlertDialog(<span style="color: #a31515;">&quot;Error: &quot;</span> + ex.Message);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">async</span> <span style="color: #2b91af;">Task</span>&lt;<span style="color: blue;">string</span>&gt; WaitASynchronously()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">await</span> <span style="color: #2b91af;">Task</span>.Delay(500);</p>
<p style="margin: 0px;"><span style="color: blue;">return</span> <span style="color: #a31515;">&quot;Finished&quot;</span>;</p>
<p style="margin: 0px;">}</p>
</div>
