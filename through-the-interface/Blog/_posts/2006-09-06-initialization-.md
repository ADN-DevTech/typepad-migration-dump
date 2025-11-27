---
layout: "post"
title: "Initialization code in your AutoCAD .NET application"
date: "2006-09-06 14:52:12"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Runtime"
original_url: "https://www.keanw.com/2006/09/initialization_.html "
typepad_basename: "initialization_"
typepad_status: "Publish"
---

<p>It's very common to need to execute some code as your application modules are loaded, and then to clean-up as they get unloaded or as AutoCAD terminates. Managed AutoCAD applications can do this by implementing the Autodesk.AutoCAD.Runtime.IExtensionApplication interface, which require Initialize() and Terminate() methods.</p>

<p>During the Initialize() method, you will typically want to set system variables and perhaps <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/calling_command.html">call commands</a> which execute some pre-existing initialization code for your application.</p>

<p>Here's some code showing how to implement this interface using VB.NET:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> InitializationTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Implements</span> Autodesk.AutoCAD.Runtime.IExtensionApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Initialize() <span style="COLOR: blue">Implements</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; IExtensionApplication.Initialize</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;Initializing - do something useful.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Terminate() <span style="COLOR: blue">Implements</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; IExtensionApplication.Terminate</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Console.WriteLine(<span style="COLOR: maroon">&quot;Cleaning up...&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TST&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;This is the TST command.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p></div></div>

<p>And here's the equivalent code in C#:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> InitializationTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; : Autodesk.AutoCAD.Runtime.IExtensionApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Editor ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;Initializing - do something useful.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: teal">Console</span>.WriteLine(<span style="COLOR: maroon">&quot;Cleaning up...&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [CommandMethod(<span style="COLOR: maroon">&quot;TST&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Test()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Editor ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;This is the TST command.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div></div>

<p>A few notes about this code:</p>

<p>.NET modules are not currently unloaded until AutoCAD terminates. While this is a popular request from developers (as it would make debugging much simpler), my understanding is that this is an issue that is inherent to the implementation of .NET - see <a href="http://blogs.msdn.com/jasonz/archive/2004/05/31/145105.aspx">this MSDN blog post</a> for more information.</p>

<p>What this means is that by the time the Terminate() method is called, AutoCAD is already in the process of closing. This is why I've used Console.Write() rather than ed.WriteMessage(), as by this point there's no command-line to write to.</p>

<p>That said, you can and should use the Terminate() callback to close any open files, database connections etc.</p>

<p>Something else you might come across when implementing this... I've implemented a single command in this application for a couple of reasons: in my next post I'm going to segregate the command into a different class, to show how you can tweak your application architecture both to follow a more logical structure and to optimize load performance.</p>

<p>The second reason I added the command was to raise a subtle you might well hit while coding: you might see the initialization string sent to the command-line as the application loads, but then the command is not found when you enter &quot;TST&quot; at the command-line. If you experience this behaviour, you're probably hitting an issue that can come up when coding managed applications against AutoCAD 2007: there's been a change in this version so that acmgd.dll is loaded on startup, and under certain circumstances this assembly might end up getting loaded again if found on a different path, causing your commands not to work.</p>

<p>The issue can be tricky to identify but is one that can be resolved in a number of ways:</p>

<ol><li>Edit the Registry to disable the demand-loading &quot;load on startup&quot; of acmgd.dll (a bad idea, in my opinion - it's safer not to second guess what assumptions might have been made about the availability of core modules)</li>

<li>Make sure AutoCAD is launched from its own working directory - commonly this issue is hit while debugging because Visual Studio doesn't automatically pick up the debugging application's working directory</li>

<li>Set the &quot;Copy Local&quot; flag for acmgd.dll to &quot;False&quot;. &quot;Copy Local&quot; tells Visual Studio whether the build process should make copies of the assemblies referenced by the project in its output folder</li></ol>

<p>My preference is for the third approach, as on a number of occasions I've overwritten acmgd.dll and acdbmgd.dll with those from another AutoCAD version (inadvertently trashing my AutoCAD installation). This usually happens when testing projects across versions (projects that's I've set to output directly to AutoCAD's program folder, for convenience), and I've forgotten to change the assemblies references before building.</p>

<p>So I would actually set both your references to acdbmgd.dll and acmgd.dll to &quot;Copy Local = False&quot;. You can do this either by selecting the Reference(s) via the Solution Explorer (in C#) or via the References tab in your Project Properties (in VB.NET) and then editing the reference's Properties.</p>
