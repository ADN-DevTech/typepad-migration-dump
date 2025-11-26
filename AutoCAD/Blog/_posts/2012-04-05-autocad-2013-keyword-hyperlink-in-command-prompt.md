---
layout: "post"
title: "AutoCAD 2013 Keyword hyperlink in command prompt"
date: "2012-04-05 03:55:47"
author: "Balaji"
categories:
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/autocad-2013-keyword-hyperlink-in-command-prompt.html "
typepad_basename: "autocad-2013-keyword-hyperlink-in-command-prompt"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>AutoCAD 2013 can display keywords / subcommands as hyperlinks that can be selected using the mouse. In ObjectARX, for your keywords to appear as hyperlinks, simply add them to the prompt message within square brackets each separated by a forward slash. For ex : "Specify floor [ First / Second ]"</p>
<p>In AutoCAD .Net API, just adding keywords will make them appear as hyperlinks. You will not need to modify the prompt message.</p>
<p>Here is sample code using ObjectARX :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d point;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> ret = RTNORM;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedInitGet(RSG_NONULL, _T(</span><span style="color: #a31515; line-height: 140%;">"First Second"</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">ret = acedGetPoint</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">NULL, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ACRX_T(</span><span style="color: #a31515; line-height: 140%;">"\nSelect a point [First / Second]"</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">asDblArray(point)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ret == RTKWORD) </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">TCHAR kw[20];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acedGetInput(kw);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(kw);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(ret == RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ACRX_T</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="color: #a31515; line-height: 140%;">"\nSelected Point : %lf, %lf, %lf"</span><span style="line-height: 140%;">),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">point.x,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">point.y,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">point.z</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(ACRX_T(</span><span style="color: #a31515; line-height: 140%;">"\nNothing selected"</span><span style="line-height: 140%;">)); </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>Here is a sample code using AutoCAD .Net API :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;"> ppo = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptPointOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">"Pick a point "</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ppo.Keywords.Add(</span><span style="color: #a31515; line-height: 140%;">"First"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ppo.Keywords.Add(</span><span style="color: #a31515; line-height: 140%;">"Second"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptPointResult</span><span style="line-height: 140%;"> ppr = ed.GetPoint(ppo);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.Keyword)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.WriteMessage(ppr.StringResult);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (ppr.Status == </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.WriteMessage(ppr.Value.ToString());</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">"Cancelled"</span><span style="line-height: 140%;">);</span></p>
</div>
