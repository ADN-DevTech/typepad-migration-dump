---
layout: "post"
title: "AutoCAD 2021 .NET API on NuGet"
date: "2020-05-08 20:23:00"
author: "Madhukar Moogala"
categories:
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2020/05/autocad-2021-net-api-on-nuget.html "
typepad_basename: "autocad-2021-net-api-on-nuget"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>For AutoCAD 2021 release we have posted API package on to <a href="https://www.nuget.org/packages/AutoCAD.NET/">Nuget</a>.</p><p>We have started posting AutoCAD .NET packages from AutoCAD 2015 release, from since then we regularly update and post .NET package as per AutoCAD release cycle.</p><p>You simply select <strong>Project</strong> –&gt; <strong>Manage NuGet Packages</strong> in your Visual Studio project:</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1863c8200c-pi"><img width="297" height="441" title="48JLOeRwRI" style="display: inline; background-image: none;" alt="48JLOeRwRI" src="/assets/image_72603.jpg" border="0"></a></p><p>Search for <strong>AutoCAD .NET </strong>click install on the primary AutoCAD.NET package (the third in the list). This will install <em>acmgd.dll</em> and its related assemblies but also the other two dependent packages. You should install AutoCAD.NET.Core (i.e. <em>acmgdcore.dll</em>, etc.) if you want to create a Core Console-compatible project, of course. Either way AutoCAD.NET.Model (i.e. <em>acdbmgd.dll</em>, etc.) will get installed.</p><p><a href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1863d0200c-pi"><img width="447" height="307" title="devenv_uqRfbNrIvG" style="display: inline; background-image: none;" alt="devenv_uqRfbNrIvG" src="/assets/image_439251.jpg" border="0"></a></p><p><br></p><p>Further reading on .NET package can be found on <a href="https://www.keanw.com/2014/12/autocad-2015-apis-available-via-nuget.html">Kean’s old post.</a></p>
