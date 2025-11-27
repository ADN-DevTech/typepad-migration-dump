---
layout: "post"
title: "Finding a local File in the Vault"
date: "2009-12-23 10:19:16"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2009/12/finding-a-local-file-in-the-vault.html "
typepad_basename: "finding-a-local-file-in-the-vault"
typepad_status: "Publish"
---

<p><img src="/assets/TipsAndTricks2.png" /> </p> <p><strong>The problem:</strong>  <br />You have a file on disk and you want to find the corresponding file in the Vault.&#0160; There are several techniques you can use, but none of them are perfect.</p> <p><strong>Technique 1: Unique file names</strong>  <br />If unique file names is turned on, then you can do a search for the file name.&#0160; Use <strong>GetUniqueFileNameRequired</strong> to determine if unique file names is turned on, then use <strong>FindFilesBySearchConditions</strong> to do a search on that file name.  <br />Even if unique file names is not turned on, this is still a pretty good approach, you just need to handle the case where multiple files have the same name.</p> <p><strong>Technique 2: Map the working folder   <br /></strong>Create a map between the local folder and the Vault folder.&#0160; Then you can look up the Vault folder by its Vault path.&#0160; Use <strong>GetEnforceWorkingFolder</strong> to tell if there is an enforced working folder.&#0160; If true, use <strong>GetRequiredWorkingFolderLocation</strong> to find out what that folder is.&#0160; If false, you need to look at the client settings to find the working folder location (<a href="http://justonesandzeros.typepad.com/blog/2009/09/client-xml-files.html">see this blog post</a>).  <br />Now that you have the working folder, you can map between the local path and the Vault path.&#0160; A call to <strong>FindLatestFilesByPaths</strong> will get you the File object from the Vault.</p> <p><strong>Technique 3: Checksum</strong>  <br />Get the checksum of the local file and use that to find the Vault file.&#0160; You can find the checksum code in Checksum.cs.&#0160; Next call <strong>FindFilePathsByNameAndChecksum</strong> to locate the file and related folders.&#0160; The nice thing about this technique is that it also locates the exact version of the file.&#0160; However, if the file has been edited on disk then this technique will not work.&#0160; </p>
