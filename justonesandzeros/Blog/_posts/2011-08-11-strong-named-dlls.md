---
layout: "post"
title: "Strong Named DLLs"
date: "2011-08-11 07:56:07"
author: "Doug Redmond"
categories:
  - "Tips and Tricks"
original_url: "https://justonesandzeros.typepad.com/blog/2011/08/strong-named-dlls.html "
typepad_basename: "strong-named-dlls"
typepad_status: "Publish"
---

<p><img alt="" src="/assets/TipsAndTricks3.png" /></p>
<p>For Vault 2011, I told you <a href="http://justonesandzeros.typepad.com/blog/2010/10/signing-the-webservices-dll.html">how to sign and unsigned DLL</a>.&#0160; Those steps worked great if you have a single, isolated DLL.&#0160; For Vault 2012, there are multiple interlinked DLLs, so the steps get much more complicated.</p>
<p>Instead of going over complicated steps, I&#39;ll just post the strong-named versions of the DLLs.&#0160; These DLLs are not officially supported, but they are the same ones used in the Vault SharePoint integration.&#0160; So some level of testing has been performed.</p>
<p><a href="http://justonesandzeros.typepad.com/Files/DLLs/StrongNamedVaultDLLs-2012.zip">Click here to download Vault 2012 strong named DLLs</a></p>
<p>Do NOT use these DLLs for Vault extensions.&#0160; They are only to be used for EXEs and plug-ins to non-Autodesk products.&#0160; The Autodesk apps use the non-signed versions of the DLLs, and you can&#39;t mix the two together.&#0160;</p>
<p><img alt="" src="/assets/TipsAndTricks3-1.png" /></p>
