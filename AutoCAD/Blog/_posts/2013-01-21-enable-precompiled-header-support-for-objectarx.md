---
layout: "post"
title: "Enable Precompiled Header support for ObjectARX"
date: "2013-01-21 10:25:04"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/enable-precompiled-header-support-for-objectarx.html "
typepad_basename: "enable-precompiled-header-support-for-objectarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>You need to create the Precompiled header through stdafx.cpp first before you can use it.</p>  <ol>   <li>First turn on Precompiled Header support for the whole project, Right click and goto the Project properties, select &quot;C/C++ -&gt; Precompiled headers&quot;</li>    <li>Set to &quot;Use Precompiled Header&quot; through &quot;Stdafx.h&quot;     <br /><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3619c0cd970b-pi"><img style="background-image: none; border-bottom: 0px; border-left: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top: 0px; border-right: 0px; padding-top: 0px" title="UsePrecimpiledHeader" border="0" alt="UsePrecimpiledHeader" src="/assets/image_896473.jpg" width="399" height="280" /></a></li>    <li>Next you need to tell the compiler the source file from which it should create the Precompiled header from, simply navigate to Stdafx.cpp and go to the Project properties, select &quot;C/C++ -&gt; Precompiled headers&quot;</li>    <li>Set &quot;Create Precompiled Header&quot;</li>    <li>Compile</li> </ol>
