---
layout: "post"
title: "Inventor ETO with Vault - set &ldquo;Embed Interop Types&rdquo; to False for Autodesk.Inventor.Interop"
date: "2012-10-18 17:47:52"
author: "Wayne Brill"
categories:
  - "Engineer-To-Order"
  - "Wayne"
original_url: "https://modthemachine.typepad.com/my_weblog/2012/10/inventor-eto-with-vault-set-embed-interop-types-to-false-for-autodeskinventorinterop.html "
typepad_basename: "inventor-eto-with-vault-set-embed-interop-types-to-false-for-autodeskinventorinterop"
typepad_status: "Publish"
---

<p>Recently I had a case where using the Vault Designs in Inventor Engineer-To-Order would cause this error and fail:</p>
<p>&quot;<em>The invoked member is not supported in a dynamic assembly</em>&quot;</p>
<p>I was unable to recreate the problem on my machine. By the process of elimination we found that it was an custom AddIn that was related to the error occurring. The solution was to simply change the setting in Visual Studio for “Embed Interop Types” to False. This screenshot shows the setting that needed to be changed in the AddIn:</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc68834017ee4429839970d-pi"><img alt="image" border="0" height="391" src="/assets/image_810671.jpg" style="background-image: none; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="image" width="408" /></a></p>
<p>-Wayne</p>
