---
layout: "post"
title: "Vault Integration Pattern - Asynchronous Queue"
date: "2013-02-08 10:03:21"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2013/02/vault-integration-pattern-asynchronous-queue.html "
typepad_basename: "vault-integration-pattern-asynchronous-queue"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>The final entry in my Vault Integration Pattern series will focus on using Vault’s job queue for transferring data.&#0160; Like I said before, there are more integrations patterns out there, I’m just focusing on the ones that I have used first-hand.</p>
<p>I used this technique for the short-lived integration between Vault and PLM.&#0160; Let me just get this out of the way upfront: The integration pattern I used had nothing to do with the app being discontinued.&#0160; The long term vision changed, and the integration was caught in the middle.&#0160; From a technical standpoint, the integration pattern is sound.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Since PLM is in the cloud and Vault is behind a firewall, there was no easy way for PLM to have a direct view into Vault.&#0160; Data would have to be copied to PLM.&#0160; I also wanted a very reliable mechanism for data transfer, so doing it real-time in the Vault clients was not a good fit.</p>
<p>I went with the Vault job queue because it provides a guaranteed mechanism for the data transfer.&#0160; If it’s on the queue, I can rely on it staying on the queue until it gets properly dealt with.&#0160; If there is an error, it’s logged in the form of an error job on the queue.&#0160; If something critical goes wrong, like a power outage, the job is still on the queue in the processing state.</p>
<p>The downside is that the queue is asynchronous.&#0160; Even if you <a href="http://justonesandzeros.typepad.com/blog/2013/02/custom-jobs-without-job-processor.html" target="_blank">grab jobs directly off the queue</a> you are still relying on polling.&#0160; There is no way to instantly know when the job is queued.&#0160; The other major disadvantage is that it runs without any user interaction.&#0160; So you can’t prompt the user for information and you can’t rely on problems getting addressed immediately.</p>
<p>If you are replicating data this way, I highly suggest having some kind of re-sync button.&#0160; It’s great when developing and debugging.&#0160; It’s also doubles as a bulk-load command, which is usually a requirement for integrations like this.&#0160; Lastly, it’s a magic fix-all button for a bunch of difficult error cases, such as a user deleting data manually.&#0160; These re-sync buttons tend to be slow, but that’s OK because things are asynchronous.&#0160; Also, with the <a href="http://justonesandzeros.typepad.com/blog/2011/07/job-queue-priority.html" target="_blank">job queue priority feature</a>, you can insure that the re-sync doesn’t interfere with everyday operations.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>To summarize...</p>
<p><strong>Advantages:</strong></p>
<ul>
<li><strong>Guaranteed delivery</strong> - Jobs on the queue are guaranteed to stay there until something process them or the Vault admin removes them.</li>
<li><strong>Failure recovery</strong> - Failed jobs can be re-submitted.</li>
<li><strong>Background process</strong> - Doesn’t interfere or slow down normal operations. </li>
</ul>
<p><strong>Disadvantages:</strong></p>
<ul>
<li><strong>Conflicts</strong> - The same data may get updated at the same time in two different systems.</li>
<li><strong>Asynchronous</strong> - You have to wait before you can see results.</li>
<li><strong>Duplicate data</strong> - The same data is now in two different systems.</li>
</ul>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
