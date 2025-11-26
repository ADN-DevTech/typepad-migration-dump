---
layout: "post"
title: "Persistent item ID of Navisworks objects"
date: "2012-06-18 08:00:00"
author: "Saikat Bhattacharya"
categories:
  - ".NET"
  - "COM"
  - "Navisworks"
  - "Saikat Bhattacharya"
original_url: "https://adndevblog.typepad.com/aec/2012/06/persistent-item-id-of-navisworks-objects.html "
typepad_basename: "persistent-item-id-of-navisworks-objects"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/saikat-bhattacharya.html" target="_self">Saikat Bhattacharya</a></p>
<p>Is there any way to identify a node or a path that is persistent across model changes and sessions?&#0160;</p>
<p>If you need stable, unique IDs on elements, the best bet is to use some attribute. If you are working with DWG files, you can use the Entity Handle attribute. If you are using MicroStation v8 files, you can use the Element ID. In an ArchiCAD model, you use GUIDs etc.</p>
