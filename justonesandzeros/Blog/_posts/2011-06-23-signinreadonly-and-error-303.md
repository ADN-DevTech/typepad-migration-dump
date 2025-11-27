---
layout: "post"
title: "SignInReadOnly and error 303"
date: "2011-06-23 07:30:24"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/06/signinreadonly-and-error-303.html "
typepad_basename: "signinreadonly-and-error-303"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>This post is for cases where you come across error 303 (PermissionDenied) but are <em><strong>sure</strong></em> that your user has adequate permissions.</p>
<p>One thing you should check is the function you use to sign in.&#0160; If you are using SignInReadOnly, it may be causing the error, even if you are not calling an edit function.</p>
<p>SignInReadOnly works by signing you in with a reduced set of permissions.&#0160; As you may already know, each web service function has a permission requirement.&#0160; If your signed-in user doesn&#39;t meet that requirement, it&#39;s an error 303.</p>
<p>SignInReadOnly will remove every permission that is related to editing, but there are some cases where a Get function requires one of these permissions.&#0160; As a result, the Get fails even though it is technically a read-only function.</p>
<p>Unfortunately, there is no way for you to see your active permission set.&#0160; AdminService.GetPermissionsByUserId(..) will only tell you your permission set under normal sign in conditions.&#0160; The best thing you can do is try a regular sign in and see if that fixes things.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
