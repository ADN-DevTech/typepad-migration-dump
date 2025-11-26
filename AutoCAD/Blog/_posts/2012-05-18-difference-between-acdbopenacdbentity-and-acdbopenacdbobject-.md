---
layout: "post"
title: "Difference between acdbOpenAcDbEntity and acdbOpenAcDbObject "
date: "2012-05-18 18:28:24"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/difference-between-acdbopenacdbentity-and-acdbopenacdbobject-.html "
typepad_basename: "difference-between-acdbopenacdbentity-and-acdbopenacdbobject-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>acdbOpenAcDbEntity() provides a means to open database resident objects that are derived from AcDbEntity.</p>
<p>acdbOpenAcDbObject() provides a means to open database resident objects that are NOT derived from AcDbEntity, ones derived from AcDbObject.</p>
<p>However, you can still use acdbOpenAcDbObject() to open an AcDbEntity sub-class object provided that you do the (AcDbObject*&amp;) cast. But YOU CANNOT USE acdbOpenAcDbEntity to open an AcDbObject level object.</p>
<p>ObjectARX also has acdbOpenObject(), this function implements further function definitions, among which are a template function definition which means that you do not need to cast the object if you use this function. It is available for all classes using the ACRX_DECLARE_MEMBERS macro either directly or indirectly through other macros.</p>
