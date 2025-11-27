---
layout: "post"
title: "Preventing a .NET module from being loaded by AutoCAD"
date: "2008-09-08 10:47:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Runtime"
original_url: "https://www.keanw.com/2008/09/preventing-a-ne.html "
typepad_basename: "preventing-a-ne"
typepad_status: "Publish"
---

<p>This is an interesting one that came up recently during an internal discussion:</p><blockquote><p><em>During my module's Initialize() function, I want to decide that the module should not actually be loaded. How can I accomplish that?</em></p></blockquote><p>The answer is surprisingly simple: if you throw an exception during the function, AutoCAD's NETLOAD mechanism will stop loading the application.</p>

<p>For an example, see this C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> PreventLoad</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; : </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">IExtensionApplication</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Initialize()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// This will prevent the application from loading</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">throw</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Exception</span></span><span style="LINE-HEIGHT: 140%">();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> Terminate(){}</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;TEST&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> TestCommand()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;doc.Editor.WriteMessage(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nThis should not get called.&quot;</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>

<p>Here's what happens when we attempt to load the application and then run the TEST command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">FILEDIA</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Enter new value for FILEDIA &lt;1&gt;: <span style="color: #ff0000;">0</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">NETLOAD</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Assembly file name: <span style="color: #ff0000;">c:\MyApplication.dll</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">TEST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Unknown command &quot;TEST&quot;.&nbsp; Press F1 for help.</p></div>
