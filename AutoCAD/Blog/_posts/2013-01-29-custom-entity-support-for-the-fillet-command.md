---
layout: "post"
title: "Custom entity support for the fillet command"
date: "2013-01-29 23:48:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/custom-entity-support-for-the-fillet-command.html "
typepad_basename: "custom-entity-support-for-the-fillet-command"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p> Assume a custom entity (MyLine) derives from AcDbCurve and draws lines in subWorlddraw. An ARX command creates two MyLine.&#0160; When you run FILLET&#0160; command, it will pop out    <br /><em>Fillet requires 2 lines, arcs, or circles.</em></p>
<p>The problem is that when the FILLET command is invoked, the entire implementation is bypassed and not used.</p>
<p>The workaround is to derive native AutoCAD entities    <br />that are based on AcDbCurve. For example, you can derive from AcDbLine so your custom line will automatically support the FILLET command.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40064d52970c-pi"><img alt="image" border="0" height="249" src="/assets/image_219655.jpg" style="display: inline; border-width: 0px;" title="image" width="449" /></a></p>
<p>&#0160;</p>
<p>There is one more issue. After FILLET command, the two custom lines are filleted. However the lines are not trimmed. This used to work in 2002.</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40064d82970c-pi"><img alt="image" border="0" height="350" src="/assets/image_217081.jpg" style="display: inline; border-width: 0px;" title="image" width="437" /></a> </p>
<p>The workaround is custom line classes can implement their own extend() method to override the one in AcDbInternalLine and get AutoCAD’s behavior&#0160; </p>
<p>MyLine.h:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="line-height: 140%; color: blue;">virtual</span><span style="line-height: 140%;"> Acad::ErrorStatus extend(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> newParam); </span></p>
</div>
<p>MyLine.cpp:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus CMyLine::extend(</span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> param) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assertWriteEnabled(); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcGePoint3d startPnt(startPoint()), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; endPnt(endPoint()); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">double</span><span style="line-height: 140%;"> norm = startPnt.distanceTo(endPnt); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcGeLine3d geLine (startPnt, endPnt); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcGePointOnCurve3d ponc(geLine); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> (param &gt; 0.0) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setEndPoint(ponc.point(param / norm)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">else</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; setStartPoint(ponc.point(param / norm)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">return</span><span style="line-height: 140%;"> Acad::eOk; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
</div>
By this method, the result is as expected:
<p>&#0160;</p>
<p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d40064db8970c-pi"><img alt="image" border="0" height="313" src="/assets/image_589936.jpg" style="display: inline; border-width: 0px;" title="image" width="470" /></a></p>
