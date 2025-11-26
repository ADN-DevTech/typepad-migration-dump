---
layout: "post"
title: "Call acdbModelerStart() before using acgex or AutoCAD may crash"
date: "2013-01-18 17:28:22"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/call-acdbmodelerstart-before-using-acgex-or-autocad-may-crash.html "
typepad_basename: "call-acdbmodelerstart-before-using-acgex-or-autocad-may-crash"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might find that sometimes, when creating 3D Solids such as an AcGeCylinder instance, AutoCAD crashes (at the constructor of these classes).</p>  <p>This problem can be resolved if the modeler dlls are loaded. When an application uses anything in acgex (set of math classes for solid modeling and computations), it must first call acdbModelerStart() to ensure that the modeler dlls are available. It must then call acdbModelerEnd() when it is finished with all calls to acgex.</p>
