---
layout: "post"
title: "More on Hacking"
date: "2012-10-19 09:17:47"
author: "Doug Redmond"
categories:
  - "Hack"
original_url: "https://justonesandzeros.typepad.com/blog/2012/10/more-on-hacking.html "
typepad_basename: "more-on-hacking"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Hack3.png" /></p>
<p>I indicated very clearly that <a href="http://justonesandzeros.typepad.com/blog/2012/10/my-fake-job-processor.html" target="_self">my last post</a> was a hack.&#0160; I want to take a bit of time to explain exactly why it was a hack.&#0160; I’ll also point out things that were not a hack.</p>
<p>&#0160;</p>
<p><strong>Assuming I could properly emulate Job Processor      <br /></strong>Verdict:&#0160; Hack     <br />In theory, the hosting app and the plug-in are decoupled.&#0160; But the reality is that they often are not.&#0160; Unless an API provides specific support for this decoupling, you should assume a tight coupling.&#0160; The Vault API makes no claims that you can load and run Job Handlers outside the context of Job Processor.</p>
<p><strong>Using JobProcessor.exe.config to figure out the DLL and class for DWF creation      <br /></strong>Verdict: Not a hack     <br />If you’ve implemented a custom Job handler, you know that you have to edit JobProcessor.exe.config and add your own &lt;jobHandler&gt;.&#0160; So the &lt;jobHandler&gt; syntax is spelled out right in the API documentation.</p>
<p><strong>Figuring out the parameters on Autodesk.Vault.DWF.Create.ipt      <br /></strong>Verdict:&#0160; Hack     <br />Just because one job showed a single parameter, FileVersionId, doesn’t mean that all jobs of that type have the same single parameter.&#0160; There may be optional parameters not shown.&#0160; There may also be cases where certain files require additional parameters.&#0160; Again, there is no way of knowing what those cases are.&#0160; Without any official documentation, the best one can do is guess.</p>
<p><img alt="" src="/assets/Hack3-1.png" /></p>
