---
layout: "post"
title: "Client XML Files"
date: "2009-09-22 07:40:30"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2009/09/client-xml-files.html "
typepad_basename: "client-xml-files"
typepad_status: "Publish"
---

<p class="asset asset-image"><a href="http://justonesandzeros.typepad.com/.a/6a0120a5728249970b0120a5e340ed970c-pi" style="DISPLAY: block"><img alt="TipsAndTricks2" border="0" class="at-xid-6a0120a5728249970b0120a5e340ed970c " src="/assets/image_b43efe.jpg" style="MARGIN: 0px" title="TipsAndTricks2" /></a>I&#39;m about to go into a topic that&#39;s outside the official API.&#0160; Therefore Autodesk will not support this information, and what I say may change without notice in the next release.<br /><br /><br /><strong>Overview:</strong><br />Vault is a client/server framework.&#0160; Some data is stored on the server side and some is stored on the client side.&#0160; To get to the server data, you use the API for that.&#0160; But there is no API to get at the client data.&#0160; So you will need to access client data directly if you want it.<br /><br />Things specific to a user&#39;s settings are stored on the client.&#0160; For example:<br />
<ul type="disc">
<li>The working folders 
<li>Saved searches 
<li>Shortcuts 
<li>Column settings </li>
</li></li></li></ul>
<br /><strong>Finding in the Information:</strong><br />Client data is stored in a bunch of XML files.&#0160; The XML syntax is pretty straightforward.&#0160; It&#39;s getting to the files that is tricky.&#0160; The path is dependent on a lot of factors.&#0160; <br /><br />Let&#39;s go through a path:<br /><span style="COLOR: #bf5f00; FONT-FAMILY: "><span style="COLOR: #855a40; FONT-FAMILY: ">C:\Documents and Settings\<strong>[Windows user]</strong>\Application Data\Autodesk\VaultCommon\Servers\<strong>[Vault version]</strong>\<strong>[Vault server]</strong>\Vaults\<strong>[Vault Name]</strong>\Objects</span></span><br /><br /><strong>Windows User</strong> - The windows login.&#0160; If there are multiple users on a single computer, they will each get different client settings.<br /><strong>Vault Version</strong> - This one looks a bit strange, for example Vault 2010 has a value of &quot;Services_Security_1_26_2009&quot;.&#0160; It&#39;s basically a code that changes with each release.&#0160; Therefore client settings are tied to a specific Vault version.<br /><strong>Vault Server</strong> - The vault server.&#0160; So settings on one server are independent of client settings on another server.<br /><strong>Vault Name</strong> - The name of the vault.&#0160; Client settings are specific to a single Vault.<br /><br />NOTE:&#0160; In Vista the path is <span style="COLOR: #855a40; FONT-FAMILY: ">C:\Users\<strong>[Windows User]</strong>\AppData\Roaming\Autodesk\VaultCommon\Servers\<strong>[Vault version]</strong>\<strong>[Vault server]</strong>\Vaults\<strong>[Vault Name]</strong>\Objects</span> .<br />Also, AppData is a hidden folder.<br /><br /><br /><strong>Writing Client Information:</strong><br />Please don&#39;t write to these XML files.&#0160; Since the XML schema isn&#39;t published, it&#39;s likely that you will break something. 
<p></p>
<p></p>
<p></p></p>
