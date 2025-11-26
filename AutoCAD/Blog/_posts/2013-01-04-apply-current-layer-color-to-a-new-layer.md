---
layout: "post"
title: "Apply current layer color to a new layer"
date: "2013-01-04 03:55:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/apply-current-layer-color-to-a-new-layer.html "
typepad_basename: "apply-current-layer-color-to-a-new-layer"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>&#0160;</strong></p>
<p><strong>Issue</strong></p>
<p>Using Lisp, is there a way that I can get the current color from the current layer?&#0160;&#0160; I want to apply the color from the current layer to a new layer, with no user input.&#0160; For example: </p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span>SETQ OLDLAYER <span style="color: #ff0000;">(</span>GETVAR <span style="color: #ff00ff;">&quot;CLAYER&quot;</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<span style="background-color: #e6e6e6; color: #800080;">;Gets the current layer TEST1&#0160; <br /></span>      <br /><span style="color: #ff0000;">(</span>COMMAND <span style="color: #ff00ff;">&quot;LAYER&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;N&quot;</span> TEST2 <span style="color: #ff00ff;">&quot;C&quot;</span>&#0160;<span style="color: #ff0000;">)</span>       <br />      <br /></span></p>
<p><strong>Solution</strong></p>
<p>The following Lisp code should accomplish what you want:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> OLDLAYER <span style="color: #ff0000;">(</span><span style="color: #0000ff;">getvar</span>&#0160;<span style="color: #ff00ff;">&quot;CLAYER&quot;</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;<span style="background-color: #e6e6e6; color: #800080;">;Gets the current layer TEST1       <br /></span><span style="background-color: #e6e6e6; color: #800080;">;Gets the ENAME of the current layer TEST1       <br /></span><span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> OLDLAYERENT <span style="color: #ff0000;">(</span>tblobjname <span style="color: #ff00ff;">&quot;LAYER&quot;</span> OLDLAYER<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>      <br />&#0160; <br /><span style="background-color: #e6e6e6; color: #800080;">;Gets the color of the current layer TEST1       <br /></span><span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> OLDCOLOR <span style="color: #ff0000;">(</span><span style="color: #0000ff;">cdr</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">assoc</span>&#0160;<span style="color: #008000;">62</span>&#0160;<span style="color: #ff0000;">(</span><span style="color: #0000ff;">entget</span> OLDLAYERENT<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>      <br />&#0160; <br /><span style="background-color: #e6e6e6; color: #800080;">;Creates new layer TEST2 and assigns OLDCOLOR to it       <br /></span><span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;LAYER&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;N&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;TEST2&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;C&quot;</span> OLDCOLOR <span style="color: #ff00ff;">&quot;TEST2&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;&quot;</span><span style="color: #ff0000;">)</span></span></p>
