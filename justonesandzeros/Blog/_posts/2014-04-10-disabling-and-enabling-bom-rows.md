---
layout: "post"
title: "Disabling and Enabling BOM Rows"
date: "2014-04-10 08:00:05"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/04/disabling-and-enabling-bom-rows.html "
typepad_basename: "disabling-and-enabling-bom-rows"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>One very big feature in Vault 2015 is the ability to turn a BOM row “on” or “off”.&#0160; I’m going to quickly go over the workflow for disabling a BOM row through the API and how to view that disabled row.</p>
<p><img alt="" src="/assets/screenshot.png" /></p>
<p>The on/off status of a BOM row is handled by the “isIncluded” property on a BOM association.&#0160; So disabling a BOM row is just a matter of setting isIncluded to false.</p>
<p>Basic steps (all functions are in the ItemService):</p>
<ol>
<li>Call <strong>EditItems</strong> on the parent Item in the association.</li>
<li>Call <strong>GetItemBOMByItemIdAndDate</strong> to read the BOM.&#0160; Make sure to use the item ID from the editable item returned in part 1.</li>
<li>In the resulting ItemBOM object, locate the <strong>ItemAssoc</strong> object representing the row you want to disable.</li>
<li>Call <strong>UpdateItemBOMAssociations</strong> to update the <strong>isIncluded</strong> value.&#0160; Although the function takes arrays of data, you only need to pass in the associations you are updating.&#0160;</li>
<li>Call <strong>UpdateAndCommitItems</strong>, passing in the parent item only.&#0160; This will finalize the change.</li>
</ol><hr noshade="noshade" style="color: #d09219;" />
<p><strong>Viewing the BOM</strong></p>
<p>By default, the GetItemBOMByItemIdAndDate will omit all of the OFF rows.&#0160; If you want to see the row you disabled, you need to include “<strong>ReturnExcluded</strong>” in the options parameter.&#0160; This parameter lets you OR together multiple options, like a bitfield.&#0160; So passing in both “Defaults” and “ReturnExcluded” with show all the ON and OFF item rows.</p>
<hr noshade="noshade" style="color: #d09219;" />
