---
layout: "post"
title: "Job Queue Priority"
date: "2011-07-11 09:51:09"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/07/job-queue-priority.html "
typepad_basename: "job-queue-priority"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Some people have noticed that there is a new job queue column called Priority.&#0160; This is an API feature, at least for now.&#0160; So you have to use custom programming if you want to make use of this value.&#0160; From Vault Explorer, you can see the value, but you can&#39;t do anything with it.</p>
<p><img alt="" src="/assets/Priority.png" /></p>
<p>Let me start by explaining how NOT to use the priority feature:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">You have 3 tasks (A, B and C) which you want to complete in order.&#0160; So you queue up all three jobs at once, giving A a priority of 1, B a priority of 2 and C a priority of 3.&#0160; Now you are guaranteed that they will get executed in order, right?         <br /> <br /><span style="color: #ff0000; font-size: x-large;">Wrong           <br /></span> <br />Because there may be multiple Job Processors running and because Job Processors can be specialized by type, you are not guaranteed the order.&#0160; It&#39;s technically possible for C to get executed first, with B second and A last.&#0160; Or any other permutation.</td>
</tr>
</tbody>
</table>
<p>Now let&#39;s move on to the intended use:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">You are doing a batch operation using the job queue.&#0160; This operation will take several days, involving thousands on jobs on the queue.&#0160; In the meantime, the vault is still in use by end users.&#0160; You want don&#39;t want the batch operations to interfere with user-driven operations.&#0160; So you queue the batch jobs with a low priority while user jobs get a high priority.         <br /> <br /><span style="color: #008000; font-size: x-large;">Correct           <br /></span> <br />The priority feature lets you move jobs to the front of the line if needed.&#0160; Likewise, you can indicate jobs that should run in the background.</td>
</tr>
</tbody>
</table>
<p>The way it works is pretty basic.&#0160; The priority value is an integer.&#0160; The lower the number, the higher priority, with 1 being the highest possible priority.&#0160; When the Job Processor reserves a job, it will reserve the job with the lowest priority for a type that the Job Processor supports.&#0160; If there are 2 jobs with the same priority on the queue, then the first one added is the first one processed.</p>
<p>Jobs with types that are not supported by a Job Processors are basically invisible to that process.&#0160; So the priority is based on only visible jobs.&#0160; For example, the Job queue consists of job A (priority 1) and job B (priority 7).&#0160; The Job Processor supports only type B.&#0160; The result is that job B is read off the queue next.&#0160; Job A will not get processed by this Job Processor since it is a unsupported type.</p>
<p>When deciding what priority your custom jobs should have, it helps to have some context.&#0160; Vault Explorer and the CAD plug-ins use 10 as the default priority value for jobs.&#0160; Autoloader uses priority 100 when it queues jobs.&#0160; Jobs triggered automatically via a lifecycle state change are also queued as priority 100.</p>
<p>Once a job is on the queue, it&#39;s priority cannot be changed.&#0160; The workaround is to remove it from the queue and re-add it with a different priority.&#0160; This can easily be done through the API, but there is currently no UI command to do this task.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
