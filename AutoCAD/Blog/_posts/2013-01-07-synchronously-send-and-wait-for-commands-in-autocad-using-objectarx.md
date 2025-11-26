---
layout: "post"
title: "Synchronously Send (and wait for) commands in AutoCAD using ObjectARX"
date: "2013-01-07 11:40:43"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/synchronously-send-and-wait-for-commands-in-autocad-using-objectarx.html "
typepad_basename: "synchronously-send-and-wait-for-commands-in-autocad-using-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>Leading on from this <a href="http://adndevblog.typepad.com/autocad/2012/04/synchronously-send-and-wait-for-commands-in-autocad-using-c-net.html">post</a>, I thought you guys should at least know how to do the same thing in ObjectARX… </p>  <p><b>Issue</b></p>  <p>How can I permit a user to draw a multi-point polyline from ObjectARX? I tried to use the acedCommand to send the '_pline' command to AutoCAD, but it's impossible to predetermine how many polyline points a user will decide to enter.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>A function can be implemented that first issues the PLINE command and then continually checks if PLINE is still active in the AutoCAD command buffer. If so, command pauses are sent to the command line to allow further user input.    <br />This activity can happen repetitively until the user escapes or presses the Enter key to complete the polyline.    <br />The following code segment does this:</p>  <pre><br />Adesk::Boolean isPlineActive() <br />{&#160; <br />&#160;&#160;&#160; struct resbuf rb;&#160; <br />&#160;&#160;&#160; acedGetVar(_T(&quot;CMDNAMES&quot;),&amp;rb);<br />&#160;&#160;&#160; if(rb.restype == RTSTR &amp;&amp; rb.resval.rstring != NULL)&#160; <br />&#160;&#160;&#160; {&#160;&#160; <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; //&quot;PLINE&quot; contained in CMDNAMES?<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; if(_tcsstr(rb.resval.rstring,_T(&quot;PLINE&quot;))) <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; return Adesk::kTrue;&#160; <br />&#160;&#160;&#160; }&#160; <br />&#160;&#160;&#160; return Adesk::kFalse;&#160; <br />}&#160; 
<p>void mkline() <br />{<br />&#160;&#160;&#160; acedCommand(RTSTR,_T(&quot;_.pline&quot;),RTSTR,PAUSE,RTNONE);&#160; <br />&#160;&#160;&#160; while(isPlineActive())<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; acedCommand(RTSTR,PAUSE,RTNONE);&#160;&#160; <br />&#160;&#160;&#160; acutPrintf (_T(&quot;\nContinue processing&quot;)); <br />}</p></pre>
