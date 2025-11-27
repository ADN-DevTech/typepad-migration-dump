---
layout: "post"
title: "Vault Web View"
date: "2010-08-09 08:44:45"
author: "Doug Redmond"
categories:
  - "Sample Applications"
original_url: "https://justonesandzeros.typepad.com/blog/2010/08/vault-web-view.html "
typepad_basename: "vault-web-view"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/SampleApp3.png" /></p>
<p><strong>Update:</strong>&#0160; The <a href="http://justonesandzeros.typepad.com/blog/2011/08/vault-web-view-20.html" target="_self">Vault 2012 version</a> is available.</p>
<p>Here is a simple app which uses a custom tab view.&#0160; Vault Web View is an Internet Explorer control with a URL that is set on a folder by folder basis.&#0160; This is useful if you have data that is not in Vault, but is accessible through a browser.</p>
<p><a href="http://justonesandzeros.typepad.com/images/2010/VaultWebView/screenshot.png" target="_blank"><img alt="" border="0" src="/assets/screenshot-scaled.png" style="border: 0px none;" /></a> <br /><span style="color: #808080;">(click image to see full size)</span></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>Requirements:</strong> <br />Vault Workgroup/Collaboration/Professional 2011</p>
<p><a href="http://justonesandzeros.typepad.com/Apps/VaultWebView/VaultWebView-1.0.2.0-bin.zip">Click here to download the application</a> <br /><a href="http://justonesandzeros.typepad.com/Apps/VaultWebView/VaultWebView-1.0.2.0-src.zip">Click here to download the source code</a></p>
<hr noshade="noshade" style="color: #013181;" />
<p><strong>To use:</strong> <br />Run the installer.  <br />Right-click on a folder and run the &quot;Set URL&quot; command to set the URL for the folders you want an active Web View tab.</p>
<p><img alt="" src="/assets/command.png" /></p>
<p><strong>Notes:</strong> <br />You need to be an administrator to set the URL on a folder.  <br />There are some special strings you can put in the URL which will get replaced at runtime:  <br />&#0160;&#0160;&#0160; #FILENAME# - Gets replaced with the name of the currently selected file.  <br />&#0160;&#0160;&#0160; #FOLDERID# - The ID of the current folder.  <br />&#0160;&#0160;&#0160; #SERVER# - The server name.</p>
<p><img alt="" src="/assets/SampleApp3-1.png" /></p>
