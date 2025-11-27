---
layout: "post"
title: "Working Folder"
date: "2013-08-30 07:26:10"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/08/working-folder.html "
typepad_basename: "working-folder"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>Working Folder is another one of those concepts that is more complex than you would expect it to be.&#0160; At it’s core, things are pretty simple, you map a local folder to a Vault folder.&#0160; That way Vault clients know were to upload and download files to.&#0160; The complexity comes from additional features that are built on or around this concept. I’ll try to detangle everything by listing the various features related to working folder and their related APIs.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Per-folder setting      <br /></strong>If you right-click on a Vault folder and select Details, you will see the local folder that corresponds to that Vault folder.&#0160; By default, the $ folder is mapped to a folder in your user settings.&#0160; Also by default, a sub-folder is mapped based on the parent folders mapping.</p>
<p><img alt="" src="/assets/PerFolder.png" /></p>
<p>You can override the default settings.&#0160; So a sub-folder on disk may not necessarily correspond to the the parent folder on disk.&#0160; In fact, you can even reverse the order so that the sub-folder in Vault is the parent file on disk.&#0160; </p>
<p>These settings are stored in an XML file on your local computer.&#0160; The easiest way to access that data is through the function Connection.WorkingFoldersManager.<strong>GetWorkingFolder()</strong>.&#0160; It takes care of all the calculations for you and gives you the same path you see in the Folder Details dialog.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Enforced working folder      <br /></strong>The Vault administrator can enforce a root working folder.&#0160; This setting has the effect of overwriting any folder mappings set in the Folder Details window.&#0160; An enforced working folder only gets applied to the root and all sub-folders are based off the root setting.&#0160; </p>
<p><img alt="" src="/assets/enforced.png" /></p>
<p>When an enforced working folder is in effect, the user is prevented from setting a working folder in the Vault client.&#0160; However, the API function Connection.WorkingFoldersManager.<strong>SetWorkingFolder()</strong> still works, so be careful.&#0160; The next time the user logs in through the Vault Explorer client, it will overwrite anything you changed with SetWorkingFolder.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Inventor project files      <br /></strong>Inventor ignores any mappings you set through the Vault client.&#0160; Instead it uses the .ipj file to map Vault folders to your local folders.&#0160; So the GetWorkingFolder() won’t help you when working with Inventor. You need to either read the .ipj contents or use the Inventor API to figure out what the mappings are.</p>
<p>Since the .ipj file is critical for almost every Inventor operation, Vault has a special setting that you can use to locate the .ipj file.&#0160; In the DocumentService there are two functions you can use to get at this data.&#0160; <strong>GetEnforceInventorProjectFile()</strong> tells you if there is an .ipj file set to be used by the entire Vault.&#0160; <strong>GetInventorProjectFileLocation()</strong> gives you the Vault path to the enforced .ipj file.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
