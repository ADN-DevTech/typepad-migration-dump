---
layout: "post"
title: "Vault Filestore Server"
date: "2013-06-20 18:05:14"
author: "Doug Redmond"
categories:
  - "Concepts"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/06/vault-filestore-server.html "
typepad_basename: "vault-filestore-server"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/Concepts3.png" /></p>
<p>Welcome to my 256th blog posting.&#0160; If you are one of my loyal readers, you probably already know why that is significant.&#0160; Regardless, I’ll be “celebrating” by going over the filestore server...</p>
<p>One of the major architecture changes in Vault 2014 is the filestore server.&#0160; In order to explain this architecture, I need to first explain how things worked in 2013.&#0160; </p>
<p>If you had a multi-site environment in Vault 2013, it looked like this.    <br /><img alt="" src="/assets/oldArch.png" /></p>
<p>There is a problem with this architecture, every time you need data from the database, you need to go through two hops.&#0160; Just about everything you do in Vault requires the database, so the extra hop is significant.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>For Vault 2014 the architecture, was updated to look like this.    <br /><img alt="" src="/assets/newArch.png" /></p>
<p>DB calls go directly to the database, so it’s only one hop.&#0160; That efficiency comes at a cost however.&#0160; The new architecture is more complex.</p>
<hr noshade="noshade" style="color: #5acb04;" />
<p>Things that may be confusing:</p>
<ul>
<li>This architecture is always in place, even for Vault Basic and single-site installs.&#0160; It’s just easier to have 1 architecture. </li>
<li>For multi-site, a client will actually be talking to two different servers. </li>
<li>You log-in to the filestore server. </li>
<li>Internally the WebServiceManager or Connection object asks the filestore server where the database server is located. </li>
<li>File upload/download operations require communicating with both servers.&#0160; Again, the Connection object handles the details internally. </li>
<li>For backward compatibility purposes, the filestore server handles all 2012 and 2013 web service calls. You need to use the 2014 API if you want the one-hop behavior.</li>
<li>There are now two sets of server log files.&#0160; vlog is for database server issues and AVFS-log is for filestore issues.&#0160; Even on a single server you see these two logs.</li>
</ul>
<hr noshade="noshade" style="color: #5acb04;" />
<p>The functionality is broken down by web service.&#0160; So some services are on the database server and some are on the filestore server.&#0160; There are no services that show up on both, with the exception of the <a href="http://justonesandzeros.typepad.com/blog/2010/11/the-information-service.html">Information Service</a>.</p>
<p>Filestore services:</p>
<ul>
<li>AuthService </li>
<li>FilestoreService </li>
<li>FilestoreVaultService </li>
<li>IdentificationService </li>
<li>InformationService </li>
</ul>
<p>Database services:</p>
<ul>
<li>AdminService </li>
<li>BehaviorService </li>
<li>CategoryService </li>
<li>ChangeOrderService </li>
<li>ContentService </li>
<li>CustomEntityService </li>
<li>DocumentService </li>
<li>DocumentServiceExtensions </li>
<li>ForumService </li>
<li>InformationService </li>
<li>ItemService </li>
<li>JobService </li>
<li>KnowledgeVaultService </li>
<li>LifeCycleService </li>
<li>PackageService </li>
<li>PropertyService </li>
<li>ReplicationService </li>
<li>RevisionService </li>
<li>SecurityService </li>
</ul>
<p>The URLs are different too depending on which server you are talking too.&#0160; Database services are <a href="http://[server]/autodeskDM/services/v18/[service].svc" title="http://nov-depot/autodeskDM/services/v18/DocumentService.svc">http://[server]/autodeskDM/services/v18/[service].svc</a> while filestore services are <a href="http://[server]/autodeskDM/services/filestore/v18/[service].svc" title="http://nov-depot/autodeskDM/services/filestore/v18/authService.svc">http://[server]/autodeskDM/services/filestore/v18/[service].svc</a>. But you shouldn’t have to care about that if you are using WebServiceManager or Connection.</p>
<p><img alt="" src="/assets/Concepts3-1.png" /></p>
