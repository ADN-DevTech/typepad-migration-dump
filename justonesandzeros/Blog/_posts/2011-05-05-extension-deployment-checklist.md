---
layout: "post"
title: "Extension Deployment Checklist"
date: "2011-05-05 09:35:47"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/05/extension-deployment-checklist.html "
typepad_basename: "extension-deployment-checklist"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks2.png" /></p>
<p>I&#39;m seeing a lot of people having issues loading their Vault 2012 extension, so here is an updated deployment checklist.&#0160; The <a href="http://justonesandzeros.typepad.com/blog/2011/01/custom-command-deployment-checklist.html" target="_blank">2011 version of the checklist is here</a>.&#0160;</p>
<p>For all extension types (custom commands/tabs, job handlers, event handlers):</p>
<ul>
<li><strong>Is your project </strong><strong>using .NET 3.5 or earlier?       <br /></strong>(<em>.NET 4.0 is not supported for extensions. <a href="http://justonesandzeros.typepad.com/blog/2011/02/net-40.html" target="_self">Click here for instructions</a></em>.) </li>
<li><strong>Are you using Vault Workgroup, Collaboration or Professional? </strong> <br />(<em>extensions are not available in base Vault</em>) </li>
<li><strong>Did you deploy to %ProgramData%\Autodesk\Vault 2012\Extensions\[myExtension]? </strong></li>
<li><strong>Did you include a .vcet.config file? </strong></li>
<li><strong>Does the .vcet.config file have the correct Assembly name? </strong> <br />(<em>it should be your DLL name without the &quot;.dll&quot; at the end</em>) </li>
<li><strong>Does the .vcet.config file have the correct Extension Type? </strong></li>
<li><strong>Does your extension have values set for AssemblyCompany, AssemblyProduct, AssemblyDescription, ApiVersion and ExtensionId assembly attributes? </strong></li>
<li><strong>Do you have &quot;4.0&quot; as the attribute value for ApiVersion?</strong></li>
<li><strong>Does your IExtension, IJobHandler or IWebServiceExtension implementation have a public default constructor? </strong> <br />(<em>a default constructor is a constructor with no arguments, if no constructor is in the code, the compiler will create one</em>) </li>
</ul>
<p>Custom commands only:</p>
<ul>
<li><strong>Did you reset your menus in Vault Explorer? </strong> <br />(<em>either delete Menus.xml or right-click on the menu bar and select Customize</em>) </li>
<li><strong>Do your CommandSite objects have non-empty Label values? </strong> <br />(<em>the label can&#39;t be null or empty, even if you are not displaying a sub menu</em>) </li>
</ul>
<p>&#0160;</p>
<p><strong>Testing the load:</strong></p>
<p>If you are still having trouble, I wrote a test loader that you can use.&#0160; It will display more detailed information about why you extension is not loading.</p>
<p><img alt="" src="/assets/LoadTester.png" /></p>
<p><a href="http://justonesandzeros.typepad.com/Apps/TestLoader/TestLoader-2012.zip" target="_blank">Click here to download the utility</a></p>
<p>The compiled EXE is in the bin\Release folder.&#0160; The source code is provided too.&#0160; It makes use of some loader utilities in Autodesk.Connectivity.Extensibility.Framework.</p>
