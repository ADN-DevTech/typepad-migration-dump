---
layout: "post"
title: "How to Deploy OEM with SCCM"
date: "2017-02-20 17:55:00"
author: "Madhukar Moogala"
categories:
  - "2017"
  - "AutoCAD OEM"
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/02/how-to-deploy-oem-with-sccm.html "
typepad_basename: "how-to-deploy-oem-with-sccm"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar Moogala</a></p>
<p>Large corporations prefer to deploy custom AutoCAD OEM suites using SCCM, here is preferred way to do, in the configuration manager tool</p>
<p>you need to enter</p>
<p><code>setup.exe /t /q /c AOEM: INSTALLDIR=&quot;C:\Program Files\MyApp&quot; InstallLevel=5
</code></p>
<p>Where MyApp is OEM program name.</p>
<p>From AutoCAD OEM 2023-</p>
<p>We have changed the installer technology, we moved to ODIS which is a On Demand Installer Service, its purpose is to create an installation, deployment, and update experience that&#39;s fast, easy, predictable, and painless for customers.</p>
<p><strong>For Silent Install</strong> -</p>
<p><code>setup.exe --silent --pf abc --sn defghijk</code></p>
<p>Note abc-defghijk is the serial number assigned to your account</p>
<p>The silent mode won’t allow you to change the INSTALLDIR.</p>
