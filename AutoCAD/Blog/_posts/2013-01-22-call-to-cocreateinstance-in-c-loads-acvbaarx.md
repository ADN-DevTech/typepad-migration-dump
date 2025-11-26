---
layout: "post"
title: "Call to CoCreateInstance (in C++) loads AcVBA.arx"
date: "2013-01-22 10:14:13"
author: "Augusto Goncalves"
categories:
  - "ActiveX"
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/call-to-cocreateinstance-in-c-loads-acvbaarx.html "
typepad_basename: "call-to-cocreateinstance-in-c-loads-acvbaarx"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>This is as designed. COM DLLs made in VB query for the IMSOComponentManager service on the STA message filter and this query is satisfied by loading VBA. </p> For instance: an ARX module that calls CoCreateInstance to instantiate VB-based COM class, when the code was executed, AutoCAD will try to initialize its VBA sub system. On this scenario, AutoCAD fails to initalize its VBA sub-system and the exception it thrown killing the CoCreateInstance function call. In fact, the system is trying to locate &quot;acvba.arx&quot; and eventually threw an ole error if it was not found.  <br />  <p>So currently, the only work around available is *not* to use VB to build components if we do not want this dependency.</p>
