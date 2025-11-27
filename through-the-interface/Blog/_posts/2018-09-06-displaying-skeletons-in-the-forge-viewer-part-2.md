---
layout: "post"
title: "Displaying skeletons in the Forge viewer &ndash; Part 2"
date: "2018-09-06 09:22:00"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
  - "MX3D"
original_url: "https://www.keanw.com/2018/09/displaying-skeletons-in-the-forge-viewer-part-2.html "
typepad_basename: "displaying-skeletons-in-the-forge-viewer-part-2"
typepad_status: "Publish"
---

<p>We <a href="http://keanw.com/2018/08/forge-skeletons-and-disappearing-signs.html" rel="noopener noreferrer" target="_blank">introduced the series</a>, then looked at <a href="http://keanw.com/2018/08/adding-3d-geometry-to-a-scene-in-the-forge-viewer.html" rel="noopener noreferrer" target="_blank">adding simple geometry</a> to the Forge viewer, followed by <a href="http://keanw.com/2018/09/displaying-skeletons-in-the-forge-viewer-part-1.html" rel="noopener noreferrer" target="_blank">animated skeletons</a>. Now it’s time to look at approaches for making these skeletons more visible.</p>
<p>As mentioned last time, today’s post is a bit of a “non-post”: it talks about adding a SkinnedMesh to be animated alongside an underlying skeleton, which we already know doesn’t currently work inside the Forge viewer. But it’s instructive to see the process in case either the situation changes or someone wants the code for a pure three.js application.</p>
<p>Basically it’s possible to add a SkinnedMesh into a render overlay or into the main scene, it just doesn’t get animated along with the skeleton itself.</p>
<p>Here’s a screenshot:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3686f7a200c-pi" rel="noopener noreferrer" target="_blank"><img alt="Meshed skeletons" border="0" height="336" src="/assets/image_479118.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Meshed skeletons" width="500" /></a></p>
<p>Here’s a video that gives a better sense of what I mean. Look closely to see the skeleton moving but the mesh staying still.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3686f87200c-pi"><img alt="Skeletons walking without their meshes" height="400" src="/assets/image_781754.jpg" style="margin: 30px auto; float: none; display: block;" title="Skeletons walking without their meshes" width="500" /></a></p>
<p>The issue is buried in the guts of the Forge viewer: basically the viewer has its own render loop where it chooses the objects it updates and displays. This is probably for performance (or just historical) reasons, but that’s just a guess on my part. Either way, the code that updates SkinnedMeshes is not included in the custom render function used by the Forge viewer. If you want to take a look at how it should work, check <a href="https://jsfiddle.net/90yt4pen/" rel="noopener noreferrer" target="_blank">this Fiddle</a>. It includes the Forge viewer code and the most recent build of three.js to show how both work (the newer three.js is overriding the Forge viewer’s implementation, if you remove the three.js reference you’ll see how Forge’s three.js tries to animate them). There seems to be a bit of strangeness in three.js r71, but that’s not completely surprising, given its age. It does raise the issue that enabling the update of SkinnedMesh objects in the viewer may not be the only thing that’s needed to get them work properly, though.</p>
<p>Here’s the overall code to add SkinnedMeshes that don’t get animated by the Forge viewer:</p>
<p>
<script src="https://gist.github.com/KeanW/648287b08589cbeae1f02fc02ba24141.js"></script>
</p>
<p>In the next post we’re going to forget all about SkinnedMeshes and look at what we can do to thicken the display of the graphics displayed by the SkeletonHelper object.</p>
