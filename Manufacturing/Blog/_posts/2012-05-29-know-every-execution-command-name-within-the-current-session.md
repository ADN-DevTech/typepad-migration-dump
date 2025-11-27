---
layout: "post"
title: "know every execution command name within the current session"
date: "2012-05-29 20:16:51"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/know-every-execution-command-name-within-the-current-session.html "
typepad_basename: "know-every-execution-command-name-within-the-current-session"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>The question is to know the list of all commands that have been executed in the session. OnActivateCommand of UserInputEvents will be helpful. In this sample below, UserInputEvents starts and an array records every execution command name. If the commands count exceeds 100, the event stops, print all the commands which have ever executed.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oCommandList </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;">&#0160; </span><span style="color: #2b91af; line-height: 140%;">ArrayList</span><span style="line-height: 140%;">&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oUserEvents </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">UserInputEvents</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> startEvent()</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; _InvApplication = _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Runtime.InteropServices.</span><span style="color: #2b91af; line-height: 140%;">Marshal</span><span style="line-height: 140%;">. _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; GetActiveObject(</span><span style="color: #a31515; line-height: 140%;">&quot;Inventor.Application&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oCommandList = </span><span style="color: blue; line-height: 140%;">New</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">ArrayList</span><span style="line-height: 140%;">()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">&#39; assume we have got Inventor application&#0160;&#0160;&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; oUserEvents =&#0160; _ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; _InvApplication.CommandManager.UserInputEvents </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">AddHandler</span><span style="line-height: 140%;"> oUserEvents.OnActivateCommand, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> oUserEvents_OnActivateCommand</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub<br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> stopEvent()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">RemoveHandler</span><span style="line-height: 140%;"> oUserEvents.OnActivateCommand, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">AddressOf</span><span style="line-height: 140%;"> oUserEvents_OnActivateCommand</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub<br /></span></p>
</div>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;">&#0160; oUserEvents_OnActivateCommand( _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> CommandName </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span><span style="line-height: 140%;">, _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">ByVal</span><span style="line-height: 140%;"> Context </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> Inventor.</span><span style="color: #2b91af; line-height: 140%;">NameValueMap</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">If</span><span style="line-height: 140%;"> oCommandList.Count &lt; 100 </span><span style="color: blue; line-height: 140%;">then</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; oCommandList.Add(CommandName)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Else</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> oIndex </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span><span style="line-height: 140%;"> </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> oIndex = 0 </span><span style="color: blue; line-height: 140%;">to</span><span style="line-height: 140%;"> oCommandList.Count </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: #2b91af; line-height: 140%;">Debug</span><span style="line-height: 140%;">.Print (oCommandList(oIndex))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">If</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
