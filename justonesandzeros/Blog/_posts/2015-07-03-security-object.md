---
layout: "post"
title: "Security Object"
date: "2015-07-03 13:24:15"
author: "Michal Liu"
categories:
  - "PLM 360"
original_url: "https://justonesandzeros.typepad.com/blog/2015/07/security-object.html "
typepad_basename: "security-object"
typepad_status: "Publish"
---

<p>In the last two posts, we saw how to access item’s <a href="http://justonesandzeros.typepad.com/blog/2015/05/item-object-fields.html" target="_blank">fields</a> and <a href="http://justonesandzeros.typepad.com/blog/2015/06/item-object-master-property.html" target="_blank">ownership</a> information. We are not done talking about <strong>Item</strong> object. There are still many things we would like to cover. But before then, I am going to wrap up another object which was mentioned when we were introducing the <strong><a href="http://justonesandzeros.typepad.com/blog/2015/06/item-object-master-property.html" target="_blank">Master</a></strong> property in <strong>Item</strong>. That’s the <strong>Security</strong> object. The <strong>Security</strong> object contains some utility functions dealing with users, groups and roles. And we have seen the examples using its <em>loadUser()</em> and <em>loadGroup()</em> functions. We are going to see what else the <strong>Security</strong> object has.</p>
<hr />
<p><strong>Security.inGroup()</strong></p>
<p><em>Security.inGroup(userId, groupName) </em>returns a <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Boolean" target="_blank">Boolean</a> object to indicate if the group specified by <em>groupName </em>(<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank">String</a>) contains the user specified by <em>userId</em> (<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank">String</a>). For example,</p>
<blockquote>
<p><em>if (Security.inGroup(userID, ‘Admin Group’)) { <br />&#0160;&#0160;&#0160; // the current user is in Admin Group&#0160;&#0160;&#0160; <br />} else { <br />&#0160;&#0160;&#0160; // the current user is NOT in Admin Group <br />}</em></p>
</blockquote>
<p style="text-align: left;">Notes: The <em>userID</em> is one of the pre-loaded variables for each script indicating the user whose action triggers the script. Usually it is the current login user’s ID.</p>
<hr />
<p><strong>Security.inRole()</strong></p>
<p><em>Security.inRole(userId, roleName)</em> checks if the user specified by the <em>userId</em> is in the role specified by <em>roleName </em>(<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank">String</a>) by returning a <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Boolean" target="_blank">Boolean</a> object. The <em>roleName</em> is the displayed name for each role in Roles page [Administration –&gt; Security –&gt; Roles].</p>
<hr />
<p><strong>Security.loadUser() </strong></p>
<p><em>Security.loadUser(userId)</em> returns the <strong>User</strong> specified by the userId. If no user can be found with the <em>userId</em>, then an empty <strong>User</strong> will be returned. For example,</p>
<blockquote>
<p><em>var user = Security.loadUser(‘Abc’); <br />if (user.id === null) { <br />&#0160; println(“The user cannot be found.”); <br />}</em></p>
</blockquote>
<hr />
<p><strong>Security.loadGroup()</strong></p>
<p><em>Security.loadGroup(groupName) </em>returns the <strong>Group</strong> specified by the <em>groupName</em>. Like <em>loadUser</em> if no group cannot be found with the <em>groupName</em>, an empty <strong>Group</strong> object will be returned.</p>
<hr />
<p><strong>Security.listGroups() </strong></p>
<p><em>Security.listGroups(userId)</em> returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of group names (<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank">String</a>) which the user belongs to.</p>
<hr />
<p><strong>Security.listRoles() </strong></p>
<p><em>Security.listRoles(userId) </em>returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of role names (<a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String" target="_blank">String</a>) which the user has. Roles don’t have direct relationship with users. Group is the middle connector with them. So to get all the roles for a user, first the server will find all the groups the user belongs to. Next all the roles in each of those groups will be collected. Then after removing all the duplicates (one role could be added into multiple groups), the collection will be returned to you.</p>
<hr />
<p><strong>Security.listUsersInGroup() </strong></p>
<p><em>Security.listUsersInGroup(groupName) </em>returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of users (<strong>User</strong> objects) that in the group.</p>
<hr />
<p><strong>Security.listUsersInRole() </strong></p>
<p><em>Security.listUsersInRole(roleName) </em>returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of users (<strong>User</strong> objects) that in the role.</p>
<hr />
<p><strong>Security.searchUsers()</strong></p>
<p><em>Security.searchUsers(criteria) </em>returns an <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array" target="_blank">Array</a> of users (<strong>User</strong> objects) matching the criteria. For example, the below function will return all the users with professional licenses and located in Toronto.</p>
<blockquote>
<p><em>function getAllProfessionalUsersInToronto() { <br />&#0160;&#0160;&#0160; var criteria = { <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; city : &#39;Toronto&#39;, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; licenseCode: &#39;S&#39; <br />&#0160;&#0160;&#0160; }; <br />&#0160;&#0160;&#0160; return Security.searchUsers(criteria); <br />}</em></p>
</blockquote>
<p>--Michal</p>
