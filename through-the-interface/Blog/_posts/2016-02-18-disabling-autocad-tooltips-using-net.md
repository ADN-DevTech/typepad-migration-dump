---
layout: "post"
title: "Disabling AutoCAD tooltips using .NET"
date: "2016-02-18 18:05:45"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2016/02/disabling-autocad-tooltips-using-net.html "
typepad_basename: "disabling-autocad-tooltips-using-net"
typepad_status: "Publish"
---

<p>I wasn’t planning on posting to this blog again, this week – three times per week is enough, I find, so I tend to save any leftovers for the week after – but <a href="http://through-the-interface.typepad.com/through_the_interface/2016/02/disabling-autocads-complete-ui-using-net.html#comment-2519939800" target="_blank">this question James Maeding asked</a> is very much related to this week’s posts, and – in any case – I already have three fun posts planned for next week. :-)</p>
<p>Here’s the question James asked, again:</p>
<blockquote>
<p><em>wish I had something to make tooltips disappear when needed. I like them on for many things, but then they get in the way sometimes, and you have to cancel all you are doing to turn them off. If only I could toggle them transparently with some command...maybe its easy I just have not thought about it too much.</em></p>
</blockquote>
<p>It turned out to be pretty easy. Based on the approach I used – way back when – for <a href="http://through-the-interface.typepad.com/through_the_interface/2011/07/automatic-translation-of-autocad-tooltips-using-net.html" target="_blank">automatic tooltip translation</a>, I went and added an event handler that makes tooltips invisible as soon as they’re shown. One interesting point is that I needed to make them visible again as they were closing (not that we see that). Otherwise they won’t come back – in the same session – once we re-enable them by removing the event handler.</p>
<p>Here’s the C# code defining the DTT and ETT commands, which disable and enable tooltips, respectively.</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.Windows;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> UserInterfaceManipulation</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DTT&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.Transparent)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> DisableToolTips()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableTooltips(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;ETT&quot;</span>, <span style="color: #2b91af;">CommandFlags</span>.Transparent)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> EnableToolTips()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableTooltips(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> EnableTooltips(<span style="color: blue;">bool</span> enable)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (enable)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentManager</span>.ToolTipOpened -= OnToolTipOpened;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentManager</span>.ToolTipClosed -= OnToolTipClosed;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">else</span></p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentManager</span>.ToolTipOpened += OnToolTipOpened;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #2b91af;">ComponentManager</span>.ToolTipClosed += OnToolTipClosed;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> OnToolTipOpened(<span style="color: blue;">object</span> sender, System.<span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> tt = sender <span style="color: blue;">as</span> System.Windows.Controls.<span style="color: #2b91af;">ToolTip</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (tt != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tt.Visibility = System.Windows.<span style="color: #2b91af;">Visibility</span>.Hidden;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">private</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> OnToolTipClosed(<span style="color: blue;">object</span> sender, System.<span style="color: #2b91af;">EventArgs</span> e)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">var</span> tt = sender <span style="color: blue;">as</span> System.Windows.Controls.<span style="color: #2b91af;">ToolTip</span>;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: blue;">if</span> (tt != <span style="color: blue;">null</span>)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; tt.Visibility = System.Windows.<span style="color: #2b91af;">Visibility</span>.Visible;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here are the commands in action:</p>
<p>&#0160;</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201b8d1a16448970c-pi" target="_blank"><img alt="Disabling and re-enabling tooltips" height="479" src="/assets/image_530110.jpg" style="float: none; margin: 30px auto; display: block;" title="Disabling and re-enabling tooltips" width="500" /></a></p>
