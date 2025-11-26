---
layout: "post"
title: "subWorldDraw not being called on DWG Open in AutoCAD 2013 for AcDb3dSolid derived entities"
date: "2012-06-26 16:42:41"
author: "Fenton Webb"
categories:
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/subworlddraw-not-being-called-on-dwg-open-in-autocad-2013-for-acdb3dsolid-derived-entities.html "
typepad_basename: "subworlddraw-not-being-called-on-dwg-open-in-autocad-2013-for-acdb3dsolid-derived-entities"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>If you are fairly seasoned ObjectARX developer and one that has created a Custom Entity or more, you will know that in AutoCAD 2012 all AutoCAD entities call the subWorldDraw() (worldDraw) on DWG open. </p>  <p>In AutoCAD 2013, the same is true, except for AcDb3dSolids, which do not. </p>  <p>This is expected behavior for 2013 in that neither subWorldDraw() nor subViewportDraw() should be called during drawing open unless the viewport parameters are somehow changed before the custom entity is drawn (in that case, then subViewportDraw() would be called and subWorldDraw() would not be called)</p>  <p>The reason is that for performance improvements in AutoCAD 2013, we changed all of the ASM based entity types to use the new AcGiDrawStream for their graphics. This is cached to disk and the cached graphics are then used the next time that the drawing is opened. This avoids the need to actually read in the ASM data in order to get the drawing open and displayed on screen. </p>  <p>The cached graphics are both global (i.e. worldDraw) and viewport specific (i.e. viewportDraw). When the drawing is opened and the cached graphics are to be used, the current global and viewport parameters are compared to what is saved in the cache. If they are within tolerance, then the cache is used and the entity's subWorldDraw() and subViewportDraw() are not called. If a certain viewport's parameters are not within tolerance, then subViewportDraw() will be called to generate new graphics for that viewport.</p>  <p><strong>If you want to make AcDb3dSolids call their subWorldDraw() on DWG open, simply set CACHEMAXFILES=0</strong></p>
