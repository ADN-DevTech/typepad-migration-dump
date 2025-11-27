---
layout: "post"
title: "2 API-Related Hotfixes"
date: "2011-06-09 08:08:44"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "Must Read"
original_url: "https://justonesandzeros.typepad.com/blog/2011/06/2-api-related-hotfixes.html "
typepad_basename: "2-api-related-hotfixes"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Announcements3.png" /></p>
<p>Recently there have been some hotfixes released to address Vault 2012 API issues.</p>
<p><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=17136349&amp;linkID=9261341" target="_blank">API Log In Hotfix</a> <br />This gives you a new Autodesk.Connectivity.WebServicesTools.dll which contains a WebServiceManager that doesn&#39;t log out at incorrect times.&#0160; I still recommend using the <a href="http://justonesandzeros.typepad.com/blog/2011/04/issues-with-the-vault-2012-api.html" target="_blank">bugfix credential classes</a> because, if there are multiple extensions, some of them may contain the old DLL.&#0160; There is no way know which version of WebServicesTools gets loaded at runtime.</p>
<p>Because the SDK is distributed as an installer, the hotfix updates that installer.&#0160; You need to re-install your SDK to get the updated DLL.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=17171058&amp;linkID=9261341" target="_blank">API Working Folder Hotfix</a> <br />IExplorerUtil had an issue with the GetWorkingFolder function which caused it to return the incorrect path.&#0160; That issue had a ripple effect causing GetLocalFileStatus and DownloadFile(File, Folder) to also be incorrect.&#0160; This hotfix corrects the problem, so all 3 functions should work properly now.</p>
<p>The fix is in one of the Vault Explorer DLLs.&#0160; The SDK itself does not get updated.&#0160; If you are using IExplorerUtil, you do not need to change anything, nor do you need to re-compile code.</p>
<p><img alt="" src="/assets/Announcements3-1.png" /></p>
