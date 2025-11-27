---
layout: "post"
title: "Lifecycle event jobs"
date: "2010-03-29 08:15:45"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2010/03/lifecycle-event-jobs.html "
typepad_basename: "lifecycle-event-jobs"
typepad_status: "Publish"
---

<p><img src="/assets/Concepts2.png" /> </p> <p>Here are some important things you need to know about the new Vault 2011 feature that allows you to queue up a job on a lifecycle state change.</p> <p><strong>Configuring the system</strong>  <br />Obviously you need to have the Job Server enabled.&#0160; So that&#39;s the first step.&#0160; You can enable this feature in the Global admin settings. <br /> <br />The next thing you need to know is that each life cycle transition has 0 to N custom jobs associated with it.&#0160; When a file moves through a transition, a job will be added to the queue for each association.&#0160; </p> <p>To set up which jobs fire for which transitions, I suggest using the Lifecycle Event Editor, which is included in the util directory of the SDK.&#0160; You can also use the Web Service API function UpdateLifeCycleStateTransitionJobTypes in Document Service Extensions.&#0160; <br />My <a href="http://justonesandzeros.typepad.com/blog/2010/03/vault-2011-sdk---video-tour.html">Vault 2011 SDK video tour</a> has a demo of the Lifecycle Event Editor.</p> <p>Keep in mind that this mechanism only applies to custom job types.&#0160; The built-in job types, property sync and DWF generate, are configured through different mechanisms.&#0160; </p> <p><strong>Common pitfalls</strong></p> <ul>
 <li><strong>Events are not handled in real time.</strong>&#0160; The job will instantly go on the queue after the file moves through the transition.&#0160; However, you don&#39;t know when the job will get processed.&#0160; If there is a large backlog, it might be hours before your handler runs. </li>
 <li><strong>Jobs are fired on a per-file basis.</strong>&#0160; For example, let&#39;s say you want to send out an email when a file gets released.&#0160; If someone Releases a 1000 part assembly, it will result in 1000 jobs on the queue.&#0160; You need to make sure that your handler doesn&#39;t send out 1000 emails.&#0160; Much better is to send out 1 email listing the 1000 files.</li>
 <li><strong>Transitions are specific to a lifecycle definition.</strong>&#0160; For example, if you set the jobs for WIP-&gt;Released in the Flexible Release Process, it will have no impact on files using the Basic Release Process. </li>
 </ul>
 <p> </p> <p><strong>Writing the job handler</strong></p> <p>Any job put on the queue this way will have two parameters.&#0160; It will have the &quot;FileId&quot; value and the &quot;LifeCyleTransitionId&quot; value.&#0160; These two pieces of information should allow your code to find out anything else it needs to.</p> <p>Again, your handler might be running hours after the file was transitioned, so you should handle cases like the file being deleted, or the File ID not pointing to the latest version.</p> <p>Other than that, things should be the same as any other job handler. See the SDK documentation for more information of the Job Processor API.</p>
