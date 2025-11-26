---
layout: "post"
title: "Set or Get Lisp Symbol in .NET"
date: "2014-08-05 05:07:40"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2015"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2014/08/set-or-get-lisp-symbol-in-net.html "
typepad_basename: "set-or-get-lisp-symbol-in-net"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>We are pretty much aware of pinvoke acedGetSym\acedPutSym in .NET to manipulate lisp symbols , we can avoid pinvoking with help of&#0160; SetLispSymbol and GetLispSymbol.</p>
<p>In the following example ,my attention is towards &quot;how to store \retireve multiple fragements of information in lisp symbol &quot; ,to achieve this we can use TypedValue object data type along with LispDataType enumerations as values.</p>
<p>&#0160;</p>
<div style="font-family: Consolas; font-size: 9pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> PutSymbol()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Document</span> d = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Database</span> db = d.Database;</p>
<p style="margin: 0px;"><span style="color: green;">// Create a list</span></p>
<p style="margin: 0px;"><span style="color: #2b91af;">TypedValue</span>[] tValue = <span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>[7];</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.ListBegin), 0);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.Text,</p>
<p style="margin: 0px;"><span style="color: #a31515;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&quot;Main List Item 1&quot;</span>), 1);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.ListBegin), 2);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.Text,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #a31515;">&quot;Nested List Item 1&quot;</span>), 3);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.Text,</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #a31515;">&quot;Nested List Item 2&quot;</span>), 4);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.ListEnd), 5);</p>
<p style="margin: 0px;">tValue.SetValue(<span style="color: blue;">new</span> <span style="color: #2b91af;">TypedValue</span>((<span style="color: blue;">int</span>)<span style="color: #2b91af;">LispDataType</span>.ListEnd), 6);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">d.SetLispSymbol(<span style="color: #a31515;">&quot;lst&quot;</span>, tValue);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> GetSymbol()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Document</span> d = <span style="color: #2b91af;">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Editor</span> ed = d.Editor;</p>
<p style="margin: 0px;"><span style="color: #2b91af;">PromptResult</span> res = ed.GetString(<span style="color: #a31515;">&quot;\nName of lisp-Symbol name: &quot;</span>);</p>
<p style="margin: 0px;"><span style="color: blue;">if</span> (res.Status == <span style="color: #2b91af;">PromptStatus</span>.OK)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: #2b91af;">TypedValue</span>[] tValue = (<span style="color: #2b91af;">TypedValue</span>[])d.GetLispSymbol(res.StringResult);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (tValue == <span style="color: blue;">null</span>) <span style="color: blue;">return</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">foreach</span> (<span style="color: #2b91af;">TypedValue</span> tV <span style="color: blue;">in</span> tValue)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(<span style="color: #a31515;">&quot;\n&quot;</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; ed.WriteMessage(tV.TypeCode + <span style="color: #a31515;">&quot;,&quot;</span> + tV.Value);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">}</p>
</div>
