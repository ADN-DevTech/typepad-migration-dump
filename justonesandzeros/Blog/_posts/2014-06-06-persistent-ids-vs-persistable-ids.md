---
layout: "post"
title: "Persistent IDs vs. Persistable IDs"
date: "2014-06-06 13:59:58"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/06/persistent-ids-vs-persistable-ids.html "
typepad_basename: "persistent-ids-vs-persistable-ids"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p><strong>Important News</strong>:&#0160; Before I get into this article, there are some hotfixes you will want to get if you are going to use persist(ent/able) IDs.&#0160; If you are already using these IDs and have replicated Vault database servers, you need to check those IDs to make sure they are valid.</p>
<p>Hotfix links:</p>
<ul>
<li><a href="http://knowledge.autodesk.com/support/vault-products/downloads/caas/downloads/content/cumulative-hotfix-2-for-autodesk-vault-2014-sp2.html" target="_self">Vault 2014 SP2</a></li>
<li><a href="http://subscription.autodesk.com/sp/servlet/download/item?siteID=11564774&amp;id=23399845" target="_blank">Vault 2014 SR1, SP1</a> (subscription center only)</li>
<li><a href="http://knowledge.autodesk.com/support/vault-products/downloads/caas/downloads/content/cumulative-hotfix-1-for-autodesk-vault-2015-service-pack-0.html" target="_blank">Vault 2015</a></li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Once again, the API contains two different things that are almost spelled the same&#0160; (<a href="http://justonesandzeros.typepad.com/blog/2013/02/generating-file-names.html" target="_blank">Naming scheme and numbering scheme</a>, I’m looking at you).&#0160; This time around it’s Persist<strong>ent</strong> IDs and Persist<strong>able</strong> IDs.&#0160; These two things are almost the same, but API’s don’t understand “almost”.&#0160; If it’s not exactly the same, it’s invalid.&#0160; There is no in-between.</p>
<p>I’ve already written about <a href="http://justonesandzeros.typepad.com/blog/2013/08/persistent-ids.html" target="_blank">persist<strong>ent</strong> IDs</a>, so you can give that a read if you want to know about the concept.&#0160; Persist<strong>able</strong> IDs are the same thing except those IDs have an extra character in from of them.&#0160; No, I’m not making that up.</p>
<p>An “m” is put on the front if the&#0160;Persist<strong>able</strong> ID is not version specific.&#0160; A “v” is put on the front if the ID to a specific version.&#0160; The “m” stands for Master, in case you are wondering.&#0160; Like how a FileMasterId is the same for all versions of a file.&#0160; Anyway, the rest of the ID is the same as the Persist<strong>ent</strong> ID.&#0160; So you can easily convert from one to the other by adding or removing the character at the front.</p>
<p>The other difference between the two ID types is where they are found at the API.&#0160; Persist<strong>ent</strong> IDs are at the web service layer, in the <strong>KnowledgeVaultService</strong>.&#0160; Persist<strong>able</strong> IDs are in the VDF layer, in Connection.<strong>PersistableIdManager</strong>.</p>
<p>Sure, it would be nice if we could consolidate these two types, but that would require one of them to change.&#0160; And the entire point of these IDs is that <em>they can’t ever change</em>.&#0160; So were stuck with these two.&#0160; If you use one to store data, just make sure you remember which type of ID you used.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>To summarize:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="156">&#0160;</td>
<td valign="top" width="156">Persist<strong>ent</strong> IDs</td>
<td valign="top" width="156">Persist<strong>able</strong> IDs</td>
</tr>
<tr>
<td valign="top" width="156">Example ID</td>
<td valign="top" width="156">RkxEUjpMQVRFU1Q6Njc5NTg</td>
<td valign="top" width="156">mRkxEUjpMQVRFU1Q6Njc5NTg</td>
</tr>
<tr>
<td valign="top" width="156">Found in...</td>
<td valign="top" width="156">web service layer: <br />KnowledgeVaultService</td>
<td valign="top" width="156">VDF: <br />Connection.PersistableIdManager</td>
</tr>
<tr>
<td valign="top" width="156">Is the ID version specific?</td>
<td valign="top" width="156">No way to tell.</td>
<td valign="top" width="156">Check the first character. <br />m = not version specific <br />v = version specific</td>
</tr>
<tr>
<td valign="top" width="156">Where do these IDs show up?</td>
<td valign="top" width="156">API</td>
<td valign="top" width="156">API <br />Thin client URLs</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<hr noshade="noshade" style="color: #5acb04;" />
