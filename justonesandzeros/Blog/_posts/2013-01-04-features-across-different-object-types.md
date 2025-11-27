---
layout: "post"
title: "Features across different object types"
date: "2013-01-04 08:55:04"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2013/01/features-across-different-object-types.html "
typepad_basename: "features-across-different-object-types"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Let’s start off with a quick quiz.&#0160; Look at the image and answer the following questions:    <br />Are the file and the folder in the same category?     <br />Are the file and the folder in the same lifecycle state?</p>
<p><img alt="" src="/assets/Grid.png" /></p>
<p>Answers:&#0160; <strong>No</strong>, they are in different categories, and <strong>yes</strong>, they are in the same lifecycle state.</p>
<p>It can be a bit confusing when you have concepts that span multiple objects types.&#0160; Sometimes you can share behavior across different types and sometimes you can’t.&#0160; In this article, I’ll attempt to explain what the scope of each feature.</p>
<p>Custom entities make things more tricky.&#0160; In case you don’t know already, all custom entities are part of the same entity class (CUSTENT).&#0160; When you create your own custom entity definition, it’s actually a <strong>sub-type</strong> of CUSTENT.&#0160; So that adds another level of complexity when defining scope.</p>
<p><img alt="" src="/assets/SubTypes.png" /></p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Let me go down feature-by-feature and explain the scope...</p>
<p><strong>Categories</strong> - A category is specific to an entity class.&#0160; It cannot be shared across different entity classes.&#0160; So a File and a Folder can never have the same category.&#0160; You can give set the display names the same on a File and Folder categories, like in my quiz, but they are still different underlying objects.</p>
<p>Categories are applied at the entity class level.&#0160; So, CUSTENT categories can be used by any of the custom entity sub-types.&#0160; So it is possible for a Foo object and a Bar object to have the same category.</p>
<p><strong>Property Definitions</strong> - A property definition can span multiple entity classes.&#0160; If you go to the admin settings, you will see that it’s common for Files and Folders to share a property definition.&#0160; Categories can provide additional behavior, but it’s not required.&#0160; If a Property Definition is associated to the FILE entity class, then any file object can have that property, regardless of category.</p>
<p>If a property definition is associated to the CUSTENT entity class, then it’s available to all sub-types.&#0160; There is no way to have a property definition specifically for Foo objects.</p>
<p><strong>Searching</strong> - A search is specific to an entity class.&#0160; This is pretty obvious at the API level since the functions are named FindXBySearchConditions, where the X is the entity class. </p>
<p>For custom entities, a search will span all CUSTENT objects, regardless of sub-types.&#0160; So it’s possible for a single search to return both Foo and Bar objects.&#0160; Most of the time, you don’t want this, so you need to add a search condition that narrows the search to a specific sub-type.</p>
<p><strong>Lifecycle Definitions</strong> - Files, folders and custom entities all share the same lifecycle engine.&#0160; Any lifecycle definition can be used across these 3 entity types and any custom object sub-types.&#0160; The only restriction is that the entity must be in a category that is associated with the lifecycle definition.</p>
<p><strong>Revision Schemes</strong> - They work similar to lifecycle definitions.&#0160; You can share them across different entity classes, but the entity must be in a category that is associated with the lifecycle definition.</p>
<p><strong>Roles and Permissions</strong> - The role usually mentions the entity class directly, such as Item Editor (Level 1).&#0160; So it’s pretty clear that roles are specific to an entity class.&#0160; </p>
<p>For custom entities, a role applies to all custom entity sub types.&#0160; So somebody with Custom Object Editor (Level 1), can edit both “foo” and “bar” objects.&#0160; If you want the user to be able to edit “foo” but not “bar”, you need to give that user the edit role, then use ACL security to restrict edit on “bar” objects.</p>
<p><strong>Custom Commands</strong> - These can be bound to a specific entity type.&#0160; In the case of custom entities, they can be bound to specific to a sub-type.&#0160; A command can work with to different types/sub-types by binding the command to multiple command sites.&#0160; Another option is to specify null for the NavigationTypes property, which has the effect of making the command always available.</p>
<p><strong>Custom Tab Views</strong> - These are always bound to a specific entity type or custom entity sub-type.&#0160; You can’t share a file tab with an item tab or a “foo” tab with a “bar” tab.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Speaking of bar tabs, it’s payday and my shift is over.&#0160; I’ll leave you with this this handy chart, summarizing everything.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="156">&#0160;</td>
<td valign="top" width="156"><strong>Entity class scope</strong></td>
<td valign="top" width="156"><strong>Custom entity            <br />sub-type scope</strong></td>
</tr>
<tr>
<td valign="top" width="156"><strong>Categories</strong></td>
<td valign="top" width="156">Specific to a single entity class</td>
<td valign="top" width="156">Always shared across all sub-types</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Property Definitions</strong></td>
<td valign="top" width="156">May be shared with multiple entity classes</td>
<td valign="top" width="156">Always shared across all sub-types</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Searching</strong></td>
<td valign="top" width="156">Specific to a single entity class</td>
<td valign="top" width="156">Always shared across all sub-types</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Lifecycle Definitions</strong></td>
<td valign="top" width="156">May be shared with multiple entity classes          <br />(dependent on category)</td>
<td valign="top" width="156">May be shared with multiple sub-types          <br />(dependent on category)</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Revision Schemes</strong></td>
<td valign="top" width="156">May be shared with multiple entity classes          <br />(dependent on category)</td>
<td valign="top" width="156">N/A</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Roles and Permissions</strong></td>
<td valign="top" width="156">Specific to a single entity class</td>
<td valign="top" width="156">Always shared across all sub-types</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Custom Commands</strong></td>
<td valign="top" width="156">May be shared with multiple entity classes</td>
<td valign="top" width="156">May be shared with multiple sub-types</td>
</tr>
<tr>
<td valign="top" width="156"><strong>Custom Tab Views</strong></td>
<td valign="top" width="156">Specific to a single entity class</td>
<td valign="top" width="156">Specific to a single sub-type</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
