---
layout: "post"
title: "Script Types: Condition And Validation Scripts"
date: "2015-04-10 12:42:24"
author: "Michal Liu"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2015/04/script-types-condition-and-validation-script.html "
typepad_basename: "script-types-condition-and-validation-script"
typepad_status: "Publish"
---

<p>In the last post, we talked about action script. Today, let&#39;s focus on the other two types of scripts: <strong>condition</strong> and <strong>validation scripts</strong>.</p>
<p>Unlike action scripts, both condition and validation scripts only serve workflow transitions.&#0160; The workflow transition properties is the only place to setup the references to them. [Administration -&gt; Workspace Manager -&gt; workspace –&gt; Workflow Editor -&gt; double click transition]</p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d0fca862970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Capture" class="asset  asset-image at-xid-6a0120a5728249970b01b8d0fca862970c img-responsive" src="/assets/image_8fda15.jpg" title="Capture" /></a></p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01bb0816fd10970d-pi" style="display: inline;"><img alt="6a0120a5728249970b01bb080cebf1970d-800wi" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01bb0816fd10970d image-full img-responsive" src="/assets/image_e0950a.jpg" title="6a0120a5728249970b01bb080cebf1970d-800wi" /></a></p>
<h3><strong>Condition scripts</strong></h3>
<ul>
<li>Condition scripts are read-only scripts. The changes made in code won&#39;t be saved.</li>
<li>A condition script needs to return a Boolean value (true or false) using predefined function<em> returnValue()</em>.</li>
<li>The returned value is used to tell whether an outgoing workflow transition from current state is available to the user to perform or not, but it is not the only prerequisite to make a transition available. The user must also have the workflow permission of the transition.</li>
<li>Condition scripts are triggered whenever PLM needs to determine if a workflow transition is available to a user.</li>
<li>Each transition can only have one condition script.</li>
<li>Each condition script can be referred by multiple transitions from different workspaces.</li>
</ul>
<h3><strong>Condition script examples</strong></h3>
<p>Example 1: Transition is only available to the owner of item.</p>
<p style="padding-left: 30px;"><em>// <span style="font-size: 10pt;"><strong><span style="font-size: 8pt;">get current item&#39;s owner</span></strong>.</span></em><br /><em><span style="font-size: 11pt;"> var owner = item.master.owner; </span></em><br /><em> // <strong><span style="font-size: 8pt;">if the owner&#39;s id is equal to the current user&#39;s id, then return true; otherwise false.</span></strong></em><br /><span style="font-size: 10pt;"><em> returnValue(owner.id === userID ? true : false);</em></span></p>
<p style="text-align: left;">Example 2: Transition is only available to the users in &quot;Admin Group&quot;</p>
<p style="padding-left: 30px;"><em>// <span style="font-size: 8pt;"><strong>Security.inGroup(userId, groupName) is built-in function checking if certain user is in certain group.</strong></span></em><br /><em> var isCurrentUserInAdminGroup = Security.inGroup(userID, &#39;Admin Group&#39;); </em><br /><em> // <span style="font-size: 8pt;"><strong>return the checking result</strong></span></em><br /><em> returnValue(isCurrentUserInAdminGroup);</em></p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d0fca9f2970c-pi" style="display: inline;"><img alt="6a0120a5728249970b01bb080cebf1970d-800wi" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01b8d0fca9f2970c image-full img-responsive" src="/assets/image_04f718.jpg" title="6a0120a5728249970b01bb080cebf1970d-800wi" /></a></p>
<h3><strong>Validation script</strong></h3>
<ul>
<li>Validation scripts are also read-only.&#0160; The changes made in code won&#39;t be saved.</li>
<li>A validation script is used to validate certain prerequisites before allowing transitioning to the next workflow state.</li>
<li>A validation script should return an array of failing messages using predefined function<em> returnValue()</em>.</li>
<li>The transitioning will be allowed if an empty array is received; otherwise, the user won&#39;t be allowed to perform the transitioning and the returned messages will be shown to the user.</li>
<li>Each transition can only have one validation script.</li>
<li>Each validation script can be referred by multiple transitions from different workspaces.</li>
</ul>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b7c77319df970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="222" class="asset  asset-image at-xid-6a0120a5728249970b01b7c77319df970b img-responsive" src="/assets/image_9486e7.jpg" title="222" /></a></p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d0fcacf5970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Dddd" class="asset  asset-image at-xid-6a0120a5728249970b01b8d0fcacf5970c img-responsive" src="/assets/image_32f2b1.jpg" title="Dddd" /></a></p>
<h3><strong>Validation script example</strong></h3>
<p>Example: Item&#39;s &quot;name&quot; field must not be empty, and item must have an attachment at least.</p>
<p style="padding-left: 30px;"><span style="font-size: 10pt;"><em>var messages = [];</em></span><br /><span style="font-size: 10pt;"><em> if (item.NAME === null) {&#0160;&#0160;&#0160;&#0160;// <span style="font-size: 8pt;"><strong>name field must not be empty</strong></span></em></span><br /><span style="font-size: 10pt;"><em> &#0160;&#0160;&#0160;&#0160;messages.push(&quot;Production&#39;s name is required to complete the transition&quot;);</em></span><br /><span style="font-size: 10pt;"><em> }</em></span><br /><span style="font-size: 10pt;"><em> if (item.attachments.length === 0) {&#0160;&#0160;&#0160;&#0160;// <span style="font-size: 8pt;"><strong>must have one attachment at least</strong></span></em></span><br /><span style="font-size: 10pt;"><em> &#0160;&#0160;&#0160;&#0160;messages.push(&quot;At least one attachment is required to complete the transition&quot;);</em></span><br /><span style="font-size: 10pt;"><em> }</em></span><br /><span style="font-size: 10pt;"><em> returnValue(messages);</em></span></p>
<p><a class="asset-img-link" href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01bb0816fe2d970d-pi" style="display: inline;"><img alt="6a0120a5728249970b01bb080cebf1970d-800wi" border="0" class="asset  asset-image at-xid-6a0120a5728249970b01bb0816fe2d970d image-full img-responsive" src="/assets/image_86776e.jpg" title="6a0120a5728249970b01bb080cebf1970d-800wi" /></a></p>
<h3><strong>Scripts in workflow transition lifecycle</strong></h3>
<p>Here is how all 3 script types are used in a workflow transition:</p>
<ol>
<li>Every <strong>condition script</strong> on the outgoing transitions of current workflow state runs to determine which transitions are available for the current user.</li>
<li>User chooses an available transition to perform workflow action.</li>
<li>The <strong>validation script</strong> for that transition runs to validate all the prerequisites.</li>
<li>If all the prerequisites are met, the <a href="http://justonesandzeros.typepad.com/blog/2015/03/script-types-action-script-and-triggers.html" target="_self"><strong>action script</strong></a> on that transition runs.</li>
<li>The transition is completed.</li>
</ol>
<p>-- Michal</p>
