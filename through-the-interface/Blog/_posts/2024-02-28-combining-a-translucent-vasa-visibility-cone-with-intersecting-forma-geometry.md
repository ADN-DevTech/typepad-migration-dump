---
layout: "post"
title: "Combining a translucent VASA visibility cone with intersecting Forma geometry"
date: "2024-02-28 22:29:59"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Forma"
  - "VASA"
original_url: "https://www.keanw.com/2024/02/combining-a-translucent-vasa-visibility-cone-with-intersecting-forma-geometry.html "
typepad_basename: "combining-a-translucent-vasa-visibility-cone-with-intersecting-forma-geometry"
typepad_status: "Publish"
---

<p>A very quick update on my holiday “fun”. After a bit of work I managed to combine two VASA voxel models - one for the visibility cone and one for its intersection with Forma’s site surroundings, <a href="https://www.keanw.com/2024/02/intersecting-vasas-visibility-cones-with-the-surroundings-inside-forma.html" target="_blank" rel="noopener">something we saw last time</a> - into a single Forma mesh.</p>
<p><a href="/assets/image_847723.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Combined visibility cone and geometry intersection.jpg" src="/assets/image_847723.jpg" alt="Combined visibility cone and geometry intersection." width="500" height="375" border="0" /></a></p>
<p>You can hopefully see that the performance is very decent, and the translucent, fog-like effect of the cone to be a reasonable visual complement to the intersection area.<a class="asset-img-link" href="/assets/image_835612.jpg" target="_blank" rel="noopener"><img class="asset  asset-image at-xid-6a00d83452464869e202c8d3ac34ec200b image-full img-responsive" style="display: block; margin: 30px auto; width: 500px;" title="Shadow cones" src="/assets/image_835612.jpg" alt="Shadow cones" border="0" /></a></p>
<p>I ended up going through the extra effort to combine them both into a single mesh - putting the vertices together as well as their respective colours - to keep the performance snappy: I'd tried pumping the two meshes through Forma's rendering API separately, to start with, which led to a modest lag as the two meshes were drawn in sequence.</p>
<p>Here's another example of the extension visualizing visibility in a model of Las Vegas.</p>
<p><a class="asset-img-link" href="/assets/image_102880.jpg" target="_blank" rel="noopener"><img class="asset  asset-image at-xid-6a00d83452464869e202c8d3a8672b200c image-full img-responsive" style="display: block; margin: 30px auto; width: 500px;" title="Shadow cones in Vegas" src="/assets/image_102880.jpg" alt="Shadow cones in Vegas" border="0" /></a></p>
