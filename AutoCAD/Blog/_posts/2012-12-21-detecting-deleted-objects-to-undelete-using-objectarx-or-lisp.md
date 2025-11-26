---
layout: "post"
title: "Detecting deleted objects to undelete using ObjectARX or LISP?"
date: "2012-12-21 18:06:44"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/detecting-deleted-objects-to-undelete-using-objectarx-or-lisp.html "
typepad_basename: "detecting-deleted-objects-to-undelete-using-objectarx-or-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might wonder, with an entity name, or a selection set of entities, if there is a way in to determine if any entities have been deleted so that they can be undeleted.</p>  <p>In AutoLISP, call entget on each entity name in the selection set. If the entity has been erased since the creation of the set, it will not return any entity data. Then call entdel to undelete the entity.</p>  <p>In ObjectARX, the AcDbObject::erase() method allows you to set or unset the erase bit of an object. You may also call acdbEntDel to undelete the entity.</p>
