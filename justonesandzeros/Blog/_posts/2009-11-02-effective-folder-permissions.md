---
layout: "post"
title: "Effective Folder Permissions"
date: "2009-11-02 08:07:18"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2009/11/effective-folder-permissions.html "
typepad_basename: "effective-folder-permissions"
typepad_status: "Publish"
---

<p><img src="/assets/SampleApp2.png" /> </p>
<p><strong>Overview:</strong> <br />&quot;Effective Folder Permissions&quot; shows a simplified view of each user’s permissions on a set of folders.</p>
<p><img src="/assets/Screenshot.png" /> </p>
<p>There is also a bonus feature which lists all the users with administrative permissions.</p>
<p><img src="/assets/Admins.png" /> </p><br />
<p><strong>Requirements:</strong> <br />Vault 2010 (all versions)</p>
<p><strong>Introduction:</strong> <br />Not only am I a programmer, but I’m an administrator of the Vault we use for internal documents.&#0160; Yes, we do use our own product.&#0160; So I decided to write a program to make things a bit easier for us admins.&#0160; It also gave me a chance to put in practice some concepts that I previously went over:&#0160; <a href="http://justonesandzeros.typepad.com/blog/2009/10/security.html" target="_blank">The Vault security model</a> and <a href="http://justonesandzeros.typepad.com/blog/2009/10/windows-authentication.html" target="_blank">Windows Authentication</a>.&#0160; </p>
<p>The result is a program that runs through all the factors that determine a user’s access level.&#0160; It takes into account user permissions, ACL information, the ‘Active’ status of the user and whether or not the user has access to the Vault.&#0160; All group information is taken into account as well.</p>
<p>Lastly, I provided a simple list of all the administrators.&#0160; I recommend that every admin run this check.&#0160; You never know when somebody might accidently be given admin permissions.</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/EffectiveFolderPermissions/EffectiveFolderPermissions-1.0.1.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/EffectiveFolderPermissions/EffectiveFolderPermissions-1.0.1.0-src.zip">Click here to download the source code</a></p>
<p>Enjoy</p>
