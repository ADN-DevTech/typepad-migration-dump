---
layout: "post"
title: "Deleting the contents of an AcDbVoidPtrArray used for creating hatch"
date: "2013-01-31 04:49:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/deleting-the-contents-of-an-acdbvoidptrarray-used-for-creating-hatch.html "
typepad_basename: "deleting-the-contents-of-an-acdbvoidptrarray-used-for-creating-hatch"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>When I use the AcDbHatch::appendLoop function that takes an AcDbVoidPtrArray of AcGeCurve objects, AutoCAD terminates unexpectedly when the memory is erased. Do I need to delete the elements of the array?</p>
<p><a name="section2"></a></p>
<p><strong>Solution     <br /></strong>The existing documentation does not explicitly mention that once the loop definition array is passed over to AutoCAD, it is then kept internally to store the loop information inside the hatch.</p>
<p>The contents of AcDbVoidPtrArray in this case, should NOT be deleted.</p>
