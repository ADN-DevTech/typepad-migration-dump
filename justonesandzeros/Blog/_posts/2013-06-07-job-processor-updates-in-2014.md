---
layout: "post"
title: "Job Processor Updates in 2014"
date: "2013-06-07 08:11:21"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/06/job-processor-updates-in-2014.html "
typepad_basename: "job-processor-updates-in-2014"
typepad_status: "Publish"
---

<p><img src="/assets/Vault3.png" alt="" /></p>
<p><img src="/assets/Concepts3.png" alt="" /></p>
<p>In an effort to address some long standing Job Processor issues, some major changes were made in 2014.&nbsp; At the time of this writing, there are <em>still</em> some issues being worked on, so be sure to read the workarounds at the bottom of the post.&nbsp; But let’s start with the good news.</p>
<hr style="color: #5acb04;" noshade="noshade" />
<p><strong>All settings are in .vcet.config</strong></p>
<p>In previous releases, you would have to update the XML in JobProcessor.exe.config so that it knew about your custom job types.&nbsp; This was an annoying extra step in deploying you plug-in.&nbsp; For Vault 2014, the .vcet.config format was overhauled (along with the loading mechanism for plug-ins) so it was a good time to slip in a feature that allowed Job Processor to read the job types directly from .vcet.config.</p>
<p>The new .vcet.config allows for meta-data in the form of key/value pairs.&nbsp; Job processor looks for keys that start with “JobType” and associates those types with your plug-in.&nbsp; You can have as many types as you want, just make sure to keep the key names unique.&nbsp; Name your keys like “JobType1”, “JobType2”, “JobType3” and so on.</p>
<p>Example (the parts in red are what you edit in your project):</p>
<table border="1" cellspacing="0" cellpadding="2" width="470">
<tbody>
<tr>
<td width="470" valign="top">
<p>&lt;configuration&gt;            <br />&nbsp; &lt;connectivity.ExtensionSettings3&gt;             <br />&nbsp;&nbsp;&nbsp;&nbsp; &lt;extension             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; interface="Autodesk.Connectivity.JobProcessor.Extensibility.IJobHandler, Autodesk.Connectivity.JobProcessor.Extensibility, Version=18.0.0.0, Culture=neutral, PublicKeyToken=aa20f34aedd220e1"             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; type="<span style="color: #ff0000;">MyNamespace.MyClassName</span>, <span style="color: #ff0000;">MyAssemblyName</span>"&gt;             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;setting key="JobType1" value="<span style="color: #ff0000;">MyJobType</span>"/&gt;             <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;setting key="JobType2" value="<span style="color: #ff0000;">AnotherJobType</span>"/&gt;             <br />&nbsp;&nbsp;&nbsp; &lt;/extension&gt;             <br />&nbsp; &lt;/connectivity.ExtensionSettings3&gt;             <br />&lt;/configuration&gt;</p>
</td>
</tr>
</tbody>
</table>
<br />
<hr style="color: #5acb04;" noshade="noshade" />
<p><strong>More command line options</strong>  </p>
<p>You can never be rich or have too many command line options.&nbsp; In 2014, you can now set username, password and server from the command line using -u, -p and -s respectively. If you are using Windows authentication, use -w.</p>
<p>Example:&nbsp; JobProcessor.exe -u Administrator -p pwd123 -s MyVaultServer    <br />Example:&nbsp; JobProcessor.exe -w -s MyVaultServer</p>
<hr style="color: #5acb04;" noshade="noshade" />
<p><strong>Connectivity.JobProcessor.Delegate.Host.exe</strong></p>
<p>To make JobProcessor.exe more stable, all the unstable operations were moved to a different process.&nbsp; All plug-ins now run in Connectivity.JobProcessor.Delegate.Host.exe, that way if a job crashes or leaks memory, the problem can be handled.&nbsp; And by “handled”, I mean that JobProcessor just kills the delegate process and starts another one.&nbsp; In fact, JobProcessor will periodically kill and restart the delegate just to be on the safe side.</p>
<p>So what does this mean for your plug-in...</p>
<ul>
<li>To debug, you need to attach to the running&nbsp; Connectivity.JobProcessor.Delegate.Host.exe.&nbsp; You can’t launch this process on your own.&nbsp; If you run against JobProcessor.exe, your breakpoints won’t activate. </li>
<li>If you are saving data in memory, keep in mind that the entire process may be killed and restarted. </li>
<li>Only one delegate exe will be running at any given time.&nbsp; And, jobs are still run single-threaded.&nbsp; So you don’t have to worry about your code running in parallel with other jobs. </li>
<li>The OnJobProcessorStartup and OnJobProcessorShutdown events trigger on the startup and <em>graceful</em> shutdown of the delegate process.&nbsp; If there is a crash, there may not be a shutdown event. </li>
</ul>
<hr style="color: #5acb04;" noshade="noshade" />
<p><strong>Open issues and workarounds</strong></p>
<p><strong>Problem:</strong> AcquireFiles hangs when running inside of a job.&nbsp; You may see errors like “Cannot evaluate expression because the current thread is in a sleep, wait, or join”.     <br /><strong>Cause:</strong>&nbsp; The VDF was initialized as a UI app, which is the default setting.&nbsp; We are working on a fix.     <br /><strong>Workaround:</strong>&nbsp; While the fix is being worked on, you can set the VDF to non-UI mode with the below line of code.&nbsp; It won’t hurt anything if multiple plug-ins call this code.&nbsp; And the code won’t harm anything after the fix is released.</p>
<p><span style="color: #ff0000;">Autodesk.DataManagement.Client.Framework.Library.Initialize(false);</span></p>
<p><strong>Additional Information:</strong>&nbsp; <a href="http://adndevblog.typepad.com/manufacturing/2013/05/deadlock-using-acquirefiles-vault-2014.html">Deadlock 
using AcquireFiles() Vault 2014</a></p>
<p><img src="/assets/Concepts3-1.png" alt="" /></p>
