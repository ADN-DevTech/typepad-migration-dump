---
layout: "post"
title: "Issues with the Vault 2012 API"
date: "2011-04-19 16:38:26"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "Must Read"
original_url: "https://justonesandzeros.typepad.com/blog/2011/04/issues-with-the-vault-2012-api.html "
typepad_basename: "issues-with-the-vault-2012-api"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Announcements2.png" /></p>
<p>There are a couple of issues that have already been found with the latest SDK.&#0160; I want to make sure everyone knows about them upfront. <br /><br /><br /></p>
<p><strong><span style="font-size: 12pt;">UserIdTicketCredentials and WebServiceCredentials</span></strong></p>
<p><strong>&#0160;</strong>Please avoid using UserIdTicketCredentials and WebServiceCredentials in Autodesk.Connectivity.WebServicesTools.dll.&#0160; We found an issue where they log out of Vault incorrectly in certain cases.</p>
<p>If you don&#39;t know what these classes are, it&#39;s because they are new in Vault 2012.&#0160; The classes are used to provide login information to the new WebServiceManager class.</p>
<p><strong>Workaround:</strong>&#0160; The links below contain fixed versions of the classes.&#0160; Basically the logout capability has been disabled so there is no danger for accidental logout.&#0160; To use, just add the .cs or .vb&#0160; to your project and use the UserIdTicketCredentials_bugfix or WebServiceCredentials_bugfix classes in place of the normal classes.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/2012Issues/Credentials_bugfix.cs" target="_blank">Click here for the C# fix</a> <br /><a href="http://justonesandzeros.typepad.com/Files/2012Issues/Credentials_bugfix.vb" target="_blank">Click here for the VB.NET fix</a></p>
<p>&#0160;</p>
<p><strong>&#0160;</strong></p>
<p><strong><span style="font-size: 12pt;">Menu resets still needed for custom commands </span></strong></p>
<p>I had this fixed.&#0160; I swear.&#0160; Unfortunately, something happened between my fix and the final release.&#0160; So when you deploy new Vault Explorer commands, you will need to reset the menus before you can see it in the menu bar.</p>
<p>The good news is that toolbars don&#39;t need a reset and menus are much more stable than they were in Vault 2011.&#0160; You should no longer see cases where the entire menu bar gets jumbled because a custom command was deployed or removed.</p>
<p><strong>Workaround:</strong>&#0160; Do a menu reset after deploying a custom command.&#0160; The reset command can be found manually by right clicking on the menu bar and running the Customize command.&#0160; Programmatically, a reset can be done by deleting the menu.xml file in <strong>%AppData%\Autodesk\[Vault Product]\Objects\Menu.xml</strong>.<br />It&#39;s the same as the Vault 2011 workaround.</p>
