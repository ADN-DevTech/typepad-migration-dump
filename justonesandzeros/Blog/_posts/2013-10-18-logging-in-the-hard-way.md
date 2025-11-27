---
layout: "post"
title: "Logging in the Hard Way"
date: "2013-10-18 11:02:25"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
  - "Vault"
original_url: "https://justonesandzeros.typepad.com/blog/2013/10/logging-in-the-hard-way.html "
typepad_basename: "logging-in-the-hard-way"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault4.png" />    <br /><img alt="" src="/assets/TipsAndTricks4.png" /></p>
<p>Because of the new Filestore Server architecture, logging in to Vault 2014 takes up more steps than it did before.&#0160; If you are using the WebServiceManager or Connection object, fell free to skip this article because the extra steps are performed automatically for you.&#0160; But if you are making direct calls to the web service URLs, then keep reading.&#0160; I’ll outline the new steps for logging in.</p>
<p>First, make sure you understand the <a href="http://justonesandzeros.typepad.com/blog/2013/06/vault-filestore-server.html">AVFS architecture</a>.&#0160; Even if everything is installed on the same computer, your code needs to see Vault as two separate servers.&#0160; The link above also lists which services are on which server.</p>
<p>The SignIn calls are in the AuthService and WinAuthService, which are part of AVFS.&#0160; If you are showing a login dialog to the user, for example, then the user is expected to type in the name of the Filestore Server, not the Data Server.</p>
<p>If you are logging in with Vault username/password, use the AuthService.&#0160; If you are logging in with Windows credentials, use WinAuthService.&#0160; Depending on the needs of your app, use either SignIn(), SignIn2() or SignInReadOnly() to authenticate. </p>
<hr noshade="noshade" style="color: #d09219;" />
<p>Once you are authenticated, you need to figure out where the Data Server is located.&#0160; You do this by calling IdentificationService.<strong>GetServerIdentities</strong>, which returns an array of ServerIdentities.&#0160; Each ServerIdentity represents a Filestore Server and it’s related Data Server.&#0160; There may be multiple results if there are multiple filestores installed.&#0160; </p>
<p>Find the ServerIdentity that matches the filestore you just authenticated to.&#0160; Remember the DataServer value, because that is the server to call for all web services on the data server.&#0160; </p>
<p>Once you have it set up&#0160; so that each web service calls are going to the correct server, you mostly don&#39;t have to worry about about the different server types.&#0160; The glaring exception is file IO, which I explain in my article: <a href="http://justonesandzeros.typepad.com/blog/2013/07/file-transfer-doing-it-the-hard-way.html">File Transfer - Doing it the Hard Way</a>.</p>
<hr noshade="noshade" style="color: #d09219;" />
