---
layout: "post"
title: "Turn on/off the running osnap"
date: "2012-05-31 18:47:16"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/turn-onoff-the-running-osnap.html "
typepad_basename: "turn-onoff-the-running-osnap"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>To turn on or off the running osnap, simply set the SNAPMODE system variable to 1 or 0.</p>
<p>Here is a sample code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Turn-off running osnap</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;SNAPMODE&quot;</span><span style="line-height: 140%;">, 0);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Turn-on running osnap</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.SetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;SNAPMODE&quot;</span><span style="line-height: 140%;">, 1);</span></p>
</div>
<p></p>
<p>The various osnap options are governed by the value of the OSMODE system variable. Here is a <a href="http://adndevblog.typepad.com/autocad/2012/05/modify-osmode-mode-in-net.html">post</a> that describes setting of the OSMODE system variable. The values that the OSMODE system variable can take can be found in the <a href="http://docs.autodesk.com/ACD/2013/ENU/index.html?url=files/GUID-DD9B3216-A533-4D47-95D8-7585F738FD75.htm,topicNumber=d30e439454">AutoCAD documentation.</a></p>
<p>Prior to AutoCAD 2009, If object snap (Osnap) is turned off, AutoCAD adds 16384, to the current setting for the variable OSMODE. This allows applications to identify the current setting for OSMODE.</p>
<p>For instance, when you activate the "endpoint" object snap and Osnap is enabled, OSMODE contains 1 as its value. If object snap is disabled, OSMODE will contain 16385 as its value (1 + 16384).</p>
<p>Here is a sample code for this :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Int16</span><span style="line-height: 140%;"> osmode = (</span><span style="color: #2b91af; line-height: 140%;">Int16</span><span style="line-height: 140%;">)</span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.GetSystemVariable(</span><span style="color: #a31515; line-height: 140%;">&quot;OSMODE&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> msg = </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Empty;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> ((osmode &amp; 16384) == 0)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; msg = </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;OSnaps are Disabled. Value of OSMODE = {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; osmode</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; msg = </span><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;">.Format</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; (</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: #a31515; line-height: 140%;">&quot;OSnaps are Enabled. Value of OSMODE = {0}&quot;</span><span style="line-height: 140%;">,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; osmode</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; );</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
