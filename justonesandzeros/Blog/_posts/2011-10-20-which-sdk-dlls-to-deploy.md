---
layout: "post"
title: "Which SDK DLLs to deploy"
date: "2011-10-20 08:35:27"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/10/which-sdk-dlls-to-deploy.html "
typepad_basename: "which-sdk-dlls-to-deploy"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>So... you have your application ready, and you want to deploy it.&#0160; Which DLLs do you deploy?&#0160; This is one of those questions that can be difficult if you let it.&#0160; My job is to not let that happen.&#0160; So I&#39;ll break it down into one rule:</p>
<table border="1" cellpadding="2" cellspacing="0" width="470">
<tbody>
<tr>
<td valign="top" width="470"><strong>Any assemblies you reference in your project need to be available to the executing application at runtime.</strong></td>
</tr>
</tbody>
</table>
<p>This rule applies to every piece of software in the universe.&#0160; But let&#39;s take a closer look at what this means for the various types of Vault customizations....</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p><strong>You are building an EXE</strong></p>
<p>If your project output is an EXE, then it needs access to everything referenced in the project.&#0160; The simplest way to do this is to just put the referenced DLLs in the same folder as your EXE.&#0160; Visual Studio does this automatically, so you can just package up the program output and call it a day.</p>
<p><strong>You are building a Vault Explorer extension</strong></p>
<p>In the case of a custom command or tab view, you want to deploy everything that is referenced in your project <em>that Vault Explorer doesn&#39;t already have access to</em>.</p>
<p>If you look in your Vault Explorer folder (the Explorer folder under your client install location), you&#39;ll notice it already has some of the SDK DLLs, such as Autodesk.Connectivity.WebServices.dll.&#0160; However, not all the SDK DLLs are there, WebServicesTools for example.&#0160; So, you need to deploy the assemblies that are referenced by your project that are not in the Explorer folder.&#0160; When you deploy, the DLLs should be in the same folder as your extension DLL.</p>
<p><strong>You are building a Job Processor extension</strong></p>
<p>The rules are exactly the same as when building a Vault Explorer extension.&#0160; In fact, JobProcessor.exe is in the same folder as Vault Explorer, so list of available DLLs is also the same.</p>
<p><strong>You are building a Web Service (event handler) extension</strong></p>
<p>In this case, you don&#39;t know which app you are running in.&#0160; There are only 2 DLLs guaranteed to be available to the hosting app:&#0160; Autodesk.Connectivity.WebServices.dll and Autodesk.Connectivity.Extensibility.Framework.dll.&#0160; All other references should be deployed in the same folder as your extension.</p>
<hr noshade="noshade" style="color: #ff5a00;" />
<p>If you deploy a DLL that is already in the application&#39;s folder, you won&#39;t hurt anything.&#0160; .NET loads first from the folder that the application is running in.&#0160; So your deployed DLL will never be used in this case.</p>
<p>If multiple extensions deploy the same DLL, the only one of them will be loaded at runtime.&#0160; There is no way to know which one that will be ahead of time.</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
