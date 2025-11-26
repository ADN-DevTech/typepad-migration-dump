---
layout: "post"
title: "Displaying the attrib dialog during insert with AcEdCommand"
date: "2013-03-22 02:16:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/displaying-the-attrib-dialog-during-insert-with-acedcommand.html "
typepad_basename: "displaying-the-attrib-dialog-during-insert-with-acedcommand"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>How can I force the attribute dialog box to display for an _INSERT issued with acedCommand using ObjectARX?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>You need to use a global function in ObjectARX called acedInitDialog. This is parallel to how the initdia function in AutoLISP must be called prior to issuing the INSERT command.&#0160; </p>
<p>When dialog initialization occurs before acedCommand, both the &quot;insert&quot; and &quot;Enter Attributes&quot;, dialog boxes will display during command execution.Because the &quot;insert&quot; fields do not normally require modification, it is only desirable to display the attributes dialog for user input. In order to display the attribute dialog box instead of command line prompts, divide the command sequence into two parts so that the initialization call is made shortly before   <br />attribute values are solicited.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> testOut()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//set vars first</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">struct</span><span style="line-height: 140%;"> resbuf rb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rb.restype = RTSHORT;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; rb.resval.rint = 1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedSetVar(L</span><span style="line-height: 140%; color: #a31515;">&quot;ATTDIA&quot;</span><span style="line-height: 140%;">, &amp;rb);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedSetVar(L</span><span style="line-height: 140%; color: #a31515;">&quot;ATTREQ&quot;</span><span style="line-height: 140%;">, &amp;rb);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//block position</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ads_point pt1;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; pt1[X] = pt1[Y] = 4.0;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//call command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> rterr = acedCommand ( RTSTR, L</span><span style="line-height: 140%; color: #a31515;">&quot;_.insert&quot;</span><span style="line-height: 140%;">, RTSTR, L</span><span style="line-height: 140%; color: #a31515;">&quot;myBlock&quot;</span><span style="line-height: 140%;">, RTPOINT,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pt1, RTNONE );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// ask to display attribute dialog</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedInitDialog(Adesk::kTrue);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// continue the command</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; acedCommand(RTREAL, 1.0,&#0160; RTREAL, 1.0, RTREAL, 0.0, RTNONE);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
</div>
<pre><br /></pre>
