---
layout: "post"
title: "SaveAsType misses types in AutoCAD 2010"
date: "2012-05-14 01:37:15"
author: "Xiaodong Liang"
categories:
  - "2010"
  - "AutoCAD"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/saveastype-misses-some-types-of-2010.html "
typepad_basename: "saveastype-misses-some-types-of-2010"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>  <p>The AcSaveAsType enum missed the formats of 2010: ac2010_dwg, ac2010_dxf , ac2010_Template.</p>  <p>This was fixed in SP1 of AutoCAD 2010. You can download it from    <br /><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=13760520&amp;linkID=9240618">http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=13760520&amp;linkID=9240618</a></p>  <p>Please note: SP1 does not update acax18ENU.tlb in ObjectARX package. So if you were using that one, please make sure to refer to the newest updated by SP1. You can find it under e.g.</p>  <p>&quot;C:\Program Files\Common Files\Autodesk Shared\acax18enu.tlb&quot;</p>  <p>From AutoCAD 2011, the three enum are available.</p>
