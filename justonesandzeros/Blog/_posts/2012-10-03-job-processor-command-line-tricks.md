---
layout: "post"
title: "Job Processor Command Line Tricks"
date: "2012-10-03 08:21:23"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/10/job-processor-command-line-tricks.html "
typepad_basename: "job-processor-command-line-tricks"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>We are working on multiple fronts to address many of the Job Processor issues that people are running into.&#0160; For this post, I want to point out some command line features we added to Vault 2013.&#0160; This feature won’t fix all problems, but it has many uses.</p>
<p>You can read about the command line options from the SDK documentation or the <a href="http://wikihelp.autodesk.com/Vault/enu/Help/Help/0001-Using_Va1/0031-Manage_t31" target="_blank">wiki</a>.&#0160; The idea behind these commands is to make JobProcessor behave more like a service.&#0160; You can now programmatically start, pause, resume, and gracefully shutdown.&#0160; Previously, start and kill were the only things you could do from a command line.</p>
<p>Here are a couple of uses I came up with for these new command line operations.&#0160; Feel free to post your ideas too in the comments section.</p>
<p>&#0160;</p>
<p><strong>Make use of idle computers</strong></p>
<p>When the CAD engineers go home for the night, use the idle computers to run Job Processor.&#0160; Create a Windows task that starts up Job Processor at 8 PM and shuts it down at 5 AM, for example.&#0160; If every CAD station does that every night, it should clear out any backups on the job queue.</p>
<p>&#0160;</p>
<p><strong>Run certain jobs at certain times</strong></p>
<p>Maybe you would rather not process DWF files during the daytime.&#0160; You can set things up so that DWF files only get processed at night.&#0160; The first step is to copy JobProcessor.exe.config.&#0160; Next edit the copy in an XML editor and comment out all the &lt;jobHandler&gt; elemets where the <strong>class</strong> attribute starts with “Autodesk.Vault.DWF.Create”.&#0160; This config will will be the one you use when you don’t want to handle DWF files.</p>
<p>Set up a Windows task that runs at 5 AM.&#0160; The task will shut down Job Processor, swap in the non-DWF config file, then re-start Job Processor.&#0160; Because the DWF &lt;JobHandler&gt; tags are commented out, it will not process DWF files.&#0160; Set up another task at 8 PM to shut down Job Processor, swap in the original config file, then re-start.</p>
<p>Here is an example BAT file for the 8 AM task.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">
<p>cd C:\Program Files\Autodesk\Vault Professional 2013\Explorer</p>
<p>REM Send the stop command to Job Processor           <br />JobProcessor /stop</p>
<p>REM Wait for Job Processor to exit           <br />:RunCheck</p>
<p>tasklist /FI &quot;IMAGENAME eq JobProcessor.exe&quot; 2&gt;NUL | find /I /N &quot;JobProcessor.exe&quot;&gt;NUL</p>
<p>if &quot;%ERRORLEVEL%&quot;==&quot;0&quot; goto RunCheck</p>
<p>REM swap in the non-DWF config file           <br />xcopy JobProcessor.exe.noDwf.config JobProcessor.exe.config /Y</p>
<p>REM restart job processor           <br />start JobProcessor</p>
<p>REM Job Processor should be running now and should be ignoring DWF jobs</p>
</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
