---
layout: "post"
title: "Show LAYER dialog synchronously"
date: "2012-07-25 04:15:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/show-layer-dialog-synchronously.html "
typepad_basename: "show-layer-dialog-synchronously"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I would like to run the LAYER command from my own command. Unfortunately, it seems that the LAYER dialog only shows once my command is finised. Would it be possible to show this dialog during my command and then continue with my command once the dialog is closed?</p>
<p><strong>Solution</strong></p>
<p>First of all, for a few releases now the layer dialog has been modeless. However, it has a setting that enables the old behaviour: LAYERDLGMODE</p>
<p>Also, to start another command synchronously, you would need to P/Invoke acedCmd() from your .NET project.</p>
<p>Since you try to run the LAYER command from inside your command, it starts in command line mode. If you want to force it to show the dialog, then you also need to P/Invoke acedInitDialog(). This has mangled name, which also means that it may be different depending on the OS type (32 bit vs. 64 bit) and the version of AutoCAD, so make sure that you are using the correct mangled name of the function.</p>
<p>You can use dumpbin.exe or depends.exe to get this information from acad.exe. You can find further information about these utilities on the internet.</p>
<p>Here is the .NET project that does what you need in case of AutoCAD 2011 on 32 bit OS:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;"> = Autodesk.AutoCAD.ApplicationServices.</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">namespace</span><span style="line-height: 140%;"> StartCommand</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CallingConvention = </span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CharSet = </span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;acedCmd&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> acedCmd(System.</span><span style="color: #2b91af; line-height: 140%;">IntPtr</span><span style="line-height: 140%;"> resBuf);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Mangled entry point may be different in case of </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: green; line-height: 140%;">// different OS (32 bit vs 64 bit) and different AutoCAD versions</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">DllImport</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;acad.exe&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CallingConvention = </span><span style="color: #2b91af; line-height: 140%;">CallingConvention</span><span style="line-height: 140%;">.Cdecl,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; CharSet = </span><span style="color: #2b91af; line-height: 140%;">CharSet</span><span style="line-height: 140%;">.Unicode,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; EntryPoint = </span><span style="color: #a31515; line-height: 140%;">&quot;?acedInitDialog@@YAHH@Z&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">extern</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> acedInitDialog(</span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> useDialog);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; [</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;StartCommand1&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> StartCommand1()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Do something</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// ...</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nCommand Started ...\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Now show the LAYER dialog</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// First make sure that we use the modal version </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">short</span><span style="line-height: 140%;"> dlgMode = (</span><span style="color: blue; line-height: 140%;">short</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;LAYERDLGMODE&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;LAYERDLGMODE&quot;</span><span style="line-height: 140%;">, 0);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Execute the LAYER command</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">ResultBuffer</span><span style="line-height: 140%;"> rb = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ResultBuffer</span><span style="line-height: 140%;">())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; rb.Add(</span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">TypedValue</span><span style="line-height: 140%;">((</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">LispDataType</span><span style="line-height: 140%;">.Text, </span><span style="color: #a31515; line-height: 140%;">&quot;_LAYER&quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Make sure that not the command line version gets started</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; acedInitDialog(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; acedCmd(rb.UnmanagedObject);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Restore the value</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;LAYERDLGMODE&quot;</span><span style="line-height: 140%;">, dlgMode);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: green; line-height: 140%;">// Continue with our command</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">acApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor.WriteMessage(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;\nCommand Continued ...\n&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
