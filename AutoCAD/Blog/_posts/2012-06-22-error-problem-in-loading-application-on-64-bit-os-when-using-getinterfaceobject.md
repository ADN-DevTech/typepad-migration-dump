---
layout: "post"
title: "Error \"Problem in loading application\" on 64 bit OS when using GetInterfaceObject"
date: "2012-06-22 06:48:08"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/error-problem-in-loading-application-on-64-bit-os-when-using-getinterfaceobject.html "
typepad_basename: "error-problem-in-loading-application-on-64-bit-os-when-using-getinterfaceobject"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I have an AutoCAD .NET AddIn which provides an ActiveX server that allows me to drive my AddIn from an external application. This works fine on a 32 bit OS, but on 64 bit I get a &quot;Problem in loading application&quot; error when using GetInterfaceObject() to access my ActiveX server. I checked and the AddIn is loaded, so I&#39;m not sure what goes wrong.</p>
<p><strong>Solution</strong></p>
<p>When you are building your application on a 64 bit OS, then the ActiveX server inside your AddIn is registered by Visual Studio in the 32 bit hive of the registry since Visual Studio itself is 32 bit even on a 64 bit OS. So you have to register your component yourself using the 64 bit version of regasm.</p>
<p>The easiest solution is if you just add a Build Event to your project which runs after each successful build of your project:</p>
<p><span style="white-space: pre; font-family: &#39;courier new&#39;, courier; font-size: 8pt; background-color: #e6e6e6;">&quot;C:\Windows\Microsoft.NET\Framework64\v2.0.50727\regasm.exe&quot; &quot;$(TargetPath)&quot; /tlb /codebase</span></p>
