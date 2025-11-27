---
layout: "post"
title: "Position Number vs. Row Order vs. Detail ID"
date: "2014-05-02 16:47:26"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/05/position-number-vs-row-order-vs-detail-id.html "
typepad_basename: "position-number-vs-row-order-vs-detail-id"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>When it comes to ordering a Bill of Materials there are a surprising number of options.&#0160; Three to be specific.&#0160; This might seem overkill to some people, but BOM ordering is a pretty important topic.&#0160; So there are couple of different mechanisms used to fit the various requirements.&#0160; Hopefully, you will find one that is right for you (because the engineering team doesn’t want to write a fourth).</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Detail ID</strong></p>
<p>Let’s start with the most inflexible.&#0160; Strictly speaking, this is for cases where there is an assembly sheet with bubbles calling out each part detail.&#0160; The parts list on the sheet is basically an ordered list of the called-out components.&#0160; Vault preserves this data in the BOM as the Detail ID.</p>
<p>One interesting aspect of these Details IDs is that they are specific to an occurrence within a BOM.&#0160; This means that a given part may have different Detail IDs in different BOMs even if the parent/child relationship is the same.&#0160;</p>
<p>Another aspect of detail ID is that it’s strongly tied to the CAD file.&#0160; Items created in the Item Master will not have these values.&#0160; Detail IDs can only be created or edited through the <a href="http://justonesandzeros.typepad.com/blog/2012/11/the-bom-pipeline.html" target="_blank">BOM pipeline</a>.&#0160; So if a value needs to be updated, it requires checking out the file, modifying the CAD data, checking the file back in, and updating the item.</p>
<p>The fact that it’s difficult to edit is actually a good thing.&#0160; The entire point of the Detail ID is that it’s tied to the CAD data.&#0160; If you want something more flexible I suggest using...</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Position Number</strong></p>
<p>Much like Detail ID this is a string value that identifies a BOM row.&#0160; The main difference here is that position number can be easily edited in Vault.&#0160; It’s not tied to CAD data, so it can be set on Items not created from a file.&#0160; Lastly, it is not occurrence based, which means the position number is the same for each parent/child relationship regardless of which BOM it shows up in.</p>
<p>The default Position Number is a copy of the Detail ID, if it exists.&#0160; Although you can edit the value in Vault, chose changes may get overwritten the next time the item gets updated from CAD.&#0160; Ideally Position Number should be used when you want Detail ID behavior on an item with no Detail ID.</p>
<p>NOTE:&#0160; Even though the word <em>number</em> is clearly in the title, the data type is a String.&#0160; This happens a lot with Vault Items for some reason, like with Item Number.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Row Order</strong></p>
<p>Row Order behaves a lot like Position Number but with two main differences:&#0160; Row Order is an integer, and Row Order does not get overwritten during an update from CAD file operation.&#0160; This value is fully controlled in Vault, with minimal connection to CAD data.&#0160; It’s great to use when you want your Row Order to have no other meanings.&#0160; It doesn’t represent callouts in a drawing, or an ID in an external system.&#0160; It’s just the order you want your BOM to be in.</p>
<p>Row Order is the only one of the three that is guraranteed to have a value.&#0160; So it&#39;s a good default mechanism to use if you don&#39;t know which one to pick.&#0160; But beware,<em> the values are not guaranteed to be unique</em> (the same is true for the other two systems).</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p><strong>Relevant ItemService APIs</strong></p>
<p>If you want to read the Detail ID, you need to read the occurrence data from a BOM.&#0160; When calling <strong>GetItemBOMByItemIdAndDate</strong> make sure to pass in <strong>ReturnOccurrences</strong> as one of the BOMViewEditOptions.&#0160; If you do, you should get back an array of <strong>ItemBOMOcc</strong> objects.&#0160; The <strong>Val</strong> property on this object is the Detail ID.</p>
<p>Both Position Number and Row Order can be read on the <strong>ItemAssoc</strong> objects in an ItemBOM.&#0160; Row Order is the <strong>BOMOrder</strong> property, and Position Number is the <strong>PositionNum</strong> property.&#0160; To update Postition Number or Row Order, use the <strong>UpdateItemBOMAssociations</strong> function.</p>
<hr noshade="noshade" style="color: #5acb04;" />
