---
layout: "post"
title: "Updating a Plug-in to Vault 2014"
date: "2013-05-23 07:59:13"
author: "Doug Redmond"
categories:
  - "Vault"
  - "Videos"
original_url: "https://justonesandzeros.typepad.com/blog/2013/05/updating-a-plug-in-to-vault-2014.html "
typepad_basename: "updating-a-plug-in-to-vault-2014"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/Vault3.png" /></p>
<p><img alt="" src="/assets/Videos3.png" /></p>
<p>Things changed just enough to be annoying when updating your plug-in to Vault 2014.&#0160; So I thought I would make a quick video on how to update your app.&#0160; This is actually me updating DECO 2013 to 2014, which makes for a good real-world example.&#0160; </p>
<iframe allowfullscreen="allowfullscreen" frameborder="0" height="315" src="http://www.youtube.com/embed/-DI6M_2oCd4" width="420"></iframe>
<p>&#0160;</p>
<p>Here is a quick summary of my steps, which can be done in any order:</p>
<ul>
<li>Updated my file and assembly version numbers (optional) </li>
<li>Updated my references to use the Vault 2014 SDK DLLs. </li>
<li>Added reference to Autodesk.DataManagement.Client.Framework.dll </li>
<li>Added reference to Autodesk.DataManagement.Client.Framework.Vault.dll </li>
<li>Deployed to %ProgramData%\Autodesk\Vault <strong>2014</strong>\Extensions </li>
<li>Changed the class implementation from IExtension to IExplorerExtension. </li>
<li>Updated code to use Connection object instead of creating a WebServiceManager from the VaultContext. </li>
<li>Fixed any compile errors from web service API changes.</li>
<li>Removed any code that held a reference to a WebServiceManager. </li>
<li>Updated .vcet.config file. </li>
<li>Updated the ApiVersion assembly attribute. </li>
</ul>
<p><img alt="" src="/assets/Videos3-1.png" /></p>
