---
layout: "post"
title: "AcDbObject::dwgOutFields can crash on custom object"
date: "2015-07-06 06:00:14"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2015/07/acdbobjectdwgoutfields-can-crash-on-custom-object.html "
typepad_basename: "acdbobjectdwgoutfields-can-crash-on-custom-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves/">Augusto Goncalves</a></p>  <p>When implementing a custom object (derived from AcDbObject or AcDbEntity) the dwgOutFields method is required, otherwise AutoCAD will not save our data. Usually the first step is call the base implementation, but that can crash. Why?</p>  <p>One reason is the safe check implemented to avoid null or erased AcDbDictionary referenced by the object, basically a isNull and isErased check. </p>  <p>Remember to configure and enable <a href="http://adndevblog.typepad.com/autocad/2013/04/announcement-autocad-2014-debug-symbols-now-available-on-the-public-server.html">AutoCAD Debug Symbols</a> on Visual Studio, that will help spot problems on internal calls.</p>
