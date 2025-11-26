---
layout: "post"
title: "AcDbDictionary is marked as ePermanentlyErased"
date: "2013-01-21 00:42:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/acdbdictionary-is-marked-as-epermanentlyerased.html "
typepad_basename: "acdbdictionary-is-marked-as-epermanentlyerased"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>I have a drawing containing custom objects that I have saved. When I reopen the drawing and try to access some of the dictionaries within the drawing, I find that they have been marked as ePermanentlyErased. Why?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>This problem usually manifests itself when custom objects do not save themselves correctly. You should review their dwgInFields and dwgOutFields methods to make sure that they are correctly implemented. In addition, when you read/write specific type of data, it is recommended to use the type-specific methods such as writeInt32, readInt32.</p>
