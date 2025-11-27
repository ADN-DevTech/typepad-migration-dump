---
layout: "post"
title: "Disabling AutoCAD&rsquo;s complete UI using .NET"
date: "2016-02-17 08:38:56"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "User interface"
original_url: "https://www.keanw.com/2016/02/disabling-autocads-complete-ui-using-net.html "
typepad_basename: "disabling-autocads-complete-ui-using-net"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2016/02/disabling-the-autocad-ribbon-using-net.html" target="_blank">the first post in this series</a>, we saw how to disable AutoCAD’s ribbon. In <a href="http://through-the-interface.typepad.com/through_the_interface/2016/02/disabling-autocads-toolbars-using-net.html" target="_blank">the second post</a>, we saw how to make (with some caveats) AutoCAD’s toolbars disappear. In this post we’re going to throw all that away and show how to get better results with a single line of code. &lt;sigh&gt;</p>
<p>But before all that, a big “thanks” to both James Meading and Alexander Rivilis, who have helped us get to this point.</p>
<p>James <a href="http://through-the-interface.typepad.com/through_the_interface/2016/02/disabling-autocads-toolbars-using-net.html#comment-2516655549" target="_blank">pointed out a fairly significant flaw</a> in yesterday’s toolbar-hiding code (hence the mention of caveats, above), in that it didn’t place toolbars on multiple rows at exactly the same position when they’re made visible, once again. I started to work around this by making toolbars visible in the order of their “top” index: this seemed to work well enough in my cursory tests, but I didn’t see a little strangeness that I was planning on dealing with today.</p>
<p>But, overnight, Alexander <a href="http://forums.autodesk.com/t5/net/toolbar-enable-disable-via-net/m-p/6042264#M47543" target="_blank">provided some MFC code</a> that disables AutoCAD&#39;s UI at a more basic level: disabling the complete AutoCAD frame with all of its user interface elements, avoiding us having to hide anything.</p>
<p>I took Alexander’s code and found a simple way to make it work from C# using P/Invoke. And that’s the code we’re going to see today:</p>
<div style="font-size: 8pt; font-family: courier new; background: white; color: black; line-height: 140%;">
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Runtime.InteropServices;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> UserInterfaceManipulation</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&#0160; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: #2b91af;">Commands</span></p>
<p style="margin: 0px;">&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">DllImport</span>(<span style="color: #a31515;">&quot;user32.dll&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">static</span> <span style="color: blue;">extern</span> <span style="color: blue;">bool</span> EnableWindow(<span style="color: #2b91af;">IntPtr</span> hWnd, <span style="color: blue;">bool</span> bEnable);</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;DU&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> DisableUICommand()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableUI(<span style="color: blue;">false</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; [<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;EU&quot;</span>)]</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> EnableUICommand()</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableUI(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">public</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> EnableUI(<span style="color: blue;">bool</span> enable)</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; {</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160;&#0160;&#0160; EnableWindow(<span style="color: #2b91af;">Application</span>.MainWindow.Handle, enable);</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; }</p>
<p style="margin: 0px;">&#0160; }</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Here are the updated DU and EU commands in action, disabling and enabling the AutoCAD UI, respectively.</p>
<p style="text-align: center;"><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb08bb715c970d-pi"><img alt="Disabling the full UI" height="479" src="/assets/image_344754.jpg" style="margin: 30px 0px; display: inline;" title="Disabling the full UI" width="500" /></a></p>
<p>You’ll notice that disabling leaves the toolbars in their place and doesn’t grey them out, but that hovering over them shows that they’re properly disabled. While disabled, the ribbon is greyed out in much the way we saw at the beginning of the week.</p>
