---
layout: "post"
title: "Vault VB Extension not loading due to Namespace setting in properties"
date: "2016-03-16 10:00:06"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2016/03/vault-vb-extension-not-loading-due-to-namespace-setting-in-properties.html "
typepad_basename: "vault-vb-extension-not-loading-due-to-namespace-setting-in-properties"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p> <p>This <a href="http://justonesandzeros.typepad.com/blog/2011/05/extension-deployment-checklist.html">post</a> has a list of things to check when a Vault Extension is not loading.</p> <p>Here is another thing to check if your extension is not loading. In the properties for the project see the setting for Root namespace (screenshot below) and compare this to the type setting in the vcet.config file.&nbsp; </p> <p>The project will have a namespace and a class similar to this:</p> <div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black">Namespace VaultTest </div> <div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black">&nbsp;&nbsp;&nbsp; Public Class Test </div> <div style="font-size: 10pt; font-family: consolas; background: #eeeeee; color: black">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Implements IWebServiceExtension </div> <p><br>In the vcet.config file the type needs to match the Namespace for the project. When “Test” is the setting for the Root namespace in the properties, the value of type for this project would need to be the following. Notice the namespace has the value from the properties as well as the value from the code. </p> <p>type="<strong>Test.VaultTest.Test</strong>, test"&gt; </p> <p>So the solution is either to remove the Root namespace from the properties or ensure that the type setting in the vcet.config includes the namespace from the properties.</p> <p>&nbsp;<br><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c82549fc970b-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="image" src="/assets/image_7dde1a.jpg" width="774" height="383"></a></p>
