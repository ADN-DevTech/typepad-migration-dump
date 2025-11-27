---
layout: "post"
title: "Script Types: Scheduled Workflow Action Script"
date: "2015-04-20 20:39:39"
author: "Michal Liu"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2015/04/script-types-supplement-scheduled-workflow-action-script-and-library-script.html "
typepad_basename: "script-types-supplement-scheduled-workflow-action-script-and-library-script"
typepad_status: "Publish"
---

<p><span style="font-size: small;">Four action script triggers (on-create, on-edit, on-demand and workflow) were introduced in the post on </span><a href="http://justonesandzeros.typepad.com/blog/2015/03/script-types-action-script-and-triggers.html" style="font-size: small;" target="_self">action script type</a><span style="font-size: small;">. There is another trigger: </span><strong>scheduled script</strong><span style="font-size: small;">, which is a subtle but very useful feature. Now let&#39;s walk through it to round off all the action script triggers.</span></p>
<p>Scheduled Workflow Action Script, as its name suggested, is an action script which is scheduled to run on a workflow state. For each workflow state in PLM, an action can be set up to perform after the configured number of days since the state is entered. Running an action script is one of the optional actions. To set up a scheduled script for a workflow state, go to: [Administration -&gt; Workspace Manager -&gt; workspace –&gt; Workflow Editor -&gt; double click a state -&gt; Escalation tab]. After enabling the escalation, configure when the escalated action will be triggered (must be in Integer days). In our case, we select “Run a script” as the type so that we are able to arrange an action script.</p>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d105fd52970c-pi"><img alt="Capture" border="0" height="399" src="/assets/image_e2fef0.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="Capture" width="452" /></a></p>
<p>Three noteworthy things about this feature:</p>
<ul>
<li>After the scheduled script is triggered, it won’t be rescheduled again until the workflow state is re-entered.</li>
<li>The scheduled script is usually used to notify certain users about current workflow state, automatically move the workflow forward and so on. But again, it is your script, so all up to you.</li>
<li>Don’t expect the timer offering a second accuracy, so this feature is not for firing a rocket.</li>
</ul>
<p>-- Michal</p>
