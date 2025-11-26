---
layout: "post"
title: "Command Window does not get updated when using WriteMessage() from a long command"
date: "2012-05-28 07:51:43"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/command-window-does-not-get-updated-when-using-writemessage-from-a-long-command.html "
typepad_basename: "command-window-does-not-get-updated-when-using-writemessage-from-a-long-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m using WriteMessage() in a longer command and would like to keep the user updated by writing messages to the command window. Unfortunately, when I use WriteMessage(), its content only appears once my command finished. How could I update/refresh the Command Window to show the text I wrote? </p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>If you add a carriage return (C# \n, VB.NET vbCr/vbCrLf) to the end of your text then it will appear straight away. As mentioned in the comments section, VB.NET vbCr might not work as well as vbCrLf does.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="line-height: 140%; color: #a31515;">&quot;AEN1WriteInfo&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> AEN1WriteInfo()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160; DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="line-height: 140%; color: #a31515;">&quot;Something\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> i = 0; i &lt; 3; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; System.Threading.Thread.Sleep(1000);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// carriage return at the end</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ed.WriteMessage(i.ToString() + </span><span style="line-height: 140%; color: #a31515;">&quot;\n&quot;</span><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
