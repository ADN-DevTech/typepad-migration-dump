---
layout: "post"
title: "Extending the AutoCAD default context menu"
date: "2012-06-13 02:33:08"
author: "Virupaksha Aithal"
categories:
  - ".NET"
  - "AutoCAD"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/extending-the-autocad-default-context-menu.html "
typepad_basename: "extending-the-autocad-default-context-menu"
typepad_status: "Publish"
---

<div>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></div>
<div>Previously I have posted an <a href="http://adndevblog.typepad.com/autocad/2012/05/object-specific-context-menu-using-net.html">article </a>on adding object specific context menu (that is, extending the context menu only when a particular entity type is selected.) Now, below code shows the procedure to extend the AutoCAD default context menu using API “AddDefaultContextMenuExtension”.</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<div style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="color: #2b91af; line-height: 140%;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;DefaultMenuTest&quot;</span><span style="line-height: 140%;">)]</span></div>
<div style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">public</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> DefaultMenuTest()</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">{</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">ContextMenuExtension</span><span style="line-height: 140%;"> contectMenu =</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ContextMenuExtension</span><span style="line-height: 140%;">();</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; contectMenu.Title = </span><span style="color: #a31515; line-height: 140%;">&quot;Default Menu Test&quot;</span><span style="line-height: 140%;">;</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;"> Item1 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Test1&quot;</span><span style="line-height: 140%;">);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; contectMenu.MenuItems.Add(Item1);</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;"> Item1a = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Sub Test1&quot;</span><span style="line-height: 140%;">);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Item1a.Click += </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">EventHandler</span><span style="line-height: 140%;">(Item1a_Click);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Item1.MenuItems.Add(Item1a);</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;"> Item2 = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">MenuItem</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Test2&quot;</span><span style="line-height: 140%;">);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Item2.Click += </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">EventHandler</span><span style="line-height: 140%;">(Test2_Click);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; contectMenu.MenuItems.Add(Item2);</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">//App Default menu</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.AddDefaultContextMenuExtension(contectMenu);</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="line-height: 140%;">}</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Item1a_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">{</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.ShowAlertDialog(</span><span style="color: #a31515; line-height: 140%;">&quot;Test1a clicked\n&quot;</span><span style="line-height: 140%;">);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">}</span></div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;">&#0160;</div>
<div style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> Test2_Click(</span><span style="color: blue; line-height: 140%;">object</span><span style="line-height: 140%;"> sender, </span><span style="color: #2b91af; line-height: 140%;">EventArgs</span><span style="line-height: 140%;"> e)</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">{</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.ShowAlertDialog(</span><span style="color: #a31515; line-height: 140%;">&quot;Test2 clicked\n&quot;</span><span style="line-height: 140%;">);</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">}</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></div>
<div style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></div>
</div>
