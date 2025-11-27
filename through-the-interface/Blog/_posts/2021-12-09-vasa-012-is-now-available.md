---
layout: "post"
title: "VASA 0.1.2 is now available"
date: "2021-12-09 13:35:00"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Dynamo"
  - "VASA"
original_url: "https://www.keanw.com/2021/12/vasa-012-is-now-available.html "
typepad_basename: "vasa-012-is-now-available"
typepad_status: "Publish"
---

<p>The <a href="https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html" rel="noopener" target="_blank">first version of VASA we posted to the Dynamo package manager</a> was numbered 0.1.0. Yesterday we posted a new version with some interesting new features, not least of which is the lack of an error when you install it (this one was very much my fault: I’d neglected to mark a couple of DLLs as not being node libraries when I posted it).</p>
<p>Here’s a breakdown of the new features in 0.1.2, courtesy of the package’s author, Rhys Goldstein:</p>
<blockquote><ul><li>Added <strong>VoxelModel.ToSolid</strong>, which converts a voxel model into a Dynamo solid that can be processed using standard Dynamo operations or visualized in Revit or FormIt.</li><li>Added <strong>VisibilityOperations.VisibilityWithinCone</strong>, which computes the region visible from a specified point, in a specified direction, and within a specified view angle.</li><li>Added <strong>VisibilityOperations.VisibilityWithinPyramid</strong>, which computes the region visible from a specified point, in a specified direction, and within both a horizontal and vertical view angle. Both of these &quot;visibility within&quot; nodes were added to support directional view analysis and camera coverage analysis.</li><li>Added <strong>DistanceField.SampleDistance</strong>, which quickly approximates the travel distance to a point without generating a path.</li><li>Added <strong>DistanceField.VoxelDistances</strong>, which produces a sorted list of distances to each voxel in the distance field. This feature allows the region around a source point to be limited by area rather than maximum distance.</li><li>Added a sample Python script, which demonstrates how to iterate over the voxels in a voxel model. The script has been added to the <em>10-VASA-Slices-and-Images.dyn</em> example.</li></ul></blockquote><p>The first new node, <strong>VoxelModel.ToSolid</strong> is going to be very handy when we start looking at importing and working with point clouds in VASA – more on that soon.</p>
<p>The next two visibility-based nodes constraint the visibility analysis to be within one of two geometric shapes. Here’s cone-based visibility:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278805e450d200d-pi" rel="noopener" target="_blank"><img alt="Visibility within a cone" border="0" height="241" src="/assets/image_885923.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Visibility within a cone" width="500" /></a></p>
<p>Here’s an animation to give you a better sense of how it works:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e136c3c4200b-pi" rel="noopener" target="_blank"><img alt="Animated visibility within a cone" height="278" src="/assets/image_674191.jpg" style="margin: 30px auto; float: none; display: block;" title="Animated visibility within a cone" width="500" /></a></p>
<p>And here are a couple of screenshots of pyramid-based visibility:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e202942f8bff13200c-pi" rel="noopener" target="_blank"><img alt="Visibility within a pyramid" border="0" height="240" src="/assets/image_362258.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Visibility within a pyramid" width="500" /></a></p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278805e4555200d-pi" rel="noopener" target="_blank"><img alt="Another angle" border="0" height="275" src="/assets/image_28507.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="Another angle" width="500" /></a></p>
<p>We expect the last two <strong>DistanceField</strong> nodes to be helpful for Generative Design workflows – something we’ll explore further in due course.</p>
<p>In case you’re wondering what happened to version 0.1.1, this was posted about 20 minutes before 0.1.2: during the publishing process Dynamo had picked up an older version of VASA hidden inside a “test” folder, and used that for the file definitions. I’ve now realised that it really does make sense to pay close attention to the warnings given during the Dynamo package publishing process. Hopefully with future releases it’ll be plain sailing, assuming I learn something from these various lessons.</p>
<p>Anyway, please do give this version of VASA a try, if there’s something in the above list that speaks to you. More features are on the way, and Rhys will certainly be recording more “how to” videos, looking at the various VASA sample graphs. Watch this space!</p>
