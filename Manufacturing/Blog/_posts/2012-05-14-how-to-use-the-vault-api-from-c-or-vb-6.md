---
layout: "post"
title: "How to use the Vault API from C++ or VB 6?"
date: "2012-05-14 22:24:20"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Vault"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/how-to-use-the-vault-api-from-c-or-vb-6.html "
typepad_basename: "how-to-use-the-vault-api-from-c-or-vb-6"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p>
<p>First of all, you may create a project in C# or VB.NET project which contains just the web service proxies. If you are using managed C++, you can simply create a C++ project which references the Vault web services.</p>
<p>If you are using unmanaged C++, it is much more complex. You need to wrap the Vault web services in a DLL and import that into your C++ project.The attached sample solves the problem by wrapping the Vault web services inside a DLL and exporting the needed API functions, then the exported API can be used by C++ app or VB 6 app through the DLL import mechanism.</p>
<p>This sample contains 3 applications:</p>
<p>1. VaultAPIWrapper - wrapping the Vault web services inside a DLL.</p>
<p>2. VaultAPIExport - exporting the needed API functions from VaultAPIWrapper</p>
<p>3. VaultList – a client application that consumes the exported APIs.</p>
<p>Compiling order is application 1#-&gt;2#-&gt;3#. You may need to change the parameter of SetRemoteHost and SignIn function in VaultList to point to the server and Vault database that you are trying to login to.</p>
<p>This sample is also included in Vault SDK.</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b0167667ffcaf970b"><a href="http://adndevblog.typepad.com/files/vaultdllsample.zip">Download VaultDLLSample</a></span></p>
