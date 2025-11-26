---
layout: "post"
title: "EventWatcherPlugin cannot work after Navisworks launches"
date: "2012-06-12 19:03:42"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/eventwatcherplugin-cannot-work-after-navisworks-launches.html "
typepad_basename: "eventwatcherplugin-cannot-work-after-navisworks-launches"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue</strong></p>
<p>We tested with the SDK sample \api\net\examples\Automation\MessageClientServer\MessageClientPlugin. It has one EventWatcherPlugin which delegates some events of the application. In theory, these events are scribed when NW launches. Thus they will work in whole sessions of Navisworks. The events are fired when Navisworks is launching. But after it launches, no matter how you open/add/new/close document, these events are not fired any more!</p>
<p><br /><strong>Solution</strong></p>
<p>Navisworks is currently an SDI Document. A single Document&#0160; is created at start-up and destroyed at shut-down.&#0160; The ‘ActiveDocument’ stays the same throughout the lifetime of the program, including when you&#0160; load/unload a file. <br />What you need to be watching is: <strong>Autodesk.Navisworks.Api.Application.ActiveDocument.Models.CollectionChanged <br /></strong></p>
<p>Snippet of the required code here is as below:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> ActiveDocument_Models_CollectionChanged(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.</span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="line-height: 140%; color: #a31515;">&quot;ActiveDocument_Models_CollectionChanged&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> Application_ActiveDocumentChanged(</span><span style="line-height: 140%; color: blue;">object</span><span style="line-height: 140%;"> sender, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; System.</span><span style="line-height: 140%; color: #2b91af;">EventArgs</span><span style="line-height: 140%;"> e)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">MessageBox</span><span style="line-height: 140%;">.Show(</span><span style="line-height: 140%; color: #a31515;">&quot;ActiveDocumentChanged&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Autodesk.Navisworks.Api.</span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.ActiveDocument.Models.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; CollectionChanged</span><span style="line-height: 140%;">+= ActiveDocument_Models_CollectionChanged;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
