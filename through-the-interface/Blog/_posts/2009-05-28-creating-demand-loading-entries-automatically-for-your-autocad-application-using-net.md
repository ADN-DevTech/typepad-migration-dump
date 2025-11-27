---
layout: "post"
title: "Creating demand-loading entries automatically for your AutoCAD application using .NET"
date: "2009-05-28 12:40:17"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Runtime"
original_url: "https://www.keanw.com/2009/05/creating-demand-loading-entries-automatically-for-your-autocad-application-using-net.html "
typepad_basename: "creating-demand-loading-entries-automatically-for-your-autocad-application-using-net"
typepad_status: "Publish"
---

<p>Here’s a question I received recently by email:</p>
<blockquote>
<p><em>How do you set up a .NET plugin for AutoCAD to install &amp; demand load in the same way as ObjectARX plugins? The documentation is not very clear at making the distinctions visible.</em></p>
<p><em>In ARX terms, we currently write a set of registry entries as part of our installer, along with refreshing these via an AcadAppInfo registration during ARX load. The ARX itself can be located anywhere as long as the registry entries point to it. I’m not sure of the correct procedure for .NET plugins to duplicate this.</em></p></blockquote>
<p>Augusto Gonçalves, from our DevTech Americas team, provided a solution for this which showed how to create <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/automatic_loadi.html">demand-loading Registry keys</a> programmatically based on the current assembly’s name and location. It occurred to me that extending the code to make further use of reflection to query the commands defined by an assembly would make this really interesting, and could essentially create a very flexible approach for creation of a demand-loading entries as an application initializes or during execution of a custom command.</p>
<p>Here’s the C# code:</p>
<div style="FONT-FAMILY: Courier New; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Collections.Generic;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Reflection;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> System.Resources;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Microsoft.Win32;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%; COLOR: blue">namespace</span><span style="LINE-HEIGHT: 140%"> DemandLoading</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryUpdate</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> RegisterForDemandLoading()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the assembly, its name and location</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Assembly</span><span style="LINE-HEIGHT: 140%"> assem = </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Assembly</span><span style="LINE-HEIGHT: 140%">.GetExecutingAssembly();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> name = assem.GetName().Name;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> path = assem.Location;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We&#39;ll collect information on the commands</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (we could have used a map or a more complex</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// container for the global and localized names</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// - the assumption is we will have an equal</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// number of each with possibly fewer groups)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; globCmds = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt;();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; locCmds = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt;();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; groups = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt;();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Iterate through the modules in the assembly</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Module</span><span style="LINE-HEIGHT: 140%">[] mods = assem.GetModules(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Module</span><span style="LINE-HEIGHT: 140%"> mod </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> mods)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Within each module, iterate through the types</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Type</span><span style="LINE-HEIGHT: 140%">[] types = mod.GetTypes();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Type</span><span style="LINE-HEIGHT: 140%"> type </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> types)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// We may need to get a type&#39;s resources</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ResourceManager</span><span style="LINE-HEIGHT: 140%"> rm =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">ResourceManager</span><span style="LINE-HEIGHT: 140%">(type.FullName, assem);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; rm.IgnoreCase = </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get each method on a type</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MethodInfo</span><span style="LINE-HEIGHT: 140%">[] meths = type.GetMethods();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">MethodInfo</span><span style="LINE-HEIGHT: 140%"> meth </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> meths)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Get the methods custom command attribute(s)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%">[] attbs =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; meth.GetCustomAttributes(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethodAttribute</span><span style="LINE-HEIGHT: 140%">),</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">object</span><span style="LINE-HEIGHT: 140%"> attb </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> attbs)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethodAttribute</span><span style="LINE-HEIGHT: 140%"> cma =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; attb </span><span style="LINE-HEIGHT: 140%; COLOR: blue">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">CommandMethodAttribute</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (cma != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And we can finally harvest the information</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// about each command</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> globName = cma.GlobalName;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> locName = globName;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> lid = cma.LocalizedNameId;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// If we have a localized command ID,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// let&#39;s look it up in our resources</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (lid != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s put a try-catch block around this</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Failure just means we use the global</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// name twice (the default)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; locName = rm.GetString(lid);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> { }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Add the information to our data structures</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; globCmds.Add(globName);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; locCmds.Add(locName);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (cma.GroupName != </span><span style="LINE-HEIGHT: 140%; COLOR: blue">null</span><span style="LINE-HEIGHT: 140%"> &amp;&amp;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; !groups.Contains(cma.GroupName))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; groups.Add(cma.GroupName);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Let&#39;s register the application to load on demand (12)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// if it contains commands, otherwise we will have it</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// load on AutoCAD startup (2)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> flags = (globCmds.Count &gt; 0 ? 12 : 2);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// By default let&#39;s create the commands in HKCU</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// (pass false if we want to create in HKLM)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; CreateDemandLoadingEntries(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; name, path, globCmds, locCmds, groups, flags, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> UnregisterForDemandLoading()</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; RemoveDemandLoadingEntries(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Helper functions</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> CreateDemandLoadingEntries(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> name,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> path,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; globCmds,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; locCmds,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">List</span><span style="LINE-HEIGHT: 140%">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">&gt; groups,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> flags,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> currentUser</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; )</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Choose a Registry hive based on the function input</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> hive =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; (currentUser ? </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Registry</span><span style="LINE-HEIGHT: 140%">.CurrentUser : </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Registry</span><span style="LINE-HEIGHT: 140%">.LocalMachine);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Open the main AutoCAD (or vertical) and &quot;Applications&quot; keys</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> ack =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; hive.OpenSubKey(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">HostApplicationServices</span><span style="LINE-HEIGHT: 140%">.Current.RegistryProductRootKey,</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (ack)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> appk = ack.CreateSubKey(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Applications&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (appk)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Already registered? Just return</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%">[] subKeys = appk.GetSubKeyNames();</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> subKey </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> subKeys)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (subKey.Equals(name))</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create the our application&#39;s root key and its values</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> rk = appk.CreateSubKey(name);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (rk)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rk.SetValue(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;DESCRIPTION&quot;</span><span style="LINE-HEIGHT: 140%">, name, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.String</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rk.SetValue(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LOADCTRLS&quot;</span><span style="LINE-HEIGHT: 140%">, flags, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.DWord);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rk.SetValue(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;LOADER&quot;</span><span style="LINE-HEIGHT: 140%">, path, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.String);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rk.SetValue(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;MANAGED&quot;</span><span style="LINE-HEIGHT: 140%">, 1, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.DWord);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Create a subkey if there are any commands...</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> ((globCmds.Count == locCmds.Count) &amp;&amp;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; globCmds.Count &gt; 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> ck = rk.CreateSubKey(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Commands&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (ck)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">for</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">int</span><span style="LINE-HEIGHT: 140%"> i = 0; i &lt; globCmds.Count; i++)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ck.SetValue(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; globCmds[i],</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; locCmds[i],</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.String</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// And the command groups, if there are any</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">if</span><span style="LINE-HEIGHT: 140%"> (groups.Count &gt; 0)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> gk = rk.CreateSubKey(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Groups&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (gk)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="LINE-HEIGHT: 140%; COLOR: blue">string</span><span style="LINE-HEIGHT: 140%"> grpName </span><span style="LINE-HEIGHT: 140%; COLOR: blue">in</span><span style="LINE-HEIGHT: 140%"> groups)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; gk.SetValue(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; grpName, grpName, </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryValueKind</span><span style="LINE-HEIGHT: 140%">.String</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="LINE-HEIGHT: 140%; COLOR: blue">void</span><span style="LINE-HEIGHT: 140%"> RemoveDemandLoadingEntries(</span><span style="LINE-HEIGHT: 140%; COLOR: blue">bool</span><span style="LINE-HEIGHT: 140%"> currentUser)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">try</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Choose a Registry hive based on the function input</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> hive =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; (currentUser ?</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Registry</span><span style="LINE-HEIGHT: 140%">.CurrentUser :</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Registry</span><span style="LINE-HEIGHT: 140%">.LocalMachine);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Open the main AutoCAD (vertical) and &quot;Applications&quot; keys</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> ack =</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; hive.OpenSubKey(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">HostApplicationServices</span><span style="LINE-HEIGHT: 140%">.Current.RegistryProductRootKey</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (ack)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">RegistryKey</span><span style="LINE-HEIGHT: 140%"> appk = ack.OpenSubKey(</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">&quot;Applications&quot;</span><span style="LINE-HEIGHT: 140%">, </span><span style="LINE-HEIGHT: 140%; COLOR: blue">true</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">using</span><span style="LINE-HEIGHT: 140%"> (appk)</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; {</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: green">// Delete the key with the same name as this assembly</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; appk.DeleteSubKeyTree(</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: #2b91af">Assembly</span><span style="LINE-HEIGHT: 140%">.GetExecutingAssembly().GetName().Name</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; );</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; &#0160; </span><span style="LINE-HEIGHT: 140%; COLOR: blue">catch</span><span style="LINE-HEIGHT: 140%"> { }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; &#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<p>If you drop this code into an existing project, you should be able simply to add a call to DemandLoading.RegistryUpdate.RegisterForDemandLoading() during your IExtensionApplication’s Initialize() method or during a custom command.</p>
<p>Here’s an example of the Registry keys created by this code (exported from Regedit) when called from the Initialize() method of the application in <a href="http://through-the-interface.typepad.com/through_the_interface/2009/05/importing-autocad-layers-from-xrefs-using-net.html">this previous post</a>:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">Windows Registry Editor Version 5.00</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R18.0\ACAD-8001:409\Applications\OffsetInXref]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;DESCRIPTION&quot;=&quot;OffsetInXref&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;LOADCTRLS&quot;=dword:0000000c</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;LOADER&quot;=&quot;C:\\Program Files\\Autodesk\\AutoCAD 2010\\OffsetInXref.dll&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;MANAGED&quot;=dword:00000001</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">[HKEY_CURRENT_USER\Software\Autodesk\AutoCAD\R18.0\ACAD-8001:409\Applications\OffsetInXref\Commands]</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;XOFFSETLAYER&quot;=&quot;XOFFSETLAYER&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt"><span style="LINE-HEIGHT: 140%">&quot;XOFFSETCPLAYS&quot;=&quot;XOFFSETCPLAYS&quot;</span></p>
<p style="MARGIN: 0px; FONT-SIZE: 8pt">&#0160;</p></div>
<p>You can see that our code found the commands defined by the application and created Registry entries which tell AutoCAD to load the module when one of them is chosen by the user. You’ll notice the LOADCTRLS value is c (hexadecimal for 12), which means the application will be loaded “on demand” as a specified command is invoked, but we could also adjust the code to force this to 2, which would mean the module would be loaded on AutoCAD startup (the default when no commands are found). This would actually be a good idea, in this case, as the command hooks into the OFFSET command, and we can’t demand-load a module on invocation of a built-in command.</p>
<p>You’ll also notice that the keys were created under R18.0\ACAD-8001:409 (the English version of AutoCAD 2010), but if the module was loaded in a different language version of an AutoCAD-based vertical product (French AutoCAD Architecture 2009, for instance) then the root key would be the one for that product. All you have to do is load the module once in the AutoCAD-based product of your choice, and it will be registered for automatic loading from then onwards.</p>
<p>This is a useful technique for people who want to deploy .NET modules without installers, or for people who wish applications to re-create their demand-loading keys on load (something this code currently does not do, by the way: if the application’s key is found we do not recreate the contents for the sake of efficiency… you may want to change the code to force creation of the keys should you be adding new commands regularly to your application, for instance).</p>
<p><strong><em>Update:</em></strong></p>
<p>I&#39;ve gone ahead and updated the above code as per the information in <a href="http://through-the-interface.typepad.com/through_the_interface/2010/03/creating-demand-loading-entries-automatically-for-your-autocad-application-using-c-f-or-vbnet.html">this more recent post</a>.</p>
