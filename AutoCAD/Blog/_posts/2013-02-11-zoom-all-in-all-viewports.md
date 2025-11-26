---
layout: "post"
title: "zoom all in all viewports"
date: "2013-02-11 01:48:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/zoom-all-in-all-viewports.html "
typepad_basename: "zoom-all-in-all-viewports"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue      <br /></strong>How can I do a &quot;zoom all&quot; in all viewports?</p>
<p><a name="section2"></a></p>
<p><strong>Solution      <br /></strong>You can easily do this by using the CVPORT system variable:</p>
<p>Type: Integer    <br />Saved in: Drawing     <br />Initial value: 2</p>
<p>Sets the identification number of the current viewport. You can change this value, thereby changing the current viewport, if the following conditions are met:</p>
<p>-- The identification number you specify is that of an active viewport.    <br />-- A command in progress has not locked cursor movement to that viewport.     <br />-- Tablet mode is off.</p>
<p>The number of current viewports can be determined using the AutoLISP function (vports) that returns something similar to:</p>
<p>((4 (0.0 0.5) (0.5 1.0)) (5 (0.0 0.0) (0.5 0.5)) (2 (0.5 0.0) (1.0 0.5)) (3 (0.5 0.5) (1.0 1.0)))</p>
<p>(setq nv (length (vports))) will give you the number of vports. If you have four vports, they are numbered 2 to 5.</p>
<p>An AutoLISP routine to zoom in for all vports can look like this:</p>
<p><span style="line-height: 120%; font-family: &#39;courier new&#39;, courier; font-size: 8pt;">     <br /><span style="color: #ff0000;">(</span><span style="color: #0000ff;">defun</span> c:zoomall <span style="color: #ff0000;">(</span>&#0160;<span style="color: #0000ff;">/</span> i nv<span style="color: #ff0000;">)</span>      <br />      <br />&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> i <span style="color: #008000;">1</span><span style="color: #ff0000;">)</span>&#0160;&#0160; <span style="background-color: #e6e6e6; color: #800080;">;initialise counter       <br /></span>&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> nv <span style="color: #ff0000;">(</span>length <span style="color: #ff0000;">(</span>vports<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160;&#0160;&#0160; <span style="background-color: #e6e6e6; color: #800080;">;get number of vports       <br /></span>      <br />&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">repeat</span> nv      <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">setq</span> i <span style="color: #ff0000;">(</span><span style="color: #0000ff;">1+</span> i<span style="color: #ff0000;">)</span><span style="color: #ff0000;">)</span>&#0160; <span style="background-color: #e6e6e6; color: #800080;">;start counting vports at 2       <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">setvar</span>&#0160;<span style="color: #ff00ff;">&quot;CVPORT&quot;</span> i<span style="color: #ff0000;">)</span>&#0160;&#0160;&#0160;&#0160; <span style="background-color: #e6e6e6; color: #800080;">;set vport       <br /></span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">command</span>&#0160;<span style="color: #ff00ff;">&quot;_zoom&quot;</span>&#0160;<span style="color: #ff00ff;">&quot;all&quot;</span><span style="color: #ff0000;">)</span>&#0160;&#0160;&#0160;&#0160; <span style="background-color: #e6e6e6; color: #800080;">;zoom all       <br /></span>&#0160;&#0160;&#0160; <span style="color: #ff0000;">)</span>      <br />      <br />&#0160;&#0160; <span style="color: #ff0000;">(</span><span style="color: #0000ff;">princ</span><span style="color: #ff0000;">)</span>      <br />&#0160;&#0160; <span style="color: #ff0000;">)</span></span></p>
<p>This works for tilemode on or off but if you define vports within vports, this will only &quot;zoom all&quot; for the vports within the current one.</p>
<p>If you want to do different operation for different viewports, examine the information in the list returned by (vports). This is from the AutoCAD help file:</p>
<p>Each viewport descriptor is a list consisting of the viewport identification number and the coordinates of the viewport&#39;s lower-left and upper-right corners. If the AutoCAD system variable TILEMODE is set to 1 (on), the returned list describes the viewport configuration created with the AutoCAD VPORTS command. The corners of the viewports are expressed in values between 0.0 and 1.0, with (0.0, 0.0) representing the lower-left corner of the display screen&#39;s graphics area, and (1.0, 1.0) the upper-right corner. If TILEMODE is 0 (off), the returned list describes the viewport objects created with the MVIEW command. The viewport object corners are expressed in paper space coordinates. Viewport number 1 is always paper space when TILEMODE is off.</p>
<p>For example, given a single-viewport configuration with TILEMODE on, the vports function might return this:</p>
<p>((1 (0.0 0.0) (1.0 1.0)))</p>
<p>Similarly, given four equal-sized viewports located in the four corners of the screen when TILEMODE is on, the vports function might return this:</p>
<p>( (5 (0.5 0.0) (1.0 0.5))   <br />(2 (0.5 0.5) (1.0 1.0))    <br />(3 (0.0 0.5) (0.5 1.0))</p>
<p>(4 (0.0 0.0) (0.5 0.5)) )</p>
<p>The current viewport&#39;s descriptor is always first in the list. In the previous example, viewport number 5 is the current viewport.</p>
