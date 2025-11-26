---
layout: "post"
title: "Drawing AcDbWipeout in custom entity worldDraw"
date: "2012-10-04 03:06:41"
author: "Virupaksha Aithal"
categories: []
original_url: "https://adndevblog.typepad.com/autocad/2012/10/drawing-acdbwipeout-in-custom-entity-worlddraw.html "
typepad_basename: "drawing-acdbwipeout-in-custom-entity-worlddraw"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Virupaksha-Aithal.html" target="_self">Virupaksha Aithal</a></p>
<p>As “AcDbWipeout” uses “AcGiViewportGeometry::rasterImageDc” to draw itself, it is not possible to draw an in memory “AcDbWipeout”. The workaround for this limitation is to make “AcDbWipeout” object a database resident (like adding to an Anonymous block) and using the database resident “AcDbWipeout” entity to draw inside the custom entity.&nbsp; Refer attached demo <a href="http://adndevblog.typepad.com/files/cuswipeout-1.zip">sample</a></p>
