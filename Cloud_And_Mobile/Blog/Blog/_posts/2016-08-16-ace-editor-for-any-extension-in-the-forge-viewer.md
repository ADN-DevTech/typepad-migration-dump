---
layout: "post"
title: "Ace Editor for Any Extension in the Forge Viewer"
date: "2016-08-16 14:46:54"
author: "Michael Ge"
categories:
  - "Michael Ge"
  - "View and Data API"
  - "Viewer"
  - "Web Development"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2016/08/ace-editor-for-any-extension-in-the-forge-viewer.html "
typepad_basename: "ace-editor-for-any-extension-in-the-forge-viewer"
typepad_status: "Publish"
---

<p>By Michael Ge (<a href="https://twitter.com/hahakumquat">@hahakumquat</a>)</p>
<p>As a <a href="http://adndevblog.typepad.com/cloud_and_mobile/2016/08/ace-editor-for-threejs-shadermaterials-in-the-forge-viewer.html">followup to yesterday&#39;s post</a>, here is an Ace Editor extension that allows you to write and test extensions! Gone are the days of including extensions in HTML files. Finally, we can run extensions, even the ones discussed on this blog, just by copy-and-pasting into the editor.</p>
<p>Before we take a look, just a warning that, as always, using eval() is a dangerous design decision on a public website! This is more for developers to try out extensions themselves.</p>
<p>Some key points in the code. I assumed the name of the extension was always &quot;test&quot;. Here is how you remove an old extension and load a new one from the Ace Editor:</p>
<script src="https://gist.github.com/hahakumquat/52e859c9c6dd44025c7c93d75d5ae472.js"></script>
<p>Many people might be using Philippe&#39;s Viewer Factory to create the Viewer in an index.js file. If you want debug information as found in this demo, you&#39;ll have to modify the index file to define an&#0160;<em>eventCallback&#0160;</em>function in the viewer&#39;s options.</p>
<script src="https://gist.github.com/hahakumquat/1be4e0e2b9c37c6ec886e93db6d6cc46.js"></script>
<p>Someone&#0160;<em>please</em> write an (extension that makes an (extension that makes an extension)) and tell me how it goes.</p>
<p>Source&#0160;<a href="https://github.com/hahakumquat/extension-editor">here!</a></p>
