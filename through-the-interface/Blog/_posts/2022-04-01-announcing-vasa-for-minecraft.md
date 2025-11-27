---
layout: "post"
title: "Announcing VASA for Minecraft"
date: "2022-04-01 05:00:00"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "VASA"
original_url: "https://www.keanw.com/2022/04/announcing-vasa-for-minecraft.html "
typepad_basename: "announcing-vasa-for-minecraft"
typepad_status: "Publish"
---

<p>Followers of this blog will have seen me mention the <a href="https://www.keanw.com/2021/09/introducing-vasa-for-voxel-based-architectural-space-analysis.html" rel="noopener" target="_blank">Voxel-based Architectural Space Analysis (VASA)</a> package (you can find all the posts about it <a href="https://keanw.com/VASA" rel="noopener" target="_blank">here</a>). <a href="https://www.keanw.com/2021/11/a-video-introduction-to-vasa.html" target="_blank">VASA</a> is a toolkit that allows you to voxelise and analyse 3D geometry, whether for <a href="https://www.keanw.com/2021/11/a-video-on-pathfinding-with-vasa.html" target="_blank">pathfinding</a>, <a href="https://www.keanw.com/2021/12/a-video-on-visibility-and-daylight-analysis-with-vasa.html" target="_blank">visibility or daylight analysis</a>.</p>
<p>The big news of the day is that Autodesk Research has managed to port VASA across to work with the popular voxel-based creation environment, Minecraft. This actually was pretty easy, as we could completely ignore all the code needed to voxelise a 3D environment from the codebase and focus on the analysis side of things. A straightforward port, all things considered, even if the conversion from C++ to Java presented a few fun challenges.</p>
<p>Here’s a view of a VASA path inside Minecraft:</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20282e14c9ee3200b-pi" rel="noopener" target="_blank"><img alt="A VASA path in Minecraft" border="0" height="288" src="/assets/image_138070.jpg" style="margin: 30px auto; border: 0px currentcolor; border-image: none; float: none; display: block; background-image: none;" title="A VASA path in Minecraft" width="500" /></a></p>
<p>Now for a video showing how VASA’s pathfinding works inside Minecraft: you place your VASA placemarkers at the start and destination locations, then use the “initiate” tool to create the path, which leads to the path voxels being converted to a transparent material, as you may be able to see in the above image.</p>
<p style="text-align: center;"><br /></p>
<p style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="283" src="https://www.youtube.com/embed/jLWHKh7qoQs" title="YouTube video player" width="500"></iframe></p>
<p><br /></p>
<p>We think that Minecraft is a fantastic environment not only for creating new 3D models but for analysing existing ones. Expect further work to support additional types of analysis, as well as to <a href="https://www.keanw.com/2014/09/exporting-minecraft-data-from-autocad.html" rel="noopener" target="_blank">streamline workflows for taking geometry from Autodesk products into Minecraft</a>.</p>
<p>If you want to give VASA for Minecraft a try, head on over to <a href="https://www.curseforge.com/" rel="noopener" target="_blank">CurseForge</a> and search for “VASA”. We’ve published versions for both <a href="https://fabricmc.net/" rel="noopener" target="_blank">Fabric</a> and <a href="https://files.minecraftforge.net/net/minecraftforge/forge/" rel="noopener" target="_blank">Forge</a>, so as long as you use one of these mod-loaders, you should find it easy to get going. Let us know what you think!</p>
