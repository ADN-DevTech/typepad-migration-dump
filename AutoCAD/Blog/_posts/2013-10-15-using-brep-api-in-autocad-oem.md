---
layout: "post"
title: "Using BRep API in AutoCAD OEM"
date: "2013-10-15 07:35:46"
author: "Balaji"
categories:
  - "2013"
  - "2014"
  - "AutoCAD"
  - "AutoCAD OEM"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2013/10/using-brep-api-in-autocad-oem.html "
typepad_basename: "using-brep-api-in-autocad-oem"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>Recently a developer reported this error message when a custom .arx module that made BRep calls was being loaded at startup : "'xxx.arx cannot find a
procedure that it needs.”</p>
<p>A way to narrow down the procedure that is not being found is to use gflags.exe that is part the debugging tools for Windows from the Windows SDK. Here are the steps :</p>
<p>1) Run the gflags.exe and specify the OEM product name.</p>
<p>2) Hit the Tab key and turn on the loader snaps&nbsp;</p>
<a class="asset-img-link" style="float: left;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b000c151b970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019b000c151b970d" style="margin: 0px 5px 5px 0px;" title="Gflags" src="/assets/image_68327.jpg" alt="Gflags" /></a>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>3) Run the OEM product under the Visual Studio debugger until the error message appears in the command prompt.</p>
<p>4) Look at the messages in the Visual Studio output window to identify the procedure name that is not being found similar to the one shown here : </p>
<p>
LdrpSnapThunk - ERROR: Procedure "?isSelfIntersecting@AcGeImpCurve3d@@UEBAHPEAVAcGeSelfIntersectTestCallback@@AEBVAcGeTol@@@Z" could not be located in DLL "AcGe19.dll"
First-chance exception at 0x77350108 in TTGCAD.exe: 0xC0000139: Entry Point Not Found.
</p>
<p>In this case a method in the "AcGe19.dll" was not being found. Copying
"AcGe19.dll" from the OEM installer folder under
"\x64\aoem\Program Files\Root\acge19.dll" to the OEM 2013 folder
and rebuilding the product resolved the issue.</p>
