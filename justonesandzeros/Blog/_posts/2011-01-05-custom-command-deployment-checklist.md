---
layout: "post"
title: "Custom Command Deployment Checklist"
date: "2011-01-05 07:33:50"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/01/custom-command-deployment-checklist.html "
typepad_basename: "custom-command-deployment-checklist"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>UPDATE:&#0160; This post applies only to the Vault 2011 release.&#0160; <a href="http://justonesandzeros.typepad.com/blog/2011/05/extension-deployment-checklist.html" target="_self">The Vault 2012 checklist can be found here</a>.</p>
<p>For my first post of the year, I would like to go over the various things that can keep your custom Vault Explorer command from deploying properly.&#0160; In many of the cases, you don&#39;t get an error or anything.&#0160; Instead, the command just doesn&#39;t show up, leaving you to wonder what went wrong.</p>
<p>With that in mind, I created a checklist for you to use when you can&#39;t figure out why your command is not showing up:</p>
<ul>
<li><strong>Are you using Vault Workgroup, Collaboration or Professional? </strong><br />(<em>custom commands are not available in base Vault</em>) </li>
<li><strong>Did you reset your menus in Vault Explorer? </strong><br />(<em>either delete Menus.xml or right-click on the menu bar and select Customize</em>) </li>
<li><strong>Did you deploy to [VaultClientInstallDir]\Explorer\Extensions\[myExtension]? </strong></li>
<li><strong>Did you include a .vcet.config file? </strong></li>
<li><strong>Does the .vcet.config file have the correct Assembly name? </strong><br />(<em>it should be your DLL name without the &quot;.dll&quot; at the end</em>) </li>
<li><strong>Does the .vcet.config file have the correct Folder name? </strong></li>
<li><strong>Does your assembly have an ApiVersion attribute? </strong></li>
<li><strong>Is your project using .NET 3.5 or earlier?</strong><br />(<em>.NET 4.0 is not supported for command extensions</em>. <em><a href="http://justonesandzeros.typepad.com/blog/2011/02/net-40.html" target="_self">Click here for instructions</a>.</em>)</li>
<li><strong>Does your IExtension implementation have a default constructor? </strong><br />(<em>a default constructor is a constructor with no arguments, if no constructor is in the code, the compiler will create one</em>) </li>
<li><strong>Do your CommandSite objects have non-empty Label values? </strong><br />(<em>the label can&#39;t be null or empty, even if you are not displaying a sub menu</em>) </li>
</ul>
