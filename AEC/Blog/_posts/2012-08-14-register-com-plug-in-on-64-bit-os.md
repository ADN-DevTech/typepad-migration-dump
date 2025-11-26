---
layout: "post"
title: "Register COM Plug-In on 64 bit OS"
date: "2012-08-14 10:43:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "COM"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/08/register-com-plug-in-on-64-bit-os.html "
typepad_basename: "register-com-plug-in-on-64-bit-os"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I installed the 64 bit version of Navisworks Manage and now would like to register my plug-in for it. How can I do it?</p>
<p><strong>Solution</strong></p>
<p>The dll needs to be compiled to the same architecture as the Navisworks application you want to load it into, so in case of x64 it needs to be 64 bit as well. In case of a .NET dll the build option would need to be <strong>x64</strong> or <strong>Any CPU</strong></p>
<p>The registry entries listed in the <strong>[Navisworks install folder]\api\COM\documentation\COM Interface.pdf</strong> will have a x64 suffix, e.g. <strong>Software\Autodesk\Navisworks Manage x64\8.0\COM Plugins</strong></p>
<p>If your plugin is written in .NET, then you need to register it using the 64 bit version of regasm: <strong>C:\Windows\Microsoft.NET\Framework64\v2.0.50727\regasm.exe</strong></p>
<p>If your plugin is written in VB6, then it  would need to be created as a COM exe, so that it could run out-of-process from Navisworks. <br />Note: if your VB6 plugin was compiled to be a dll, then it would be a 32 bit dll, as that’s the only type of dll/exe VB6 can create, and a 64 bit process (inc. Navisworks x64) cannot load a 32 bit dll.</p>
