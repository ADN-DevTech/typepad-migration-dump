---
layout: "post"
title: "Using VASA with Forma - Part 1"
date: "2023-10-19 14:38:26"
author: "Kean Walmsley"
categories:
  - "AU"
  - "Autodesk Research"
  - "Dynamo"
  - "Forma"
  - "VASA"
original_url: "https://www.keanw.com/2023/10/using-vasa-with-forma-part-1.html "
typepad_basename: "using-vasa-with-forma-part-1"
typepad_status: "Publish"
---

<p>While preparing for one of my upcoming AU classes, I decided to explore how someone could use VASA - our Voxel-based Architectural Space Analysis toolkit - with Forma. Right now Forma is largely focused on conceptual design - taking into account the surrounding urban context - so this thinking was largely focused on how VASA might be used at the urban scale. This is something we’ve looked at before in the context of <a href="https://www.keanw.com/2021/11/using-vasa-with-formit-via-dynamo-part-2.html" target="_blank" rel="noopener">FormIt and Dynamo</a>.</p>
<p>While a tighter integration is likely to be possible at some point - I’ll talk a little more about our work on a WebAssembly version in the next post, and there’s some news coming soon that’s likely to be relevant - for now I wanted to focus on a slightly disconnected workflow: exporting geometry from Forma and loading that into a standalone Dynamo graph that uses VASA for pathfinding.</p>
<p>The simplest way I could find to export the currently scene from Forma is via an extension. And the simplest way to build an extension that does this is to use one of the <a href="https://github.com/spacemakerai/forma-extensions-samples" target="_blank" rel="noopener">Forma Sample Extensions</a>, which happens to define a very handy <a href="https://aps.autodesk.com/en/docs/forma/v1/embedded-views/introduction/" target="_blank" rel="noopener">Embedded View</a> with a button to <a href="https://github.com/spacemakerai/forma-extensions-samples/tree/main/analyses/export-stl" target="_blank" rel="noopener">export an STL file</a>. Perfect!</p>
<p>To get started you need to clone the GitHub repo containing the sample:</p>
<pre>git clone https://github.com/spacemakerai/forma-extensions-samples</pre>
<p>(If you don’t want to use the Git command-line tools or desktop app, you can also <a href="https://github.com/spacemakerai/forma-extensions-samples/archive/refs/heads/main.zip" target="_blank" rel="noopener">download a ZIP of the repo</a>.)</p>
<p>Once extracted you need to launch a local web-server that can server up the contents of the analyses/export-stl sub-folder. I use the one recommended in <a href="https://aps.autodesk.com/en/docs/forma/v1/embedded-views/getting-started/" target="_blank" rel="noopener">the Forma documentation for the local_testing extension</a> (more on this in a bit):</p>
<pre>npx http-server --cors -c-1 --port 8081</pre>
<p>The above launches a local HTTP server on port 8081 (which means the page being served will show up on the right of the Forma window) and with a cache timeout of -1 (which means if you do modify the code it’ll get served up straight away).</p>
<p>Once that’s running you can install and run the local_testing extension (as per the above-linked instructions) and you should see the ExportSTL button in the right panel:<a href="/assets/image_156294.jpg" target="_blank" rel="noopener"> <img style="display: block; margin: 30px auto;" title="Neuchatel inside Forma with the STL export extension" src="/assets/image_156294.jpg" alt="Neuchatel inside Forma with the STL export extension" width="500" height="375" border="0" /></a>Clicking the button will cause you to be prompted to choose an STL file for the export.</p>
<p>Loading the STL file in an external viewer - I chose one called STL Master on MacOS - we can see the contents of the scene. What’s nice is that the base plate is included, which will simplify our work in VASA.</p>
<p><a href="/assets/image_235028.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="The exported STL inside an external viewer" src="/assets/image_235028.jpg" alt="The exported STL inside an external viewer" width="500" height="375" border="0" /></a>Loading the STL file in Dynamo is easy. <a href="https://through-the-interface.typepad.com/files/VASA-pathfinding-in-3D-with-visibility-using-geometry-from-Forma.dyn" target="_blank" rel="noopener">Here’s a graph</a> that loads <a href="https://through-the-interface.typepad.com/files/Neuchatel%20in%20Forma.stl" target="_blank" rel="noopener">an STL file</a> and allows you to use VASA with it for both pathfinding and visibility:</p>
<p><a href="/assets/image_80646.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Graph using VASA on Forma geometry" src="/assets/image_80646.jpg" alt="Graph using VASA on Forma geometry" width="500" height="375" border="0" /></a>Here’s the graph working with our STL export from Forma. You can see a path in purple and a visibility dome in orange.</p>
<p><a href="/assets/image_778997.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="Using VASA on Forma geometry" src="/assets/image_778997.jpg" alt="Using VASA on Forma geometry" width="500" height="375" border="0" /></a></p>
<p>Here's a video of it in action:</p>
<p><a class="asset-img-link" href="/assets/image_643944.jpg"><img class="asset  asset-image at-xid-6a00d83452464869e202c8d3a0e79d200d image-full img-responsive" style="display: block; margin: 30px auto;" title="VASA in Dynamo using Forma geometry" src="/assets/image_643944.jpg" alt="VASA in Dynamo using Forma geometry" width="500" border="0" /></a></p>
<p>In the next post we’ll take a look at this same workflow working directly in Forma via an extension that hosts our (currently not published) WebAssembly version of VASA.</p>
