---
layout: "post"
title: "Rotating View Cube with JavaScript in the Viewer"
date: "2016-07-20 20:30:51"
author: "Shiya Luo"
categories:
  - "Shiya Luo"
  - "Viewer"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/07/rotating-view-cube-with-javascript-in-the-viewer.html "
typepad_basename: "rotating-view-cube-with-javascript-in-the-viewer"
typepad_status: "Publish"
---

<p>By <a href="https://twitter.com/ShiyaLuo">@ShiyaLuo</a></p>

<p>This is a question we've gotten from partners pretty often. How to programmatically rotate the View Cube?</p>

<p>If you don't want to use THREE.js camera transformations, and just want to call functions to turn the "View Cube" like in a traditional CAD software, there's a function you can call.</p>

<p>In the current version (2.9) of viewer3D, you can call the <code>viewer.setViewCube(face)</code> where <code>viewer</code> is your - DOH - viewer.
The options for <code>face</code> are strings <code>front</code>, <code>back</code>, <code>top</code>, <code>bottom</code>, <code>left</code>, <code>right</code>. Calling this function does exactly the same as clicking on the different surface of a View Cube.</p>

<p>Enjoy!</p>
