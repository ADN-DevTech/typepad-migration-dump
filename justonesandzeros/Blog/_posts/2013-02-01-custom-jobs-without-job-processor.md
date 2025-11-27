---
layout: "post"
title: "Custom Jobs without Job Processor"
date: "2013-02-01 08:28:47"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2013/02/custom-jobs-without-job-processor.html "
typepad_basename: "custom-jobs-without-job-processor"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>If you are building your own custom job, you do not have to plug-in to Job Processor if you don’t want to.&#0160; The job queue can be accessed through the Vault web services API, just like with files and folders.&#0160; </p>
<p>The <em>recommended</em> approach is to use Job Processor.&#0160; That approach is what you see in the SDK documentation and sample apps.&#0160; But the Job Processor can’t handle all the cases.&#0160; There are some times when you may want to just write your own service.&#0160; This article will help you identify those cases and provide some help writing the code.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>First let’s talk about what Job Processor does well.&#0160; It’s a single application than can handle multiple job types.&#0160; It has various job handlers loaded out-of-the box, and additional functionality can be plugged in.&#0160; For a Vault admin, this is good because everything can be managed from a single app.&#0160; If something goes wrong, there is only one place to look, one log file to check, and so on.&#0160; From the developer&#39;s point of view, Job Processor offers some nice features.&#0160; You don’t have to worry about Vault authentication, managing the job queue and logging.&#0160; None of these things are very hard, but it’s nice to have the app take care of them.</p>
<p>On the flip side, Job Processor does have some limitations.&#0160; First, it’s not a true Windows service.&#0160; A user has to log in, launch the app, and leave it running.&#0160; Job Processor handles jobs one at a time.&#0160; There is no parallel processing.&#0160; And you can only run a single process on a machine.&#0160; A common problem is that Job Processor gets bogged down with jobs that take a long time, like DWF generate jobs.&#0160; This prevents other types from running even if they take less time to run.&#0160; </p>
<p>If you are running in a multi-site environment, Job Processor can only process jobs from one site.&#0160; Vault requires that a job can only be handled by the site that queued it.&#0160; So if you have 5 sites queuing jobs, you would need 5 Job Processors running.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>By communicating directly with the job queue you can bypass these limitations and build exactly the app you want.&#0160; Working with the job queue is easy.&#0160; You just need to use the following functions from the JobService:</p>
<ul>
<li><strong>ReserveNextJob</strong> - This grabs a job and reserves it to you so that nobody else works on it.&#0160; You pass in the job types you support and Vault passes back the “next” job based on the priority and the time it was queued. </li>
<li><strong>UpdateJobSuccess</strong> - This tells Vault that you are done with the job, and it can be removed from the queue. </li>
<li><strong>UpdateJobFailure</strong> - This tells Vault that something went wrong.&#0160; The job stays on the queue in the error state.&#0160; You can pass back information on what went wrong. </li>
<li><strong>UnReserveJobById</strong> - This function is used for cases where a job gets reserved but never updated.&#0160; For example, maybe the service crashed or there was a power outage.&#0160; </li>
</ul>
<p>If you want an example, have a look a <a href="http://justonesandzeros.typepad.com/blog/2012/05/q-tools-and-watch-folder-2013.html">Q-Tools</a>.&#0160; It’s a Windows service that can interact with queues from multiple Vault sites.</p>
<p><img alt="" src="/assets/Services-scaled.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
