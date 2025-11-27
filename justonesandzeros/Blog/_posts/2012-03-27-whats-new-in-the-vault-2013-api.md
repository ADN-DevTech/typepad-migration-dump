---
layout: "post"
title: "What&rsquo;s new in the Vault 2013 API"
date: "2012-03-27 13:17:37"
author: "Doug Redmond"
categories:
  - "Announcements"
original_url: "https://justonesandzeros.typepad.com/blog/2012/03/whats-new-in-the-vault-2013-api.html "
typepad_basename: "whats-new-in-the-vault-2013-api"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Announcements3.png" /></p>
<p>We’ve officially released the Vault 2013 line of products, so I can now talk about the new API features.&#0160; This has been another good release for the API, some very nice stuff has been added for you to do interesting things with.&#0160; I’ll also list some of the more noticeable changes you will run into if you are coming from the Vault 2012 API.</p>
<p>In the coming weeks, I’ll be elaborating on most of these features, but for now I’ll just give you a high level view of the new API.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>New Feature:&#0160; Custom Entities      <br /></strong>Vault Professional allows you do define your own entity types.&#0160; You are no longer forced to use Files, Folders, Items and Change Orders for all your data.&#0160; Custom entities support properties, categories, ACL security, links and lifecycles (the same engine that files use).&#0160; The only behavior you might miss is versions and revisions.</p>
<p>Through the API you can create custom commands, custom tabs, custom new and delete behavior, and lifecycle events for you custom entity types.</p>
<p><strong>New Feature:&#0160; API Compatibility      <br /></strong>Vault Server 2013 contains two sets of web services.&#0160; Once set is the new 2013 services, but the 2012 services are still there too.&#0160; This means that 2012 clients will still work with the 2013 server.&#0160; No update required.</p>
<p>Note:&#0160; Compatibility does not apply to plug-ins such as custom commands, job handlers and event hooks.</p>
<p><strong>New Feature:&#0160; Job Processor API      <br /></strong>The IJobHandler interface has some new additions.&#0160; You can now run code during startup and shutdown.&#0160; You also have hooks for sleep, which is right before Job Processor goes idle, and wake, which is right after the idle period is over.</p>
<p>Job Processor itself has some enhancements.&#0160; It can use Windows credentials for login (however a Vault license is still consumed).&#0160; There are also new command line options for Job Processor so you can do service related operations, such as pause, resume and stop.</p>
<p><strong>New Feature:&#0160; Auto Ownership Transfer      <br /></strong>Transferring entities in a replicated environment just got much easier.&#0160; First transfers usually happen instantly instead of requiring you to wait for a few minutes.&#0160; Also the WebServiceManager will automatically transfer ownership for you attempt to edit an entity that is owned by another workgroup.&#0160;</p>
<p><strong>New Feature:&#0160; Impersonation      <br /></strong>If your program is logged in as an administrator, it can switch its credentials to that of another user.&#0160; This is useful for utilities that edit Vault data, but you want it to preserve the user that originally created the data.</p>
<p><strong>Change:&#0160; .NET 4.0      <br /></strong>All the SDK DLLs have been moved over to .NET 4.0.&#0160; Since 4.0 uses a new CLR, you can’t use projects that are 3.5 or earlier.&#0160; All SDK samples have been updated to Visual Studio 2010.</p>
<p><strong>Change:&#0160; Strong named assemblies      <br /></strong>All SDK DLLs now have a strong name.&#0160; It’s recommended you strong name your project if possible.</p>
<p><strong>Change:&#0160; Autodesk.Connectivity.WebServicesTools.dll has been removed.      <br /></strong>All the code has been moved into Autodesk.Connectivity.WebServices.dll.&#0160; So no code change is needed, you just have one less assembly to link to and deploy.&#0160; The WebServicesTools namespace has not changed.</p>
<p><strong>Change:&#0160; System Name control      <br /></strong>You can now define the system name for objects created through the API.&#0160; This gives you a reliable string that you can use to look up things like property definitions and categories.</p>
<p><strong>Change:&#0160; Lifecycle Service      <br /></strong>The code for managing file lifecycles has been moved to the new lifecycle service.&#0160; The functions have been genericized to use entities instead of files. This change was done to support lifecycles on Folders and Custom Entities.</p>
<p><strong>Change:&#0160; byte [] has been changed to ByteArray      <br /></strong>Web service functions have been updated to transfer a ByteArray object in most cases where byte [] was used, for example, upload and download.&#0160; This change was done to support the compression feature.</p>
<p>By default, compression is not used.&#0160; However the option is there to use LZ4 compression when transferring binary data.</p>
<p><img alt="" src="/assets/Announcements3-1.png" /></p>
