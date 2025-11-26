---
layout: "post"
title: "Delete Buttons on Default Toolbar in Autodesk Viewer"
date: "2016-08-30 05:37:04"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/delete-buttons-on-default-toolbar-in-autodesk-viewer.html "
typepad_basename: "delete-buttons-on-default-toolbar-in-autodesk-viewer"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/ShiyaLuo">@ShiyaLuo</a></p>

<p>If you instantiate a basic viewer with the <code>Autodesk.Viewing.Private.GuiViewer3D</code> class, you'll get an array of buttons like this:</p>

<p><img src="/assets/image_1f1ba8.jpg" alt="Autodesk Toolbar image" /></p>

<p>This is the toolbar at Viewer version 2.10.</p>

<p>However, if there are unwanted functionalities on the bar, you can disable it using the following lines of code.</p>

<p><code>var control = toolbar.getControl('modelTools');</code></p>

<p><code>control.removeControl('toolbar-explodeTool');</code></p>

<p>This will get rid of the explode button in the default Viewer toolbar.</p>

<p>Happy coding!</p>
