---
layout: "post"
title: "Using the Forge viewer? It&rsquo;s time to switch to SVF2!"
date: "2021-08-03 19:55:38"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk"
  - "Autodesk Research"
  - "IoT"
original_url: "https://www.keanw.com/2021/08/using-the-forge-viewer-its-time-to-switch-to-svf2.html "
typepad_basename: "using-the-forge-viewer-its-time-to-switch-to-svf2"
typepad_status: "Publish"
---

<p>The big news on the Forge side of things over the last few weeks is probably <a href="https://forge.autodesk.com/blog/update-svf2-ga-new-streaming-web-format-forge-viewer-now-production-ready" target="_blank">the official release of SVF2</a>. This is a more efficient streaming format the Forge viewer (from v7.25 onwards) can consume. We’ve been using SVF2 (which was previously known as <a href="https://www.keanw.com/2019/06/this-years-forge-accelerator-in-barcelona.html" target="_blank">OTG</a>) for <a href="https://www.keanw.com/2019/09/a-major-update-to-dasher-360-now-showing-the-nest-building.html" target="_blank">the last couple of years</a> inside <a href="https://dasher360.com" target="_blank">Project Dasher</a>, and it’s been brilliant. If you’re interested in the technical details on SVF2, be sure to check <a href="https://forge.autodesk.com/blog/svf2-public-beta-new-optimized-viewer-format" target="_blank">the blog post that announced the SVF2 Beta</a> from late last year.</p><p>Just for instance, we’ve seen significant performance improvements from using SVF2 for <a href="https://autode.sk/dasher-nest" target="_blank">our public demo using the NEST building</a>:</p><p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdee553cb200c-pi" target="_blank"><img width="500" height="312" title="The NEST building using SVF2" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" alt="The NEST building using SVF2" src="/assets/image_971685.jpg" border="0"></a></p><p>In a nutshell, SVF2 uses instancing to optimize the use and storage of meshes within and across viewables. For instance, a typical building model contains a whole bunch of cuboids: SVF2 reduces many of these to a single mesh with varying transformations (these can, of course, represent rotations and translations but also – very importantly – non-uniform scaling). In any case the reduction in memory usage should speak for itself, and be enough to convince developers who haven’t already switched across to do so.</p><p>I’ll leave it there: do check the above-linked posts for more information on how to switch across to using this new format. (This should be really easy; the only current issue may be if you’re relying on models with physical materials coming from 3ds Max, something that’s expected to work in an update in the near future.)</p>
