---
layout: "post"
title: "Cancel AutoCAD commands programmatically using ObjectARX"
date: "2012-12-19 01:04:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/cancel-autocad-commands-programmatically-using-objectarx.html "
typepad_basename: "cancel-autocad-commands-programmatically-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>How do I cancel out of a currently running AutoCAD command, such as MOVE. I would do some handling in an editor reactor&#39;s commandWillStart function, and in some cases, cancel the regular AutoCAD MOVE function.</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The AcEdCommandStack stores pointers to ARX objects and functions, but there is no access to the function pointers of native AutoCAD commands. However, you can perform a cancellation in an editor reactor callback.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> Adesk::Boolean acedPostCommand(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;">* ); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> MyEditorReactor::commandWillStart(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">char</span><span style="line-height: 140%;"> * pCmdStr) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">if</span><span style="line-height: 140%;"> ( strcmp(pCmdStr,</span><span style="line-height: 140%; color: #a31515;">&quot;MOVE&quot;</span><span style="line-height: 140%;"> ) == 0&#0160; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedPostCommand(</span><span style="line-height: 140%; color: #a31515;">&quot;CANCELCMD&quot;</span><span style="line-height: 140%;">);&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
