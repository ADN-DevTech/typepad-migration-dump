---
layout: "post"
title: "What&rsquo;s new in the Vault 2015 SDK"
date: "2014-03-28 17:00:45"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2014/03/whats-new-in-the-vault-2015-sdk.html "
typepad_basename: "whats-new-in-the-vault-2015-sdk"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" /> <br /><img alt="" src="/assets/Announcements4.png" /></p>
<p>Vault 2015 has just been released.&#0160; Here is a quick breakdown of what’s new and what’s changed in the API.&#0160; Overall, these changes are reactions to changes in the Vault product itself.&#0160; There’s nothing new here specific to API developers.</p>
<p>&#0160;</p>
<p><strong>Edited out of Turn enhancements</strong></p>
<p>Vault has changed how it handles changes to files that have not been checked out.&#0160; It used to show an “edited out of turn” icon.&#0160; In 2015, the status now matches the status of the last downloaded state of the file (before the local edits).&#0160; An extra piece of information is provided indicating the “dirty” status of the local file.&#0160; This data is available through the UI and API.</p>
<p><img alt="" src="/assets/status.png" /></p>
<p>When acquiring a file in this “dirty” state, the download will not proceed unless a “force override” option is applied.&#0160; This override can be done through both UI and non-UI workflows in the API.</p>
<p>&#0160;</p>
<p><strong>Item BOM updates</strong></p>
<p>There have been changes to the BOM workflows.&#0160; A lot of these changes are around being able to add items first and easily hook to files later on.&#0160; Previous releases favored file-first workflows. Another workflow change is the ability to promote only part of a BOM, instead of being an “all or nothing” operation. <br />There are also some new concepts, such as “on” and “off” BOM rows.</p>
<p>&#0160;</p>
<p><strong>Vault 2012 clients are no longer compatible</strong></p>
<p>You will not be able to use any of your Vault 2012 clients with Vault 2015 server.&#0160; This includes CAD applications (like AutoCAD 2012 and Inventor 2012), Vault 2012 SharePoint integration, custom apps, etc.</p>
<p>&#0160;</p>
<p><strong>Compatibility with 2013 and 2014 clients*</strong></p>
<p>Vault 2013 and 2104 clients are compatible with Vault 2015 server.</p>
<p>* Calls to the Item and Package service will result in a runtime error.&#0160; This is due to breaking changes to the Item BOM workflows.&#0160; If your app is using either of these services, you should update to the 2015 SDK.</p>
<p>&#0160;</p>
<p><strong>.NET 4.5</strong></p>
<p>SDK DLLs are built against .NET 4.5.&#0160; Visual Studio 2012 is the recommended development environment.</p>
<hr noshade="noshade" style="color: #ff0000;" />
