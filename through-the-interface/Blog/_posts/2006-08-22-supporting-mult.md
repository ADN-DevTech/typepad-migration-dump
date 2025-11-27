---
layout: "post"
title: "Supporting multiple AutoCAD versions - conditional compilation in C#"
date: "2006-08-22 15:25:27"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
original_url: "https://www.keanw.com/2006/08/supporting_mult.html "
typepad_basename: "supporting_mult"
typepad_status: "Publish"
---

<p>Someone asked by email how to get the block import code first shown <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/import_blocks_f.html">last week</a> and further discussed in <a href="http://through-the-interface.typepad.com/through_the_interface/2006/08/breaking_it_dow.html">yesterday's post</a> working with AutoCAD 2006. I've been using AutoCAD 2007 for this, but to get it working there are only a few changes to be made.</p>

<p>Firstly you'll need to make sure you have the correct versions of acmgd.dll and acdbmgd.dll referenced into the project (you should be able to tell from their path which version of AutoCAD they were installed with).</p>

<p>Secondly you'll need to modify your code slightly, as WblockCloneObjects() has changed its signature from 2006 to 2007. The below code segment shows how to use pre-processor directives to implement conditional compilation in C#. You will need to set either AC2006 or AC2007 as a &quot;conditional compilation symbol&quot; in your project settings for this to work:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#if</span> AC2006</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: gray">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; mapping = sourceDb.WblockCloneObjects(blockIds,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: gray">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; destDb.BlockTableId,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: gray">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; DuplicateRecordCloning.Replace,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: gray">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; false);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#elif</span> AC2007</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; sourceDb.WblockCloneObjects(blockIds,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;destDb.BlockTableId,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;mapping,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: teal">DuplicateRecordCloning</span>.Replace,</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">false</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">#endif</span></p></div><p>You would possibly use #else rather than #elif above, simply because that would make your code more future-proof: it would automatically support new versions without needing to specifically add code to check for them.</p>

<p>On a side note, you can actually specify that entire methods should only be compiled into a certain version. Firstly, you'll need to import the System.Diagnostics namespace:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">using</span> System.Diagnostics;</p></div><p>Then you simply use the Conditional attribute to declare a particular method (in this case a command) for a specific version of AutoCAD (you might also specify a debug-only command by being conditional on the DEBUG pre-processor symbol, should you wish):</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">namespace</span> BlockImport</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">class</span> <span style="COLOR: teal">BlockImportClass</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; [<strong><span style="COLOR: teal">Conditional</span>(<span style="COLOR: maroon">&quot;AC2007&quot;</span>)</strong>,<span style="COLOR: teal">CommandMethod</span>(<span style="COLOR: maroon">&quot;IB&quot;</span>)] </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">public</span> <span style="COLOR: blue">void</span> ImportBlock()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; {</p></div><p>...</p>
