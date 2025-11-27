---
layout: "post"
title: "Detail ID in Vault Item BOM"
date: "2013-01-17 10:20:20"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/detail-id-in-vault-item-bom.html "
typepad_basename: "detail-id-in-vault-item-bom"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>In some cases when you add an ItemBom the Detail ID is not automatically added. You can see this in Vault Explorer on the “Bill of Materials” for the Item. The Detail ID field is blank.</p>  <p>To get a Detail ID the following conditions need to be met: </p>  <p>•&#160; <em>The file has BOM data.      <br />•&#160; The file BOM has detail ID information.       <br />•&#160; You create the items through the “Assign Item” command, Which is the ItemService.PromoteFiles() function in the API.</em></p>  <p>&#160;</p>  <p>This <a href="http://justonesandzeros.typepad.com/blog/2012/11/the-bom-pipeline.html" target="_blank">blog post</a> on has a good description on bill of material data in Vault. Also the &quot;PromoteFiles Method&quot; topic in the SDK help has an example for PromoteFiles(). </p>
