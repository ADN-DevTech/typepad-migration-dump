---
layout: "post"
title: "Using VASA with Forma via Dynamo"
date: "2024-03-27 15:49:45"
author: "Kean Walmsley"
categories:
  - "Autodesk Research"
  - "Dynamo"
  - "User interface"
  - "VASA"
original_url: "https://www.keanw.com/2024/03/using-vasa-with-forma-via-dynamo.html "
typepad_basename: "using-vasa-with-forma-via-dynamo"
typepad_status: "Publish"
---

<p>I’ve been talking quite a bit about our use of VASA inside Forma via our WebAssembly build of the toolkit (which is still not currently available publicly). I’ve done quite a lot of work to improve that particular prototype and I’m happy to say it’s shaping up nicely: the UX is much better than it was, for instance.</p>
<p><a href="/assets/image_670551.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="The latest VASA in Forma prototype extension" src="/assets/image_670551.jpg" alt="The latest VASA in Forma prototype extension" width="500" height="375" border="0" /></a></p>
<p>I’ll be talking about our work on this at an upcoming Autodesk-internal tech conference, and as part of the preparation for this I’ve spent a few days building a pure Dynamo equivalent of the extension. VASA’s Dynamo package is available in the Dynamo Package Manager - which means it can be used in the various Dynamo environments, whether standalone or integrated with Revit, Civil 3D or Forma. This means it can be used today by Forma customers wanting to implement custom voxel-based analysis workflows.</p>
<p>Here’s a high-level view of the current state of the “VASA in Forma” Dynamo graph:</p>
<p><a href="/assets/image_109749.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="The Dynamo graph" src="/assets/image_109749.jpg" alt="The Dynamo graph" width="500" height="375" border="0" /></a>The graph pulls down geometry from Forma and analyses it using VASA inside Dynamo. Here you can see a visibility dome, a shortest path and some view cones from the windows of a newly designed building.</p>
<p><a href="/assets/image_719103.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="VASA inside Dynamo" src="/assets/image_719103.jpg" alt="VASA inside Dynamo" width="500" height="375" border="0" /></a></p>
<p>When the graph executes this geometry gets previewed inside Forma, ready for import.</p>
<p><a href="/assets/image_648324.jpg" target="_blank" rel="noopener"><img style="display: block; margin: 30px auto;" title="VASA results in Forma" src="/assets/image_648324.jpg" alt="VASA results in Forma" width="500" height="375" border="0" /></a></p>
<p>The workflow is clearly a little less streamlined than having a fully integrated extension, but obviously also has more potential for end-user customization: you could extend or adapt this graph in any number of ways that suit your particular workflow needs. I’m still working on cleaning the graph up before publication - if you have an urgent need for it, please post a comment and I’ll find a way to get it to you.</p>
