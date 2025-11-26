---
layout: "post"
title: "Work Half, AKS Opener, RvtFader and ForgeFader"
date: "2017-04-05 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AVF"
  - "Element Creation"
  - "Family"
  - "Forge"
  - "Geometry"
  - "JavaScript"
  - "JSON"
  - "Parameters"
  - "Ribbon"
  - "Threejs"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/04/work-half-aks-opener-rvtfader-and-forgefader.html "
typepad_basename: "work-half-aks-opener-rvtfader-and-forgefader"
typepad_status: "Publish"
---

<p>I completed a first revision of 
the <a href="https://github.com/jeremytammik/forgefader">ForgeFader</a> project,
bringing it up to par with <a href="https://github.com/jeremytammik/Rvtfader">RvtFader</a>.</p>

<p>It is pretty cool seeing the same functionality implemented in two such different ways, on completely different platforms, using different tools.</p>

<p>Alan Seidel shared another exciting Revit add-in.</p>

<p>First and not least, another exciting topic for me personally is switching to half-time work:</p>

<ul>
<li><a href="#2">Work half</a></li>
<li><a href="#3">AKS Opener</a>
<ul>
<li><a href="#4">Video</a></li>
<li><a href="#5">GitHub repository</a></li>
<li><a href="#6">Why?</a></li>
<li><a href="#7">Specific interest</a></li>
</ul></li>
<li><a href="#8">RvtFader</a></li>
<li><a href="#9">ForgeFader</a></li>
</ul>

<h4><a name="2"></a>Work Half</h4>

<p>Starting April 1, I am working half time.</p>

<p>Unfortunately, April 1 fell on Saturday, we held a team meeting in Gothenburg, and my flight back home with Air Berlin was cancelled, so I ended up working a lot on that specific day, and a weekend, to boot.</p>

<p>I also ended up working more than full time the last two days, finishing off the ForgeFader sample.</p>

<p>Up to me to stop, though.</p>

<p>From now on, my aim is to focus on the Revit question answering system Q4R4, blog, mentor my new colleagues (when they materialise) and spend less time with cases and repetitive questions.</p>

<p>They should be handled automatically by Q4R4 anyway &nbsp; :-)</p>

<h4><a name="3"></a>AKS Opener</h4>

<p>Allan
<a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/540057">@aksaks</a>
<a href="https://github.com/akseidel">@akseidel</a>
Seidel is really churning them out now!</p>

<p>He shared another full-fledged and advanced Revit add-in in
his <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/giving-back-a-workshared-file-to-be-local-file-opener/m-p/6990230">giving back &ndash; a workshared file to be local file opener</a>:</p>

<blockquote>
  <p>In the spirit of giving back for all the help garnered here to experiment with this idea, and also the challenge to make a Revit file opening video slightly more interesting than watching paint dry, here is something that might be useful to some. Its purpose is to be a one click open a Revit workshared file to be a local file without needing further user input and to do so maintaining the local Revit disk storage area organized better than a heap.</p>
  
  <ul>
  <li><a href="https://www.youtube.com/watch?v=oquPOrq7ORA">Video</a> </li>
  <li><a href="https://github.com/akseidel/RevitAKSOpen">Repository</a></li>
  </ul>
</blockquote>

<p>This is a beautiful and advanced sample that provides many important programming pointers.</p>

<p>I love the <code>RevitFileSniffer</code> to read <code>BasicFileInfo</code> directly from binary RVT.
I love all the aspects of it!
Modeless WPF, the works!</p>

<p>Here are Allan's video and repository notes:</p>

<h4><a name="4"></a>AKS Opener Video</h4>

<p><center></p>

<p><a class="asset-img-link"  style="float: left;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c8e9de81970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c8e9de81970b img-responsive" style="width: 32px; margin: 0px 5px 5px 0px;" alt="RevitAKSOpen GenieOpener" title="RevitAKSOpen GenieOpener" src="/assets/image_e86254.jpg" /></a></p>

<p></center></p>

<p>What is more boring than a Revit file opener? A video of opening a Revit file. That was the challenge here; how to show a one click Revit workshared file opener that opens a workshared central file as a local file without requiring any more user input beyond that first click.</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/oquPOrq7ORA?rel=0" frameborder="0" allowfullscreen></iframe>
</center></p>

<h4><a name="5"></a>AKS Opener GitHub Repository</h4>

<p><a href="https://github.com/akseidel/RevitAKSOpen">RevitAKSOpen</a> is
a Revit add-in in C# that creates a custom ribbon tab with a single control for opening workshared Revit files in one click.
The same control is added to the Addins tab.
The controls are modeless, active at all times.</p>

<h4><a name="6"></a>Why?</h4>

<p>Opening a workshared Revit file to be a Local work file is a multistep process.
Revit, furthermore, is sloppy about it.
It trashes the same local folder with all the local files and their overhead files it saves.
Revit also leaves a trap in the Revit desktop when it deposits an icon for the central file one chose.
Of course, one can reopen the saved local file instead of the central file, but some are not so daring and often forget to do the critical initial sync.
Their Revit managers might insist they never use a local to avoid "issues".
Autodesk's publications actually suggest to operate that way. Situations where the Revit coordinator replaces the central file with a copy orphans the local file, so one has to start from the central file again.</p>

<p>Why not present the user with choices from what they have already been working on or point them to choices of the appropriate Revit version to be opened ready to go as a local file stored in an organized, structured pattern while also saving previous work in a similar manner. This add-in does that. It tries, in one click, to automatically pass the various Revit roadblocks and hazards, with added smarts, to properly open a workshared file.      </p>

<p>This add-in demonstrates many of the typical tasks and implementation required for providing a tab menu interface and dealing with files.
A back-burner improvement is to make the user selection landing areas larger, perhaps changing to or adding preview icons to the interface.</p>

<h4><a name="7"></a>Specific Interest</h4>

<p><strong>"Tell Me About It" Mode</strong></p>

<ul>
<li>A function reporting a Revit file's metadata to determine what Revit version it may be. It reports other useful information. This is one way to determine a Revit file's provenance without actually opening the file in Revit.</li>
</ul>

<p><strong>Zero document state operation</strong></p>

<ul>
<li>Having the add-in available to run when there is not already a Revit file open is called zero document state availability. It makes sense for this add-in to have that availability.</li>
</ul>

<p><strong>Writing to the Revit status bar</strong></p>

<ul>
<li>A thing to know. Revit funnels most of its feedback to the status bar. This add-in uses the status bar to be a whimsical check on who is paying attention.</li>
</ul>

<p><strong>Useful mundane file and directory operations for reuse</strong></p>

<ul>
<li>Making local folders identified with the user's initials.</li>
<li>Moving prior local files to a local stash folder.</li>
<li>Indexing the prior local file names so that one can see their generation. This involves the "make a unique name" task one runs into needing in many situations.</li>
<li>Pruning the number of stashed files.</li>
</ul>

<p>This repository is provided for sharing and learning purposes. Perhaps someone might provide improvements or education. Perhaps it will help to boost someone further up the steep learning curve needed to create Revit task add-ins. Hopefully it does not show too much of the wrong way.  </p>

<p>Much of the code is by others. Its mangling and ignorant misuse is my ongoing doing. Much thanks to the professionals like Jeremy Tammik who provided the means directly or by mention one way or another for probably all the code needed.</p>

<p>Many thanks to Allan for putting together and sharing this!</p>

<h4><a name="8"></a>RvtFader</h4>

<p><a href="https://github.com/jeremytammik/Rvtfader">RvtFader</a> is pretty well documented in its GitHub repository.</p>

<p>I described the initial release talking
about <a href="http://thebuildingcoder.typepad.com/blog/2017/03/rvtfader-avf-ray-tracing-and-signal-attenuation.html">RvtFader, AVF, ray tracing and signal attenuation</a>.</p>

<p>I think it provides a cool starting point for any new little application as well, implementing:</p>

<ul>
<li>A nice external little external application with custom ribbon tab, panel, split button, main and settings commands</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb098d0038970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb098d0038970d img-responsive" style="width: 141px; " alt="RvtFader ribbon tab" title="RvtFader ribbon tab" src="/assets/image_dbb69f.jpg" /></a><br /></p>

<p></center></p>

<ul>
<li>Manage settings to be edited and stored in a JSON text file.</li>
<li>Enable user to pick a source point on a floor.</li>
<li>Determine the floor boundaries.</li>
<li>Shoot rays from the picked point to an array of other target points covering the floor.</li>
<li>Determine the obstacles encountered by the ray, specifically wall elements.</li>
<li>Display a 'heat map', i.e. colour gradient, representing the signal loss caused by the distance and number of walls between the source and the target points.</li>
</ul>

<p>It uses the Revit API <code>ReferenceIntersector</code> ray tracing functionality to detect walls and
the <a href="http://thebuildingcoder.typepad.com/blog/avf">analysis visualisation framework AVF</a> to display the heat map.</p>

<p>The result of launching the command and picking a point looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301bb098d003f970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301bb098d003f970d img-responsive" alt="Signal attenuation calculated and displayed by RvtFader" title="Signal attenuation calculated and displayed by RvtFader" src="/assets/image_c7bc1d.jpg" border="0" /></a><br /></p>

<p></center></p>

<h4><a name="9"></a>ForgeFader</h4>

<p><a href="https://github.com/jeremytammik/forgefader">ForgeFader</a> implements
the same functionality as RvtFader in
the <a href="https://forge.autodesk.com">Forge</a>
<a href="https://developer.autodesk.com/en/docs/viewer/v2/overview">viewer</a> environment.</p>

<p>It is an extension app that calculates and displays signal attenuation caused by distance and obstacles in a building model with a floor plan containing walls.</p>

<p>Instead of the Revit API functionality, it makes use of JavaScript
and <a href="https://threejs.org">three.js</a>.</p>

<p>Here is the result of processing the model displayed above in Revit using ForgeFader:</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2743d92970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2743d92970c image-full img-responsive" alt="Signal attenuation calculated and displayed by ForgeFader" title="Signal attenuation calculated and displayed by ForgeFader" src="/assets/image_9bec07.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>The four-minute <a href="https://youtu.be/78JlGnf49mc">ForgeFader Autodesk Forge sample app</a> YouTube video explains some of the background and shows this sample app live in action:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/78JlGnf49mc?rel=0" frameborder="0" allowfullscreen></iframe>
</center></p>

<p>So far, I discussed two implementation steps here on The Building Coder; and a third important step was contributed by Cyrille Fauvel:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/adding-custom-geometry-to-the-forge-viewer.html">Adding custom geometry to the Forge viewer</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/03/threejs-raytracing-in-the-forge-viewer.html">Three.js raytracing in the Forge Viewer</a></li>
<li><a href="https://github.com/jeremytammik/forgefader#implementing-a-custom-shader-in-the-forge-viewer">Implementing a custom shader in the Forge Viewer</a></li>
</ul>

<p>ForgeFader is based 
on <a href="https://github.com/leefsmp">Philippe Leefsma</a>'s 
<a href="https://github.com/Autodesk-Forge/forge-react-boiler.nodejs">Forge React boilerplate sample</a>.
Please refer to that for more details on the underlying architecture and components used.</p>

<p>Now the time is more than overdue for me to stop working and get out and do something else!</p>

<p>Already half-way through the third day of the week, and sitting here blogging...</p>
