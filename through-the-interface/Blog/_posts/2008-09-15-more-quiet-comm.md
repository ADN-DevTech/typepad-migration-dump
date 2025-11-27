---
layout: "post"
title: "More quiet command-calling: adding an inspection dimension inside AutoCAD using .NET"
date: "2008-09-15 07:04:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Commands"
  - "Dimensions"
  - "Runtime"
original_url: "https://www.keanw.com/2008/09/more-quiet-comm.html "
typepad_basename: "more-quiet-comm"
typepad_status: "Publish"
---

<p>I'm still a little frazzled after transcribing the 18,000 word <a href="http://through-the-interface.typepad.com/through_the_interface/2008/09/an-interview-wi.html">interview with John Walker</a> (and largely with two fingers - at such times the fact that I've never learned to touch-type is a significant cause of frustration, as you might imagine). I'm also attending meetings all this coming week, so I've gone for the cheap option, once again, of dipping into my magic folder of code generated and provided by my team.</p>

<p><em>The technique for this one came from a response sent out by Philippe Leefsma, from DevTech EMEA, but he did mention a colleague helped him by suggesting the technique. So thanks to Philippe and our anonymous colleague for the initial idea of calling -DIMINSPECT.</em></p>

<p>The problem solved by Philippe's code is that no API is exposed via .NET to enable the &quot;inspection&quot; capability of dimensions inside AutoCAD. The protocol is there in ObjectARX, in the AcDbDimension class, but this has - at the time of writing - not been exposed via&nbsp; the managed API. One option would be to create a wrapper or mixed-mode module to call through to unmanaged C++, but the following approach simply calls through to the -DIMINSPECT command (the command-line version of DIMINSPECT) to set the inspection parameters.</p>

<p>I've integrated - and extended - the technique shown in <a href="http://through-the-interface.typepad.com/through_the_interface/2008/09/no-muttering-at.html">this previous post</a> to send the command as quietly as possible. One problem I realised with the previous implementation is that UNDO might leave the user in a problematic state - with the NOMUTT variable set to 1. This modified approach does a couple of things differently:</p>

<ul><li>Rather than using the NOMUTT command to set the system variable, it launches another custom command, FINISH_COMMAND <ul><li>This command has been registered with the NoUndoMarker command-flag, to make it invisible to the undo mechanism (well, at least in terms of automatic undo) </li>

<li>It sets the NOMUTT variable to 0 </li>

<li>It should be possible to share this command across others that have the need to call commands quietly</li></ul></li>

<li>It uses the COM API to create an undo group around the operations we want to consider one &quot;command&quot; </li>

<li>The implementation related to the &quot;quiet command calling&quot; technique is wrapped up in a code region to make it easy to hide</li></ul>

<p>One remaining - and, so far, unavoidable - piece of noise on the command-line is due to our use of the (handent) function: it echoes entity name of the selected dimension.</p>

<p>Here's the C# code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.ApplicationServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.DatabaseServices;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.EditorInput;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Runtime;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> Autodesk.AutoCAD.Interop;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">namespace</span><span style="LINE-HEIGHT: 140%"> InspectDimension</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">class</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Commands</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;INSP&quot;</span></span><span style="LINE-HEIGHT: 140%">)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> InspectDim()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Database</span></span><span style="LINE-HEIGHT: 140%"> db = doc.Database;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Editor</span></span><span style="LINE-HEIGHT: 140%"> ed = doc.Editor;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span></span><span style="LINE-HEIGHT: 140%"> peo =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">new</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityOptions</span></span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nSelect a dimension: &quot;</span></span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;peo.SetRejectMessage(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\nEntity must be a dimension.&quot;</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;peo.AddAllowedClass(</span><span style="COLOR: blue; LINE-HEIGHT: 140%">typeof</span><span style="LINE-HEIGHT: 140%">(</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Dimension</span></span><span style="LINE-HEIGHT: 140%">), </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptEntityResult</span></span><span style="LINE-HEIGHT: 140%"> per = ed.GetEntity(peo);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (per.Status != </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">PromptStatus</span></span><span style="LINE-HEIGHT: 140%">.OK)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">return</span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Transaction</span></span><span style="LINE-HEIGHT: 140%"> tr =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; db.TransactionManager.StartTransaction();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">using</span><span style="LINE-HEIGHT: 140%"> (tr)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;{</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Dimension</span></span><span style="LINE-HEIGHT: 140%"> dim =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; tr.GetObject(per.ObjectId, </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">OpenMode</span></span><span style="LINE-HEIGHT: 140%">.ForRead)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">as</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Dimension</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">if</span><span style="LINE-HEIGHT: 140%"> (dim != </span><span style="COLOR: blue; LINE-HEIGHT: 140%">null</span><span style="LINE-HEIGHT: 140%">)</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> shape = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;Round&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> label = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;myLabel&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> rate = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;100%&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> cmd =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;-DIMINSPECT Add (handent \&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%"> +</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;dim.Handle + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\&quot;&quot;</span></span><span style="LINE-HEIGHT: 140%"> + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;) \n&quot;</span></span><span style="LINE-HEIGHT: 140%"> +</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;shape + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&quot;</span></span><span style="LINE-HEIGHT: 140%"> + label + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&quot;</span></span><span style="LINE-HEIGHT: 140%"> +</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;rate + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;\n&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; SendQuietCommand(doc, cmd);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; };</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; tr.Commit();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;}</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &nbsp; #region</span><span style="LINE-HEIGHT: 140%"> QuietCommandCalling</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">const</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> kFinishCmd = </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;FINISH_COMMAND&quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">private</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> SendQuietCommand(</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc,</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">string</span><span style="LINE-HEIGHT: 140%"> cmd</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; )</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Get the old value of NOMUTT</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">object</span><span style="LINE-HEIGHT: 140%"> nomutt =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.GetSystemVariable(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;NOMUTT&quot;</span></span><span style="LINE-HEIGHT: 140%">);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Add the string to reset NOMUTT afterwards</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AcadDocument</span></span><span style="LINE-HEIGHT: 140%"> oDoc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AcadDocument</span></span><span style="LINE-HEIGHT: 140%">)doc.AcadDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;oDoc.StartUndoMark();</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;cmd += </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;_&quot;</span></span><span style="LINE-HEIGHT: 140%"> + kFinishCmd + </span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot; &quot;</span></span><span style="LINE-HEIGHT: 140%">;</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="COLOR: green; LINE-HEIGHT: 140%">// Set NOMUTT to 1, reducing cmd-line noise</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.SetSystemVariable(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;NOMUTT&quot;</span></span><span style="LINE-HEIGHT: 140%">, 1);</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;doc.SendStringToExecute(cmd, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">true</span><span style="LINE-HEIGHT: 140%">, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">, </span><span style="COLOR: blue; LINE-HEIGHT: 140%">false</span><span style="LINE-HEIGHT: 140%">);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; [</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandMethod</span></span><span style="LINE-HEIGHT: 140%">(kFinishCmd, </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">CommandFlags</span></span><span style="LINE-HEIGHT: 140%">.NoUndoMarker)]</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; </span><span style="COLOR: blue; LINE-HEIGHT: 140%">static</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">public</span><span style="LINE-HEIGHT: 140%">&nbsp;</span><span style="COLOR: blue; LINE-HEIGHT: 140%">void</span><span style="LINE-HEIGHT: 140%"> FinishCommand()</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; {</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Document</span></span><span style="LINE-HEIGHT: 140%"> doc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; </span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.DocumentManager.MdiActiveDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AcadDocument</span></span><span style="LINE-HEIGHT: 140%"> oDoc =</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; (</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">AcadDocument</span></span><span style="LINE-HEIGHT: 140%">)doc.AcadDocument;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;oDoc.EndUndoMark();</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"> </p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp;&nbsp; &nbsp;</span><span style="color: #2b91af;"><span style="COLOR: #2b91af; LINE-HEIGHT: 140%">Application</span></span><span style="LINE-HEIGHT: 140%">.SetSystemVariable(</span><span style="color: #a31515;"><span style="COLOR: #a31515; LINE-HEIGHT: 140%">&quot;NOMUTT&quot;</span></span><span style="LINE-HEIGHT: 140%">, 0);</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; &nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue; LINE-HEIGHT: 140%">&nbsp; &nbsp; #endregion</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">&nbsp; }</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">}</span></p></div><p>Here are the results of running the INSP command and selecting a dimension object. First the command-line output:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Command: <span style="color: #ff0000;">INSP</span></span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Select a dimension: &lt;Entity name: -401f99f0&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="LINE-HEIGHT: 140%">Command:</span></p></div><p>And now the graphics, before and after calling INSP and selecting the dimension:</p>

<p><a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/Before%20-DIMINSPECT.png"><img height="240" alt="Before -DIMINSPECT" src="/assets/Before%20-DIMINSPECT_thumb.png" width="200" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> <a href="http://through-the-interface.typepad.com/through_the_interface/WindowsLiveWriter/After%20-DIMINSPECT.png"><img height="240" alt="After -DIMINSPECT" src="/assets/After%20-DIMINSPECT_thumb.png" width="200" border="0" style="BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px" /></a> </p>

<p>In the end the post didn't turn out to be quite as quick to write as expected, but anyway - so it goes. I'm now halfway from Zürich to Washington, D.C., and had the time to kill. I probably won't have the luxury when I'm preparing my post for the middle of the week, unless I end up suffering from jet-lag. :-)</p>
