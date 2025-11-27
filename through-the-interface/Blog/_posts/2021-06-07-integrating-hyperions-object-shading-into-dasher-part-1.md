---
layout: "post"
title: "Integrating Hyperion&rsquo;s object shading into Dasher &ndash; Part 1"
date: "2021-06-07 17:23:57"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "AU"
  - "Autodesk Research"
  - "IoT"
  - "MX3D"
original_url: "https://www.keanw.com/2021/06/integrating-hyperions-object-shading-into-dasher-part-1.html "
typepad_basename: "integrating-hyperions-object-shading-into-dasher-part-1"
typepad_status: "Publish"
---

<p>Over the next few posts – in this series, anyway – we’re going to take a look at the shading of objects (actually meshes) using the <a href="https://www.keanw.com/2021/05/its-officially-available-data-visualization-extension-for-the-forge-viewer.html" rel="noopener" target="_blank">Forge viewer’s Data Visualization Extension</a> (Project Hyperion). This is something we’ve done in Dasher for some time, and I was excited that using Hyperion would once again allow us not only to rip out some of our old code but also to go in new directions and explore interesting new capabilities.</p>
<p>Let’s first explain how this type of shading differs from what we’ve seen in previous posts, namely <a href="https://www.keanw.com/2021/05/integrating-hyperions-volumetric-heatmaps-into-dasher.html" rel="noopener" target="_blank">volumetric</a> <a href="https://www.keanw.com/2021/05/integrating-hyperions-volumetric-room-heatmaps-into-dasher-part-2.html" rel="noopener" target="_blank">room</a> and <a href="https://www.keanw.com/2021/05/integrating-hyperions-planar-heatmaps-into-dasher.html" rel="noopener" target="_blank">planar</a> shading: simply put, this style looks at shading the surface of an object rather than a room. It’s also volumetric, but the effect can be applied to more uneven geometry. In fact the same low-level shader is used for both the volumetric approaches. Here’s <a href="https://forge.autodesk.com/en/docs/dataviz/v1/developers_guide/examples/heatmap_for_arbitrary_models/" rel="noopener" target="_blank">the Hyperion documentation on this topic</a>.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e10751f9200b-pi" rel="noopener" target="_blank"><img alt="Shaded MX3D bridge" border="0" height="312" src="/assets/image_831598.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Shaded MX3D bridge" width="500" /></a></p>
<p>The main model we’ve used in the past to test out this mechanism is the one for the <a href="https://www.keanw.com/2018/10/adding-smarts-to-the-mx3d-bridge.html" rel="noopener" target="_blank">MX3D bridge</a> (which I’m happy to say will be installed in about a month’s time, something you’ll be hearing more about in due course). An interesting side-note relates to units: as the bridge was modelled in millimetres, the coordinate space is much larger than a typical building model; something that has helped us to make sure larger scale models are supported by both Dasher and Hyperion.</p>
<p>What changes were needed to support larger models? Actually nothing very complicated: we mainly needed to be able to specify a larger “confidence” value for the display of sensor data. This confidence setting dictates how far data will be shown from the sensor location (in world coordinate space) before it starts to drop off. With the NEST building we typically use a value of 60, while for the MX3D bridge we use a value of 5000. (As an internal detail, we needed to increase the initial value of a shader variable to be much higher so that both larger and smaller models would work with the same logic. This has been fixed from v7.45 of the Forge viewer.)</p>
<p>The other setting we used for the MX3D bridge, was to increase the “power parameter” from 2 to 3. If you refer back to <a href="https://en.wikipedia.org/wiki/Inverse_distance_weighting" rel="noopener" target="_blank">Shepard’s method – inverse distance weighting – on Wikipedia</a>, you can see that as you increase the power parameter, the visualization tends towards being a <a href="https://en.wikipedia.org/wiki/Voronoi_diagram" rel="noopener" target="_blank">Voronoi</a> partition. We found that going beyond 2 worked better for the bridge model: this was previously hard-coded in the shader for “non-building” models, which was less than ideal.</p>
<p>The Hyperion team (thanks, Ben!) took this feedback and used it to modify the core shader to expose variables (as uniforms, in shader-speak) for confidence and the power parameter, but also for the alpha value that gets applied to the shading (allowing the coloured overlay to be more or less transparent). So not only can we now vary the confidence and power parameter for different models, we can allow the user to change these settings in a much more dynamic fashion, such as via sliders in Dasher’s UI.</p>
<p>Let’s take a spin with these new controls in Dasher’s settings, to see how they affect their respective parameters. First, here’s confidence:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bded702ab200c-pi" rel="noopener" target="_blank"><img alt="It affects your confidence" height="312" src="/assets/image_99573.jpg" style="margin: 30px auto; float: none; display: block;" title="It affects your confidence" width="500" /></a></p>
<p>This is the power parameter – you can see the Voronoi pattern emerge as the power increases:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bded702c2200c-pi" rel="noopener" target="_blank"><img alt="I have the power" height="312" src="/assets/image_507906.jpg" style="margin: 30px auto; float: none; display: block;" title="I have the power" width="500" /></a></p>
<p>And here’s the alpha value, which affects the blending between the heatmap and the underlying model.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bded7030d200c-pi" rel="noopener" target="_blank"><img alt="You&#39;re an alpha" height="312" src="/assets/image_722617.jpg" style="margin: 30px auto; float: none; display: block;" title="You&#39;re an alpha" width="500" /></a></p>
<p>All of this goodness – at least the underlying implementation provided by Hyperion – is heading your way in v7.45 of the Forge viewer, which should be available in the coming days (and in <a href="https://dasher360.com" rel="noopener" target="_blank">Dasher</a> shortly afterwards).</p>
<p>In terms of the steps needed to implement per-object shading, here’s a summary of the process, which should mirror what is explained in the documentation:</p>
<ol>
<li>Create a new SurfaceShadingGroup with a unique name.</li>
<li>Create a main SurfaceShadingNode for the root dbId of what you want to shade.</li>
<li>For each of your sensors, add a SurfaceShadingPoint to the main node.</li>
<li>Add the main node as a child of the shading group.</li>
<li>Create a new SurfaceShadingData object.</li>
<li>Add the shading group as a child of the shading data.</li>
<li>Initialize the fragment Ids in the shading data structure.</li>
<ul>
<li>I found this still needs to be done manually for this style of shading, but I need to double-check as I may be missing something.</li>
</ul>
<li>Call setupSurfaceShading() with our shading data.</li>
<li>Register the colour range for our various sensor types using registerSurfaceShadingColors().</li>
<li>Call renderSurfaceShading() with the name we gave to our shading group back at the beginning.</li>
</ol>
<p>As we were working through this implementation, I realised that getSensorValue() was being called once per fragment – which is inefficient for models that have multiple fragments per dbId. The Hyperion has fixed this from v7.44, I believe.</p>
<p>Now for some less-than-great news… <a href="https://www.keanw.com/2021/04/my-au-2021-class-proposal-on-integrating-hyperion-with-dasher.html" rel="noopener" target="_blank">my AU 2021 class proposal</a> didn’t make the cut, this year: there were very few slots for Forge-related content, it seems, with the priority understandably going to customer stories. I’ll be working with our Developer Advocacy &amp; Support team to see whether there’ll be another way to get Autodesk-sourced Forge material out during the AU timeframe, but it may well be that this blog series is the best (or perhaps only) way for you to access it. So it goes. If you feel you’d like to hear me talk about this at another event, do get in touch. (I’m also available for birthdays, weddings &amp; bar mitzvahs. ;-)</p>
<p>In the next part of this series, we’re going to take a look at how object-level shading can be used to shade objects such as sensors in a building model.</p>
