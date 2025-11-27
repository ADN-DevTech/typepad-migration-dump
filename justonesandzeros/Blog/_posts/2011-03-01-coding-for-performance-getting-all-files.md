---
layout: "post"
title: "Coding for Performance - Getting All Files"
date: "2011-03-01 08:33:45"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/03/coding-for-performance-getting-all-files.html "
typepad_basename: "coding-for-performance-getting-all-files"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>In <a href="http://justonesandzeros.typepad.com/blog/2011/02/coding-for-performance-array-functions.html" target="_blank">my last Coding for Performance article</a>, I explained the best way to get File objects from a set of File ID values.&#0160; But what if you don&#39;t have the ID&#39;s?&#0160; What if you want to get all the files in the Vault, and you don&#39;t have any information to start with?&#0160; I came up with 3 techniques you can use to get this information.&#0160; Let&#39;s see wich one performs the best.</p>
<p><strong>Technique 1:</strong> Start at the root folder and recursively scan through each folder one level at a time, gathering up the files along the way.&#0160; The cost is going to be 2 API calls per folder in the Vault.&#0160; For each folder, we will be calling GetLatestFilesByFolderId and GetFoldersByParentId with the recurse parameter set to false.</p>
<p><strong>Technique 2:</strong>&#0160; Gather up all the folders and get all the files in all the folders with 1 API call.&#0160; You can get all the folders by calling GetFolderRoot then calling GetFoldersByParentId with the recurse parameter set to true.&#0160; Next, call GetLatestFilesByFolderIds and pass in all folder IDs.&#0160; The result will be all files.&#0160; So we get it all in 3 API calls.</p>
<p><strong>Technique 3</strong>: Do a file search with no search conditions, which will result in finding all files.&#0160; FindFilesBySearchConditions is the functions that I will be using.&#0160; I will be passing in null for the folderIds parameter, which will result in a search across all folders.&#0160; The number of API calls depends on the paging setup.&#0160; Divide the total number of files by the paging size and that&#39;s how many API calls are made.</p>
<p>NOTE: I&#39;m going to assume that there are no file shares for this test.&#0160; In other words, each file lives in one and only one folder.&#0160;</p>
<p>NOTE: Technically there are 2 variables here, the number of folders and the number of files.&#0160; To simplify things, I&#39;ll assume a constant ratio between files and folders.&#0160; This is usually the case in the real world.&#0160; The more files you have, the more folders you will probably have.&#0160; I will be using a ratio of about 10 files for every 1 folder.</p>
<p>&#0160;</p>
<p>Here is the resulting graph<br /><img alt="" src="/assets/Graph.png" /></p>
<p>So what happened here?&#0160; It looked like Technique 2 was slightly better than Technique 3, but it curved up at the end.&#0160; The reason is that Technique 3 tries to get to much information in a single call.&#0160; First, it gets every folder in the Vault with a single call to GetFoldersByParentId.&#0160; Next, it gets every file in the Vault with a single call to GetLatestFilesByFolderIds.</p>
<p>As I mentioned in <a href="http://justonesandzeros.typepad.com/blog/2011/02/coding-for-performance-array-functions.html" target="_blank">my last performance article</a>, you need an upper bound on these calls.&#0160; You can&#39;t just get millions of objects in a single call.&#0160; Both Technique 1 and Technique 3 have boundaries built-in, which keeps things nice an linear.&#0160; Technique 3 wins out because it makes less API calls than Technique 1.&#0160; Another nice thing is that Technique 3 is folder independent.&#0160; It will have the same performance regardless of the number of folders or the folder structure.</p>
