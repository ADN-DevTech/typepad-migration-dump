---
layout: "post"
title: "More work on VASA inside Forma"
date: "2024-02-22 19:16:10"
author: "Kean Walmsley"
categories:
  - "Autodesk"
  - "Autodesk Research"
  - "Forma"
  - "VASA"
  - "Web/Tech"
original_url: "https://www.keanw.com/2024/02/more-work-on-vasa-inside-forma.html "
typepad_basename: "more-work-on-vasa-inside-forma"
typepad_status: "Publish"
---

<p>This week I’ve been continuing some of the work I did during the recent <a href="https://www.keanw.com/2024/02/forma-hackathon-in-oslo.html" rel="noopener" target="_blank">Forma Hackathon in Oslo</a>.</p>
<p>Part of this was using the Forma Elements API to extract information about buildings included in the “proposal” being developed. For the Hackathon I’d hardcoded the size of the buildings - the number of floors and the heights of each floor - but the Forma team (thanks, Vilde!) has been helping me figure out how to get this information from Forma. In a nutshell, it’s possible to get the grossFloorAreaPolygons for the building of interest and then analyze the associated data: the elevations of each floor, the number of floors and even the polygons representing the each floor’s outline.</p>
<p>I’d originally used VASA to calculate sample points for the building - that we could use to approximate window locations - but I found that using the polygons for each floor would allow us to get regularly spaced windows and more easily determine which side we would be looking out from. (I used a simple cross product of the direction we follow the polygonal loop with the up vector to get the view direction.)</p>
<p>All in all a much more consistent experience than the one&nbsp;<a href="https://www.keanw.com/2024/02/my-vasa-demo-from-this-weeks-forma-hackathon.html" rel="noopener" target="_blank">I posted last week</a>. Here you can see the way the cones are projected outwards in a much more predictable way.<a class="asset-img-link" href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202c8d3a7d9e4200c-pi"></a></p>
<p><a class="asset-img-link" href="/assets/image_968464.jpg" rel="noopener" target="_blank"><img class="asset  asset-image at-xid-6a00d83452464869e202c8d3ac1503200b image-full img-responsive" alt="View cones" title="View cones" src="/assets/image_968464.jpg" border="0" style="display: block; margin: 30px auto; width: 500px;" /></a></p>
<p>Another control I decided to add was for the angle of the cone. Here you can see how that slider works alongside the distance slider to give more control on that front.</p>
<p><a class="asset-img-link" href="/assets/image_730669.jpg" rel="noopener" target="_blank"><img alt="Angle and distance" border="0" class="asset  asset-image at-xid-6a00d83452464869e202c8d3ac0cb9200d image-full img-responsive" src="/assets/image_730669.jpg" style="display: block; margin: 30px auto; width: 500px;" title="Angle and distance" /></a></p>
<p>One of my initial goals at the Hackathon was to aggregate the results of performing “visibility with a cone” operations from each of the windows to show a single mesh with the visibility results merged.</p>
<p>This was actually a non-trivial problem, mainly because I needed to expose a “Union” function in VASA that creates a single VoxelModel from a list of them. Emscripten - which we’re using to build the WebAssembly package from the C++ codebase - is very picky about passing objects as pointers. I ended up having to build in a local lambda function to do some custom marshaling from a vector of objects to a vector of pointers. Tricky stuff, but it seems to be working well.</p>
<p>Here’s a view showing the unioned visibility cones for the whole building at the same time. It’s a little slow to generate (it takes about 5 seconds or so), but it’s pretty.</p>
<p><a href="/assets/image_611263.jpg" rel="noopener" target="_blank"><img alt="The Union of VASA" border="0" height="375" src="/assets/image_611263.jpg" style="display: block; margin: 30px auto;" title="The Union of VASA" width="500" /></a>Speaking of pretty, here’s a view that was generated somewhere along the way. I’m really enjoying working with Forma as a visualization platform!</p>
<p><a href="/assets/image_502479.jpg" rel="noopener" target="_blank"><img alt="An interesting effect" border="0" height="375" src="/assets/image_502479.jpg" style="display: block; margin: 30px auto;" title="An interesting effect" width="500" /></a></p>
<p>That’s it for now. See you next time.</p>
