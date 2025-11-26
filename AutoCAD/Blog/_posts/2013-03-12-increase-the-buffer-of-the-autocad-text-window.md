---
layout: "post"
title: "increase the buffer of the AutoCAD text window"
date: "2013-03-12 02:52:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/increase-the-buffer-of-the-autocad-text-window.html "
typepad_basename: "increase-the-buffer-of-the-autocad-text-window"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>To begin with, I’d recommend the other post on Kean’s blog</p>
<h5><a href="http://through-the-interface.typepad.com/through_the_interface/2011/08/increasing-the-size-of-autocads-command-line-history.html">Increasing the size of AutoCAD’s command line history</a></h5>
<p>you will know how to increase the size of command line history manually. At API side, there are two ways to access the Text Window buffer size, expressed in numbers of history lines: the    <br />CmdHistLines environment variable, and the AutoCAD.AcadPreferencesDisplay.HistoryLines Automation property.</p>
<p>Here is how to set the environment variable from AutoLISP:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span>setenv <span style="color: #ff00ff;">&quot;CmdHistLines&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;2048&quot;</span><span style="color: #ff0000;">)</span></span></p>
Here is how to set the Automation property from AutoLISP:
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">vl-load-com</span><span style="color: #ff0000;">)</span>      <br /><span style="color: #ff0000;">(</span><span style="color: #0000ff;">vla-put-historylines</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">vla-get-display</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">vla-get-preferences</span>      <br /><span style="color: #ff0000;">(</span><span style="color: #0000ff;">vlax-get-acad-object</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<span style="color: #008000;">2048</span><span style="color: #ff0000;">)</span></span></p>
Note: The value has to be between 25 and 2048.
