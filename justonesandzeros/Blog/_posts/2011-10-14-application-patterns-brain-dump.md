---
layout: "post"
title: "Application Patterns - Brain Dump"
date: "2011-10-14 08:33:18"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/10/application-patterns-brain-dump.html "
typepad_basename: "application-patterns-brain-dump"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Usually, I try to present information in a clear, organized manner.&#0160; However, that&#39;s not the way I work on a day to day basis.&#0160; My thoughts are usually organized in brain dumps.&#0160; Basically it&#39;s a bunch of idea fragments loosely tied around a central concept.</p>
<p>As the Vault API matures, certain patterns emerge on how add-ins should work.&#0160; I&#39;ve been making notes on these patterns, but I don&#39;t have anything well organized yet.&#0160; So here is my current brain dump.&#0160; This is a bit of an experiment.&#0160; Maybe some people will find this useful. Or maybe not, in which case I&#39;ll abandon the format in future posts.</p>
<p><strong>Application Patterns      <br /></strong>What are the common patterns in Vault customizations?</p>
<p>Admin-specific vs user-specific features:</p>
<ul>
<li>Customizations usually have a mix of commands, some for the Vault administrator and some for the end user. </li>
<li>Admin commands are usually related to configuration.</li>
<li>User commands are usually the things that do the real work. </li>
<li>Admin steps are sometimes needed before user features are available.      <br />For example, Vault Web View can&#39;t be used until the Vault admin designates a URL property. </li>
<li>Admin settings are best stored in the vault.&#0160; Vault Options are a good way of doing this.</li>
<li>User settings are best stored on the local computer.</li>
</ul>
<p>Other:</p>
<ul>
<li>Custom tabs are always shown for a given object type.&#0160; This aspect limits it&#39;s use.&#0160; For example, a tab that shows layer information about a DWG is useless for every other file type, resulting in UI clutter.</li>
<li>Custom jobs are usually what you use when you want an operation to happen &quot;on the server&quot;</li>
<li>Job processor extensions are usually double sided.&#0160; You have the customization that adds the job to the queue and you have the job handler that executes the job.&#0160; These two components will almost always have code to share. </li>
<li>It doesn&#39;t matter how cool your blog is or how many emails you send out,&#0160; you still have to keep reminding people that you already wrote an app for that. </li>
</ul>
<p>Got more to add?&#0160; Drop me a comment.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
