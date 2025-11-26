---
layout: "post"
title: "Cannot remove .NET event handler"
date: "2012-07-17 06:34:08"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/cannot-remove-net-event-handler.html "
typepad_basename: "cannot-remove-net-event-handler"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>When my AddIn is loaded it starts listening to the DocumentToBeDestroyed event and it works fine.</p>
<p>However, when later on I try to stop listening to this event I do not succeed and so my handler keeps getting called.</p>
<p>Here is my code:</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Commands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> Initialize() </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span><span style="line-height: 140%;">.Initialize</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">AddHandler</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.DocumentToBeDestroyed, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> docBeginDocClose</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> Terminate() </span><span style="color: blue; line-height: 140%;">Implements</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">IExtensionApplication</span><span style="line-height: 140%;">.Terminate</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> docBeginDocClose( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> senderObj </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> docColDocActEvtArgs </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DocumentCollectionEventArgs</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; System.Diagnostics.</span><span style="color: #2b91af; line-height: 140%;">Debug</span><span style="line-height: 140%;">.Print(</span><span style="color: #a31515; line-height: 140%;">&quot;in docBeginDocClose&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &lt;</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;StopEvent&quot;</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> StopEvents()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">RemoveHandler</span><span style="line-height: 140%;"> _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.DocumentToBeDestroyed, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; &#0160; </span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> docBeginDocClose</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">Catch</span><span style="line-height: 140%;"> ex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Exception</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; MsgBox(</span><span style="color: #a31515; line-height: 140%;">&quot;Error: &quot;</span><span style="line-height: 140%;"> &amp; ex.Message)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Try</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Class</span></p>
</div>
<p><strong>Solution</strong></p>
<p>AutoCAD creates an instance of the class that contains the Initialize() function. If the command function is an instance function (not static/shared), then AutoCAD also creates a new instance of the class that contains that function for each document. If the handler is an instance function as well, then you&#39;ll get the instance of it that belongs to the same class instance as the command handler.</p>
<p>So you attach one instance’s function to the event inside Initialize() or a command, then you try to remove another instance’s function later on – so you do not remove the function that you attached to the event in the first place.</p>
<p>That’s why your handler keeps being called even after you tried to remove it.</p>
<p>Based on the above, the solution is to simply make the function static (C#) / Shared (VB.NET):</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Shared</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> docBeginDocClose( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> senderObj </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> docColDocActEvtArgs </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">DocumentCollectionEventArgs</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; System.Diagnostics.</span><span style="color: #2b91af; line-height: 140%;">Debug</span><span style="line-height: 140%;">.Print(</span><span style="color: #a31515; line-height: 140%;">&quot;in docBeginDocClose&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
