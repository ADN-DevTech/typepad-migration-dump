---
layout: "post"
title: "AcquireFiles Download not fixing references of renamed files after installing update"
date: "2014-12-11 11:55:31"
author: "Wayne Brill"
categories:
  - "Vault"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2014/12/acquirefiles-download-not-fixing-references-of-renamed-files-after-installing-update.html "
typepad_basename: "acquirefiles-download-not-fixing-references-of-renamed-files-after-installing-update"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>If a referenced part file is renamed and AcquireFiles is used to download the parent assembly. (include Children) the assembly and children part files are downloaded but the name of the child part file may not have the new name. Also the ResultDescription may have this error:</p>  <p>“Unknown error was encountered while updating file references&quot;</p>  <p>Here is a screenshot of the Watch window:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c71d5a5f970b-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; margin: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="image" src="/assets/image_946dde.jpg" width="596" height="298" /></a></p>  <p>When the assembly is opened in Inventor. (It is looking for the ipt with the old name). Using the similar code with Vault 2015 this problem is not occurring.&#160; </p>  <p><strong>Solution:</strong></p>  <p>In this case the problem was caused by older dlls in the SDK after a the vault service pack was installed. When Exceptions were enabled in Visual Studio this exception was displayed. Notice the Version number:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07c21258970d-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; background-image: none; border-bottom-width: 0px; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-top-width: 0px" border="0" alt="image" src="/assets/image_4c50e5.jpg" width="575" height="329" /></a>&#160;&#160;&#160; <br /></p>  <p>The version of Vault Explorer was 18.2.2.0. (Update 2). The problem was be resolved by uninstalling the Vault API SDK and reinstalling it. (Reference the updated dlls in the SDK bin directory in Visual Studio). The SDK install is here by default:</p>  <p><em>C:\Program Files\Autodesk\Vault Professional 2014\SDK </em></p>  <p><strong>Note:</strong> Be sure to back up any customization you have done to the SDK samples before uninstalling the SDK.&#160; </p>
