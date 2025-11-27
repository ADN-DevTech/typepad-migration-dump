---
layout: "post"
title: "Calling AutoCAD commands from .NET"
date: "2006-08-30 23:33:15"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
original_url: "https://www.keanw.com/2006/08/calling_command.html "
typepad_basename: "calling_command"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/techniques_for_.html">this earlier entry</a> I showed some techniques for calling AutoCAD commands programmatically from ObjectARX and from VB(A). Thanks to Scott Underwood for proposing that I also mention calling commands from .NET, and also to Jorge Lopez for (in a strange coincidence) pinging me via IM this evening with the C# <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/calling_objecta.html">P/Invoke declarations</a> for ads_queueexpr() and acedPostCommand(). It felt like the planets were aligned... :-)</p>

<p>Here are some ways to send commands to AutoCAD from a .NET app:</p>

<ul><li>SendStringToExecute from the managed document object</li>

<li>SendCommand from the COM document object</li>

<li>acedPostCommand via P/Invoke</li>

<li>ads_queueexpr() via P/Invoke</li></ul>

<p>Here's some VB.NET code - you'll need to add in a COM reference to AutoCAD 2007 Type Library in addition to the standard .NET references to acmgd.dll and acdbmgd.dll.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.ApplicationServices</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Imports</span> Autodesk.AutoCAD.Interop</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">Public</span> <span style="COLOR: blue">Class</span> SendCommandTest</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Declare</span> <span style="COLOR: blue">Auto</span> <span style="COLOR: blue">Function</span> ads_queueexpr <span style="COLOR: blue">Lib</span> <span style="COLOR: maroon">&quot;acad.exe&quot;</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: blue">ByVal</span> strExpr <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>) <span style="COLOR: blue">As</span> <span style="COLOR: blue">Integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Private</span> <span style="COLOR: blue">Declare</span> <span style="COLOR: blue">Auto</span> <span style="COLOR: blue">Function</span> acedPostCommand <span style="COLOR: blue">Lib</span> <span style="COLOR: maroon">&quot;acad.exe&quot;</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Alias</span> <span style="COLOR: maroon">&quot;?acedPostCommand@@YAHPB_W@Z&quot;</span> _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: blue">ByVal</span> strExpr <span style="COLOR: blue">As</span> <span style="COLOR: blue">String</span>) <span style="COLOR: blue">As</span> <span style="COLOR: blue">Integer</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TEST1&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> SendStringToExecuteTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> doc <span style="COLOR: blue">As</span> Autodesk.AutoCAD.ApplicationServices.Document</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc = Application.DocumentManager.MdiActiveDocument</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; doc.SendStringToExecute(<span style="COLOR: maroon">&quot;_POINT 1,1,0 &quot;</span>, <span style="COLOR: blue">False</span>, <span style="COLOR: blue">False</span>, <span style="COLOR: blue">True</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TEST2&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> SendCommandTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">Dim</span> app <span style="COLOR: blue">As</span> AcadApplication = Application.AcadApplication</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; app.ActiveDocument.SendCommand(<span style="COLOR: maroon">&quot;_POINT 2,2,0 &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TEST3&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> PostCommandTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; acedPostCommand(<span style="COLOR: maroon">&quot;_POINT 3,3,0 &quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; &lt;CommandMethod(<span style="COLOR: maroon">&quot;TEST4&quot;</span>)&gt; _</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">Public</span> <span style="COLOR: blue">Sub</span> QueueExprTest()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ads_queueexpr(<span style="COLOR: maroon">&quot;(command&quot;&quot;_POINT&quot;&quot; &quot;&quot;4,4,0&quot;&quot;)&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">End</span> <span style="COLOR: blue">Sub</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">End</span> <span style="COLOR: blue">Class</span></p></div>

<p>In case you're working in C#, here are the declarations of acedPostCommand() and ads_queueexpr() that Jorge sent to me:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">[DllImport(<span style="COLOR: maroon">&quot;acad.exe&quot;</span>, CharSet = CharSet.Auto,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; CallingConvention = CallingConvention.Cdecl)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">private</span> <span style="COLOR: blue">int</span> ads_queueexpr(<span style="COLOR: blue">string</span> strExpr);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">[DllImport(<span style="COLOR: maroon">&quot;acad.exe&quot;</span>, CharSet = CharSet.Auto,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; CallingConvention = CallingConvention.Cdecl,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; EntryPoint = <span style="COLOR: maroon">&quot;?acedPostCommand@@YAHPB_W@Z&quot;</span>)]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: blue">static</span> <span style="COLOR: blue">private</span> <span style="COLOR: blue">int</span> acedPostCommand(<span style="COLOR: blue">string</span> strExpr);</p></div>

<p>You'll need to specify:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Runtime.InteropServices;</p></div>

<p>to get DllImport to work, of course.</p>
