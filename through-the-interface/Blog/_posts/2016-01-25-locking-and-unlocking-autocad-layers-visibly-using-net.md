---
layout: "post"
title: "Locking and unlocking AutoCAD layers visibly using .NET"
date: "2016-01-25 15:11:32"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Drawing structure"
  - "Graphics system"
original_url: "https://www.keanw.com/2016/01/locking-and-unlocking-autocad-layers-visibly-using-net.html "
typepad_basename: "locking-and-unlocking-autocad-layers-visibly-using-net"
typepad_status: "Publish"
---

<p>On Friday I visited Munich Airport: not only to catch a flight home to Switzerland but also to take a look at some AutoCAD issues frustrating the developers working on the airport’s facilities management system. During the course of the day we were able to work through a number of problems – such as using COM (SendCommand()) to run a script synchronously as well as to get the fill pattern for MPolygon objects (this is apparently broken in .NET) – but there was one I had to bring home with me: when locking and unlocking layers programmatically, their geometry doesn’t fade and unfade despite LAYLOCKFADECTL being set correctly.</p>
<p>I found a few instances of this reported on the web, but I didn’t come across a solution. After some work I found that if you set the IsOff property (or even IsFrozen, although I didn’t actually try this) on the layer that’s being locked/unlocked, then it’s enough to trigger the layer change that can be picked up by Editor.ApplyCurDwgLayerTableChanges() (and then Editor.Regen()).</p>
<p>So yes, we’re <a href="https://youtu.be/nn2FB1P_Mn8" target="_blank">turning it off and on again</a>. Sort of: we don’t actually need to toggle IsOff – we can just set it to the current property value.</p>
<p>Here’s the C# code implementing our LL (LockLayers) and UL (UnlockLayers) commands:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.DatabaseServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> LayerManipulation</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Extensions</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> LockOrUnlockAllLayers(</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">this</span> <span style="color: #2b91af;">Document</span> doc, <span style="color: blue;">bool</span> dolock, <span style="color: blue;">bool</span> lockZero = <span style="color: blue;">false</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; )</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> db = doc.Database;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">using</span> (<span style="color: blue;">var</span> tr = db.TransactionManager.StartTransaction())</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> lt = (<span style="color: #2b91af;">LayerTable</span>)tr.GetObject(db.LayerTableId, <span style="color: #2b91af;">OpenMode</span>.ForRead);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: blue;">var</span> ltrId <span style="color: blue;">in</span> lt)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Don&#39;t try to lock/unlock either the current layer or layer 0</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// (depending on whether lockZero == true for the latter)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (ltrId != db.Clayer &amp;&amp; (lockZero || ltrId != db.LayerZero))</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Open the layer for write and lock/unlock it</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> ltr = (<span style="color: #2b91af;">LayerTableRecord</span>)tr.GetObject(ltrId, <span style="color: #2b91af;">OpenMode</span>.ForWrite);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.IsLocked = dolock;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ltr.IsOff = ltr.IsOff; <span style="color: green;">// This is needed to force a graphics update</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tr.Commit();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// These two calls will result in the layer&#39;s geometry fading/unfading</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// appropriately</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.ApplyCurDwgLayerTableChanges();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.Regen();</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;LL&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> LockLayers()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.LockOrUnlockAllLayers(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;UL&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">void</span> UnlockLayers()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> doc = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (doc == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; doc.LockOrUnlockAllLayers(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here we see the two commands in action:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1966f13970c-pi" target="_blank"><img alt="Locking and unlocking layers" height="377" src="/assets/image_36743.jpg" style="float: none; margin: 30px auto; display: block;" title="Locking and unlocking layers" width="500" /></a></p>
