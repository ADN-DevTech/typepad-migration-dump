---
layout: "post"
title: "Replacing AutoCAD's OPEN command using .NET"
date: "2007-03-04 14:47:14"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Documents"
original_url: "https://www.keanw.com/2007/03/replacing_the_o.html "
typepad_basename: "replacing_the_o"
typepad_status: "Publish"
---

<p>This very good question came in by email from Patrick Nikoletich:</p><blockquote dir="ltr"><p><em>I was wondering what the preferred method for overriding the default “Open” common dialog in AutoCAD 2007 would be? I can catch the event but can’t send AutoCAD a command to cancel the request so I can launch my Win Form instead.</em></p></blockquote><p>This is quite a common requirement - especially for people integrating document management systems with AutoCAD.</p>

<p>The simplest way to accomplish this - and this technique goes back a bit - is to undefine the OPEN command, replacing it with your own implementation. The classic way to do this from LISP was to use the (command) function to call the UNDEFINE command on OPEN, and then use (defun) to implement your own (c:open) function.</p>

<p>This technique can be adapted for use with .NET. The following C# code calls the UNDEFINE command in its initialization and then implements an OPEN command of its own.</p>

<p>A few notes on the implementation:</p>

<ul><li>I'm using the COM SendCommand(), rather than SendStringToExecute(), as it is called synchronously and is executed before the command gets defined<ul><li>Unfortunately this causes the UNDEFINE command to be echoed to the command-line, an undesired side effect.</li>

<li>I have not tested this being loaded on AutoCAD Startup - it may require some work to get the initialization done appropriately, if this is a requirement (as SendCommand is called on the ActiveDocument).</li></ul></li>

<li>I've implemented the OPEN command very simply - just to request a filename from the user with a standard dialog - and then call a function to open this file. More work may be needed to tailor this command's behaviour to match AutoCAD's or to match your application requirements.<ul><li>This is defined as a session command, allowing it to transfer focus to the newly-opened document. It does not close the active document, which AutoCAD's OPEN command does if the document is &quot;default&quot; and unedited (such as &quot;Drawing1.dwg&quot;).</li></ul></li></ul>

<p>And so here's the code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Interop;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Runtime.InteropServices;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> RedefineOpen</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">CommandRedefinitions</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; : Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">AcadApplication</span> app =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">AcadApplication</span>)<span style="COLOR: teal">Application</span>.AcadApplication;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;app.ActiveDocument.SendCommand(<span style="COLOR: maroon">&quot;_.UNDEFINE _OPEN &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate(){}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;OPEN&quot;</span>, <span style="COLOR: teal">CommandFlags</span>.Session)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">static</span> <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Open()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DocumentCollection</span> dm = <span style="COLOR: teal">Application</span>.DocumentManager;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed = dm.MdiActiveDocument.Editor;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptOpenFileOptions</span> opts =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> <span style="COLOR: teal">PromptOpenFileOptions</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;Select a drawing file (for a custom command)&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;opts.Filter = <span style="COLOR: maroon">&quot;Drawing (*.dwg)&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">PromptFileNameResult</span> pr = ed.GetFileNameForOpen(opts);</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">if</span> (pr.Status == <span style="COLOR: teal">PromptStatus</span>.OK)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dm.Open(pr.StringResult);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p></div>
