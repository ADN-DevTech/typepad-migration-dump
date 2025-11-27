---
layout: "post"
title: "Setting the vertical position of planar heatmaps"
date: "2021-06-09 12:09:07"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2021/06/setting-the-vertical-position-of-planar-heatmaps.html "
typepad_basename: "setting-the-vertical-position-of-planar-heatmaps"
typepad_status: "Publish"
---

<p>In a recent post we talked about <a href="https://www.keanw.com/2021/05/integrating-hyperions-planar-heatmaps-into-dasher.html" target="_blank">the integration of Hyperion’s planar heatmap capability into Dasher</a>. Towards the end of that post, I mentioned that the Hyperion team was looking into exposing some way to place planar heatmaps not only at the minimum and maximum vertical locations in the bounding box, but at levels in-between. The team has delivered this more quickly than I expected, and you’ll be able to try it for yourselves in v7.45 of the Forge viewer.</p><p>While I was adding the sliders needed for the capabilities shown in <a href="https://www.keanw.com/2021/06/integrating-hyperions-object-shading-into-dasher-part-1.html" target="_blank">the last post</a>, I went ahead and added one for planar heatmap placement, too. Here’s how it varies the placement of a planar heatmap inside <a href="https://dasher360.com" target="_blank">Dasher</a> (you’ll have to wait a few days before I can update the site to use v7.45 so you can try for yourselves):</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bded7387c200c-pi" target="_blank"><img width="500" height="312" title="Level setting" style="margin: 30px auto; float: none; display: block;" alt="Level setting" src="/assets/image_821183.jpg"></a></p><p>The placement is specified by the new placementPosition option – a float between 0 and 1 , with 0 being the minimum and 1 the maximum – that complements (and will ultimately replace) the current placePosition option (that can either be ‘min’ or ‘max’).</p><p>This option can only be specified as you’re setting up surface shading via setupSurfaceShading() – not during an updateSurfaceShading() call – but you can hopefully see from the above animation that there isn’t a major performance cost associated with re-initializing surface shading when the option changes.</p>
