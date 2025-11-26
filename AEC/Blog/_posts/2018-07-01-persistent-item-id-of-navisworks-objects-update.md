---
layout: "post"
title: "Persistent item ID of Navisworks objects - Update"
date: "2018-07-01 20:17:57"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2018/07/persistent-item-id-of-navisworks-objects-update.html "
typepad_basename: "persistent-item-id-of-navisworks-objects-update"
typepad_status: "Publish"
---

<p>By <a href="https://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>This is an update on&#0160;<a href="https://adndevblog.typepad.com/aec/2012/06/persistent-item-id-of-navisworks-objects.html">Persistent item ID of Navisworks objects</a>:</p>
<p>What IDs are available depend on the file format you’re loading into Navisworks. Some file formats don’t have any per objects ids, some have ids but they aren’t stable (edit the file in some unrelated way and the ids change). Most of these ids are only unique within a file. Whatever ids are there show up as object properties which are different depending on format – Entity Handle for Autocad, Element Id for Revit, etc.</p>
<p>In addition Navisworks has support for a per object GUID which is a stable, globally unique id for an object: <strong>ModelItem.InstanceGuid</strong>. GUIDs exist for file formats which directly support GUIDs (e.g. IFC) and for some file formats where we can generate stable, globally unique ids based on the file format specific ids. This includes AutoCAD and Revit but not Catia, PDMS or Integraph. So, for such file formats:&#0160; you&#0160;would have to rely on custom properties that the creator of the design file has setup.</p>
<p>The below is a demo of IFC file:</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad378740b200c-pi" style="display: inline;"><img alt="Screen Shot 2018-11-07 at 10.49.26 AM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad378740b200c image-full img-responsive" src="/assets/image_512826.jpg" title="Screen Shot 2018-11-07 at 10.49.26 AM" /></a></p>
