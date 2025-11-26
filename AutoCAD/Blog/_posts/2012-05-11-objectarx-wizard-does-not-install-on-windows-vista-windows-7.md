---
layout: "post"
title: "ObjectARX Wizard does not install on Windows Vista / Windows 7"
date: "2012-05-11 02:34:32"
author: "Virupaksha Aithal"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Virupaksha Aithal"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/objectarx-wizard-does-not-install-on-windows-vista-windows-7.html "
typepad_basename: "objectarx-wizard-does-not-install-on-windows-vista-windows-7"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>Windows Vista / Windows 7 includes a much tighter security mechanism for controlling script modules that are run from it. Because the ObjectARX Wizard runs a VBScript to initialise some of its components, Windows Vista or Windows 7 immediately disables the VBScript routines from running and thus aborts the installation.</p>
<p>To solve the problem, you must temporarily disable Windows Vista or Windows 7 UAC (User Account Control).</p>
<p>Once installed, you must re-enable UAC.</p>
