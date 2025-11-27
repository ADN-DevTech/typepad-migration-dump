---
layout: "post"
title: "Selection Sets in Vault Explorer"
date: "2010-12-03 08:37:41"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/12/selection-sets-in-vault-explorer.html "
typepad_basename: "selection-sets-in-vault-explorer"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p><strong>Update:</strong> Additonal features have been added for <a href="http://justonesandzeros.typepad.com/blog/2011/06/file-versions-and-links-in-the-selection-set.html" target="_self">Vault 2012</a>.&#0160; This article focuses on the Vault 2011 features.</p>
<p>You have a command or tab view in Vault Explorer.&#0160; And you have your command and tab view handlers in place for when the user interacts with your control.&#0160; These handlers get a Context object passed in so that your code can know things about the state of the application.&#0160; More specifically, your code can determine which things have been selected.</p>
<p>Let&#39;s take a deeper look at these selection sets.</p>
<p>Each set is a collection of <strong>ISelection</strong> objects.&#0160; Each object consists of three pieces of data.&#0160; The <strong>Label</strong> is the display value of the things selected.&#0160; The <strong>TypeId</strong> tells you what type of object is selected.&#0160; The <strong>ID</strong> tells you the Vault ID of the thing selected.&#0160; So the things you can do with a selection is dependant on what type it is.</p>
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
<p>Make sure to always check the type before you attempt to use the ID value.&#0160; The HelloWorld sample in the SDK has a great example of not following this rule.</p>
<p>&#0160;</p>
<p>The <strong>Current Selection Set</strong> represents the currently selected thing, assuming the thing supports the selection set framework.&#0160; It&#39;s possible to have nothing selected, so this set may be empty.&#0160; But if you are running from a context menu, this should always always have at least 1 value.</p>
<p>Here is a list of things that can have selection sets :</p>
<ul>
<li>The navigation tree </li>
<li>The main View grid </li>
<li>The history tab (items only) </li>
<li>The where used tab (items only) </li>
<li>The change order tab </li>
<li>The records tab (items only) </li>
<li>The Find dialog </li>
</ul>
<p>&#0160;</p>
<p>The <strong>Nav Selection Set</strong> represents the tree view on the left.&#0160; If a Folder is selected, this will contain a selection type Folder with an ID value.&#0160; For non-folder items, such as saved searches or the Item Master, you will get Other as the type with -1 as the ID.&#0160; There should always be 1 value in this set.&#0160;</p>
<p>&#0160;</p>
<p>The <strong>View Selection Set</strong> represents the selections in the main grid.&#0160; It works pretty much how you would expect.&#0160; In cases where there is a pop-up menu, like the find dialog, the View selection set is still from the main grid on the parent dialog.</p>
<p>&#0160;</p>
<p>The <strong>Preview Selection Set</strong> seems to be mostly useless for files.&#0160; Even if you do have a file selected, it shows up as an Other type with -1 as the ID.&#0160; <br />However, with Items, you can select BOM Items and get BOM selection data.&#0160; The History and Where Used tabs both give Item selections as you would expect.&#0160; For the other tabs (General, View and custom) the selection set will match the view selection set.     <br />Change Orders mostly don&#39;t use this set.&#0160; With the Records tab, you can get all the Item objects properly, but Files show up as Other with -1 Ids.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
