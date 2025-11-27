---
layout: "post"
title: "Inventor View Control is not registered on Inventor 2012+"
date: "2012-05-27 20:28:32"
author: "Barbara Han"
categories:
  - "Barbara Han"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/05/inventor-view-control-is-not-registered-on-inventor-2012.html "
typepad_basename: "inventor-view-control-is-not-registered-on-inventor-2012"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/barbara-han.html" target="_self">Barbara Han</a></p> <p>Before Inventor 2012, InventorViewCtrl.ocx was registered automatically, but starting from 2012, the registry-free was introduced. That means no registry key like Inventor.ViewControl.1 will be written. </p> <p>Plug-in developers have to link the corresponding manifest files to their application if they want to use the OCX. This manifest file can be created with mt.exe (MS tool). More information about registry-free COM, please refer to <a href="http://msdn.microsoft.com/en-us/library/ms973913.aspx">http://msdn.microsoft.com/en-us/library/ms973913.aspx</a> .</p> <p>Bottom line: you have two options:<br>1. Use mt.exe to generate manifest file by yourself and link the manifest file to your application.<br>2. Use old style. But you have to register InventorViewCtrl.ocx manually by running “Regsvr32 InventorViewCtrl.ocx”.</p>
