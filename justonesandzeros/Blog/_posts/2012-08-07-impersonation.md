---
layout: "post"
title: "Impersonation"
date: "2012-08-07 09:32:31"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2012/08/impersonation.html "
typepad_basename: "impersonation"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p><strong>Update</strong>:&#0160; This feature has been deprecated.&#0160; It will not be available in the next release of Vault. </p>
<p>&#0160;</p>
<p>This is Queen Elizabeth with an important message about bicycle safety.</p>
<p>Ha!&#0160; Fooled you.&#0160; I’m just computer programmer/blogger Doug Redmond, as usual.&#0160; I pretended to be the queen to give an example of impersonation.&#0160; Please don’t hate me.</p>
<p>The Vault 2013 API now allows you to impersonate other users.&#0160; It’s a useful feature in certain cases, but it’s not without limitations.&#0160; I’ll go over the feature so that you know when and when not to impersonate another user.</p>
<p>Basically, impersonation allows an administrator to switch credentials to that of another user without the need to know the other user’s password.&#0160; When you impersonate another user in Vault, you essentially log out and log back in as the other user.&#0160; Only administrators can impersonate for obvious reasons.&#0160; However, the process of impersonation means that you give up your admin rights (unless you impersonate another admin).&#0160; When you impersonation another user, you <strong><em>are</em></strong> that other user as far as Vault is concerned, including any security limitations.</p>
<p>Your initial set of admin credentials is completely lost after the impersonate.&#0160; If you want to go back to your old identity, you need to sign-in again.&#0160; For this reason, you should not use this feature during a custom command or event handler.&#0160; In those cases, you are sharing a connection, and the parent application is still using that admin login.&#0160; You can use this feature in a job handler, however.&#0160; Each job gets its own login, so you are not going to break anything by switching to another user.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>The code is fairly straightforward.&#0160; You call SecurityService.Impersonate() to switch to another user.&#0160; When the call completes, the credentials on the SecurityService switch to the new user and the old credentials are invalid.</p>
<p>Unfortunately, the WebServiceManager doesn’t have built in support for impersonation.&#0160; So it’s best to throw away the old manager and create a new one after the impersonate.&#0160; For example...</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span><span style="font-size: 9pt; color: #008000;">// C#</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span>&#0160;</span></span></span><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9pt;">m_svcMgr.SecurityService.Impersonate(</span></span><span style="font-size: 9pt;"><span><span style="color: #a31515;">&quot;Guest&quot;</span></span><span style="color: #000000;">, </span><span><span style="color: #a31515;">&quot;Vault&quot;</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">false</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #2b91af;"><span style="font-size: 9pt;">WebServiceCredentials</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> cred = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceCredentials</span></span><span style="color: #000000;">(m_svcMgr.SecurityService);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9pt;">m_svcMgr = </span></span><span style="font-size: 9pt;"><span><span style="color: #0000ff;">null</span></span><span style="color: #000000;">;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="color: #000000; font-family: Consolas;">&#0160;</span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">using</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> (</span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;"> svcMgr2 = </span><span><span style="color: #0000ff;">new</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;">(cred))</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">{</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 9pt; color: #008000;">// do stuff as other user</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">}</span></span></span></p>
</td>
</tr>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span><span style="font-size: 9pt; color: #008000;">&#39; VB.NET</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;">&#0160;</span></span><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9pt;">mgr.SecurityService.Impersonate(</span></span><span style="font-size: 9pt;"><span><span style="color: #a31515;">&quot;Guest&quot;</span></span><span style="color: #000000;">, </span><span><span style="color: #a31515;">&quot;Vault&quot;</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">false</span></span><span style="color: #000000;">, </span><span><span style="color: #0000ff;">Nothing</span></span><span style="color: #000000;">)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">Dim</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> cred </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">New</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceCredentials</span></span><span style="color: #000000;">(mgr.SecurityService)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="font-size: 9pt;">mgr = </span></span><span><span style="font-size: 9pt; color: #0000ff;">Nothing</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="mso-tab-count: 2;"><span style="font-family: Consolas;"><span style="font-size: 9pt; color: #000000;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">Using</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> svcMgr2 </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">New</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;">(cred)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span style="font-size: 9pt;">&#0160;&#0160;&#0160; </span></span></span><span><span style="font-size: 9pt; color: #008000;">&#39; do stuff as other user</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span style="font-size: 9pt;">End</span></span></span><span><span style="font-size: 9pt;"><span style="color: #000000;"> </span></span><span><span style="font-size: 9pt; color: #0000ff;">Using</span></span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p><strong>&#0160;</strong></p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Summary:</strong></p>
<ul>
<li>Impersonation allows an administrator to switch credentials to that of another user without the need to know the other user’s password. </li>
<li>Impersonation logs you out of your current session. </li>
<li>You have all the security limitations of the impersonated user. </li>
</ul>
<ul>
<li>When to use:       
<ul>
<li>Job Handlers </li>
<li>Custom EXEs </li>
</ul>
</li>
<li>When not to use:       
<ul>
<li>Custom commands </li>
<li>Event handlers </li>
</ul>
</li>
</ul>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
