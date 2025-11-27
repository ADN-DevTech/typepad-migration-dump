---
layout: "post"
title: "A Quick Performance Tip"
date: "2012-01-05 08:13:35"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/01/a-quick-performance-tip.html "
typepad_basename: "a-quick-performance-tip"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>The below code will save you a round-trip for each of your Vault API web service calls.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="468">System.Net.ServicePointManager.Expect100Continue = false;</td>
</tr>
</tbody>
</table>
<p>If you have your own Vault client, you should put the code sometime during startup.&#0160; If you are running inside an Vault client from Autodesk, this value should already be set to false.</p>
<p>I don’t have much more to say on this.&#0160; You find out more about the <a href="http://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.expect100continue.aspx">Expect100Continue</a> property on MSDN.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
