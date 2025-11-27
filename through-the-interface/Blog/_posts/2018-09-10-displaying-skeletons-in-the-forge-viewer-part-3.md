---
layout: "post"
title: "Displaying skeletons in the Forge viewer &ndash; Part 3"
date: "2018-09-10 08:40:00"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
  - "MX3D"
original_url: "https://www.keanw.com/2018/09/displaying-skeletons-in-the-forge-viewer-part-3.html "
typepad_basename: "displaying-skeletons-in-the-forge-viewer-part-3"
typepad_status: "Publish"
---

<p>Right then… now it’s time for the really fun stuff. Looking back over this series of posts, we <a href="http://keanw.com/2018/08/forge-skeletons-and-disappearing-signs.html">introduced it</a> then looked at <a href="http://keanw.com/2018/08/adding-3d-geometry-to-a-scene-in-the-forge-viewer.html">adding simple geometry</a> to the Forge viewer, followed by <a href="http://keanw.com/2018/09/displaying-skeletons-in-the-forge-viewer-part-1.html">animated skeletons</a> and <a href="http://keanw.com/2018/09/displaying-skeletons-in-the-forge-viewer-part-2.html" rel="noopener noreferrer" target="_blank">animated skeletons with fixed meshes attached</a>.</p>
<p>Today we’ll dig into making our animated skeletons properly visible. Having given up on using a SkinnedMesh, the remaining option was to tweak the underlying display of the SkeletonHelper object. This proved challenging for all sorts of reasons. Back in Chrome 55, it seems <a href="https://github.com/mrdoob/three.js/issues/10357" rel="noopener noreferrer" target="_blank">the ability to show lines with widths broke in three.js</a>. This was <a href="https://github.com/mrdoob/three.js/pull/11349" rel="noopener noreferrer" target="_blank">fixed in three.js r91 or r92</a>, which doesn’t help users of the Forge viewer, who are currently using r71. So we can’t just go in and tweak the linewidth setting of the SkeletonHelper’s material, as it won’t change anything.</p>
<p>Luckily for us, there’s the <a href="https://github.com/spite/THREE.MeshLine" rel="noopener noreferrer" target="_blank">MeshLine</a> library, which helps build a mesh to represent a thick line. I wasn’t aware of this awesome library until <a href="https://github.com/elifer5000" rel="noopener noreferrer" target="_blank">Elias Cohenca</a> from the BIM 360 viewer team pointed me at it during an internal discussion. Elias has even <a href="https://github.com/elifer5000/THREE.MeshLine" rel="noopener noreferrer" target="_blank">forked the library</a> to make <a href="https://github.com/elifer5000/THREE.MeshLine/blob/work-with-r71/src/THREE.MeshLine.js" rel="noopener noreferrer" target="_blank">a version that works well with the r71 (and consequently the Forge viewer)</a>. Too cool!</p>
<p>Elias suggested copying the implementation of the standard SkeletonHelper object and updating it to extend a Mesh – managed by an underlying MeshLine – rather than extending a LineSegments object, as it does in r71. I went ahead and did this, but quickly found that having a single MeshLine doesn’t work for representing skeletal geometry: it’s great for geometry with contiguous segments – such as a flowline, something we’ll see in a future post – but it doesn’t work with pairs of vertices that represent disconnected segments.</p>
<p>This left me with two choices: either to tweak the shader implementation to pick up pairs of vertices – rather than treating them as connected segments – or to store a series of MeshLines, one for each bone, and then use these to generate multiple Mesh objects. I’m not a hardcore HLSL coder, so opted for the latter choice, leaving the shader modification path open in case the performance wasn’t adequate. (This might well be the case if dealing with huge crowds of skeletons, but I doubt we’ll be working with so many for some time to come.)</p>
<p>So rather than having the new SkeletonHelper2 object extend a Mesh, I switched to have it extend a <a href="https://threejs.org/docs/#api/en/objects/Group" rel="noopener noreferrer" target="_blank">Group</a> (which is basically a THREE.Object3D that brings a bit of syntactic clarity to the fact it contains multiple objects, in turn).</p>
<p>I’m really happy with the results… having separate meshes also makes it easier to manage colours, having a different one for each bone.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad368fcda200c-pi" rel="noopener noreferrer" target="_blank"><img alt="Skeletons" border="0" height="399" src="/assets/image_165771.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Skeletons" width="500" /></a></p>
<p>One major improvement came when – at Simon Breslav’s suggestion, as he’d come across this when <a href="http://www.keanw.com/2017/10/dasher-360-at-autodesk-university-and-the-forge-devcon.html" rel="noopener noreferrer" target="_blank">animating robots inside the Forge viewer</a> – I set the MeshLines’ materials to have depthTest set to false. This means the skeletons will be visible through walls, but frankly that’s actually a feature, rather than a bug: it’ll be very useful for people trying to work out where (for instance) the bridge is inside Pier 9, once we can visualise bodies walking across it.</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad368fcde200c-pi" rel="noopener noreferrer" target="_blank"><img alt="Walking skeleton" height="307" src="/assets/image_988662.jpg" style="margin: 30px auto; float: none; display: block;" title="Walking skeleton" width="500" /></a><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad38f1fd4200d-pi" rel="noopener noreferrer" target="_blank"><img alt="Walking skeletons" height="378" src="/assets/image_778382.jpg" style="margin-right: auto; margin-left: auto; float: none; display: block;" title="Walking skeletons" width="500" /></a></p>
<p>Here’s the TypeScript code for the SkeletonHelper2 class:</p>
<p>
<script src="https://gist.github.com/KeanW/c1817c384d388e1dd9aa2768d7d7eb7b.js"></script>
</p>
<p>Here’s the updated extension implementation:</p>
<p>
<script src="https://gist.github.com/KeanW/f1f5edf3661f347f4f3970da802e88f3.js"></script>
</p>
<p>That’s it for this series on animating skeletons inside the Forge viewer. At some point I’ll be hooking this up to movement data derived from video footage, which will be really cool. I’ll share news about its integration into <a href="http://dasher360.com" rel="noopener noreferrer" target="_blank">Dasher 360</a>, as it happens.</p>
<p>In the meantime I’ll share some information on other uses we’ve found for the MeshLine class, to implement something we call Streamlines (or Speedlines, in a more extreme case). I think this is going to be super-interesting to anyone looking to display simulation and analysis results in the Forge viewer (I’m thinking CFD results, in particular, but that’s just one obvious example).</p>
