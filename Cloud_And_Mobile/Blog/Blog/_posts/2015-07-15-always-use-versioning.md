---
layout: "post"
title: "Always use versioning"
date: "2015-07-15 15:26:57"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/07/always-use-versioning.html "
typepad_basename: "always-use-versioning"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>Each time a new version of the viewer comes out it provides some new functionality that you can take advantage of. Unfortunately, sometimes in order to implement those, certain things need to be reorganised and those changes might break your code.</p>
<p>The way to avoid your web app being broken when a new viewer version comes out, is specifying the version number that your application is compatible with. This option has been introduced in version <strong>0.1.86</strong></p>
<p>So when you link to the <strong>CSS</strong> and <strong>JS</strong> files of the viewer, simply add a "<strong>v</strong>" parameter to the <strong>URL</strong> and specify the version number. E.g. in case we want to use version <strong>0.1.86</strong> we would use this in our html file:</p>
<pre style="overflow-x:scroll;">&lt;<span style="color: #0000bf;">link</span> <span style="color: #0000ff;">type=</span><span style="color: #007f40;">"text/css"</span> <span style="color: #0000ff;">rel=</span><span style="color: #007f40;">"stylesheet"</span> 
<span style="color: #0000ff;">href=</span><span style="color: #007f40;">"https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css?<strong>v=0.1.86</strong>"</span>/&gt; <br />&lt;<span style="color: #0000bf;">script</span> <span style="color: #0000ff;">src=</span><span style="color: #007f40;">"https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js?<strong>v=0.1.86</strong>"</span>&gt;&lt;/<span style="color: #0000bf;">script</span>&gt;</pre>
<p>This way it's up to you when you move to the latest version of the viewer.</p>
<p>Note: If you did not provide a version then you'll also get this message in the browser's <strong>Console</strong> window:</p>
<pre>No viewer version specified, will implicitly use &lt;latest version&gt;</pre>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7aedd3d970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7aedd3d970b img-responsive" title="Viewerversion" src="/assets/image_28db88.jpg" alt="Viewerversion" border="0" /></a></p>
<p>You can find some info about the improvements that were added in each version here:&nbsp;<a title="" href="http://forums.autodesk.com/t5/view-and-data-api/release-history/td-p/5570580" target="_self">http://forums.autodesk.com/t5/view-and-data-api/release-history/td-p/5570580</a></p>
