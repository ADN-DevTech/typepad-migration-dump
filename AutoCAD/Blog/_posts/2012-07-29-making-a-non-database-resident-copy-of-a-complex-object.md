---
layout: "post"
title: "Making a non database resident copy of a complex object"
date: "2012-07-29 08:54:40"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/making-a-non-database-resident-copy-of-a-complex-object.html "
typepad_basename: "making-a-non-database-resident-copy-of-a-complex-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>In ObjectARX you can do a non database-resident copy of a single object using the clone() or copyFrom() methods. But these two methods do not operate on complex entities such as an AcDbBlockReference with AcDbAttributes. As is explained in the documentation, the clone() method calls the copyFrom() method that does a shallow clone/copy of the object. In other words, it does not copy the owned objects (AcDbAttribute). There is no direct method available to do this in ObjectARX.</p>
<p>The solution is to copy each object individually and to reattach the newly created attributes with the block-reference object. Be aware of a special case where the block-definition of the block has constant attributes. These attributes are not sub-entities of the block-reference entity, instead they are AcDbAttributeDefinitions in the block table record (the block definition).</p>
<p>If you want a database resident copy of such complex objects, do not use the deepClone() method directly: you should call the AcDbDatabase::deepCloneObjects() method, ensuring the AcDbObjectId of the block-reference is in the array argument. As deepCloneObjects() follows the AcDbHardOwnerShipId and AcDbHardPointerId relationships, the AcDbAttributes will be copied as well.</p>
