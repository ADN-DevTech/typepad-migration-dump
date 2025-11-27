---
layout: "post"
title: "GoToLocation"
date: "2012-07-20 09:53:56"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2012/07/gotolocation.html "
typepad_basename: "gotolocation"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>I can’t believe I forgot to add this to the “What’s New” page in the SDK documentation.&#0160; New in the Vault 2013 API, you have the ability to jump to a different location when you custom command completes.&#0160; This is a useful feature if, for example, your command creates a folder and you want the user to navigate there.</p>
<p>To enable this feature, you just need to set GoToLocation property on the ICommandContext object that gets passed into your custom command.&#0160; It works similar to ForceRefresh.&#0160; You set the value somewhere in your custom command, and Vault Explorer performs additional actions when your command completes.&#0160; And yes, you can use GoToLocation and ForceRefresh together.&#0160; The refresh happens first, then the location jump happens next.</p>
<p>To set GoToLocation, you need to set up a new LocationContext object.&#0160; That object has 2 parts to it.&#0160; One part is the SelectionTypeId, which is basically the type of object we are navigating to.&#0160; The other part is the FullName, or path, to the object.&#0160; Because we are dealing with navigation tree locations, the currency is names and paths instead of IDs.</p>
<p>What you put for FullName is dependent on the object type.&#0160; Here is a table that explains the values.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="235"><strong>Entity Class</strong></td>
<td valign="top" width="235"><strong>LocationContext.FullName</strong></td>
</tr>
<tr>
<td valign="top" width="235">File</td>
<td valign="top" width="235">Full Vault path to file</td>
</tr>
<tr>
<td valign="top" width="235">Folder</td>
<td valign="top" width="235">Full Vault path to the folder</td>
</tr>
<tr>
<td valign="top" width="235">Item</td>
<td valign="top" width="235">The Item number</td>
</tr>
<tr>
<td valign="top" width="235">Change Order</td>
<td valign="top" width="235">The Change Order number</td>
</tr>
<tr>
<td valign="top" width="235">Custom Entity</td>
<td valign="top" width="235">The Custom Entity number</td>
</tr>
</tbody>
</table>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
