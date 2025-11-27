---
layout: "post"
title: "Batch Add/Edit of Items"
date: "2014-01-23 14:09:39"
author: "Doug Redmond"
categories:
  - "PLM 360"
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2014/01/batch-addedit-of-items.html "
typepad_basename: "batch-addedit-of-items"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/PLM4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>If you are doing a mass import of Items, you don’t need to do them one at a time.&#0160; The same is true for update.&#0160; You can do multiple edits at once through the REST API.&#0160; In general, it&#39;s much more efficient to do things in batch because it cuts down on network latency.&#0160; </p>
<p>If you’ve added an Item through the API, you probably noticed that the <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-8F2A1001-3BCE-41F8-BA81-62DB0F498126" target="_blank">POST URL</a> takes an array of ItemDetail objects.&#0160; Even if you are just adding a single Item, you still have to put it in an array.&#0160; To do a batch add, just include multiple objects in the array.</p>
<p>For Item updates, there are two endpoints: <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-EE4AFB1B-8E45-42C1-8B4C-9578A47712DD" target="_blank">one for batch</a> and <a href="http://help.autodesk.com/view/PLM/ENU/?guid=GUID-36270D54-8588-4F0B-BDB9-E79DDE60B728" target="_blank">one for single items</a>.&#0160; There is not much of a downside to using the batch endpoint for all item edits, even if it&#39;s just a single item.&#0160; Just like with batch add, batch edit takes an array of ItemDetail objects.&#0160; The only difference is that the objects must have ID values.</p>
<p>Example JSON of setting CITY=Paris on two Items:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470">[ <br />&#0160; {&quot;id&quot;:2913,&quot;fields&quot;:{&quot;CITY&quot;:&quot;Paris&quot;}}, <br />&#0160; {&quot;id&quot;:2912,&quot;fields&quot;:{&quot;CITY&quot;:&quot;Paris&quot;}} <br />]</td>
</tr>
</tbody>
</table>
<hr noshade="noshade" style="color: #d09219;" />
<p>All REST calls are transacted unless documented otherwise.&#0160; So batch Add/Edit will either all succeed or all fail.</p>
<p>The limit is 10 Items at a time for batch add or edit.&#0160; We’re starting out conservative, but we may decide to increase the limit in the future.&#0160; It’s still 90% better than doing things one at a time.</p>
<hr noshade="noshade" style="color: #d09219;" />
