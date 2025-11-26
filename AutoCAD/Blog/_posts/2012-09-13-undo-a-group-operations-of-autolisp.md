---
layout: "post"
title: "Undo a group operations of AutoLISP"
date: "2012-09-13 03:51:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/undo-a-group-operations-of-autolisp.html "
typepad_basename: "undo-a-group-operations-of-autolisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue      <br /></strong>My menu item uses AutoLISP code to call two AutoCAD commands.&#0160; Why does the UNDO command only undo the last of the two commands?</p>
<p><strong>Solution      <br /></strong>If all you are executing with a menu item is series of multiple commands as in:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">[Draw Many]^c^cLINE 0,0,0 10,10,0 <span style="background-color: #e6e6e6; color: #800080;">;CIRCLE 5,5 5;</span></span></p>
<p>Then when you type U at the command line, both items will be undone.However, if you are doing the following via a menu item and AutoLISP code:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="background-color: #e6e6e6; color: #800080;">       <br /></span></span></p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span> testCmd <span style="color: #ff0000;">(</span><span style="color: #ff0000;">)</span>       <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;LINE&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;0,0,0&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;10,10,0&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;&quot;</span><span style="color: #ff0000;">)</span>       <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;CIRCLE&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;5,5&quot;</span>&#0160;<span style="color: #008000;">5</span><span style="color: #ff0000;">)</span>       <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span><span style="color: #ff0000;">)</span>       <br /><span style="color: #ff0000;">)</span></span><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">&#0160;</span></span></p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">[Draw Many w<span style="color: #0000ff;">/</span>Lisp]^c^c<span style="color: #ff0000;">(</span>testCmd<span style="color: #ff0000;">)</span><span style="background-color: #e6e6e6; color: #800080;">;</span></span></p>
<p>The U only erases the circle.</p>
<p>The solution is to code your AutoLISP function like so:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;"><span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span> testCmd <span style="color: #ff0000;">(</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;UNDO&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;BE&quot;</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;LINE&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;0,0,0&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;10,10,0&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;&quot;</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;CIRCLE&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;5,5&quot;</span>&#0160;<span style="color: #008000;">5</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;UNDO&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;END&quot;</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span><span style="color: #ff0000;">)</span>      <br /><span style="color: #ff0000;">)</span></span></p>
