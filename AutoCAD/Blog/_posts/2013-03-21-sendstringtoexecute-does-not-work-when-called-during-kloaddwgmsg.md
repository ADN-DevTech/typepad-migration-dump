---
layout: "post"
title: "SendStringToExecute does not work when called during kLoadDwgMsg"
date: "2013-03-21 01:39:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "LISP"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/03/sendstringtoexecute-does-not-work-when-called-during-kloaddwgmsg.html "
typepad_basename: "sendstringtoexecute-does-not-work-when-called-during-kloaddwgmsg"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>Why does an &quot;eNoDocument&quot; error occur when executing an AutoLISP expression while in the kLoadDwgMsg handler and when using sendStringToExecute?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>Although sendStringToExecute is the preferred way to execute an AutoLISP expression or commands on AutoCAD, this function does not work during the kLoadDwgMsg. This is because the AutoCAD command line is not yet fully ready at this point. (Note that your application receives a kLoadDwgMsg when it is loaded    <br />into AutoCAD in the middle of a session, and at that time sendStringToExecute will work. However, kLoadDwgMsg is also sent to your application when a new document is created and sendStringToExecute will not work in this scenario.)</p>
<p>The workaround is to use the ads_queueexpr function in this context. (Search the ObjectARX online help for more information on ads_queueexpr.)</p>
<p>Example:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">extern</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #a31515;">&quot;C&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">int</span><span style="line-height: 140%;"> ads_queueexpr(</span><span style="line-height: 140%; color: blue;">const</span><span style="line-height: 140%;"> TCHAR*);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OnkLoadDwgMsg()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; ads_queueexpr(L</span><span style="line-height: 140%; color: #a31515;">&quot;(command \&quot;line\&quot; \&quot;0,0\&quot; \&quot;1,1\&quot; \&quot;\&quot;)\n&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
