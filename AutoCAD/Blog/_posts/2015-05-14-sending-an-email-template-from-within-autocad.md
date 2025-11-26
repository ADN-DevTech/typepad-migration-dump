---
layout: "post"
title: "Sending an email template from within AutoCAD"
date: "2015-05-14 22:24:11"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "2014"
  - "2015"
  - "2016"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2015/05/sending-an-email-template-from-within-autocad.html "
typepad_basename: "sending-an-email-template-from-within-autocad"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Recently, I received a query from an ADN partner, as part of partner customisation requirement expects users to send feedback about publisher app.</p>
<p>The idea is to trigger outlook from a .NET command which is loaded in a macro button.</p>
<p>We can use&#0160; Microsoft.Office.Interop.Outlook API to achieve this in our command.</p>
<p>Sample code:</p>
<p>Use assemblyref&#0160; Microsoft.Office.Interop.Outlook</p>
<p>&#0160;</p>
<div style="font-size: 9pt; font-family: consolas; background: white; color: black;">
<p style="margin: 0px;">[<span style="color: #2b91af;">CommandMethod</span>(<span style="color: #a31515;">&quot;SMTP&quot;</span>)]</p>
<p style="margin: 0px;"><span style="color: blue;">static</span> <span style="color: blue;">public</span> <span style="color: blue;">void</span> SMTP()</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: blue;">try</span></p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">string</span>&gt; lstAllRecipients = <span style="color: blue;">new</span> <span style="color: #2b91af;">List</span>&lt;<span style="color: blue;">string</span>&gt;();</p>
<p style="margin: 0px;"><span style="color: green;">//Below is hardcoded - can be replaced with db data</span></p>
<p style="margin: 0px;">lstAllRecipients.Add(<span style="color: #a31515;">&quot;Helpdesk@Publisher.com&quot;</span>);</p>
<p style="margin: 0px;">Outlook.<span style="color: #2b91af;">Application</span> outlookApp =</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; <span style="color: blue;">new</span> Outlook.<span style="color: #2b91af;">Application</span>();</p>
<p style="margin: 0px;">Outlook.<span style="color: #2b91af;">_MailItem</span> oMailItem =&#0160;</p>
<p style="margin: 0px;">&#0160;&#0160;&#0160; (Outlook.<span style="color: #2b91af;">_MailItem</span>)outlookApp.CreateItem(Outlook.<span style="color: #2b91af;">OlItemType</span>.olMailItem);</p>
<p style="margin: 0px;">Outlook.<span style="color: #2b91af;">Inspector</span> oInspector = oMailItem.GetInspector;</p>
<p style="margin: 0px;"><span style="color: green;">// Recipient</span></p>
<p style="margin: 0px;">Outlook.<span style="color: #2b91af;">Recipients</span> oRecips =</p>
<p style="margin: 0px;">(Outlook.<span style="color: #2b91af;">Recipients</span>)oMailItem.Recipients;</p>
<p style="margin: 0px;"><span style="color: blue;">foreach</span> (<span style="color: #2b91af;">String</span> recipient <span style="color: blue;">in</span> lstAllRecipients)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">Outlook.<span style="color: #2b91af;">Recipient</span> oRecip =</p>
<p style="margin: 0px;">(Outlook.<span style="color: #2b91af;">Recipient</span>)oRecips.Add(recipient);</p>
<p style="margin: 0px;">oRecip.Resolve();</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">//Add Subject</span></p>
<p style="margin: 0px;">oMailItem.Subject = <span style="color: #a31515;">&quot;Test Mail&quot;</span>;</p>
<p style="margin: 0px;"><span style="color: green;">// body</span></p>
<p style="margin: 0px;">oMailItem.Body = <span style="color: #a31515;">&quot; Write your Body &quot;</span>;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green;">//Display the mailbox</span></p>
<p style="margin: 0px;">oMailItem.Display(<span style="color: blue;">true</span>);</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;"><span style="color: blue;">catch</span> (<span style="color: #2b91af;">SystemException</span> objEx)</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;"><span style="color: #2b91af;">Application</span>.ShowAlertDialog(objEx.ToString());</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">}</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">In the CUI macro button, pass ^C^C_SMTP</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p><iframe allowfullscreen="allowfullscreen" frameborder="0" height="250" src="https://screencast.autodesk.com/Embed/a517c91d-6d44-401f-9f6d-e9d3df30e76d" webkitallowfullscreen="webkitallowfullscreen" width="400"></iframe></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">Reference:<a href="http://stackoverflow.com/questions/22152589/how-to-send-mail-from-users-outlook-mailbox" target="_blank">DisplayMailbox</a></p>
