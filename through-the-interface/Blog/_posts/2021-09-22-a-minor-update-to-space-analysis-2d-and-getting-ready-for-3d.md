---
layout: "post"
title: "A minor update to Space Analysis (2D), and getting ready for 3D!"
date: "2021-09-22 14:53:38"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2021/09/a-minor-update-to-space-analysis-2d-and-getting-ready-for-3d.html "
typepad_basename: "a-minor-update-to-space-analysis-2d-and-getting-ready-for-3d"
typepad_status: "Publish"
---

<p>This week I’ve been getting my Revit installation working again – it had been a while since I’d used it, and it was overdue an upgrade from 2021 to 2022 – mainly because I have to publish a couple of new packages (or package versions) to the Dynamo Package Manager.</p>
<p>The first is a minor update to the Space Analysis package, taking it from version 0.3.3 to version 0.4.0. There is a breaking change, however, in that people using the Visibility nodes from Python may have to modify their code to use the correct (newly added) namespace.</p>
<p>We only hit this issue once in <a href="http://autode.sk/mars-graph-v3" rel="noopener" target="_blank">V3 of the MaRS graph</a>. On load – and execute with the new library loaded – we see a Python error that we can navigate to easily using <a href="https://www.keanw.com/2018/07/introducing-warnamo-a-dynamo-package-for-managing-warnings-and-errors.html" rel="noopener" target="_blank">Warnamo</a>:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e123056e200b-pi" rel="noopener" target="_blank"><img alt="Warnamo showing the migration issue in the MaRS graph" border="0" height="312" src="/assets/image_102065.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Warnamo showing the migration issue in the MaRS graph" width="500" /></a></p>
<p>The reported error is quite clear, in that we have to modify line 9 of the Python script:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278804a8ea9200d-pi" rel="noopener" target="_blank"><img alt="The error is on line 9 of the Python script" border="0" height="312" src="/assets/image_842454.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="The error is on line 9 of the Python script" width="500" /></a></p>
<p>Adding “SpaceAnalysis.” as a prefix is the way to get the node properly resolved:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e2026bdef2b22d200c-pi" rel="noopener" target="_blank"><img alt="Adding the SpaceAnalysis namespace" border="0" height="312" src="/assets/image_111276.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Adding the SpaceAnalysis namespace" width="500" /></a></p>
<p>Saving the Python script and running it shows that it works fine:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20278804a8eaf200d-pi" rel="noopener" target="_blank"><img alt="The error has gone and the visibility results are displayed on run" border="0" height="312" src="/assets/image_163625.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="The error has gone and the visibility results are displayed on run" width="500" /></a></p>
<p>Here’s <a href="https://autode.sk/mars-graph-v31" rel="noopener" target="_blank">the fixed version of the graph</a>, so you don’t need to migrate it yourselves.</p>
<p>Hopefully it’s obvious why adding a namespace is a good thing – it makes it more obvious that the nodes are grouped in a single package, and reduces the risk of name-clashes with other libraries – and with any luck it won’t affect people very much in terms of the migration effort required. (If you’re not creating the SpaceAnalysis nodes inside a Python script then the chances are you’re not going to see any migration effort at all: nodes that have been added to the graph will continue to work as they did before.)</p>
<p>Another change in this version will hopefully address something Jon Pierson came across during <a href="https://youtu.be/wNlR3pfJ2xA" rel="noopener" target="_blank">his <em>awesome</em> walk-through on using Space Analysis with Revit</a>. This is such a cool video – I thoroughly recommend watching it, if you have the time and interest.</p>
<p style="text-align: center">&#0160;</p>
<p style="text-align: center"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="283" src="https://www.youtube.com/embed/wNlR3pfJ2xA" title="YouTube video player" width="500"></iframe></p>
<p>&#0160;</p>
<p>A couple of times Jon came across some “interesting” behaviour from Space Analysis where it showed perhaps more than strictly made sense. While we weren’t able to reproduce this behaviour on our side, Rhys was able to make a fair guess at what’s happening and how to fix it, which he has hopefully managed to do this update.</p>
<p>Next week we’ll be unveiling the evolution of Space Analysis, a voxel-based system that will be usable for 3D path-finding and visibility analysis. Watch this space!</p>
