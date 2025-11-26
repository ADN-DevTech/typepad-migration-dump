---
layout: "post"
title: "Extracting the raw Geometry from Plant3d Objects"
date: "2013-07-18 11:24:00"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "Fenton Webb"
  - "ObjectARX"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2013/07/extracting-the-raw-geometry-from-plant3d-objects.html "
typepad_basename: "extracting-the-raw-geometry-from-plant3d-objects"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are creating some sort of converter tool between AutoCAD Plant3d and some other product you are more than likely going to need to gain access to the raw geometry that makes up Plant3d objects, and, in addition you will also need to grab the property names and values of those objects.</p>  <p>There are 2 ways to obtain the 3dSolid information from Plant3d objects…</p>  <ol>   <li>The first way is to explode() a Plant3d object until you receive Solid3d objects. Once you have the Solid3d objects, you can then use the BREP API to extract the boundary representation of the solids. This option is the quickest to implement, however, the interpretation of the geometry is going to be tricky (at least for me it is).      <br /></li>    <li>The second way is to create custom WorldDraw and ViewportDraw classes (in ObjectARX they are called AcGiWorldDraw and AcgiViewprtDraw). By doing this, you can essentially override the low level draw routines that AutoCAD uses and replace them with your own functions which can be used to work out the geometrical data that is being drawn. A good example of how this works can be found in the ObjectARX sample called acgisamp. Check out this post on <a href="http://adndevblog.typepad.com/autocad/2013/07/how-acdb3dsolids-are-drawn-when-using-a-custom-derived-acgiworlddraw.html">How AcDb3dSolids are drawn when using a custom derived AcGiWorldDraw</a>. This option is going to be much more work to implement, but once you have it implemented then extracting the geometrical shapes will be much easier.</li> </ol>  <p>Then, you are of course going to need to match the geometry properties of the solid with the actual geometry of the part… To do this, you can either use the DataLinksManager or the Part/SubPart.PartSizeProperties()</p>
