---
layout: "post"
title: "Can I use Vault API to edit a file?"
date: "2012-05-08 18:59:51"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/can-i-use-vault-api-to-edit-a-file.html "
typepad_basename: "can-i-use-vault-api-to-edit-a-file"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/barbara-han.html" target="_self">Barbara Han</a></p>  <p>If you only need to edit the properties of the file, and you are using Vault 2012 or 2013, then you can use Vault API -&#160; ExplorerUtil.UpdateFileProperties for doing this. It will auto-check out file and update the properties and check the file in. If your program is not a <a href="http://justonesandzeros.typepad.com/blog/2011/08/what-makes-a-vault-extension.html">Vault Explorer extension</a>, you can refer to VaultFileBrowser sample in Vault SDK to see how to use LoadExplorerUtil API to initialize ExplorerUtil interface. If your program is a Vault Explorer extension, you should use GetExplorerUtil to get ExplorerUtil interface.     <br /></p>  <p>If you are using Vault 2011, or if what you want to edit is more than file properties, the only solution is to check out file (through Vault API “CheckoutFile” or using Vault AddIn from CAD software), then use CAD software to edit the file (through CAD's API or UI), then check the file in to Vault again (through Vault API “CheckinFile” or using Vault AddIn from CAD software).</p>
