---
layout: "post"
title: "Project Rediscover - version 2 of the MaRS graph is now available for download"
date: "2019-11-11 17:27:48"
author: "Kean Walmsley"
categories:
  - "AU"
  - "Autodesk"
  - "Autodesk Research"
  - "Dynamo"
  - "Generative design"
original_url: "https://www.keanw.com/2019/11/project-rediscover-version-2-of-the-mars-graph-is-now-available-for-download.html "
typepad_basename: "project-rediscover-version-2-of-the-mars-graph-is-now-available-for-download"
typepad_status: "Publish"
---

<p>In advance of <a href="https://www.autodesk.com/autodesk-university/class/Hands-Project-Rediscover-generatively-designing-Autodesk-Torontos-office-2019" rel="noopener" target="_blank">next week’s AU2019 class entitled “Hands-on with Project Rediscover: generatively designing Autodesk Toronto's office”</a>, I’m happy to announce that version 2 of the MaRS graph is <a href="http://autode.sk/mars-graph" rel="noopener" target="_blank">now available for download</a>.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4e9c5f4200b-pi" rel="noopener" target="_blank"><img alt="v2 of the MaRS graph" border="0" height="86" src="/assets/image_816865.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="v2 of the MaRS graph" width="500" /></a></p>
<p>(Many thanks to Simon Breslav for all his work on the graph, as well as to both Simon and Rhys Goldstein for their hard work making <a href="https://www.keanw.com/2019/03/the-space-analysis-package-for-dynamo-and-refinery-is-now-available.html" target="_blank" rel="noopener">Space Analysis</a> what it is: a cornerstone of Project Rediscover.)</p>
<p>While version 1 was about “getting something out there that works”, the focus for version 2 was to have something that’s more discoverable and understandable.</p>
<p>We’ve done a few things to make the graph more understandable. Firstly, we’ve added notes to all the major node categories to provide a high-level description of their role in the overall process.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c52936200d-pi" target="_blank" rel="noopener"><img alt="A note in a node category" border="0" height="131" src="/assets/image_449226.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="A note in a node category" width="500" /></a></p>
<p>Secondly, we’ve given the ability to decouple the geometry system from the metrics calculations. Freezing this one node will stop all the downstream nodes from evaluating, allowing you to explore how the inputs to the graph affect the generation of geometry without having to wait for the often time-consuming metrics to evaluate.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4e9c680200b-pi" rel="noopener" target="_blank"><img alt="Freezing the central node" height="313" src="/assets/image_254864.jpg" style="margin: 30px auto; float: none; display: block;" title="Freezing the central node" width="500" /></a></p>
<p>You can even change the graph to execute automatically and then play with the input sliders: with the evaluation system frozen performance is good enough for tolerably real-time feedback.</p>
<p>Thirdly, we’ve added a series of steps that show how the geometry system works. These can be enabled one after the other (or selectively) to make it very clear how the geometry system functions.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c52a53200d-pi" target="_blank" rel="noopener"><img alt="Steps that explain the geometry system" border="0" height="310" src="/assets/image_254513.jpg" style="margin: 30px auto; border: 0px currentcolor; float: none; display: block; background-image: none;" title="Steps that explain the geometry system" width="500" /></a></p>
<p>It’s this mechanism that helped me create this GIF from a few weeks ago.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c523d7200d-pi" rel="noopener" target="_blank"><img alt="MaRS steps" height="485" src="/assets/image_909519.jpg" style="margin-right: auto; margin-left: auto; float: none; display: block;" title="MaRS steps" width="600" /></a></p>
<p>We’ve done some other things, too, such as to swap out our previous “hand-rolled” daylight analysis approach for the “Solar Analysis for Dynamo” package (it’s a repackaging of the former Ecotect technology that you can install via the Dynamo package manager).</p>
<p>Interestingly – and I believe this is a side-effect of swapping out our custom daylighting implementation – the graph now runs just as quickly using the serial processing in Python as it does when using <a href="https://www.keanw.com/2019/05/parallel-operations-in-dynamo-and-refinery.html" rel="noopener" target="_blank">parallel loops</a>: avoiding parallelism does make Refinery perform more reliably, from what we can tell, so all of this is a good thing.</p>
<p>So this graph now seems to work very well with both Project Refinery and also with Capturefinery, if you want to capture some cool animations from your Refinery studies.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c5297d200d-pi" target="_blank" rel="noopener"><img alt="distraction-tiny" height="284" src="/assets/image_6323.jpg" style="margin: 30px auto 0px; float: none; display: block;" title="distraction-tiny" width="500" /></a></p>
<p>Here are some that I’ve managed to generate over the last week or so by combining various metric visualizations from the core graph.</p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c523c5200d-pi" rel="noopener" target="_blank"><img alt="Showing congestion with the desk and amenity layout" height="269" src="/assets/image_793838.jpg" style="margin: 30px auto 0px; float: none; display: block;" title="Showing congestion with the desk and amenity layout" width="500" /></a></p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a49be7b5200c-pi" rel="noopener" target="_blank"><img alt="Dyalight alongside views to outside" height="269" src="/assets/image_725699.jpg" style="margin-right: auto; margin-left: auto; float: none; display: block;" title="Dyalight alongside views to outside" width="500" /></a></p>
<p><a href="https://through-the-interface.typepad.com/.a/6a00d83452464869e20240a4c523c7200d-pi"><img alt="Work style with shortest paths" height="269" src="/assets/image_513227.jpg" style="margin-right: auto; margin-left: auto; float: none; display: block;" title="Work style with shortest paths" width="500" /></a></p>
<p>If there ends up being another phase to this project, I expect it will focus on understanding and streamlining the graph’s performance. Right now the graph executes in a very tolerable 40 seconds or so (on the systems I’ve tested it on), but with the help of profiling tools people are working on for Dynamo I suspect we can shave several seconds off that number. We’ll see.</p>
<p>If you’re interested in this topic and will be at AU Las Vegas, do try to <a href="https://autodeskuniversity.smarteventscloud.com/connect/sessionDetail.ww?SESSION_ID=322469" target="_blank" rel="noopener">come along to the class</a>, or otherwise stop me for a chat at some point if you see me wandering past.</p>
