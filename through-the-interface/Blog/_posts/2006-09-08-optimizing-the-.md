---
layout: "post"
title: "Optimizing the loading of AutoCAD .NET applications"
date: "2006-09-08 09:00:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Runtime"
original_url: "https://www.keanw.com/2006/09/optimizing_the_.html "
typepad_basename: "optimizing_the_"
typepad_status: "Publish"
---

<p>In my <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/initialization_.html">previous post</a> I described how you could use the Autodesk.AutoCAD.Runtime.IExtensionApplication interface to implement initialization code in your .NET module. Building on this, we're now going to look at how use of the Autodesk.AutoCAD.Runtime.IExtensionApplication interface can also allow you - with very little effort - to optimize the architecture of your managed modules for faster loading into AutoCAD.</p>

<p>First some information from the &quot;Using .NET for AutoCAD documentation&quot; (which is available in the ObjectARX Developer's Guide on the ObjectARX SDK):</p><blockquote dir="ltr"><p><em>When AutoCAD loads a managed application, it queries the application's assembly for an <strong>ExtensionApplication</strong> custom attribute. If this attribute is found, AutoCAD sets the attribute's associated type as the application's entry point. If no such attribute is found, AutoCAD searches all exported types for an IExtensionApplication implementation. If no implementation is found, AutoCAD simply skips the application-specific initialization step.</em></p>

<p><em>...</em></p>

<p><em>In addition to searching for an IExtensionApplication implementation, AutoCAD queries the application's assembly for one or more <strong>CommandClass</strong> attributes. If instances of this attribute are found, AutoCAD searches only their associated types for command methods. Otherwise, it searches all exported types.</em></p></blockquote><p dir="ltr">The samples that I've shown in this blog - and most of those on the ObjectARX SDK - do not show how you can use the ExtensionApplication or CommandClass attribute in your code, as it's not essential to implement them for your application to work. But if you have a large .NET module to be loaded into AutoCAD, it might take some time for AutoCAD to check the various objects in the assembly, to find out which is the ExtensionApplication and which are the various CommandClasses.</p>

<p dir="ltr">The attributes you need to implement are very straightforward:</p>

<p dir="ltr"><strong>C#:</strong></p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly: <span style="COLOR: teal">ExtensionApplication</span>(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">InitClass</span>))]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly: <span style="COLOR: teal">CommandClass</span>(<span style="COLOR: blue">typeof</span>(<span style="COLOR: teal">CmdClass</span>))]</p></div>

<p dir="ltr"><strong>VB.NET:</strong></p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;Assembly: ExtensionApplication(<span style="COLOR: blue">GetType</span>(InitClass))&gt; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;Assembly: CommandClass(<span style="COLOR: blue">GetType</span>(CmdClass))&gt; </p></div>

<p dir="ltr">These assembly-level attributes simply tell AutoCAD where to look for the various objects it will otherwise need to identify by searching. Here's some more information from the documentation on the use of these attributes:</p><blockquote dir="ltr"><p dir="ltr"><em>The <strong>ExtensionApplication</strong> attribute can be attached to only one type. The type to which it is attached must implement the IExtensionApplication interface.</em></p>

<p dir="ltr"><em>...</em></p>

<p dir="ltr"><em>A <strong>CommandClass</strong> attribute may be declared for any type that defines AutoCAD command handlers. If an application uses the CommandClass attribute, it must declare an instance of this attribute for every type that contains an AutoCAD command handler method.</em></p></blockquote><p dir="ltr">While optimizing yesterday's code to reduce load-time, I also changed the structure slightly to be more logical. The above attributes also take classes within a namespace, so I decided to split the initialization code (the &quot;Initialization&quot; class) away from the command implementations (the &quot;Commands&quot; class), but keeping them both in the same (&quot;ManagedApplication&quot;) namespace.</p>

<p dir="ltr">And here's the code...</p>

<p dir="ltr"><strong>C#:</strong></p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Runtime;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.ApplicationServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.EditorInput;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">ExtensionApplication</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">typeof</span>(ManagedApplication.<span style="COLOR: teal">Initialization</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[assembly:</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">CommandClass</span>(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">typeof</span>(ManagedApplication.<span style="COLOR: teal">Commands</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> ManagedApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Initialization</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; : Autodesk.AutoCAD.Runtime.<span style="COLOR: teal">IExtensionApplication</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Initialize()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;Initializing - do something useful.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Terminate()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Console</span>.WriteLine(<span style="COLOR: maroon">&quot;Cleaning up...&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">Commands</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;TST&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> Test()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Editor</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Application</span>.DocumentManager.MdiActiveDocument.Editor;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;This is the TST command.&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p dir="ltr"><strong>VB.NET:</strong></p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.EditorInput</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> System</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;Assembly: _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; ExtensionApplication( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">GetType</span>(ManagedApplication.Initialization))&gt; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&lt;Assembly: _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; CommandClass( _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">GetType</span>(ManagedApplication.Commands))&gt; </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Namespace</span> ManagedApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> Initialization</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Implements</span> Autodesk.AutoCAD.Runtime.IExtensionApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Initialize() <span style="COLOR: blue">Implements</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; IExtensionApplication.Initialize</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;Initializing - do something useful.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Terminate() <span style="COLOR: blue">Implements</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; IExtensionApplication.Terminate</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Console.WriteLine(<span style="COLOR: maroon">&quot;Cleaning up...&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> Commands</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TST&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> Test()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">Dim</span> ed <span style="COLOR: blue">As</span> Editor = _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;ed.WriteMessage(<span style="COLOR: maroon">&quot;This is the TST command.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Namespace</span></p></div>

<p dir="ltr"></p>
