---
layout: "post"
title: "Detecting collisions in the Forge viewer"
date: "2022-03-31 07:34:00"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
  - "Robotics"
original_url: "https://www.keanw.com/2022/03/detecting-collisions-in-the-forge-viewer.html "
typepad_basename: "detecting-collisions-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>In a recent post we saw that <a href="https://www.keanw.com/2022/03/switching-to-use-threebuffergeometry-in-forge-viewer-applications.html" rel="noopener" target="_blank">switching to use THREE.BufferGeometry</a> brought some unexpected benefits when it came to rendering robots inside <a href="https://dasher360.com" rel="noopener" target="_blank">Dasher</a>. I wasn’t very happy about the fact that said robots were spinning destructively on the <a href="https://keanw.com/mx3d" rel="noopener" target="_blank">MX3D bridge</a>, so I started looking into options for collision detection inside a Forge viewer application.</p>
<p>From the start I should say that the approach that I ended up choosing is fairly rudimentary in nature: a much better solution would be to integrate a <a href="https://adndevblog.typepad.com/cloud_and_mobile/2015/01/integrating-ammojs-physics-with-autodesk-view-data-api.html" rel="noopener" target="_blank">physics engine such as Ammo.js</a> or perhaps even a <a href="https://www.keanw.com/2021/10/streamlines-in-the-forge-viewer.html" rel="noopener" target="_blank">voxelization engine such as VASA</a>. But I figured that providing this simpler technique might still be of benefit for certain simpler situations.</p>
<p>The basic approach is outlined in <a href="https://stackoverflow.com/questions/11473755/how-to-detect-collision-in-three-js" rel="noopener" target="_blank">this StackOverflow response</a> and <a href="https://stemkoski.github.io/Three.js/Collision-Detection.html" rel="noopener" target="_blank">this GitHub example</a>. The idea is to take the various vertices of your source mesh and then fire rays from the mesh’s position to each vertex: if the rays intersect the scene somewhere closer than the distance from the position to the vertex then there’s a collision happening.</p>
<p>This is fine when you’re dealing with simple geometry – the sample uses cubes, for instance, so we’re really only talking about 8 vertices – but interactive performance is going to be a challenge when dealing with complex meshes such as our robot.</p>
<p>To explore this I decided to start with the robot’s skeleton – the core structure that connects the “bones” of the robot and allows us to manipulate the various limbs independently but connectedly.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202788073ebf3200d-pi" rel="noopener" target="_blank"><img alt="Robot with skeleton" border="0" height="446" src="/assets/image_909191.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Robot with skeleton" width="500" /></a></p>
<p>There’s a <a href="https://threejs.org/docs/#api/en/helpers/SkeletonHelper" rel="noopener" target="_blank">THREE.SkeletonHelper</a> object that allows to query the skeleton’s vertices, which we can simply transform to world coordinates using the helper’s matrixWorld property. The vertex order is the same as those of the skeleton’s bones, but with the start-point and end-point swapped (the first bone is from v[1] to v[0], the second from v[3] to v[2], etc.). At least for the version currently used in the Forge viewer – this may change, in time.</p>
<p>This approach works well, although the core skeleton can sometimes be quite far from the surface of the mesh itself. I went ahead and created a number of vectors that were offset by the approximate bounding size (I didn’t get this quite right, but never mind) around the core skeleton.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e14c9505200b-pi" rel="noopener" target="_blank"><img alt="Robot with more skeleton-derived vectors" border="0" height="477" src="/assets/image_316239.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Robot with more skeleton-derived vectors" width="500" /></a></p>
<p>To check intersections with the scene the Forge viewer has a handy rayIntersect() method (it’s under the impl property, so you need to access it via viewer.impl.rayIntersect()). As the robot itself is displayed via an overlay, it doesn’t participate in the intersection operation (which is a good thing).</p>
<p>We don’t need to check collisions for every bone, or for all the vectors around the central one. In fact I ended up reducing down to just the end bone and one single radial vector, to keep the collision detection operation responsive. If you’re not calling this super-regularly (we’re doing so for every joint transformation, so it’s being called a lot) you can ratchet up the accuracy by including more vectors.</p>
<p>In the below animation the collision detection isn’t perfect, but it’s good enough for our purposes.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e14c950b200b-pi"><img alt="Robot with collision check" height="307" src="/assets/image_247294.jpg" style="margin: 30px auto; float: none; display: block;" title="Robot with collision check" width="500" /></a></p>
<p>One interesting effect is that when we reverse the robot operation we can’t just decrement the counter we’re using to keep track of the robot position: we need to reduce it by a larger amount to avoid this kind of “Max Headroom” stuttering.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202788073ec77200d-pi"><img alt="Stuttering robot" height="290" src="/assets/image_326166.jpg" style="margin: 30px auto; float: none; display: block;" title="Stuttering robot" width="500" /></a></p>
<p>I’m not fully sure why this happens – maybe there’s some lag introduced because we’re applying multiple joint transformations, one after the other – but anyway.</p>
<p>I hope this proves to be useful for people who want to implement some kind of rudimentary client-side collision detection using the Forge viewer. It won’t work for all scenarios, but will hopefully still prove useful for someone.</p>
