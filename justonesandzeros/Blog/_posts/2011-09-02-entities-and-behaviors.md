---
layout: "post"
title: "Entities and Behaviors"
date: "2011-09-02 12:19:06"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/09/entities-and-behaviors.html "
typepad_basename: "entities-and-behaviors"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Last year, I tried to explain the Entity concept in the Vault API, and I never felt that I did a good job.&#0160; Read <a href="http://justonesandzeros.typepad.com/blog/2010/05/entities.html">the article</a> at your own peril.&#0160; In true software developer form, here is the hotfix to my bug-filled earlier article.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>There are 2 concepts in the Vault API:&#0160; Entities and Behaviors.&#0160; The two concepts are intertwined; you can&#39;t describe one without describing the other.</p>
<p>An <strong>Entity</strong> is an <span style="text-decoration: underline;"><em>object</em></span> that can have behaviors on it.     <br />A <strong>Behavior</strong> is a <span style="text-decoration: underline;"><em>feature</em></span> that can be applied to entities.</p>
<p>The driver behind these concepts is something every programmer should already be familiar with: re-use.&#0160; By having features that can work with multiple object types, Vault has less code, a cleaner API, greater stability, and so on.</p>
<p>Files and Folders are examples of entities.&#0160; Properties and Categories are examples of behaviors.&#0160; Not every object in Vault is an entity, and not every feature is a behavior.&#0160; It&#39;s also important to know that a given entity type may not support every behavior.&#0160;</p>
<p>For example, here is a table explaining which entities have which behaviors in Vault Professional 2012.</p>
<table border="1" cellpadding="2" cellspacing="0" width="467">
<tbody>
<tr>
<td align="center" valign="top" width="75">&#0160;</td>
<td align="center" valign="top" width="65">Property</td>
<td align="center" valign="top" width="65">Revision</td>
<td align="center" valign="top" width="67">Category</td>
<td align="center" valign="top" width="50">Security          <br />(ACL)</td>
<td align="center" valign="top" width="67">Lifecycle</td>
<td align="center" valign="top" width="76">Ownership         <br />(replication)</td>
</tr>
<tr>
<td valign="top" width="76">Files</td>
<td align="center" valign="top" width="65">*</td>
<td align="center" valign="top" width="64">*</td>
<td align="center" valign="top" width="67">*</td>
<td align="center" valign="top" width="49">*</td>
<td align="center" valign="top" width="67">*</td>
<td align="center" valign="top" width="76">*</td>
</tr>
<tr>
<td valign="top" width="77">Folders</td>
<td align="center" valign="top" width="65">*</td>
<td align="center" valign="top" width="64">&#0160;</td>
<td align="center" valign="top" width="67">*</td>
<td align="center" valign="top" width="49">*</td>
<td align="center" valign="top" width="67">&#0160;</td>
<td align="center" valign="top" width="76">*</td>
</tr>
<tr>
<td valign="top" width="77">Items</td>
<td align="center" valign="top" width="65">*</td>
<td align="center" valign="top" width="64">*</td>
<td align="center" valign="top" width="67">*</td>
<td align="center" valign="top" width="49">*</td>
<td align="center" valign="top" width="67">&#0160;</td>
<td align="center" valign="top" width="76">*</td>
</tr>
<tr>
<td valign="top" width="77">Change Orders</td>
<td align="center" valign="top" width="65">*</td>
<td valign="top" width="64">&#0160;</td>
<td valign="top" width="67">&#0160;</td>
<td valign="top" width="49">&#0160;</td>
<td valign="top" width="67">&#0160;</td>
<td align="center" valign="top" width="76">*</td>
</tr>
<tr>
<td valign="top" width="77">Forum Message</td>
<td align="center" valign="top" width="65">*</td>
<td align="center" valign="top" width="64">&#0160;</td>
<td align="center" valign="top" width="67">&#0160;</td>
<td align="center" valign="top" width="49">&#0160;</td>
<td align="center" valign="top" width="67">&#0160;</td>
<td align="center" valign="top" width="76">&#0160;</td>
</tr>
<tr>
<td valign="top" width="77">Reference          <br />Designator</td>
<td align="center" valign="top" width="65">*</td>
<td valign="top" width="64">&#0160;</td>
<td valign="top" width="67">&#0160;</td>
<td valign="top" width="49">&#0160;</td>
<td valign="top" width="67">&#0160;</td>
<td valign="top" width="76">&#0160;</td>
</tr>
</tbody>
</table>
<p>Note:&#0160; Even though Items and Change Orders have lifecycles, the feature cannot be applied to other entity types.&#0160; The File lifecycle engine is the only one that is entity-based.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>The web services in the API is structured according to the entity/behavior model as shown in the table below.&#0160;</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td align="center" valign="top" width="156"><strong>Entity Services</strong></td>
<td align="center" valign="top" width="156"><strong>Behavior Services</strong></td>
<td align="center" valign="top" width="156"><strong>Other</strong></td>
</tr>
<tr>
<td valign="top" width="156">
<ul>
<li>Change Order Service </li>
<li>Document Service </li>
<li>Document Service Extensions </li>
<li>Forum Service </li>
<li>Item Service </li>
</ul>
</td>
<td valign="top" width="156">
<ul>
<li>Behavior Service </li>
<li>Category Service </li>
<li>Property Service </li>
<li>Replication Service </li>
<li>Revision Service </li>
<li>Security Service </li>
</ul>
</td>
<td valign="top" width="156">
<ul>
<li>Admin Service </li>
<li>Knowledge Vault Service </li>
<li>Information Service </li>
<li>Job Service </li>
<li>Package Service </li>
<li>Win Auth Service </li>
</ul>
</td>
</tr>
</tbody>
</table>
<p>The behavior services are easiest to spot because the functions have parameters like &quot;entityId&quot; and &quot;entityClassId&quot;.&#0160; In the entity-based services, the functions usually take more specific IDs, such as &quot;fileId&quot;.&#0160; Let&#39;s focus more on the terminology used in the behavior services.</p>
<p><strong>EntityId</strong> - Each entity has an ID that uniquely identifies it.&#0160; Usually this is the &quot;Id&quot; property on the object.&#0160; For example:&#0160; File.Id</p>
<p><strong>EntityMasterId</strong> - If the entity can be versioned, then there is a master ID, which is used to refer to the entire collection of versions.&#0160; Usually this is the &quot;MasterId&quot; property on the object.&#0160; For example:&#0160; File.MasterId     <br />If a function is asking for an EntityMasterId, but the object doesn&#39;t have one, just pass in the ID value.&#0160; For example:&#0160; Folder.Id</p>
<p><strong>EntityClassId</strong> - This is a string value containing the type name.&#0160; Call AdminService.GetServerConfiguration() to see the list of legal values.&#0160; For your convenience, I will list them out here.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="235"><strong>Entity type</strong></td>
<td valign="top" width="235"><strong>EntityClassId value</strong></td>
</tr>
<tr>
<td valign="top" width="235">File</td>
<td valign="top" width="235">FILE</td>
</tr>
<tr>
<td valign="top" width="235">Folder</td>
<td valign="top" width="235">FLDR</td>
</tr>
<tr>
<td valign="top" width="235">Item</td>
<td valign="top" width="235">ITEM</td>
</tr>
<tr>
<td valign="top" width="235">Change Order</td>
<td valign="top" width="235">CO</td>
</tr>
<tr>
<td valign="top" width="235">Reference Designator</td>
<td valign="top" width="235">ITEMRDES</td>
</tr>
<tr>
<td valign="top" width="235">Forum Message</td>
<td valign="top" width="235">FRMMSG</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
