---
layout: "post"
title: "Display a custom entity during dragging"
date: "2013-01-10 11:23:41"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/display-a-custom-entity-during-dragging.html "
typepad_basename: "display-a-custom-entity-during-dragging"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>  <p>For a custom entity derived from <strong>AcDbEntity</strong>, when try to use the polyline() and polygon() primitives of <strong>AcGiWorldGeometry</strong> inside the <strong>worldDraw</strong>() function, in some cases the functions may return a bad error status when used while dragging or modifying grip points.</p>  <p>This error does not produce a crash, but when stop dragging the entity while editing grip points, it’s no possible to see the new transformed/modified entity, only the points. This may cause a lot of inconvenience while scaling or rotating entities using grip points.</p>  <p>These functions return an error to indicate that the operation has been aborted. AutoCAD aborts the draw operation if the user continues to drag your entity, based on the ACAD variables <strong>DRAGP1</strong> and <strong>DRAGP2</strong>. To see your entity more detailed during dragging, it’s possible to increase these two variables (you should try to set <strong>DRAGP1</strong> to 100 and <strong>DRAGP2</strong> to 250).</p>  <p>Note: When you can't see your entity when you stop dragging, then several parts of your entity might overlap. Since we use XOR to draw an entity during draggen, two vectors on top of each other are not visible (the first XOR operation displays the vector, and the second XOR operation makes it invisible again).</p>
