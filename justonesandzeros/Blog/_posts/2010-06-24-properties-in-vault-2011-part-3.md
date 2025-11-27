---
layout: "post"
title: "Properties in Vault 2011 - Part 3"
date: "2010-06-24 08:48:08"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2010/06/properties-in-vault-2011---part-3.html "
typepad_basename: "properties-in-vault-2011---part-3"
typepad_status: "Publish"
---

<p><img src="/assets/Concepts2.png" /> </p> <p>This article will focus on Item properties, so feel free to skip this one if you are not using Vault Professional or do not use Items.&#0160; If you haven&#39;t read the article on <a href="http://justonesandzeros.typepad.com/blog/2010/05/entities.html">Entities</a>, you might want to do that first.&#0160; And, obviously, parts <a href="http://justonesandzeros.typepad.com/blog/2010/06/properties-in-vault-2011---part-1.html">1</a> and <a href="http://justonesandzeros.typepad.com/blog/2010/06/properties-in-vault-2011---part-2.html">2</a> should be read. </p> <p><strong>Item Properties</strong>  <br />Items is one of those features where the UI makes it look like things work one way, but at the API level, things are completely different.&#0160; Unmapped Item UDPs are pretty straightforward.&#0160; It&#39;s when you get into mapped properties that things get confusing.</p> <p>For example, take the following workflow:</p> <ol>
  <li>Create a new Property</li>
  <li>Associate it to File and Item</li>
  <li>Set up the File mappings</li>
  <li>Save the Property</li>
  <li>Upload a file with the property in it.&#0160; The UDP contains the expected value.</li>
  <li>Assign the file to an item.&#0160; </li>
  <li>The item does not contain the UDP value like you might expect.&#0160; The reason is that you still need to set up the Item mapping.&#0160; The file value doesn&#39;t automatically copy over to the item just because the Item box is checked on the property.</li>
 </ol>
 <p><strong>Mappings</strong>  <br />Items also support mapping between UDP and a Content Source Property.&#0160; But the meaning is different for Items.&#0160; <strong><em><span style="text-decoration: underline;">Item properties do not map to data within the file.</span></em></strong>&#0160; </p> <p>You know that &#39;bom&#39; parameter on the add and checkin functions?&#0160; That&#39;s what Item properties map to.&#0160; The BOM contains bill of materials data for the file.&#0160; The central objects in the BOM are the BOMComp objects.&#0160; BOMComp objects can map to Item objects.&#0160; Therefore BOMProp objects are what the Item UDPs map to.</p> <p><strong>Content Source Providers</strong>  <br />So Item properties still have the concept of a content source property.&#0160; But the file&#39;s BOM is the content source in this case, not the file itself.&#0160; Again, the content source provider is the glue that hooks the content source property to the UDP.&#0160; So depending on the file type, the Item mapping behavior may be different.</p> <p><strong>Reference Designators</strong>  <br />This is the only other entity class that supports property mappings.&#0160; Just like with Items, a Reference Designator property maps to data in the BOM.&#0160; The only difference is that the mapping is to BOMProp data on a BOMRefDesig object.</p> <p>AutoCAD Electrical is the only application that sets reference designator BOM data.&#0160; So mappings are only possible with the content source provider that handles .dwg files.&#0160; </p><p><strong>Next up...</strong><br />In the fourth and final article, sample code!</p>
