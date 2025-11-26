---
layout: "post"
title: "Some Tips on AutoCAD OEM"
date: "2013-04-22 10:22:47"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/some-tips-on-autocad-oem.html "
typepad_basename: "some-tips-on-autocad-oem"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>Just some small for new users of AutoCAD OEM…</p>  <p>1) When you install OEM on your system, you install two things - the Toolkit (OemMakeWizard and OemInstallerWizard) and a runnable application called AutoCAD OEM.</p>  <p>2) AutoCAD OEM is a relaxed version of any OEM product you build with the OemMakeWizard. It is used to test OEM specific stuff you are doing, it allows NETLOAD, Appload and the VBAIDE (if VBA is installed)...</p>  <p>3) The modules you add to OEM are usually just your modules and direct dependencies – basically the ones that you load into normal AutoCAD, and the ones that your App relies on (excluding standard runtime modules). </p>  <p>4) DLLs that you include *must* be built with the OEM ObjectARX SDK because OEM modules are dependent on aoem.exe not acad.exe like in the normal ObejctARX SDK. If you try to load modules built with the normal ObjectARX SDK they will not load because they are being physically loaded into the wrong host exe.</p>  <p>5) When you rebuild your app DLLs, you must rebuild your OEM product in order to test them - basically, your application DLLs must be restamped and aligned with your OEM product.</p>  <p>6) In order for your application to load properly, you must have a unique Logical Name in Your Modules declaration page of the OemMakeWizard.</p>
