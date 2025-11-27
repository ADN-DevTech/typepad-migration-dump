---
layout: "post"
title: "The Information Service"
date: "2010-11-04 07:33:01"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2010/11/the-information-service.html "
typepad_basename: "the-information-service"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>I&#39;d like to take a moment to talk about the Information Service and why it is unique.&#0160; You see, this is the only service compatible across multiple releases.&#0160; All other services are locked to a specific Vault release.&#0160; For example, a Document Service that talks to a Vault 2010 server will fail if you try to connect it to a Vault 2009 or Vault 2011 server.&#0160;</p>
<p>But not the Information service.&#0160; Starting with R5, all Information service functions have been compatible with the future release.&#0160; No need to re-compile or update the web reference.</p>
<p>The reason it works that way is because this service tells you the version of the Vault server.&#0160; Locking it to a specific version would have been self-defeating.&#0160; The main idea is that you can check with the Information service before you sign in.&#0160; If the server version doesn&#39;t match the version you expect, you abort the operation because you know that the sign in won&#39;t connect properly.&#0160; Not only can you get the version of the server, but you can tell which Vault product is running on the server.&#0160;</p>
<p><strong>GetSystemProducts</strong></p>
<p>GetSystemProducts() is the main function in the server.&#0160; The other functions are there for rare cases and backward compatibility, so you will probably not need to deal with them.&#0160; GetSystemProducts has some interesting behavior.&#0160; First, you get an array back, not a single value.&#0160; This array will contain the server product and all subset products.&#0160; So, a Vault Professional server will return 4 Products: Vault Professional, Vault Collaboration, Vault Workgroup, and Vault.&#0160; A base Vault server will just return the Vault object.</p>
<p>The ProductName value on the Product object is tricky.&#0160; Because of backward compatibility, this value resembles the original product name, which may not be the current product name.&#0160; You probably know that Productstream was the original name of Vault Professional, but did you know that Vault Professional was the codename for Vault Workgroup?</p>
<p>Here, let me demonstrate with a chart:</p>
<table border="1" cellpadding="2" cellspacing="0" width="450">
<tbody>
<tr>
<td valign="top" width="225"><strong>ProductName</strong></td>
<td valign="top" width="225"><strong>DisplayName (2011 release)</strong></td>
</tr>
<tr>
<td valign="top" width="225">Autodesk.Vault</td>
<td valign="top" width="225">Vault Server</td>
</tr>
<tr>
<td valign="top" width="225">Autodesk.VaultPro</td>
<td valign="top" width="225">Vault Workgroup Server</td>
</tr>
<tr>
<td valign="top" width="225">Autodesk.VaultCollaboration</td>
<td valign="top" width="225">Vault Collaboration Server</td>
</tr>
<tr>
<td valign="top" width="225">Autodesk.Productstream</td>
<td valign="top" width="225">Vault Professional Server</td>
</tr>
</tbody>
</table>
<p>Lastly, there is the ProductVersion value.&#0160; This is a string with a 4 part version number corresponding to the build number of the Vault server.&#0160; For example &quot;1.0.3.200&quot;.&#0160; For some strange reason, Vault uses Inventor&#39;s primary build number.&#0160; So even though Vault 2011 is technically R9, it has a primary build number of &quot;15&quot;.</p>
<p>Anyway, the primary number is the only one you need to worry about.&#0160; The second build number is the service pack level, but the API does not change during service packs.&#0160; And the last two numbers are just increment values.</p>
<p>All the Product objects returned should have the same build number.&#0160; In the past, it has always been the same, and I don&#39;t see that ever changing.&#0160; The effort to test and support a mixed version server would be astronomical.</p>
