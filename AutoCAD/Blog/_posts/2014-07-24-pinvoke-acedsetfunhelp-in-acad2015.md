---
layout: "post"
title: "Pinvoke acedSetFunHelp in ACAD2015"
date: "2014-07-24 06:52:44"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2014/07/pinvoke-acedsetfunhelp-in-acad2015.html "
typepad_basename: "pinvoke-acedsetfunhelp-in-acad2015"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>&#0160;&#0160;&#0160;&#0160;We have been recieving few queries from our ADN partners regarding setting help message display on F1 click on custom commands that</p>
<p>&#0160;&#0160;&#0160; <em>The call to the function acedSetFunHelp always passes the value 0 (zero) as the cmd parameter.</em></p>
<p><em>&#0160;&#0160;&#0160;&#0160; Has the call to the acedSetFunHelp function changed in the 2015 release and is it different between the 32bit and 64bit versions?</em></p>
<p>Yes, there has been change in the entry points of the function acedSetFunHelp and is different between 32/64 bit machines.</p>
<p>I have already answered this in forum post <a href="http://forums.autodesk.com/t5/NET/Unable-to-display-help-in-AutoCAD-2015-32bit/m-p/5090978#M40992" target="_self" title="HelpIssue">acedSetFunHelp</a>&#0160;, as to reach larger audience I planned to blog this with minor additions ,thanks to Alexander Rivilis for&#0160;bringing up this issue ,used his part of code.</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 10pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">#define</span> AUTOCAD_NEWER_THAN_2012</p>
<p style="margin: 0px;"><span style="color: blue;">#define</span> AUTOCAD_NEWER_THAN_2014</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Reflection;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Diagnostics;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.IO;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Runtime.InteropServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> cad = Autodesk.AutoCAD.ApplicationServices.Application;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Ap = Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Db = Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Ed = Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Rt = Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;">[<span style="color: blue;">assembly</span>: Rt.CommandClass(<span style="color: blue;">typeof</span>(Help.Commands))]</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> Help</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Help File</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">sealed</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; #region</span> PInvoke</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#if</span> AUTOCAD_NEWER_THAN_2012</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> fnc_location = <span style="color: #a31515;">&quot;accore.dll&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#else</span></p>
<p style="margin: 0px;"><span style="color: gray;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;const String fnc_location = &quot;acad.exe&quot;;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#endif</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#if</span> AUTOCAD_NEWER_THAN_2014</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> x86_Prefix = <span style="color: #a31515;">&quot;_&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#else</span></p>
<p style="margin: 0px;"><span style="color: gray;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;const String x86_Prefix = &quot;&quot;;</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;#endif</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; #region</span> acedSetFunHelp</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> acedSetFunHelp_Name = <span style="color: #a31515;">&quot;acedSetFunHelp&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; [<span style="color: #2b91af;">DllImport</span>(fnc_location, CharSet = <span style="color: #2b91af;">CharSet</span>.Auto,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CallingConvention = <span style="color: #2b91af;">CallingConvention</span>.Cdecl,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; EntryPoint = <span style="color: #a31515;">&quot;acedSetFunHelp&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">extern</span> <span style="color: #2b91af;">Int32</span> acedSetFunHelp_x64(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">String</span> functionName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">String</span> helpFile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">String</span> helpTopic,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">Int32</span> cmd);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; [<span style="color: #2b91af;">DllImport</span>(fnc_location, CharSet = <span style="color: #2b91af;">CharSet</span>.Auto,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; CallingConvention = <span style="color: #2b91af;">CallingConvention</span>.Cdecl,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; EntryPoint = x86_Prefix + <span style="color: #a31515;">&quot;acedSetFunHelp&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">extern</span> <span style="color: #2b91af;">Int32</span> acedSetFunHelp_x86(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">String</span> functionName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;String</span> helpFile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">String</span> helpTopic,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Int32</span> cmd);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: #2b91af;">Int32</span> acedSetFunHelp(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">String</span> functionName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">String</span> helpFile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">String</span> helpTopic,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #2b91af;">Int32</span> cmd)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: #2b91af;">IntPtr</span>.Size == 4)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">return</span> acedSetFunHelp_x86(functionName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;helpFile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;helpTopic,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;cmd);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">return</span> acedSetFunHelp_x64(functionName,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;helpFile,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;helpTopic,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;cmd);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; #endregion</span> <span style="color: green;">// acedSetFunHelp</span></p>
<p style="margin: 0px;"><span style="color: blue;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; #endregion</span> <span style="color: green;">// PInvoke</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> commandGroup = <span style="color: #a31515;">&quot;MyCommand&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">internal</span> <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> throughAcedSetFunHelp = &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #a31515;">&quot;ThroughAcedSetFunHelp&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> helpPageExtension = <span style="color: #a31515;">&quot;.htm&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">/*Place the Chm file where .NET dll is resourced*/</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> chmFileName = <span style="color: #a31515;">&quot;MyHelp.chm&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">readonly</span> <span style="color: #2b91af;">String</span> asm_location =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">Path</span>.GetDirectoryName(<span style="color: #2b91af;">Assembly</span>.GetExecutingAssembly()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;.Location);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: green;">// chm-file is in the directory of dll-file.</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">readonly</span> <span style="color: #2b91af;">String</span> chmFileFullName =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #2b91af;">Path</span>.Combine(asm_location, chmFileName);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">const</span> <span style="color: #2b91af;">String</span> f1_msg =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;\nPress F1 for opening of &quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;help system.\n&quot;</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: green;"> Registaration with calling acedSetFunHelp</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: gray;">///</span><span style="color: gray;">&lt;/summary&gt;</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; [Rt.CommandMethod(commandGroup,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;throughAcedSetFunHelp,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;Rt.CommandFlags.Session</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ThroughAcedSetFunHelp_Command()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; Ap.Document doc =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;cad.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">if</span> (<span style="color: blue;">null</span> == doc)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; <span style="color: blue;">string</span> CmdName = <span style="color: #a31515;">&quot;c:&quot;</span> +</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; throughAcedSetFunHelp.ToUpper();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; acedSetFunHelp(CmdName, chmFileFullName, <span style="color: #a31515;">&quot;&quot;</span>, 0);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; doc.Editor.GetPoint(f1_msg);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
