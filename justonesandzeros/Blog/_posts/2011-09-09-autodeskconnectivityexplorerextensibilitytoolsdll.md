---
layout: "post"
title: "Autodesk.Connectivity.Explorer.ExtensibilityTools.dll"
date: "2011-09-09 10:16:23"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/09/autodeskconnectivityexplorerextensibilitytoolsdll.html "
typepad_basename: "autodeskconnectivityexplorerextensibilitytoolsdll"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>I&#39;ve been getting a lot of questions about Autodesk.Connectivity.Explorer.ExtensibilityTools.dll so I thought I would explain a bit about it.&#0160; On the surface it looks like a collection of useful functions, but there&#39;s more to it that you should be aware of.</p>
<p>Most importantly, you need to be aware of two things:</p>
<ul>
<li>Using this DLL creates a mini Vault Explorer within your process. </li>
<li>There is a significant memory and performance cost with these tools.&#0160; <br />For example, Downloading a small Inventor file will cause about 60 MB of DLLs to be loaded.&#0160; The initial load may take about 1.5 seconds. </li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>The concept behind ExtensibilityTools is simple enough.&#0160; Provide some functions which do client-side operations.&#0160; The web service API is only concerned with server features.&#0160; By design, it can&#39;t do anything related to client-side data.&#0160; Meanwhile the Vault Explorer client performs a lot of client-side operations, seemingly with ease.&#0160; People would frequently ask me for these features and indicate that &quot;it should just work like Vault Explorer&quot;.</p>
<p>The trick is that a lot of the Vault Explorer features are not easy at all.&#0160; Update Properties, for example, is very hard if the property is mapped to a property in the CAD file.&#0160; In these cases, the CAD API needs to be loaded in order to update the CAD file on disk.&#0160; So that implies that A) you have a bunch of CAD API&#39;s handy and B) you have a way to determine which one to use.</p>
<p>If all that logic were placed directly into the SDK, it would become huge because you would have to re-distribute the CAD APIs along with it.&#0160; Instead, ExtensibilityTools just acts as a gateway to Vault Explorer, which already has all the supported CAD API&#39;s.&#0160; This keeps the SDK small and re-uses an existing product.&#0160; The downside is that Vault Explorer was never intended to work in this mode.&#0160; It&#39;s optimized for UI workflows, and not meant to be a utility library.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Best practices:</strong></p>
<ul>
<li>If possible, try to load IExplorerUtils only once and hold on to the reference. </li>
<li>Keep in mind that there are no easy ways to unload .NET assemblies.&#0160; To the memory cost is not temporary. </li>
<li>The ideal case is if you are a Vault Explorer customization.&#0160; In that case, you there is no load cost because Vault Explorer is already loaded.&#0160; In these cases, make sure to call ExplorerLoader.GetExplorerUtil instead of LoadExplorerUtil. </li>
</ul>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>Known issues:</strong></p>
<ul>
<li><strong>Doesn&#39;t work with base Vault</strong> - You need Workgroup, Collaboration or Professional. </li>
<li><strong>Doesn&#39;t work inside Inventor</strong> - Vault Explorer uses Inventor Apprentice, which can&#39;t run inside of Inventor.exe. </li>
</ul>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
