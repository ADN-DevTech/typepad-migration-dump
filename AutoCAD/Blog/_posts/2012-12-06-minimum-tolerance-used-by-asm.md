---
layout: "post"
title: "Minimum tolerance used by ASM"
date: "2012-12-06 11:38:48"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/minimum-tolerance-used-by-asm.html "
typepad_basename: "minimum-tolerance-used-by-asm"
typepad_status: "Draft"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/augusto-goncalves.html">Augusto Goncalves</a></p>
<p>ASM uses a tolerance of 1e-6 for its modeling operations. For ASM, coincident points in a body are determined to be within 1e-6, and lines in a closed loop are determined to be collinear if the square of the maximum triangular area between all nodes is less than or equal to the square of 1e-6.</p>
<p>A circle or arc is determined to be degenerate if its radius is less than or equal to 1e-6, or if the difference between it start and end angles is less than or equal to 1e-6. </p>
<p>Ellipses and splines must have the difference between the start and ending parameters larger than 1e-6. It must not be C0 discontinuous at any knot, and it must not be self intersecting if periodic, or only self intersect at its endpoints if it is not periodic. </p>
<p>The minimum sized square that would be accepted for construction of a region would have edges larger than the square root of 1e-6, or greater than 0.001. In general, avoid solid modeling operations at or near the ASM tolerance of 1e-6.</p>
<p>When a solid or region is transformed, the ASM body is not &#39;fixed&#39; at that transform unless a modeling operation is performed. Exploding a solid that has been transformed that is below the ASM tolerance is asking ASM to construct geometry from its edges, which ASM determines are degenerate. Explode the model first, and then scale it.</p>
