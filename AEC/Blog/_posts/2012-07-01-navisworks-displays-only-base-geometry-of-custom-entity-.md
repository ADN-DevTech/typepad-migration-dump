---
layout: "post"
title: "Navisworks displays only base geometry of custom entity "
date: "2012-07-01 20:20:35"
author: "Mikako Harada"
categories:
  - "Mikako Harada"
  - "Navisworks"
original_url: "https://adndevblog.typepad.com/aec/2012/07/navisworks-displays-only-base-geometry-of-custom-entity-.html "
typepad_basename: "navisworks-displays-only-base-geometry-of-custom-entity-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I created an ObjectDBX with a custom entity, which is derived from AcDbCircle.&#0160;I have a DWG which contains my custom entity. When I opened this DWG in Navisworks, it only displays the base geometry, i.e., a circle. The custom geometry is missing.&#0160; What is going on?&#0160;</p>
<p><strong>Solution</strong></p>
<p>In Navisworks, if a custom entity is derived from AutoCAD&#0160;basic geometry, such as AcDbCircle and AcDbLine, Navisworks will identify it as an AutoCAD&#0160;basic entity, Circle or Line, and custom geometry is ignored. If the custom entity is derived from an abstract entity, such as AcDbCurve and AcDbEntity, Navisworks displays your custom geometry.&#0160;</p>
<p>However, if OE is NOT loaded successfully, it is treated as a proxy entity, both basic geometry and&#0160; custom geometry will be displayed. It will also report missing OE in Scene Statistics.</p>
<p>Many thanks to Xiaodong for testing this.</p>
