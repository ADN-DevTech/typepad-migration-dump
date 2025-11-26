---
layout: "post"
title: "Maximize or minimize AutoCAD window through automation?"
date: "2012-12-21 17:59:48"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/maximize-or-minimize-autocad-window-through-automation.html "
typepad_basename: "maximize-or-minimize-autocad-window-through-automation"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>AutoCAD application can be maximized or minimized through automation by utilizing the 'WindowState' property of the Application object as shown in the following sample code:</p>  <p><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#008000">'To minimize the application</font></font></span></p>  <p><span style="line-height: 11pt"></span><font face="Consolas"><span style="line-height: 11pt"><font color="#2b91af"><font style="font-size: 8pt">Application</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">.WindowState = acMin</font></span></font></p>  <div style="font-family: ; background: white">   <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#008000">'To maximize the application</font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#2b91af"><font style="font-size: 8pt">Application</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">.WindowState = acMax</font></span></font></p>    <p style="margin: 0px"><font face="Consolas"><font style="font-size: 8pt" color="#000000">&#160;</font></font></p>    <p style="margin: 0px"><span style="line-height: 11pt"><font face="Consolas"><font style="font-size: 8pt" color="#008000">'The window is normal (neither minimized nor maximized).</font></font></span></p>    <p style="margin: 0px"><font face="Consolas"><span style="line-height: 11pt"><font color="#2b91af"><font style="font-size: 8pt">Application</font></font></span><span style="line-height: 11pt"><font style="font-size: 8pt" color="#000000">.WindowState = acNorm</font></span></font></p> </div>
