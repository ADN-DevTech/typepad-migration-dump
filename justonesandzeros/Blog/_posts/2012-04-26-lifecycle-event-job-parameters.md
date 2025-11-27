---
layout: "post"
title: "Lifecycle Event Job Parameters"
date: "2012-04-26 16:25:50"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/04/lifecycle-event-job-parameters.html "
typepad_basename: "lifecycle-event-job-parameters"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>File this post under: “Things that Doug should have put in the SDK documentation.”    <br />I’m not sure how this information was omitted, but I apologize.</p>
<p>Anyway, when jobs are created from a lifecycle state change, the job will have a specific set of parameters depending on the entity type.&#0160; Here is the list of parameters.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Vault 2012</strong></p>
<ul>
<li>Files       
<ul>
<li>FileId - The ID of the updated file. </li>
<li>LifeCyleTransitionId (<em>sic</em>) - The ID of the lifecycle transition that the file just went through. </li>
</ul>
</li>
<li>Items       
<ul>
<li>ItemId - The ID of the updated item. </li>
<li>FromStateId - The state that the item started in. </li>
<li>ToStateId - The state that the item ended in. </li>
</ul>
</li>
<li>Change Orders       
<ul>
<li>ChangeOrderId - The ID of the updated change order. </li>
<li>FromStateId - The state that the change order started in. </li>
<li>ToStateId - The state that the change order ended in. </li>
<li>ActivityId - The activity defining the transition. </li>
</ul>
</li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Vault 2013</strong></p>
<p>The main change here is that the file lifecycle engine in 2012 has been expanded to other entity types.&#0160; So file-specific parameters have been changed to the more generic entity concept.</p>
<ul>
<li>Files, Folders and Custom Entities       
<ul>
<li>EntityId - The ID of the updated file, folder or custom entity. </li>
<li>EntityClassId - They type of entity that was updated. </li>
<li>LifeCycleTransitionId - The ID of the lifecycle transition that the file just went through. </li>
<li>LifeCyleTransitionId <strong>[deprecated]</strong> - Use Life<strong>Cycle</strong>TransitionId instead. </li>
<li>FileId <strong>[deprecated]</strong> - Use EntityId instead. </li>
</ul>
</li>
<li>Items       
<ul>
<li>ItemId - The ID of the updated item. </li>
<li>FromStateId - The state that the item started in. </li>
<li>ToStateId - The state that the item ended in. </li>
</ul>
</li>
<li>Change Orders       
<ul>
<li>ChangeOrderId - The ID of the updated change order. </li>
<li>FromStateId - The state that the change order started in. </li>
<li>ToStateId - The state that the change order ended in. </li>
<li>ActivityId - The activity defining the transition. </li>
</ul>
</li>
</ul>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
