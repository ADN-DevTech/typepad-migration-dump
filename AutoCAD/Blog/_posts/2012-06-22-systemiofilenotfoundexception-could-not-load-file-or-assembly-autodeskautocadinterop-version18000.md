---
layout: "post"
title: "System.IO.FileNotFoundException: Could not load file or assembly 'Autodesk.AutoCAD.Interop, Version=18.0.0.0"
date: "2012-06-22 10:04:21"
author: "Adam Nagy"
categories:
  - ".NET"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/systemiofilenotfoundexception-could-not-load-file-or-assembly-autodeskautocadinterop-version18000.html "
typepad_basename: "systemiofilenotfoundexception-could-not-load-file-or-assembly-autodeskautocadinterop-version18000"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>My AddIn is referencing the Autodesk.AutoCAD.Interop version 18.0.0.0 assembly from the ObjectARX 2010 SDK, so that it works with both AutoCAD 2010 and 2011. Unfortunately, if my application is run on a computer where only AutoCAD 2011 is installed I get a FileNotFoundException:</p>
<p>System.IO.FileNotFoundException: Could not load file or assembly &#39;Autodesk.AutoCAD.Interop, Version=18.0.0.0, Culture=neutral, PublicKeyToken=eed84259d7cbf30b&#39; or one of its dependencies. The system cannot find the file specified. File name: &#39;Autodesk.AutoCAD.Interop, Version=18.0.0.0, Culture=neutral, PublicKeyToken=eed84259d7cbf30b&#39;</p>
<p>How can I solve this problem?</p>
<p><strong>Solution</strong></p>
<p>Unfortunately, the AutoCAD 2011 installer does not install the AutoCAD 2010 version (18.0.0.0) of the Autodesk.AutoCAD.Interop.dll. This is supposed to be remedied in the oncoming Update 1.1 for AutoCAD 2011.</p>
<p>In the meantime you could create your own version of the interop assembly using tlbimp, reference that in your application and distribute this interop assembly with your application. It&#39;s recommended to use a different name for it, e.g. My.Autodesk.AutoCAD.Interop.dll <br />In the Visual Studio Command Prompt enter go to the appropriate ARX SDK include folder and type <strong>tlbimp acax18enu.tlb /out:My.Autodesk.AutoCAD.Interop.dll</strong></p>
<p>We have the same issue on x64 OS&#39;s as well, that the 64 bit interop dll&#39;s are not installed - this would require the same workaround as described above. This is supposed to be resolved for AutoCAD 2012.</p>
<p>Note: you can simply start explorer and type <strong>%WINDIR%\assembly</strong> in the Address field to see which assemblies are installed on your system.</p>
