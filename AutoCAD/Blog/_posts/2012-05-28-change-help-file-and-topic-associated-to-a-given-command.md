---
layout: "post"
title: "Change help file and topic associated to a given command"
date: "2012-05-28 05:20:16"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/change-help-file-and-topic-associated-to-a-given-command.html "
typepad_basename: "change-help-file-and-topic-associated-to-a-given-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When the user hovers over a ribbon button then the tooltip that pops up says &quot;Press F1 for more help&quot;. I would like to change the help file and topic that is shown when the user presses F1 while the tooltip is displayed.</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>You can use acedSetFunHelp() from ARX or P/Invoke it from .NET:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: green;">// In case of AutoCAD 2013 and above <br />// replace &quot;acad.exe&quot; with &quot;accore.dll&quot;<br /></span>
<span style="line-height: 140%;">[DllImport(</span><span style="line-height: 140%; color: #a31515;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">, CharSet = CharSet.Auto, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; CallingConvention = CallingConvention.Cdecl, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; EntryPoint = </span><span style="line-height: 140%; color: #a31515;">&quot;acedSetFunHelp&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> acedSetFunHelp(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> functionName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> helpFile, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">string</span><span style="line-height: 140%;"> helpTopic, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> cmd);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[CommandMethod(</span><span style="line-height: 140%; color: #a31515;">&quot;AEN1Cmd1&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Cmd1()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedSetFunHelp(</span><span style="line-height: 140%; color: #a31515;">&quot;C:LINE&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;acad_acg.chm&quot;</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: #a31515;">&quot;ACG.01.008&quot;</span><span style="line-height: 140%;">, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>This only has effect for the current AutoCAD session and is not persisted in the CUIx file.</p>
<p>This and other ToolTip related settings cannot be changed at the moment using the CUI API for AutoCAD.Customization.Macro.ToolTip is internal.</p>
