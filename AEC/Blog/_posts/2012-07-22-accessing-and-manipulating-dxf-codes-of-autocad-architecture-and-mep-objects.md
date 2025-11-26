---
layout: "post"
title: "Accessing and manipulating DXF codes of AutoCAD Architecture and MEP objects"
date: "2012-07-22 14:08:01"
author: "Mikako Harada"
categories:
  - "AutoCAD Architecture"
  - "AutoCAD MEP"
  - "Lisp"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/accessing-and-manipulating-dxf-codes-of-autocad-architecture-and-mep-objects.html "
typepad_basename: "accessing-and-manipulating-dxf-codes-of-autocad-architecture-and-mep-objects"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>Is there documentation on DXF codes of AEC objects? Are there any functions or variables that can be directly accessed via LISP as opposed to parsing them manually?</p>
<p><strong>Solution</strong></p>
<p>The creation and modification of AEC objects using DXF codes is NOT an officially supported programming method for AutoCAD Architecture (ACA) and MEP (AME). AEC objects are complex entities, and have not designed to be used in this fashion. Please also note that LISP has not been officially supported API for ACA and AME.&#0160;</p>
<p>In pre-2004 releases, accessing AEC objects through DXF may have worked for some objects (although mostly they were in read-only mode). From AutoCAD Desktop (ADT) 2004, AEC objects do not support DXF.&#0160; And DXFIN/DXFOUT and SaveAs DXF format for AEC has been disabled since 2004 release.</p>
<p>Further, in spring of year 2008, we have announced that we will be removing all AEC related DXF support completely from 2010 and later releases of ACA/AME.&#0160; This transition is not complete.&#0160; So you may still be able to use some of DXF code you find with AEC objects.&#0160; But please expect that it may be dropped at any time in later releases, too.&#0160;&#0160;&#0160;</p>
<p>This means that your LISP routines that rely on ACA/AME specific group codes may no longer function as before.&#0160;</p>
<p>As a workaround when you encounter dropped functionality, you may still be able to use ActiveX API through LISP vla support. Alternatively, you can also write a missing function in .NET and call from LISP.&#0160; If you are new to .NET, the following webcast will give a good starting point:</p>
<p>Webcast: <a href="http://download.autodesk.com/media/adn/AutoCAD.NETforLispProgrammersWebcast_16Oct08.zip" target="_self" title="Webcast AutoCAD .NET for LISP programmers">AutoCAD: .NET for LISP Programmers</a></p>
