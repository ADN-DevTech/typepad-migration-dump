---
layout: "post"
title: "Using VASA with Forma - Part 3"
date: "2023-11-01 14:26:00"
author: "Kean Walmsley"
categories:
  - "AU"
  - "Autodesk Research"
  - "Forma"
  - "VASA"
original_url: "https://www.keanw.com/2023/11/using-vasa-with-forma-part-3.html "
typepad_basename: "using-vasa-with-forma-part-3"
typepad_status: "Publish"
---

<p>I was sure this series was finished - as indicated in <a href="https://www.keanw.com/2023/10/using-vasa-with-forma-part-2.html" target="_blank" rel="noopener">the last post which talked about both VASA’s pathfinding and visibility from a point inside Forma</a>, after having talked about <a href="https://www.keanw.com/2023/10/using-vasa-with-forma-part-1.html" target="_blank" rel="noopener">a manual approach for connecting the two tools</a> - but I did leave the door ajar to me finding something else to talk about on this topic.</p>
<p>Sure enough, I got thinking about the missing “big ticket” item that VASA is able to implement with relatively low effort: visibility from a direction (as opposed to from a point) which can simulate things like shadows cast by direct sunlight.</p>
<p>It was indeed quick to implement, and it was a helpful process as it reminded me of one of the key values of voxelisation: you can adjust the resolution of the voxel model to match the performance and quality required. While for pathfinding and visibility from a point we wanted to voxelise our neighbourhood-scale model at quite a granular resolution (with a 1m voxel size), for visibility from a direction with good enough performance to display interactive changes I could adjust the size of the voxels to be much coarser (I think I went with 4m, in the end). It’s a good enough approximation for most purposes - certainly for visualization - and helps keep the UI responsive.</p>
<p>I’ve kept the UI highly streamlined, too: I took a series of sun directions for a specifc date (June 21st) and latitude (I’m actually not sure which one, to be honest, I’d just be skeptical of these shadows being correct for any particular location) and added a slider that allowed the user to switch between them. There’s clearly scope to flesh this out into a more complete feature, but that’s beyond the scope of this prototype.</p>
<p><a class="asset-img-link" href="/assets/image_769579.jpg" target="_blank" rel="noopener"><img class="asset  asset-image at-xid-6a00d83452464869e202c8d39d8427200c img-responsive" style="display: block; margin: 30px auto;" title="VASA shadow casting in Forma" src="/assets/image_769579.jpg" alt="VASA shadow casting in Forma" width="500" border="0" /></a>While this may not seem directly useful for people familiar with Forma - which already has an excellent sun study and shadow analysis capability - this is just one example of how this particular VASA feature might be used to calculate shadows. You could easily aggregate the results from VASA to calculate the shadows cast throughout the year by surrounding buildings on your newly modeled space, or combine these results with pathfinding to understand the most shaded/sunlit - or perhaps the most open/enclosed - path to take between two points. There are some really interesting possibilities presented by this technology.</p>
<p>I’ll be showing the above integration live for people attending our sold-out VASA class at AU 2023. Hoping to see you there!</p>
