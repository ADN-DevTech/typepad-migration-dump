---
layout: "post"
title: "Initialization code in your F# AutoCAD application"
date: "2008-03-25 11:26:10"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "F#"
  - "Notification / Events"
original_url: "https://www.keanw.com/2008/03/initialization.html "
typepad_basename: "initialization"
typepad_status: "Publish"
---

<p>Back from a nice long weekend, although I spent most of it sick with a cold. I find this increasingly the way with me: I fend off illness for months at a time (probably through stress, truth be told) but then I get a few days off and <em>wham</em>. A shame, as we had a huge dump of snow over the weekend... we get white Christmases here every five years or so, but it's really uncommon to get a white Easter.</p>

<p>I had a very interesting question come in by email from 冷血儿, who wanted to get the technique shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/09/initialization_.html" target="_blank">this post</a> working in his F# application.</p>

<p>Here's the F# code I managed to put together after consulting <a href="http://cs.hubfs.net/" target="_blank">hubFS</a>, in particular:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">#light</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> MyNamespace</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.Runtime</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">open</span> Autodesk.AutoCAD.ApplicationServices</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">type</span> InitTest() =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">class</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">let</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">interface</span> IExtensionApplication <span style="COLOR: blue">with</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">member</span> x.Initialize() =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; ed.WriteMessage</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; (<span style="COLOR: maroon">&quot;\nInitializing - do something useful.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">member</span> x.Terminate() =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; printfn <span style="COLOR: maroon">&quot;\nCleaning up...&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">end</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">end</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">module</span> MyApplication =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> ed =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; Application.DocumentManager.MdiActiveDocument.Editor</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [&lt;CommandMethod(<span style="COLOR: maroon">&quot;TST&quot;</span>)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">let</span> f () =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nThis is the TST command.&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; [&lt;assembly: ExtensionApplication(<span style="COLOR: blue">type</span> InitTest)&gt;]</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">do</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; ed.WriteMessage(<span style="COLOR: maroon">&quot;\nModule do&quot;</span>)</p></div>

<p>Here's what happens when we load our module and run the TST command:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">NETLOAD</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Module do</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Initializing - do something useful.</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">Command: <span style="color: #ff0000;">TST</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">This is the TST command.</p></div>
