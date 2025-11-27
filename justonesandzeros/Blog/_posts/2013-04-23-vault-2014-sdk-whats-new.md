---
layout: "post"
title: "Vault 2014 SDK - What&rsquo;s New"
date: "2013-04-23 17:17:42"
author: "Doug Redmond"
categories:
  - "Announcements"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/04/vault-2014-sdk-whats-new.html "
typepad_basename: "vault-2014-sdk-whats-new"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/Announcements3.png" /></p>
<p>The Vault 2014 SDK has some nice goodies for the development community.&#0160; Of course there are also changes that you need to make when upgrading your old code.&#0160; I’ll be going into each of these topics in more detail.&#0160; For now, here is a quick overview of what’s new.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>The VDF</strong></p>
<p>We came up with a set of tools so cool, we had to come up with a new acronym.&#0160; The Vault Development Framework (VDF) is a set of client-side libraries to support higher level operations and workflows.&#0160; It’s basically there so that you can do more with less code.</p>
<p>Here is a brief look at the architecture.</p>
<p><img alt="" src="/assets/VDFdiagram.png" /></p>
<p>The VDF doesn’t provide complete feature coverage, so there are still many cases where you need to make web service calls.&#0160; If you are updating your code from a previous release, your web service calls can stay in place for the most part.</p>
<p>There are some <a href="http://wikihelp.autodesk.com/Vault/enu/Help/Help/0378-Develope378/0379-Vault_De379">VDF pages</a> on the Vault Wiki.&#0160; I’ll be covering the same content on my blog in the upcoming weeks.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>Filestore Server</strong></p>
<p>The web services have shifted around to incorporate the concept of a <strong>filestore</strong> <strong>server</strong>.&#0160; For many years now, the top tier Vault products have allowed for the filestore and database to be on separate servers.&#0160; This is a good feature, but the API wasn’t reflecting the architecture.&#0160; All API calls used to go through the server with the filestore on it.&#0160; It’s not a very good approach since a second hop would be needed for database data.</p>
<p>In Vault 2014, the API has been re-designed so that database-related calls go straight to the database server and file-related calls go straight to the file store.&#0160; The WebService manager takes care of most of this for you, but there are some changes that will impact your code...</p>
<p><img alt="" src="/assets/FilestoreService.png" /></p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>Uploading and Downloading</strong></p>
<p>Uploading and Downloading requires both database and filestore data, so those operations can no longer be done in a single call.&#0160; The good news is that the VDF wraps the extra complexity.&#0160; I suggest you use Connection.FileManager.<strong>AcquireFiles</strong> for download and check out.&#0160; The FileManager also has <strong>AddFile</strong> and <strong>CheckinFile</strong> for the upload-related tasks.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>API Compatibility</strong></p>
<p>Vault 2014 continues with the web service API compatibility feature from the last release.&#0160; This time around, Vault supports 2 releases back, which means Vault 2012 and Vault 2013 clients will still be able to talk to a Vault 2014 server.&#0160; </p>
<p>Just like before, the compatibility feature only applies to the web service layer.&#0160; All other API features are version-specific.</p>
<p>Vault 2014 will be the last release that supports the 2012 web services.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>.vcet.config Syntax</strong></p>
<p>The XML syntax has changed for the .vcet.config files.&#0160; The new format is more flexible and allows for new extension types.&#0160; The easiest thing to do is copy-paste from the Getting Started section of the SDK documentation.&#0160; If you want to see the full spec, it’s in the Knowledgebase section of the SDK documentation.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>Vault Explorer and Job Processor Extensions</strong></p>
<p>The VaultContext class has been removed.&#0160; Instead your extension will be given a fully working Connection object.&#0160; Things are much cleaner this way since you don’t have to construct your own WebServiceManager.&#0160; It also fixes a couple of other issues, such as error 300.</p>
<p>The interface for Vault Explorer extensions has been renamed from IExtension to <strong>IExplorerExtension</strong>.</p>
<hr noshade="noshade" style="color: #ff0000;" />
<p><strong>Persistent IDs</strong></p>
<p>In the past, there was never a reliable way to reference a File from outside Vault.&#0160; File names can change, so that’s not good.&#0160; File ID and MasterId are database keys, so it’s not a good idea to use those.&#0160; In Vault 2014, we now support persistent IDs, which are guaranteed to never change.&#0160; The feature applies to Files, Folders, Items, Change Orders, and Custom Entities.</p>
<p>The relevant functions are <strong>GetPersistentIds</strong> and <strong>ResolvePersistentIds</strong> in the KnowledgeVaultService. </p>
<p><img alt="" src="/assets/Announcements3-1.png" /></p>
