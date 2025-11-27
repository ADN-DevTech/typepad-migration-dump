---
layout: "post"
title: "Voxelising point clouds in Dynamo with VASA"
date: "2023-06-27 10:04:02"
author: "Kean Walmsley"
categories:
  - "Dynamo"
  - "Generative design"
  - "Point clouds"
  - "VASA"
original_url: "https://www.keanw.com/2023/06/voxelising-point-clouds-in-dynamo-with-vasa.html "
typepad_basename: "voxelising-point-clouds-in-dynamo-with-vasa"
typepad_status: "Publish"
---

<p><a href="https://www.keanw.com/2023/06/flocking-point-clouds-in-dynamo.html" rel="noopener" target="_blank">Last week</a> we saw a Dynamo graph that imports a point cloud from an ASCII-based format to specify the initial locations for a fun flocking simulation.</p>
<p>This week we’re going to look at a graph that uses a similar import technique but rather creates a voxel model based on the point cloud:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202c1b257cb3d200d-pi" rel="noopener" target="_blank"><img alt="Point cloud" border="0" height="267" src="/assets/image_877724.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Point cloud" width="500" /></a></p>
<p>The main difference from the previous import code is that rather than returning a list of Point objects, we’ve tweaked the code to return 9 doubles for each point that are taken as triangle definitions. The little secret is that the triangle’s vertices are coincident: we use the same XYZ coordinates 3 times.</p>
<p>VASA’s VoxelModel has a handy ByTriangles node that will consume this list to populate itself: wherever a point (defined as a zero area triangle) is ingested that sits within a voxel, that voxel then gets switched on. Obviously many (if not all) voxels will get “hit” by multiple points, so this does in effect simplify the point cloud. And you can choose to read a lot fewer points – we read 1 in a 100 or so – from the file and get a very similar-looking voxel model.</p><p>A quick word of warning when looking to make this work with your own point cloud data: when working with grid- or voxel-based tools it’s important to get the scale and resolution right, otherwise you run out of available memory or the calculations take too long. This is perhaps even more important when working with data coming from something like a point cloud, as you can quickly hit problems. VASA will warn you when you allocate beyond a certain limit, though, so it should be obvious when you’re likely to run into issues.</p>
<p>This mechanism allows us to work with point clouds as if they’re solid, which is pretty cool. There are number of parameters that are interesting to play with in the graph, so let’s take a look at them.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202c1b257cb4d200d-pi" rel="noopener" target="_blank"><img alt="Point cloud load and slice graph" border="0" height="82" src="/assets/image_705323.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Point cloud load and slice graph" width="500" /></a></p>
<p>Firstly there’s the ability to expand the generated voxel model using SurfaceOperations.ExpandAlongX (and Y and Z). Here’s an animation showing the changes with this set from 0 to 3. (We use 1 in the graph.)</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b751a959a5200c-pi" rel="noopener" target="_blank"><img alt="Point cloud with expansion" height="267" src="/assets/image_71963.jpg" style="margin: 30px auto; float: none; display: block;" title="Point cloud with expansion" width="500" /></a></p>
<p>Next is the Level of Detail (LOD) that is used in the ByTriangles call. Here we look at the values of 0 to 5. (We use 4 in the graph.)</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202c1a6cbe793200b-pi" rel="noopener" target="_blank"><img alt="Point cloud with LOD change" height="267" src="/assets/image_423200.jpg" style="margin: 30px auto; float: none; display: block;" title="Point cloud with LOD change" width="500" /></a></p>
<p>To show that this is a shell – rather than being completely solid – I created the below animation from a series of horizontal slices through the model. You can imagine using this technique to extract a floorplan from a more elaborate point cloud showing a building’s internal structure.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202b751a9599b200c-pi" rel="noopener" target="_blank"><img alt="Point cloud slicing" height="267" src="/assets/image_587422.jpg" style="margin: 30px auto; float: none; display: block;" title="Point cloud slicing" width="500" /></a></p>
<p>Here’s <a href="https://through-the-interface.typepad.com/files/PointCloudWithSlice.dyn" rel="noopener" target="_blank">the graph for you to experiment with</a>. You will need to download <a href="https://through-the-interface.typepad.com/files/WhatTheFlockingPointClouds.zip" rel="noopener" target="_blank">the archive from the previous post</a> to get the point cloud (which is pretty huge), as I didn’t want to post it again.</p>
<p>In an upcoming post we’ll take a look at some of the other fun things you can do with a voxel model created from a point cloud. A quick teaser: have you ever seen a point cloud that casts shadows?</p>
