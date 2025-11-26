---
layout: "post"
title: "Introducing Level of Detail to Autodesk’s Viewer API"
date: "2016-08-04 18:23:06"
author: "Vaibhav Vavilala"
categories: []
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/introducing-level-of-detail-to-autodesks-view-data-api.html "
typepad_basename: "introducing-level-of-detail-to-autodesks-view-data-api"
typepad_status: "Draft"
---

<p class="p1">Writing a javascript-based generator of multiple resolutions of a model, and a client-side extension that can select the appropriate LOD.&#0160;</p>
<p class="p1">This project is the result of a summer internship project that addresses the feasibility of introducing level of detail to Autodesk&#39;s Viewer API.&#0160;</p>
<p class="p3">&#0160;<strong>I. Motivation</strong></p>
<p class="p2">Level of Detail refers to decreasing the resolution of a 3D model based on its distance from the camera. When an object is far away, we won’t be able to appreciate all of its details anyway. Scenes with smaller models (represented by fewer polygons) are faster to render. LOD is especially useful in interactive applications like video games, where maintaining a high frame rate is paramount. In films, LOD can make richly detailed environments tractable to render.</p>
<p class="p2">The Autodesk <a href="https://a360.autodesk.com/viewer/">A360 viewer</a> is a web-based renderer for both desktop and mobile platforms. It enables interactive viewing of 3D data (as opposed to production-quality rendering that can require hours of rendering per frame). Within A360, there already exist some rendering optimizations to maintain interactivity. For example, with progressive rendering mode, larger geometry renders before smaller objects. When memory is a limitation on browsers, especially on mobile devices, introducing distance-based Level of Detail could be expensive because multiple resolutions of each model would need to be sent to the platform. And sending more data slows download and loading times.</p>
<p class="p2">&#0160;</p>
<p class="p2"><img alt="Screen Shot 2016-08-04 at 5.34.59 PM" class="asset  asset-image at-xid-6a0167607c2431970b01b7c883953b970b img-responsive" height="324" src="/assets/image_dc14e0.jpg" style="float: left;" title="Screen Shot 2016-08-04 at 5.34.59 PM" width="239" /><img alt="Screen Shot 2016-08-02 at 11.32.14 PM" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20d41eb970c img-responsive" height="324" src="/assets/image_b83213.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Screen Shot 2016-08-02 at 11.32.14 PM" width="239" /></p>
<p class="p2"><em>[Left] 1000 polygon model. [Right] 70K polygon model.&#0160;The graphics hardware available determines which model we should render. Model courtesy of <a href="http://www.cs.cmu.edu/~kmcrane/Projects/ModelRepository/">Keenan Crane</a>.&#0160;</em></p>
<p class="p2">In keeping with the spirit of maintaining interactivity with large models on all platforms, we proposed the following variant of Level of Detail. We generate multiple resolutions of a model, translate them, and store them in the A360 Cloud. Then, when the model is requested on the client-side, the appropriate version of the model is selected, and the LOD representation can be modified on the fly. The client will be provided with the LODs that are available (expressed as percentages), and the representation will automatically decrease if the frame rate is poor, or increase if more detail can be handled while maintaining interactivity.</p>
<p class="p3">Here, we further describe how the LODs were generated, and how the client consumes them. Below, find links to the code, which the reader is free to use or extend in their own projects.&#0160;</p>
<p class="p3">Link to LOD Server: <a href="https://github.com/vibe007/Autodesk-Viewer-Level-of-Detail-Server">https://github.com/vibe007/Autodesk-Viewer-Level-of-Detail-Server</a></p>
<p class="p5"><span class="s1">Link to LOD Client: <a href="https://github.com/vibe007/Autodesk-Viewer-Level-of-Detail-Client">https://github.com/vibe007/Autodesk-Viewer-Level-of-Detail-Client&#0160;</a></span></p>
<p class="p3">&#0160;</p>
<p class="p3"><strong>II. LOD generator</strong></p>
<p class="p2">The LOD generator is a javascript-based mesh processing utility. The user inputs a model (only OBJ support for now), an optional texture, and the LODs requested. The output includes the LODs also in OBJ format, and the URNs. We use the child_process node module to run processes on the command line within our script. Here are the steps taken:</p>
<ol class="ol1">
<li class="li2">Use the MeshLab API to simplify the model, and save the versions. MeshLab ships with both a GUI version and a command line tool called <a href="http://www.andrewhazelden.com/blog/2012/04/automate-your-meshlab-workflow-with-mlx-filter-scripts/">meshlabserver</a>. We use the latter here. Meshlab simplifies models via the “Quadric edge collapse” algorithm proposed by <a href="https://www.cs.cmu.edu/~./garland/quadrics/quadrics.html">Garland &amp; Heckbert</a>. It also offers simplification with texture support, so we can reuse the original texture on a decimated model. As a side note, we first tried <a href="https://github.com/libigl/libigl">libigl</a>, a C++ based geometry processing library, but it does not yet support mesh decimation with textures.&#0160;</li>
<li class="li2">Compress (zip) each LOD representation, together with associated files like textures. We use the &#39;child_process&#39; node module to execute commands on the command line (for zipping and executing meshlabserver).&#0160;</li>
<li class="li2">Translate each LOD using the Viewer&#0160;API and save the bubble.json containing the URN. The Viewer&#0160;codebase, which powers the A360 viewer, contains a utility called translate-util.js which we modify and use for our implementation. This utility uploads the zip file to the A360 cloud and downloads the derivative files. We choose to only keep the URN in our program, as the derivative files are safely stored in the cloud and can be accessed via the URN.</li>
</ol>
<p class="p3">&#0160;</p>
<p class="p3">&#0160;</p>
<p class="p4"><strong>III. Client-side Viewing</strong></p>
<p class="p2">To demonstrate our contribution, we implement a javascript-based extension that consumes the LODs we generated using LOD_server in the pervious step. The extension should be provided with the URN of one of the models we created, as well as the LODs that are available (expressed as percents - 10%, 50%, 80% etc). The extension works as follows:</p>
<ol class="ol1">
<li class="li2">Load the base URN into the viewer.</li>
<li class="li2">Construct the URNs of all the LODs available. Because the URN is a Base64 encoded string, we can decode it and modify it as shown in the figure. We simply adjust the percent representation (here, 10), and encode back to Base64.&#0160;</li>
<li class="li2">The Viewer API gives the programmer access to the frame rate. We monitor the frame rate, and can increase or decrease the LOD representation based on the relationship between the target rate and the current rate. The target rate parameter is exposed and can be modified. When we do need to change the representation, we download the new model via the URN, unload the current model, and then load the new one into the viewer. For example: if the LOD representations of a model are [10%, 40%, 60%, 80%], the target frame rate is 50 fps, and the usual frame rates associated with these LODs are [100, 60, 40, 30], the extension will render the first, swap it with the second, then swap it with the third, and stop there.&#0160;</li>
</ol>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d20d4fdd970c-pi" style="float: left;"><img alt="Screen Shot 2016-08-04 at 1.00.15 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d20d4fdd970c image-full img-responsive" src="/assets/image_3246eb.jpg" style="margin: 0px 5px 5px 0px;" title="Screen Shot 2016-08-04 at 1.00.15 PM" /></a></p>
<p>&#0160;</p>
<p>&#0160;<em>The URNs of the various LODs can be dynamically computed via Base64 encode/decode operations. &#0160;</em>&#0160;</p>
<p>&#0160;</p>
<p><strong>IV.&#0160;Future Work</strong></p>
<p>There is a popping effect when one model is swapped out and the next&#0160;LOD is substituted. Platforms that could handle more memory could have multiple LODs saved in the same SVF packet for smoother transitions (SVF is Autodesk&#39;s file format that the viewer can render). Including multiple LODs in the same SVF could also reduce the total data transmitted, as textures, material information, and metadata is repeated between the SVF packets. A possible extension could be to stream the contents of a single SVF packet, starting with the lowest LOD, which would speed up time to first render. Then based on the performance metrics (hardware, frame rate), we could either stream the next LOD or stop there.&#0160;</p>
<p>In addition, we could also store a cookie on the client&#39;s browser with an indication as to how many polygons the client can handle while maintaining interactive frame rates (of course, the exact frame rate will vary based on the shaders used). Then future model loads can show the appropriate LOD immediately.&#0160;</p>
<p>The current client-side extension does not examine the the graphics hardware when determining the LOD to render; it only looks at the frame rate.&#0160;</p>
<p>This project also validated that we cannot swap fragments within a single mesh under the current implementation of the Viewer API, because fragments between multiple LODs do not necessarily coincide. For example, if we have the 10% model of a cow loaded, we cannot stream parts of the 50% cow and replace the 10% head first with the 50% head, then the body and so forth. However, it should be possible to swap out models of different resolutions (i.e. if we had a horse and a saddle, future work should allow us&#0160;to render the 10% horse alongside a 20% saddle).&#0160;</p>
<p>&#0160;</p>
<p><strong>V. Author</strong></p>
<p>Vaibhav Vavilala</p>
<p>Intern Developer Evangelist @ Autodesk</p>
<p>Rising senior, Columbia University, B.S. Computer Science (exp. 2017)</p>
<p>4 August 2016</p>
<p>&#0160;</p>
<p><strong>VI. Thanks</strong></p>
<ul>
<li>Cyrille Fauvel and Stephen Preston, who supervised the project</li>
<li>Petr Broz, Michael Beale, and John Hutchinson for helpful conversations</li>
</ul>
