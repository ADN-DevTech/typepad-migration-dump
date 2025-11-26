---
layout: "post"
title: "64 Bit VBA Performance Issue"
date: "2012-05-28 07:24:52"
author: "Philippe Leefsma"
categories:
  - "AutoCAD"
  - "Philippe Leefsma"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/64-bit-vba-performance-issue.html "
typepad_basename: "64-bit-vba-performance-issue"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/philippe-leefsma.html" target="_self">Philippe Leefsma</a></p>  <p>Here is a common question we get through ADN support:</p>  <p>I ported our existing VBA extension for AutoCAD to Windows 7 64 bit. After installing the VBA enabler and changing the COM references to the new version, everything is now working. Unfortunately, performance is very bad, much slower as on my previous laptop (XP 32 bit, 4 years old).</p>  <p>Do you have any hints where to find the bottleneck?</p>  <p>I did not change anything in coding.</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The fact that VBA runs slower on the 64 bit version of AutoCAD is a known issue. There is already a change request present against the reported behavior, request number 1147047 [AutoCAD has slowed in the 64-bit platforms when loaded VBA projects].</p>  <p>The main reason is that there is no x64 version of VBA. We ship the x32 VBA version in an out-of-process server with x64 AutoCAD. Therefore, a lot of marshaling is required going between the out-of-process VBA server and x64 AutoCAD. As a result, x64 VBA is slower than x86 VBA. As a solution or workaround I would suggest porting the VBA application to a .NET DLL. </p>
