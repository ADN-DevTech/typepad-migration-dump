---
layout: "post"
title: "Forge Viewer Consolidated Geometry"
date: "2016-12-13 06:21:05"
author: "Cyrille Fauvel"
categories:
  - "Browser"
  - "Client"
  - "Cyrille Fauvel"
  - "Forge"
  - "Javascript"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/12/forge-viewer-consolidated-geometry.html "
typepad_basename: "forge-viewer-consolidated-geometry"
typepad_status: "Publish"
---

<p>There is an endpoint to know which versions of the viewer are available. Checkout the&#0160;<a href="https://developer.api.autodesk.com/viewingservice/v1/viewers">https://developer.api.autodesk.com/viewingservice/v1/viewers</a>&#0160;endpoint.</p>
<p>it would return a list like the one below – Model Consolidation, which brings performance enhancements is coming with v2.12+</p>
<pre>[
  &quot;2.12.60&quot;,
  &quot;2.12&quot;,
  &quot;2.11.58&quot;,
  &quot;2.11.57&quot;,
  &quot;2.11.55&quot;,
  &quot;2.11&quot;,
  &quot;2.10.58&quot;,
  ...
]</pre>
<p><strong>Performance Enhancement: Model Consolidation</strong></p>
<p>Model Consolidation optimizes a model for faster rendering by using mesh consolidation and hardware instancing.</p>
<p>An optional value can be specified to control the amount of GPU memory spent for mesh merging. Default value is 100MB.</p>
<p>Consolidation requires the full model to be in memory first. I.e., progressive rendering will have the unoptimized speed until loading is finished and the consolidation step is run.</p>
<p>The optimization step is most effective for models where the large shape count is the main bottleneck. E.g. models like the NWD model can be rendered several times faster</p>
<p><u>Example:</u></p>
<p>var initializerOptions = {</p>
<p>&#0160;&#0160; env: &#39;AutodeskProduction&#39;,</p>
<p>&#0160;&#0160; useConsolidation: true,</p>
<p>&#0160;&#0160; consolidationMemoryLimit: 150 * 1024 * 1024 // 150MB - Optional, defaults to 100MB</p>
<p>}</p>
<p>Autodesk.Viewing.Initializer( initializerOptions, function() {</p>
<p>&#0160; // ...</p>
<p>});</p>
<p><strong>Some Limitations</strong></p>
<p># It requires significant extra memory (CPU-side and GPU-side).</p>
<p># Consolidation will be turned off automatically when playing animations or exploding the model.</p>
<p># Consolidation reduces the granularity of the scene, which conflicts with existing techniques like progressive rendering, fine-grained frustum culling, and sorting (e.g., for transparency).</p>
