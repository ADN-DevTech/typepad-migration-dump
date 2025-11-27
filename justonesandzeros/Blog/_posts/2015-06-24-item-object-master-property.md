---
layout: "post"
title: "Item Object: Master Property"
date: "2015-06-24 10:27:50"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/06/item-object-master-property.html "
typepad_basename: "item-object-master-property"
typepad_status: "Publish"
---

<p><strong>Master</strong> is one property of <strong>Item</strong> object. It contains the item’s DMS ID, workspace ID, owner, additional user owners and additional group owners. In this post, we will go through each of them.</p>
<hr />
<p><strong>Item’s DMS ID</strong></p>
<p><em>item.master.dmsID</em> returns the item’s DMS ID which is read-only <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number" target="_blank">Number</a> type.</p>
<hr />
<p><strong>Item’s workspace ID</strong></p>
<p><em>item.master.workspaceID</em> returns the ID of the workspace which the item belongs to, which is read-only <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number" target="_blank">Number</a> type.</p>
<hr />
<p><strong>Item’s owner</strong></p>
<p><em>item.master.owner </em>returns an <strong>User</strong> object contains the information about that owner user, e.g. name, phone, email, license, etc. That’s all you need to know about <strong>User</strong> object at this moment. We will have a separate post to focus on it.</p>
<p>The owner property is editable, so we can update the item’s owner using script.</p>
<blockquote>
<p><em>var newOwner = Security.loadUser(‘newOwnerUserId’); <em><span style="font-size: xx-small;"><strong>// load user with id <br /></strong></span></em>item.master.owner = newOwner;&#0160; <span style="font-size: xx-small;"><strong>// update owner</strong></span></em></p>
</blockquote>
<p>Here we use<em> Security.loadUser(‘newOwnerUserId’) </em>to get a new owner user. The function takes an user ID; returns the <strong>User</strong> object representing the user. The <strong>Security</strong> object can be used to handle users, groups and roles. We will be explained in detail in the future post. In this post, we only need to know how you to use its <em>loadUser</em> function and <em>loadGroup</em> function.</p>
<p>We are also able to clone the owner from another item like this:</p>
<blockquote>
<p><em>item.master.owner = loadItem(1234).master.owner;</em></p>
</blockquote>
<hr />
<p><strong>Item’s additional user owners</strong></p>
<p><em>item.master.additionalOwners</em> gives us an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of <strong>User </strong>objects. To add additional user owners, we need to push each user object into this array.</p>
<blockquote>
<p><em>var newAdditionalUserOwner = Security.loadUser(‘UserId’); <br />item.master.additionalOwners.<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/push" target="_blank">push</a>(newAdditionalUserOwner);</em></p>
</blockquote>
<p>To remove all the additional user owners, we can do this:</p>
<blockquote>
<p><em>item.master.additionalOwners.length = 0;</em></p>
</blockquote>
<p>To find and remove particular additional user owner, we can use <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/splice" target="_blank"><em>splice</em></a>,</p>
<blockquote>
<p><em>var ownerToRemove = Security.loadUser(‘UserId’); <br /></em><em>var index = item.master.additionalOwners.indexOf(ownerToRemove); <br />if (index &gt; -1) { // <span style="font-size: 8pt;"><strong>if the owner exists in the array</strong> </span><br />&#0160;&#0160;&#0160; item.master.additionalOwners.splice(index, 1); <br /></em><em>}</em></p>
</blockquote>
<p>If we want to send email notification to all the additional user owners, we could use the code like this:</p>
<blockquote>
<p><em>var additionalOwners = item.master.additionalOwners; <br />for (var index in additionalOwners) { <br />&#0160;&#0160;&#0160; var email = new Email(); <br />&#0160;&#0160;&#0160; email.to = additionalOwners[index].email; <br />&#0160;&#0160;&#0160; email.subject = &quot;Owner Notification&quot;; <br />&#0160;&#0160;&#0160; email.body = &quot;Please update current workflow.&quot;; <br />&#0160;&#0160;&#0160; email.send(); <br />}</em></p>
</blockquote>
<hr />
<p><strong>Item’s additional group owners</strong></p>
<p><em>item.master.groupAdditionalOwners</em> returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of <strong>Group </strong>objects. We could use the same procedure as what we did for the User object above to do the add/remove/clear actions on the group array. The only difference is that we are working on <strong>Group</strong> object instead of <strong>User</strong>. To get a <strong>Group</strong> object we need use <em>Security.loadGroup(&#39;Group Name&#39;)</em>. This function takes the String of group name as input; returns a <strong>Group</strong> object.</p>
<hr />
<p><strong>Tips</strong></p>
<p>When we are writing script code, we often need to know the user ID for certain user. (The function <em>Security.loadUser(‘UserId’)</em> introduced above is an example.) But to find a user’s ID is a tricky thing, so here is an easy way to get it quickly. You need go to “Users” page first [Administration –&gt; Security –&gt; Users]. Then after clicking the user’s name under the “User Name” column, you will find the user’s ID is hidden in the address of the new window.</p>
<p><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b01b8d12b93fa970c-pi"><img alt="Capture" border="0" height="183" src="/assets/image_e36ab3.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="Capture" width="486" /></a></p>
<p>This user’s ID is “A1mMw”. Now you can use <em>Security.loadUser(‘A1mMw’) t</em>o load the <strong>User</strong> object for this user in scripting.</p>
<p>To use the function <em>Security.loadGroup(&#39;Group Name&#39;), </em>we need to know the group name. Finding group name in PLM is very straightforward. In the Groups page [Administration –&gt; Security –&gt; Groups], each of the displayed names under column “Name” is just the input variable you need for that function.</p>
<p>&#0160;</p>
<p>--Michal</p>
