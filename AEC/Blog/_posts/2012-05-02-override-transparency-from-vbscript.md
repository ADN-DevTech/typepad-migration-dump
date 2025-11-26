---
layout: "post"
title: "Override transparency from VBScript"
date: "2012-05-02 03:59:02"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/05/override-transparency-from-vbscript.html "
typepad_basename: "override-transparency-from-vbscript"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>If you have an nwf document with a saved selection set and you want to override the transparency for the objects that belong to that, and finally save the document to nwd format, then you can use the following code which is based on the AUTO_08.vbs sample.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">option explicit</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">roamer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">attrib</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">ndx</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">arg_in</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">arg_out</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">arg_title</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">arg_password </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">flags</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">arg_expiry</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">expiry</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">count</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">selsets</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">current</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">dim </span><span style="line-height: 140%;">i</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">count=WScript.Arguments.Count</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">arg_in=WScript.Arguments(0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">arg_out=WScript.Arguments(1)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">arg_title=WScript.Arguments(2)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">arg_password=WScript.Arguments(3)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">arg_expiry=WScript.Arguments(4)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">expiry=CDate(arg_expiry)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">'create roamer via automation</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">set</span><span style="line-height: 140%;"> roamer=createobject(</span><span style="line-height: 140%; color: #a31515;">"navisWorks.document"</span><span style="line-height: 140%;">)&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">'open input file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">roamer.openfile arg_in</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">'create publishing attribute</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ndx=roamer.state.getenum(</span><span style="line-height: 140%; color: #a31515;">"eObjectType_nwOaPublishAttribute"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">set</span><span style="line-height: 140%;"> attrib=roamer.state.objectFactory(ndx)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">'set publishing properties</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">attrib.title=arg_title</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">attrib.password=arg_password</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">attrib.expirydate=expiry</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">flags=attrib.flags</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ndx=roamer.state.getenum(</span><span style="line-height: 140%; color: #a31515;">"ePublishFlag_DISPLAY_ON_OPEN"</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">flags=flags or ndx</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">attrib.flags=flags</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">' get selection sets</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">set</span><span style="line-height: 140%;"> selsets = roamer.state.SelectionSets()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">count = selsets.Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">for</span><span style="line-height: 140%;"> i = 1 </span><span style="line-height: 140%; color: blue;">to</span><span style="line-height: 140%;"> count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&nbsp; set</span><span style="line-height: 140%;"> current = selsets.Item(i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #a31515;">' check the name&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> current.name = </span><span style="line-height: 140%; color: #a31515;">"MySelSet"</span><span style="line-height: 140%;"> then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #a31515;">'override transparency</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; roamer.state.overridetransparency current.selection, .5</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">&nbsp; end if</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">next</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: #a31515;">'write output file</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">roamer.publishfile arg_out,attrib</span></p>
</div>
