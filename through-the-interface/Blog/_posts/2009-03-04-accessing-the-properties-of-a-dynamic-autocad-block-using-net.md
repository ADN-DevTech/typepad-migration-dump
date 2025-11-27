---
layout: "post"
title: "Accessing the properties of a dynamic AutoCAD block using .NET"
date: "2009-03-04 22:32:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Dynamic Blocks"
original_url: "https://www.keanw.com/2009/03/accessing-the-properties-of-a-dynamic-autocad-block-using-net.html "
typepad_basename: "accessing-the-properties-of-a-dynamic-autocad-block-using-net"
typepad_status: "Publish"
---

<p>This is one of those funny scenarios... I was just thinking about what to do for my next post - whether to dive into some new features of AutoCAD 2010 (which I will do soon, I promise! :-) or whether to choose something from my ever-increasing to-do list, when I received two emails.</p>
<p>One was from our old friend <a href="http://arxdummies.blogspot.com/">Fernando Malard</a>, suggesting a topic for a blog post, and the other was from Philippe Leefsma, a member of our DevTech team in Europe, in response to an ADN members question. It provided some code that could eventually form the basis for a response to Fernando&#39;s question. Coincidence? Maybe. Am I one to ignore serendipity at work (or to look a gift horse in the mouth)? No!</p>
<p>So, here&#39;s Fernando&#39;s question:</p>
<blockquote>
<p><em>Just would suggest a new topic for your Blog. An article explaining how to insert a dynamic block and modifying its dynamic properties like point, rotation, dimension. This is something I’m currently working on .NET and I think it would be a very interesting topic to evolve.</em></p></blockquote>
<p>Philippe&#39;s code is a little different - it shows how to retrieve and display the &quot;dynamic&quot; properties of a dynamic block read in from an external file - but it was a great start for my first step towards Fernando&#39;s goal, which was simply to access the dynamic properties for a block selected by the user.</p>
<p>Here&#39;s the C# code I based on Philippe&#39;s:</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Geometry;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> DynamicBlocks</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; [</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;DBP&quot;</span><span style="LINE-HEIGHT: 140%">)]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> DynamicBlockProps()</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span><span style="LINE-HEIGHT: 140%"> doc =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStringOptions</span><span style="LINE-HEIGHT: 140%"> pso =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStringOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nEnter dynamic block name or enter to select: &quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; pso.AllowSpaces = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptResult</span><span style="LINE-HEIGHT: 140%"> pr = ed.GetString(pso);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (pr.Status != </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span><span style="LINE-HEIGHT: 140%"> tr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.TransactionManager.StartTransaction();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockReference</span><span style="LINE-HEIGHT: 140%"> br = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// If a null string was entered allow entity selection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (pr.StringResult == </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Select a block reference</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span><span style="LINE-HEIGHT: 140%"> peo =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect dynamic block reference: &quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; peo.SetRejectMessage(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nEntity is not a block.&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; peo.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockReference</span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityResult</span><span style="LINE-HEIGHT: 140%"> per =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.GetEntity(peo);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (per.Status != </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span><span style="LINE-HEIGHT: 140%">.OK)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Access the selected block reference</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; per.ObjectId,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ) </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockReference</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">else</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Otherwise we look up the block by name</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span><span style="LINE-HEIGHT: 140%"> bt =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; db.BlockTableId,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead) </span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTable</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (!bt.Has(pr.StringResult))</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nBlock \&quot;&quot;</span><span style="LINE-HEIGHT: 140%"> + pr.StringResult + </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\&quot; does not exist.&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Create a new block reference referring to the block</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockReference</span><span style="LINE-HEIGHT: 140%">(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Point3d</span><span style="LINE-HEIGHT: 140%">(),</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bt[pr.StringResult]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span><span style="LINE-HEIGHT: 140%"> btr =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockTableRecord</span><span style="LINE-HEIGHT: 140%">)tr.GetObject(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br.DynamicBlockTableRecord,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span><span style="LINE-HEIGHT: 140%">.ForRead</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Call our function to display the block properties</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DisplayDynBlockProperties(ed, br, btr.Name);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Committing is cheaper than aborting</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%"> </span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> DisplayDynBlockProperties(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span><span style="LINE-HEIGHT: 140%"> ed, </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">BlockReference</span><span style="LINE-HEIGHT: 140%"> br, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> name</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; )</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Only continue is we have a valid dynamic block</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (br != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%"> &amp;&amp; br.IsDynamicBlock)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nDynamic properties for \&quot;{0}\&quot;\n&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; name</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the dynamic block&#39;s property collection</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DynamicBlockReferencePropertyCollection</span><span style="LINE-HEIGHT: 140%"> pc =</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br.DynamicBlockReferencePropertyCollection;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Loop through, getting the info for each property</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">DynamicBlockReferenceProperty</span><span style="LINE-HEIGHT: 140%"> prop </span><span style="COLOR: blue; LINE-HEIGHT: 140%">in</span><span style="LINE-HEIGHT: 140%"> pc)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Start with the property name, type and description</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nProperty: \&quot;{0}\&quot; : {1}&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop.PropertyName,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop.UnitsType</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (prop.Description != </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;&quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&#0160; Description: {0}&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop.Description</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Is it read-only?</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (prop.ReadOnly)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot; (Read Only)&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the allowed values, if it&#39;s constrained</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">bool</span><span style="LINE-HEIGHT: 140%"> first = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">foreach</span><span style="LINE-HEIGHT: 140%"> (</span><span style="COLOR: blue; LINE-HEIGHT: 140%">object</span><span style="LINE-HEIGHT: 140%"> value </span><span style="COLOR: blue; LINE-HEIGHT: 140%">in</span><span style="LINE-HEIGHT: 140%"> prop.GetAllowedValues())</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (first ? </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&#0160; Allowed values: [&quot;</span><span style="LINE-HEIGHT: 140%"> : </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;, &quot;</span><span style="LINE-HEIGHT: 140%">)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\&quot;{0}\&quot;&quot;</span><span style="LINE-HEIGHT: 140%">, value);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; first = </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (!first)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;]&quot;</span><span style="LINE-HEIGHT: 140%">);</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: green; LINE-HEIGHT: 140%">// And finally the current value</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&#0160; Current value: \&quot;{0}\&quot;\n&quot;</span><span style="LINE-HEIGHT: 140%">,</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; prop.Value</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160;&#0160;&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; }</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div>
<br />
<p>Here&#39;s what happens when we run the DBP command, selecting the &quot;Hex Socket Bolt (Side) - Metric&quot; block from the &quot;Mechanical - Metric.dwg&quot; file in the Samples\Dynamic Blocks folder of your AutoCAD installation.</p>
<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new">
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Command: <font color="#ff0000">DBP</font></span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Enter dynamic block name or enter to select:</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Select dynamic block reference:</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%"></span>&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Dynamic properties for &quot;Hex Socket Bolt (Side) - Metric&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;d1&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;3&quot;, &quot;4&quot;, &quot;5&quot;, &quot;6&quot;, &quot;8&quot;, &quot;10&quot;, &quot;12&quot;, &quot;14&quot;, &quot;16&quot;, &quot;20&quot;, &quot;24&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;27&quot;, &quot;30&quot;, &quot;36&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;14&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(97.714,-5,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;b&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;18&quot;, &quot;20&quot;, &quot;22&quot;, &quot;24&quot;, &quot;28&quot;, &quot;32&quot;, &quot;36&quot;, &quot;40&quot;, &quot;44&quot;, &quot;52&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;57&quot;, &quot;60&quot;, &quot;65&quot;, &quot;66&quot;, &quot;72&quot;, &quot;73&quot;, &quot;84&quot;, &quot;85&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;40&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(100,-3.5,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;k&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;3&quot;, &quot;4&quot;, &quot;5&quot;, &quot;6&quot;, &quot;8&quot;, &quot;10&quot;, &quot;12&quot;, &quot;14&quot;, &quot;16&quot;, &quot;20&quot;, &quot;24&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;27&quot;, &quot;30&quot;, &quot;36&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;14&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(5.64204767561892E-15,-7,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;d2&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;5.5&quot;, &quot;7&quot;, &quot;8.5&quot;, &quot;10&quot;, &quot;13&quot;, &quot;16&quot;, &quot;18&quot;, &quot;21&quot;, &quot;24&quot;, &quot;30&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;36&quot;, &quot;40&quot;, &quot;45&quot;, &quot;54&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;21&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(3.5527136788005E-15,-8,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Size&quot; : NoUnits</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;M3&quot;, &quot;M4&quot;, &quot;M5&quot;, &quot;M6&quot;, &quot;M8&quot;, &quot;M10&quot;, &quot;M12&quot;, &quot;M14&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;M14&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Visibility&quot; : NoUnits</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;M3&quot;, &quot;M4&quot;, &quot;M5&quot;, &quot;M6&quot;, &quot;M8&quot;, &quot;M10&quot;, &quot;M12&quot;, &quot;M14&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;M14&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M3)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;25&quot;, &quot;30&quot;, &quot;35&quot;, &quot;40&quot;, &quot;45&quot;, &quot;50&quot;, &quot;60&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(0,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M4)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;30&quot;, &quot;35&quot;, &quot;40&quot;, &quot;45&quot;, &quot;50&quot;, &quot;60&quot;, &quot;70&quot;, &quot;80&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(0,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M5)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;30&quot;, &quot;35&quot;, &quot;40&quot;, &quot;45&quot;, &quot;50&quot;, &quot;55&quot;, &quot;60&quot;, &quot;70&quot;, &quot;80&quot;, &quot;90&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;100&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(0,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M6)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;35&quot;, &quot;40&quot;, &quot;45&quot;, &quot;50&quot;, &quot;55&quot;, &quot;60&quot;, &quot;65&quot;, &quot;70&quot;, &quot;75&quot;, &quot;80&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;90&quot;, &quot;100&quot;, &quot;110&quot;, &quot;120&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(2.25597318603832E-14,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M8)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;40&quot;, &quot;45&quot;, &quot;50&quot;, &quot;55&quot;, &quot;60&quot;, &quot;65&quot;, &quot;70&quot;, &quot;75&quot;, &quot;80&quot;, &quot;85&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;90&quot;, &quot;100&quot;, &quot;110&quot;, &quot;120&quot;, &quot;130&quot;, &quot;140&quot;, &quot;150&quot;, &quot;160&quot;, &quot;180&quot;, &quot;200&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(2.25597318603832E-14,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M10)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;45&quot;, &quot;50&quot;, &quot;55&quot;, &quot;60&quot;, &quot;65&quot;, &quot;70&quot;, &quot;75&quot;, &quot;80&quot;, &quot;85&quot;, &quot;90&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;100&quot;, &quot;110&quot;, &quot;120&quot;, &quot;130&quot;, &quot;140&quot;, &quot;150&quot;, &quot;160&quot;, &quot;180&quot;, &quot;200&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(2.25597318603832E-14,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M12)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;55&quot;, &quot;60&quot;, &quot;65&quot;, &quot;70&quot;, &quot;75&quot;, &quot;80&quot;, &quot;85&quot;, &quot;90&quot;, &quot;100&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;110&quot;, &quot;120&quot;, &quot;130&quot;, &quot;140&quot;, &quot;150&quot;, &quot;160&quot;, &quot;180&quot;, &quot;200&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(2.25597318603832E-14,0,0)&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Length (M14)&quot; : Distance</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Description: Set the bolt length</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Allowed values: [&quot;60&quot;, &quot;65&quot;, &quot;70&quot;, &quot;75&quot;, &quot;80&quot;, &quot;90&quot;, &quot;100&quot;, &quot;110&quot;, &quot;120&quot;, </span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&quot;130&quot;, &quot;140&quot;, &quot;150&quot;, &quot;160&quot;]</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;100&quot;</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px">&#0160;</p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Property: &quot;Origin&quot; : NoUnits (Read Only)</span></p>
<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&#0160; Current value: &quot;(2.25597318603832E-14,0,0)&quot;</span></p></div>
<br />
<p>Many of the properties are actually 0, but have ended up being printed as a very small number... it would be simple enough to check these against a tolerance rather than trusting them to be understood as being zero.</p>
<p>OK, that&#39;s a good enough start for today. In the next post we&#39;ll address the need to capture dynamic properties from an inserted dynamic block and copy them across to another, already-inserted dynamic block. A bit like a &quot;property painter&quot; for dynamic blocks. (If you&#39;re thinking that this doesn&#39;t sound quite like what Fernando originally asked for, then you&#39;d be quite right. We exchanged a few emails, and I then opted for a &quot;property painter&quot; approach to address the problem.)</p>
<p>Thanks for the inspiration, Fernando and Philippe! :-)</p>
