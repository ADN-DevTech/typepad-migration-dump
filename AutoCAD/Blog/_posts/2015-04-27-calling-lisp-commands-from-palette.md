---
layout: "post"
title: "Calling Lisp commands from Palette"
date: "2015-04-27 10:29:06"
author: "Balaji"
categories:
  - ".NET"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/04/calling-lisp-commands-from-palette.html "
typepad_basename: "calling-lisp-commands-from-palette"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>If you want to invoke Lisp commands from .Net code, the Application.Invoke should help. This should work ok for Lisp commands that do not use (command-s ...) to invoke other AutoCAD commands.&nbsp;</p>
<p>As an example, consider this Lisp command to create two circles.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> (defun c:mycircles1()</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(setq ms (vla-get-ModelSpace </pre>
<pre style="margin: 0em;"> 	(vla-get-ActiveDocument (vlax-get-acad-object))))</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;">     (setq myCircle1</pre>
<pre style="margin: 0em;"> 		(progn </pre>
<pre style="margin: 0em;"> 			(setq ctrPt &lt;span'(0.0 0.0 0.0))</pre>
<pre style="margin: 0em;"> 			(setq radius 5.0)</pre>
<pre style="margin: 0em;"> 			(vla-addCircle ms </pre>
<pre style="margin: 0em;"> 			(vlax-3d-point ctrPt) radius)</pre>
<pre style="margin: 0em;"> 		)</pre>
<pre style="margin: 0em;">     )</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(setq myCircle2</pre>
<pre style="margin: 0em;"> 		(progn </pre>
<pre style="margin: 0em;"> 			(setq ctrPt &lt;span'(10.0 0.0 0.0))</pre>
<pre style="margin: 0em;"> 			(setq radius 5.0)</pre>
<pre style="margin: 0em;"> 			(vla-addCircle ms </pre>
<pre style="margin: 0em;"> 			(vlax-3d-point ctrPt) radius)</pre>
<pre style="margin: 0em;"> 		)</pre>
<pre style="margin: 0em;">     )</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(princ <span style="color: #a31515;">"SUCCESS !"</span><span style="color: #000000;"> )</span></pre>
<pre style="margin: 0em;"> )</pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->
<p>This can be invoked from a button click handler in your palette using the following code snippet :</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  <span style="color:#0000ff">void</span><span style="color:#000000">  DrawCirclesBtn1_Click(object sender, EventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DocumentCollection acDocMgr </pre>
<pre style="margin:0em;"> 		= Autodesk.AutoCAD.ApplicationServices</pre>
<pre style="margin:0em;"> 		.Application.DocumentManager;</pre>
<pre style="margin:0em;">     Document acDoc = acDocMgr.MdiActiveDocument;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#0000ff">using</span><span style="color:#000000">  (DocumentLock dl = acDoc.LockDocument())</pre>
<pre style="margin:0em;">     <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">         Editor ed = acDoc.Editor;</pre>
<pre style="margin:0em;">         <span style="color:#0000ff">using</span><span style="color:#000000">  (ResultBuffer rb = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer())</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue((<span style="color:#0000ff">int</span><span style="color:#000000"> )LispDataType.Text, </pre>
<pre style="margin:0em;"> 				<span style="color:#a31515">&quot;c:mycircles1&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">             ResultBuffer rbRes = Autodesk.AutoCAD.</pre>
<pre style="margin:0em;"> 			ApplicationServices.Application.Invoke(rb);</pre>
<pre style="margin:0em;">             <span style="color:#0000ff">if</span><span style="color:#000000">  (rbRes != null)</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 TypedValue[] tvalues = rbRes.AsArray();</pre>
<pre style="margin:0em;">                 foreach (TypedValue tv in tvalues)</pre>
<pre style="margin:0em;">                     ed.WriteMessage(<span style="color:#a31515">&quot;\\n&quot;</span><span style="color:#000000">  + tv.ToString());</pre>
<pre style="margin:0em;">                 rbRes.Dispose();</pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                 ed.WriteMessage(<span style="color:#a31515">&quot;\\n </span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 				Result buffer is null.<span style="color:#a31515">&quot;);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">     <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
<p>But, consider this Lisp command that relies on using AutoCAD's native CIRCLE command to create the two circles.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color: black; overflow: auto; width: 99.5%;">
<pre style="margin: 0em;"> (defun c:mycircles2()</pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(command-s <span style="color: #a31515;">"CIRCLE"</span><span style="color: #000000;">  <span style="color: #a31515;">"0,0,0"</span><span style="color: #000000;">  5.0)</span></span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(command-s <span style="color: #a31515;">"CIRCLE"</span><span style="color: #000000;">  <span style="color: #a31515;">"10,0,0"</span><span style="color: #000000;">  5.0)</span></span></pre>
<pre style="margin: 0em;">&nbsp;</pre>
<pre style="margin: 0em;"> 	(princ <span style="color: #a31515;">"SUCCESS !"</span><span style="color: #000000;"> )</span></pre>
<pre style="margin: 0em;"> )</pre>
<pre style="margin: 0em;">&nbsp;</pre>
</div>
<!-- End block -->
<p>To invoke this version of the Lisp command from .Net, it becomes important to use the right context. As the .Net code for the button click event handler runs in session context, it is not appropriate for invoking Lisp commands that need to run in document context. Here is a version of the button click event handler to invoke the Lisp command using the command context. In earlier releases prior to 2016, you will need to use SendStringToExecute. In AutoCAD 2016, it is simpler to use the "ExecuteInCommandContextAsync" as shown in the below code snippet.</p>
<!-- Start block. Created with Code4Blog for Microsoft Visual Studio 2010. Copyright (c)2010 Vitaly Zayko http://zayko.net -->
<div style="color:black;overflow:auto;width:99.5%;">
<pre style="margin:0em;"> <span style="color:#0000ff">private</span><span style="color:#000000">  async <span style="color:#0000ff">void</span><span style="color:#000000">  DrawCirclesBtn2_Click(</pre>
<pre style="margin:0em;"> 					object sender, </pre>
<pre style="margin:0em;"> 					EventArgs e)</pre>
<pre style="margin:0em;"> <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">     DocumentCollection docManager </pre>
<pre style="margin:0em;"> 		= Autodesk.AutoCAD.ApplicationServices.</pre>
<pre style="margin:0em;"> 		Application.DocumentManager;</pre>
<pre style="margin:0em;">     Document doc = docManager.MdiActiveDocument;</pre>
<pre style="margin:0em;">     Editor ed = doc.Editor;</pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Option 1 : </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">//acDoc.SendStringToExecute(&quot;(c:mycircles2) &quot;,</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> 	<span style="color:#008000">//	true, false, false);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// Option 2 : </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     <span style="color:#008000">// run the command in command context. Works in 2016+</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">     await docManager.ExecuteInCommandContextAsync(</pre>
<pre style="margin:0em;">         async (obj) =&gt;</pre>
<pre style="margin:0em;">         <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">             <span style="color:#0000ff">using</span><span style="color:#000000">  (ResultBuffer rb = <span style="color:#0000ff">new</span><span style="color:#000000">  ResultBuffer())</pre>
<pre style="margin:0em;">             <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                 rb.Add(<span style="color:#0000ff">new</span><span style="color:#000000">  TypedValue((<span style="color:#0000ff">int</span><span style="color:#000000"> )LispDataType.Text,</pre>
<pre style="margin:0em;"> 					<span style="color:#a31515">&quot;c:mycircles2&quot;</span><span style="color:#000000"> ));</pre>
<pre style="margin:0em;">                 ResultBuffer rbRes = Autodesk.AutoCAD.</pre>
<pre style="margin:0em;"> 			ApplicationServices.Application.Invoke(rb);</pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">if</span><span style="color:#000000">  (rbRes != null)</pre>
<pre style="margin:0em;">                 <span style="color:#000000">{</span></pre>
<pre style="margin:0em;">                     TypedValue[] tvalues = rbRes.AsArray();</pre>
<pre style="margin:0em;">                     foreach (TypedValue tv in tvalues)</pre>
<pre style="margin:0em;">                       ed.WriteMessage(<span style="color:#a31515">&quot;\\n&quot;</span><span style="color:#000000">  + tv.ToString());</pre>
<pre style="margin:0em;">                     rbRes.Dispose();</pre>
<pre style="margin:0em;">                 <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">                 <span style="color:#0000ff">else</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     ed.WriteMessage(<span style="color:#a31515">&quot;\\n </span><span style="color:#000000"> </pre>
<pre style="margin:0em;">                     Result buffer is null.<span style="color:#a31515">&quot;);</span><span style="color:#000000"> </pre>
<pre style="margin:0em;">             <span style="color:#000000">}</span></pre>
<pre style="margin:0em;">         <span style="color:#000000">}</span>,</pre>
<pre style="margin:0em;">         null</pre>
<pre style="margin:0em;">     );</pre>
<pre style="margin:0em;"> <span style="color:#000000">}</span></pre>
<pre style="margin:0em;"> </pre>
</div>
<!-- End block -->
