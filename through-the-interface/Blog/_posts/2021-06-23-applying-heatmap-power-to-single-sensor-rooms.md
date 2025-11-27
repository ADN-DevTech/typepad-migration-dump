---
layout: "post"
title: "Applying heatmap power to single-sensor volumes"
date: "2021-06-23 17:47:34"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2021/06/applying-heatmap-power-to-single-sensor-rooms.html "
typepad_basename: "applying-heatmap-power-to-single-sensor-rooms"
typepad_status: "Publish"
---

<p>I’ve been heads-down on a side project to integrate a C++ library into Forge using Web Assembly (hopefully I’ll share more on this sometime soon). Thankfully while I’ve been busy on that, the Hyperion team has been beavering away on some changes to their core shader.</p><p>Last week I realised – now that we have controls to adjust the confidence and power settings in the shader – that rooms with just one sensor have a uniform shading style. This was a deliberate choice we made, back when we initially implemented the shader, as we felt it would help highlight the value for a particular room. You can see the effect below, where the left-hand rooms change when the power is increased:</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdeda6176200c-pi" target="_blank"><img width="500" height="312" title="Heatmap with uniform single-sensor shading" style="margin: 30px auto; float: none; display: block;" alt="Heatmap with uniform single-sensor shading" src="/assets/image_772193.jpg"></a></p><p>Now that we have control over parameters such as power and confidence, though, it made sense not to mandate this behaviour: if the power is set to around 2 – with a higher confidence for larger models – then the effect is similar to what we had before. It does make sense to allow the data visualization to tail off as you get further from the sensor, though, even in a room with just one sensor.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2027880324a85200d-pi"><img width="500" height="312" title="Heatmap without uniform single-sensor shading" style="margin: 30px auto; float: none; display: block;" alt="Heatmap without uniform single-sensor shading" src="/assets/image_533011.jpg"></a></p><p>We can see how the modified shader – which should be available as part of the v7.46 release of the Forge viewer – now works with single-sensor volumes. The 2D heatmap once again validates the visualization nicely, too.</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2027880324a87200d-pi"><br></a></p>
