---
layout: "post"
title: "Ace Editor for Three.js ShaderMaterials in the Forge Viewer"
date: "2016-08-15 17:55:45"
author: "Michael Ge"
categories:
  - "Javascript"
  - "Michael Ge"
  - "THREE.js"
  - "Viewer"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/ace-editor-for-threejs-shadermaterials-in-the-forge-viewer.html "
typepad_basename: "ace-editor-for-threejs-shadermaterials-in-the-forge-viewer"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c887721f970b-pi" style="display: inline;"><img alt="Thumb" class="asset  asset-image at-xid-6a0167607c2431970b01b7c887721f970b img-responsive" height="201" src="/assets/image_101c81.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Thumb" width="420" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092ac766970d-pi" style="float: left;"><br /><br /><br /></a></p>
<p>By Michael Ge (<a href="https://twitter.com/hahakumquat">@hahakumquat</a>)</p>
<p>Reloading the viewer every time you make a change can be a hassle. Fortunately, &#0160;we can skip the refresh time by updating the scene&#39;s code directly within the viewer.</p>
<p>Here is an extension that implements a dynamically sizing&#0160;<a href="https://ace.c9.io/">Ace Editor</a>&#0160;within the viewer. In this case, I used the editor to write WebGL&#0160;shader code that would update a selected fragment&#39;s material.</p>
<p><strong>Creating the&#0160;Editor Panel</strong></p>
<p><a href="http://adndevblog.typepad.com/cloud_and_mobile/2015/05/screenshot-extension-manager-for-the-viewer.html">Philippe&#39;s post</a>&#0160;does a good job explaining how to make a panel from a DockingPanel. From there, it&#39;s just a matter of injecting the editor. Simply calling&#0160;<em>ace.edit(&quot;containerId&quot;) </em>will create an editor (wrapped in a div)&#0160;within the&#0160;<em>containerId</em> div.</p>
<p>The Ace Editor has a <em>.resize()</em> function that resizes an editor to its parent element&#39;s size. We can thus use this to our advantage by calling <em>.resize()</em> every time&#0160;<em>containerId</em> is resized.&#0160;Here is a good&#0160;<a href="https://jsfiddle.net/1x9pgn2u/1/">jsfiddle</a>&#0160;that shows the correct DOM hierarchy. In order to check for a resize event, I used the following script which resizes whenever the mouse is down and moving:</p>
<script src="https://gist.github.com/hahakumquat/c1b8b519464c8d7dca6de0e844829bae.js"></script>
<p><strong>Validating the Shaders</strong></p>
<p>Since the ace editor doesn&#39;t come with built-in GLSL error checking, I found a workaround by&#0160;<a href="http://shdr.bkcore.com/">bkCore&#39;s shader editor example</a>&#0160;that compiles a test WebGL shader and prints out the errors. Some&#0160;issues I ran into were&#0160;errors that Three.js handles when creating a ShaderMaterial, so I did some error checking to parse out unnecessary errors.</p>
<script src="https://gist.github.com/hahakumquat/f695c41841060a8529470dd93ae38377.js"></script>
<p><strong>Displaying the Shaders</strong></p>
<p>Displaying the shaders uses a similar function I made for a heatmap demo I made a while back:</p>
<script src="https://gist.github.com/hahakumquat/21feccdaf27a60d0c3377c2a1b0a0cdd.js"></script>
<p>An interesting thing to note is that, in my demo, I allowed the user to input uniforms via a javascript JSON object and running the string with an&#0160;<em>eval&#0160;</em>call. I&#39;d typically suggest against doing that, as such a feature might leave your website vulnerable to&#0160;<a href="http://stackoverflow.com/questions/13167403/is-javascript-eval-so-dangerous">malicious attacks</a>.</p>
<p>That said, the demo poses some interesting applications. I&#39;ll be looking into writing entire extensions within viewer panels like these as well that affect not only the textures, but also the geometry and other scene states as well.</p>
<p>View the complete sample <a href="https://github.com/hahakumquat/shader-editor">here.</a></p>
