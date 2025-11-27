---
layout: "post"
title: "Persistent IDs"
date: "2013-08-02 09:26:26"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/08/persistent-ids.html "
typepad_basename: "persistent-ids"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>If you are integrating Vault and another system, you will probably want to use Persistent IDs.&#0160;&#0160; Like the name implies, these identifiers are persisted and will not change in the future.&#0160; </p>
<p>Persistent IDs came out in Vault 2013 as an undocumented feature.&#0160; For Vault 2014, it’s now being officially supported.&#0160; Before Vault 2013, there was no guaranteed way to store a reference to an entity.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Non Persistent IDs</strong></p>
<p>Let me give you some examples of non-persistent IDs to illustrate why a new feature was needed.</p>
<p><strong>File Name</strong> - Files can be renamed.     <br /><strong>Property Values</strong> - The values can be changed in new releases.&#0160; Old releases can be purged.     <br /><strong>File ID and MasterID</strong> - These are the database keys, which may need to change if the database gets re-designed.&#0160; This did actually occur during the Vault 3 -&gt; 4 migration.&#0160; That was 8 releases ago, but it could happen again.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>API Usage</strong></p>
<p>The Persistent ID feature is handled by 2 functions in the KnowledgeVaultService: GetPersistentIds and ResolvePersistentIds.</p>
<p><strong>GetPersistentIds</strong> gives you the PersistentIds for a set of entities.&#0160; There are two types of IDs you can get: History and Latest.&#0160; History gives you an ID to a specific version of an entity.&#0160; Latest gives you a version independent ID, which is basically a persistent Master ID.&#0160; If an entity is not versioned, then either type gives you the same result.</p>
<p>Be careful when using History.&#0160; If you have an ID to a version that gets purged, then that ID will not be able to get resolved.&#0160; </p>
<p><strong>ResolvePersistentIds</strong> takes a set of Persistent IDs and gives you back the entity IDs and types.&#0160; If the Persistent ID is a Latest type, then you get back the latest version of the entity.&#0160; A History type results in a specific version of an entity being returned.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Other Details</strong></p>
<ul>
<li>Persistent IDs only work for <a href="http://justonesandzeros.typepad.com/blog/2011/09/entities-and-behaviors.html" target="_blank">entities</a>, which are Files, Folders, Items, Change Orders and Custom Objects. </li>
<li>Persistent IDs are unique for a given Vault, but not across multiple Vaults. </li>
<li>Some non-entity classes, such as Property Definitions and Categories, use a <a href="http://justonesandzeros.typepad.com/blog/2012/04/system-name-vs-display-name.html" target="_blank">System Name</a> as a non-changing identifier. 
<ul>
<li>System Names are persistent in the sense that they cannot be changed by a user and will not change in future releases.&#0160; So you can safely store these outside of Vault. </li>
<li>System Names are not compatible with the GetPersistentIds and ResolvePersistentIds functions. </li>
</ul>
</li>
</ul>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
