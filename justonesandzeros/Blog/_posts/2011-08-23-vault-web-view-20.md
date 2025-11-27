---
layout: "post"
title: "Vault Web View 2.0"
date: "2011-08-23 11:32:59"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2011/08/vault-web-view-20.html "
typepad_basename: "vault-web-view-20"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p>Update:&#0160; <a href="http://justonesandzeros.typepad.com/blog/2013/09/vault-web-view-2014.html" target="_self">Vault Web View 2014</a> is now available.&#0160; Sorry, there is no 2013 version.</p>
<p>Here is my update of <a href="http://justonesandzeros.typepad.com/blog/2010/08/vault-web-view.html">Vault Web View</a>.&#0160; It&#39;s basically a web control embedded in a Vault Explorer tab.&#0160;</p>
<p><img alt="" src="/assets/screenshot-scaled.png" /></p>
<p>To use it, your Vault administrator sets up a Vault property which will contain the URL information.&#0160; After that, whenever you select a Vault object that contains the property, the Web View tab will browse to that URL.&#0160; The URL can be set and updated using Vault&#39;s property editor.</p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong> <br />Vault Workgroup/Collaboration/Professional 2012</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/VaultWebView/VaultWebView-2.0.1.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/VaultWebView/VaultWebView-2.0.1.0-src.zip">Click here to download the source code</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Enhancements:      <br /></strong>In version 1.0 a custom command was needed to set and update a folder URL.&#0160; In Vault 2012, folders can have property values, so Web View was updated to use properties instead of a custom command.&#0160; Version 2 also allows you to set URL data on all main entity types (files, folders, items and change orders).&#0160; Lastly, you don&#39;t need to be an administrator to set the URL.&#0160; Anyone who can set property data, can set the URL.&#0160; I&#39;m not sure if this is good or not, but it&#39;s a result of using Vault property data.</p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
