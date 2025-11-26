---
layout: "post"
title: "Calling a VBA macro from ARX and getting data back"
date: "2013-01-07 14:42:37"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/calling-a-vba-macro-from-arx-and-getting-data-back.html "
typepad_basename: "calling-a-vba-macro-from-arx-and-getting-data-back"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>How can I call a VBA macro from ARX and get data back? After I input some data into the dialog box, which now is in VBA environment, how can I get this data back to my ARX project?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>If you have a dialog box built in VBA, and want your ObjectARX project to run the dialog box, and then retrieve the data that has been input into the dialog   <br />box so that you can continue your .ARX project, you can call a VBA macro (named &quot;test&quot; for example) from an ARX application using ads_command() :</p>  <pre style="font-family: ; background: white; color: ; line-height: normal"><font face="Consolas"><span style="color: "><font color="#010001"><font style="font-size: 9.8pt"></font></font></span></font></pre>

<pre style="font-family: ; background: white; color: ; line-height: normal"><font face="Consolas"><span style="color: "><font color="#010001"><font style="font-size: 9.8pt">acedCommand</font></font></span><font style="font-size: 9.8pt"><font color="#000000"> (</font><span style="color: "><font color="#010001">RTSTR</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">_T</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;_-vbarun&quot;</font></span><font color="#000000">), </font><span style="color: "><font color="#010001">RTSTR</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">_T</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;test&quot;</font></span><font color="#000000">), </font><span style="color: "><font color="#010001">RTNONE</font></span><font color="#000000">);
</font></font></font></pre>

<p>It is not possible to return values from VBA to ARX, but you can work around this by using the system variables USERS1 ... USERS5 for strings and USERR1 ... USERR5 for doubles to exchange data between C++ and VBA. </p>

<p>When working with VBA you can use the SetVariable method to set the system variables values.</p>

<div style="font-family: ; background: white; color: ">
  <p style="margin: 0px"><font face="Consolas"><span style="color: "><font color="#010001"><font style="font-size: 8pt">Public</font></font></span><font style="font-size: 8pt"><font color="#000000"> </font><span style="color: "><font color="#010001">Sub</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">test</font></span><font color="#000000">()</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">Dim</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">sysVarName</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">As</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">String</font></span><font color="#000000">&#160;&#160;&#160; </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">Dim</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">sysVarData</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">As</font></span><font color="#000000"> </font></font><span style="color: "><font style="font-size: 8pt" color="#010001">Variant</font></span></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">Dim</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">dataType</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">As</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">Integer</font></span><font color="#000000">&#160;&#160;&#160;&#160; </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">Dim</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">dataDouble</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">As</font></span><font color="#000000"> </font></font><span style="color: "><font style="font-size: 8pt" color="#010001">Double</font></span></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">sysVarName</font></span><font color="#000000"> = </font></font><span style="color: "><font style="font-size: 8pt" color="#a31515">&quot;USERR1&quot;</font></span></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">dataDouble</font></span><font color="#000000"> = 3</font></font><span style="color: "><font style="font-size: 8pt" color="#0000ff">#</font></span></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">sysVarData</font></span><font color="#000000"> = </font><span style="color: "><font color="#010001">dataDouble</font></span><font color="#000000"> </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">Call</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">ThisDrawing</font></span><font color="#000000">.</font><span style="color: "><font color="#010001">SetVariable</font></span><font color="#000000">(</font><span style="color: "><font color="#010001">sysVarName</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">sysVarData</font></span><font color="#000000">) </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><span style="color: "><font color="#010001"><font style="font-size: 8pt">End</font></font></span><font style="font-size: 8pt"><font color="#000000"> </font></font><span style="color: "><font style="font-size: 8pt" color="#010001">Sub</font></span></font></p>
</div>

<p>Your C++ code should contain something like:</p>

<div style="font-family: ; background: white; color: ">
  <p style="margin: 0px"><font face="Consolas"><span style="color: "><font color="#0000ff"><font style="font-size: 8pt">int</font></font></span><font style="font-size: 8pt"><font color="#000000"> </font><span style="color: "><font color="#010001">adstest</font></span><font color="#000000">()</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">{ </font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">acedCommand</font></span><font color="#000000"> (</font><span style="color: "><font color="#010001">RTSTR</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">_T</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;_-vbarun&quot;</font></span><font color="#000000">), </font><span style="color: "><font color="#010001">RTSTR</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">_T</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;test&quot;</font></span><font color="#000000">), </font><span style="color: "><font color="#010001">RTNONE</font></span><font color="#000000">);</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#0000ff">struct</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">resbuf</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">result</font></span><font color="#000000">;</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#0000ff">if</font></span><font color="#000000"> (</font><span style="color: "><font color="#010001">acedGetVar</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;Userr1&quot;</font></span><font color="#000000">, &amp;</font><span style="color: "><font color="#010001">result</font></span><font color="#000000">) != </font><span style="color: "><font color="#010001">RTNORM</font></span><font color="#000000">)&#160;&#160;&#160; </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160; {</font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">acutPrintf</font></span><font color="#000000">(</font><span style="color: "><font color="#a31515">&quot;\nError getting USERR1&quot;</font></span><font color="#000000">);</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#0000ff">return</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">RTNORM</font></span><font color="#000000">;&#160;&#160;&#160;&#160;&#160; </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160; }</font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#010001">acutPrintf</font></span><font color="#000000"> (</font><span style="color: "><font color="#a31515">&quot;Userr1 = %f\n&quot;</font></span><font color="#000000">, </font><span style="color: "><font color="#010001">result</font></span><font color="#000000">.</font><span style="color: "><font color="#010001">resval</font></span><font color="#000000">.</font><span style="color: "><font color="#010001">rreal</font></span><font color="#000000">);</font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><font style="font-size: 8pt"><span style="color: "><font color="#0000ff">return</font></span><font color="#000000"> </font><span style="color: "><font color="#010001">RTNORM</font></span><font color="#000000">; </font></font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">}</font></font></p>

  <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>
</div>
