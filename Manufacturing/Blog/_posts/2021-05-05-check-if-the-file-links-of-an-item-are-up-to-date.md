---
layout: "post"
title: "Check if the File links of an Item are up-to-date"
date: "2021-05-05 23:18:00"
author: "Sajith Subramanian"
categories:
  - "Sajith Subramanian"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2021/05/check-if-the-file-links-of-an-item-are-up-to-date.html "
typepad_basename: "check-if-the-file-links-of-an-item-are-up-to-date"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/sajith-subramanian.html" target="_self">Sajith Subramanian</a></p><p>If you wish to check if the file links of an item are up-to-date, using the API, then you can make use of the <strong>ItemFileLinkState</strong> property for that particular Item. So for e.g:</p><pre   "=""><span style="color: rgb(143, 8, 196);">try</span>
{&nbsp;&nbsp;&nbsp; <br>  Item <span style="color: rgb(31, 55, 127);">item</span> = connection.WebServiceManager.ItemService.GetLatestItemByItemNumber(<span style="color: rgb(163, 21, 21);">"10001"</span>);&nbsp;&nbsp; <br><br>  PropDef[] <span style="color: rgb(31, 55, 127);">propDefs</span> =<br>    connection.WebServiceManager.PropertyService.GetPropertyDefinitionsByEntityClassId(<span style="color: rgb(163, 21, 21);">"ITEM"</span>);&nbsp; <br><br>  PropDef prop<span style="color: rgb(31, 55, 127);">def</span> =<br>  propDefs.Where(<span style="color: rgb(31, 55, 127);">s</span> =&gt; s.SysName == VDF.Vault.Currency.Properties.PropertyDefinitionIds.<br>                                                             Server.ItemFileLinkState).First();&nbsp; <br><br>  PropInst <span style="color: rgb(31, 55, 127);">prop</span> = <br>    connection.WebServiceManager.PropertyService.GetProperties(<span style="color: rgb(163, 21, 21);">"ITEM"</span>, <br>                                        <span style="color: blue;">new</span>&nbsp;<span style="color: blue;">long</span>[] { item.Id }, <span style="color: blue;">new</span>&nbsp;<span style="color: blue;">long</span>[] { def.Id }).First();&nbsp;&nbsp;&nbsp;&nbsp; <br><br><span style="color: blue;">  var</span>&nbsp;<span style="color: rgb(31, 55, 127);">value</span> = prop.Val;
}</pre><p>This would return 3 possible values - <strong>0</strong>, <strong>10</strong> and <strong>20</strong>, which basically means: <strong>0</strong> – <strong>“Out of Date “;</strong> <strong>10</strong> – “<strong>Current</strong>”; <strong>20</strong> - "<strong>No links</strong>".</p>
