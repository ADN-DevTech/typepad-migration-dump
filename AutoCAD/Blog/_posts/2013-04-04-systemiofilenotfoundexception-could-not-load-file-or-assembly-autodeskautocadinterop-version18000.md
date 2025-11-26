---
layout: "post"
title: "System.IO.FileNotFoundException: Could not load file or assembly 'Autodesk.AutoCAD.Interop, Version=18.0.0.0"
date: "2013-04-04 03:21:00"
author: "Adam Nagy"
categories:
  - ".NET"
  - "2011"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2013/04/systemiofilenotfoundexception-could-not-load-file-or-assembly-autodeskautocadinterop-version18000.html "
typepad_basename: "systemiofilenotfoundexception-could-not-load-file-or-assembly-autodeskautocadinterop-version18000"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p><strong>Issue</strong></p>
<p>My AddIn is referencing the <strong>Autodesk.AutoCAD.Interop</strong> version <strong>18.0.0.0</strong> assembly from the <strong>ObjectARX 2010 SDK,</strong> so that it works with AutoCAD 2010, 2011 and 2012. Unfortunately, if my application is run on a computer where only AutoCAD 2011 is installed I get a FileNotFoundException:</p>
<p>System.IO.FileNotFoundException: Could not load file or assembly &#39;Autodesk.AutoCAD.Interop, Version=18.0.0.0, Culture=neutral, PublicKeyToken=eed84259d7cbf30b&#39; or one of its dependencies. The system cannot find the file specified.<br />File name: &#39;Autodesk.AutoCAD.Interop, Version=18.0.0.0, Culture=neutral, PublicKeyToken=eed84259d7cbf30b&#39;</p>
<p><strong>Solution</strong></p>
<p>Unfortunately, the AutoCAD 2011 installer does not install the AutoCAD 2010 version (18.0.0.0) of the Autodesk.AutoCAD.Interop.dll. This issue is supposed to be remedied in Update 1.1 for AutoCAD 2011.</p>
<p>You could also work around the issue by creating your own version of the interop assembly using tlbimp, reference that in your application&#0160;and distribute&#0160;this interop assembly&#0160;with your application. It&#39;s recommended to use a different name for it, e.g.&#0160;My.Autodesk.AutoCAD.Interop.dll<br />In the Visual Studio Command Prompt navigate to the appropriate ARX SDK include folder and type&#0160;<strong>tlbimp acax18enu.tlb /out:My.Autodesk.AutoCAD.Interop.dll</strong></p>
<p>We have the same issue on x64 OS&#39;s as well, that the 64 bit interop dll&#39;s are not installed - this would require the same workaround as described above. This issue is supposed to be fixed in AutoCAD 2012.</p>
<p>Note: you can&#0160;simply start explorer and type&#0160;<strong>%WINDIR%\assembly</strong>&#0160;in the&#0160;<strong>Address</strong>&#0160;field to see which assemblies are installed on your system.&#0160;</p>
