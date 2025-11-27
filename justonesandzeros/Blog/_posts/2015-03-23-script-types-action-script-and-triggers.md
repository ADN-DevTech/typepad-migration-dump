---
layout: "post"
title: "Script Types: Action Script And Triggers"
date: "2015-03-23 15:57:28"
author: "Michal Liu"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2015/03/script-types-action-script-and-triggers.html "
typepad_basename: "script-types-action-script-and-triggers"
typepad_status: "Publish"
---

<p>PLM Scripting supports three types of scripts: action, condition, validation. In this post, let’s focus on the <strong>action script</strong> and its triggers.</p>
<p>Action scripts can be used to create items, edit item field values, update bill of materials, perform workflow transitions and so on. In my previous post, the script we created for the <a href="http://justonesandzeros.typepad.com/blog/2015/03/plm-scripting-hello-world.html" target="_self">Hello World example</a> is an action script used to update an item’s title to “Hello World”. An action script can be divided into on-create script, on-edit script, on-demand script, and workflow script according to how it is triggered.</p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d0f27c6d970c-pi" style="display: inline;"><img alt="Untitled" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01b8d0f27c6d970c image-full img-responsive" height="5" src="/assets/image_7b1ce9.jpg" style="width: 1792px;" title="Untitled" /></a></p>
<h3>Four kinds of action scripts</h3>
<h4><strong>On-create script:</strong></h4>
<ul>
<li>A on-create script will be triggered after an item is created (by either UI or a script).</li>
<li>A workspace can only have one on-create script.</li>
<li>An action script can be assigned to multiple workspaces as the on-create script.</li>
</ul>
<h4><strong>On-edit script:</strong></h4>
<ul>
<li>A on-edit script will be triggered after any field value of an item (under Item Details tab) is modified.</li>
<li>The changes made on the other tabs (Project Management, Relationships…) won’t trigger on-edit script.</li>
<li>A workspace can only have one on-edit script.</li>
<li>An action script can be assigned to multiple workspaces as the on-edit script.</li>
</ul>
<h4><strong>On-demand script: </strong></h4>
<ul>
<li>An on-demand script will be triggered by clicking the script icon in item’s page.</li>
<li>The script icon is available only if the current item is not locked (which means there are workflow actions available for the current user).</li>
<li>One workspace can only have multiple on-demand scripts.</li>
<li>An action script can be assigned to multiple workspaces as on-demand script.</li>
</ul>
<h4><strong>Workflow script:</strong></h4>
<ul>
<li>A workflow action script will be triggered when a workflow action is performed on the transition which the script is assigned.</li>
<li>A workflow transition can only have one workflow action script.</li>
<li>An action script can be assigned to multiple workflow transitions.</li>
</ul>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01bb080cebf1970d-pi" style="display: inline;"><img alt="Untitled" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01bb080cebf1970d image-full img-responsive" height="5" src="/assets/image_d29612.jpg" title="Untitled" width="1830" /></a></p>
<h3>Set up triggers</h3>
<p>The action script in “Hello World” example is&#0160;an on-demand script. In that post, we had seen how to configure the script into a workspace as on-demand script. The on-create and on-edit scripts can be set up in the same configuration page as below. [Administration -&gt; Workspace Manager -&gt; workspace -&gt; Behaviors]</p>
<p>&#0160; <img alt="" src="/assets/WorkspaceAdmin.png" /><br /><br /></p>
<p>The workflow action script can be set up for a transition in the Workflow Editor page as below. [Administration -&gt; Workspace Manager -&gt; workspace –&gt; Workflow Editor]</p>
<p><img alt="" src="/assets/WorkflowEditor.png" /></p>
<p>-- Michal</p>
