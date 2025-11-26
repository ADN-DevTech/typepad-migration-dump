---
layout: "post"
title: "Allowing users to escape from long operations in AutoCAD .NET"
date: "2012-07-05 15:13:58"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/allowing-users-to-escape-from-long-operations-in-autocad-net.html "
typepad_basename: "allowing-users-to-escape-from-long-operations-in-autocad-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html">Stephen Preston</a></p>
<p>In a long operation, you may want to give your user the option to hit escape to cancel the operation and regain control. Here is some code from a DevNote originally written for AutoCAD 2007-2009, which still works today.</p>
<div style="font-family: consolas; background: white; color: black; font-size: 14pt;">
<p style="margin: 0px;"><span style="font-size: x-small;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;loop&quot;</span>)]</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;"><span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> Loop()</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">{</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: #2b91af;">DocumentCollection</span> dm = </span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.DocumentManager;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: #2b91af;">Editor</span> ed = dm.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: green;">// Create and add our message filter</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: #2b91af;">MyMessageFilter</span> filter = <span style="color: blue;">new</span> <span style="color: #2b91af;">MyMessageFilter</span>();</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.AddMessageFilter(filter);</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: green;">// Start the loop</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: blue;">while</span> (<span style="color: blue;">true</span>)</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: green;">// Check for user input events</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.DoEvents();</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: green;">// Check whether the filter has set the flag</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (filter.bCanceled == <span style="color: blue;">true</span>)</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nLoop cancelled.&quot;</span>);</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">break</span>;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\nInside while loop...&quot;</span>);</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: green;">// We&#39;re done - remove the message filter</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.RemoveMessageFilter(filter);</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">}</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;</span></p>
<p style="margin: 0px;"><span style="color: green;"><span style="font-size: x-small;">// Our message filter class</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;"><span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">MyMessageFilter</span> : <span style="color: #2b91af;">IMessageFilter</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">{</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">const</span> <span style="color: blue;">int</span> WM_KEYDOWN = 0x0100;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> bCanceled = <span style="color: blue;">false</span>;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> PreFilterMessage(<span style="color: blue;">ref</span> <span style="color: #2b91af;">Message</span> m)</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; {</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m.Msg == WM_KEYDOWN)</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Check for the Escape keypress</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">Keys</span> kc = (<span style="color: #2b91af;">Keys</span>)(<span style="color: blue;">int</span>)m.WParam &amp; <span style="color: #2b91af;">Keys</span>.KeyCode;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (m.Msg == WM_KEYDOWN &amp;&amp; kc == <span style="color: #2b91af;">Keys</span>.Escape)</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; bCanceled = <span style="color: blue;">true</span>;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: green;">// Return true to filter all keypresses</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">true</span>;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: green;">// Return false to let other messages through</span></span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;&#0160;&#0160; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160; }</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">}</span></p>
<p style="margin: 0px;"><span style="font-size: x-small;">&#0160;</span></p>
</div>
<p>Update 7/30/12:</p>
<p>And here is the VB.NET translation (mostly through automatic translation using <a href="http://www.developerfusion.com/tools/convert/csharp-to-vb/" target="_blank">DeveloperFusion</a>):</p>
<p>&#0160;</p>
<div style="font-family: Consolas; font-size: 14pt; color: black; background: white;">
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160;&#0160;&#0160; &lt;<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;LOOP&quot;</span>)&gt;</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Shared</span> <span style="color: blue;">Sub</span> qqq()</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> dm <span style="color: blue;">As</span> <span style="color: #2b91af;">DocumentCollection</span> =</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; Autodesk.AutoCAD.ApplicationServices.<span style="color: #2b91af;">Application</span>.DocumentManager</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> ed <span style="color: blue;">As</span> <span style="color: #2b91af;">Editor</span> = dm.MdiActiveDocument.Editor</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Create and add our message filter</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> filter <span style="color: blue;">As</span> <span style="color: blue;">New</span> <span style="color: #2b91af;">MyMessageFilter</span>()</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.AddMessageFilter(filter)</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; Start the loop</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">While</span> <span style="color: blue;">True</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Check for user input events</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.DoEvents()</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Check whether the filter has set the flag</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> filter.bCanceled = <span style="color: blue;">True</span> <span style="color: blue;">Then</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbLf &amp; <span style="color: #a31515;">&quot;Loop cancelled.&quot;</span>)</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Exit While</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; ed.WriteMessage(vbLf &amp; <span style="color: #a31515;">&quot;Inside while loop...&quot;</span>)</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">While</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: green;">&#39; We&#39;re done - remove the message filter</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; System.Windows.Forms.<span style="color: #2b91af;">Application</span>.RemoveMessageFilter(filter)</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Sub</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; <span style="color: green;">&#39; Our message filter class</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Class</span> <span style="color: #2b91af;">MyMessageFilter</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Implements</span> <span style="color: #2b91af;">IMessageFilter</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Const</span> WM_KEYDOWN <span style="color: blue;">As</span> <span style="color: blue;">Integer</span> = &amp;H100</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Public</span> bCanceled <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> = <span style="color: blue;">False</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160;</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Public</span> <span style="color: blue;">Function</span> PreFilterMessage1(</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">ByRef</span> m <span style="color: blue;">As</span> System.Windows.Forms.<span style="color: #2b91af;">Message</span>) <span style="color: blue;">As</span> <span style="color: blue;">Boolean</span> _</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">Implements</span> System.Windows.Forms.<span style="color: #2b91af;">IMessageFilter</span>.PreFilterMessage</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> m.Msg = WM_KEYDOWN <span style="color: blue;">Then</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Check for the Escape keypress</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Dim</span> kc <span style="color: blue;">As</span> <span style="color: #2b91af;">Keys</span> = <span style="color: blue;">DirectCast</span>(<span style="color: blue;">CInt</span>(m.WParam), <span style="color: #2b91af;">Keys</span>) <span style="color: blue;">And</span> <span style="color: #2b91af;">Keys</span>.KeyCode</span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">If</span> m.Msg = WM_KEYDOWN <span style="color: blue;">AndAlso</span> kc = <span style="color: #2b91af;">Keys</span>.Escape <span style="color: blue;">Then</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; bCanceled = <span style="color: blue;">True</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Return true to filter all keypresses</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Return</span> <span style="color: blue;">True</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">If</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: green;">&#39; Return false to let other messages through</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; <span style="color: blue;">Return</span> <span style="color: blue;">False</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Function</span></span></p>
<p style="margin: 0px;"><span style="font-size: 10pt;">&#0160; &#0160; <span style="color: blue;">End</span> <span style="color: blue;">Class</span></span></p>
</div>
