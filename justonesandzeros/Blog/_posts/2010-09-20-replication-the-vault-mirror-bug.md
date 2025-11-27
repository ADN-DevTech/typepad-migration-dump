---
layout: "post"
title: "Replication - The Vault Mirror Bug"
date: "2010-09-20 15:03:00"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/09/replication-the-vault-mirror-bug.html "
typepad_basename: "replication-the-vault-mirror-bug"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>Replication is one of those &quot;game changer&quot; features.&#0160; It&#39;s a very powerful feature, but it has the potential to impact any customizations you write.&#0160; Code that works perfectly well in a single workgroup environment may behave incorrectly in a multi-workgroup environment.&#0160; Even worse, the incorrect behavior may be intermittent, making it hard to isolate what the exact problem is.</p>
<p>To illustrate my point, I&#39;d like to go over a bug that can show up when running <a href="http://justonesandzeros.typepad.com/blog/2010/01/vault-mirror.html" target="_blank">Vault Mirror</a> in a multi-workgroup environment.&#0160; First, let me quickly go over how the Partial Mirror command works.&#0160; When you sync data in Vault Mirror, it remembers the time.&#0160; When you run a Partial Mirror command later, it does a search for all new files since the date of the last sync.&#0160; This way it only downloads down the new files.&#0160;</p>
<p>On a single workgroup, Partial Mirror works perfectly.&#0160; It will always find the all the new files added.&#0160; However in a replicated environment, it may miss files.&#0160; In these cases there are no errors or messages.&#0160; It just doesn&#39;t download all the files it&#39;s supposed to.</p>
<p>Let me describe the issue in detail.&#0160; In this example, there are 2 workgroups, A and B.&#0160; Vault Mirror is on workgroup A and is set to run a Partial Mirror every minute.&#0160; Also, in this example, a minute is about how long it takes to replicate SQL data between A and B.&#0160; Let&#39;s now add a file to workgroup B and see if Vault Mirror detects the new file.&#0160; Here is an example timeline:</p>
<ul>
<li>4:05:00 – Partial Mirror runs on site A. Scans for files with a CreateDate between 4:04:00 and 4:05:00.   <br /><span style="color: #800080;">Result: No hits</span> </li>
<li>4:05:32 – File X added to site B. </li>
<li>4:06:00 – Partial Mirror runs on site A. Scans for files with a CreateDate between 4:05:00 and 4:06:00.   <br /><span style="color: #800080;">Result: No hits</span> </li>
<li>4:06:28 – File X replicated to site A. CreateDate is still 4:05:32. </li>
<li>4:07:00 – Partial Mirror runs on site A. Scans for files with a CreateDate between 4:06:00 and 4:07:00.   <br /><span style="color: #800080;">Result: No hits</span> </li>
</ul>
<p>Did you see what happened there?&#0160; Because there is a delay replicating the file data from B to A, Vault Mirror was not able to detect the new file.&#0160; Another way to look at is is that the CreateDate doesn&#39;t accurately reflect the time when the file was added to the database.&#0160; There is not data field that will tell you the time of replication.</p>
<p>&#0160;</p>
<p><strong>How to fix</strong></p>
<p>In my mind Vault Mirror already has a fix, the Full Mirror command.&#0160; It will catch any files that slip through the cracks.&#0160; In other words, if you run Full Mirror on Site A at 4:05, it&#39;s guaranteed to pick up all the files that are on Site A at 4:05.</p>
<p>Another solution is to construct 2 queries, one for replicated data and one for local data.&#0160; <a href="http://justonesandzeros.typepad.com/blog/2010/05/watch-folder.html" target="_blank">Watch Folder</a> has this same bug, and I used this approach to fix.&#0160; The program remembers 2 sets of dates and uses them to run 2 separate queries.&#0160; Query 1:&#0160; Remember the last time the command was run, do a search on all new files owned by local workgroup since the time of the last command.&#0160; Query 2:&#0160; Remember the greatest CreateDate of a replicated file, do a search on all files not owned by the local workgroup since the saved date.</p>
<p>The second solution is not 100%, but I thought it was good enough for Watch Folder.&#0160; The second solution incorrectly assumes that each workgroup replicates at the same time.&#0160; Transferring ownership manually on files will also screw up this algorithm.</p>
<p>&#0160;</p>
<p><strong>Conclusion</strong></p>
<p>These are the only 2 solutions I can think of at the moment, but I&#39;m sure there are more.&#0160; The bigger concern is that there other potential bugs that can happen in a replicated environment.&#0160; This is just an example of one such bug, and the purpose is to get you thinking in terms of replicated data.</p>
<p>The best way to locate and fix these issues is to <a href="http://justonesandzeros.typepad.com/blog/2010/08/replication---testing-on-a-single-server.html" target="_blank">test on a replicated environment</a>.&#0160; I don&#39;t think it&#39;s possible to catch these issues on a single workgroup.</p>
