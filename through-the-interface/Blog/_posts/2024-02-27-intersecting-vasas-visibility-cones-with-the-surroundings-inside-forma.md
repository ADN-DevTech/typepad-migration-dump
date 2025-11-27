---
layout: "post"
title: "Intersecting VASA's visibility cones with the surroundings inside Forma"
date: "2024-02-27 13:40:05"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Forma"
  - "VASA"
original_url: "https://www.keanw.com/2024/02/intersecting-vasas-visibility-cones-with-the-surroundings-inside-forma.html "
typepad_basename: "intersecting-vasas-visibility-cones-with-the-surroundings-inside-forma"
typepad_status: "Publish"
---

<p>This week I’m up in the mountains with my family. It’s my younger children’s winter half-term break, but as I do have a few meetings to attend during the week I decided to only take half days off: snowboarding in the morning and working in the afternoons.</p>
<p>I found some time between meetings to work on the <em>VASA inside Forma</em> prototype. My goal for the week was to have the visibility cones displayed for our proposal building - something we saw in the <a href="https://www.keanw.com/2024/02/forma-hackathon-in-oslo.html" rel="noopener" target="_blank">last</a> <a href="https://www.keanw.com/2024/02/my-vasa-demo-from-this-weeks-forma-hackathon.html" rel="noopener" target="_blank">few</a> <a href="https://www.keanw.com/2024/02/more-work-on-vasa-inside-forma.html" rel="noopener" target="_blank">posts</a> - intersect with the surrounding geometry, whether other buildings or greenery of some kind.</p>
<p><a href="/assets/image_237225.jpg" rel="noopener" target="_blank"><img alt="A visibilty cone" border="0" height="375" src="/assets/image_237225.jpg" style="display: block; margin: 30px auto;" title="A visibilty cone" width="500" /></a></p>
<p>Ultimately the goal is to be able to assess views from a building. Here you can see the voxels that were “hit” by the above visibility cone.</p>
<p><a href="/assets/image_518265.jpg" rel="noopener" target="_blank"><img alt="Where it intersects with the surroundings" border="0" height="375" src="/assets/image_518265.jpg" style="display: block; margin: 30px auto;" title="Where it intersects with the surroundings" width="500" /></a></p>
<p>To make this work I had to make sure the “Intersect” method was exposed via WebAssembly and working properly. When I first started looking at it I was convinced it wasn’t working: whatever I did I couldn’t get an intersection between the visibility cone and the surrounding geometry.</p>
<p>To get passed this, I asked Rhys Goldstein for help. (For those of you who don’t know him, Rhys is the brains behind VASA and probably dreams in voxels. ;-) As I was talking to Rhys, I realized my foolish mistake: the visibility voxels are already clipped by the surrounding geometry, so there’s by definition no intersection! Duh. I suggested creating a “pure” visibility cone that could be intersected with the surroundings - which would have worked but would have returned voxels that weren’t visible, something Rhys referred to as X-ray behaviour. Rhys’s suggestion was to expand the voxels of the surrounding geometry both horizontally and vertically by a small amount and then intersect this expanded voxel set with the visibility cone.</p>
<p>I really liked this approach, as the expansion could be done once on setup, and simply used to intersect the visibility cone at runtime. One other additional benefit is that the start of the cone intersects with the expanded building being analyzed, which means there’s a little stub showing the window that’s being assessed. An unintended consequence, but a welcome one.</p>
<p>Here’s an example of this working for the proposal we saw previously: for now the “intersection” mode is enable by clicking the “Views” text. (I really am starting to master the art of obscure UX anti-patterns. ;-)</p>
<p><a class="asset-img-link" href="/assets/image_270212.jpg" rel="noopener" target="_blank"><img alt="Intersectional" border="0" class="asset  asset-image at-xid-6a00d83452464869e202c8d3ac0f20200b image-full img-responsive" src="/assets/image_270212.jpg" style="display: block; margin: 30px auto; width: 500px;" title="Intersectional" /></a></p>
<p>Here’s the same approach in action for a larger building - one I drew on the Las Vegas Strip - that has more interesting surroundings. This building has interior windows and you can even see the views onto the other side of the same building.<a class="asset-img-link" href="/assets/image_125981.jpg" rel="noopener" target="_blank"><img alt="Intersectional strip" border="0" class="asset  asset-image at-xid-6a00d83452464869e202c8d3ac7008200d image-full img-responsive" src="/assets/image_125981.jpg" style="display: block; margin: 30px auto; width: 500px;" title="Intersectional strip" /></a>It’s also possible to union the intersections for the full building, just as we saw last time.</p>
<p><a href="/assets/image_401586.jpg" rel="noopener" target="_blank"><img alt="Union of intersections" border="0" height="375" src="/assets/image_401586.jpg" style="display: block; margin: 30px auto;" title="Union of intersections" width="500" /></a></p>
<p>All in all this is working quite nicely, and is starting to feel quite useable. What’s really nice about using voxels in this way is that we could easily count the voxels in the visible intersection, and use this number as a numerical evaluation of views, as well. It really lends itself to workflows involving quantification.</p>
