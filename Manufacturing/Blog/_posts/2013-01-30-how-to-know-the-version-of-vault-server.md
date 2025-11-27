---
layout: "post"
title: "How to know the version of Vault Server"
date: "2013-01-30 17:07:49"
author: "Daniel Du"
categories:
  - "Daniel Du"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/01/how-to-know-the-version-of-vault-server.html "
typepad_basename: "how-to-know-the-version-of-vault-server"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/Daniel-Du.html">Daniel Du</a></p> <p>Here is a way to know which version of Vault Server you are running.:</p> <p>1. log into Vault Server with "Autodesk Data Management Server Console"  <p>2. Open &lt;ServerName&gt; -&gt; Management -&gt; Console Logs -&gt; Today, You will see the Core Server version. Here is mine:&nbsp; <p>1/29/2013 9:15:59 AM ==================================================================  <p>1/29/2013 9:15:59 AM Logging initiated: <b>Core server version 17.0.62.0</b>  <p>1/29/2013 9:15:59 AM ==================================================================  <p>1/29/2013 9:15:59 AM Connectivity.Request.Common.RequestLibrary - RequestRuntimeStart()  <p>1/29/2013 9:15:59 AM Connectivity.Request.Vault.RequestLibrary - RequestRuntimeStart()  <p>1/29/2013 9:15:59 AM Connectivity.Request.Foundation.RequestLibrary - RequestRuntimeStart()  <p>1/29/2013 9:15:59 AM Connectivity.Request.Document.RequestLibrary - RequestRuntimeStart()  <p>1/29/2013 9:16:08 AM Checking if a migration is necessary.  <p>1/29/2013 9:16:46 AM All vaults and libraries have already been migrated.  <p>&nbsp;</p> <p>If you’d like to get these information by API, you will need Information service , please refer to this <a href="http://adndevblog.typepad.com/manufacturing/2012/08/vault-api-getting-information-about-the-vault-server.html">post</a> and <a href="http://justonesandzeros.typepad.com/blog/2010/11/the-information-service.html">this one</a>.</p>
