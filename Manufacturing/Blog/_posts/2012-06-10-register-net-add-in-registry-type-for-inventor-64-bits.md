---
layout: "post"
title: "Register .NET add-In (registry type) for Inventor (64 bits)"
date: "2012-06-10 08:12:05"
author: "Xiaodong Liang"
categories:
  - "Inventor"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/06/register-net-add-in-registry-type-for-inventor-64-bits.html "
typepad_basename: "register-net-add-in-registry-type-for-inventor-64-bits"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>From Inventor 2009, you need 64 bit add-ins to work with 64 bit Inventor. There is no 64-bit version of the Visual Basic compiler. So VB6 is not capable of compiling to 64 bit. Even you use the same registry setting, Inventor(64 bits) cannot load it. So it is recommended that you either compile for an out of process exe component, or migrate your code to .Net. </p>  <p>Do not rely on the post build step for registration of 64 bit add-Ins. For 64bit Inventor add-in, you need to manually register the add-in through regasm utility from Microsoft .NET Framework 64-bit, e.g. </p>  <p>regasm.exe /codebase SimpleAddIn.dll</p>  <p>Regasm will be located in a directory similar to this:   <br />&quot;C:\windows\Microsoft.NET\Framework64\v2.0.50727\regasm.exe&quot;</p>  <p>Selecting the Register for COM Interop option in Visual Studio (on 64bit machine) is useless because it adds the registry entries for the add-in in the wrong section. Other settings are as same, e.g. compiled the add-in with ANY CPU configuration (The warning when setting to X64 can be ignored and you do not need to set it to X64 as AnyCPU will work).</p>
