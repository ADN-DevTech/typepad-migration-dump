---
layout: "post"
title: "TaskDialog with progress bar"
date: "2013-01-05 10:16:37"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/taskdialog-with-progress-bar.html "
typepad_basename: "taskdialog-with-progress-bar"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I found the ShowProgressBar property of TaskDialog, but it&#39;s not clear how I could make use of it. Any samples?</p>
<p><strong>Solution</strong></p>
<p>You need to assign a callback function to the TaskDialog that can be called continously so you can do parts of the lengthy process you want to show a progress bar for. Here is a sample that shows how you can do it.</p>
<p>As you can see it from the comments below there are issues with this on 64 bit OS. My colleague Dianne Phillips looked into it and it turned out to be some issue with assigning your own data to the TaskDialog like so:</p>
<p>td.CallbackData =&#0160;new&#0160;MyData();</p>
<p>Her modified code circumvents the issue in two ways: either by using a global variable or assigning your data as an IntPtr. Both ways seem to work fine on 64 bit OS as well.</p>
<span style="line-height: 120%;">
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> System;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.Windows;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.Runtime;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.Windows;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> Autodesk.AutoCAD.Geometry;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> <span style="color: #33a2bd;">acApp</span> = Autodesk.AutoCAD.ApplicationServices.<span style="color: #33a2bd;">Application</span>;</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> System.Threading;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> System.Globalization;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> System.Runtime.InteropServices;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">using</span> WinForms = System.Windows.Forms;&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">[<span style="color: #0433ff;">assembly</span>: <span style="color: #33a2bd;">CommandClass</span>(<span style="color: #0433ff;">typeof</span>(TaskDialogTest.<span style="color: #33a2bd;">Commands</span>))]</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">[</span><span style="color: #0433ff;">assembly</span><span style="color: #000000;">: </span>ExtensionApplication<span style="color: #000000;">(</span><span style="color: #0433ff;">typeof</span><span style="color: #000000;">(TaskDialogTest.</span>Module<span style="color: #000000;">))]</span></span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">namespace</span> TaskDialogTest</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">{</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; </span>// to make the AutoCAD managed runtime happy</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; </span><span style="color: #0433ff;">public</span><span style="color: #000000;"> </span><span style="color: #0433ff;">class</span><span style="color: #000000;"> </span>Module<span style="color: #000000;"> : </span>IExtensionApplication</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> Initialize()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> Terminate()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; </span>public<span style="color: #000000;"> </span>class<span style="color: #000000;"> </span><span style="color: #33a2bd;">Commands</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span><span style="color: #0433ff;">public</span><span style="color: #000000;"> </span><span style="color: #0433ff;">class</span><span style="color: #000000;"> </span>MyVarOverride<span style="color: #000000;"> : </span>IDisposable</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">object</span> oldValue;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">string</span> varName;</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> MyVarOverride(<span style="color: #0433ff;">string</span> name, <span style="color: #0433ff;">object</span> value)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; varName = name;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; oldValue = <span style="color: #33a2bd;">acApp</span>.GetSystemVariable(name);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">acApp</span>.SetSystemVariable(name, value);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> Dispose()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">acApp</span>.SetSystemVariable(varName, oldValue);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>public<span style="color: #000000;"> </span>class<span style="color: #000000;"> </span><span style="color: #33a2bd;">MyData</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">int</span> counter = 0;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">bool</span> delay = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// since the callback data can be reused, be sure</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// to reset it before invoking the task dialog</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> Reset()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; counter = 0;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; delay = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">&#0160; &#0160; #region</span> HELPER METHODS</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// helper method for processing the callback both in the&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// data-member case and the callback argument case</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">private</span> <span style="color: #0433ff;">bool</span> handleCallback(<span style="color: #33a2bd;">ActiveTaskDialog</span> taskDialog,</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">TaskDialogCallbackArgs</span> args,</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">MyData</span> callbackData)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// This gets called continuously until we finished completely</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (args.Notification == <span style="color: #33a2bd;">TaskDialogNotification</span>.Timer)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// To make it longer we do some delay in every second call</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (callbackData.delay)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; System.Threading.<span style="color: #33a2bd;">Thread</span>.Sleep(1000);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="color: #0433ff; font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; else</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; callbackData.counter += 10;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; taskDialog.SetProgressBarRange(0, 100);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; taskDialog.SetProgressBarPosition(</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; callbackData.counter);</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; </span>// This is the main action - adding 100 lines 1 by 1</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Database</span> db = <span style="color: #33a2bd;">HostApplicationServices</span>.WorkingDatabase;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Transaction</span> tr = db.TransactionManager.TopTransaction;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">BlockTable</span> bt = (<span style="color: #33a2bd;">BlockTable</span>)tr.GetObject(</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; db.BlockTableId, <span style="color: #33a2bd;">OpenMode</span>.ForRead);</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; </span>BlockTableRecord<span style="color: #000000;"> ms = (</span>BlockTableRecord<span style="color: #000000;">)tr.GetObject(</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bt[<span style="color: #33a2bd;">BlockTableRecord</span>.ModelSpace], <span style="color: #33a2bd;">OpenMode</span>.ForWrite);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Line</span> ln = <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">Line</span>(</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">Point3d</span>(0, callbackData.counter, 0),&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">Point3d</span>(10, callbackData.counter, 0));</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; ms.AppendEntity(ln);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.AddNewlyCreatedDBObject(ln, <span style="color: #0433ff;">true</span>);</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; </span>// To make it appear on the screen - might be a bit costly</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.TransactionManager.QueueForGraphicsFlush();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">acApp</span>.DocumentManager.MdiActiveDocument.Editor.Regen();</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; </span>// We are finished</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (callbackData.counter &gt;= 100)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span>// We only have a cancel button,&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span>// so this is what we can press</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; taskDialog.ClickButton(</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; (<span style="color: #0433ff;">int</span>)WinForms.<span style="color: #33a2bd;">DialogResult</span>.Cancel);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">return</span> <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; callbackData.delay = !callbackData.delay;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">else</span> <span style="color: #0433ff;">if</span> (</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; args.Notification == <span style="color: #33a2bd;">TaskDialogNotification</span>.ButtonClicked)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// we only have a cancel button</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (args.ButtonId == (<span style="color: #0433ff;">int</span>)WinForms.<span style="color: #33a2bd;">DialogResult</span>.Cancel)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">return</span> <span style="color: #0433ff;">false</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>return<span style="color: #000000;"> </span>true<span style="color: #000000;">;</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">private</span> <span style="color: #33a2bd;">TaskDialog</span> CreateTaskDialog()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>TaskDialog<span style="color: #000000;"> td = </span><span style="color: #0433ff;">new</span><span style="color: #000000;"> </span>TaskDialog<span style="color: #000000;">();</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.WindowTitle = <span style="color: #b4261a;">&quot;Adding lines&quot;</span>;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; td.ContentText = </span>&quot;This operation adds 10 lines one at a &quot;<span style="color: #000000;"> +</span></span></p>
<p style="margin: 0px; font-size: 8pt; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span>&quot;time and might take a bit of time.&quot;<span style="color: #000000;">;</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.EnableHyperlinks = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; td.ExpandedText = </span>&quot;This operation might be lengthy.&quot;<span style="color: #000000;">;</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.ExpandFooterArea = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.AllowDialogCancellation = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.ShowProgressBar = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.CallbackTimer = <span style="color: #0433ff;">true</span>;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; td.CommonButtons = <span style="color: #33a2bd;">TaskDialogCommonButtons</span>.Cancel;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">return</span> td;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; #endregion</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">&#0160; &#0160; #region</span> TASK DIALOG USING CALLBACK DATA ARGUMENT</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>/////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// This sample uses a local instance of the callback data.&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// Since the TaskDialog class needs to convert the callback data</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// to an IntPtr to pass it across the managed-unmanaged divide,</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// be sure to convert it to an IntPtr before passing it off</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// to the TaskDialog instance.&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="color: #008f00; font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; //</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// This case requires more code than the member-based sample&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// below, but is useful when a callback is shared&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// between multiple task dialogs.</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>/////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// task dialog callback that uses the mpCallbackData argument</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">bool</span> TaskDialogCallback(<span style="color: #33a2bd;">ActiveTaskDialog</span> taskDialog,</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">TaskDialogCallbackArgs</span> args,</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">object</span> mpCallbackData)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// convert the callback data from an IntPtr to the actual</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// object using GCHandle</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #33a2bd;">GCHandle</span> callbackDataHandle =&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">GCHandle</span>.FromIntPtr((<span style="color: #33a2bd;">IntPtr</span>)mpCallbackData);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #33a2bd;">MyData</span> callbackData = (<span style="color: #33a2bd;">MyData</span>)callbackDataHandle.Target;</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// use the helper method to do the actual processing</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">return</span> handleCallback(taskDialog, args, callbackData);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #33a2bd;">CommandMethod</span><span style="color: #000000;">(</span>&quot;ShowTaskDialog&quot;<span style="color: #000000;">)]</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> ShowTaskDialog()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>Database<span style="color: #000000;"> db = </span>HostApplicationServices<span style="color: #000000;">.WorkingDatabase;</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">using</span> (</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Transaction</span> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// create the task dialog and initialize the callback method</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">TaskDialog</span> td = CreateTaskDialog();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; td.Callback = <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">TaskDialogCallback</span>(TaskDialogCallback);</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// create the callback data and convert it to an IntPtr</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// using GCHandle</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">MyData</span> cbData = <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">MyData</span>();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">GCHandle</span> cbDataHandle = <span style="color: #33a2bd;">GCHandle</span>.Alloc(cbData);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; td.CallbackData = <span style="color: #33a2bd;">GCHandle</span>.ToIntPtr(cbDataHandle);</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// Just to minimize the &quot;Regenerating model&quot; messages</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">using</span> (<span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">MyVarOverride</span>(<span style="color: #b4261a;">&quot;NOMUTT&quot;</span>, 1))</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; td.Show(<span style="color: #33a2bd;">Application</span>.MainWindow.Handle);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// If the dialog was not cancelled before it finished</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// adding the lines then commit transaction</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (memberCallbackData.counter &gt;= 100)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// be sure to clean up the gc handle before returning</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; cbDataHandle.Free();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; #endregion</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #0433ff;">&#0160; &#0160; #region</span> TASK DIALOG USING DATA-MEMBER-BASED CALLBACK DATA</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>/////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// This sample uses a data member for the callback data.&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// This avoids having to pass the callback data as an IntPtr.</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>/////////////////////////////////////////////////////////////////</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// member-based callback data -&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// used with MemberTaskDialogCallback</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #33a2bd;">MyData</span> memberCallbackData = <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">MyData</span>();</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// task dialog callback that uses the callback data member;&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; </span>// does not use mpCallbackData</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">bool</span> TaskDialogCallbackUsingMemberData(</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #33a2bd;">ActiveTaskDialog</span> taskDialog,</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>TaskDialogCallbackArgs<span style="color: #000000;"> args,</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">object</span> mpCallbackData)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>// use the helper method to do the actual processing</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">return</span> handleCallback(taskDialog, args, memberCallbackData);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #b4261a;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; [</span><span style="color: #33a2bd;">CommandMethod</span><span style="color: #000000;">(</span>&quot;ShowTaskDialogWithDataMember&quot;<span style="color: #000000;">)]</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; <span style="color: #0433ff;">public</span> <span style="color: #0433ff;">void</span> ShowTaskDialogWithDataMember()</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #33a2bd;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; </span>Database<span style="color: #000000;"> db = </span>HostApplicationServices<span style="color: #000000;">.WorkingDatabase;</span></span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; <span style="color: #0433ff;">using</span> (</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">Transaction</span> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// create the task dialog and initialize the callback method</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #33a2bd;">TaskDialog</span> td = CreateTaskDialog();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; td.Callback =&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">TaskDialogCallback</span>(TaskDialogCallbackUsingMemberData);</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// make sure the callback data is initialized before&#0160;</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// invoking the task dialog</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; memberCallbackData.Reset();</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// Just to minimize the &quot;Regenerating model&quot; messages</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">using</span> (<span style="color: #0433ff;">new</span> <span style="color: #33a2bd;">MyVarOverride</span>(<span style="color: #b4261a;">&quot;NOMUTT&quot;</span>, 1))</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; td.Show(<span style="color: #33a2bd;">Application</span>.MainWindow.Handle);</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// If the dialog was not cancelled before it finished</span></p>
<p style="margin: 0px; font-size: 8pt; color: #008f00;"><span style="font-family: &#39;courier new&#39;, courier;"><span style="color: #000000;">&#0160; &#0160; &#0160; &#0160; </span>// adding the lines then commit transaction</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; <span style="color: #0433ff;">if</span> (memberCallbackData.counter &gt;= 100)</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; tr.Commit();</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt; min-height: 11px;">&#0160;</p>
<p style="margin: 0px; font-size: 8pt; color: #0433ff;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; #endregion</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; }</span></p>
<p style="margin: 0px; font-size: 8pt;"><span style="font-family: &#39;courier new&#39;, courier;">}</span></p>
</span>
