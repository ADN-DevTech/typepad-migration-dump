---
layout: "post"
title: "File Transfer - Doing it the Hard Way"
date: "2013-07-10 08:01:00"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/07/file-transfer-doing-it-the-hard-way.html "
typepad_basename: "file-transfer-doing-it-the-hard-way"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>So, you don’t want to use <a href="http://justonesandzeros.typepad.com/blog/2013/05/how-to-acquire-files.html" target="_blank">AcquireFiles</a>?&#0160; You want to stick with web services?&#0160; Fine, you can do that, but don’t say I didn’t warn you.&#0160; The web service mechanism for uploading/downloading got real ugly in Vault 2014.</p>
<p>The main reason for the change was the <a href="http://justonesandzeros.typepad.com/blog/2013/06/vault-filestore-server.html" target="_blank">file store server architecture</a>.&#0160; The database server controls things like security and the file store server controls the actual bits.&#0160; So you need to make at least 2 calls for every file transfer.&#0160; Also, there are special headers that need to get set on each call.&#0160; They were optional in the last release but they are required for 2014.</p>
<p>It’s probably best if I just post the code...   <br /><a href="http://justonesandzeros.typepad.com/Files/LegacyFileTransfer/LegacyFileTransfer.cs">LegacyFileTransfer.cs</a>&#0160; <br /><a href="http://justonesandzeros.typepad.com/Files/LegacyFileTransfer/LegacyFileTransfer.vb">LegacyFileTransfer.vb</a></p>
<p>The code above contains examples of AddFile, CheckinFile, CheckoutFile, and DownloadFile.&#0160; All Vault operations are done through the WebServiceManager.&#0160; No Connection object required.</p>
<p>Related post:&#0160; <a href="http://justonesandzeros.typepad.com/blog/2013/10/logging-in-the-hard-way.html">Logging in the Hard Way</a></p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
