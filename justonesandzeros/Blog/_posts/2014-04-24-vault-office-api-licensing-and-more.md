---
layout: "post"
title: "Vault Office API licensing and more"
date: "2014-04-24 08:54:54"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/04/vault-office-api-licensing-and-more.html "
typepad_basename: "vault-office-api-licensing-and-more"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Concepts4.png" /></p>
<p>There was a video posted recently about <a href="https://autodesk-2.wistia.com/medias/zzt0pl52qj" target="_blank">Vault Office</a>, so I thought I would have an article about API licensing for Vault Office.&#0160; And while I’m at it, I may as well go over all the licensing rules for Vault 2015.&#0160; It’s not my favorite topic either, but it has to be done.&#0160; I’ll try to go quickly.</p>
<p>In case you are not aware, Vault Office is a separate license type designed for non-CAD users.&#0160; The idea is simple:&#0160; The Office license is cheaper than a standard Vault license, but the functionality is limited.&#0160; There are specialized clients which consume Vault Office licenses instead of regular Vault Workgroup or Professional licenses.</p>
<p>Don’t confuse Vault Office with the <a href="http://autodesk-2.wistia.com/medias/7xuthpmuq6" target="_blank">Vault plug-in for Microsoft Office</a>.&#0160; They are different things.&#0160; Actually, there is an MS Office plug-in which consumes a normal Vault license and an MS Office plug-in which consumes a Vault Office license.&#0160; So I guess it is kind of confusing.</p>
<p>Regarding API development, <strong>you cannot develop your own apps against a Vault Office license</strong>.&#0160; Currently, none of the Vault Office clients support plug-ins.&#0160; If you write your own standalone client, it will log in using a standard Vault license.</p>
<p>&#0160;</p>
<p>Here is a run down of the different licensing rules by product:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="117">&#0160;</td>
<td valign="top" width="117"><strong>Regular license</strong></td>
<td valign="top" width="117"><strong>Read only login</strong></td>
<td valign="top" width="117"><strong>Vault Office license</strong></td>
</tr>
<tr>
<td valign="top" width="117"><strong>Vault Basic</strong></td>
<td valign="top" width="117">Development allowed.&#0160; End user must have a qualifying CAD license.</td>
<td valign="top" width="117">Development allowed.&#0160; End user must have a qualifying CAD license.</td>
<td valign="top" width="117">N/A</td>
</tr>
<tr>
<td valign="top" width="117"><strong>Vault Workgroup and Professional</strong></td>
<td valign="top" width="117">Development allowed.&#0160; Vault Server manages the license.</td>
<td valign="top" width="117">Development allowed.&#0160; No license is consumed.</td>
<td valign="top" width="117">Development not allowed.</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<hr noshade="noshade" style="color: #5acb04;" />
