---
layout: "post"
title: "Adding .NET BRep Support for AutoCAD OEM"
date: "2013-02-14 10:20:51"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD OEM"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/adding-net-brep-support-for-autocad-oem.html "
typepad_basename: "adding-net-brep-support-for-autocad-oem"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>The BRep modules are not included with any commands via the OEMMakeWizard, therefore if your .NET OEM application depends on the Brep API you will have to add the <strong>acdbmgdbrep.dll</strong> to the OEMMakeWizard by hand. </p>  <p>Here’s how:</p>  <p>1) In the OemMakeWizard <strong>Your Modules</strong> Page, add <strong>acdbmgdbrep.dll</strong> as a BindMGDARX to be placed in the EXE folder, not required</p>  <p>2) In the <strong>Your Module Settings</strong> page, for <strong>acdbmgdbrep.dll</strong>, set the Logical Name to <strong>ACDBMGDBREP </strong>and set the Load Controls to the AcadApp::LoadReasons::LoadOnAutoCADStartup i.e. 2</p>  <p>3) Rebuild.</p>
