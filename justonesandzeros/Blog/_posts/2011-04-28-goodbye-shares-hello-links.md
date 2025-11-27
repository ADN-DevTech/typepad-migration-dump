---
layout: "post"
title: "Goodbye Shares - Hello Links"
date: "2011-04-28 13:20:56"
author: "Doug Redmond"
categories:
  - "Concepts"
original_url: "https://justonesandzeros.typepad.com/blog/2011/04/goodbye-shares-hello-links.html "
typepad_basename: "goodbye-shares-hello-links"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Concepts2.png" /></p>
<p>In Vault 2012, the shares feature has finally been removed.&#0160; We&#39;ve been slowly deprecating it over the past few releases, but now it&#39;s final.&#0160; A new feature, links, has been added as a replacement feature.</p>
<p>In case you are not familiar with shares, it was a feature that allowed a file to live equally in multiple folders.&#0160; This was a big pain to deal with in code because you couldn&#39;t assume that a file had a single location.&#0160; In Vault 2012, you don&#39;t have to worry any more.&#0160; <strong>A file lives in one and only one folder, always.</strong></p>
<p>The new feature, links, is an object that points to a file in another location.&#0160; It&#39;s very similar to Windows shortcuts.&#0160; So even though a file has only one true location, there can be many links to it that live in other folders.</p>
<p>Files are not the only thing you can link to either.&#0160; Folders, items and change orders can have links too.&#0160; So you can use it to organize your items into folders or crate project folders containing all the relevant objects.</p>
<p>When you migrate to Vault 2012 from a previous release, any existing shares get converted to links.&#0160; The oldest folder location is chosen as the true location of the file.&#0160; Any other folder locations become links to the file.&#0160;</p>
<p>Links can only be created in Vault Workgroup and above, but because of legacy data, it may be possible for links to show up in the basic version of Vault.</p>
<p>At the API level, the code is still structured around shares.&#0160; For example, GetFoldersByFileMasterId returns an array of Folders even though the array will always have 1 element in 2012.&#0160; And DeleteFileFromFolder has you pass in a folder ID even though there is no reason to.</p>
<p>The API functions that deal with Links are pretty straightforward.&#0160; DocumentService.AddLink() adds the link.&#0160; DocumentService.GetLinksByFolderIds() gets the links for a given set of folders.</p>
<p>DocumentService.FindLinksBySearchConditions() is especially cool because it allows you to search for links.&#0160; Technically, the property conditions are searching on the thing that the link points to, but the results are in link form.&#0160; However, any folder information passed in will be applied to the link locations, not the original file locations. There are some creative uses for this feature.&#0160; For example, you can search for all items within a folder.</p>
