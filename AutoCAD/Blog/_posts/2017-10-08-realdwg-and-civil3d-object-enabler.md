---
layout: "post"
title: "RealDWG and Civil3d Object Enabler"
date: "2017-10-08 20:42:00"
author: "Madhukar Moogala"
categories:
  - "Madhukar Moogala"
original_url: "https://adndevblog.typepad.com/autocad/2017/10/realdwg-and-civil3d-object-enabler.html "
typepad_basename: "realdwg-and-civil3d-object-enabler"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p>If you are planning to extend your realDWG app to support C3D drawings, then it is must for your app to have access to Civil3D dbx modules to avoid proxy objects.</p><p>The installer of Civil C3D OE 2018 was changed, it’s independent any other product now. Different with OE 2017.<p>From 2018 onwards OE installs files and registries to fixed location.<p>File: C:\Program Files\Autodesk\Autodesk AutoCAD Civil 3D 2018.1 Object Enabler 64 Bit<p>Registry: HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\ObjectDBX\R22.0<p>RealDWG&nbsp; always loading .dbx from above registry key. <p>In theory, OE 2018 is available for all RealDWG application (with consistent version), We only verify the official target products (AutoCAD and family, InfraWorks, Navisworks Manager and simulation).<h3>How to deploy Civil3d Object Enabler with our application ?</h3><p>Civil3d Object Enabler is a free application, your free to <a href="https://knowledge.autodesk.com/support/autocad-civil-3d/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/Civil3D-ObjectEnabler/files/GUID-66260884-6E17-45D3-8B75-F19F6603287D-htm.html">redistribute</a> along with your application.</p><blockquote><h4>How do I distribute the AutoCAD Civil 3D Object Enabler install?</h4><p>The AutoCAD Civil 3D Object Enabler is free, and can be distributed to anyone who wants to view the civil objects in your drawings. However, the AutoCAD Civil 3D Object Enabler alone cannot display the objects in your drawings. You must have an installed copy of one of the Autodesk products listed in <a href="https://knowledge.autodesk.com/support/autocad-civil-3d/learn-explore/caas/CloudHelp/cloudhelp/2017/ENU/Civil3D-ObjectEnabler/files/GUID-2FA1A44F-4DC0-4B23-BC4F-AE32A44698EC-htm.html">Using the AutoCAD Civil 3D Object Enabler</a>.</p></blockquote><p>One approach is to extract Civil3DOE.MSI from Civil3D_2018_OE_64Bit.EXE which you have downloaded from <a href="https://knowledge.autodesk.com/search-result/caas/downloads/content/autocad-civil-3d-2018-object-enabler.html">web</a>. You can use ‘<a href="http://wixtoolset.org/documentation/manual/v3/overview/alltools.html">dark’</a> tool from Wix toolset for extraction. And wrap the extracted MSI in to your application.</p><p><br></p>
<pre>C:\Program Files (x86)\WiX Toolset v3.11\bin&gt;dark D:\Crap\Civil3D_2018_OE_64Bit.exe -x D:\Crap\Civil3D_2018_OE_64Bit
</pre>
