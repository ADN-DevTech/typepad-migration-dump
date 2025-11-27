---
layout: "post"
title: "What&rsquo;s Wrong with this Code - Admin Check"
date: "2012-03-23 14:50:54"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/03/whats-wrong-with-this-code-admin-check.html "
typepad_basename: "whats-wrong-with-this-code-admin-check"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>It’s time for everyone’s favorite game show, <strong>What’s Wrong with this Code!</strong>&#0160; Today we are going to look at some code that is supposed to check if the logged in Vault user is an administrator or not.</p>
<p>Have a look at the below code and try to spot all the things wrong with it.&#0160;</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #008000;">// C#</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>public</span></span></span><span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">bool</span></span><span style="color: #000000;"> IsAdmin(</span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;"> mgr)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">long</span></span><span style="color: #000000;"> userid = mgr.SecurityService.SecurityHeader.UserId;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #2b91af;">Role</span></span><span style="color: #000000;">[] roles = mgr.AdminService.GetRolesByUserId(userid);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> roles.Any(n =&gt; n.Name == </span><span><span style="color: #a31515;">&quot;Administrator&quot;</span></span><span style="color: #000000;">);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></span></p>
</td>
</tr>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #008000;">&#39; VB.NET</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>Public</span></span></span><span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">Function</span></span><span style="color: #000000;"> IsAdmin(mgr </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;">) </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">Boolean</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> userid </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">Long</span></span><span style="color: #000000;"> = mgr.SecurityService.SecurityHeader.UserId</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> roles </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">Role</span></span><span style="color: #000000;">() = mgr.AdminService.GetRolesByUserId(userid)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Return</span></span><span style="color: #000000;"> roles.Any(</span><span><span style="color: #0000ff;">Function</span></span><span style="color: #000000;">(n) n.Name = </span><span><span style="color: #a31515;">&quot;Administrator&quot;</span></span><span style="color: #000000;">)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>End</span></span></span><span><span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">Function</span></span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p>You can assume good data is being passed in. You can also assume that unexpected errors, such as network failures, are handled in a higher level catch block.</p>
<p>Continue on when you think you found all the errors...</p>

<!--more-->

<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
<p><strong>Error 1:</strong>&#0160; There should be a check to see if userid is valid.&#0160; In the case of an anonymous connection, userid will be 0.&#0160; All valid User IDs are greater than 0.</p>
<p><strong>Error 2:</strong>&#0160; GetRolesByUserId will fail unless the user is logged in as an administrator.</p>
<p><strong>Error 3:</strong>&#0160; For non-English installs, the role name will probably not be “Administrator”.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>I’ll be the first to admit that correct code is not very elegant.&#0160; The core Vault framework doesn’t really have an administrator concept.&#0160; Instead it just understands that users have groups of permissions.&#0160; “Administrator” is a higher level concept, and is a collection of almost every permission.</p>
<p>So the permission set is what we need to look at, and we do that with GetPermissionsByUserId.&#0160; Non-admins can call this function as long as they are calling it for their own user ID.&#0160; Once we get the permission set, we check for a permission that only the admin group has.&#0160; Permission IDs don’t change between vaults, so this is one of the few cases where it’s OK hard-code an ID.</p>
<p>Here is the correct code:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #008000;">// C#</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>public</span></span></span><span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">bool</span></span><span style="color: #000000;"> IsAdmin(</span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;"> mgr)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">{</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">long</span></span><span style="color: #000000;"> userid = mgr.SecurityService.SecurityHeader.UserId;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">if</span></span><span style="color: #000000;"> (userid &lt;= 0)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">false</span></span><span style="color: #000000;">;</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #2b91af;">Permis</span></span><span style="color: #000000;">[] permissions = </span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-spacerun: yes;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span>mgr.AdminService.GetPermissionsByUserId(userid);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span style="color: #008000;">// check for AdminUserRead permission ID=82</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-spacerun: yes;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">return</span></span><span style="color: #000000;"> permissions.Any(n =&gt; n.Id == 82);</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">}</span></span></span></p>
</td>
</tr>
<tr>
<td valign="top" width="470">
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #008000;">&#39; VB.NET</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>Public</span></span></span><span><span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">Function</span></span><span style="color: #000000;"> IsAdmin(mgr </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">WebServiceManager</span></span><span style="color: #000000;">) </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">Boolean</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> userid </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #0000ff;">Long</span></span><span style="color: #000000;"> = mgr.SecurityService.SecurityHeader.UserId</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">If</span></span><span style="color: #000000;"> userid &lt;= 0 </span></span><span><span style="color: #0000ff;">Then</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 2;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Return</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">False</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">End</span></span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">If</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Dim</span></span><span style="color: #000000;"> permissions </span><span><span style="color: #0000ff;">As</span></span><span style="color: #000000;"> </span><span><span style="color: #2b91af;">Permis</span></span><span style="color: #000000;">() = _</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;"><span style="mso-tab-count: 1;"><span>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span></span><span><span style="mso-spacerun: yes;">&#0160;</span>mgr.AdminService.GetPermissionsByUserId(userid)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="color: #000000;">&#0160;</span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span style="color: #008000;">&#39; check for AdminUserRead permission ID=82</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span><span style="font-family: Consolas;"><span style="mso-tab-count: 1;"><span style="color: #000000;"><span>&#0160;&#0160;&#0160; </span></span></span><span><span><span style="color: #0000ff;">Return</span></span><span style="color: #000000;"> permissions.Any(</span><span><span style="color: #0000ff;">Function</span></span><span style="color: #000000;">(n) n.Id = 82)</span></span></span></span></p>
<p class="MsoNormal" style="line-height: normal; margin: 0in 0in 0pt; mso-layout-grid-align: none;"><span style="font-family: Consolas;"><span><span style="color: #0000ff;"><span>End</span></span></span><span><span><span style="color: #000000;"> </span></span><span><span style="color: #0000ff;">Function</span></span></span></span></p>
</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
