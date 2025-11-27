---
layout: "post"
title: "Job Processor logins"
date: "2013-02-22 16:26:51"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2013/02/job-processor-logins.html "
typepad_basename: "job-processor-logins"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Job Processor is constantly logging in and out of Vault.&#0160; So plugging-in to that app is different than with Vault Explorer, where you assume that the user is mostly staying logged in as a single user.&#0160; Job Processor, on the other hand, may have zero, one or two simultaneous log-ins at any given time.</p>
<p>It’s probably best if I just go over Job Processors timeline:</p>
<ol>
<li>Job Processor wakes up. </li>
<li>Job Processor logs in to the server, but not to a specific Vault.&#0160; This type of log in has limited functionality and does <strong>not</strong> consume a license. </li>
<li>IJobHandler.OnJobProcessorWake is called. </li>
<li>Job Processor checks the Job queue. </li>
<li>For each job:      <ol>
<li>Job Processor logs in to a Vault. This part consumes the license. </li>
<li>Job Processor sends the job off to the corresponding handler. (IJobHandler.Execute) </li>
<li>Job Processor logs out of Vault, which frees the license. </li>
</ol>   </li>
<li>Job processor logs out of the server. </li>
<li>IJobHandler.OnJobProcessorSleep is called. </li>
<li>Job processor goes to sleep. </li>
<li>Go back to step 1 after X number of minutes. </li>
</ol>  
<hr noshade="noshade" style="color: #ff5a00;" />
<p>So yes, at times Job Processor is logged in twice.&#0160; Although the same username and password are used, the contexts are different.&#0160; For all intents and purposes, they are completely separate log-ins.&#0160; Each job gets its own log-in, so your Execute handler should not be remembering anything about the context.&#0160; If your plug-in wants to do any useful Vault operations during the Wake or Sleep events, it will have to have its own set of credentials (see <a href="http://justonesandzeros.typepad.com/blog/2012/05/q-tools-and-watch-folder-2013.html">Watch Folder 2013</a>).</p>
<p>Here are some other random facts:</p>
<ul>
<li>You can query the job queue without being logged into a specific Vault.&#0160; However, adding a job to the queue requires you to be logged in to a Vault. </li>
<li>Speaking of Watch Folder, I hooked it up to our internal Vault at Autodesk.&#0160; Every day I get a report of what’s going on in the Vault, and it’s been running solid for the past 8 months. </li>
<li>IExplorerUtil had a bug that was caused by it remembering the login context.&#0160; If you are using IExplorerUtil tool inside Job Processer 2013, make sure to grab <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=19844149&amp;linkID=9261341" target="_blank">the hotfix</a>. <br />Note: The hotfix has been rolled up into <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=21017345&amp;linkID=9261341" target="_blank">Service Pack 1</a>.</li>
</ul>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
