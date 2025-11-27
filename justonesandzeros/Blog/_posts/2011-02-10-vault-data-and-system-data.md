---
layout: "post"
title: "Vault Data and System Data"
date: "2011-02-10 08:46:28"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/02/vault-data-and-system-data.html "
typepad_basename: "vault-data-and-system-data"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts2.png" /></p>
<p>If you have worked with Vault for a while, you may have noticed that certain data is specific to a vault and some data applies to all vaults.&#0160; I&#39;ll refer to the second type as System Data.&#0160;</p>
<p>As much as I hate to go into the database details, it&#39;s the only way to fully understand this topic.&#0160; Each vault gets its own database, but there is also another database, called KnowledgeVaultMaster (KVM), which manages all the vaults.&#0160; This database design is what distinguishes between system data and vault data.&#0160; <strong>If</strong> <strong>it&#39;s in the KVM, it&#39;s system data</strong>.&#0160; <strong>If it&#39;s in a Vault database, it&#39;s vault data</strong>.&#0160; That&#39;s the critical difference, and it shows itself in many aspects of the Vault framework.</p>
<p>One of the more obvious places is the Administration sub-menu in Vault Explorer.&#0160; There are two options, one for vault-specific data and one for system data (aka. global data).    <br /><img alt="" src="/assets/menu.png" /></p>
<p>Another example is the SignIn and SignIn2 functions in the SecurityService.&#0160; The first one is for logging in to a specific vault database and the second one is for logging in to the KVM.&#0160;</p>
<p>If you are in a replicated environment and you want to get admin ownership, you need to call GetDatabaseOwnership and specify if you want to own a vault database or the KVM.</p>
<p>Here is a basic breakdown of which data is in which database.</p>
<p><strong>Vault-specific data:</strong></p>
<ul>
<li>Files </li>
<li>Folders </li>
<li>Items </li>
<li>Change Orders </li>
<li>Properties </li>
<li>Categories </li>
<li>Lifecycles </li>
<li>ACLs </li>
<li>Jobs </li>
</ul>
<p><strong>System data:</strong></p>
<ul>
<li>Users </li>
<li>Groups </li>
<li>Roles </li>
<li>Job Queue </li>
</ul>
<p><br />The job queue is a weird one.&#0160; The queue itself is maintained by the KVN, but each job is associated to a specific vault.&#0160; So you need to be logged in to a vault to add the job, but you can be logged in to the KVM for reading the queue.&#0160; If you are viewing the job queue in Vault Explorer, you are seeing jobs from all vaults.   <br /><img alt="" src="/assets/JobQueue.png" /> <br />You can customize the columns if you want to show the associated vault.</p>
