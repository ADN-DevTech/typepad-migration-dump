---
layout: "post"
title: "Maximize document window in mdi environment"
date: "2012-10-29 02:57:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/maximize-document-window-in-mdi-environment.html "
typepad_basename: "maximize-document-window-in-mdi-environment"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>How do I maximize an active document window in AutoCAD environment?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The sample code that follows maximizes an active document window. Before proceeding, be sure that the project supports MFC and .DLL as file extensions.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">static</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> test(</span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; CMDIChildWnd&#0160;&#0160; *m_pWnd;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160; CMDIFrameWnd&#0160;&#0160;&#0160; *m_pWndFrame;</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//&#0160;&#0160; get the pointer to AutoCAD&#39;s main frame window. </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_pWndFrame = acedGetAcadFrame();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// get the handle of the active window</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_pWnd&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; = m_pWndFrame-&gt;MDIGetActive();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// maximize it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; m_pWnd-&gt;ShowWindow(SW_MAXIMIZE);</span>&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
</div>
