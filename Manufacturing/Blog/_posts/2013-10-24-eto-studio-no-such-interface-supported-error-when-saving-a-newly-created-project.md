---
layout: "post"
title: "ETO Studio - &quot;No such Interface supported&quot; error when saving a newly created project"
date: "2013-10-24 12:45:38"
author: "Wayne Brill"
categories:
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/10/eto-studio-no-such-interface-supported-error-when-saving-a-newly-created-project.html "
typepad_basename: "eto-studio-no-such-interface-supported-error-when-saving-a-newly-created-project"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>If you use the Visual Studio ETO wizard to create a new ETO project an error may occur when you Save the project. (If it was created in the C:\Users\xxxxxx\AppData\Local\Temporary Projects\) </p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0047244c970c-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_58ed48.jpg" width="505" height="321" /></a></p>  <p>Error that can occur when you do a Save All:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00473e6c970b-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_aae3a8.jpg" width="510" height="305" /></a></p>  <p>The work around to this problem is to enable “Save new projects when created” in Visual Studio Options:</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b00479b78970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_9db201.jpg" width="522" height="347" /></a></p>  <p>With this setting enabled a new project is created and saved without error. When this option is unchecked you can create and discard projects quickly. Unfortunately this is adversely effecting the creation of new ETO Studio projects. This behavior has been reported to ETO Engineering.</p>
