---
layout: "post"
title: "Adding 3D geometry to a scene in the Forge viewer"
date: "2018-08-31 09:05:47"
author: "Kean Walmsley"
categories:
  - "APS (Forge)"
  - "Autodesk Research"
  - "IoT"
  - "Machine Learning"
original_url: "https://www.keanw.com/2018/08/adding-3d-geometry-to-a-scene-in-the-forge-viewer.html "
typepad_basename: "adding-3d-geometry-to-a-scene-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>In <a href="http://keanw.com/2018/08/forge-skeletons-and-disappearing-signs.html" rel="noopener noreferrer" target="_blank">the last post</a> I introduced the series where I’ll be talking about the journey I’ve been going through to add skeletion data inside <a href="http://dasher360.com" rel="noopener noreferrer" target="_blank">Dasher 360</a> (which is, of course, based on the Forge viewer).</p>
<p>The first step I took along this path was to find a way to add simple 3D geometry into an existing scene inside the viewer. This is something we do in a limited way inside Dasher – we use point clouds to represent the locations of sensors, for instance – but I wanted to work out the right approach for doing this for data that could show people’s bodies as they walk around. The eventual aim will be to have the skeletal positioning driven by the database – which has been populated by a neural network harnessing computer vision data – but the primary goal for the next few posts is to prove that the Forge viewer is capable of displaying skeletal geometry.</p>
<p>I came across <a href="https://github.com/Autodesk-Forge/library-javascript-viewer-extensions/blob/3bc8881519513945dde7352881e23ddbf1facd70/src/Autodesk.ADN.Viewing.Extension.MeshData/Autodesk.ADN.Viewing.Extension.MeshData.js" rel="noopener noreferrer" target="_blank">this sample extension</a> by Philippe Leefsma that turned out to be a useful starting point: it highlights the various mesh faces of the object you select in the viewer. There were a few changes I made to the way it works:</p>
<ul>
<li>Rather than adding the geometry to the scene I want to add it to a “render overlay” which is managed separately by the Forge viewer infrastructure.</li>
<ul>
<li>There’s commented code if you want to try adding geometry directly to the main scene.</li>
</ul>
<li>I found I had to change the way lines were rendered: a standard line material didn’t work for me, I had to create 3D geometry (a thin cylinder or box) to represent each line.</li>
<li>We use TypeScript to implement Dasher 360, so I chose to use that (we also have our own set of extension-related features that I’ve made use of, so the code isn’t direct usable for people without them).</li>
</ul>
<p>Here’s a quick image of the results:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3aceef9200b-pi" rel="noopener noreferrer" target="_blank"><img alt="Selected geometry" border="0" height="343" src="/assets/image_472918.jpg" style="margin: 30px auto; float: none; display: block; background-image: none;" title="Selected geometry" width="500" /></a></p>
<p>We draw red spheres at the vertices and draw edges between them as green cylinders, as you can see from this animation:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2022ad3acef01200b-pi" rel="noopener noreferrer" target="_blank"><img alt="Selecting geometry" height="315" src="/assets/image_130108.jpg" style="margin: 30px auto; float: none; display: block;" title="Selecting geometry" width="500" /></a></p>
<p>Here’s the code that does it all:</p>
<p>
<script src="https://gist.github.com/KeanW/263f254cb0701fdf67e5b60029565638.js"></script>
</p>
<p>In the next post we’re going to see how we can modify the geometry placement code to create a simple skeleton on the selected geometry.</p>
