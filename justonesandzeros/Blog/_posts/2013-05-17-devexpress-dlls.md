---
layout: "post"
title: "DevExpress DLLs"
date: "2013-05-17 08:12:58"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/05/devexpress-dlls.html "
typepad_basename: "devexpress-dlls"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/Announcements3.png" /></p>
<p>The Vault 2014 SDK contains a bunch of DLLs from a third-party vendor, DevExpress.&#0160; Autodesk and DevExpress entered into an agreement for usage and distribution of these DLLs in the Vault SDK.&#0160; Here is my understanding of that agreement.&#0160; Keep in mind that I do not represent the Autodesk legal department.</p>
<p><strong>Background</strong></p>
<p>The Vault 2014 API contains re-usable UI components, which make use of DevExpress controls.&#0160; Therefore those controls needed to be included in the Vault SDK.</p>
<p><strong>The Agreement</strong></p>
<p>The DevExpress DLLs in the Vault SDK (these are the ones in the bin folder that start with “DevExpress”) are free for you (the user of the Vault SDK) to use and re-distribute within the context of the Vault SDK.&#0160; </p>
<p>However, if you use the DevExpress controls directly, then you need to get a developer license from DevExpress.&#0160; Things like cost, and usage are between you and DevExpress.&#0160; Autodesk has no involvement.</p>
<p>If you try to make direct use of DevExpress controls without a license, you should see a pop-up in Visual Studio indicating unlicensed usage.</p>
<p>DevExpress website:&#0160; <a href="http://www.devexpress.com/" title="http://www.devexpress.com/">http://www.devexpress.com/</a></p>
<p><strong>Examples</strong></p>
<p>If you paste a VaultBrowserControl in your dialog, that’s free, even though VaultBrowserControl makes use of DevExpress controls internally.</p>
<p>If you paste a DevExpress.XtraGrid control in your dialog, you need a DevExpress developer license.</p>
<p><strong>DLL Redistribution</strong></p>
<p>The Vault Client installs all the needed DevExpress controls into the GAC.&#0160; So if your app requires the Vault Client, then you don’t need to re-distribute the DevExpress DLLs.&#0160; It’s only in cases where the user may not have the Vault Client that you need to worry about re-distributing the DLLs.&#0160; </p>
<p>Although the DLLs can go in the GAC, I recommend putting the DevExpress DLLs in the running folder.</p>
<p><strong>Special Thanks</strong></p>
<p>Thanks goes out to the people at DevExpress.&#0160; Not only did they let us include their DLLs in he Vault SDK, but they also make good controls.&#0160; Since the very first Vault release, the Vault client has been using DevExpress controls to great satisfaction.</p>
<p><img alt="" src="/assets/Announcements3-1.png" /></p>
