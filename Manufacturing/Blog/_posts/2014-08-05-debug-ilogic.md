---
layout: "post"
title: "Debug iLogic code"
date: "2014-08-05 17:38:21"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "iLogic"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/08/debug-ilogic.html "
typepad_basename: "debug-ilogic"
typepad_status: "Publish"
---

<p>By&#0160;<a href="http://adndevblog.typepad.com/manufacturing/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Unfortunately, in <strong>iLogic</strong> you do not have the same features you get in case of <strong>VBA</strong> or <strong>Visual Studio</strong> projects. Stepping through the code, checking variable values, setting up breakpoints are all missing from the <strong>iLogic</strong> environment.</p>
<p>If the <strong>iLogic</strong> functionality is not enough and you also need to access the underlying <strong>Inventor</strong> <strong>API</strong>, then I think the easiest way is to test things out in <strong>VBA</strong>. There you can also get familiar with the object model by examining the properties of existing objects: <a href="http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html" target="_self" title="">http://adndevblog.typepad.com/manufacturing/2013/10/discover-object-model.html</a></p>
<p>One way to debug <strong>iLogic</strong> code could be using&#0160;<strong>Trace</strong> calls, which can write out information about where the code is at a given moment and what the values of certain variables are. This was mentioned on the forum: <a href="https://forums.autodesk.com/t5/Inventor-General-Discussion/iLogic-Developing-and-debugging/m-p/2850580" target="_self" title="">https://forums.autodesk.com/t5/Inventor-General-Discussion/iLogic-Developing-and-debugging/m-p/2850580</a>&#0160;&#0160;</p>
<p>I used one of my test documents (<span class="asset  asset-generic at-xid-6a0167607c2431970b01a3fd41260b970b img-responsive"><a href="http://adndevblog.typepad.com/files/ilogictestdebug.ipt">Download ILogicTestDebug</a>)</span>&#0160;and added a couple more rules to it so that I can see which rule is called after which other rule, etc. Just placed <strong>Trace</strong> calls like these inside the rules:<strong><br /></strong></p>
<pre>Trace.WriteLine(&quot;iLogic: &#39;Update&#39; rule start&quot;)
Trace.WriteLine(&quot;iLogic: &#39;Width&#39; = &quot; + currentWidth.ToString())</pre>
<p><strong>Note:</strong>&#0160;you might need to add&#0160;<strong>Imports System.Diagnostics&#0160;</strong>at the beginning of the rule&#0160;</p>
<p>Once I downloaded the <a href="http://technet.microsoft.com/en-us/sysinternals/bb896647" target="_self">DebugView</a> application I could see all my messages in there, along with messages coming from other applications:</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd412773970b-pi" style="display: inline;"><img alt="Debugview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd412773970b image-full img-responsive" src="/assets/image_741d16.jpg" title="Debugview" /></a></p>
<p>&#0160;</p>
