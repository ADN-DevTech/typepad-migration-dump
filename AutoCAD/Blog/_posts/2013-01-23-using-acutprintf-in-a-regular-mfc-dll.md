---
layout: "post"
title: "Using acutPrintf in a Regular MFC DLL"
date: "2013-01-23 09:34:46"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "MFC"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/using-acutprintf-in-a-regular-mfc-dll.html "
typepad_basename: "using-acutprintf-in-a-regular-mfc-dll"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>If you are using an MFC Regular DLL, you have to manage module state switching when calling acutPrintf(). You need to do something like this:</p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Courier New"><span><font color="#0000ff"><font style="font-size: 8pt">void</font></font></span><font style="font-size: 8pt" color="#000000"> arx_func() {</font></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><span><font style="font-size: 8pt" color="#008000">// Put AFX_MANAGE_STATE in its own scope</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160; { </font></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;&#160;&#160; AFX_MANAGE_STATE(AfxGetAppModuleState()); </font></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160;&#160;&#160; acutPrintf(L</font></font><font style="font-size: 8pt"><span><font color="#a31515">&quot;\nHi!&quot;</font></span><font color="#000000">);</font></font></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; } </font></font><span><font style="font-size: 8pt" color="#008000">// End of scope</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>    <p style="margin: 0px"><font face="Courier New"><font color="#000000"><font style="font-size: 8pt">&#160; </font></font><span><font style="font-size: 8pt" color="#008000">// More of your code</font></span></font></p>    <p style="margin: 0px"><font face="Courier New"><font style="font-size: 8pt" color="#000000">}</font></font></p> </div>
