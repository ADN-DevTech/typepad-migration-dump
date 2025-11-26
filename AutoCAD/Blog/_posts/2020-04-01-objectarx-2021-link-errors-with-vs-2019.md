---
layout: "post"
title: "ObjectARX 2021: Link Errors With VS 2019"
date: "2020-04-01 23:59:00"
author: "Madhukar Moogala"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "RealDWG"
original_url: "https://adndevblog.typepad.com/autocad/2020/04/objectarx-2021-link-errors-with-vs-2019.html "
typepad_basename: "objectarx-2021-link-errors-with-vs-2019"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Madhukar-Moogala.html" target="_self">Madhukar 
Moogala</a></p><p><br></p><p>Q: <strong>We are getting link error compiling code for AutoCAD 2021 with Visual Studio 2019 and ObjectARX SDK 2021</strong>.</p><p>A. The AutoCAD code for 3D modelling entities namely AcDbSubDMesh, AcDb3dSolid, AcDbRegion, AcDbAsmBody, AcDbShape etc are now ported to new library AcGeomEnt.lib</p><p>The declaration for almost all 3D entities are moved <strong>AcGeomEnt.lib</strong>, you need to link your source code with <strong>AcGeomEnt.lib</strong> which is present in &lt;SDK&gt;\lib-x64\</p><p>And, you may also need to <em>appload</em>&nbsp; the <strong>AcGeomentObj.dbx </strong>in case if you are reading a drawing with 3D entities in side databases.</p>
