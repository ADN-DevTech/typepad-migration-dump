---
layout: "post"
title: "Introducing VASA, a tool for Voxel-based Architectural Space Analysis"
date: "2021-09-29 14:20:53"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Dynamo"
  - "Generative design"
  - "VASA"
original_url: "https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html "
typepad_basename: "introducing-vasa-for-voxel-based-architectural-space-analysis"
typepad_status: "Publish"
---

<p>I’ve been hinting about this for a few weeks now, but today is (very excitingly) the day for the big unveiling. My colleague Rhys Goldstein has been working his magic, once again, taking the algorithms he developed for the 2D <a href="https://www.keanw.com/2019/04/using-the-space-analysis-package-for-pathfinding-and-visibility-in-dynamo.html" rel="noopener" target="_blank">Space Analysis</a> package and applying them to 3-space. If you were impressed by Space Analysis, what we’re showing now may just knock your socks off.</p>
<p>The new package is called VASA, which stands for <strong>Voxel-based Architectural Space Analysis</strong>. It’s available today for download from the Dynamo Package Manager.</p>
<p>It’s worth unpacking a couple of terms, here: <em>voxel-based</em> means VASA breaks the world up into cubes of a certain size, in much the way Minecraft does. It can do so by reading from a mesh – it can use STL files or meshes generated from Revit geometry, for instance – and checking whether any of the polygons clash with any of the voxels (at which point the voxel is marked as being “on”). It then allows you to perform a number of analyses on this 3D volume, such as pathfinding, daylight and visibility analysis. It’s <em>architectural</em>, in that certain design decisions have been made – particularly for features such as pathfinding – that target the movement of people through a space. Which means it’s not – out of the box, anyway – going to be good at solving problems such as routing of MEP/HVAC systems. It could, in time, be extended to help address this type of question, but it wasn’t he focus for this initial release.</p>
<p>VASA is being shared as a set of Dynamo nodes that can be loaded into either Dynamo Sandbox or Dynamo for Revit. There are a set of 10 samples – which you can find in <em>%appdata%\Dynamo\Dynamo Core\2.x\packages\VASA\extra</em> (or perhaps <em>%appdata%\Dynamo\Dynamo Revit\2.x\packages\VASA\extra</em>) – that will step you through the VASA feature-set.</p>
<p>Here’s a quick animation from the “overview” sample that shows daylight, visibility and pathfinding combined. Performance is impressive – it’s very nearly interactive as we change the distance for the visibility field’s cut-off:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278804c93d1200d-pi"><img alt="VASA animation" height="312" src="/assets/image_876184.jpg" style="margin: 30px auto; float: none; display: block;" title="VASA animation" width="500" /></a></p>
<p>A bit of fun: Rhys chose the name VASA as an acronym, but also because it’s also the name of <a href="https://en.wikipedia.org/wiki/Vasa_(ship)" rel="noopener" target="_blank">a famous 17th century Swedish warship who happened to sink on her maiden voyage</a> (ahem). <a href="https://en.wikipedia.org/wiki/Bjarne_Stroustrup" rel="noopener" target="_blank">Bjarne Stroustrup</a>, the inventor of C++, <a href="https://www.stroustrup.com/P0977-remember-the-vasa.pdf" rel="noopener" target="_blank">talked about Vasa as a metaphor for overly ambitious software projects</a>:</p>
<blockquote>
<p><em>There are people who concluded from the Vasa story that all incremental improvement is a bad strategy. However, if the Vasa had been sent to sea as originally designed, it could not have served its purpose. Being under-gunned, someone would have sent it to the bottom full of holes. Being somewhat ordinary, it would have failed in its representative (image) role. Recent research has shown that a relatively modest increase of the Vasa’s length and breadth (claimed technically feasible) would have made it stable, so my reading of the Vasa story is: Work hard on a solid foundation, learn from experience, and don’t scrimp on the testing.</em></p>
</blockquote>
<p>This story resonated with Rhys, hence its adoption for this project, which we believe has a solid enough foundation to support the various analyses we want it to perform. :-)</p>
<p>Rhys and I will be posting lots more information about VASA over the coming weeks, although for me this will only happen from mid-October, as for the next 2 weeks I’m going to be scuba-diving with my family in Sardinia).</p>
<p>While I’ll mostly be completely offline until October 18th, I will be delivering a keynote for the <a href="https://www.designcomputation.org/dcio-about" rel="noopener" target="_blank">DC I/O conference</a> on October 8th. It’s been recorded in advance, but I’ll try to join for the Q&amp;A, assuming the timing works with my family’s holiday activities. Here’s <a href="https://www.designcomputation.org/2021-programme" rel="noopener" target="_blank">the conference agenda</a>.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278804c9778200d-pi" rel="noopener" target="_blank"><img alt="DC IO keynote" border="0" height="312" src="/assets/image_908868.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="DC IO keynote" width="500" /></a></p>
<p>If you’d like to join this year’s DC I/O – it’s a conference focused on Design Computation and my keynote is about Digital Twins, so there’s something for everyone ;-) – you can use the below code for a 20% discount:</p>
<blockquote>
<p><span style="font-family: Courier New;">DCIO-S20-IP27-TSZU</span></p>
</blockquote>
<p>You can apply this coupon during <a href="https://www.eventbrite.co.uk/e/dc-io-2021-tickets-118694451299" rel="noopener" target="_blank">registration via EventBrite</a>. It even works on the early bird prices, although those expire in a day’s time (September 30th).</p>
<p><strong><em>&#0160;</em></strong></p>
<p><strong><em>Update:</em></strong></p>
<p>Rhys tells me he had trouble installing VASA from the Package Manager. He saw this error:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e12564cd200b-pi" rel="noopener" target="_blank"><img alt="Load error" border="0" height="375" src="/assets/image_483151.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Load error" width="412" /></a></p>
<p>The error is benign – and can be ignored safely –and we’ve informed the Dynamo team who will look into addressing it. It’s apparently a false positive that I’m hoping will be addressed in the not-too-distant future.</p>
