---
layout: "post"
title: "File Versions and Links in the Selection Set"
date: "2011-06-28 08:49:31"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/06/file-versions-and-links-in-the-selection-set.html "
typepad_basename: "file-versions-and-links-in-the-selection-set"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>If you are working with custom Vault Explorer commands or tabs, then you probably deal with <a href="http://justonesandzeros.typepad.com/blog/2010/12/selection-sets-in-vault-explorer.html" target="_blank">selection sets</a>.&#0160; Vault 2012 adds some cool new features to make your customizations even better.</p>
<p>There is a new SelectionTypeId value in Vault 2012.&#0160; It&#39;s called FileVersion, and if it&#39;s in the selection set, it means that a specific version of a file has been selected.&#0160; Don&#39;t confuse this with SelectionTypeId.File, which means a file is selected, but you don&#39;t know the version.</p>
<p>Files in the main grid will have SelectionTypeId.File, whereas selections in the history tab will show up as SelectionTypeId.FileVersion.</p>
<p><img alt="" src="/assets/FileSelections.png" /></p>
<p>The meaning of the ID is different depending on the selection type.&#0160; For example, if you want to get the File object from the server, you will call either GetFileById or GetLatestFileByMasterId depending on the selection type.</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="225"><strong>TypeId</strong></td>
<td valign="top" width="225"><strong>Id meaning</strong></td>
</tr>
<tr>
<td valign="top" width="225">File</td>
<td valign="top" width="225">File.MasterId</td>
</tr>
<tr>
<td valign="top" width="225">FileVersion</td>
<td valign="top" width="225">File.Id</td>
</tr>
<tr>
<td valign="top" width="225">Folder</td>
<td valign="top" width="225">Folder.Id</td>
</tr>
<tr>
<td valign="top" width="225">Item</td>
<td valign="top" width="225">Item.Id</td>
</tr>
<tr>
<td valign="top" width="225">Bom</td>
<td valign="top" width="225">Item.Id</td>
</tr>
<tr>
<td valign="top" width="225">Change Order</td>
<td valign="top" width="225">ChangeOrder.Id</td>
</tr>
<tr>
<td valign="top" width="225">Other</td>
<td valign="top" width="225">&quot;-1&quot;&#0160; <br />Whatever is selected is not supported.</td>
</tr>
<tr>
<td valign="top" width="225">None</td>
<td valign="top" width="225">Not used</td>
</tr>
</tbody>
</table>
<p>In practice, you usually want to handle both types when dealing with files.&#0160; If you have a file command, you set the type as both File and FileVersion.&#0160; When you read in a selection set, you check for both File and FileVersion entries.&#0160; See <a href="http://justonesandzeros.typepad.com/blog/2011/03/google-map-extension.html" target="_blank">Google Map Extension</a> for sample code.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Links</strong></p>
<p>If you have a link to an entity, it shows up in the selection set as the entity itself.&#0160; In other words, your code has no idea it&#39;s a link.&#0160; A link to a file shows up as a file in the selection set.&#0160; Also, you can add links to folders, items and change orders.&#0160; So for a given folder, it&#39;s possible to have links to many different types of objects.&#0160; You can no longer assume that it will always be files like in Vault 2011.&#0160;</p>
<p>It&#39;s possible for a single selection set to contain a mix files, folders, items and change orders.&#0160; So you <strong>always</strong> want to check the type on the ISelection object before you do anything with the Id value.</p>
<p><img alt="" src="/assets/LinkSelections.png" />&#0160; <br />In the above example, the selection set will contain 4 items, one of each entity type that can be a link target.&#0160; The rows that indicate the entity type do not show up in the selection set.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
