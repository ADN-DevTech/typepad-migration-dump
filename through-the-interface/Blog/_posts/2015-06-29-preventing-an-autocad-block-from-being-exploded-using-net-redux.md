---
layout: "post"
title: "Preventing an AutoCAD block from being exploded using .NET (redux)"
date: "2015-06-29 17:07:01"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Blocks"
  - "Commands"
  - "Drawing structure"
  - "Object properties"
  - "Runtime"
original_url: "https://www.keanw.com/2015/06/preventing-an-autocad-block-from-being-exploded-using-net-redux.html "
typepad_basename: "preventing-an-autocad-block-from-being-exploded-using-net-redux"
typepad_status: "Publish"
---

<p>When I was a boy, I used to love going to play with toys at my grandmother’s house. My absolute favourite was <a href="http://www.batmobile.free.fr/English/Intro_267.htm" target="_blank">a die-cast Batmobile</a> made by Corgi in the UK. What I particularly liked about this toy was its hidden features: the cars apparently came with secret instructions, although these were nowhere to be seen by the time I started playing with it. The Batmobile had plastic flames that came out of the exhaust when the rear wheels turned and spring-loadable, vertical rocket launchers. The biggest surprise was when I discovered the cutting blade that popped out of the car’s nose when you pressed a button on the bonnet. Happy days!</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb084a6da4970d-pi" target="_blank"><img alt="Corgi 1960&#39;s Batmobile" border="0" height="185" src="/assets/image_849698.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Corgi 1960&#39;s Batmobile" width="390" /></a></p>
<p>Sometimes AutoCAD reminds me of that Batmobile: you work with it for years and only then find out about some “hidden in plain sight” feature that really surprises you. Although the sense of joy I remember as a child is typically replaced by mild annoyance at having to update old blog posts. ;-)</p>
<p>Back in the day I posted <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/preventing-an-a.html" target="_blank">a solution translated from ObjectARX</a> that shows how to stop blocks from being exploded. It works by making AutoCAD think it has already performed the explode operation by hooking into Database.BeginDeepClone and BeginDeepCloneTranslation. The approach works well, but it turns out that even at the time of writing there was a simpler way to achieve this.</p>
<p>Blocks have an “allow exploding” property that can be unset during their definition:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb084a6dac970d-pi" target="_blank"><img alt="Block definition dialog" border="0" height="229" src="/assets/image_645420.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin: 20px auto; display: block; padding-right: 0px; border-width: 0px;" title="Block definition dialog" width="394" /></a></p>
<p>This feature was added to the DWG format – and presumably to the block dialog – during the AutoCAD 2007 timeframe, although it was apparently originally implemented using XData for the version of AutoCAD R12 used for AutoSurf (a big thanks to Joel Petersen for pointing out both the property and its history).</p>
<p>Here’s some C# code that shows how the equivalent BlockTableRecord.Explodable property can be set on certain blocks (we’re going from selected block references, but we could also access the block table directly, of course):</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> BlockExplosion</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;ALEX&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> AllowExploding()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Ask the user to select block references to [un]lock</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> sf =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">SelectionFilter</span>(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>[1] {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">DxfCode</span>.Start, <span style="color: #a31515;">&quot;INSERT&quot;</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> psr = ed.GetSelection(sf);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (psr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Do we make the referenced blocks explodable or not?</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pko =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">new</span> <span style="color: #2b91af;">PromptKeywordOptions</span>(<span style="color: #a31515;">&quot;Allow exploding [Yes/No]&quot;</span>, <span style="color: #a31515;">&quot;Yes No&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pko.Keywords.Default = <span style="color: #a31515;">&quot;Yes&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> pr = ed.GetKeywords(pko);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (pr.Status != <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">bool</span> explodable = pr.StringResult == <span style="color: #a31515;">&quot;Yes&quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">int</span> changed = 0, unchanged = 0;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Start our transaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = doc.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Iterate through our various, selected block references</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> id <span style="color: blue;">in</span> psr.Value.GetObjectIds())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> br = tr.GetObject(id, <span style="color: #2b91af;">OpenMode</span>.ForRead) <span style="color: blue;">as</span> <span style="color: #2b91af;">BlockReference</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (br != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Get the referenced block definition</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> btr =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; (<span style="color: #2b91af;">BlockTableRecord</span>)tr.GetObject(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; br.BlockTableRecord, <span style="color: #2b91af;">OpenMode</span>.ForRead</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Set its Explodable property, if required</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (btr.Explodable != explodable)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.UpgradeOpen();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; btr.Explodable = explodable;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; changed++;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; unchanged++;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Commit the transaction</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #a31515;">&quot;\nMade {0} blocks {1}explodable ({2} left unchanged).&quot;</span>,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; changed, (explodable ? <span style="color: #a31515;">&quot;&quot;</span> : <span style="color: #a31515;">&quot;un&quot;</span>), unchanged</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; );</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
</div>
<p>&#0160;</p>
<p>When we run the ALEX command (for ALlow EXploding) and select some blocks, we can see that the EXPLODE command no longer works on them:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;">Command: <span style="color: #ff0000;">ALEX</span></p>
<p style="margin: 0px;">Select objects: 1 found</p>
<p style="margin: 0px;">Select objects: 1 found, 2 total</p>
<p style="margin: 0px;">Select objects:</p>
<p style="margin: 0px;">Allow exploding [Yes/No] &lt;Yes&gt;: <span style="color: #ff0000;">No</span></p>
<p style="margin: 0px;">Made 2 blocks unexplodable (0 left unchanged).</p>
<p style="margin: 0px;">Command: <span style="color: #ff0000;">EXPLODE</span></p>
<p style="margin: 0px;">1 found</p>
<p style="margin: 0px;">1 could not be exploded.</p>
</div>
<p>&#0160;</p>
<p>The ALEX command can also be used to make blocks explodable again, although there’s <a href="http://knowledge.autodesk.com/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/Block-cannot-be-exploded.html" target="_blank">a clearly documented approach</a> for users to do this, in case they need to (aside from using the BLOCK command to redefine the block while retaining its contents and its various other properties). If your application needs to stop blocks from being exploded in a less user-controllable manner, you may want to take a look into <a href="http://through-the-interface.typepad.com/through_the_interface/2008/08/preventing-an-a.html" target="_blank">the approach we mentioned earlier</a> that uses BeginDeepClone[Translation].</p>
<p>&#0160;</p>
<p><span style="color: #a5a5a5;">photo credit: &quot;</span><a href="https://commons.wikimedia.org/wiki/File:Corgi_1960%27s_Batmobile.jpg#/media/File:Corgi_1960%27s_Batmobile.jpg"><span style="color: #a5a5a5;">Corgi 1960&#39;s Batmobile</span></a><span style="color: #a5a5a5;">&quot; by </span><a href="//commons.wikimedia.org/wiki/User:DPdH" title="User:DPdH"><span style="color: #a5a5a5;">DPdH</span></a><span style="color: #a5a5a5;"> - <span class="int-own-work" lang="en">Own work</span>. Licensed under </span><a href="http://creativecommons.org/licenses/by-sa/3.0" title="Creative Commons Attribution-Share Alike 3.0"><span style="color: #a5a5a5;">CC BY-SA 3.0</span></a><span style="color: #a5a5a5;"> via </span><a href="https://commons.wikimedia.org/wiki/"><span style="color: #a5a5a5;">Wikimedia Commons</span></a></p>
