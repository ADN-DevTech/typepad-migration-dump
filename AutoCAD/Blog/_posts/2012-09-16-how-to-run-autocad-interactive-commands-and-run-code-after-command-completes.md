---
layout: "post"
title: "How to run AutoCAD interactive commands and run code after command completes"
date: "2012-09-16 11:44:47"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/how-to-run-autocad-interactive-commands-and-run-code-after-command-completes.html "
typepad_basename: "how-to-run-autocad-interactive-commands-and-run-code-after-command-completes"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>I need to run an AutoCAD command from my .NET command and allow the user be able to interactively input data. After I call this command I need to take further action. I am using ActiveX and SendCommand. My code runs before the AutoCAD command is completed by the user. Is there a way to cause my code to wait until the command is finished?</p>
<!--stopindex-->
<div><a name="section2"> </a><!--startindex-->
<div><strong>Solution</strong></div>
<p>An approach to consider is to place the .NET code that needs to run after the AutoCAD command finishes in a separate .NET command. This command can be called after the AutoCAD command is complete. Here is a VB.NET example that shows this idea. It runs the LINE command and when the command is finished it runs a command named Test2:</p>
</div>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Interop</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Interop.Common</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&lt;Autodesk.AutoCAD.Runtime.CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;Test1&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Shared</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> wbtest1()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oApp </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> AcadApplication = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> cmdStr </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'this will allow for an open ended line command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">' when the line command is finished the .NET command wbtest2 will run</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; cmdStr = </span><span style="color: #a31515; line-height: 140%;">&quot;(command &quot;&quot;Line&quot;&quot;)(while(&gt;(getvar &quot;&quot;cmdactive&quot;&quot;)0)(command pause))(command &quot;&quot;test2&quot;&quot;) &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; oApp.ActiveDocument.SendCommand(cmdStr)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">' this is called before the LINE command is completed</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;From Test1&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&lt;Autodesk.AutoCAD.Runtime.CommandMethod(</span><span style="color: #a31515; line-height: 140%;">&quot;Test2&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Shared</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> wbtest2()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">' this is called after LINE command completes</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;From Test2&quot;</span><span style="line-height: 140%;">, MsgBoxStyle.Information)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
