---
layout: "post"
title: "Loading OMF ARX modules in AutoCAD 2013"
date: "2012-07-04 01:11:01"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/07/loading-omf-arx-modules-in-autocad-2013.html "
typepad_basename: "loading-omf-arx-modules-in-autocad-2013"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p> <p>In AutoCAD Architecture Object Enabler (OE) environment such as a vanilla AutoCAD, AecUIBase.arx is the only Arx module that we could use prior to 2013 release. In 2013 release, AecUIBase.arx has been replaced by AecCore.crx. The API exposed by the these two modules are almost the same, except that class AecAppArx has been replaced by AecAppCrx.</p>  <p>If you have OMF ARX module working in AutoCAD 2012 and you really want the module to be loadable in AutoCAD 2013, you will need to migrate their application classes to use AecAppCrx as base class and link to AecCore.lib instead. Thanks to Tony (Congbai ZOU) in our engineering team for providing this information.</p>  <p>In my blog post for <a href="http://adndevblog.typepad.com/aec/2012/07/migrating-an-omf-sample.html">OMF migration</a>, you can remove AecGuiBase.lib reference in the linker setting in the project and replace all AecAppArx class with AecAppCrx class in the source code, the migration is done for OE environment. When you load the ARX module in AutoCAD, you need to place the resource dll in “en-US” sub folder (if English version) of AutoCAD installation location because AutoCAD always looks for the resource dll in the folder.</p>
