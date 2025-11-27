---
layout: "post"
title: "API Hotfixes"
date: "2012-08-02 07:58:42"
author: "Doug Redmond"
categories:
  - "Announcements"
original_url: "https://justonesandzeros.typepad.com/blog/2012/08/api-hotfixes.html "
typepad_basename: "api-hotfixes"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Announcements3.png" /></p>
<p>We recently released <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=19844149&amp;linkID=9261341" target="_blank">Hotfix - API Lifecycle/Job Processor</a>, which addresses two separate API related issues in Vault 2013.&#0160; In both cases, the fixes are with Vault applications themselves, not with the SDK DLLs.&#0160;&#0160; So you don’t need to update any of your code and you don’t need to re-compile.&#0160; Just apply the hotfix if you run into any of these issues and things should work.</p>
<p>&#0160;</p>
<p><strong>Issue 1</strong>:&#0160; If your extension hooks into change lifecycle events and adds a restriction, Vault Explorer shows a confusing error message.&#0160; The operation is restricted, as expected, but the user sees the message “restrictedEntities Files Folders and CustomEntities are all empty.”</p>
<p>After applying the hotfix, users will see the standard Restrictions Found error dialog, which can be expanded to show messages from your extension.</p>
<p>&#0160;</p>
<p><strong>Issue 2</strong>:&#0160; When IExplorerUtil is used in a job handler extension, it will fail for all jobs after the first one.&#0160; The underlying problem is that each job gets its own login and IExplorerUtil was remembering the security context.&#0160; So IExplorerUtil would work for the first job but fail for all other jobs.&#0160; The hotfix clears out any security details that IExplorerUtil may be remembering between jobs.</p>
<p><img alt="" src="/assets/Announcements3-1.png" /></p>
