---
layout: "post"
title: "Passing arguments to .NET-defined commands"
date: "2006-09-01 19:47:57"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "AutoLISP / Visual LISP"
  - "Commands"
original_url: "https://www.keanw.com/2006/09/passing_argumen.html "
typepad_basename: "passing_argumen"
typepad_status: "Publish"
---

<p>Another case of planets aligning: two different people have asked me this question in two days...</p>

<p>It's quite common for commands to require users to provide additional input during execution. This information might be passed in via a script or a (command) from LISP, or it may simply be typed manually or pasted in by the user.</p>

<p>The fact is, though, that commands don't actually take arguments. It may seem like they do, but they don't. What they do is ask for user input using dialogs or the command-line.</p>

<p>Here are a few tips on how to support passing of information into your commands...</p>

<p><strong>Define a version of your command that asks for input via the command-line</strong></p>

<p>It's very easy to define beautiful UIs in .NET applications. You absolutely should do so. But it's also helpful to provide an alternative that can be called via the command-line and from scripts. I'd suggest a couple of things here for your commands:</p>

<ul><li>Define a standard version (e.g. &quot;MYCOMMAND&quot;) and a command-line version (&quot;-MYCOMMAND&quot;). It's common to prefix command-line versions of commands in AutoCAD with a hyphen (see -PURGE vs. PURGE, for instance).</li>

<li>An alternative is to check for the values of FILEDIA and CMDDIA system variables - see the AutoCAD documentation on these commands to understand what effect they're meant to have on commands.</li></ul>

<p>When implementing a command-line version of your command, you simply use the standard user input functions (GetXXX()) available in the Autodesk.AutoCAD.EditorInput namespace. Here's some VB.NET code showing this:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TST&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed = Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> name <span style="COLOR: blue">As</span> PromptResult = ed.GetString(vbCr + <span style="COLOR: maroon">&quot;Enter name: &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;You entered: &quot;</span> + name.StringResult)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p></div>

<p>When it's run, you get this (typed text in <span style="COLOR: red">red</span>):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="COLOR: red">tst</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter name: <span style="COLOR: red">Hello</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">You entered: Hello</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command:</p></div>

<p>By the way, I tend to put a vbCr (or &quot;\n&quot; in ObjectARX) in front of prompts being used in GetXXX() functions, as it's also possible to terminate text input with a space in AutoCAD: this means that even is a space is entered to launch the command, the prompt string displays on a new line.</p>

<p><strong>Define a LISP version of your command</strong></p>

<p>The &lt;LispFunction&gt; attribute allows you to declare .NET functions as LISP-callable. A very good technique is to separate the guts of your command into a separate function that is then called both by your command (after it has asked the user for the necessary input) and by the LISP-registered function, which unpackages the arguments passed into it.</p>

<p>To understand more about how to implement LISP-callable functions in .NET, see <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/breathe_fresh_l.html">this previous post</a>.</p>
