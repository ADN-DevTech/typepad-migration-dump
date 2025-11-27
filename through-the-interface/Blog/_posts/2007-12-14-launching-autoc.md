---
layout: "post"
title: "Launching AutoCAD from a .NET application"
date: "2007-12-14 12:21:01"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2007/12/launching-autoc.html "
typepad_basename: "launching-autoc"
typepad_status: "Publish"
---

<p><em>This topic has been raised a few times, and Adam Nagy, from our DevTech EMEA team, sent a technical response recently with code that I decided to use as the basis for this post.</em></p>

<p>Developers typically want to either integrate functionality into AutoCAD (using its plug-in architecture to add commands, user-interface elements, objects, etc.), or to drive it, automating common tasks. Clearly the line can get blurred between these two areas, but today I’m going to focus on the second category.</p>

<p>To help with later explanations, I’d like to introduce two types of application interaction:</p>

<p><strong>Out-of-process</strong></p>

<p>In this situation we have two separate executables trying to communicate. Imagine you have an EXE and you want to drive AutoCAD. You need to find some way to launch AutoCAD and communicate with it – typically using <a href="http://en.wikipedia.org/wiki/Component_Object_Model">COM</a> or, before that, <a href="http://en.wikipedia.org/wiki/Dynamic_Data_Exchange">DDE</a>. The communication is, by definition, done via <a href="http://en.wikipedia.org/wiki/Inter-process_communication">Inter-Process Communication (IPC)</a>, which is a very inefficient way to send large chunks of data. This is why old ADS and external VB applications ran (or run) so slowly. </p>

<p><strong>In-process</strong></p>

<p>When your code is packaged inside a <a href="http://en.wikipedia.org/wiki/Dynamic-link_library">DLL</a> (whether an ActiveX DLL defined in VB6, an ObjectARX module or a .NET assembly), the communication with the host process (AutoCAD) is much more efficient – data structures can be passed by pointer or reference rather than squirting the information through the inefficient marshalling of IPC.</p>

<p>Most of AutoCAD’s current APIs are designed to be used “in-process” – this includes LISP, ObjectARX and AutoCAD’s .NET API. Given the availability of <a href="http://en.wikipedia.org/wiki/.NET_Remoting">.NET Remoting</a>, people often hope or expect AutoCAD to be drivable via .NET “out-of-process”, but this is not how the managed API was designed: it is really a wrapper on top of ObjectARX, whose performance is based on direct access to objects via pointers, and this simply does not extend to work across process boundaries. One of the great features of COM Automation is that it was designed to work either out-of-process (from an external EXE) or in-process (from VBA or by calling GetInterfaceObject() to load a VB6 ActiveX DLL). This is still the best way to drive AutoCAD from an external executable.</p>

<p>I generally recommend not trying to pass too much information across the process boundary: if you need to drive AutoCAD from an external executable, simply launch it (or connect to a running instance, if that’s an option) and then load an in-process module to do the heavy lifting from within AutoCAD’s process space.</p>

<p>The following code shows how to do just that, using C#. It tries to connect to a running instance of AutoCAD (this is optional – you can easily adjust the code only to launch AutoCAD), and failing that launches it. Once there’s a running instance we make it visible and run a custom command. My recommendation is to set up <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">demand-loading</a> for your application – either to load it on AutoCAD startup or when a command is invoked – and then to run a command defined in your module.</p>

<p>You will need to add a reference to the “AutoCAD Type Library” from your application, as well as including these assembly references:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Runtime.InteropServices;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> Autodesk.AutoCAD.Interop;</p></div>

<p>This C# code can be added to an “on-click” event handler for a button (for example) or another function that makes sense in the context of your application.</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;AutoCAD.Application.17&quot; uses 2007 or 2008,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">//&nbsp; whichever was most recently run</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">// &quot;AutoCAD.Application.17.1&quot; uses 2008, specifically</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">const</span> <span style="COLOR: blue">string</span> progID = <span style="COLOR: maroon">&quot;AutoCAD.Application.17.1&quot;</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: teal">AcadApplication</span> acApp = <span style="COLOR: blue">null</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acApp =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;(<span style="COLOR: teal">AcadApplication</span>)<span style="COLOR: teal">Marshal</span>.GetActiveObject(progID);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">Type</span> acType =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: teal">Type</span>.GetTypeFromProgID(progID);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;acApp =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (<span style="COLOR: teal">AcadApplication</span>)<span style="COLOR: teal">Activator</span>.CreateInstance(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; acType,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">true</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">catch</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">MessageBox</span>.Show(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">&quot;Cannot create object of type \&quot;&quot;</span> +</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; progID + <span style="COLOR: maroon">&quot;\&quot;&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">if</span> (acApp != <span style="COLOR: blue">null</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// By the time this is reached AutoCAD is fully</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: green">// functional and can be interacted with through code</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acApp.Visible = <span style="COLOR: blue">true</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; acApp.ActiveDocument.SendCommand(<span style="COLOR: maroon">&quot;_MYCOMMAND &quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p></div>
