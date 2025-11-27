---
layout: "post"
title: "Reading Items in a Workspace"
date: "2014-01-08 14:26:10"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2014/01/reading-the-items-in-a-workspace.html "
typepad_basename: "reading-the-items-in-a-workspace"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>There are two main ways to get a list of items in a workspace.&#0160; You can use the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-14018953-E39F-4CA5-B069-974618231608" target="_blank">items endpoint</a> or the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-BB31FFBD-3904-44A1-80C4-DDB451035617" target="_blank">query endpoint</a>.&#0160; In this article, I will be comparing and contrasting these API calls.&#0160; (Spoiler Alert: My preference is the query endpoint)</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>The <strong>items endpoint</strong> follows the standard REST pattern.&#0160; You make a GET call to a collection URL ( /api/v2/workspaces/{workspaceId}/items).&#0160; The return value is all items in the workspace.&#0160; The return value is a set of <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-AA0CFA55-6CB1-468E-8067-C49794266D63" target="_blank">Item</a> objects, which just provide basic information.&#0160; Because the payload is simple, the API call is fast.</p>
<p>At first, this endpoint seems like an ideal way to navigate workspaces, but there are some big downsides.&#0160; For one, the endpoint will return deleted Items.&#0160; This is because “deleted” Items are not actually removed from the system.&#0160; Instead, they are just tagged as deleted.&#0160; The endpoint makes no assumptions about what type of item you want to get, so it returns both deleted and non-deleted Items.</p>
<p>The items endpoint also doesn’t have any sort or filter capability.&#0160; In a large workspace, this is a significant drawback.&#0160; If you are looking for a specific item, you may have to scan through hundreds of pages in order to find it.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>The <strong>query endpoint</strong> is meant to give you more control over what you get back.&#0160; It lets you sort your results and lets you filter out Items you don’t want.&#0160; There is <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-23719984-36F0-453B-99F3-4F05B0E17FE2" target="_blank">an article on how to query</a> over on the PLM 360 online help. One common use is to filter out deleted items and sort by the descriptor name, like in this example JSON.</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">{ <br />&#0160; &quot;conditions&quot;:[ <br />&#0160; { <br />&#0160;&#0160;&#0160; &quot;propertyDefinition&quot;: <br />&#0160;&#0160;&#0160; { <br />&#0160;&#0160;&#0160;&#0160;&#0160; &quot;id&quot;:&quot;IS_DELETED&quot;, <br />&#0160;&#0160;&#0160;&#0160;&#0160; &quot;type&quot;:&quot;SYSTEM&quot; <br />&#0160;&#0160;&#0160; }, <br />&#0160;&#0160;&#0160; &quot;operator&quot;:&quot;EQUALS&quot;, <br />&#0160;&#0160;&#0160; &quot;value&quot;:&quot;false&quot; <br />&#0160; }], <br />&#0160;&#0160;&#0160; &quot;page&quot;:1, <br />&#0160;&#0160;&#0160; &quot;pageSize&quot;:100, <br />&#0160;&#0160;&#0160; &quot;sort&quot;:[ <br />&#0160;&#0160;&#0160; { <br />&#0160;&#0160;&#0160;&#0160;&#0160; &quot;propertyDefinition&quot;: <br />&#0160;&#0160;&#0160;&#0160;&#0160; { <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;id&quot;:&quot;ITEM_DESCRIPTOR&quot;, <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;type&quot;:&quot;SYSTEM&quot; <br />&#0160;&#0160;&#0160;&#0160;&#0160; }, <br />&#0160;&#0160;&#0160;&#0160; &quot;sortAscending&quot;:true <br />&#0160; }] <br />}</td>
</tr>
</tbody>
</table>
<p>The return value is a set of <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-B3CE8894-4DEE-4C89-A051-4D15921DA250" target="_blank">ItemDetail</a> objects.&#0160; It contains more data than an Item, but at the cost of performance.&#0160; Currently, the query endpoint has no way to control which parts of the ItemDetail are returned.&#0160; So it always attempts to look up the field values, ownership and audit data.</p>
<p>This call is a bit harder to work with because it’s a POST call.&#0160; Since a payload has to be passed in, the GET verb can’t be used.&#0160; You can’t easily test it out in your web browser, and it’s harder to cycle through pages.</p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Although the query endpoint is slower, it’s still the one I recommend using for your apps.&#0160; The flexibility and scalability are a must-have.&#0160; There will likely be improvements in the future, so don&#39;t be too discouraged if it doesn&#39;t yet support the type of search you want.&#0160; The only time I suggest using the items endpoint is to do a quick scan of the entire workspace.</p>
<hr noshade="noshade" style="color: #d09219;" />
