---
layout: "post"
title: "Change an Autodesk Exchange App installer from Single User to an All Users install"
date: "2012-09-27 09:48:26"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "Mac"
  - "ObjectARX"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/change-an-autodesk-exchange-app-installer-from-single-user-to-an-all-users-install.html "
typepad_basename: "change-an-autodesk-exchange-app-installer-from-single-user-to-an-all-users-install"
typepad_status: "Publish"
---

<p>By default all Autodesk Exchange App installers are built to deploy the App bundles to the <strong>%APPDATA%\Autodesk\ApplicationPlugin</strong> folder. This folder is specifically a Single User install location, which means that if any other user logs onto the machine the App will need to be reinstalled for that user to be available, to that user.</p>  <p>The static install folder design (folders that cannot be changed) was specifically designed to help developers bypass the 2nd stage installer issues that comes with installing plugins, particularly difficult in AutoCAD, for the All User installation scenario. The way this was handled was to include a different static folder designed to support loading apps for the All User installation model, this folder can be found in the <strong>%PROGRAMDATA%\Autodesk\ApplicationPlugins </strong>folder.</p>  <p>The question is, how do I change an Exchange App Installer from being a Single User to an All User installer… </p>  <p>The answer - It’s all about changing the static folder installation path from the %APPDATA%\Autodesk\ApplicationPlugin (Single User) to %PROGRAMDATA%\Autodesk\ApplicationPlugins (All User). There are two ways that this can be achieved:</p>  <p>1) From a DOS command window:</p>  <p><b>c:\&gt;msiexec /i MyExhangeApp.msi INSTALLDIR=C:\ProgramData\Autodesk\ApplicationPlugins AUTODESK=C:\ProgramData\Autodesk</b></p>  <p>2) You can edit the MSI directly using ORCA.exe and change the Directory Table-&gt;AUTODESK from <strong>AppDataFolder</strong> to <strong>CommonAppDataFolder</strong></p>
