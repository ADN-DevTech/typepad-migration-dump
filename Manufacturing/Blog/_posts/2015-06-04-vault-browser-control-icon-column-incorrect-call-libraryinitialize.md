---
layout: "post"
title: "Vault Browser control &ndash; icon column incorrect &ndash; call .Library.Initialize()"
date: "2015-06-04 12:21:54"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2015/06/vault-browser-control-icon-column-incorrect-call-libraryinitialize.html "
typepad_basename: "vault-browser-control-icon-column-incorrect-call-libraryinitialize"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>This Login method used to log in to the Vault does not support a read-only login.&#160; </p>  <p><em>Autodesk.DataManagement.Client.Framework.Vault.Forms Namespace &gt; Library&#160; Login Method</em>     <br /></p>  <p>To log in read only you could use:</p>  <p><em>Autodesk.DataManagement.Client.Framework.Vault.Services Namespace &gt; IVaultConnectionManagerService Interface &gt; LogIn Method</em>     <br /></p>  <p>Logging in this way adversely effects the Vault Browser control. Instead of the getting the icons in the icon column you get text with the ImageInfo type. (this behavior has been reported to Vault engineering).</p>  <p>The solution is to Initialize the library before logging in by making this call:</p>  <p><em>Autodesk.DataManagement.Client.Framework.Vault.Forms.Library.Initialize()</em></p>
