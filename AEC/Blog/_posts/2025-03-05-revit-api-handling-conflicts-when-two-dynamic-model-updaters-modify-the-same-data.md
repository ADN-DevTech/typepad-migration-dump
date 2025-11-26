---
layout: "post"
title: "Revit API: Handling Conflicts When Two Dynamic Model Updaters Modify the Same Data"
date: "2025-03-05 23:16:26"
author: "Naveen Kumar"
categories:
  - ".NET"
  - "Naveen Kumar"
  - "Revit"
original_url: "https://adndevblog.typepad.com/aec/2025/03/revit-api-handling-conflicts-when-two-dynamic-model-updaters-modify-the-same-data.html "
typepad_basename: "revit-api-handling-conflicts-when-two-dynamic-model-updaters-modify-the-same-data"
typepad_status: "Publish"
---

<p>
<script src="https://cdn.jsdelivr.net/gh/google/code-prettify@master/loader/run_prettify.js?skin=sunburst"></script>
</p>
<p>By <a href="https://adndevblog.typepad.com/aec/Naveen-Kumar.html" target="_self">Naveen Kumar</a><br /><br /><span style="font-family: georgia, palatino;">Let&#39;s say I have two updaters—one created by the user and the other by the vendor. Both updaters modify the same parameter within an element whenever a change occurs. Since they operate on the same parameter, conflicts may arise due to simultaneous or overlapping modifications, leading to an internal error. The Revit journal logs the issue as follows:</span></p>
<pre class="prettyprint">Dynamic Updater with ID xxxxx touched atom ((Element, 147536, -1)) that was previously touched by another updater with ID xxxxx.</pre>
<p data-pm-slice="1 1 []"><span style="font-family: georgia, palatino;">If you work with Revit APIs and automation, managing multiple updaters can be a complex and challenging task. So, let&#39;s break this down and see what can be done.</span></p>
<h3 data-pm-slice="1 1 []"><span style="font-size: 13pt; font-family: georgia, palatino;"><strong>What Was Tried (But didn&#39;t work)</strong></span></h3>
<p data-pm-slice="1 3 []"><span style="font-family: georgia, palatino; font-size: 11pt;">Several approaches were attempted to fix the issue:</span></p>
<ul data-spread="false">
<li>
<p><span style="font-family: georgia, palatino; font-size: 11pt;"><strong>Changing ChangePriority values</strong> – The idea was to set different priority levels so Revit could process one before the other. No luck.</span></p>
</li>
<li>
<p><span style="font-family: georgia, palatino; font-size: 11pt;"><strong>Using UpdaterRegistry.SetExecutionOrder</strong> – This should have controlled the sequence of execution, but the error still happened.</span></p>
</li>
<li>
<p><span style="font-family: georgia, palatino; font-size: 11pt;"><strong>Using an ExternalEvent</strong> – This workaround technically worked, but it’s not ideal. It creates unnecessary or unexpected transactions that the user can undo, potentially leaving the model in an inconsistent state.</span></p>
</li>
</ul>
<h3 data-pm-slice="1 1 []"><span style="font-size: 13pt; font-family: georgia, palatino;"><strong>Why This Happens?</strong></span></h3>
<p data-end="1782" data-start="1531"><span style="font-family: georgia, palatino;">Revit enforces this restriction to prevent multiple updaters from making conflicting changes to the same data within an element. If both updaters modify the same parameter, there&#39;s no way around it—both can&#39;t run without interfering with each other.</span></p>
<p data-end="2061" data-start="1784"><span style="font-family: georgia, palatino;">For developers, this is a nightmare because you don’t want to be in a situation where your updater &quot;stops working&quot; because another vendor’s add-in is overwriting your data. Revit seems to assume that a single vendor should manage all updates to an element to avoid conflicts.</span></p>
<h3 data-pm-slice="1 1 []"><span style="font-size: 13pt; font-family: georgia, palatino;"><strong>What&#39;s The Best Solution?</strong></span></h3>
<p><span style="font-family: georgia, palatino;">If you ever run into this situation, here’s what you can do:</span></p>
<ul data-spread="false">
<li>
<p><span style="font-family: georgia, palatino;"><strong>Check if both updaters modify the same parameter.</strong> If they do, they are fundamentally incompatible.</span></p>
</li>
<li>
<p><span style="font-family: georgia, palatino;"><strong>Talk to the third-party vendor.</strong> There may be a way to tweak how their updater works so it doesn’t conflict with yours.</span></p>
</li>
<li>
<p><span style="font-family: georgia, palatino;"><strong>Disable one updater if possible.</strong> Depending on the project, you might be able to work around the issue by limiting when one of them runs.</span></p>
</li>
</ul>
<p><span style="font-family: georgia, palatino;">Ultimately, the cleanest solution is to either use a single add-in to modify the specific data or work with the vendor to develop a custom solution that integrates the necessary features of both add-ons without conflicts. Otherwise, expect compatibility issues to keep popping up.</span></p>
