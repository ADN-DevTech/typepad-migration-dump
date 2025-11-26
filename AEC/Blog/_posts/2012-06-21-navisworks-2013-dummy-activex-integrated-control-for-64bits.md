---
layout: "post"
title: "Navisworks 2013: dummy ActiveX Integrated control for 64bits"
date: "2012-06-21 02:00:49"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/06/navisworks-2013-dummy-activex-integrated-control-for-64bits.html "
typepad_basename: "navisworks-2013-dummy-activex-integrated-control-for-64bits"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>If you are working with ActiveX control on 64bits, this is really a good news! As we know, Integrated control has only 32bits on 32bits OS, 64bits on 64bits OS. 32bits Integrated control cannot be installed on 64bits. We cannot see the Integrated control of 64bits in the toolbox&#160; in Visual Studio because VS designer is still 32bits on 64bits OS. This is a limitation that the VS designer cannot load 64bits control. In the past, we suggest you create the project on 32bits OS, move a copy to 64bits, re-build. VS will load 64bits control at run time. Without doubt, this is tedious and we have no chance to re-design. </p>  <p>Now, in 2013, a dummy 32-bit ‘Integrated’ ActiveX control comes. This is to allow use inside the 32-bit only Visual Studio 2010 designer. When Navisworks is installed, the dummy control (32bits) is deployed to c:\Program Files (X86)\Autodesk\Navisworks Manage 2013\.&#160; You must <strong>manually</strong> register this using </p>  <p>&quot;regsvr32.exe lcodied.dll”. </p>  <p>Now you can see it in the toolbox of VS. The usage is same to what you work on 32bits OS. This control is only for development purposes and won’t be required once you have built your application. The supplied ActiveX samples will not compile unless this registration has been performed. And please remember, however at run time, the code will load the real control that is 64bits. </p>  <p>enjoy it!</p>  <p>&#160;</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016767badb48970b-pi"><img style="border-right-width: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px" title="image" border="0" alt="image" src="/assets/image_254125.jpg" width="476" height="360" /></a></p>
