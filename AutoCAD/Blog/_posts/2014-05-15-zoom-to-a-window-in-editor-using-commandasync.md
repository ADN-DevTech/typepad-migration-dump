---
layout: "post"
title: "Zoom to a Window in Editor using CommandASync"
date: "2014-05-15 06:24:05"
author: "Madhukar Moogala"
categories:
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2014/05/zoom-to-a-window-in-editor-using-commandasync.html "
typepad_basename: "zoom-to-a-window-in-editor-using-commandasync"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>In this post, I will illustrate a sample based on AutoCAD 2015 API <q>Editor.CommandAsync</q>.</p>
<p>Problem: Can I zoom to particular window in an Editor as long as I press esc or cancel to quit?</p>
<p>Answer: Yes, with help of CommandAsync, you can achieve this task.</p>
<div style="font-family: Courier New; font-size: 9pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">#region</span><span style="line-height: 140%;"> &quot;send Zoom command by callback function&quot;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private </span><span style="color: blue; line-height: 140%;">static </span><span style="color: blue; line-height: 140%;">bool</span><span style="line-height: 140%;"> ZoomExit = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//declare the callback delegation</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">delegate </span><span style="color: blue; line-height: 140%;">void </span><span style="color: #2b91af; line-height: 140%;">Del</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">private </span><span style="color: blue; line-height: 140%;">static </span><span style="color: #2b91af; line-height: 140%;">Del</span><span style="line-height: 140%;"> _actionCompletedDelegate;</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Exit function，check if Zoom command is esc\cancelled</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static </span><span style="color: blue; line-height: 140%;">void</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">MdiActiveDocument_CommandCancelled(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender,</span><span style="color: #2b91af; line-height: 140%;">CommandEventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ZoomExit = </span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;TestZoom&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public </span><span style="color: blue; line-height: 140%;">static </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> TestZoom()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//esc event</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;doc.CommandCancelled += MdiActiveDocument_CommandCancelled;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// start Zoom command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.</span><span style="color: #2b91af; line-height: 140%;">CommandResult</span><span style="line-height: 140%;"> cmdResult1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;ed.CommandAsync(</span><span style="color: blue; line-height: 140%;">new </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[]{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;_.ZOOM&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.PauseToken, </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.PauseToken});</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// delegate callback function, wait for interaction ends </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;_actionCompletedDelegate = </span><span style="color: blue; line-height: 140%;">new </span><span style="color: #2b91af; line-height: 140%;">Del</span><span style="line-height: 140%;">(CreateZoomAsyncCallback);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;cmdResult1.OnCompleted(</span><span style="color: blue; line-height: 140%;">new </span><span style="color: #2b91af; line-height: 140%;">Action</span><span style="line-height: 140%;">(_actionCompletedDelegate));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;ZoomExit = </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// callback function </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">public </span><span style="color: blue; line-height: 140%;">static </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> CreateZoomAsyncCallback()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">var</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//if Zoom command is running </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (!ZoomExit)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// AutoCAD hands over to the callback function </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.</span><span style="color: #2b91af; line-height: 140%;">CommandResult</span><span style="line-height: 140%;"> cmdResult1 =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.CommandAsync(</span><span style="color: blue; line-height: 140%;">new </span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;">[]{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #a31515; line-height: 140%;">&quot;_.ZOOM&quot;</span><span style="line-height: 140%;">, </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.PauseToken, </span><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;">.PauseToken});</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// delegate callback function, wait for interaction ends </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _actionCompletedDelegate = </span><span style="color: blue; line-height: 140%;">new </span><span style="color: #2b91af; line-height: 140%;">Del</span><span style="line-height: 140%;">(CreateZoomAsyncCallback);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; cmdResult1.OnCompleted(</span><span style="color: blue; line-height: 140%;">new </span><span style="color: #2b91af; line-height: 140%;">Action</span><span style="line-height: 140%;">(_actionCompletedDelegate));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160; &#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;Zoom Exit&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">#endregion</span></p>
</div>
