---
layout: "post"
title: "Reading Values Through the PropertyManager"
date: "2014-03-21 12:40:34"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/03/reading-values-through-the-propertymanager.html "
typepad_basename: "reading-values-through-the-propertymanager"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>Personally, I don’t like using Object, especially in an API.&#0160; You don’t get type safety, it’s hard to know what classes are supported, and it’s easy for code to break in future releases.&#0160; Unfortunately, Object is the type for property values.&#0160; So I’ll be taking a look at Connection.PropertyManager.GetPropertyValue() and describing what you can expect for the return types.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>The API documentation for PropertyDataType lists what the .NET classes are for the various Vault property types.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="235"><strong>PropertyDataType</strong></td>
<td valign="top" width="235"><strong>Object type</strong></td>
</tr>
<tr>
<td valign="top" width="235">Bool</td>
<td valign="top" width="235">System.Boolean</td>
</tr>
<tr>
<td valign="top" width="235">DateTime</td>
<td valign="top" width="235">System.DateTime</td>
</tr>
<tr>
<td valign="top" width="235">Image</td>
<td valign="top" width="235">ThumbnailInfo</td>
</tr>
<tr>
<td valign="top" width="235">ImageInfo</td>
<td valign="top" width="235">ImageInfo</td>
</tr>
<tr>
<td valign="top" width="235">Numeric</td>
<td valign="top" width="235">System.Double</td>
</tr>
<tr>
<td valign="top" width="235">Object</td>
<td valign="top" width="235">System.Object</td>
</tr>
<tr>
<td valign="top" width="235">String</td>
<td valign="top" width="235">System.String</td>
</tr>
</tbody>
</table>
<p>Unfortunately this table is not 100% correct.&#0160; There are various cases and outright defects you need to know.</p>
<p>First, let me explain the difference between <strong>Image</strong> and <strong>ImageInfo</strong>.&#0160; ImageInfo is for things like the Category Glyph properties.&#0160; These are graphics provided client-side.&#0160; The Image type is for thumbnails stored server-side.&#0160;&#0160;</p>
<p><strong>Picklists</strong> are one of the special cases you have to worry about.&#0160; If the property requires picking from a list, the value is of type PropertyDefinition.EnumeratedValue, which has both a display value (String) and a “true” value (Object).&#0160; The true value will have a data type matching the table above.&#0160; You can use the HasListValues property on PropertyDefintion to tell if a property is a picklist or not.</p>
<p>The Object is mainly there for if you want to <a href="http://justonesandzeros.typepad.com/blog/2014/01/adding-a-custom-column-to-a-vdf-grid.html" target="_blank">add your own client-side property definitions</a>.&#0160; <strong>EntityType</strong> is the only out-of-the-box property that uses Object.&#0160; In that case, the object type is EntityClass.</p>
<p>In running my own test, I found that the system properties <strong>FileSize</strong> and <strong>VersionNumber</strong> did not behave as expected.&#0160; These have int values, not Doubles.&#0160; If you try a direct cast to Double, it will fail.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>So here is my summary of special cases...</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="235"><strong>Property System Name</strong></td>
<td valign="top" width="235"><strong>Object type</strong></td>
</tr>
<tr>
<td valign="top" width="235">EntityType</td>
<td valign="top" width="235">EntityClass</td>
</tr>
<tr>
<td valign="top" width="235">FileSize</td>
<td valign="top" width="235">int</td>
</tr>
<tr>
<td valign="top" width="235">VersionNumber</td>
<td valign="top" width="235">int</td>
</tr>
<tr>
<td valign="top" width="235">&lt;any property with a value list&gt;</td>
<td valign="top" width="235">PropertyDefinition.EnumeratedValue</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<hr noshade="noshade" style="color: #d09219;" />
