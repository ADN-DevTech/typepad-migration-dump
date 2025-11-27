---
layout: "post"
title: "Security - Folder ACLs and System ACLs"
date: "2010-10-28 08:06:28"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2010/10/security-folder-acls-and-system-acls.html "
typepad_basename: "security-folder-acls-and-system-acls"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts2.png" /></p>
<p>If you haven&#39;t read my <a href="http://justonesandzeros.typepad.com/blog/2009/10/security.html" target="_blank">earlier article on security</a>, you might want to do that first.&#0160; This article is going to dig deeper into Vault&#39;s the security framework.&#0160; That&#39;s right, there&#39;s even more.&#0160; The good news is that the hard stuff is over.&#0160; Now that you know what an ACL is an how it&#39;s used to calculate the read, write and delete permissions, we can talk about how ACLs are hooked to files.</p>
<p><strong>Folder ACLs</strong></p>
<p>A folder can have an ACL on it.&#0160; That ACL will not only apply to the folder itself, but it also applies to any files in that folder.&#0160; If a sub-folder is created, it will inherit the ACL from the parent folder, but this can be changed later on.&#0160; When you change the ACL on a folder it immediately applies to the files in that folder.&#0160; There are also some options for propagating the changes to sub-folders.</p>
<p><img alt="" src="/assets/folderSecurity.png" /> <br /><img alt="" src="/assets/folderSecurity2.png" />&#0160;</p>
<p><strong>System ACLs</strong></p>
<p>A file can have 2 ACLs on it.&#0160; One comes from its parent Folder and the other one is something called a System ACL.&#0160; If present, the System ACL overrides the Folder&#39;s ACL.&#0160; The 2 ACLs are not merged in any way.&#0160; The System ACL completely wins out.&#0160; So you may find cases where the folder says that the user has read only access, but they can edit certain files in the folder.</p>
<p>There are various ways to set the System ACL:</p>
<ul>
<li>If the Vault is not set up to use Item security, then the file&#39;s lifecycle state dictates the System ACL.      <br /><img alt="" src="/assets/fileLifecycle-scaled.png" /> </li>
<li>If the Vault is set up to use Item security, then the item&#39;s security settings dictate the System ACL.&#0160; This is only for cases where the file is linked to an Item and the Item is in the Released state.      <br /><img alt="" src="/assets/itemLifecycle.png" /> </li>
<li>The file&#39;s System ACL can be set explicitly in the Properties dialog under the file menu (not to be confused with the Properties grid).&#0160; Setting the System ACL in this manner will overwrite any file or item lifecycle security settings.&#0160; At the API level, you use SecurityService.SetSystemACLs().      <br /><img alt="" src="/assets/fileProperties.png" /> </li>
</ul>
<p>Regardless of how the System ACL is set, you can read it using SecurityService.GetEntACLsByEntityIds().&#0160; Even nicer is that the function returns both the regular ACL (the one from the folder security) and System ACL.</p>
